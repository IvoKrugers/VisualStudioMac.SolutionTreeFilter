#!/bin/sh
clear

SCRIPTFILE=$0

#Get the absolute path to the containing folder
PROJECTFOLDER=${SCRIPTFILE%/*}

cd ${PROJECTFOLDER}

pwd

rm *.mpack
mono /Applications/Visual\ Studio.app/Contents/Resources/lib/monodevelop/bin/vstool.exe setup pack ./VisualStudioMac.SolutionTreeFilter/bin/VisualStudioMac.SolutionTreeFilter.dll
