#!/bin/bash

if [ $# -lt 1 ]; then
	echo "The pattern is incorrect"
	exit 1
fi

if [ $# -ge 2 ]; then
	pathDir=""
	packageStr=""
	for arg in "${@:2}" 
	do
		pathDir+="${arg}/"
		packageStr+="${arg}."
	done

	classname=${1}
	if [ -f "${pathDir}${classname}.java" ]; then
		javac "${pathDir}${classname}.java"
		java "${packageStr}${classname}"
	else
		echo "File does not exist: ${pathDir}${classname}.java"
	fi
else
	classname=${1}
	if [ -f "${classname}.java" ]; then
		javac "${classname}.java"
		java ${1}
	else
		echo "File does not exists: ${classname}.java!"
	fi
fi
