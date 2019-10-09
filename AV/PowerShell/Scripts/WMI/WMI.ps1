#-----==-----#
#     WMI    #
#-----==-----#
# Duane Billue
# 2019-04-27

# MS Docs URL
# https://docs.microsoft.com/en-us/powershell/scripting/samples/getting-wmi-objects--get-wmiobject-?view=powershell-6

Clear-Host
$FileTimeMS = (Get-Date).Millisecond.ToString()
$FileDate = (Get-Date).ToShortDateString().ToString().Replace("/", "") + "_" + ($FileTimeMS - ($FileTimeMS * 2))

##################
#  Get WMI-Objs  #
##################
$FilePath = "C:\Users\dbill\Documents\PSScripts\WMI\WMI_" + $FileDate + ".txt"
Get-WmiObject -List `
    | Sort-Object -Property Name `
    | Out-File -FilePath $FilePath


# Get WMI-Obj list using IP
Clear-Host
Get-WmiObject -List -ComputerName 192.168.0.14 `
    | Sort-Object -Property Name


# Get WMI-Obj list using computer name
Clear-Host
Get-WmiObject -List -ComputerName "Cradle-Laptop" `
    | Sort-Object -Property Name


# Get WMI-Obj class  Win32_Product #
Clear-Host
Get-WmiObject -Class Win32_Product -ComputerName . `
    | Sort-Object -Property Name