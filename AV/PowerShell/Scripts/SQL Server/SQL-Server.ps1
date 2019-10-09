Clear-Host

#
# Needs Work #
#

$Wmi = (get-item).ManagedComputer
$DfltSQLInstance = $Wmi.Services['MSSQL$instancename'] 
$DfltSQLAgentInstance = $Wmi.Services['SQLSERVERAGENT']
$DfltSQLBrowserInstance = $Wmi.Services['SQLBROWSER'] 

# Display the state of the service.  
$DfltInstance  
# Start the service.  
$DfltInstance.Start();  
# Wait until the service has time to start.  
# Refresh the cache.  
$DfltInstance.Refresh();   
# Display the state of the service.  
$DfltInstance  
# Stop the service.  
$DfltInstance.Stop();  
# Wait until the service has time to stop.  
# Refresh the cache.  
$DfltInstance.Refresh();   
# Display the state of the service.  
$DfltInstance  
