$opened = open(FH, '>', 'C:\Data\Resume_files\blank.txt');
print ($opened);
$str = "yup";
print FH $str;
close(FH);