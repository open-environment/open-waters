@ECHO OFF
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%

echo Installing Open Waters Service...
echo ---------------------------------------------------
InstallUtil C:\OpenWatersSvc\OpenWatersSvc.exe
echo ---------------------------------------------------
PAUSE
echo Done.
