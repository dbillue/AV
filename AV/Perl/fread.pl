$opened = open(FH, '<', 'C:\Data\Resume_files\trace.html');
$line = <FH>;
#print ("First Line: "  $line);
while(<FH>)
{
	print $_;
}
close(FH);