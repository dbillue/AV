#-----------------==-----------------#
#     Enumerations Statements        #
#-----------------==-----------------#
# Duane Billue
# 2019-04-2x


#-----------------<*>-------------------#
#   Reading a Text File into an Array   #
#-----------------<*>-------------------#
#$FilePath = "C:\Temp\Apple\Fiji.txt"
#Get-Content -Path C:\Temp\Apple\Fiji.txt
#(Get-Content -Path $FilePath).Length #line count (array pipe)


#-----<*>-----#
#   ForEach   #
#-----<*>-----#
#$intCnt = 1
#$Apples = Get-Content -Path $FilePath
#"Array Length: " + (Get-Content -Path $FilePath).Length
#foreach ($Apple in $Apples) 
#{ 
#    $Apple + ": " + $intCnt
#    $intCnt = $intCnt + 1
#}
