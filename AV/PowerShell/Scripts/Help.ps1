Clear-Host

################################
##  Help Commands for cmdlets ##
################################
#Get-Help Get-ChildItem
#Get-ChildItem -?
#Get-Help Get-Help
#Get-Help -Category Cmdlet
#man Get-NetIPAddress
#help Wait-Process


############################################
##  Conceptual Help Commands for Articles ##
############################################
#Get-Help about_*  # all articles
#Get-Help about_Parallel > C:\about_Parrallel.txt
#Get-Help about_command_syntax > C:\about_command_syntax.txt


##################################
##  Help Commands for Providers ##
##################################
#Get-Help registry
#Get-Help FileSystem
#Get-Help -Category provider

Get-Command -CommandType Alias

cls
Get-Command -CommandType Function

cls
$loc = Get-Location
$loc | Get-Member -MemberType Property