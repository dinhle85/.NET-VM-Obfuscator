using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Core.Injection
{
    class Resource
    {
        static byte[] array;

        public static void setup()
        {
            var chars = "ДЬҪDЭӺGҤЇJҜLԠЙФPQЯSҬЏVШӼҰZ诶比西迪伊艾弗吉艾尺艾杰开艾勒艾1234567890øæåüäöÀﻋﻚﻺﺰﺩﻑﺡﺶꝌḶᶵᶽṇ۞۝ۖ";
            var stringChars = new char[10000];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            if (Debugger.IsAttached) Environment.Exit(0);

            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream("RT"))
            using (StreamReader reader = new StreamReader(stream))
            {
                array = new byte[stream.Length];
                stream.Read(array, 0, array.Length);
            }

            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }

        public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
        {
            byte[] EvanVMRun = new byte[14];
            EvanVMRun[0] = 82;
            EvanVMRun[1] = 0;
            EvanVMRun[2] = 117;
            EvanVMRun[3] = 0;
            EvanVMRun[4] = 110;
            EvanVMRun[5] = 0;
            EvanVMRun[6] = 116;
            EvanVMRun[7] = 0;
            EvanVMRun[8] = 105;
            EvanVMRun[9] = 0;
            EvanVMRun[10] = 109;
            EvanVMRun[11] = 0;
            EvanVMRun[12] = 101;
            EvanVMRun[13] = 0;
            string RuntimeMethod = System.Text.Encoding.Unicode.GetString(EvanVMRun);
            return e.Name.Contains(RuntimeMethod) ? Assembly.Load(array) : null;
        }
    }
}