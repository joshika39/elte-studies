#!/bin/bash

if (( $(id -u) != 0 )) ; then
	echo "Please run with elevated privileges"
	exit
fi

sudo pacman -S dotnet-sdk dotnet-runtime

if [ -d "$HOME/.dotnet/" ] ; then
	echo '$PATH=$HOME/.dotnet/:$PATH' >> $HOME/.bashrc
fi
