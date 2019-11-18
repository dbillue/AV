##############################
# Author:		Duane Billue
# Date:			2019-11-17
# Description:	File open and read / print to output
##############################

$opened = open(FH, '<', 'C:\Data\Resume_files\trace.html');
$line = <FH>;
#print ("First Line: "  $line);
while(<FH>)
{
	print $_;
}
close(FH);