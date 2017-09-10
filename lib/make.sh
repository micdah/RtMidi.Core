#!/bin/sh
cd rtmidi-3.0.0/
make clean
make
cp .libs/librtmidi.dylib ../../RtMidi.Core
cd ..

