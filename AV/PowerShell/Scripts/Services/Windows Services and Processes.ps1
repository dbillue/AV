Get-Process
Get-Process | Get-Member
Get-Process -Name 'snmp'

Get-Service
Get-Service | Get-Member
Get-Service -Name 'W3SVC' -RequiredServices
Get-Service -Name 'WinHttpAutoPro*' -DependentServices
Get-Service -Name "win*" -Exclude "WinRM"
Get-Service | Where-Object {$_.Status -eq "Running"}
Get-Service -DisplayName 'Internet Connection Sharing (ICS)'
Get-Service * | Format-Table ServiceType, StartType, Status, -auto
Get-Service * | Sort-Object -property ServiceType | Format-Table Name, ServiceType, Status, CanStop, -auto