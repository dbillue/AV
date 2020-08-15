Clear

# Login to Azure
#Connect-AzureRmAccount

# Set database variables
$resourceGroup = "AVSQLDemo2"
$server = Get-AzureRMSqlServer -ResourceGroupName $resourceGroup
$logical_server = $server.ServerName

$database = Get-AzureRmSqlDatabase `
                -ResourceGroupName $resourceGroup `
                -ServerName $logical_server `
                | Where DatabaseName -Like Demo*

$databaseName = $database.DatabaseName

#Write-Host $resourceGroup
#Write-Host $logical_server
#Write-Host $databaseName

# Set resrouce group and azure server
az configure --defaults resource-group=$resourceGroup sql-server=$logical_server group=$resourceGroup

# Obtain defaults
#az configure --list-defaults

# Obtain out database(s)
#az sql db list

# Obtain database properties
az sql db show --name $databaseName

# Obtain database usage and size
#az sql db list-usages --name $databaseName