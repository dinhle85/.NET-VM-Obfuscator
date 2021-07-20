using IL_Emulator_Dynamic;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using VMExample.Instructions;

namespace ConversionBack
{
    public class 哦勒艾艾伊吉
    {

        public static OpCode[] oneByteOpCodes;
        public static OpCode[] twoByteOpCodes;
        public static StackTrace stackTrace;
        public static System.Reflection.Module callingModule;

        public static byte[] byteArrayResource;
        public static a bc;

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetProcAddress", ExactSpelling = true)]
        static extern IntPtr e(IntPtr intptr, string str);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "GetModuleHandle")]
        static extern IntPtr ab(string str);

        public delegate void a(byte[] bytes, int len, byte[] key, int keylen);
        public static void 哦勒伊吉ҒӺGҐЯҒ(string resName)
        {
            var chars = "ДЬҪDЭӺGҤЇJҜLԠЙФPQЯSҬЏVШӼҰZ诶比西迪伊艾弗吉艾尺艾杰开艾勒艾1234567890øæåüäöÀﻋﻚﻺﺰﺩﻑﺡﺶꝌḶᶵᶽṇ۞۝ۖ";
            var stringChars = new char[450000];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
           
            int num1 = new Random().Next(1000, 5000);

            char c2 = Convert.ToChar(num1);
            char c = c2;
            short code = (short)c;
            ushort code2 = (ushort)c;
            string final = "\\u" + code2;
            callingModule = new StackTrace().GetFrame(1).GetMethod().Module;

            byteArrayResource = extractResource(resName);
            byte[] tester = extractResource("EvanVM");
            VMExample.Instructions.All.binr = new BinaryReader(new MemoryStream(tester));
            VMExample.Instructions.All.val = new ValueStack();
            VMExample.Instructions.All.val.parameters = new object[1];
            All.val.parameters[0] = byteArrayResource;

            All.val.locals = new object[10];
            VMExample.Instructions.All.run();
            IntPtr abb;
            IntPtr def;
            if (IntPtr.Size == 4)
            {
                byte[] tester2 = extractResource("X86");
                EmbeddedDllClass.ExtractEmbeddedDlls("NativePRo.dll", tester2);
                abb = EmbeddedDllClass.LoadDll("NativePRo.dll");
                def = e(abb, " " + final + finalString);
            }
            else
            {
                byte[] tester2 = extractResource("X64");
                EmbeddedDllClass.ExtractEmbeddedDlls("NativePRo.dll", tester2);
                abb = EmbeddedDllClass.LoadDll("NativePRo.dll");
                def = e(abb, "a");
            }

          //   a(x,x,x,x) 0000000010001070 1

            bc = (a)Marshal.GetDelegateForFunctionPointer(def, typeof(a));
            byteArrayResource = (byte[])All.val.locals[1];
            // process all opcodes into fields so that they relate to the way i process them in the conversion to method
            var array = new OpCode[256];
            var array2 = new OpCode[256];
            oneByteOpCodes = array;
            twoByteOpCodes = array2;
            var typeFromHandle = typeof(OpCode);
            var typeFromHandle2 = typeof(OpCodes);
            foreach (var fieldInfo in typeFromHandle2.GetFields())
                if (fieldInfo.FieldType == typeFromHandle)
                {
                    var opCode = (OpCode)fieldInfo.GetValue(null);
                    var num = (ushort)opCode.Value;
                    if (opCode.Size == 1)
                    {
                        var b = (byte)num;
                        oneByteOpCodes[b] = opCode;
                    }
                    else
                    {
                        var b2 = (byte)(num | 65024);
                        twoByteOpCodes[b2] = opCode;
                    }
                }
        }

        static byte[] extractResource(string resourceName)
        {
            using (Stream stream = callingModule.Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                byte[] array = new byte[stream.Length];
                stream.Read(array, 0, array.Length);
                return array;
            }
        }
    }
}