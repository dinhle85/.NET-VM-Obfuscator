namespace VMExample.Instructions
{
    class Ldc : Base
    {
        public override void emu()
        {
            var val = All.binr.ReadInt32();
            All.val.valueStack.Push(val);
        }
    }
}