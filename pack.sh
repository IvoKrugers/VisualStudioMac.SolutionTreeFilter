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
# mono /Applications/Visual\ Studio\ \(2019\).app/Contents/Resources/lib/monodevelop/bin/vstool.exe setup pack ./VisualStudioMac.SolutionTreeFilter/bin/VisualStudioMac.SolutionTreeFilter.dll
/Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack /Users/ivo/Xamarin_Projects/VS2022.EXTENSIONS/VisualStudioMac.SolutionTreeFilter/VisualStudioMac.SolutionTreeFilter/bin/Release/VisualStudioMac.SolutionTreeFilter.dll 


# Copy to local dir
for filename in /Applications/Visual\ Studio\.app/*SolutionTreeFilter*.mpack;
do
  echo "move $filename"
  mv "$filename" .
done

# Uninstall
# /Applications/Visual\ Studio\ \(Preview\).app/Contents/MacOS/vstool setup uninstall VisualStudioMac.SolutionTreeFilter -y

# # Install
# for filename in *.mpack;
# do
#   echo "$filename"
#   /Applications/Visual\ Studio\ \(Preview\).app/Contents/MacOS/vstool setup install "$PROJECTFOLDER/$filename" -y
# done
