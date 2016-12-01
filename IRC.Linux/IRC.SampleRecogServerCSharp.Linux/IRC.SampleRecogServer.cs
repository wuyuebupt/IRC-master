using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Prajna.Tools;
using Prajna.Service.ServiceEndpoint;
using VMHub.Data;
using VMHub.ServiceEndpoint;
using Prajna.Service.CSharp;

namespace SampleRecogServerCSharp
{

    public class SampleRecogInstanceCSharp : VHubBackEndInstance<VHubBackendStartParam>
    {

        //!!!IMPORTANT!!! set LD_LIBRARY_PATH in Linux   e.g.:   export LD_LIBRARY_PATH=.:$LD_LIBRARY_PATH    
        [DllImport("libNativeWrapper.so", EntryPoint = "Init")]
        static extern IntPtr Init();

        [DllImport("libNativeWrapper.so", EntryPoint = "Predict")]
        static extern void Predict(IntPtr handle, string imgFileName, StringBuilder predRes);

        [DllImport("libNativeWrapper.so", EntryPoint = "Release")]
        static extern void Release(IntPtr handle);

        static IntPtr classifierHandle;

        static string exeDir;
        static int numImageRecognized = 0;

        // Please set appropriate values for the following variables for a new Recognition Server.
        // ProviderName and ProviderGuid should be obtained from jinl@microsoft.com.
        // recognizerName means the name of this recognizer, and should be unique for each participant team. 
        //     We sugguest you use "recognizer_name@team_name" to name your recognizer.
        //     For example, dog@msr (for dog breed recognition from MSR).
        static string providerName = "DRNfLSCR";
        static string providerGuidStr = "179ec63b-7a6a-44e7-9806-ebad687258c0";
        static string recognizerName = "celebrity@DRNfLSCR";

        public static SampleRecogInstanceCSharp Current { get; set; }
        public SampleRecogInstanceCSharp() :
            /// Fill in your recognizing engine name here
            base(providerName)
        {
            /// CSharp does support closure, so we have to pass the recognizing instance somewhere, 
            /// We use a static variable for this example. But be aware that this method of variable passing will hold a reference to SampleRecogInstanceCSharp

            SampleRecogInstanceCSharp.Current = this;
            Func<VHubBackendStartParam, bool> del = InitializeRecognizer;
            this.OnStartBackEnd.Add(del);

            var exeName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            SampleRecogInstanceCSharp.exeDir = Path.GetDirectoryName(exeName);
        }

        /// <summary>
        /// This function will be called only once when the this backend recognition server is started
        /// </summary>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static bool InitializeRecognizer(VHubBackendStartParam pa)
        {
            var bInitialized = true;
            var x = SampleRecogInstanceCSharp.Current;
            if (!Object.ReferenceEquals(x, null))
            {
                /// <remarks>
                /// To implement your own image recognizer, please obtain a connection Guid by contacting jinl@microsoft.com
                /// </remarks> 
                Guid providerGUID = new Guid(providerGuidStr);
                x.RegisterAppInfo(providerGUID, "0.0.0.1");
                Func<Guid, int, RecogRequest, RecogReply> del = PredictionFunc;
                Trace.WriteLine("****************** Register TeamName: " + providerName + " provider GUID: " + providerGUID.ToString() + " recognizerName: " + recognizerName);
                /// <remarks>
                /// Register your prediction function here, which is PredictionFunc(...). 
                /// </remarks> 
                x.RegisterClassifierCS(recognizerName, Path.Combine(exeDir, "logo.jpg"), 100, del);

                classifierHandle = Init();

            }
            else
            {
                bInitialized = false;
            }
            return bInitialized;
        }

        /// <summary>
        /// This function will be called once a client app sends a request by specifying the service GUID,
        /// which correponds to the recognizerName.
        /// </summary>
        /// <param name="id">the request GUID</param>
        /// <param name="timeBudgetInMS">time budget to finish the recognition task</param>
        /// <param name="req">the request data</param>
        /// <returns></returns>
        public static RecogReply PredictionFunc(Guid id, int timeBudgetInMS, RecogRequest req)
        {
            // imgBuf points to the image stream sent from a client app
            byte[] imgBuf = req.Data;

            // save image to file as required by some recognition functions
            // but this step is not necessary if your function can directly process image data in memory
            byte[] imgType = System.Text.Encoding.UTF8.GetBytes("jpg");
            Guid imgID = BufferCache.HashBufferAndType(imgBuf, imgType);
            string imgFileName = "images/" + imgID.ToString() + ".jpg";

            string filename = Path.Combine( exeDir, imgFileName );
            if (!File.Exists(filename))
                FileTools.WriteBytesToFileConcurrent(filename, imgBuf);

            // call your prediction function
            StringBuilder sb = new StringBuilder(256);
            Predict(classifierHandle, filename, sb);

            // do not cache the file for the privacy issue
            File.Delete(filename);

            string resultString = sb.ToString();
            
            numImageRecognized++;
            Console.WriteLine("Image {0}: {1}", numImageRecognized, resultString);
            return VHubRecogResultHelper.FixedClassificationResult(resultString, resultString);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var usage = @"
    Usage: Launch a local instance of IRC.SampleRecogServerCSharp.
    Command line arguments:
        -gateway     SERVERURI       ServerUri
";
            List<string> args_list = args.ToList();
            args_list.Add("-con");
            args = args_list.ToArray();
            var parse = new ArgumentParser(args);
            var usePort = VHubSetting.RegisterServicePort;
            var gatewayServers = parse.ParseStrings("-gateway", new string[] { "vm-hubr.trafficmanager.net" });
            var serviceName = "IRC.SampleRecognitionServiceCS";

            if (!parse.AllParsed(usage))
                return;

            // prepare parameters for registering this recognition instance to vHub gateway
            var startParam = new VHubBackendStartParam();
            /// Add traffic manager gateway, see http://azure.microsoft.com/en-us/services/traffic-manager/, 
            /// Gateway that is added as traffic manager will be repeatedly resovled via DNS resolve
            foreach (var gatewayServer in gatewayServers)
            {
                if (!(StringTools.IsNullOrEmpty(gatewayServer)))
                    startParam.AddOneTrafficManager(gatewayServer, usePort);
            };

            // start a local instance. 
            Console.WriteLine("Local instance started and registered to {0}", gatewayServers[0]);
            Console.WriteLine("Current working directory: {0}", Directory.GetCurrentDirectory());
            Console.WriteLine("Press ENTER to exit");
            RemoteInstance.StartLocal(serviceName, startParam, () => new SampleRecogInstanceCSharp());
            while (RemoteInstance.IsRunningLocal(serviceName))
            {
                if (Console.KeyAvailable)
                {
                    var cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("ENTER pressed, exiting...");
                        RemoteInstance.StopLocal(serviceName);
                    }
                    else
                        System.Threading.Thread.Sleep(10);
                }
            }
        }
    }
}
