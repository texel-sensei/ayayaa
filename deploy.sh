#! /usr/bin/env bash

set -x

chmod ugo+x create_release.sh

source $NVM_DIR/nvm.sh
nvm --version
nvm install lts/*
npm install semantic-release@15
npm install @semantic-release/exec
npx semantic-release@15
