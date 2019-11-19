##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-19
# Description:	Do Until loop control statement
##############################

use warnings;
#use strict;

$counter = 100;

until($counter == 25)
{
	print($counter, "\n");
	$counter--;
}

print ("\n");
$newcounter = 0;
@keywords = qw(perl makes it look easy);

until(!scalar @keywords)
{
	$counter++;
	print shift(@keywords) . "\n";
}