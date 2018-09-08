#! /usr/bin/env bash
VERSION="$1"
echo $VERSION

SRC_FOLDER=ayayaa-server/
PROJECT=$SRC_FOLDER/server/server.csproj

FOLDER_NAME="ayayaa-server-$VERSION"
RELEASE_FOLDER=$(pwd)/$FOLDER_NAME
dotnet publish "$PROJECT" -c Release -o "$RELEASE_FOLDER"

SUCC=$?
if [[ $SUCC -ne 0 ]] ; then
	echo "Failed building!"
	exit 1
fi


RUN_SCRIPT="$RELEASE_FOLDER/run.sh"
echo "dotnet ayayaa-server.dll" > "$RUN_SCRIPT"
chmod +x "$RUN_SCRIPT"

cp "$RUN_SCRIPT" "$RELEASE_FOLDER/run.bat"

zip -r "$FOLDER_NAME.zip" "$FOLDER_NAME"
