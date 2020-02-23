# Author: Duane Billue
# Date: 2019-03-10
# Description: Contains commands for network properties

Clear-Host; arp -a

Clear-Host
'---------------------'
'arp'
'---------------------'
arp -a
'---------------------'
'IPv6'
'---------------------'
Get-NetRoute -AddressFamily IPv6
'---------------------'
'IPv4'
'---------------------'
Get-NetRoute -AddressFamily IPv4

'---------------------'
'Get Network adapters'
'---------------------'
Get-NetAdapter

'---------------------'
'MSFT_Net_Adapter'
'---------------------'
Get-WmiObject -ComputerName "." -Namespace Root\StandardCimv2 -class MSFT_NetAdapter

'---------------------'
'MSFT_Net_Adapter by device Id'
'---------------------'
$deviceID = "{B68B5DFD-90BA-4276-BD36-9C4674B47EA4}"
Get-WmiObject -ComputerName "." -Namespace Root\StandardCimv2 -class MSFT_NetAdapter | Where-Object {$_.DeviceID -eq $deviceID} #> C:\Source\AV\PowerShell\Scripts\Networking\outout\Net_Adapter_WireLess.out

'---------------------'
'win32_networkadapter'
'---------------------'
Clear-Host
get-wmiobject win32_networkadapter | Get-Member
Get-WmiObject Win32_NetworkAdapter

'---------------------'
'win32_networkadapter by NetConnectionId'
'---------------------'
Clear-Host
get-wmiobject win32_networkadapter | `
    select DeviceID, netconnectionid, name, InterfaceIndex, Status, netconnectionstatus, description, netenabled, GUID | `
    Where-Object {$_.netconnectionid -eq 'Wi-Fi'}

'---------------------'
'Win32_NetworkConnection !!! Fail'
'---------------------'
Clear-Host
get-wmiobject -Class Win32_NetworkConnection

'---------------------'
'Win32_NetworkAdapterConfiguration'
'---------------------'
Clear-Host
#Get-WmiObject -Class Win32_NetworkAdapterConfiguration | Get-Member > C:\Source\AV\PowerShell\Scripts\Networking\outout\Win32_NetworkAdapterConfiguration_Members.out
Get-WmiObject -Class Win32_NetworkAdapterConfiguration | `
    select ServiceName, IPAddress, DefaultIPGateway, dhcpenabled, DNSDomain, DNSHostName, MACAddress, Description | `
    Where-Object {$_.ServiceName -eq 'RTWlanE'}

'###############################################'
'###############################################'
Clear-Host

Get-WmiObject -Class Win32_Product -ComputerName . > C:\temp\CimInstance_Win32_BIOS.txt

Get-WmiObject -Class Win32_NetworkAdapterConfiguration -Filter IPEnabled=$true -ComputerName . | Format-Table -Property IPAddres

Get-WmiObject -Class Win32_NetworkAdapterConfiguration -Filter IPEnabled=$true -ComputerName .

Clear-Host
Get-WmiObject Win32_Product | Sort-Object Name | `
Format-Table Name, Version, Vendor

Get-CimInstance -ClassName Win32_BIOS -ComputerName DESKTOP-2BFUPF5 > C:\temp\CimInstance_Win32_BIOS.txt

Get-CimInstance -ClassName Win32_Desktop -ComputerName .

