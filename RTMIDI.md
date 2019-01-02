# RtMidi

We are using a fork of [rtmidi](https://github.com/micdah/rtmidi) where the `rtmidi_c` files have been adapted for
better usage with _RtMidi.Core_.

The fork is regularly merged with _upstream_, ensuring fixes, improvements etc are carried over into the version we are
using. 

## Compiling

Guides on how to compile the runtime-dependent library binary.

### OS X

1. Open terminal and navigate to the root of the repository
n1. Compile project: 
    ```bash
    $ ./autogen.sh --no-configure
    $ ./configure
    $ make
    ```
1. Copy the newly compiled `dylib` file into the _RtMidi.Core_ project:
    ```bash
    $ cp .libs/librtmidi.dylib ~/git/RtMidi.Core/RtMidi.Core/librtmidi.dylib
    ```

#### Pre-requisites

Requires `automake`, `autoconf` and `libtool` to be installed, which is easily done using Homebrew:

```bash
$ brew install autoconf automake libtool
```


### Windows (64 bit)

1. From the root of the repository, navigate to `msw/`
1. Open the solution file `rtmidilib.sln`
1. Change configuration to _**Export**_
1. Change target framework to _**x64**_
1. Build solution
1. Copy newly compiled `dll` file for x64 into the _RtMidi.Core_ project:
    ```bash
    $ cp msw/x64/Export/rtmidilib.dll ~/git/RtMidi.Core/RtMidi.Core/librtmidi.dylib
    ```
1. Change target framework to _**Win32**_
1. Build solution
1. Copy newly compiled `dll` file for x86 into the _RtMidi.Core_ project:
    ```bash
    $ cp msw/Export/rtmidilib.dll ~/git/RtMidi.Core/RtMidi.Core/rtmidi32.dll
    ```

#### Pre-requisites

Requires Visual Studio 2017 (_or newer_) with the following components installed (_use Visual Studio Installer_)

* Workload: Desktop development with C++
* Individual components:
  * Windows 8.1 SDK
  * Windows Universal CRT SDK