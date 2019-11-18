##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-17
# Description:	For loop / foreach control statement
##############################

use warnings;
#use strict;

@arr = (1..9);

#simple for loop
for(@arr)
{
	print("$_", "\n");
}

#iterator i
for my $i (@arr)
{
	print("$i", "\n");
}

$i = 20;
for $i (@arr)
{
	print("$i", "\n");
}
print('iterator $i is ', "$i", "\n");


