# Changelog

## 1.0.52
* Added Linux support - thanks [abirke](https://github.com/abirke)

## 1.0.51
* Added missing System Common Messages:
  - MIDI Time Code Quarter Frame
  - Song Position Pointer
  - Song Select
  - Tune Request

## 1.0.50
* Pulled updates for `rtmidi` from [upstream](https://github.com/thestk/rtmidi)

## 1.0.49

* Pulled updates for `rtmidi` from [upstream](https://github.com/thestk/rtmidi) and compiled new unmanaged binaries for
  macOS (_x64_) and Windows (_x64, x86_)
  * Changed the `rtmidi_get_compiled_api` to take an additional `unsigned int apis_size` indicating the size of the
    array for `RtMidiApi` enums
* No changes in public-facing code, so will not break existing code

## 1.0.48.1

* Bug fix for receiving **SysEx** messages - thanks [mat1jaczyyy](https://github.com/mat1jaczyyy)  

## 1.0.48

* Support for sending and receiving **SysEx** messages - thanks [mat1jaczyyy](https://github.com/mat1jaczyyy)

## 1.0.47

First documented release with initial features and C# 7 `readonly struct` and `in` features to improve performance by
making messages _immutable structs_ and passing them by reference, thus reducing copy operations and allowing the
compiler to optimize the code.

Supported MIDI messages:
* Channel Pressure
* Control Change
* Note On / Off
* Pitch Bend
* Polyphonic Key Pressure
* Program Change
* Non-Registered Parameter Number (_NRPN_)