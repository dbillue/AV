##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-19
# Description:	Do Until Continue loop control statement
##############################

use warnings;
#use strict;

$x = 0;
$y = 10;

do
{{
	$x++;
	next if $x % 2 == 1;
	print($x, "\n");
}}until ($x == $y);

print("\n\n");

$needle = int(rand(10));
$max = 4;
$attempt = 0;
$num = 0;

print ("Enter a number (0 - 9) to match, 3 times ONLY, -1 to quit:\n");

game:
{	do
	{
		if($attempt < $max)
		{
			print(">");
			$num = int(<STDIN>);
			last if $num == -1;
			$attempt++;
		} else {
			print ("Game over!\n");
			last;
		}
	}
}
	