# OnionFruit Native Libraries
[![Latest Nuget](https://img.shields.io/nuget/v/dragonfruit.onionfruit.native?logo=nuget)](https://nuget.org/packages/dragonfruit.onionfruit.native)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://opensource.org/licenses/MIT)

Provides native libraries from the Tor website along with a helper class for locating said binaries.

### Example usage
```csharp
using System.Diagnostics;
using DragonFruit.OnionFruit.Native;

var process = new Process(new ProcessStartInfo
{
    FileName = TorResolver.LocateTorExecutable(),
    Arguments = "-f ./path/to/torrc",
    UseShellExecute = false
});

process.Start();
```

### License
The project is licensed under the MIT Licence. Please note the Tor is distributed under a different license, which has been provided with the binaries