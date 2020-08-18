###########################################################
# Author: Duane Billue
# Date: 2020-08-15
# Description: Azure SQL Server VM template
###########################################################

Clear

# Login to Azure
#Connect-AzureRmAccount

$adminSqlLogin = "av"
# $password = Read-Host "Your username is 'av'. Please enter a password for your Azure SQL Database server that meets the password requirements"
$password = "All13B1llu3"
# Prompt for local ip address
$ipAddress = "73.184.17.17"
# Get resource group and location and random string
$resourceGroup = Get-AzResourceGroup | Where ResourceGroupName -like "Sandbox resource group name"
$resourceGroupName = "Sandbox resource group name"
$uniqueID = Get-Random -Minimum 100000 -Maximum 1000000
$storageAccountName = "mslearnsa"+$uniqueID
$location = $resourceGroup.Location
# The logical server name has to be unique in the system
$serverName = "aw-server$($uniqueID)"