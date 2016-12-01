===========================================================================
This sample code shows how to wrap and convert visual recognition functions
implemented in a dynamic library in Linux to a web service in Prajna Hub. 
It is tested with Mono 4.0.1 and Ubuntu Server 14.04
To install mono, please refer to
http://www.mono-project.com/docs/getting-started/install/linux/
===========================================================================
1) IRC.SampleRecogServerCSharp.Linux/:
	CSharp sample code
2) ImgClassifier/:
	Image classification library. Should be replaced by your own classification code. 
3) NativeWrapper.cpp
	Wrapper of native Linux code, to be called in “SampleRecogServerCSharp.Linux”
===========================================================================
To build and run the sample code in Linux:

1) Build IRC.SampleRecogServerCSharp.Linux, you can either (1) build it by visual studio in Windows or (2) build it in Linux by xbuild
2) Build NativeWrapper and ImgClassifier:
    $   cd ./IRC.Linux/ 
    $   make
   The compiled shared libraries are copied to "IRC.SampleRecogServerCSharp.Linux/bin/Debug"
3) Set $LD_LIBRARY_PATH
    $   export LD_LIBRARY_PATH=.:$LD_LIBRARY_PATH

4) Run SampleRecogServerCSharp to register your classifier
    $   cd ./IRC.SampleRecogServerCSharp.Linux/bin/Debug
    $   mono IRC.SampleRecogServerCSharp.Linux.exe
    
   
