##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-19
# Description:	Do While loop control statement
##############################

use warnings;
#use strict;

@haystack = qw(1 2 3 4 5 6 7 8 9);
$count = scalar @haystack;
$i = 0;
$needle;

print("Enter a number to search (1 - 9)");
$needle = int(<STDIN>);

findit:
{
	do
	{
		if($haystack[$i] == $needle)
		{
			print("The needle has been found...",  $needle);
		}
		$i++;
	} until ($i == $count);
}
