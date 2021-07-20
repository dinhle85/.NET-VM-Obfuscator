using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ConversionBack
{
    public class EmbeddedDllClass
    {
        static string tempFolder = "";

        /// <summary>
        /// Extract DLLs from resources to temporary folder
        /// </summary>
        /// <param name="dllName">name of DLL file to create (including dll suffix)</param>
        /// <param name="resourceBytes">The resource name (fully qualified)</param>
        public static void ExtractEmbeddedDlls(string dllName, byte[] resourceBytes)
        {
            var assem = Assembly.GetExecutingAssembly();
            string[] names = assem.GetManifestResourceNames();
            var an = assem.GetName();

            // The temporary folder holds one or more of the temporary DLLs
            // It is made "unique" to avoid different versions of the DLL or architectures.
            tempFolder = string.Format("{0}.{1}.{2}", an.Name, an.ProcessorArchitecture, an.Version);

            var dirName = Path.Combine(Path.GetTempPath(), tempFolder);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }

            // Add the temporary dirName to the PATH environment variable (at the head!)
            var path = Environment.GetEnvironmentVariable("PATH");
            string[] pathPieces = path.Split(';');
            var found = false;
            foreach (string pathPiece in pathPieces)
            {
                if (pathPiece == dirName)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Environment.SetEnvironmentVariable("PATH", dirName + ";" + path);
            }

            // See if the file exists, avoid rewriting it if not necessary
            var dllPath = Path.Combine(dirName, dllName);
            var rewrite = true;
            if (File.Exists(dllPath))
            {
                byte[] existing = File.ReadAllBytes(dllPath);
                if (Equality(resourceBytes, existing))
                {
                    rewrite = false;
                }
            }

            if (rewrite)
            {
                File.WriteAllBytes(dllPath, resourceBytes);
            }
        }

        public static bool Equality(byte[] a1, byte[] b1)
        {
            int i;
            if (a1.Length == b1.Length)
            {
                i = 0;
                while (i < a1.Length && (a1[i] == b1[i])) //Earlier it was a1[i]!=b1[i]
                    i++;


                if (i == a1.Length)
                {
                    return true;
                }
            }

            return false;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string dllToLoad, IntPtr hFile, uint flags);

        /// <summary>
        /// managed wrapper around LoadLibrary
        /// </summary>
        /// <param name="dllName"></param>
        static public IntPtr LoadDll(string dllName)
        {
            if (tempFolder == "")
            {
                throw new Exception("Please call ExtractEmbeddedDlls before LoadDll");
            }

            var h = LoadLibraryEx(dllName, IntPtr.Zero, 0);
            if (h == IntPtr.Zero)
            {
                Exception e = new Win32Exception();
                throw new DllNotFoundException("Unable to load library: " + dllName + " from " + tempFolder, e);
            }

            return h;
        }
    }
}