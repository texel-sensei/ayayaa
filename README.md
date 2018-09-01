# Ayayaa
| master | develop |
| ------ | ------- |
| [![Build Status](https://travis-ci.org/texel-sensei/ayayaa.svg?branch=master)](https://travis-ci.org/texel-sensei/ayayaa) | [![Build Status](https://travis-ci.org/texel-sensei/ayayaa.svg?branch=develop)](https://travis-ci.org/texel-sensei/ayayaa) |


[![semantic-release](https://img.shields.io/badge/%20%20%F0%9F%93%A6%F0%9F%9A%80-semantic--release-e10079.svg)](https://github.com/semantic-release/semantic-release)

Ayayaa (And Yet another 'Yet another' Acronym) is an Picture management software based on a client server architecture.

The Server offers image storage and managment (like tagging and searching for tags) while clients can upload and display images.
Communication between those functions via and [REST](https://spring.io/understanding/REST) API. The API is currently neither complete
nor stable.

## Available tools
Server binaries can be downloaded from the [Releases](https://github.com/texel-sensei/ayayaa/releases) section. The server runs on
[dotnet core](https://www.microsoft.com/net/download) and works on windows and linux.

An experimental python client for watching a directory and uploading all images copied into it is [available](src/picture_inputs/directory_watcher) but still in a very experimental phase.

## Contributing
Please read the [Contribution Guidelines](CONTRIBUTING.md).
