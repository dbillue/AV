##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-17
# Description:	If, Else, ElsIf control statement
##############################

use warnings;
#use strict;

our ($l_curr, $f_curr, $l_amount, $f_amount);

%rates =
(
	USD => 1,
	YPY => 85.25,
	EUR => 0.78,
	GBP => 0.62,
	CNY => 6.23
);

#Print currencies
print("Supported Currencies:\n");
for(keys %rates)
{
	print(uc($_),"\n");
}

print("Enter local currency:\n");
$l_curr = <STDIN>;

print("Enter foreign currency:\n");
$f_curr = <STDIN>;

print("Enter amount:\n");
$l_amount = <STDIN>;

chomp($l_curr, $f_curr, $l_amount);

if(not exists $rates{$l_curr})
{
	print("Local currency is not supported");
} elsif(not exists $rates{$f_curr}) 
{
	print("Foreign currency is not supported");
} else {
	$f_amount = ($rates{$f_curr} / $rates{$l_curr}) * $l_amount;
	print("$l_amount $l_curr = $f_amount $f_curr","\n");
}
