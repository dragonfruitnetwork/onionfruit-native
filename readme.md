# OnionFruit Native Libraries
Provides native libraries from the Tor website as pacakges for supported operating systems

[![](https://img.shields.io/nuget/v/dragonfruit.onionfruit.native.win?label=win&logo=nuget)](https://nuget.org/packages/dragonfruit.onionfruit.native.win)
[![](https://img.shields.io/nuget/v/dragonfruit.onionfruit.native.osx?label=osx&logo=nuget)](https://nuget.org/packages/dragonfruit.onionfruit.native.osx)

### Example Locator Script
> When publishing, the correct binaries are copied directly to the output folder. If in debug or non-publish build, the files can be found under `runtimes/{rid}/{arch}/native`

```csharp
public static class TorResolver
{
    /// <summary>
    /// Attempts to locate the tor executable, returning the full path if found.
    /// </summary>
    /// <remarks>
    /// Currently not supported on linux platforms
    /// </remarks>
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("linux")]
    [UnsupportedOSPlatform("android")]
    public static string LocateTorExecutable()
    {
        var filename = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "tor.exe" : "tor";
        return EnumeratePotentialDirectories().Select(targetDir => Path.Combine(targetDir, filename)).FirstOrDefault(File.Exists);
    }

    private static IEnumerable<string> EnumeratePotentialDirectories()
    {
        // app dir (in case of published program)
        yield return AppDomain.CurrentDomain.BaseDirectory;

        // native executable location (debug/local build)
        string platformName;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            platformName = "win";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            platformName = "osx";
        }
        else
        {
            throw new PlatformNotSupportedException("OnionFruit NativeLibs are currently only available on Windows and macOS");
        }

        var genericRid = $"{platformName}-{RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant()}";
        yield return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "runtimes", genericRid, "native");
    }
}
```

### License
The project is licensed under the MIT Licence. Please note the Tor is distributed under a different license, which has been provided with the binaries
