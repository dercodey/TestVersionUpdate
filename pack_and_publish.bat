echo off
if not exist %1.nuspec goto NoNuspecFile

set OriginalPackageVersion=
for /f "usebackq tokens=2" %%v in (`nuget list %1 -source c:\nuget_feed`) do set OriginalPackageVersion=%%v
echo OriginalPackageVersion for %1 = %OriginalPackageVersion%

set PackageMajor=&& set PackageMinor=&& set PackageBuildNumber=
for /F "tokens=1,2,3 delims=." %%a in ("%OriginalPackageVersion%") do (set Major=%%a&& set Minor=%%b&& set /a BuildNumber=%%c+1)
echo Packing %1 for version %Major%.%Minor%.%BuildNumber%

del %1.*.nupkg
nuget pack %1.nuspec -properties version=%Major%.%Minor%.%BuildNumber%

echo Publishing %1
for %%n in (%1.*.nupkg) do nuget add %%n -source c:\nuget_feed
exit /B

:NoNuspecFile
echo Provide nuspec file name, without .nuspec