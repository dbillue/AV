#Get-IISAppPool
#Get-IISSite

Clear

iisreset /stop

$blazorFamilyAppIISAppPool = Get-IISAppPool $IISsiteName
$IISAppPool = "BlazorFamilyApp"
$IISAppPoolState = Get-WebAppPoolState $IISAppPool
$IISSiteName = "BlazorFamilyApp"
$solutionPath = "C:\Users\dbill\source\repos\AV\AV\VS\FamilyApp\FamilyApp.sln"
$outputPath = "C:\inetpub\wwwroot\BlazorFamilyApp\"

Write-Host "Begin build and publish..."
Write-Host "Solution name and directory:" $solutionPath
Write-Host "Output directory:" $outputPath

dotnet publish $solutionPath -o $outputPath

iisreset /start