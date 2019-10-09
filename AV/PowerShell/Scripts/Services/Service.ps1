#--------------==--------------#
#           Services           #
#--------------==--------------#
# Duane Billue


clear-host
$Service = get-service -name WirelessButtonDriver64
"Service Name: " + $Service.Name
"Status: " + $Service.Status
if ($Service.CanStop) { $Service.WaitForStatus('Stopped') }
"Status: " + $Service

$serviceName = 'XblAuthManager'
clear-host
$Service = get-service -name $serviceName
$Service
#-----
#Set service status based on existing status
#if ($Service.Start) { $Service.WaitForStatus('Stopped') }
#-----
$Service