#! /usr/bin/env bash
VERSION="$1"
echo $VERSION

SRC_FOLDER=src/core/ayaya-server/
PROJECT=$SRC_FOLDER/ayaya-server/ayaya-server.csproj

FOLDER_NAME="ayayaa-server-$VERSION"
RELEASE_FOLDER=$(pwd)/$FOLDER_NAME
dotnet publish "$PROJECT" -c Release -o "$RELEASE_FOLDER"

SUCC=$?
if [[ $SUCC -ne 0 ]] ; then
	echo "Failed building!"
	exit 1
fi


RUN_SCRIPT="$RELEASE_FOLDER/run.sh"
echo "dotnet ayaya-server.dll" > "$RUN_SCRIPT"
chmod +x "$RUN_SCRIPT"

cp "$RUN_SCRIPT" "$RELEASE_FOLDER/run.bat"

zip -r "$FOLDER_NAME.zip" "$FOLDER_NAME"
