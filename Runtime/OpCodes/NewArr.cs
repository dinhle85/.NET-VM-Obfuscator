namespace VMExample.Instructions
{
    class NewArr : Base
    {
        public override void emu()
        {
            var amunt = All.val.valueStack.Pop();
            All.val.valueStack.Push(new byte[(int)amunt]);
        }
    }
}