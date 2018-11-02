 
using System.Runtime.InteropServices;
 

namespace Probotix.Utils
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ByteArrayFloat
    {
        [FieldOffset(0)]
        public byte Byte0;
        [FieldOffset(1)]
        public byte Byte1;
        [FieldOffset(2)]
        public byte Byte2;
        [FieldOffset(3)]
        public byte Byte3;

        [FieldOffset(0)]
        public float FloatNumber;

    }
}
