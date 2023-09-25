#!/usr/bin/sh

szam=$1
szam2=$1
fakt=1
fakt2=1

while [ $szam -ge 1 ] ; do # itt >= a feltetel
	fakt=$((fakt * szam))
	szam=$((szam - 1))
done	
echo "while fact: $fakt"

until [ $szam2 -lt 1 ] ; do # itt annyi, hogy nem <= helyett csak < kell
	fakt2=$((fakt2 * szam2))
	szam2=$((szam2 - 1))
done	

echo "until fact: $fakt2"

