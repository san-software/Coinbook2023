set build = "C:\Program Files\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\msbuild.exe"
set ModulVerwaltung = Coinbook.ModulVerwaltung.Plugin\Plugin\bin\
set Configuration = Debug
set netreaktor = "C:\Program Files\Eziriz\.NET Reactor\dotNET_Reactor.Console.exe"

%build% Coinbook.sln /property:Configuration=%Configuration%
rem %build% Coinbook.Import\Coinbook.Import.sln /property:Configuration=%Configuration%
rem %build% Coinbook.Konvert\Coinbook.Konvert.sln /property:Configuration=%Configuration%
%build% Coinbook.ModulVerwaltung.Plugin\Coinbook.ModulVerwaltung.Plugin.sln /property:Configuration=%Configuration%

xcopy %ModulVerwaltung%\%Configuration%\Coinbook.ModulVerwaltung.dll Coinbook\bin\%Configuration%\Plugins /E /F /Y
xcopy %ModulVerwaltung%\%Configuration%\LastschriftMandat.docx Coinbook\bin\%Configuration%\Plugins /E /F /Y
xcopy %ModulVerwaltung%\%Configuration%\Lokalisation Coinbook\bin\%Configuration%\Plugins /E /F /Y

pause
goto ende

%netreaktor% -project Coinbook.nrproj" -targetfile 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\netReaktor\<AssemblyFileName>" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\Coinbook.Enumerations.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\Coinbook.Lokalisierung.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\Coinbook.Model.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\Coinbook.Sprache.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\LiteDB.Database.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\OleDB.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\SAN.Converter.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\SplashScreen.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\Coinbook.Import.exe" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\Coinbook.Konvert.exe" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\Coinbook.ModulVerwaltung.exe" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\AutoClosingMessageBox.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\AutoUpdater.NET.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\SAN.FileDownloader.dll" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\SAN.FTP.exe" -satellite_assemblies 
"E:\Coinbook 2020\Coinbook-2021.1\Coinbook\bin\%Configuration%\SAN.UI.DataGridView.dll" -satellite_assemblies

rem xcopy Coinbook\bin\%Configuration%\netReaktor\*.* Coinbook\bin\%Configuration% /E /F /Y

rem rd Coinbook\bin\%Configuration%\netReaktor /s/q
pause

ende:

