echo off
if not exist %1.nuspec goto NoNuspecFile

set OriginalPackageVersion=
for /f "usebackq tokens=2" %%v in (`nuget list %1 -source %LOCALNUGET%`) do set OriginalPackageVersion=%%v
echo OriginalPackageVersion for %1 = %OriginalPackageVersion%

set PackageMajor=&& set PackageMinor=&& set PackageBuildNumber=
for /F "tokens=1,2,3 delims=." %%a in ("%OriginalPackageVersion%") do (set Major=%%a&& set /a Minor=%%b+1&& set BuildNumber=0)
echo Packing %1 for version %Major%.%Minor%.%BuildNumber%

del %1.*.nupkg
nuget pack %1.nuspec -properties version=%Major%.%Minor%.%BuildNumber%

echo Publishing %1
for %%n in (%1.*.nupkg) do nuget add %%n -source %LOCALNUGET%
exit /B

:NoNuspecFile
echo Provide nuspec file name, without .nuspec