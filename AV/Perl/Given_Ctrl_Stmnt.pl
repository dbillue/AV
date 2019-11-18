##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-17
# Description:	Given control statement
##############################

use v5.10;
use warnings;
#use strict;

$color;
$code;

print("Enter a RGB color :-)\n");
$color = <STDIN>;
chomp($color);
$color = uc($color);

switch($color)
{
	when ('RED') { $code = '#FF0000'; }
	when ('GREEN') { $code = '#00FF00'; }
	when ('BLUE') { $code = '#0000FF'; }
	default { $code = ''; }
}

if($code ne '')
{
	print("color of $color is $code\n");
} else {
	print("$color is is not a RGB color\n");
}
