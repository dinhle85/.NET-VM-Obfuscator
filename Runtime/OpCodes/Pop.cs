namespace VMExample.Instructions
{
    class Pop : Base
    {
        public override void emu()
        {
            dynamic var = All.val.valueStack.Pop();
        }
    }
}