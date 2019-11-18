##############################
# Author:		Duane Billue / https://www.perltutorial.org/
# Date:			2019-11-17
# Description:	Hash
##############################

use warnings;
#use strict;

%countries = 
(
	England => 'English',
	France => 'French',
	Spain => 'Spanish',
	China => 'Chinese',
	Germany => 'German'
);

$lang = $countries{'England'};
print ($lang, "\n");

#Add new element
$countries{'Italy'} = 'Italian';

#Delete element
delete $countries{'China'};

#Enumerate hash elements
for(keys %countries)
{
	print("Country: ", $_, " Language: ", $countries{$_}, "\n");
}
