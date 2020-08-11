# Author: Duane Billue
# Date: 2020-08-10
# Description: Automated publishing script for REST API family application.

Clear

iisreset /stop

$IISAppPool = "FamilyRESTAPI"
$IISSiteName = "FamilyRESTAPI"
$FamilyAPIRESTIISAppPool = Get-IISAppPool $IISsiteName
$IISAppPoolState = Get-WebAppPoolState $IISAppPool
$solutionPath = "C:\Users\dbill\source\repos\AV\AV\VS\FamilyDemoAPIv2\FamilyDemoAPIv2.sln"
$outputPath = "C:\inetpub\wwwroot\FamilyRESTAPI\"

Write-Host "Begin build and publish..."
Write-Host "Solution name and directory:" $solutionPath
Write-Host "Output directory:" $outputPath

dotnet publish $solutionPath -o $outputPath

iisreset /start