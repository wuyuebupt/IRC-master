===========================================================================
This sample code shows how to wrap and convert visual recognition functions 
implemented in a native DLL to a web service in Prajna Hub. 
It is tested with Visual Studio 2015 and Windows 8.1.
===========================================================================
1) IRC.SampleRecogServerCSharp/:
	CSharp sample code
2) ImageClassifierLib/:
	Image classification library. Should be replaced by your own classification code. 
3) ImageClassifierDllWrapper/:
	Wrapper of native C++ code, to be called in “IRC.SampleRecogServerCSharp”
===========================================================================
To build and run the sample code in Windows:
You can build the solution IRC.sln in Visual Studio 2013, and run IRC.SampleRecogServerCSharp.

1) Open the solution file IRC.sln in Visual Studion 2013
2) Build IRC.SampleRecogServerCSharp
3) Run SampleRecogServerCSharp to register your classifier
    Under folder: .\IRC.Windows.Call.DLL\bin\Debug
    Run:  IRC.SampleRecogServerCSharp.exe
    
   
