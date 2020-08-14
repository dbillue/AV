Clear

# Login to Azure
#Connect-AzureRmAccount

# Change server connection type
az sql server conn-policy update --connection-type Default

# Obtain server connection policy
az sql server conn-policy show
