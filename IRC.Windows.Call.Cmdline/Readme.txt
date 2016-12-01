===========================================================================
This sample code shows how to wrap and convert visual recognition functions 
implemented in an EXE to a web service in Prajna Hub.
The sample can be used if you want to register to Prajna Hub a recognition 
module implemented in Matlab or Python. 
It is tested with Visual Studio 2015 and Windows 8.1.
===========================================================================
1) IRC.SampleRecogServerCSharp/:
	CSharp sample code
2) IRC.SampleRecogCmdlineCSharp/:
	A simple console application to be called by “IRC.SampleRecogServerCSharp” 
	Should be replaced by your own code. 
===========================================================================
To build and run the sample code in Windows:
You can build the solution IRC.sln in Visual Studio 2013, and run IRC.SampleRecogServerCSharp.

1) Open the solution file IRC.sln in Visual Studion 2013
2) Build IRC.SampleRecogServerCSharp
3) Run SampleRecogServerCSharp to register your classifier
    Under folder: .\IRC.Windows.Call.Cmdline\bin\Debug
    Run:  IRC.SampleRecogServerCSharp.exe
    
   
