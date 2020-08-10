Clear

$IISAppPoolState = Get-WebAppPoolState $IISAppPool
Write-Host $IISAppPoolState.Value

#--------------------------------#

If($IISAppPoolState.Value -eq "Started")
{
    Write-Host "Stopping IIS app pool" $IISAppPool
    Stop-WebAppPool -Name $IISAppPool -Passthru:$false

    do
    {
        Write-Host "Stopping IIS app pool" $IISAppPool
    }
    while ($IISAppPoolState.Value -eq "Stopping")
}

Write-Host "Stopping IIS site" $IISsiteName
Stop-IISSite -Name $IISSiteName -Confirm:$false

#--------------------------------#

If($IISAppPoolState.Value -eq "Stopped")
{
    Write-Host "Starting IIS app pool" $IISAppPool
    Start-WebAppPool -Name $IISAppPool -Passthru:$false

    do
    {
        Write-Host "Starting IIS app pool" $IISAppPool
    }
    while ($IISAppPoolState.Value -eq "Stopping")
}

Write-Host "Starting IIS site" $IISsiteName
Start-IISSIte  -Name "BlazorFamilyApp"