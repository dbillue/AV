# Author: Duane Billue
# Date: 2020-08-10
# Description: Automated publishing script for Blazor family application.

#Get-IISAppPool
#Get-IISSite

Clear

iisreset /stop

$IISAppPool = "BlazorFamilyApp"
$IISSiteName = "BlazorFamilyApp"
$blazorFamilyAppIISAppPool = Get-IISAppPool $IISsiteName
$IISAppPoolState = Get-WebAppPoolState $IISAppPool
$solutionPath = "C:\Users\dbill\source\repos\AV\AV\VS\FamilyApp\FamilyApp.sln"
$outputPath = "C:\inetpub\wwwroot\BlazorFamilyApp\"

Write-Host "Begin build and publish..."
Write-Host "Solution name and directory:" $solutionPath
Write-Host "Output directory:" $outputPath

dotnet publish $solutionPath -o $outputPath

iisreset /start