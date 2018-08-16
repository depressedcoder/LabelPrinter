//------------------------------------------------------------------------------------------------
// Create FrmMain.cs by Jeffrey 2014/07/14
//------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO.Ports;
using System.Collections;
using EzioDll;

namespace DLLTest
{
    public partial class FrmMain : Form
    {
        GodexPrinter Printer = new GodexPrinter();

        //------------------------------------------------------------------------
        // Codepage Definition
        // http://msdn.microsoft.com/en-us/library/system.text.encoding(v=vs.110).aspx
        //------------------------------------------------------------------------

        int[] CodePage_Code = { 0,
            864, 708, 720, 28596, 10004, 1256, 775, 28594, 1257, 852,                               // 01
            28592, 10029, 1250, 51936, 54936, 936, 20936, 52936, 50227, 10008,                      // 02
            950, 20000, 20002, 10002, 10082, 866, 28595, 20866, 21866, 10007,                       // 03
            1251, 28603, 29001, 863, 20106, 737, 28597, 10006, 1253, 869,                           // 04
            862, 38598, 28598, 10005, 1255, 20420, 20880, 21025, 20277, 1142,                       // 05
            20278, 1143, 20297, 1147, 20273, 1141, 875, 20423, 20424, 20871,                        // 06
            1149, 500, 1148, 20280, 1144, 20290, 20833, 870, 20284, 1145,                           // 07
            20838, 1026, 20905, 20285, 1146, 37, 1140, 1047, 20924, 20003,                          // 08
            861, 10079, 57006, 57003, 57002, 57010, 57008, 57009, 57007, 57011,                     // 09
            57004, 57005, 20269, 51932, 20932, 50220, 50222, 50221, 10001, 932,                     // 10
            949, 51949, 50225, 1361, 10003, 20949, 28593, 28605, 865, 20108,                        // 11
            855, 858, 437, 860, 10010, 20107, 20261, 20001, 20004, 10021,                           // 12
            874, 857, 28599, 10081, 1254, 10017, 1200, 1201, 12001, 12000,                          // 13
            65000, 65001, 20127, 1258, 20005, 850, 20105, 28591, 10000, 1252};                      // 14

        string[] CodePage_Name = { "System Default", 
            "IBM864", "ASMO-708", "DOS-720", "iso-8859-6", "x-mac-arabic", "windows-1256", "ibm775", "iso-8859-4", "windows-1257", "ibm852",                    // 01
            "iso-8859-2", "x-mac-ce", "windows-1250", "EUC-CN", "GB18030", "gb2312", "x-cp20936", "hz-gb-2312", "x-cp50227", "x-mac-chinesesimp",               // 02
            "big5", "x-Chinese-CNS", "x-Chinese-Eten", "x-mac-chinesetrad", "x-mac-croatian", "cp866", "iso-8859-5", "koi8-r", "koi8-u", "x-mac-cyrillic",      // 03
            "windows-1251", "iso-8859-13", "x-Europa", "IBM863", "x-IA5-German", "ibm737", "iso-8859-7", "x-mac-greek", "windows-1253", "ibm869",               // 04
            "DOS-862", "iso-8859-8-i", "iso-8859-8", "x-mac-hebrew", "windows-1255", "IBM420", "IBM880", "cp1025", "IBM277", "IBM01142",                        // 05
            "IBM278", "IBM01143", "IBM297", "IBM01147", "IBM273", "IBM01141", "cp875", "IBM423", "IBM424", "IBM871",                                            // 06
            "IBM01149", "IBM500", "IBM01148", "IBM280", "IBM01144", "IBM290", "x-EBCDIC-KoreanExtended", "IBM870", "IBM284", "IBM01145",                        // 07
            "IBM-Thai", "IBM1026", "IBM905", "IBM285", "IBM01146", "IBM037", "IBM01140", "IBM01047", "IBM00924", "x-cp20003",                                   // 08
            "ibm861", "x-mac-icelandic", "x-iscii-as", "x-iscii-be", "x-iscii-de", "x-iscii-gu", "x-iscii-ka", "x-iscii-ma", "x-iscii-or", "x-iscii-pa",        // 09
            "x-iscii-ta", "x-iscii-te", "x-cp20269", "euc-jp", "EUC-JP", "iso-2022-jp", "iso-2022-jp", "csISO2022JP", "x-mac-japanese", "shift_jis",            // 10
            "ks_c_5601-1987", "euc-kr", "iso-2022-kr", "Johab", "x-mac-korean", "x-cp20949", "iso-8859-3", "iso-8859-15", "IBM865", "x-IA5-Norwegian",          // 11
            "IBM855", "IBM00858", "IBM437", "IBM860", "x-mac-romanian", "x-IA5-Swedish", "x-cp20261", "x-cp20001", "x-cp20004", "x-mac-thai",                   // 12
            "windows-874", "ibm857", "iso-8859-9", "x-mac-turkish", "windows-1254", "x-mac-ukrainian", "utf-16", "unicodeFFFE", "utf-32BE", "utf-32",           // 13
            "utf-7", "utf-8", "us-ascii", "windows-1258", "x-cp20005", "ibm850", "x-IA5", "iso-8859-1", "macintosh", "Windows-1252" };                          // 14

        string[] CodePage_DispName = { "System Default", 
            "Arabic (864)", "Arabic (ASMO 708)", "Arabic (DOS)", "Arabic (ISO)", "Arabic (Mac)", 
            "Arabic (Windows)", "Baltic (DOS)", "Baltic (ISO)", "Baltic (Windows)", "Central European (DOS)", 
            "Central European (ISO)", "Central European (Mac)", "Central European (Windows)", "Chinese Simplified (EUC)", "Chinese Simplified (GB18030)", 
            "Chinese Simplified (GB2312)", "Chinese Simplified (GB2312-80)", "Chinese Simplified (HZ)", "Chinese Simplified (ISO-2022)", "Chinese Simplified (Mac)", 
            "Chinese Traditional (Big5)", "Chinese Traditional (CNS)", "Chinese Traditional (Eten)", "Chinese Traditional (Mac)", "Croatian (Mac)", 
            "Cyrillic (DOS)", "Cyrillic (ISO)", "Cyrillic (KOI8-R)", "Cyrillic (KOI8-U)", "Cyrillic (Mac)", 
            "Cyrillic (Windows)", "Estonian (ISO)", "Europa", "French Canadian (DOS)", "German (IA5)", 
            "Greek (DOS)", "Greek (ISO)", "Greek (Mac)", "Greek (Windows)", "Greek, Modern (DOS)", 
            "Hebrew (DOS)", "Hebrew (ISO-Logical)", "Hebrew (ISO-Visual)", "Hebrew (Mac)", "Hebrew (Windows)", 
            "IBM EBCDIC (Arabic)", "IBM EBCDIC (Cyrillic Russian)", "IBM EBCDIC (Cyrillic Serbian-Bulgarian)", "IBM EBCDIC (Denmark-Norway)", "IBM EBCDIC (Denmark-Norway-Euro)", 
            "IBM EBCDIC (Finland-Sweden)", "IBM EBCDIC (Finland-Sweden-Euro)", "IBM EBCDIC (France)", "IBM EBCDIC (France-Euro)", "IBM EBCDIC (Germany)", 
            "IBM EBCDIC (Germany-Euro)", "IBM EBCDIC (Greek Modern)", "IBM EBCDIC (Greek)", "IBM EBCDIC (Hebrew)", "IBM EBCDIC (Icelandic)", 
            "IBM EBCDIC (Icelandic-Euro)", "IBM EBCDIC (International)", "IBM EBCDIC (International-Euro)", "IBM EBCDIC (Italy)", "IBM EBCDIC (Italy-Euro)", 
            "IBM EBCDIC (Japanese katakana)", "IBM EBCDIC (Korean Extended)", "IBM EBCDIC (Multilingual Latin-2)", "IBM EBCDIC (Spain)", "IBM EBCDIC (Spain-Euro)", 
            "IBM EBCDIC (Thai)", "IBM EBCDIC (Turkish Latin-5)", "IBM EBCDIC (Turkish)", "IBM EBCDIC (UK)", "IBM EBCDIC (UK-Euro)", 
            "IBM EBCDIC (US-Canada)", "IBM EBCDIC (US-Canada-Euro)", "IBM Latin-1", "IBM Latin-1", "IBM5550 Taiwan", 
            "Icelandic (DOS)", "Icelandic (Mac)", "ISCII Assamese", "ISCII Bengali", "ISCII Devanagari", 
            "ISCII Gujarati", "ISCII Kannada", "ISCII Malayalam", "ISCII Oriya", "ISCII Punjabi", 
            "ISCII Tamil", "ISCII Telugu", "ISO-6937", "Japanese (EUC)", "Japanese (JIS 0208-1990 and 0212-1990)", 
            "Japanese (JIS)", "Japanese (JIS-Allow 1 byte Kana - SO/SI)", "Japanese (JIS-Allow 1 byte Kana)", "Japanese (Mac)", "Japanese (Shift-JIS)", 
            "Korean", "Korean (EUC)", "Korean (ISO)", "Korean (Johab)", "Korean (Mac)", "Korean Wansung", 
            "Latin 3 (ISO)", "Latin 9 (ISO)", "Nordic (DOS)", "Norwegian (IA5)", 
            "OEM Cyrillic", "OEM Multilingual Latin I", "OEM United States", "Portuguese (DOS)", "Romanian (Mac)", 
            "Swedish (IA5)", "T.61", "TCA Taiwan", "TeleText Taiwan", "Thai (Mac)", 
            "Thai (Windows)", "Turkish (DOS)", "Turkish (ISO)", "Turkish (Mac)", "Turkish (Windows)", 
            "Ukrainian (Mac)", "Unicode", "Unicode (Big endian)", "Unicode (UTF-32 Big endian)", "Unicode (UTF-32)", 
            "Unicode (UTF-7)", "Unicode (UTF-8)", "US-ASCII", "Vietnamese (Windows)", "Wang Taiwan", 
            "Western European (DOS)", "Western European (IA5)", "Western European (ISO)", "Western European (Mac)", "Western European (Windows)" };

        //------------------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------------------
        public FrmMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //------------------------------------------------------------------------
        // Form Load
        //------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            Cbo_PosTTF.SelectedIndex = 0;

            // Find Com Port
            string[] ComPrinter = SerialPort.GetPortNames();
            if (ComPrinter != null)
            {
                Cbo_COM.Items.Clear();
                for (int i = 0; i < ComPrinter.Length; i++)
                    Cbo_COM.Items.Add(ComPrinter[i]);
                if (Cbo_COM.Items.Count > 0)
                    Cbo_COM.SelectedIndex = 0;
            }

            // Find GoDEX Driver Printer
            string[] DriverPrinter = GodexPrinter.GetDriverPrinter();
            if (DriverPrinter != null)
            {
                Cbo_Driver.Items.Clear();
                for (int i = 0; i < DriverPrinter.Length; i++)
                    Cbo_Driver.Items.Add(DriverPrinter[i]);
                if (Cbo_Driver.Items.Count > 0)
                    Cbo_Driver.SelectedIndex = 0;
            }

            // LPT Port
            Cbo_LPT.SelectedIndex = 0;

            // Continus Paper
            Cbo_PaperType.SelectedIndex = 1;

            // Set QrCode Encoding
            Cbo_QR_Encoding.Items.Clear();
            CodePage_Code[0] = Encoding.Default.CodePage;
            for (int i = 0; i < CodePage_DispName.Length; i++)
                Cbo_QR_Encoding.Items.Add(CodePage_DispName[i] + " - " + CodePage_Code[i].ToString());
            
            //// UTF8
            //Cbo_QR_Encoding.SelectedIndex = 132;

            // Default Encoding
            Cbo_QR_Encoding.SelectedIndex = 0;
        }

        //------------------------------------------------------------------------
        // Paper Type Change Event
        //------------------------------------------------------------------------
        private void Cbo_PaperType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cbo_PaperType.SelectedIndex == 0)
                Lbl_GapFeed.Text = "Gap Length (mm)";
            else
                Lbl_GapFeed.Text = "Feed Length (mm)";
        }

        //------------------------------------------------------------------------
        // Command Change
        //------------------------------------------------------------------------
        private void Cbo_Cmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Txt_Cmd.Text = Cbo_Cmd.SelectedItem.ToString();
        }

        //------------------------------------------------------------------------
        // Connect Printer
        //------------------------------------------------------------------------
        private void ConnectPrinter()
        {
            if (RBtn_USB.Checked == true)
            {
                Printer.Open(PortType.USB);
            }
            else if (RBtn_COM.Checked == true)
            {
                if (Cbo_COM.SelectedItem != null)
                {
                    Printer.Open(Cbo_COM.SelectedItem.ToString());
                    Printer.SetBaudrate(int.Parse(Txt_Baud.Text));
                }
            }
            else if (RBtn_LPT.Checked == true)
            {
                if (Cbo_LPT.SelectedIndex == 0)
                    Printer.Open(PortType.LPT1);
                else
                    Printer.Open(PortType.LPT2);
            }
            else if (RBtn_Driver.Checked == true)
            {
                Printer.Open(Cbo_Driver.SelectedItem.ToString());
            }
            else if (RBtn_NET.Checked == true)
            {
                Printer.Open(Txt_IP.Text, int.Parse(Txt_NetPort.Text));
            }
        }

        //------------------------------------------------------------------------
        // Disconnect Printer
        //------------------------------------------------------------------------
        private void DisconnectPrinter()
        {
            Printer.Close();
        }

        //------------------------------------------------------------------------
        // Label Setup
        //------------------------------------------------------------------------
        private void LabelSetup()
        {
            Printer.Config.LabelMode((PaperMode)Cbo_PaperType.SelectedIndex, (int)Num_Label_H.Value, (int)Num_GapFeed.Value);
            Printer.Config.LabelWidth((int)Num_Label_W.Value);
            Printer.Config.Dark((int)Num_Dark.Value);
            Printer.Config.Speed((int)Num_Speed.Value);
            Printer.Config.PageNo((int)Num_Page.Value);
            Printer.Config.CopyNo((int)Num_Copy.Value);
        }

        //------------------------------------------------------------------------
        // Click [Print Text] Button
        //------------------------------------------------------------------------
        private void Btn_PrintText_Click(object sender, EventArgs e)
        {
            int PosX = 10;
            int PosY = 10;
            int FontHeight = 34;

            ConnectPrinter();
            LabelSetup();

            // Print Text
            Printer.Command.Start();
            Printer.Command.PrintText_Unicode(PosX, PosY += 40, FontHeight, "Arial", "這是中文測試");
            Printer.Command.PrintText_Unicode(PosX, PosY += 40, FontHeight, "MS Gothic", "これは日本のテストです", 0, FontWeight.FW_400_NORMAL, RotateMode.Angle_180);
            Printer.Command.PrintText_Unicode(PosX, PosY += 40, FontHeight, "GulimChe", "이것은 한국의 테스트입니다", 0, FontWeight.FW_900_HEAVY, RotateMode.Angle_0);
            Printer.Command.PrintText(PosX, PosY += 40, FontHeight, "Arial", "GoDEX EZio DLL Test");
            Printer.Command.PrintText(PosX, PosY += 40, FontHeight, "Arial", "GoDEX EZio DLL Test", 0, FontWeight.FW_900_HEAVY, RotateMode.Angle_180);
            Printer.Command.PrintText(PosX, PosY += 40, FontHeight, "Arial", "GoDEX EZio DLL Test", 0, FontWeight.FW_700_BOLD, RotateMode.Angle_0, Italic_State.OFF, Underline_State.OFF, Strikeout_State.OFF, Inverse_State.ON);
            Printer.Command.End();
            
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Click [Print Image] Button
        //------------------------------------------------------------------------
        private void Btn_PrintImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog mOpenFileDialog = new OpenFileDialog();
            mOpenFileDialog.Filter = "*.bmp|*.bmp|*.gif|*.gif";
            if (mOpenFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            ConnectPrinter();
            LabelSetup();
            Printer.Command.Start();
            Printer.Command.PrintImage(10, 10, mOpenFileDialog.FileName, 0);
            Printer.Command.End();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Click [Auto Sensing] Button
        //------------------------------------------------------------------------
        private void Btn_AutoSensing_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();
            Printer.Command.AutoSensing();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Click [Get DLL Version] Button
        //------------------------------------------------------------------------
        private void Btn_GetVersion_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Printer.GetDllVersion());
        }

        //------------------------------------------------------------------------
        // Click [Send Byte Array] Button
        //------------------------------------------------------------------------
        private void Btn_SendByte_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            byte[] byteArray = System.Text.Encoding.Default.GetBytes("~V\r\n");
            Printer.Command.SendByte(byteArray);
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Click [Upload Text Image] Button
        //------------------------------------------------------------------------
        private void Btn_UploadTextImage_Click(object sender, EventArgs e)
        {
            string TextObjectName = "MyText";
            string mData = "EZio DLL - Download Text";

            ConnectPrinter();
            LabelSetup();

            // Upload Text Image
            Printer.Command.UploadText(34, "Arial", mData, 20, FontWeight.FW_400_NORMAL, RotateMode.Angle_0, TextObjectName);

            // Print Text Image
            Printer.Command.Start();
            Printer.Command.PrintImageByName(TextObjectName, 10, 10);
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Click [Execute] Button
        //------------------------------------------------------------------------
        private void Btn_Execute_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            Printer.Command.Send(Txt_Cmd.Text);
            Txt_Reply.Text  = Printer.Command.Read();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Upload Image To Internal Memory] Button
        //------------------------------------------------------------------------
        private void Btn_Upload_Int_Click(object sender, EventArgs e)
        {
            string FilePath = "Test.bmp";
            string DisplayName = "AAA";

            ConnectPrinter();

            // Upload Image
            Printer.Command.UploadImage_Int(FilePath, DisplayName, Image_Type.BMP);
            
            // Print Image
            Printer.Command.Start();
            Printer.Command.PrintImageByName(DisplayName, 10, 10);
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Upload Image To External Memory] Button
        //------------------------------------------------------------------------
        private void Btn_Upload_Ext_Click(object sender, EventArgs e)
        {
            string FilePath = "Test.bmp";
            string DisplayName = "CCC";

            ConnectPrinter();

            // Upload Image
            Printer.Command.UploadImage_Ext(FilePath, DisplayName, Image_Type.BMP);

            // Print Image
            Printer.Command.Start();
            Printer.Command.PrintImageByName(DisplayName, 10, 10);
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Upload Image And Do Halftoning] Button
        //------------------------------------------------------------------------
        private void Btn_HalfToneImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog mOpenFileDialog = new OpenFileDialog();
            mOpenFileDialog.Filter = "*.bmp|*.bmp|*.jpg|*.jpg";
            if (mOpenFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            string DisplayName = "BBB";

            ConnectPrinter();
            Printer.Command.UploadImage_FullColor(mOpenFileDialog.FileName, DisplayName, 0);

            // Print Image
            Printer.Command.Start();
            Printer.Command.PrintImageByName(DisplayName, 10, 10);
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Print 1D Barcode] Button
        //------------------------------------------------------------------------
        private void Btn_PrintCode39_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();

            Printer.Command.Start();
            Printer.Command.PrintText(10, 10, 34, "Arial", "Code 39");
            Printer.Command.PrintBarCode(BarCodeType.Code39, 10, 50, "1234");           // Code39
            Printer.Command.PrintText(10, 210, 34, "Arial", "EAN128");
            Printer.Command.PrintBarCode(BarCodeType.EAN128, 10, 250, "1234");          // EAN128(GS1128)
            Printer.Command.PrintText(10, 410, 34, "Arial", "Code128 Subset A");
            Printer.Command.PrintBarCode(BarCodeType.Code128_Subset, 10, 450, 2, 6, 80, 0, 1, "A1234");     // Code128 Subset A
            Printer.Command.End();
            
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Print QRCode] Button
        //------------------------------------------------------------------------
        private void Btn_QRCode_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();
            Printer.Command.Start();

            // Print QR Code by Dll Function
            Encoding mEncoding = Encoding.GetEncoding(CodePage_Code[Cbo_QR_Encoding.SelectedIndex]);
            Printer.Command.PrintQRCode(10, 10, Txt_QRcode_Data.Text, mEncoding);
           

            // Print QR Code by Send() & SendByte()
            Printer.Command.Send("W240,10,5,2,M,8,5," + mEncoding.GetByteCount(Txt_QRcode_Data.Text) + ",0");
            Printer.Command.SendByte(mEncoding.GetBytes(Txt_QRcode_Data.Text));

            

            Printer.Command.End();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Print PDF417] Button
        //------------------------------------------------------------------------
        private void Btn_PDF417_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();
            Printer.Command.Start();
            Printer.Command.PrintPDF417(10, 10, "GoDEX PDF417 Test");
            Printer.Command.End();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Print Aztec] Button
        //------------------------------------------------------------------------
        private void Btn_Aztec_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();
            Printer.Command.Start();
            Printer.Command.PrintAztec(10, 10, "GoDEX Aztec Test");
            Printer.Command.End();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Print Maxicode] Button
        //------------------------------------------------------------------------
        private void Btn_Maxicode_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();
            Printer.Command.Start();
            Printer.Command.PrintMaxiCode(10, 10, "840", "068107317", "666", "123456");
            Printer.Command.End();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Print DataMatrix Code] Button
        //------------------------------------------------------------------------
        private void Btn_DataMatrix_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();
            Printer.Command.Start();
            Printer.Command.PrintDataMatrix(10, 10, "GoDEX Test");
            Printer.Command.End();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Upload TTF] Button
        //------------------------------------------------------------------------
        private void Btn_TTF_Click(object sender, EventArgs e)
        {
            int len;
            int nCurrentSize = 0;
            int nPackageSize = 1024;
            byte[] ByteData = new byte[nPackageSize];

            // Select TTF File
            OpenFileDialog mOpenFileDialog = new OpenFileDialog();
            mOpenFileDialog.Filter = "*.ttf|*.ttf";
            if (mOpenFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            // TTFFontName : English alphabet and numbers only
            string TTFFontName = System.IO.Path.GetFileNameWithoutExtension(mOpenFileDialog.FileName);
            for (int i = TTFFontName.Length - 1; i >= 0; i--)
            {
                if (!(System.Text.RegularExpressions.Regex.IsMatch(TTFFontName.Substring(i, 1), @"^[0-9]+$")) &&
                    !(System.Text.RegularExpressions.Regex.IsMatch(TTFFontName.Substring(i, 1), @"^[a-z]+$")) &&
                    !(System.Text.RegularExpressions.Regex.IsMatch(TTFFontName.Substring(i, 1), @"^[A-Z]+$")))
                {
                    TTFFontName = TTFFontName.Remove(i, 1);
                }
            }

            // Get File Size
            System.IO.FileInfo mFileInfo = new System.IO.FileInfo(mOpenFileDialog.FileName);
            long nFileSize = mFileInfo.Length;

            Prog_TTF.Visible = true;

            // Connect Printer
            ConnectPrinter();

            string mCommand = "~MDELC," + Cbo_PosTTF.SelectedItem.ToString() + "\r\n\r\n\r\n" + "~H,TTF," + Cbo_PosTTF.SelectedItem.ToString() + TTFFontName + "," + nFileSize.ToString() + "\r";
            byte[] ByteCmd = Encoding.Default.GetBytes(mCommand);
            Printer.Command.SendByte(ByteCmd);

            //// Delete TTF Command
            //Printer.Command.Send("~MDELC," + Cbo_PosTTF.SelectedItem.ToString() + "\r\n\r\n");
            //
            //// Upload TTF Command
            //Printer.Command.Send("~H,TTF," + Cbo_PosTTF.SelectedItem.ToString() + TTFFontName + "," + nFileSize.ToString());

            // Open TTF File 
            System.IO.FileStream fs = System.IO.File.OpenRead(mOpenFileDialog.FileName);

            // Upload TTF Body
            while (nCurrentSize < nFileSize)
            {
                len = fs.Read(ByteData, 0, nPackageSize);

                if (len > 0)
                {
                    //ByteData = new byte[len];
                    Printer.Command.SendByte(ByteData, len);
                    nCurrentSize += len;
                }

                Prog_TTF.Value = (int)(nCurrentSize * 100 / nFileSize);
            }

            fs.Close();
            DisconnectPrinter();

            Prog_TTF.Visible = false;
        }

        //------------------------------------------------------------------------
        // Press [Print Shape] Button
        //------------------------------------------------------------------------
        private void Btn_PrintShape_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();

            // First Label
            Printer.Command.Start();
            Printer.Command.DrawHorLine(10, 10, 200, 5);
            Printer.Command.DrawVerLine(10, 10, 200, 5);
            Printer.Command.DrawOblique(10, 10, 5, 200, 200);
            Printer.Command.FillRec(10, 260, 200, 150);
            Printer.Command.DrawRec(300, 260, 200, 150, 5, 5);
            Printer.Command.End();

            // Second Label
            Printer.Command.Start();
            Printer.Command.DrawEllipse(10, 10, 200, 100, 5);
            Printer.Command.DrawRoundRec(300, 10, 200, 100, 20, 20, 5);
            Printer.Command.DrawTriangle(50, 150, 200, 180, 150, 250, 5);
            Printer.Command.DrawDiamond(300, 150, 200, 100, 5);
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Print Halftone Image] Button
        //------------------------------------------------------------------------
        private void Btn_PrintHalftoneImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog mOpenFileDialog = new OpenFileDialog();
            mOpenFileDialog.Filter = "*.bmp|*.bmp|*.gif|*.gif|*.jpg|*.jpg|*.png|*.png";
            if (mOpenFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            ConnectPrinter();
            LabelSetup();
            Printer.Command.Start();
            Printer.Command.PrintHalftoneImage(10, 10, mOpenFileDialog.FileName, 0, HalftoneMode.ClusterDithering);
            Printer.Command.PrintHalftoneImage(160, 10, mOpenFileDialog.FileName, 0, HalftoneMode.DispersedDithering);
            Printer.Command.PrintHalftoneImage(310, 10, mOpenFileDialog.FileName, 0, HalftoneMode.DiffisionDithering);
            Printer.Command.End();
            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Internal Font Print] Button
        //------------------------------------------------------------------------
        private void Btn_InternalFont_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();

            // Internal Font (Change Font)
            Printer.Command.Start();
            Printer.Command.PrintText_EZPL_Internal("A", 10, 10, "ABCDEFG");
            Printer.Command.PrintText_EZPL_Internal("B", 10, 60, "ABCDEFG");
            Printer.Command.PrintText_EZPL_Internal("C", 10, 110, "ABCDEFG");
            Printer.Command.PrintText_EZPL_Internal("D", 10, 160, "ABCDEFG");
            Printer.Command.PrintText_EZPL_Internal("E", 10, 210, "ABCDEFG");
            Printer.Command.PrintText_EZPL_Internal("F", 10, 260, "ABCDEFG");
            Printer.Command.End();

            // Internal Font (Change Zoom)
            Printer.Command.Start();
            Printer.Command.PrintText_EZPL_Internal("C", 10, 10, 1, 1, 0, "0", "Font C x 1");
            Printer.Command.PrintText_EZPL_Internal("C", 10, 80, 2, 2, 0, "0", "Font C x 2");
            Printer.Command.PrintText_EZPL_Internal("C", 10, 150, 3, 3, 0, "0", "Font C x 3");
            Printer.Command.PrintText_EZPL_Internal("C", 10, 220, 4, 4, 0, "0", "Font C x 4");
            Printer.Command.End();

            // Internal Font (Change Rotate & Inverse)
            Printer.Command.Start();
            Printer.Command.PrintText_EZPL_Internal("E", 10, 10, 1, 1, 0, "0", "Degree 0");
            Printer.Command.PrintText_EZPL_Internal("E", 100, 100, 1, 1, 0, "1I", "Degree 90");
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Asia Font Print] Button
        //------------------------------------------------------------------------
        private void Btn_AsiaFont_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();

            // Asia Font (Need to upload [Tranditional Chainese] to AZ1)
            Printer.Command.Start();
            Printer.Command.PrintText_EZPL_Internal("Z1", 10, 10, "亞洲字測試");
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [Download Font Print] Button
        //------------------------------------------------------------------------
        private void Btn_BitmappedFont_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();

            // EZPL VA ~ VZ Command
            Printer.Command.Start();
            Printer.Command.PrintText_EZPL_Bitmapped("A", 10, 10, "Download Font");
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
        // Press [True Type Font Print] Button
        //------------------------------------------------------------------------
        private void Btn_TrueTypeFont_Click(object sender, EventArgs e)
        {
            ConnectPrinter();
            LabelSetup();

            // EZPL ATA ~ ATZ Command
            Printer.Command.Start();
            Printer.Command.PrintText_EZPL_TTF("", 10, 10, "Built-In TTF Test");
            Printer.Command.PrintText_EZPL_TTF("A", 10, 100, "ATA TTF Test");
            Printer.Command.End();

            DisconnectPrinter();
        }

        //------------------------------------------------------------------------
    }
}
