// OnionFruit Native Copyright DragonFruit Network <inbox@dragonfruit.network>
// Licensed under the MIT License. Refer to licence.md for more info

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace DragonFruit.OnionFruit.Native
{
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
}