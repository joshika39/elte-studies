#!/bin/bash

while read line
do
	echo $line
	workers+=( "$line" )
done < $1 

read -p "Kérem a besorolást (c. részfeladat): " besor
pultosok=()
szorgosok=()
customBesor=0

for worker in "${workers[@]}"
do
    IFS=',' read -r -a details <<< "$worker"

    for index in "${!details[@]}"
    do
        if [[ "${details[$index]::1}" == " " ]]; then
            details[$index]=${details[$index]:1}
        fi
    done

    if [[ "${details[1]}" == "Pultos" ]]; then
        pultosok+=( "${details[0]}," )
    fi
    if (( ${#details[@]} > 4 )); then
        szorgosok+=( "${details[0]}," )
    fi
    if [[ "${details[1]}" == "$besor" ]]; then
        (( customBesor++ ))
    fi
done

echo "a) Pultosok: ${pultosok[*]}"
echo "b) Szorgosok (3+ munkakor): ${szorgosok[*]}"
echo "c) $besor besorolassal: $customBesor szemely"
