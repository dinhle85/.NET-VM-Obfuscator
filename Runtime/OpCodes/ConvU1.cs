using System;

namespace VMExample.Instructions
{
    class ConvU1 : Base
    {
        public override void emu()
        {
            var val = All.val.valueStack.Pop();
            var bt = Convert.ToByte(val);
            All.val.valueStack.Push((int)bt);
        }
    }
}