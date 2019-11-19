##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-19
# Description:	Simple RegEx
##############################

use warnings;
#use strict;

$str = "Perl regular expressions is powerful and makes it look easy";
print "Match found..." if($str =~ /ul/);

print "\n\n";

@words = ('Perl', 'regular expression', 'is', 'a very powerful', 'feature');
foreach(@words)
{
	print("$_ \n") if($_ !~ /er/);
}

print "\n\n";

@words = ('dog', 'fog', 'chicken');
for(@words)
{
	print("$_\n") if(/[dfr]og/);
}