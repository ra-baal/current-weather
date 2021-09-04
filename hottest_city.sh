#!/bin/bash

LC_ALL=C

if [[ -z "$GET_CURRENT_WEATHER_BIN" ]]
then
	echo "The GET_CURRENT_WEATHER_BIN variable is not set."
	exit 1
fi

get_current_weather () {
	result=`${GET_CURRENT_WEATHER_BIN} --city "$1" --units metric`
	if (( $? == 0 ))
	then
		echo "$1" "("`echo $result | cut -d "|" -f 2`")"
	else
		echo -n
	fi
}

#results=
{
	while read city
	do
		get_current_weather "$city" &
	done

	wait
} | sort -r -g -t "(" -k 2 | head -n 3

exit 0
