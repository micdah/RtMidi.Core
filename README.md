# RtMidi.Core

[![Build status](https://ci.appveyor.com/api/projects/status/tfn3a8lhmiyla5ox/branch/master?svg=true)](https://ci.appveyor.com/project/MichaelDahl/rtmidi-core/branch/master)

MIDI support on both Winows (64-bit) and Mac OS X for .Net Standard 2.0 with support for both input and output midi devices, and support the following midi messages:

* Channel Pressure
* Control Change
* Note On / Off
* Pitch Bend
* Polyphonic Key Pressure
* Program Change
* Non-Registered Parameter Number (NRPN) (_used to send/receive 14-bit parameter and value_)

## Example
**TODO** How to use this library will be described here in the near future

### RtMidi Version
We are using a fork off [rtmidi](https://github.com/thestk/rtmidi) `master` branch with a few changes (_you can see a diff [here](https://github.com/thestk/rtmidi/compare/master...micdah:master) between our fork and official repository_) to make it possible to build on the platforms we are interested in and with changes to better support .Net P/Invoke.

You can find our fork at [micdah/rtmidi](https://github.com/micdah/rtmidi).