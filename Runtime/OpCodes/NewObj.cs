using System;
using System.Reflection;

namespace VMExample.Instructions
{
    class NewObj : Base
    {
        public override void emu()
        {
            var mdtoken = All.binr.ReadInt32();
            var metho = (ConstructorInfo)typeof(Random).GetConstructor(new Type[] { typeof(int) });

            object[] typ = new object[metho.GetParameters().Length];
            for (int i = 0; i < typ.Length; i++)
                typ[i] = All.val.valueStack.Pop();

            var a = Activator.CreateInstance(metho.DeclaringType, typ);
            All.val.valueStack.Push(a);
        }
    }
}