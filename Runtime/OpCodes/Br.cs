namespace VMExample.Instructions
{
    class Br : Base
    {
        public override void emu()
        {
            var a = All.binr.ReadInt32();
            All.binr.BaseStream.Position = a;
        }
    }
}