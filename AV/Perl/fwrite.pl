$opened = open(FH, '>', 'C:\Data\Resume_files\blank.txt');
print ($opened);
$str = "123457890";
print FH $str;
close(FH);