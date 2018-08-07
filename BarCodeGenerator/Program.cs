using System.Drawing;

namespace BarCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            Image img = b.Encode(BarcodeLib.TYPE.CODE128, "038000356216", Color.Black, Color.White, 290, 120);
        }
    }
}
