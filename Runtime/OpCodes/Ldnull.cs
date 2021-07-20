namespace VMExample.Instructions
{
    class Ldnull : Base
    {
        public override void emu() => All.val.valueStack.Push(null);
    }
}