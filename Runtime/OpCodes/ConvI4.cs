﻿namespace VMExample.Instructions
{
    class ConvI4 : Base
    {
        public override void emu()
        {
            var val = All.val.valueStack.Pop();
            All.val.valueStack.Push((int)val);
        }
    }
}