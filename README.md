# Image Recognition Challenge (IRC) Sample Codes
This repository is for providing sample codes for IRC Challenge. It includes binaries of Prajna libraries and makes the build easy and self-contained.

The repository includes samples for both Windows and Linux, by calling visual recognition functions from either a dynamic library or an executable.

- `IRC.Linux` - Call dynamic library in Linux
- `IRC.Windows.Call.DLL` - Call dynamic library (DLL) in Windows
- `IRC.Windows.Call.Cmdline` - Call executable in Windows

# Development
## Requirements
- VS 2015
- `git` and `git-lfs`

## Repository Setup
Run the following line in Powershell (Windows) or Bash (Linux)

    git clone git@github.com:MSRCCS/IRC.git
    
Then you should be able to build `IRC.sln` using Visual Studio.
