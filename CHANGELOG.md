# Changelog

## 1.0.47

First documented release with initial features and C# 7 `readonly struct` and `in` features to improve performance by
making messages _immutable structs_ and passing them by reference, thus reducing copy operations and allowing the
compiler to optimize the code.

Supported MIDI messages:
 - Channel Pressure
 - Control Change
 - Note On / Off
 - Pitch Bend
 - Polyphonic Key Pressure
 - Program Change
 - Non-Registered Parameter Number (_NRPN_)