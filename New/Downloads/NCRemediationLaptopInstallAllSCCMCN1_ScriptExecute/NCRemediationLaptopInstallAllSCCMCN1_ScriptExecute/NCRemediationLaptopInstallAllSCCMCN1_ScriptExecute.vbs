On Error Resume Next
Dim Pversion, ProgName, returnval, PName, AMPreturnval, KeyPath, KeyVal, StrValue, strServiceName, fso, temp, filepath, strComputer, Source, strUserName, DownloadPath, ServStatreturnVal, FolderSize, Downloadexists, DownPath, i, Reg, ValueNames, Types, Value, Downloadfix, Usrnamestat, PassStat, Passreturnvalue, Azurepscommand, Azurecmd, executor, AzureStatus, Productversion, sDisplayName
Const HKEY_LOCAL_MACHINE  = &H80000002
SET objShell = createObject("WScript.shell")
Set objFSO = CreateObject("Scripting.FileSystemObject")
strComputer = objShell.ExpandEnvironmentStrings( "%COMPUTERNAME%" )
strCompDNS = strComputer & ".cts.com"
DownloadPath = "C:\NCRemediation"
If objFSO.FolderExists(DownloadPath) Then
	'Folder Already exists
Else
	objFSO.CreateFolder ("C:\NCRemediation")
End if

KSSize = "0,000524"
FyeSize = "27,267674"
AMPSize = "65,341772"
DLPSize = "252,72364"
CoreVPNSize = "13,572266"
VPNPostureSize = "9,930664"
ISEPostureSize = "3,046387"
UmbrellaSize = "4,006836"
DARTSize = "8,061035"
NXTSize = "35,687789"
PMCSize = "47,637146"
MBAMSize = "26,939142"
TaniumSize = "6,504953"

Set ScriptVersionFile = objFSO.CreateTextFile("C:\NCRemediation\UpdatedScriptV1.1.txt", True)
ScriptVersionFile.close

'Apply Download fix

objFSO.CopyFile ".\*.bat", "C:\NCRemediation\", true
objFSO.CopyFile ".\*.reg", "C:\NCRemediation\", true
objFSO.CopyFile ".\*.ps1", "C:\NCRemediation\", true

If objFSO.FileExists("C:\NCRemediation\DownloadLimitFix.bat") and objFSO.FileExists("C:\NCRemediation\DownloadLimitFix.reg") then
'do nothing
Else
Msgbox "Additional script files not found. Script will not continue"
Msgbox "Please make sure to download all 4 required files which are available when you open the Sharepoint link and ensure the downloaded folder is extracted before the script is executed"
	killProcess "cmd.exe"
	killProcess "conhost.exe"
	killProcess "cmd.exe"
	killProcess "Powershell.exe"
	killProcess "wscript.exe"
	killProcess "cscript.exe"
	Wscript.quit
End if

'Check Computer Certificate Status
Wscript.echo "-------------------------------------------Important Info-----------------------------------------------"
Wscript.echo "Checking Computer PKI Certificate"
'Certpscmd = chr(34) & "Get-ChildItem -path Cert:\LocalMachine\my -Dnsname " & strCompDNS & " | where {$_.Subject -like '*" & strComputer & "*'}" & chr(34)
Certpscmd = chr(34) & "Get-ChildItem -path Cert:\LocalMachine\my -Dnsname " & strCompDNS & " | where-object { $_.Issuer -match 'Cognizant Issuing CA 3' }" & chr(34)
Set WshExecCert = objShell.Exec("powershell.exe -executionpolicy -bypass -command " & Certpscmd)
WshExecCert.StdIn.Close
CertStatOut = WshExecCert.StdOut.ReadAll
If Len(CertStatOut) = 0 Then
	Wscript.echo "Computer PKI Certificate is missing. Please connect to Cognizant VPN and run gpupdate /force to get a valid PKI vertificate"
	Wscript.echo "Please verify now if the PKI certificate is installed and then continue with the remediation"
	Wscript.echo "If a valid PKI certificate is already found, please ignore this message and continue with script execution"
	Wscript.Sleep 20000
Else
	Wscript.echo "Computer PKI Certificate is Installed"
	CertStatCol = "Green"
	Certvalpscmd = chr(34) & "Get-ChildItem -path Cert:\LocalMachine\my -Dnsname " & strCompDNS & " | where-object { $_.Issuer -match 'Cognizant Issuing CA 3' } | foreach { ($_.NotAfter - (Get-Date)).TotalDays; }" & chr(34)
	Set WshExecCert = objShell.Exec("powershell.exe -executionpolicy -bypass -command " & Certvalpscmd)
	WshExecCert.StdIn.Close
	CertValStatOut = WshExecCert.StdOut.ReadAll
	If Len(CertValStatOut) = 0 Then
			Wscript.echo "Unable to verify Computer PKI Certificate validity. Please connect to Cognizant VPN and run gpupdate /force to get a valid PKI vertificate"
			Wscript.echo "Please verify now if the PKI certificate's validity is not expired and then continue with the remediation"
			Wscript.echo "If the PKI certificate has validity, please ignore this message and continue with script execution"
			Wscript.Sleep 20000
	Else
		CertValStatOut = Left(CertValStatOut, 3)
		If CertValStatOut < 1 then
			Wscript.echo "Unable to verify Computer PKI Certificate validity. Please connect to Cognizant VPN and run gpupdate /force to get a valid PKI vertificate"
			Wscript.echo "Please verify now if the PKI certificate's validity is not expired and then continue with the remediation."
			Wscript.echo "If the PKI certificate has validity, please ignore this message and continue with script execution"
			Wscript.Sleep 20000
		Else
			Wscript.echo "PKI Certificate has validity of " & CertValStatOut & " days"
			Wscript.echo "No action required on PKI certificate. Please proceed with the script"
		End if
	End if
End if
Wscript.echo "--------------------------------------------------------------------------------------------------------"
'End Check Computer Certificate Status

Usrnamestat = ""
PassStat = ""
Do Until Usrnamestat = "True"
	strUserName = inputBox("Enter Local Admin account username. (Ex.: it_admin or vnameit")
	If strUserName = "" then
		wscript.quit
	End if
set Localacc = GetObject("winmgmts:\\.\root\cimv2").ExecQuery("Select * from Win32_useraccount Where Name='" & strUserName & "'")
    if Localacc.count <> 0 then
        Wscript.echo strUserName & " Exists"
		Usrnamestat = "True"
	Else
	        Wscript.echo strUserName & " doesn't Exist. Enter valid username"
			Usrnamestat = "False"
    end if
Loop

KeyPath = "SYSTEM\CurrentControlSet\Services\WebClient\Parameters"
KeyVal = "FileSizeLimitInBytes"
CheckifInstalledDword Keypath, keyval
If strvalue = "-1" or StrValue = "4294967295" then
'Do nothing
Else
Do Until PassStat = "True"
	Passreturnvalue = objshell.Run("cmd /c runas /user:" & strUserName & " C:\NCRemediation\DownloadLimitFix.bat", 1, true)
	If Passreturnvalue = "0" Then
		PassStat = "True"
		'wscript.echo "Password correct"
	Elseif Passreturnvalue = "1" Then
		PassStat = "False"
		wscript.echo "Password incorrect. Please try again"
	End if
Loop
End if

KeyPath = "SYSTEM\CurrentControlSet\Services\WebClient\Parameters"
KeyVal = "FileSizeLimitInBytes"
Downloadfix = "False"
Do until Downloadfix = "True"
	CheckifInstalledDword Keypath, keyval
	If strvalue = "-1" or StrValue = "4294967295" then
		Downloadfix = "True"
	else
		Downloadfix = "False"
	End if
Loop

'objshell.Run("cmd /c runas /user:" & strUserName & " C:\NCRemediation\DownloadLimitFix.bat"), 1, true

Wscript.sleep 5000

'End Apply Download fix

killProcess "iexplore.exe"
Wscript.echo "IE window will open now. Once user authenticates and Sharepoint page is open, Close IE Window. Script will then Auto-Continue"
Wscript.sleep 2000

IEPath = "C:\Program Files\internet explorer\iexplore.exe"
strURL = "https://cognizantonline.sharepoint.com/:f:/s/ComplianceRemediationReportCognizant/Es5Fk4DYtDlKptDR7vTF23sBVoLJSqdcRaQ47F2-Y3sjzg?e=gpP1wQ"
objShell.Run """" & IEPath & """ " & strURL, 1, true

Azurepscommand = "C:\NCRemediation\TestAzure.ps1"
Azurecmd = "powershell.exe -executionpolicy bypass -command " & Azurepscommand
Set executor = Objshell.Exec(Azurecmd)
executor.StdIn.Close
AzureStatus = executor.StdOut.ReadAll
'wscript.echo AzureStatus

If AzureStatus < "1" Then
Wscript.echo "Unable to access the Share to download files. Check if user is logged in or Azure registration is working"
Wscript.sleep 10000
Wscript.quit
Else
Wscript.echo "Azure check is success, Script will continue"
End if

Timestamp = Right(Year(Now),2) _
 & Right("00" & Month(Now),2) _
 & Right("00" & Day(Now),2) _
 & Right("00" & Hour(Now),2) _
 & Right("00" & Minute(Now),2) _
 & Right("00" & Second(Now),2)
 
'Check if KillSwitch is Compliant
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\KillSwitch"
Downpath = "C:\NCRemediation\KillSwitch"
Downloadexists = objFSO.FolderExists(Downpath)
Filepath = objShell.ExpandEnvironmentStrings("C:\sf3\jytj43t\a33ewe\WY&WdhJSDIHGDjd.HSgshh")
exists = objFSO.FileExists(filepath)
If (exists) then
	Wscript.echo "Kill Switch is already installed and found on the system"
	objFSO.MoveFolder "C:\NCRemediation\KillSwitch" , "C:\NCRemediation\KillSwitch.old" & Timestamp
	'wscript.echo "KillSwitch Status: " & KillSwStatus
else
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = KSSize Then
				Wscript.echo "KillSwitch Installation file already exists, download not required. Will be installed"
			Else
					Wscript.echo "Kill Switch is not yet installed, Downloading now..."
					DownloadContent Source
					'wscript.echo "KillSwitch Status: " & KillSwStatus
			End if
		Else
			Wscript.echo "Kill Switch is not yet installed, Downloading now..."
			DownloadContent Source
		End if
			
End if
'End Install KillSwitch
'Check if FireEye is compliant
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\FireEye"
strServiceName = "%FEWSCService%"
strServiceName1 = "xagt"
Downpath = "C:\NCRemediation\FireEye"
Downloadexists = objFSO.FolderExists(Downpath)
CheckifInstalled64 "FireEye Endpoint Agent"
PVersion = "31.28.8"
Pversion = Replace(Pversion, ".", ",")
Productversion = Replace(Productversion, ".", ",")
If Productversion = "NA" then
	CheckServiceState strServiceName
	FyeServ1 = ServStatreturnVal
	CheckServiceState strServiceName1
	FyeServ2 = ServStatreturnVal
	If FyeServ1 = "Compliant" and FyeServ2 = "Compliant" Then
		wscript.echo "FireEye " & Productversion & " is installed and running"
		objFSO.MoveFolder "C:\NCRemediation\FireEye" , "C:\NCRemediation\FireEye.old" & Timestamp
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = FyeSize Then
					Wscript.echo "FireEye Installation file already exists, download not required. Will be installed"
					Else
						wscript.echo "FireEye is installed but not running. Downloading now"
						DownloadContent Source
				End if
			Else
				wscript.echo "FireEye is not installed. Downloading now"
				DownloadContent Source
			End if
	End if
ElseIf Productversion > PVersion Then
	CheckServiceState strServiceName
	FyeServ1 = ServStatreturnVal
	CheckServiceState strServiceName1
	FyeServ2 = ServStatreturnVal
	If FyeServ1 = "Compliant" and FyeServ2 = "Compliant" Then
		wscript.echo "FireEye " & Productversion & " is installed and running"
		objFSO.MoveFolder "C:\NCRemediation\FireEye" , "C:\NCRemediation\FireEye.old" & Timestamp
	Elseif FyeServ1 = "NA" and FyeServ2 = "Compliant" Then
		wscript.echo "FireEye " & Productversion & " is installed and running"
		objFSO.MoveFolder "C:\NCRemediation\FireEye" , "C:\NCRemediation\FireEye.old" & Timestamp
	End if
Elseif Productversion = PVersion then
	CheckServiceState strServiceName
	FyeServ1 = ServStatreturnVal
	CheckServiceState strServiceName1
	FyeServ2 = ServStatreturnVal
	If FyeServ1 = "Compliant" and FyeServ2 = "Compliant" Then
		wscript.echo "FireEye " & Productversion & " is installed and running"
		objFSO.MoveFolder "C:\NCRemediation\FireEye" , "C:\NCRemediation\FireEye.old" & Timestamp
	Else
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = FyeSize Then
				Wscript.echo "FireEye is installed but not running. FireEye Installation file already exists, download not required. Will be installed"
				Else
					wscript.echo "FireEye is installed but not running. Downloading now"
					DownloadContent Source
			End if
			Else
				wscript.echo "FireEye is installed but not running. Downloading now"
				DownloadContent Source
		End if
	End if
End if
'End Check if FireEye is compliant

'Begin check if AMP is installed
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\AMP"
pName = "Cisco AMPEndpoint Connector"
pversion = "7.3.9.20091"
Downpath = "C:\NCRemediation\AMP"
Downloadexists = objFSO.FolderExists(Downpath)
Pversion = Replace(Pversion, ".", ",")
CheckAMPVersion
AMPreturnval = Replace(AMPreturnval, ".", ",")
strServiceName = "%CiscoAMP%"
If AMPreturnval = "NA" Then
	CheckServiceState strServiceName
		If ServStatreturnVal = "Compliant" Then
			Wscript.echo "Cisco AMP is installed and running"
			objFSO.MoveFolder "C:\NCRemediation\AMP" , "C:\NCRemediation\AMP.old" & Timestamp
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = AMPSize Then
					Wscript.echo "AMP Installation file already exists, download not required. Will be installed"
					Else
						Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
						DownloadContent Source
						'objShell.Run("""c:\Program Files\Cisco\AMP\7.2.5\iptray.exe""")
				End if
			Else
				Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
				DownloadContent Source
			End if
		End if
	ElseIf AMPreturnval < pversion Then
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = AMPSize Then
				Wscript.echo "AMP Installation file already exists, download not required. Will be installed"
				Else
					Wscript.echo "Downloading AMP now"
					DownloadContent Source
					'objShell.Run("""c:\Program Files\Cisco\AMP\7.2.5\iptray.exe""")
			End if
		Else
			WScript.Echo  pName & " Version " & AMPreturnval & " is installed, Latest version will be downloaded"
			DownloadContent Source
		End if
	Elseif AMPreturnval >= pversion Then
		CheckServiceState strServiceName
		If ServStatreturnVal = "Compliant" Then
			Wscript.echo "Cisco AMP is installed and running"
			objFSO.MoveFolder "C:\NCRemediation\AMP" , "C:\NCRemediation\AMP.old" & Timestamp
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = AMPSize Then
					Wscript.echo "AMP Installation file already exists, download not required. Will be installed"
					Else
						Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
						DownloadContent Source
						'objShell.Run("""c:\Program Files\Cisco\AMP\7.2.5\iptray.exe""")
				End if
			Else
				Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
				DownloadContent Source
			End if
		End if
End If
'End check if AMP is installed

'Begin Check and install DLP ForcePoint Endpoint
PName = "FORCEPOINT ONE ENDPOINT"
Pversion = "19.12.4417"
Pversion = Replace(Pversion, ".", ",")
strServiceName = "%WSDLP%"
Downpath = "C:\NCRemediation\DLPsetup"
Downloadexists = objFSO.FolderExists(Downpath)
CheckifInstalled PName
strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\DLPsetup"
If strValue = "NA" Then
	CheckServiceState strServiceName
	If ServStatreturnVal = "Compliant" Then
		Wscript.Echo pName & ", Version: " & pversion & " is installed already and running"
		objFSO.MoveFolder "C:\NCRemediation\DLPSetup" , "C:\NCRemediation\DLPSetup.old" & Timestamp
		'Wscript.Echo "....................................."
	Else
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = DLPSize Then
				Wscript.echo "DLP Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
			DownloadContent Source
		End if
	End if
	ElseIf StrValue < Pversion  Then
		'If (Downloadexists) then
		'	CheckDownloadsize Downpath
		'	If FolderSize = DLPSize Then
		'		Wscript.echo "DLP Installation file already exists, download not required. Will be installed"
		'	Else
		'		Wscript.echo "Downloading DLP now"
		'		DownloadContent Source
		'	End if
		'Else
			Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer"
			objFSO.MoveFolder "C:\NCRemediation\DLPSetup" , "C:\NCRemediation\DLPSetup.old" & Timestamp
			'DownloadContent Source
		'End if
	ElseIf StrValue >= Pversion  Then
		CheckServiceState strServiceName
		If ServStatreturnVal = "Compliant" Then
			Wscript.Echo pName & " Version: " & Pversion & " is installed and running"
			objFSO.MoveFolder "C:\NCRemediation\DLPSetup" , "C:\NCRemediation\DLPSetup.old" & Timestamp
			'Wscript.Echo "....................................."
		Else
			'If (Downloadexists) then
				'CheckDownloadsize Downpath
				'If FolderSize = DLPSize Then
					'Wscript.echo "DLP Installation file already exists, download not required. Will be installed"
				'Else
					'Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
					'DownloadContent Source
				'End if
			'Else
				Wscript.Echo pName & " version: " & StrValue & " is installed and not running."
				objFSO.MoveFolder "C:\NCRemediation\DLPSetup" , "C:\NCRemediation\DLPSetup.old" & Timestamp
				'DownloadContent Source
			'End if
		End if
End If
'End Check and install DLP ForcePoint Endpoint

'Begin Check and install Cisco Core VPN 4.8.03036
PName = "Cisco AnyConnect Secure Mobility Client"
Pversion = "4.8.03036"
strServiceName = "vpnagent"
Downpath = "C:\NCRemediation\CiscoCoreVPN"
Downloadexists = objFSO.FolderExists(Downpath)
Pversion = Replace(Pversion, ".", ",")
CheckifInstalled PName
strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\CiscoCoreVPN"
If strValue = "NA" Then
	CheckServiceState strServiceName
	If ServStatreturnVal = "Compliant" Then
		Wscript.Echo pName & ", Version: " & pversion & " is installed already"
		objFSO.MoveFolder "C:\NCRemediation\CiscoCoreVPN" , "C:\NCRemediation\CiscoCoreVPN.old" & Timestamp
		'Wscript.Echo "....................................."
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = CoreVPNSize Then
					Wscript.echo "Cisco VPN Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
					DownloadContent Source
				End if
			Else
				Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
				DownloadContent Source
			End if
	End if
	ElseIf StrValue < Pversion  Then
		If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = CoreVPNSize Then
					Wscript.echo "Cisco VPN Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
					DownloadContent Source
				End if
		Else
			Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
			DownloadContent Source
		End if
	ElseIf StrValue >= Pversion  Then
		CheckServiceState strServiceName
		If ServStatreturnVal = "Compliant" Then
			Wscript.Echo pName & ", Version: " & pversion & " is installed already"
			objFSO.MoveFolder "C:\NCRemediation\CiscoCoreVPN" , "C:\NCRemediation\CiscoCoreVPN.old" & Timestamp
			'Wscript.Echo "....................................."
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = CoreVPNSize Then
					Wscript.echo "Cisco VPN Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
					DownloadContent Source
				End if
			Else
				Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
				DownloadContent Source
			End if
		End if
End If
'End Check and install Cisco Core VPN 4.8.03036

'Begin Check and install Cisco VPN Posture 4.8.03036
PName = "Cisco AnyConnect Posture Module"
Pversion = "4.8.03036"
Downpath = "C:\NCRemediation\CiscoPosture"
Downloadexists = objFSO.FolderExists(Downpath)
Pversion = Replace(Pversion, ".", ",")
CheckifInstalled PName
strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\CiscoPosture"
If strValue = "NA" Then
	If (Downloadexists) then
		CheckDownloadsize Downpath
		If FolderSize = VPNPostureSize Then
			Wscript.echo "Cisco VPN Posture Installation file already exists, download not required. Will be installed"
		Else
			Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
			DownloadContent Source
		End if
	Else
		Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
		DownloadContent Source
	End if
	ElseIf StrValue < Pversion  Then
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = VPNPostureSize Then
				Wscript.echo "Cisco VPN Posture Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
			DownloadContent Source
		End if
	ElseIf StrValue >= Pversion  Then
			Wscript.Echo pName & ", Version: " & pversion & " is installed already"
			objFSO.MoveFolder "C:\NCRemediation\CiscoPosture" , "C:\NCRemediation\CiscoPosture.old" & Timestamp
			'Wscript.Echo "....................................."
End If
'End Check and install Cisco VPN Posture 4.8.03036

'Begin Check and install Cisco ISE Posture 4.8.03036
PName = "Cisco AnyConnect ISE Posture Module"
Pversion = "4.8.03036"
Downpath = "C:\NCRemediation\CiscoISE"
Downloadexists = objFSO.FolderExists(Downpath)
strServiceName = "%aciseagent%"
Pversion = Replace(Pversion, ".", ",")
CheckifInstalled PName
strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\CiscoISE"
If strValue = "NA" Then
	CheckServiceState strServiceName
	If ServStatreturnVal = "Compliant" Then
		Wscript.Echo pName & ", Version: " & pversion & " is installed already"
		objFSO.MoveFolder "C:\NCRemediation\CiscoISE" , "C:\NCRemediation\CiscoISE.old" & Timestamp
		'Wscript.Echo "....................................."
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = ISEPostureSize Then
					Wscript.echo "Cisco VPN ISE Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer,  Will be downloaded now"
					DownloadContent Source
				End if
			Else
				Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer,  Will be downloaded now"
				DownloadContent Source
			End if
	End if
	ElseIf StrValue < Pversion  Then
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = ISEPostureSize Then
				Wscript.echo "Cisco VPN ISE Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer,  Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer,  Will be downloaded now"
			DownloadContent Source
		End if
	ElseIf StrValue >= Pversion  Then
		CheckServiceState strServiceName
		If ServStatreturnVal = "Compliant" Then
			Wscript.Echo pName & ", Version: " & pversion & " is installed already"
			objFSO.MoveFolder "C:\NCRemediation\CiscoISE" , "C:\NCRemediation\CiscoISE.old" & Timestamp
			'Wscript.Echo "....................................."
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = ISEPostureSize Then
					Wscript.echo "Cisco VPN ISE Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
					DownloadContent Source
				End if
			Else
				Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
				DownloadContent Source
			End if
		End if
End If
'End Check and install Cisco ISE Posture 4.8.03036

'Begin Check and install Cisco Umbrella Roaming 4.8.03036
PName = "Cisco AnyConnect Umbrella Roaming Security Module"
Pversion = "4.8.03036"
strServiceName = "%acumbrellaagent%"
Downpath = "C:\NCRemediation\CiscoUmbrella"
Downloadexists = objFSO.FolderExists(Downpath)
Pversion = Replace(Pversion, ".", ",")
CheckifInstalled PName
strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\CiscoUmbrella"
If strValue = "NA" Then
	CheckServiceState strServiceName
	If ServStatreturnVal = "Compliant" Then
		Wscript.Echo pName & ", Version: " & pversion & " is installed already"
		objFSO.MoveFolder "C:\NCRemediation\CiscoUmbrella" , "C:\NCRemediation\CiscoUmbrella.old" & Timestamp
		'Wscript.Echo "....................................."
	Else
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = UmbrellaSize Then
				Wscript.echo "Cisco Umbrella Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
			DownloadContent Source
		End if
	End if
	ElseIf StrValue < Pversion  Then
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = UmbrellaSize Then
				Wscript.echo "Cisco Umbrella Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
			DownloadContent Source
		End if
	ElseIf StrValue >= Pversion  Then
		CheckServiceState strServiceName
		If ServStatreturnVal = "Compliant" Then
			Wscript.Echo pName & ", Version: " & pversion & " is installed already"
			objFSO.MoveFolder "C:\NCRemediation\CiscoUmbrella" , "C:\NCRemediation\CiscoUmbrella.old" & Timestamp
			'Wscript.Echo "....................................."
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = UmbrellaSize Then
					Wscript.echo "Cisco Umbrella Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
					DownloadContent Source
				End if
			Else
				Wscript.Echo pName & " version: " & StrValue & " is installed and not running, Will be downloaded now"
				DownloadContent Source
			End if
		End if
End If
'End Check and install Cisco Umbrella Roaming 4.8.03036

'Begin Check and install Cisco DART 4.8.03036
PName = "Cisco AnyConnect Diagnostics and Reporting Tool"
Pversion = "4.8.03027"
Downpath = "C:\NCRemediation\CiscoDart"
Downloadexists = objFSO.FolderExists(Downpath)
Pversion = Replace(Pversion, ".", ",")
CheckifInstalled PName
strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\CiscoDart"
If strValue = "NA" Then
	If (Downloadexists) then
		CheckDownloadsize Downpath
		If FolderSize = DARTSize Then
			Wscript.echo "Cisco DART Installation file already exists, download not required. Will be installed"
		Else
			Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
			DownloadContent Source
		End if
	Else
		Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
		DownloadContent Source
	'objShell.Run("cmd /c runas /user:" & strUserName & " " & DownloadPath & "\CiscoDart\InstallDart.bat"), 1, true
	'wscript.Sleep 5000
	End if
	ElseIf StrValue < Pversion  Then
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = DARTSize Then
				Wscript.echo "Cisco DART Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
			DownloadContent Source
			'objShell.Run("cmd /c runas /user:" & strUserName & " " & DownloadPath & "\CiscoDart\InstallDart.bat"), 1, true
			'Wscript.sleep 5000
		End if
	ElseIf StrValue >= Pversion  Then
		Wscript.Echo pName & ", Version: " & pversion & " is installed already"
		objFSO.MoveFolder "C:\NCRemediation\CiscoDart" , "C:\NCRemediation\CiscoDart.old" & Timestamp
End If
'End Check and install Cisco DART 4.8.03036

'Begin Check and install NextThink
PName = "Nexthink Collector"
Pversion = "6.23.02142"
strServiceName = "%Nexthink Service%"
strServiceName1 = "%Nexthink Coordinator%"
Downpath = "C:\NCRemediation\NextThink"
Downloadexists = objFSO.FolderExists(Downpath)
Pversion = Replace(Pversion, ".", ",")
CheckifInstalled PName
strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\NextThink"
If strValue = "NA" Then
	CheckServiceState strServiceName
	If ServStatreturnVal = "Compliant" Then
		CheckServiceState strServiceName1
		If ServStatreturnVal = "Compliant" Then
			Wscript.Echo pName & ", Version: " & pversion & " is installed already"
			objFSO.MoveFolder "C:\NCRemediation\NextThink" , "C:\NCRemediation\NextThink.old" & Timestamp
		Else
			Set objFile = objFSO.CreateTextFile("C:\NCRemediation\NexthinkRepair.txt",True)
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = NXTSize Then
					Wscript.echo "Nexthink Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " version: " & StrValue & " is installed and not running, Will be downloaded now"
					DownloadContent Source
				End if
			Else
					Wscript.Echo pName & " version: " & StrValue & " is installed and not running, Will be downloaded now"
					DownloadContent Source
			End if
		End if
	Else
		Set objFile = objFSO.CreateTextFile("C:\NCRemediation\NexthinkRepair.txt",True)
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = NXTSize Then
				Wscript.echo "Nexthink Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
			DownloadContent Source
		End if
	End if
	ElseIf StrValue < Pversion  Then
		Set objFile = objFSO.CreateTextFile("C:\NCRemediation\NexthinkRepair.txt",True)
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = NXTSize Then
				Wscript.echo "Nexthink Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
			DownloadContent Source
		End if
	ElseIf StrValue >= Pversion  Then
		CheckServiceState strServiceName
		If ServStatreturnVal = "Compliant" Then
			CheckServiceState strServiceName1
			If ServStatreturnVal = "Compliant" Then
				Wscript.Echo pName & ", Version: " & pversion & " is installed already"
				objFSO.MoveFolder "C:\NCRemediation\NextThink" , "C:\NCRemediation\NextThink.old" & Timestamp
			Else
				Set objFile = objFSO.CreateTextFile("C:\NCRemediation\NexthinkRepair.txt",True)
				If (Downloadexists) then
					CheckDownloadsize Downpath
					If FolderSize = NXTSize Then
						Wscript.echo "Nexthink Installation file already exists, download not required. Will be installed"
					Else
						Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
						DownloadContent Source
					End if
				Else
					Wscript.Echo pName & " version: " & StrValue & " is installed and not running, Will be downloaded now"
					DownloadContent Source
				End if
			End if
		Else
			Set objFile = objFSO.CreateTextFile("C:\NCRemediation\NexthinkRepair.txt",True)
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = NXTSize Then
					Wscript.echo "Nexthink Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " version: " & StrValue & " is installed and not running,  Will be downloaded now"
					DownloadContent Source
				End if
			Else
				Wscript.Echo pName & " version: " & StrValue & " is installed and not running, Will be downloaded now"
				DownloadContent Source
			End if
		End if
End If
'End Check and install NextThink

'Begin Check and install Avecto and PMC
PName = "Avecto and PMC"
'PVersion = "5.5.149.0"
Downpath = "C:\NCRemediation\AvectoPMC"
Downloadexists = objFSO.FolderExists(Downpath)
'strServiceName = "%Avecto Defendpoint Service%"
'strServiceName1 = "%IC3Adapter%"
'KeyPath = "SOFTWARE\WOW6432Node\Avecto\Privilege Guard Client"
'KeyVal = "ProductVersion"
'Pversion = Replace(Pversion, ".", ",")
'CheckifInstalledAlready KeyPath, KeyVal
'strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\AvectoPMC"
If (Downloadexists) then
	CheckDownloadsize Downpath
	If FolderSize = PMCSize Then
		Wscript.echo "AvectoPMC Installation file already exists, download not required. Will be installed/repaired"
	Else
		Wscript.Echo pName & " needs to be installed/repaired,  Will be downloaded now"
		DownloadContent Source
	End if
Else
	Wscript.Echo pName & " needs to be installed/repaired,  Will be downloaded now"
	DownloadContent Source
End if
'End Check and install Avecto and PMC

'Begin MBAM Installation check

bidom = "NA"
Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\CIMV2")
Set colItems = objWMIService.ExecQuery("SELECT * FROM Win32_ComputerSystem")
For Each objItem In colItems
	bidom = objItem.Domain
Next
If bidom = "cts.com" then
PName = "MDOP MBAM"
Pversion = "2.5.1143.0"
strServiceName = "%MBAMAgent%"
Downpath = "C:\NCRemediation\MBAM"
Downloadexists = objFSO.FolderExists(Downpath)
Pversion = Replace(Pversion, ".", ",")
CheckifInstalled PName
strValue = Replace(strValue, ".", ",")
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\MBAM"
If strValue = "NA" Then
	CheckServiceState strServiceName
	If ServStatreturnVal = "Compliant" Then
		Wscript.Echo pName & ", Version: " & pversion & " is installed already"
		objFSO.MoveFolder "C:\NCRemediation\MBAM" , "C:\NCRemediation\MBAM.old" & Timestamp
		Wscript.Echo "....................................."
	Else
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = MBAMSize Then
				Wscript.echo "MBAM Installation file already exists, download not required. Will be installed"
			Else
				Wscript.echo "Downloading MBAM files now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Version: " & Pversion & " doesn't exist on the computer, Will be downloaded now"
			DownloadContent Source
		End if
	End if
	ElseIf StrValue < Pversion  Then
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = MBAMSize Then
				Wscript.echo "MBAM Installation file already exists, download not required. Will be installed"
			Else
				Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " Lower version: " & StrValue & " is installed on this computer, Will be downloaded now"
			DownloadContent Source
		End if
	ElseIf StrValue >= Pversion  Then
		CheckServiceState strServiceName
		If ServStatreturnVal = "Compliant" Then
			Wscript.Echo pName & ", Version: " & pversion & " is installed already"
			objFSO.MoveFolder "C:\NCRemediation\MBAM" , "C:\NCRemediation\MBAM.old" & Timestamp
			'Wscript.Echo "....................................."
		Else
			If (Downloadexists) then
				CheckDownloadsize Downpath
				If FolderSize = MBAMSize Then
					Wscript.echo "MBAM Installation file already exists, download not required. Will be installed"
				Else
					Wscript.Echo pName & " version: " & StrValue & " is installed and not running, Will be downloaded now"
					DownloadContent Source
				End if
			Else
				Wscript.Echo pName & " version: " & StrValue & " is installed and not running, Will be downloaded now"
				DownloadContent Source
			End if
		End if
End If
Else
Wscript.echo "Skipping MBAM as it's not applicable for Workgroup assets"
End if
'End MBAM Installation Check

'Begin Check and install Tanium
PName = "Tanium"
Downpath = "C:\NCRemediation\Tanium"
Downloadexists = objFSO.FolderExists(Downpath)
strServiceName = "%Tanium%"
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1\Tanium"
CheckServiceState strServiceName
If ServStatreturnVal = "Compliant" Then
	Wscript.Echo pName & " is installed already"
	objFSO.MoveFolder "C:\NCRemediation\Tanium" , "C:\NCRemediation\Tanium.old" & Timestamp
	'Wscript.Echo "....................................."
	Else
		If (Downloadexists) then
			CheckDownloadsize Downpath
			If FolderSize = TaniumSize Then
				Wscript.echo "Tanium Installation file already exists, download not required. Will be installed"
			Else
				Wscript.echo "Downloading Tanium now"
				DownloadContent Source
			End if
		Else
			Wscript.Echo pName & " needs to be installed, Will be downloaded now"
			DownloadContent Source
		End if	
End if
'End Check and install Tanium

'Begin Copy Installation Scripts
Downpath = "C:\NCRemediation\Scripts"
Downloadexists = objFSO.FolderExists(Downpath)
Source = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\teams\ITSoftRepo\Shared Documents\EPTAgents\ROW_ComplianceRemediationScripts\NCRemediationLaptopInstallAllSCCMCN1_Test\Scripts"
Wscript.echo "Downloading Script files"
DownloadContent Source
wscript.sleep 1000
	
'End Copy Installation Scripts

InstallComponents = "C:\NCRemediation\Scripts\InstallComponents.vbs"
LaptopChecker = "C:\NCRemediation\Scripts\Laptop_CLChecker.vbs"
StartInstall = "C:\NCRemediation\Scripts\StartInstall.bat"

If objFSO.FileExists(InstallComponents) and objFSO.FileExists(LaptopChecker) and objFSO.FileExists(StartInstall) then
	'Installation files exists, script will continue
Else
	Msgbox "Required script files are not downloaded properly. Please add the SharePoint site to trusted sites and try script execution again"
	Msgbox "This is a known issue in AutoPilot machines. If this issue occurs next time on script execution, please remediate manually. Script will end now"
		killProcess "Powershell.exe"
		killProcess "wscript.exe"
		killProcess "cscript.exe"
		LogToFile "End Script"
		LogToFile "-----------------------------------------------------"
		Wscript.quit
End if

'Begin Call Main Install File with elevation
'strUserName = inputBox("Enter Local Admin account username. (Ex.: it_admin or vnameit")
'objShell.Run("cmd /c runas /user:" & strUserName & " C:\NCRemediation\Scripts\StartInstall.bat"), 1, true
PassStat = ""
Do Until PassStat = "True"
	Passreturnvalue = objShell.Run("cmd /c runas /user:" & strUserName & " C:\NCRemediation\Scripts\StartInstall.bat", 1, true)
	If Passreturnvalue = "0" Then
		PassStat = "True"
		'wscript.echo "Password correct"
	Elseif Passreturnvalue = "1" Then
		PassStat = "False"
		wscript.echo "Password incorrect. Please try again"
	End if
Loop
'End Call Main Install File with elevation

'Copy Reports
'msgbox("Ready to copy the reports to central sharepath? Note: Please click OK only after the script execution is fully completed"), vbOKOnly
wscript.echo "Waiting for the script to complete to copy the reports to sharepoint"
wscript.sleep 60000
set oShellEnv = objShell.Environment("Process")
computerName  = oShellEnv("ComputerName")
Wscript.echo "Verifying if the report is Generated"
WorkDir = "C:\Windows\SoEValidator\" & computerName & "\"
OutputFile =  WorkDir & computerName & ".csv"
Outputfilestat = "False"
Do until Outputfilestat = "True"
	if objFSO.FileExists(OutputFile) then
		Outputfilestat = "True"
		Wscript.echo "Report is Generated, Copy will start now"
		DownloadPath = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\sites\ComplianceRemediationReportCognizant\Shared Documents\General\Reports"
		Source = "C:\Windows\SOEValidator\" & computerName
		'wscript.echo computerName & ", " & Source
		DownloadContent Source
		objStartFolder = "C:\Windows\SOEValidator\" & computerName & "\"
		'wscript.echo objStartFolder
		Set objFolder = objFSO.GetFolder(objStartFolder)
		SPURL = "\\cognizantonline.sharepoint.com@SSL\DavWWWRoot\sites\ComplianceRemediationReportCognizant\Shared Documents\General\Reports\"
		Set colFiles = objFolder.Files
			For Each objFile in colFiles
				'wscript.echo objFile.Name
				If instr(objFile.Name, computerName & ".csv") = 1 Then
					'wscript.echo "Match"
					objFSO.CopyFile objStartFolder & objFile.Name, SPURL & objFile.Name, TRUE
				End if
			Next
	Else
		'Wscript.echo "Report is not yet generated"
		Outputfilestat = "False"
	End if
Loop
'End Copy Reports

Msgbox("Script completed. Please connect to Cognizant VPN, run gpupdate /force, wait for 30 minutes and then restart the machine")

'Function Starts

Function CheckifInstalled(var1)
strvalue = ""
ProgName = var1
strComputer = "."
	Set objWMIProgram = GetObject("winmgmts:" _ 
		& "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2") 
		Set ColInstalledSoft = objWMIProgram.ExecQuery("Select * from Win32_Product Where Name Like'" & ProgName & "'")
		If ColInstalledSoft.Count = 0 Then
			strValue = "NA"
			Else
			For Each objItem in ColInstalledSoft
				strValue = objItem.Version
			Next
		End If
		
End Function

 Function CheckifInstalledAlready(var1, var2)
	StrValue = ""
	strComputer = "."
	Set oReg=GetObject("winmgmts:{impersonationLevel=impersonate}!\\" & _
    strComputer & "\root\default:StdRegProv")
	strKeypath = var1
	strValueName = var2
	oReg.GetStringValue HKEY_LOCAL_MACHINE,strKeyPath,strValueName,StrValue
End function

Function CheckAMPVersion
AMPreturnval = ""
strComputer = "."
Set oReg=GetObject("winmgmts:{impersonationLevel=impersonate}!\\" & _
strComputer & "\root\default:StdRegProv")
strKeypath = "SOFTWARE\WOW6432Node\Immunet Protect"
strValueName = "IPTray"
oReg.GetStringValue HKEY_LOCAL_MACHINE,strKeyPath,strValueName,StrValue
StrValue = Replace(StrValue, """", "")
Set FSO   = CreateObject("Scripting.FileSystemObject")
filepath = objShell.ExpandEnvironmentStrings(StrValue)
exists = fso.FileExists(StrValue)
	If (exists) then
		temp = fso.GetFileVersion(StrValue)
		If Len(temp) Then
			AMPreturnval = temp
			'wscript.echo AMPreturnval
			'Set AMPreturnval = Nothing
			Else
				Set AMPreturnval = "NA"
				'wscript.echo AMPreturnval
		End If
		Else
			AMPreturnval = "NA"
			'wscript.echo AMPreturnval
	End If
End Function

Function CheckServiceState(Var1)
	ServStatreturnVal = "NA"
	ServName = Var1
	strComputer = "."
	Set objWMIService = GetObject("winmgmts:" _ 
		& "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2") 
		Set colRunningServices = objWMIService.ExecQuery("Select * from Win32_Service where Name Like'" & ServName & "'") 
		If colRunningServices.Count = 0 Then
			ServState = ServName & " Service doesn't exist"
			Else
				For Each objItem in colRunningServices
					If objItem.State = "Stopped" Then 
						'Wscript.Echo objItem.DisplayName & " Service is Installed/Stopped"
						ServState = objItem.DisplayName & " Service is Installed/Stopped"
						'Wscript.Echo "....................................."
						ServStatreturnVal = "Stopped"
						
						ElseIf objItem.State = "Running" Then 
							'Wscript.Echo objItem.DisplayName & " Service is Installed/Running"
							ServState = objItem.DisplayName & " Service is Installed/Running"
							'Wscript.Echo "....................................."
							ServStatreturnVal = "Compliant"
					End If
				Next
		End if
 End Function
 

Function CheckDownloadsize(var1)
FolderSize = ""
Set ObjFSO = WScript.CreateObject("Scripting.FileSystemObject")
Dim oFolder:Set oFolder = ObjFSO.GetFolder(var1)
FolderSize = round(oFolder.Size/1024/1024,6)
FolderSize = Replace(FolderSize, ".", ",")
End Function

Function killProcess(strProcessName)
    set colProcesses = GetObject("winmgmts:\\.\root\cimv2").ExecQuery("Select * from Win32_Process Where Name='" & strProcessName & "'")
    if colProcesses.count <> 0 then
        for each objProcess in colProcesses
            objProcess.Terminate()
        next
    end if
end Function 

Function DownloadContent(Source)
Const FOF_CREATEPROGRESSDLG = &H10&
Set wshShell = CreateObject("shell.application")
Set objFolder = wshShell.NameSpace(DownloadPath)
objFolder.CopyHere Source, FOF_CREATEPROGRESSDLG
End Function

Function CheckMBAMRegIntegrity(KeyPath)
Const HKLM = &H80000002
i=0
set Reg = GetObject("Winmgmts:root\default:StdRegProv")
If Reg.EnumValues( HKLM, KeyPath, ValueNames, Types ) = 0 Then
If isArray( ValueNames ) Then
For i = 0 to UBound( ValueNames )
Reg.getStringValue HKLM, KeyPath, ValueNames(i), Value
Next
End If
End If
'Wscript.echo i
End Function

Function CheckifInstalledDword(var1, var2)
	StrValue = "NA"
	strComputer = "."
	Set oReg=GetObject("winmgmts:{impersonationLevel=impersonate}!\\" & _
    strComputer & "\root\default:StdRegProv")
	strKeypath = var1
	strValueName = var2
	oReg.GetDwordValue HKEY_LOCAL_MACHINE,strKeyPath,strValueName,StrValue
End function

Function CheckifInstalled64(Var1)
sMatch = Var1
Productversion = "NA"
Const HKLM = &H80000002 
strComputer = "." 
strKey = "SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\" 
Set objReg = GetObject("winmgmts://" & strComputer & "/root/default:StdRegProv") 
objReg.EnumKey HKLM, strKey, arrSubkeys 
For Each strSubkey In arrSubkeys
	objReg.GetStringValue HKLM, strKey & strSubkey, "DisplayName", sDisplayName
	objReg.GetStringValue HKLM, strKey & strSubkey, "DisplayVersion", Prodversion
	If sDisplayName <> "" Then 
		if instr(sDisplayName, sMatch) > 0 then
		Productversion = Prodversion
		'wscript.echo sDisplayName
		'wscript.echo Productversion
		End if
	Else
		'wscript.echo "Not found"
		'Productversion = "NA"
	End If
Next 

End function