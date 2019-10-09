#--------------==--------------#
#     Files and Folders        #
#--------------==--------------#
# Duane Billue
# 2019-04-23

#!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!#
#
# --== Powershell can show symbolic links both soft and hard ==--
#
#PS C:\Documents and Settings> dir
#dir : Access to the path 'C:\Documents and Settings' is denied.
#At line:1 char:1
#+ dir
#+ ~~~
#    + CategoryInfo          : PermissionDenied: (C:\Documents and Settings:String) [Get-ChildItem], UnauthorizedAccessException
#    + FullyQualifiedErrorId : DirUnauthorizedAccessError,Microsoft.PowerShell.Commands.GetChildItemCommand
#
#
#PS C:\Documents and Settings> 
#!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!#

Clear-Host
$FileTimeMS = (Get-Date).Millisecond.ToString()
$FileDate = (Get-Date).ToShortDateString().ToString().Replace("/", "") + "_" + ($FileTimeMS - ($FileTimeMS * 2))

#------------------<*>-------------------#
#    Get all items within a  directory   #
#------------------<*>-------------------#
#$FileFolder = Get-ChildItem -Attributes R, S, D -Path C:\ -Force
#$FilePath = "C:\Users\dbill\Documents\PSScripts\Files and Folders\FilesandFolderListing_Root_" + $FileDate + ".txt"
#$FileFolder = Get-ChildItem -File -Attributes H -Path C:\ -Force -Recurse | Out-File -FilePath $FilePath

#$FilePath = "C:\Users\dbill\Documents\PSScripts\Files and Folders\DLL_FilesandFolderListing_Root_" + $FileDate + ".txt"
#$FileFolder = Get-ChildItem -Path C:\ -Recurse -Include *.exe `
#    | Where-Object -FilterScript {($_.LastWriteTime -gt '2018-09-24')} `
#    | Sort-Object -Property LastWriteTime `
#    | Out-File -FilePath $FilePath


#-------------<*>-------------#
#       Search Patterns       #
#-------------<*>-------------#
# * 
# ??? (filename 3 characters long)
# [kbd] (filenames that start with contained etters)
# -Exclude [a-y]*.dll (excludes all dll(s) except for letter z

#------------------------------#
#$FilePath = "C:\Users\dbill\Documents\PSScripts\Files and Folders\All_FilesandFolderListing_Root_" + $FileDate + ".txt"
#$FilePath = Get-ChildItem -Path C:\Windows\system32\* `
#    | Out-File -FilePath $FilePath

#$FilePath = "C:\Users\dbill\Documents\PSScripts\Files and Folders\DLL_FilesandFolderListing_Root_" + $FileDate + ".txt"
#$FilePath = Get-ChildItem -Path C:\Windows\system32\??????.dll `
#    | Out-File -FilePath $FilePath

#$FilePath = "C:\Users\dbill\Documents\PSScripts\Files and Folders\All_FilesandFolderListing_Root_" + $FileDate + ".txt"
#$FilePath = Get-ChildItem -Path C:\Windows\system32\[k]* `
#    | Out-File -FilePath $FilePath

#! Exclude parameter !#
#$FilePath = "C:\Users\dbill\Documents\PSScripts\Files and Folders\DLL_FilesandFolderListing_Root_" + $FileDate + ".txt"
#$FilePath = Get-ChildItem -Path C:\Windows\System32\*.dll -Recurse -Exclude [a-c]*.dll `
#    | Out-File -FilePath $FilePath


#-------------<*>-------------#
#    Copy Files and Folders   #
#-------------<*>-------------#
#Copy-Item -Path 'C:\Users\dbill\Documents\PSScripts\Files and Folders\*' -Destination C:\Temp\ -Force
#Copy-Item -Filter *.* -Path 'C:\Users\dbill\Documents\PSScripts\Files and Folders\' -Recurse -Destination C:\Temp\ -Force


#-----------------<*>-----------------#
#  Create, Delete Files and Folders   #
#-----------------<*>-----------------#
#New-Item -Path 'C:\temp\Apple' -ItemType Directory
#New-Item -Path 'C:\temp\Apple\Fiji.txt' -ItemType File
#Remove-Item -Path 'C:\temp\Apple' -Recurse


#-----------------------------<*>---------------------------#
#    Mapping a Local Folder as a Windows Accessible Drive   #
#-----------------------------<*>---------------------------#
#subst n: C:\Temp
#subst n: /d

#$FilePath = "C:\Users\dbill\Documents\PSScripts\Files and Folders\MountedDrives_Root_" + $FileDate + ".txt"
#Get-PSDrive | Sort-Object -Property Provider | Out-File -FilePath $FilePath


#--------------<*>--------------#
#    Working with Drives        #
#--------------<*>--------------#
Clear-Host
#Get-Disk
Get-Disk -Number 0
#Get-Disk | Where-Object -FilterScript {$_.BusType -Eq "USB"}
#Get-DiskImage -ImagePath "D:\"
#Dismount-DiskImage -ImagePath "D:\"


#-----------------<*>-------------------#
#   Reading a Text File into an Array   #
#-----------------<*>-------------------#
#$FilePath = "C:\Temp\Apple\Fiji.txt"
#Get-Content -Path C:\Temp\Apple\Fiji.txt
#(Get-Content -Path $FilePath).Length #line count (array pipe)

#$intCnt = 1
#$Apples = Get-Content -Path $FilePath
#"Array Length: " + (Get-Content -Path $FilePath).Length
#foreach ($Apple in $Apples) 
#{ 
#    $Apple + ": " + $intCnt
#    $intCnt = $intCnt + 1
#}