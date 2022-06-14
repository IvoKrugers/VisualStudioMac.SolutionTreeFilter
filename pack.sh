#!/bin/sh
clear

# Get and goto folder of this script's location
SCRIPTFILE=$0
PROJECTFOLDER=${SCRIPTFILE%/*}
cd ${PROJECTFOLDER}
PROJECTFOLDER=$(pwd)

# Clean
rm *.mpack

# Pack
mono /Applications/Visual\ Studio.app/Contents/Resources/lib/monodevelop/bin/vstool.exe setup pack ./VisualStudioMac.SolutionTreeFilter/bin/VisualStudioMac.SolutionTreeFilter.dll

# Uninstall
/Applications/Visual\ Studio\ \(Preview\).app/Contents/MacOS/vstool setup uninstall VisualStudioMac.SolutionTreeFilter -y

# Install
for filename in *.mpack;
do
  echo "$filename"
  /Applications/Visual\ Studio\ \(Preview\).app/Contents/MacOS/vstool setup install "$PROJECTFOLDER/$filename" -y
done
