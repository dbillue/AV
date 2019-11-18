##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-17
# Description:	Perl programming tutorials
##############################

use warnings;
#use strict;

our $c; #global var

$opened = open(FH, '>>', 'C:\Data\Resume_files\blank.txt');
print ($opened, "\n");
$str = "\n123457890";
print FH $str;
close(FH);

#program block
{
$c = "ABCD";  
my $v = "XYZ"; #local var
print ($c, "\n", $v, ,"\n");
}

#variable scope
print ($c, "\n", $v);

print uc("alexandra "); #upper case
print lc("BILLUE");		#lower case

#Index (front and reverse)
{
$departments = "Department HR, IT and ACCOUNTING";
$charpos = index($departments, "ACC");
print ("\nPosition of ACC: ", $charpos);
$charpos = rindex($departments, "IT");
print ("\nPosition of IT from reverse: ", $charpos);
}

#String funcs 
#Concatenation
print "\n\nMy momma told me" . " You better shop around...\n\n";

#Repition
print "\nyes!" x 4, "\n";

#chomp()
#chomp($in = <STDIN>);
#print $in;

#Index
$arr = (1, 2, 3, 4, 5);
print ("Element at position 2: ", (1, 2, 3, 4, 5)[1]);
print "\n";
print ("Element at position 3: ", ("Red", "White", "Blue")[2]);

print "\n\n###### Arrays ######";

#Array ...use the @ symbol
@colors = qw(RED WHITE BLUE YELLOW GREEN);
print ("\n\n", $colors[3]);
print ("\n", @colors[-3..-1]); #build list in reverse order

#Array length (UBound)
print ("\nUBOUND of colors array: ", $#colors);

#Array sort alpha
@list = qw(command argument lexical index);
@listSorted = sort @list;
print ("\nOriginal array: ", @list, "\nAlpha sorted array: ", @listSorted);

#Array sort numeric
@numList = qw/10 55 200 11/;
@numListSorted = sort { $a <=> $b } @numList;
print ("\nOriginal array: ", @numList, "\nNumeric sorted array: ", @numListSorted);

#Array sort reverse alpha
@newList = qw(heart lungs brain phalanges);
@newRevSortedList = reverse sort @newList;
print ("\nOriginal array: ", @newList, "\nAlpha sorted array reverse: ", @newRevSortedList);

#Array sort reverse numeric 
@numList = qw/10 55 200 11/;
@numListSorted = sort { $b <=> $a } @numList;
print ("\nOriginal array: ", @numList, "\nNumeric sorted array reverse: ", @numListSorted);