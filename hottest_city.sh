#!/bin/bash

LC_ALL=C

if [[ -z "$GET_CURRENT_WEATHER_BIN}" ]]
then
	echo "The GET_CURRENT_WEATHER_BIN variable is not set."
	exit 1
fi

get_current_weather () {
	result=`${GET_CURRENT_WEATHER_BIN} --city "$1" --units metric`
	if (( $? == 0 ))
	then
		echo $result
	else
		echo -n
	fi
}

results=
{   
	while read city
	do
		get_current_weather "$city" &
	done

	wait

} | sort -t "|" -k 2,2 -g -r | head -n 3

ifs=$IFS
IFS='\n'
for r in $results
do
	echo "$r" | cut -d "|" -f 2 # doesnt work properly !!!
done
IFS=$ifs



#declare -A array

#array[$city]=`echo "$result" | cut -d "|" -f 2`
#echo "$city (${array[$city]})"


exit 0
