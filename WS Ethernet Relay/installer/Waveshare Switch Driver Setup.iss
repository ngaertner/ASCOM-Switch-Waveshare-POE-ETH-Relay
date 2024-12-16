;
; Script generated by the ASCOM Driver Installer Script Generator 6.6.0.0
; Generated by Nico Gaertner - AstroNico on 12/1/2024 (UTC)
;
[Setup]
AppID={{7fa74dd0-fd90-4b28-bc0b-b06262d0b770}
AppName=ASCOM AstroNico Waveshare Switch Driver Switch Driver
AppVerName=ASCOM AstroNico Waveshare Switch Driver Switch Driver 0.0.3
AppVersion=0.0.4
AppPublisher=Nico Gaertner - AstroNico <n/a>
AppPublisherURL=mailto:n/a
AppSupportURL=https://ascomtalk.groups.io/g/Help
AppUpdatesURL=https://ascom-standards.org/
VersionInfoVersion=1.0.0
MinVersion=6.1.7601
DefaultDirName="{cf}\ASCOM\Switch\Waveshare Switch Driver\"
DisableDirPage=yes
DisableProgramGroupPage=yes
OutputDir="."
OutputBaseFilename="Waveshare Switch Driver Setup"
Compression=lzma
SolidCompression=yes
; Put there by Platform if Driver Installer Support selected
WizardImageFile="C:\Users\ngaer\source\repos\WS Ethernet Relay\WS Ethernet Relay\installer\installer.bmp"
LicenseFile="C:\Users\ngaer\source\repos\WS Ethernet Relay\LICENSE.txt"
; {cf}\ASCOM\Uninstall\Switch folder created by Platform, always
UninstallFilesDir="{cf}\ASCOM\Uninstall\Switch\Waveshare Switch Driver"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
Name: "{cf}\ASCOM\Uninstall\Switch\Waveshare Switch Driver"
; TODO: Add subfolders below {app} as needed (e.g. Name: "{app}\MyFolder")

[Files]
Source: "C:\Users\ngaer\source\repos\WS Ethernet Relay\WS Ethernet Relay\bin\Release\ASCOM.Waveshare_Modbus_POE_ETH_Relay.exe"; DestDir: "{app}" ;AfterInstall: RegASCOM()
; Require a read-me HTML to appear after installation, maybe driver's Help doc
//Source: "C:\Users\ngaer\source\repos\WS Ethernet Relay\WS Ethernet Relay\bin\Release\readme.txt"; DestDir: "{app}"; Flags: isreadme
; TODO: Add other files needed by your driver here (add subfolders above)

;Only if COM Local Server
[Run]
Filename: "{app}\ASCOM.Waveshare_Modbus_POE_ETH_Relay.exe"; Parameters: "/regserver"




;Only if COM Local Server
[UninstallRun]
Filename: "{app}\ASCOM.Waveshare_Modbus_POE_ETH_Relay.exe"; Parameters: "/unregserver"



;  DCOM setup for COM local Server, needed for TheSky
[Registry]
; TODO: If needed set this value to the Switch CLSID of your driver (mind the leading/extra '{')
#define AppClsid "{{7fa74dd0-fd90-4b28-bc0b-b06262d0b770}"

; set the DCOM access control for TheSky on the Switch interface
Root: HKCR; Subkey: CLSID\{#AppClsid}; ValueType: string; ValueName: AppID; ValueData: {#AppClsid}
Root: HKCR; Subkey: AppId\{#AppClsid}; ValueType: string; ValueData: "ASCOM Waveshare Switch Driver Switch Driver"
Root: HKCR; Subkey: AppId\{#AppClsid}; ValueType: string; ValueName: AppID; ValueData: {#AppClsid}
Root: HKCR; Subkey: AppId\{#AppClsid}; ValueType: dword; ValueName: AuthenticationLevel; ValueData: 1
; set the DCOM key for the executable as a whole
Root: HKCR; Subkey: AppId\ASCOM.Waveshare_Modbus_POE_ETH_Relay.exe; ValueType: string; ValueName: AppID; ValueData: {#AppClsid}
; CAUTION! DO NOT EDIT - DELETING ENTIRE APPID TREE WILL BREAK WINDOWS!
Root: HKCR; Subkey: AppId\{#AppClsid}; Flags: uninsdeletekey
Root: HKCR; Subkey: AppId\ASCOM.Waveshare_Modbus_POE_ETH_Relay.exe; Flags: uninsdeletekey

[Code]
const
   REQUIRED_PLATFORM_VERSION = 6.2;    // Set this to the minimum required ASCOM Platform version for this application

//
// Function to return the ASCOM Platform's version number as a double.
//
function PlatformVersion(): Double;
var
   PlatVerString : String;
begin
   Result := 0.0;  // Initialise the return value in case we can't read the registry
   try
      if RegQueryStringValue(HKEY_LOCAL_MACHINE_32, 'Software\ASCOM','PlatformVersion', PlatVerString) then 
      begin // Successfully read the value from the registry
         Result := StrToFloat(PlatVerString); // Create a double from the X.Y Platform version string
      end;
   except                                                                   
      ShowExceptionMessage;
      Result:= -1.0; // Indicate in the return value that an exception was generated
   end;
end;

//
// Before the installer UI appears, verify that the required ASCOM Platform version is installed.
//
function InitializeSetup(): Boolean;
var
   PlatformVersionNumber : double;
 begin
   Result := FALSE;  // Assume failure
   PlatformVersionNumber := PlatformVersion(); // Get the installed Platform version as a double
   If PlatformVersionNumber >= REQUIRED_PLATFORM_VERSION then	// Check whether we have the minimum required Platform or newer
      Result := TRUE
   else
      if PlatformVersionNumber = 0.0 then
         MsgBox('No ASCOM Platform is installed. Please install Platform ' + Format('%3.1f', [REQUIRED_PLATFORM_VERSION]) + ' or later from https://www.ascom-standards.org', mbCriticalError, MB_OK)
      else 
         MsgBox('ASCOM Platform ' + Format('%3.1f', [REQUIRED_PLATFORM_VERSION]) + ' or later is required, but Platform '+ Format('%3.1f', [PlatformVersionNumber]) + ' is installed. Please install the latest Platform before continuing; you will find it at https://www.ascom-standards.org', mbCriticalError, MB_OK);
end;

// Code to enable the installer to uninstall previous versions of itself when a new version is installed
procedure CurStepChanged(CurStep: TSetupStep);
var
  ResultCode: Integer;
  UninstallExe: String;
  UninstallRegistry: String;
begin
  if (CurStep = ssInstall) then // Install step has started
	begin
      // Create the correct registry location name, which is based on the AppId
      UninstallRegistry := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#SetupSetting("AppId")}' + '_is1');
      // Check whether an extry exists
      if RegQueryStringValue(HKLM, UninstallRegistry, 'UninstallString', UninstallExe) then
        begin // Entry exists and previous version is installed so run its uninstaller quietly after informing the user
          MsgBox('Setup will now remove the previous version.', mbInformation, MB_OK);
          Exec(RemoveQuotes(UninstallExe), ' /SILENT', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);
          sleep(1000);    //Give enough time for the install screen to be repainted before continuing
        end
  end;
end;

//
// Register and unregister the driver with the Chooser
// We already know that the Helper is available
//


procedure RegASCOM();
var
   P: Variant; 
begin
//   P := CreateOleObject('ASCOM.Utilities.Profile');
//   P.DeviceType := 'Switch';
//   P.Register('Waveshare Switch Driver.Switch', 'AstroNico Waveshare Ethernet Relay');
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
   P: Variant;
begin
   if CurUninstallStep = usUninstall then
//   begin
//     P := CreateOleObject('ASCOM.Utilities.Profile');
//     P.DeviceType := 'Switch';
//     P.Unregister('Waveshare Switch Driver.Switch');
//  end;
end;
