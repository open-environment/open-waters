REM The following directory is for .NET 4.0
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%

echo Installing OpenWatersSvc...
echo ---------------------------------------------------
InstallUtil C:\OpenWatersSvcPub\OpenWatersSvc.exe
echo ---------------------------------------------------
echo Done.
PAUSE