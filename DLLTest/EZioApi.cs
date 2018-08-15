//------------------------------------------------------------------------------------------------
// Create EZioApi.cs by Jeffrey 2014/07/14
//------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EzioDll
{
    public class EZioApi
    {
        [DllImport("Ezio32.dll")]
        public static extern bool openport(
            int Port);

        [DllImport("Ezio32.dll")]
        public static extern bool openport(
            [MarshalAs(UnmanagedType.LPStr)]string DeviceName);

        [DllImport("Ezio32.dll")]
        public static extern bool OpenDriver(
            [MarshalAs(UnmanagedType.LPStr)]
            string DeviceName);

        [DllImport("Ezio32.dll")]
        public static extern bool OpenNet(
            [MarshalAs(UnmanagedType.LPStr)]string IP,
            [MarshalAs(UnmanagedType.LPStr)]string Port);

        [DllImport("Ezio32.dll")]
        public static extern int setup(
            int LabelLength,
            int Darkness,
            int Speed,
            int LabelMode,
            int LabelGap,
            int BlackTop);

        [DllImport("Ezio32.dll")]
        public static extern int setbaudrate(
            int BaudRte);

        [DllImport("Ezio32.dll")]
        public static extern void closeport();

        [DllImport("Ezio32.dll")]
        public static extern void sendbuf(
            [MarshalAs(UnmanagedType.LPArray)]byte[] command, 
            int length);

        [DllImport("Ezio32.dll")]
        public static extern int sendcommand(
            [MarshalAs(UnmanagedType.LPStr)]string command);

        [DllImport("Ezio32.dll")]
        public static extern int intloadimage(
            [MarshalAs(UnmanagedType.LPStr)]string Filename,
            [MarshalAs(UnmanagedType.LPStr)]string ImageName,
            [MarshalAs(UnmanagedType.LPStr)]string ImageType);

        [DllImport("Ezio32.dll")]
        public static extern int extloadimage(
            [MarshalAs(UnmanagedType.LPStr)]string Filename,
            [MarshalAs(UnmanagedType.LPStr)]string ImageName,
            [MarshalAs(UnmanagedType.LPStr)]string ImageType);

        [DllImport("Ezio32.dll")]
        public static extern int ecTextOut(
            int PosX, 
            int PosY, 
            int FontHeight,
            [MarshalAs(UnmanagedType.LPStr)]string FontName,
            [MarshalAs(UnmanagedType.LPStr)]string Data);

        [DllImport("Ezio32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ecTextOutW(
            int PosX, 
            int PosY,
            int FontHeight,
            [MarshalAs(UnmanagedType.LPStr)]string FontName,
            [MarshalAs(UnmanagedType.LPArray)]byte[] UnicodeByteData,
            int TextLength);

        [DllImport("Ezio32.dll")]
        public static extern int ecTextOutR(
            int PosX,
            int PosY,
            int FontHeight,
            [MarshalAs(UnmanagedType.LPStr)]string FontName,
            [MarshalAs(UnmanagedType.LPStr)]string Data, 
            int TextWidth, 
            int Dark, 
            int Rotate);

        [DllImport("Ezio32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ecTextOutRW(
            int PosX,
            int PosY,
            int FontHeight,
            [MarshalAs(UnmanagedType.LPStr)]string FontName,
            [MarshalAs(UnmanagedType.LPArray)]byte[] UnicodeByteData,
            int TextWidth, 
            int Dark, 
            int Rotate,
            int TextLength);

        [DllImport("Ezio32.dll")]
        public static extern int ecTextOutFine(
            int PosX,
            int PosY,
            int FontHeight,
            [MarshalAs(UnmanagedType.LPStr)]string FontName,
            [MarshalAs(UnmanagedType.LPStr)]string Data,
            int TextWidth,
            int Dark,
            int Rotate,
            int Italic, 
            int Underline,
            int Strikeout,
            int Inverse);

        [DllImport("Ezio32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ecTextOutFineW(
            int PosX,
            int PosY,
            int FontHeight,
            [MarshalAs(UnmanagedType.LPStr)]string FontName,
            [MarshalAs(UnmanagedType.LPArray)]byte[] UnicodeByteData,
            int TextWidth,
            int Dark,
            int Rotate,
            int Italic,
            int Underline,
            int Strikeout,
            int Inverse,
            int TextLength);

        [DllImport("Ezio32.dll")]
        public static extern int ecTextDownLoad(int FontHeight, string FontName, string Data, int TextWidth, int Dark, int Rotate,
            [MarshalAs(UnmanagedType.LPStr)]string ObjectName);

        [DllImport("Ezio32.dll")]
        public static extern int putimage(
            int PosX, 
            int PosY,
            [MarshalAs(UnmanagedType.LPStr)]string Filename, 
            int Degree);

        [DllImport("Ezio32.dll")]
        public static extern int putimage_Halftone(
            int PosX,
            int PosY,
            [MarshalAs(UnmanagedType.LPStr)]string Filename,
            int Degree,
            int Halftone);

        [DllImport("Ezio32.dll")]
        public static extern int downloadimage(
            [MarshalAs(UnmanagedType.LPStr)]string Filename, 
            int Degree,
            [MarshalAs(UnmanagedType.LPStr)]string ObjectName);

        [DllImport("Ezio32.dll")]
        public static extern int FindFirstUSB(
            [MarshalAs(UnmanagedType.LPArray)]byte[] UsbID);

        [DllImport("Ezio32.dll")]
        public static extern int RcvBuf(
            byte[] ByteArray,
            int ByteArraySize);

        [DllImport("Ezio32.dll")]
        public static extern int FindNextUSB(
            [MarshalAs(UnmanagedType.LPArray)]byte[] UsbID);

        [DllImport("Ezio32.dll")]
        public static extern int OpenUSB(
            [MarshalAs(UnmanagedType.LPStr)]string UsbID);

        [DllImport("Ezio32.dll")]
        public static extern void GetDllVersion(
            [MarshalAs(UnmanagedType.LPArray)]byte[] Version);

        [DllImport("Ezio32.dll")]
        public static extern int Bar(
            [MarshalAs(UnmanagedType.LPStr)]string BarcodeType, 
	        int PosX, 
	        int PosY,
	        int Narrow,
	        int Wide,
	        int Height,
	        int Rotation,
	        int Readable,
	        [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_S(
            [MarshalAs(UnmanagedType.LPStr)]string BarcodeType,
            int PosX,
            int PosY,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_GS1DataBar(
            [MarshalAs(UnmanagedType.LPStr)]string BarcodeType,
            int PosX,
            int PosY,
            int Narrow,
            int Segment,
            int Height,
            int Rotation,
            int Readable,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_GS1DataBar_S(
            [MarshalAs(UnmanagedType.LPStr)]string BarcodeType,
            int PosX,
            int PosY,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_PDF417(
            int PosX,
            int PosY,
            int Width,
            int Height,
            int Row,
            int Column,
            int ErrorLevel,
            int Len,
            int Rotation,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_PDF417_S(
            int PosX,
            int PosY,
            int Len,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_MicroPDF417(
            int PosX,
            int PosY,
            int Width,
            int Height,
            int Mode,
            int Len,
            int Rotation,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_MicroPDF417_S(
            int PosX,
            int PosY,
            int Len,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_Maxicode(
            int PosX,
            int PosY,
            int SymbolNo,
            int SetNo,
            int Mode,
            [MarshalAs(UnmanagedType.LPStr)]string CountryCode,
            [MarshalAs(UnmanagedType.LPStr)]string PostalCode,
            [MarshalAs(UnmanagedType.LPStr)]string Class,
            int Rotation,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_Maxicode_S(
            int PosX,
            int PosY,
            [MarshalAs(UnmanagedType.LPStr)]string CountryCode,
            [MarshalAs(UnmanagedType.LPStr)]string PostalCode,
            [MarshalAs(UnmanagedType.LPStr)]string Class,
            int Rotation,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_DataMatrix(
            int PosX,
            int PosY,
            int Enlarge,
            [MarshalAs(UnmanagedType.LPStr)]string RotationR,
            int Len,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_DataMatrix_S(
            int PosX,
            int PosY,
            int Len,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_QRcode(
            int PosX,
            int PosY,
            int Mode,
            int Type,
            [MarshalAs(UnmanagedType.LPStr)]string ErrorLevel,
            int Mask,
            int Mul,
            int Len,
            int Rotation,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_QRcode(
            int PosX,
            int PosY,
            int Mode,
            int Type,
            [MarshalAs(UnmanagedType.LPStr)]string ErrorLevel,
            int Mask,
            int Mul,
            int Len,
            int Rotation,
            [MarshalAs(UnmanagedType.LPArray)]byte[] data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_QRcode_S(
            int PosX,
            int PosY,
            int Len,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_QRcode_S(
            int PosX,
            int PosY,
            int Len,
            [MarshalAs(UnmanagedType.LPArray)]byte[] data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_Aztec(
            int PosX,
            int PosY,
            int Rotation,
            int Mul,
            [MarshalAs(UnmanagedType.LPStr)]string ECICs,
            int Type,
            [MarshalAs(UnmanagedType.LPStr)]string MenuSymbol,
            int Len,
            [MarshalAs(UnmanagedType.LPStr)]string data);

        [DllImport("Ezio32.dll")]
        public static extern int Bar_Aztec_S(
            int PosX,
            int PosY,
            int Len,
            [MarshalAs(UnmanagedType.LPStr)]string data);


        [DllImport("Ezio32.dll")]
        public static extern int DrawHorLine(
            int PosX,
            int PosY,
            int Length,
            int Thick);

        [DllImport("Ezio32.dll")]
        public static extern int DrawVerLine(
            int PosX,
            int PosY,
            int Length,
            int Thick);

        [DllImport("Ezio32.dll")]
        public static extern int FillRec(
            int PosX,
            int PosY,
            int Rec_W,
            int Rec_H);

        [DllImport("Ezio32.dll")]
        public static extern int DrawRec(
            int PosX,
            int PosY,
            int Rec_W,
            int Rec_H,
            int lrw,
            int ubw);

        [DllImport("Ezio32.dll")]
        public static extern int DrawOblique(
            int PosX1,
            int PosY1,
            int Thick,
            int PosX2,
            int PosY2);

        [DllImport("Ezio32.dll")]
        public static extern int DrawEllipse(
            int PosX,
            int PosY,
            int Ellipse_W,
            int Ellipse_H,
            int PenWidth);

        [DllImport("Ezio32.dll")]
        public static extern int DrawRoundRec(
            int PosX,
            int PosY,
            int Rec_W,
            int Rec_H,
            int Arc_W,
            int Arc_H,
            int PenWidth);

        [DllImport("Ezio32.dll")]
        public static extern int DrawTriangle(
            int PosX1,
            int PosY1,
            int PosX2,
            int PosY2,
            int PosX3,
            int PosY3,
            int Thick);

        [DllImport("Ezio32.dll")]
        public static extern int DrawDiamond(
            int PosX,
            int PosY,
            int Diamand_W,
            int Diamand_H,
            int Thick);

        [DllImport("Ezio32.dll")]
        public static extern int InternalFont_TextOut(
            [MarshalAs(UnmanagedType.LPStr)]string FontType, 
            int PosX, 
            int PosY, 
            int Mul_X, 
            int Mul_Y, 
            int Gap, 
            [MarshalAs(UnmanagedType.LPStr)]string RotationInverse, 
            [MarshalAs(UnmanagedType.LPStr)]string Data);

        [DllImport("Ezio32.dll")]
        public static extern int InternalFont_TextOut_S(
            [MarshalAs(UnmanagedType.LPStr)]string FontType, 
            int PosX, 
            int PosY, 
            [MarshalAs(UnmanagedType.LPStr)]string Data);
        
        [DllImport("Ezio32.dll")]
        public static extern int DownloadFont_TextOut(
            [MarshalAs(UnmanagedType.LPStr)]string FontName, 
            int PosX, 
            int PosY, 
            int Mul_X, 
            int Mul_Y, 
            int Gap, 
            [MarshalAs(UnmanagedType.LPStr)]string RotationInverse, 
            [MarshalAs(UnmanagedType.LPStr)]string Data);
        
        [DllImport("Ezio32.dll")]
        public static extern int DownloadFont_TextOut_S(
            [MarshalAs(UnmanagedType.LPStr)]string FontName, 
            int PosX, 
            int PosY, 
            [MarshalAs(UnmanagedType.LPStr)]string Data);
        
        [DllImport("Ezio32.dll")]
        public static extern int TrueTypeFont_TextOut(
            [MarshalAs(UnmanagedType.LPStr)]string FontName, 
            int PosX, 
            int PosY, 
            int Font_W, 
            int Font_H, 
            int SpaceChar, 
            [MarshalAs(UnmanagedType.LPStr)]string RotationInverse, 
            [MarshalAs(UnmanagedType.LPStr)]string TTFTable, 
            int WidthMode, 
            [MarshalAs(UnmanagedType.LPStr)]string Data);
        
        [DllImport("Ezio32.dll")]
        public static extern int TrueTypeFont_TextOut_S(
            [MarshalAs(UnmanagedType.LPStr)]string FontName, 
            int PosX, 
            int PosY, 
            [MarshalAs(UnmanagedType.LPStr)]string Data);
    }
}
