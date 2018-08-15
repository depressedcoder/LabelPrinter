//------------------------------------------------------------------------------------------------
// Create GodexPrinter.cs by Jeffrey 2014/07/14
//------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing.Printing;

namespace EzioDll
{
    #region Enum Definition

    public enum HalftoneMode
    { 
        None = 0,
        ClusterDithering = 1,
        DispersedDithering = 2,
        DiffisionDithering = 3
    }

    public enum BarCodeType
    { 
        Code39_Extended,            // BA
        Code39_Extended_CheckDidit, // BA2
        Code39,                     // BA3
        Code39_CheckDidit,          // BA4
        EAN8,                       // BB
        EAN8_Add2,                  // BC
        EAN8_Add5,                  // BD
        EAN13,                      // BE
        EAN13_Add2,                 // BF
        EAN13_Add5,                 // BG
        UPCA,                       // BH
        UPCA_Add2,                  // BI
        UPCA_Add5,                  // BJ
        UPCE,                       // BK
        UPCE_Add2,                  // BL
        UPCE_Add5,                  // BM
        I2of5,                      // BN
        I2of5_CheckDigit,           // BN2
        Codabar,                    // BO
        Code93,                     // BP
        Code128_Auto,               // BQ
        Code128_Subset,             // BQ2
        UCC_128,                    // BR
        PostNET,                    // BS
        ITF14,                      // BT
        EAN128,                     // BU
        RPS128,                     // BV
        HIBC,                       // BX
        MSI_1MOD10,                 // BY
        MSI_2MOD10,                 // BY2
        MSI_1MOD1110,               // BY3
        MSI_NoDigitCheck,           // BY4
        I2of5_ShippingBearerBars,   // BZ
        UCC_EAN128_KMART,           // B1
        UCC_EAN128_RANDOM,          // B2
        Telepen,                    // B3
        FIM,                        // B4
        Plessey                     // B7
    }

    public enum PortType
    {
        LPT1 = 0,
        COM1 = 1,
        COM2 = 2,
        COM3 = 3,
        COM4 = 4,
        LPT2 = 5,
        USB = 6
    }

    public enum PaperMode
    { 
        GapLabel = 0,
        PlainPaperLabel = 1
    }

    public enum RotateMode
    { 
        Angle_0 = 0,
        Angle_90 = 90,
        Angle_180 = 180,
        Angle_270 = 270
    }

    public enum FontWeight
    {
        FW_100_THIN = 100,
        FW_200_EXTRALIGHT = 200,
        FW_300_LIGHT = 300,
        FW_400_NORMAL = 400,
        FW_500_MEDIUM = 500,
        FW_600_FW_SEMIBOLD = 600,
        FW_700_BOLD = 700,
        FW_800_EXTRABOLD = 800,
        FW_900_HEAVY = 900
    }

    public enum Italic_State
    {
        OFF = 0,
        ON = 1
    }

    public enum Underline_State
    {
        OFF = 0,
        ON = 1
    }

    public enum Strikeout_State
    {
        OFF = 0,
        ON = 1
    }

    public enum Inverse_State
    {
        OFF = 0,
        ON = 1
    }

    public enum Image_Type
    {
        BMP = 0,
        PCX = 1
    }

    #endregion Enum Definition

    public class clsPrinterCommand
    {
        static Hashtable BarcodeTypeHash = new Hashtable();

        static void GetBarcodeTypeHash()
        {
            if (BarcodeTypeHash.Count > 0)
                return;

            BarcodeTypeHash[BarCodeType.Code39_Extended] = "A";
            BarcodeTypeHash[BarCodeType.Code39_Extended_CheckDidit] = "A2";
            BarcodeTypeHash[BarCodeType.Code39] = "A3";
            BarcodeTypeHash[BarCodeType.Code39_CheckDidit] = "A4";
            BarcodeTypeHash[BarCodeType.EAN8] = "B";
            BarcodeTypeHash[BarCodeType.EAN8_Add2] = "C";
            BarcodeTypeHash[BarCodeType.EAN8_Add5] = "D";
            BarcodeTypeHash[BarCodeType.EAN13] = "E";
            BarcodeTypeHash[BarCodeType.EAN13_Add2] = "F";
            BarcodeTypeHash[BarCodeType.EAN13_Add5] = "G";
            BarcodeTypeHash[BarCodeType.UPCA] = "H";
            BarcodeTypeHash[BarCodeType.UPCA_Add2] = "I";
            BarcodeTypeHash[BarCodeType.UPCA_Add5] = "J";
            BarcodeTypeHash[BarCodeType.UPCE] = "K";
            BarcodeTypeHash[BarCodeType.UPCE_Add2] = "L";
            BarcodeTypeHash[BarCodeType.UPCE_Add5] = "M";
            BarcodeTypeHash[BarCodeType.I2of5] = "N";
            BarcodeTypeHash[BarCodeType.I2of5_CheckDigit] = "N2";
            BarcodeTypeHash[BarCodeType.Codabar] = "O";
            BarcodeTypeHash[BarCodeType.Code93] = "P";
            BarcodeTypeHash[BarCodeType.Code128_Auto] = "Q";
            BarcodeTypeHash[BarCodeType.Code128_Subset] = "Q2";
            BarcodeTypeHash[BarCodeType.UCC_128] = "R";
            BarcodeTypeHash[BarCodeType.PostNET] = "S";
            BarcodeTypeHash[BarCodeType.ITF14] = "T";
            BarcodeTypeHash[BarCodeType.EAN128] = "U";
            BarcodeTypeHash[BarCodeType.RPS128] = "V";
            BarcodeTypeHash[BarCodeType.HIBC] = "X";
            BarcodeTypeHash[BarCodeType.MSI_1MOD10] = "Y";
            BarcodeTypeHash[BarCodeType.MSI_2MOD10] = "Y2";
            BarcodeTypeHash[BarCodeType.MSI_1MOD1110] = "Y3";
            BarcodeTypeHash[BarCodeType.MSI_NoDigitCheck] = "Y4";
            BarcodeTypeHash[BarCodeType.I2of5_ShippingBearerBars] = "Z";
            BarcodeTypeHash[BarCodeType.UCC_EAN128_KMART] = "1";
            BarcodeTypeHash[BarCodeType.UCC_EAN128_RANDOM] = "2";
            BarcodeTypeHash[BarCodeType.Telepen] = "3";
            BarcodeTypeHash[BarCodeType.FIM] = "4";
            BarcodeTypeHash[BarCodeType.Plessey] = "7";
        }

        public void Start()
        {
            EZioApi.sendcommand("^L");
        }

        public void End()
        {
            EZioApi.sendcommand("E");
        }

        public int PrintText(int PosX, int PosY, int FontHeight, string FontName, string Data)
        {
            return EZioApi.ecTextOut(PosX, PosY, FontHeight, FontName, Data);
        }

        public int PrintText(int PosX, int PosY, int FontHeight, string FontName, string Data, int TextWidth, FontWeight Dark, RotateMode Rotate)
        {
            return EZioApi.ecTextOutR(PosX, PosY, FontHeight, FontName, Data, TextWidth, (int)Dark, (int)Rotate);
        }

        public int PrintText(int PosX, int PosY, int FontHeight, string FontName, string Data, int TextWidth, FontWeight Dark, RotateMode Rotate, Italic_State Italic, Underline_State Underline, Strikeout_State Strikeout, Inverse_State Inverse)
        {
            return EZioApi.ecTextOutFine(PosX, PosY, FontHeight, FontName, Data, TextWidth, (int)Dark, (int)Rotate, (int)Italic, (int)Underline, (int)Strikeout, (int)Inverse);
        }

        public int PrintText_Unicode(int PosX, int PosY, int FontHeight, string FontName, string Data)
        {
            byte[] ByteData = Encoding.Unicode.GetBytes(Data);
            return EZioApi.ecTextOutW(PosX, PosY, FontHeight, FontName, ByteData, Data.Length);
        }

        public int PrintText_Unicode(int PosX, int PosY, int FontHeight, string FontName, string Data, int TextWidth, FontWeight Dark, RotateMode Rotate)
        {
            byte[] ByteData = Encoding.Unicode.GetBytes(Data);
            return EZioApi.ecTextOutRW(PosX, PosY, FontHeight, FontName, ByteData, TextWidth, (int)Dark, (int)Rotate, Data.Length);
        }

        public int PrintText_Unicode(int PosX, int PosY, int FontHeight, string FontName, string Data, int TextWidth, FontWeight Dark, RotateMode Rotate, Italic_State Italic, Underline_State Underline, Strikeout_State Strikeout, Inverse_State Inverse)
        {
            byte[] ByteData = Encoding.Unicode.GetBytes(Data);
            return EZioApi.ecTextOutFineW(PosX, PosY, FontHeight, FontName, ByteData, TextWidth, (int)Dark, (int)Rotate, (int)Italic, (int)Underline, (int)Strikeout, (int)Inverse, Data.Length);
        }

        public int PrintText_EZPL_Internal(
            string FontType,
            int PosX,
            int PosY,
            int Mul_X,
            int Mul_Y,
            int Gap,
            string RotationInverse,
            string Data)
        {
            return EZioApi.InternalFont_TextOut(FontType, PosX, PosY, Mul_X, Mul_Y, Gap, RotationInverse, Data);
        }

        public int PrintText_EZPL_Internal(
            string FontType,
            int PosX,
            int PosY,
            string Data)
        {
            return EZioApi.InternalFont_TextOut_S(FontType, PosX, PosY, Data);
        }

        public int PrintText_EZPL_Bitmapped(
            string FontName,
            int PosX,
            int PosY,
            int Mul_X,
            int Mul_Y,
            int Gap,
            string RotationInverse,
            string Data)
        {
            return EZioApi.DownloadFont_TextOut(FontName, PosX, PosY, Mul_X, Mul_Y, Gap, RotationInverse, Data);
        }

        public int PrintText_EZPL_Bitmapped(
            string FontName,
            int PosX,
            int PosY,
            string Data)
        {
            return EZioApi.DownloadFont_TextOut_S(FontName, PosX, PosY, Data);
        }

        public int PrintText_EZPL_TTF(
            string FontName,
            int PosX,
            int PosY,
            int Font_W,
            int Font_H,
            int SpaceChar,
            string RotationInverse,
            string TTFTable,
            int WidthMode,
            string Data)
        {
            return EZioApi.TrueTypeFont_TextOut(FontName, PosX, PosY, Font_W, Font_H, SpaceChar, RotationInverse, TTFTable, WidthMode, Data);
        }

        public int PrintText_EZPL_TTF(
            string FontName,
            int PosX,
            int PosY,
            string Data)
        {
            return EZioApi.TrueTypeFont_TextOut_S(FontName, PosX, PosY, Data);
        }

        public void UploadImage_Int(string Filename, string DisplayName, Image_Type mType)
        {
            string strType;
            if (mType == Image_Type.BMP)
                strType = "bmp";
            else
                strType = "pcx";

            // Delete Image
            EZioApi.sendcommand("~MDELG," + DisplayName);
            
            // Upload Image
            EZioApi.intloadimage(Filename, DisplayName, strType);
        }

        public void UploadImage_Ext(string Filename, string DisplayName, Image_Type mType)
        {
            string strType;
            if (mType == Image_Type.BMP)
                strType = "bmp";
            else
                strType = "pcx";

            // Delete Image
            EZioApi.sendcommand("~MDELG," + DisplayName);

            // Upload Image
            EZioApi.extloadimage(Filename, DisplayName, strType);
        }

        public void UploadImage_FullColor(string Filename, string DisplayName, int nRotate)
        {
            // Delete Image
            EZioApi.sendcommand("~MDELG," + DisplayName);

            // Upload Image
            EZioApi.downloadimage(Filename, nRotate, DisplayName);
        }

        public void UploadText(int FontHeight, string FontName, string Data, int TextWidth, FontWeight Dark, RotateMode Rotate, string Name)
        {
            // Delete Image
            EZioApi.sendcommand("~MDELG," + Name);

            // Upload Text Image
            EZioApi.ecTextDownLoad(FontHeight, FontName, Data, TextWidth, (int)Dark, (int)Rotate, Name);
        }

        public void PrintImageByName(string DisplayName, int PosX, int PosY)
        {
            // Print Image
            EZioApi.sendcommand("Y" + PosX.ToString() + "," + PosY.ToString() + "," + DisplayName);
        }

        public int PrintImage(int PosX, int PosY, string FileName, int mRotation)
        {
            return EZioApi.putimage(PosX, PosY, FileName, mRotation);
        }

        public int PrintHalftoneImage(int PosX, int PosY, string FileName, int mRotation, HalftoneMode nHalftoneMode)
        {
            return EZioApi.putimage_Halftone(PosX, PosY, FileName, mRotation, (int)nHalftoneMode);
        }

        public void AutoSensing()
        {
            EZioApi.sendcommand("~S,SENSOR,0");
        }

        public int Send(string Cmd)
        {
            return EZioApi.sendcommand(Cmd);
        }

        public void SendByte(byte [] ByteArray)
        {
            EZioApi.sendbuf(ByteArray, ByteArray.Length);
        }

        public void SendByte(byte[] ByteArray, int nDataSize)
        {
            EZioApi.sendbuf(ByteArray, nDataSize);
        }

        public string Read()
        {
            byte[] byteArray = new byte[2048];
            string RetData = "";
            int RetryNo = 3;
            int CurNo = 0;

            while (true)
            {
                if (EZioApi.RcvBuf(byteArray, byteArray.GetUpperBound(0)) == 0)
                {
                    CurNo++;
                }
                else
                {
                    CurNo = 0;
                    string[] temp = Encoding.Default.GetString(byteArray).Split('\0');
                    RetData += temp[0];
                }

                Array.Clear(byteArray, 0, byteArray.GetUpperBound(0));

                if (CurNo >= RetryNo)
                    break;
            }

            return RetData;
        }

        public int PrintBarCode(BarCodeType mBarCodeType, int PosX, int PosY, string Data)
        {
            GetBarcodeTypeHash();
            return EZioApi.Bar_S(BarcodeTypeHash[mBarCodeType].ToString(), PosX, PosY, Data);
        }

        public int PrintBarCode(BarCodeType mBarCodeType, int PosX, int PosY, int Narrow, int Wide, int Height, int Rotation, int Raedable, string Data)
        {
            GetBarcodeTypeHash();
            return EZioApi.Bar(BarcodeTypeHash[mBarCodeType].ToString(), PosX, PosY, Narrow, Wide, Height, Rotation, Raedable, Data);
        }

        public int PrintQRCode(int PosX, int PosY, string Data, Encoding mEncoding)
        {
            byte[] ByteData = mEncoding.GetBytes(Data);
            return EZioApi.Bar_QRcode_S(PosX, PosY, mEncoding.GetByteCount(Data), ByteData);
        }

        public int PrintQRCode(int PosX, int PosY, int Mode, int Type, string ErrorLavel, int Mask, int Mul, int Rotation, string Data, Encoding mEncoding)
        {
            byte[] ByteData = mEncoding.GetBytes(Data);
            return EZioApi.Bar_QRcode(PosX, PosY, Mode, Type, ErrorLavel, Mask, Mul, mEncoding.GetByteCount(Data), Rotation, ByteData);
        }

        public int PrintQRCode(int PosX, int PosY, string Data)
        {
            return PrintQRCode(PosX, PosY, Data, Encoding.Default);
        }

        public int PrintQRCode(int PosX, int PosY, int Mode, int Type, string ErrorLavel, int Mask, int Mul, int Rotation, string Data)
        {
            return PrintQRCode(PosX, PosY, Mode, Type, ErrorLavel, Mask, Mul, Rotation, Data, Encoding.Default);
        }

        public int PrintQRCode_UTF8(int PosX, int PosY, string Data)
        {
            return PrintQRCode(PosX, PosY, Data, Encoding.UTF8);
        }

        public int PrintQRCode_UTF8(int PosX, int PosY, int Mode, int Type, string ErrorLavel, int Mask, int Mul, int Rotation, string Data)
        {
            return PrintQRCode(PosX, PosY, Mode, Type, ErrorLavel, Mask, Mul, Rotation, Data, Encoding.UTF8);
        }

        public int PrintPDF417(int PosX, int PosY, string Data)
        {
            return EZioApi.Bar_PDF417_S(PosX, PosY, Encoding.Default.GetByteCount(Data), Data);
        }

        public int PrintPDF417(int PosX, int PosY, int Width, int Height, int Row, int Col, int ErrorLevel, int Rotation, string Data)
        {
            return EZioApi.Bar_PDF417(PosX, PosY, Width, Height, Row, Col, ErrorLevel, Encoding.Default.GetByteCount(Data), Rotation, Data);
        }

        public int PrintAztec(int PosX, int PosY, string Data)
        {
            return EZioApi.Bar_Aztec_S(PosX, PosY, Encoding.Default.GetByteCount(Data), Data);
        }

        public int PrintAztec(int PosX, int PosY, int Rotation, int Mul, string ECICs, string MenuSymbol, int Type, string Data)
        {
            return EZioApi.Bar_Aztec(PosX, PosY, Rotation, Mul, ECICs, Type, MenuSymbol, Encoding.Default.GetByteCount(Data), Data);
        }

        public int PrintMaxiCode(int PosX, int PosY, string CountryCode, string PostalCode, string nClass, string Data)
        {
            return EZioApi.Bar_Maxicode_S(PosX, PosY, CountryCode, PostalCode, nClass, 0, Data);
        }

        public int PrintMaxiCode(int PosX, int PosY, int SymbolNo, int SetNo, int Mode, string CountryCode, string PostalCode, string nClass, int Rotate, string Data)
        {
            return EZioApi.Bar_Maxicode(PosX, PosY, SymbolNo, SetNo, Mode, CountryCode, PostalCode, nClass, Rotate, Data);
        }

        public int PrintDataMatrix(int PosX, int PosY, string Data)
        {
            return EZioApi.Bar_DataMatrix_S(PosX, PosY, Encoding.Default.GetByteCount(Data), Data);
        }

        public int PrintDataMatrix(int PosX, int PosY, int Enlarge, string RotationR, string Data)
        {
            return EZioApi.Bar_DataMatrix(PosX, PosY, Enlarge, RotationR, Encoding.Default.GetByteCount(Data), Data);
        }

        public int DrawHorLine(int PosX, int PosY, int Length, int Thick)
        {
            return EZioApi.DrawHorLine(PosX, PosY, Length, Thick);
        }

        public int DrawVerLine(int PosX, int PosY, int Length, int Thick)
        {
            return EZioApi.DrawVerLine(PosX, PosY, Length, Thick);
        }

        public int FillRec(int PosX, int PosY, int Rec_W, int Rec_H)
        {
            return EZioApi.FillRec(PosX, PosY, Rec_W, Rec_H);
        }

        public int DrawRec(int PosX, int PosY, int Rec_W, int Rec_H, int lrw, int ubw)
        {
            return EZioApi.DrawRec(PosX, PosY, Rec_W, Rec_H, lrw, ubw);
        }

        public int DrawOblique(int PosX1, int PosY1, int Thick, int PosX2, int PosY2)
        {
            return EZioApi.DrawOblique(PosX1, PosY1, Thick, PosX2, PosY2);
        }

        public int DrawEllipse(int PosX, int PosY, int Ellipse_W, int Ellipse_H, int PenWidth)
        {
            return EZioApi.DrawEllipse(PosX, PosY, Ellipse_W, Ellipse_H, PenWidth);
        }

        public int DrawRoundRec(int PosX, int PosY, int Rec_W, int Rec_H, int Arc_W, int Arc_H, int PenWidth)
        {
            return EZioApi.DrawRoundRec(PosX, PosY, Rec_W, Rec_H, Arc_W, Arc_H, PenWidth);
        }

        public int DrawTriangle(int PosX1, int PosY1, int PosX2, int PosY2, int PosX3, int PosY3, int PenWidth)
        {
            return EZioApi.DrawTriangle(PosX1, PosY1, PosX2, PosY2, PosX3, PosY3, PenWidth);
        }

        public int DrawDiamond(int PosX, int PosY, int Rec_W, int Rec_H, int PenWidth)
        {
            return EZioApi.DrawDiamond(PosX, PosY, Rec_W, Rec_H, PenWidth);
        }
    }

    public class clsPrinterConfig
    {
        public void Setup(int LabelLength,
            int Darkness,
            int Speed,
            int LabelMode,
            int LabelGap,
            int BlackTop)
        {
            EZioApi.setup(LabelLength, Darkness, Speed, LabelMode, LabelGap, BlackTop);
        }

        public void LabelMode(PaperMode nMode, int nLabelHeight, int nGapFeed)
        {
            if (nMode == PaperMode.GapLabel)
                EZioApi.sendcommand("^Q" + nLabelHeight.ToString() + "," + nGapFeed.ToString());
            else
                EZioApi.sendcommand("^Q" + nLabelHeight.ToString() + ",0," + nGapFeed.ToString());
        }

        public void LabelWidth(int nWidth)
        {
            EZioApi.sendcommand("^W" + nWidth.ToString());
        }

        public void Dark(int nDark)
        {
            EZioApi.sendcommand("^H" + nDark.ToString());
        }

        public void Speed(int nSpeed)
        {
            EZioApi.sendcommand("^S" + nSpeed.ToString());
        }

        public void PageNo(int nPageNo)
        {
            EZioApi.sendcommand("^P" + nPageNo.ToString());
        }

        public void CopyNo(int nCopyNo)
        {
            EZioApi.sendcommand("^C" + nCopyNo.ToString());
        }
    }

    public class GodexPrinter
    {
        public clsPrinterCommand Command = new clsPrinterCommand();
        public clsPrinterConfig Config = new clsPrinterConfig();

        public static string[] GetDriverPrinter(string FilterName = "GoDEX")
        {
            string[] PrinterList = null;
            ArrayList DriverPrinterList = new ArrayList();
            
            foreach (String PrinterName in PrinterSettings.InstalledPrinters)
                if (PrinterName.ToUpper().Contains(FilterName.ToUpper()))
                    DriverPrinterList.Add(PrinterName);

            if (DriverPrinterList.Count > 0)
            {
                PrinterList = new string[DriverPrinterList.Count];
                for (int i = 0; i < PrinterList.Length; i++)
                    PrinterList[i] = DriverPrinterList[i].ToString();
            }

            return PrinterList;
        }

        public void Open(PortType mPortType)
        {
            EZioApi.openport(((int)mPortType).ToString());
        }

        public void Open(string PortName)
        {
            if (PortName.Contains("COM") == true)
                EZioApi.OpenUSB(PortName);
            else
                EZioApi.OpenDriver(PortName);
        }

        public void Open(string strIP, int nPort)
        {
            EZioApi.OpenNet(strIP, nPort.ToString());
        }

        public int SetBaudrate(int nBaud)
        {
            return EZioApi.setbaudrate(nBaud);
        }

        public void Close()
        {
            EZioApi.closeport();
        }

        public string GetDllVersion()
        {
            byte[] version = new byte[100];
            Array.Clear(version, 0, version.Length);
            EZioApi.GetDllVersion(version);
            return System.Text.Encoding.ASCII.GetString(version);
        }
    }


}
