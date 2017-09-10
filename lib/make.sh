#!/bin/sh
cd rtmidi-3.0.0/
make
cp .libs/librtmidi.dylib ../../RtMidi.Core
cd ..

