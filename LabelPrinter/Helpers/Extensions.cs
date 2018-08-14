namespace LabelPrinter.Helpers
{
    public static class Extensions
    {
        public static BarcodeLib.TYPE ToBarcodeType(this string barcodeType)
        {
            switch (barcodeType)
            {
                case "Code39":
                    return BarcodeLib.TYPE.CODE39;
                case "Code128":
                    return BarcodeLib.TYPE.CODE128;
                case "EAN13":
                    return BarcodeLib.TYPE.EAN13;
                case "EAN8":
                    return BarcodeLib.TYPE.EAN8;
                case "2/5 Interleaved":
                    return BarcodeLib.TYPE.Interleaved2of5;
                default:
                    return BarcodeLib.TYPE.UNSPECIFIED; // It will be treated as Datamatrix
            }
        }
    }
}
