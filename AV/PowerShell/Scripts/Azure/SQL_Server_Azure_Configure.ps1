Clear

# Login to Azure
# Connect-AzureRmAccount

# Set database variables
$resourceGroup = "AVSQLDemo"
$server = Get-AzureRMSqlServer -ResourceGroupName $resourceGroup
$logical_server = $server.ServerName
$databaseName = Get-AzureRmSqlDatabase `
                -ResourceGroupName $resourceGroup `
                -ServerName $logical_server `
                | Where DatabaseName -Like "Adventure*"
$databaseName = $databaseName.DatabaseName

# Set resrouce group and azure server
az configure --defaults resource-group=$resourceGroup sql-server=$logical_server

az configure --list-defaults