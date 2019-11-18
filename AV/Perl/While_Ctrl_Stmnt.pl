##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-17
# Description:	While loop control statement
##############################

use warnings;
#use strict;

my $counter = 10;

# 1st Example
while($counter > 0) 
{
	print($counter, "\n");
	$counter--;
	sleep(1);
	if($counter == 0)
	{
		print ("Happy NY!!!!\n");
	}
}

# 2nd Example
$i = 5;
print($i--, "\n") while($i > 0);