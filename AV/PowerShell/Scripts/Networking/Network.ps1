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


Clear-Host

Get-WmiObject -Class Win32_Product -ComputerName . > C:\temp\CimInstance_Win32_BIOS.txt

Get-WmiObject -Class Win32_NetworkAdapterConfiguration -Filter IPEnabled=$true -ComputerName . | Format-Table -Property IPAddres

Get-WmiObject -Class Win32_NetworkAdapterConfiguration -Filter IPEnabled=$true -ComputerName .

Clear-Host
Get-WmiObject Win32_Product | Sort-Object Name | `
Format-Table Name, Version, Vendor

Get-CimInstance -ClassName Win32_BIOS -ComputerName Cradle-Laptop > C:\temp\CimInstance_Win32_BIOS.txt

Get-CimInstance -ClassName Win32_Desktop -ComputerName .