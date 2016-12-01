using System;
using System.Linq;

namespace IRC.SampleRecogCmdlineCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() > 0)
            {
                string imgFileName = args[0];
                // place your recogizion code here
                
                // The following message will be captured by IRC.SampleRecogServerCSharp. 
                // Please don't output any message other than the following result information.
                Console.Write("tag1:0.95;tag2:0.32;tag3:0.05;tag4:0.04;tag5:0.01");
            }
        }
    }
}
