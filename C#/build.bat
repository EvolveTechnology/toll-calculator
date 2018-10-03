@echo off
mkdir obj

SET PATH=C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin;%PATH%

SET "VSCMD_START_DIR=%CD%"
CALL "C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat" amd64

mkdir Output
csc.exe -out:Output/CalculateCarToll.exe *.cs Tests/*.cs Vehicles/*.cs