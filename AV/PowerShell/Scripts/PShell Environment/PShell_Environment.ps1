#--------------==--------------#
#     Powershell Env           #
#--------------==--------------#
# Duane Billue


#Execution policies
Clear-Host
Get-ExecutionPolicy -List

#Execution policy for current user, local machine, etc.
Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope MachinePolicy
Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope UserPolicy
Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope Process
Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope CurrentUser
Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope LocalMachine

Get-ExecutionPolicy -List

#Get execution policy of remote machine
Invoke-Command -ComputerName x -ScriptBlock  { $env:PSExecutionPolicyPreference } | { $exePolicy }

#Current PS Version
Clear-Host
$PSVersionTable.PSVersion

#Query .Net Frameworks
Clear-Host   
Get-ChildItem 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP' -Recurse `
    | Get-ItemProperty -Name Version, Release -ErrorAction 0  `
    | where { $_.PSChildName -match '^(?!S)\p{L}'} `
    | select PSChildName, Version, Release | Select -ExpandProperty Version
