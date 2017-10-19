# RtMidi
We are using a fork of [rtmidi](https://github.com/micdah/rtmidi) where the `rtmidi_c` files have been adapted for better usage with _RtMidi.Core_

# Compiling

## OS X
Requires `automake` and `autoconf` to be installed, easily done if you have Homebrew installed:
```
$ brew install autoconf
$ brew install automake
```

Next, from the root of the `rtmidi/` repository:
```
$ ./autogen.sh --no-configure
$ ./configure
$ make
```

Finally, copy the newly compiled `dylib` file into the _RtMidi.Core_ project:
```
$ cp .libs/librtmidi.dylib ~/git/RtMidi.Core/RtMidi.Core/librtmidi.dylib
```

## Windows (64 bit)
Requires Visual Studio 2017, simply open `rtmidi/msw/rtmidilib.sln` and compile using configuration _Export_ and target framework _x64_ and copy resulting dll from `rtmidi/msw/x64/Export` folder to the _RtMidi.Core_ project folder.

### Pre-requisits
* Visual Studio 2017 (with the following components selected)
    - Workload: Desktop development with C++
    - Individual components:
        + Windows 8.1 SDK
        + Windows Universal CRT SDK