#!/bin/bash

export TEST_VAR 

while read city
do
	cmd="${GET_CURRENT_WEATHER_BIN} --city ${city} --units metric"

	echo "$cmd"

	result=$($cmd)

	echo "$result"

done

