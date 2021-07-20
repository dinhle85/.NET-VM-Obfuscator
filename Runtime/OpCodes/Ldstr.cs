namespace VMExample.Instructions
{
    class Ldstr : Base
    {
        public override void emu()
        {
            var str = All.binr.ReadString();
            All.val.valueStack.Push(str);
        }
    }
}