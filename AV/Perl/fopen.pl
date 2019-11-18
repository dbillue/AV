##############################
# Author:		Duane Billue
# Date:			2019-11-17
# Description:	File open and write to file
##############################

$opened = open(FH, '>', 'C:\Data\Resume_files\blank.txt');
print ($opened);
$str = "yup";
print FH $str;
close(FH);