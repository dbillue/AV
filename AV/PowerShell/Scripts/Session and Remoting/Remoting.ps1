
cls
Get-Command | where { $_.parameters.keys -contains -ComputerName "Cradle-Laptop" -and $_.parameters.keys -contains "Session"}

Get-Command New-PSSession -syntax

Get-Childitem $Env:Temp -Recurse
$Env:Path

$env:ProgramData

Get-CimInstance -ClassName Win32_LogonSession -ComputerName "Cradle-Laptop" 

winrm quickconfig