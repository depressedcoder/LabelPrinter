namespace DLLTest
{
    partial class FrmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_PrintText = new System.Windows.Forms.Button();
            this.Btn_Upload_Int = new System.Windows.Forms.Button();
            this.Btn_UploadTextImage = new System.Windows.Forms.Button();
            this.Btn_PrintImage = new System.Windows.Forms.Button();
            this.Btn_HalfToneImage = new System.Windows.Forms.Button();
            this.Cbo_COM = new System.Windows.Forms.ComboBox();
            this.Txt_Baud = new System.Windows.Forms.TextBox();
            this.Btn_Upload_Ext = new System.Windows.Forms.Button();
            this.Btn_Execute = new System.Windows.Forms.Button();
            this.Btn_GetVersion = new System.Windows.Forms.Button();
            this.Btn_SendByte = new System.Windows.Forms.Button();
            this.Grp_Communication = new System.Windows.Forms.GroupBox();
            this.Cbo_Driver = new System.Windows.Forms.ComboBox();
            this.Lbl_NetPort = new System.Windows.Forms.Label();
            this.Lbl_Baud = new System.Windows.Forms.Label();
            this.Cbo_LPT = new System.Windows.Forms.ComboBox();
            this.Txt_NetPort = new System.Windows.Forms.TextBox();
            this.Txt_IP = new System.Windows.Forms.TextBox();
            this.RBtn_NET = new System.Windows.Forms.RadioButton();
            this.RBtn_Driver = new System.Windows.Forms.RadioButton();
            this.RBtn_LPT = new System.Windows.Forms.RadioButton();
            this.RBtn_COM = new System.Windows.Forms.RadioButton();
            this.RBtn_USB = new System.Windows.Forms.RadioButton();
            this.Grp_Label = new System.Windows.Forms.GroupBox();
            this.Lbl_Page = new System.Windows.Forms.Label();
            this.Num_Page = new System.Windows.Forms.NumericUpDown();
            this.Num_Speed = new System.Windows.Forms.NumericUpDown();
            this.Lbl_Speed = new System.Windows.Forms.Label();
            this.Lbl_Copy = new System.Windows.Forms.Label();
            this.Num_Copy = new System.Windows.Forms.NumericUpDown();
            this.Num_Dark = new System.Windows.Forms.NumericUpDown();
            this.Lbl_Dark = new System.Windows.Forms.Label();
            this.Num_GapFeed = new System.Windows.Forms.NumericUpDown();
            this.Lbl_GapFeed = new System.Windows.Forms.Label();
            this.Lbl_PaperType = new System.Windows.Forms.Label();
            this.Lbl_Label_H = new System.Windows.Forms.Label();
            this.Num_Label_H = new System.Windows.Forms.NumericUpDown();
            this.Num_Label_W = new System.Windows.Forms.NumericUpDown();
            this.Cbo_PaperType = new System.Windows.Forms.ComboBox();
            this.Lbl_Label_W = new System.Windows.Forms.Label();
            this.Btn_AutoSensing = new System.Windows.Forms.Button();
            this.Cbo_Cmd = new System.Windows.Forms.ComboBox();
            this.Grp_Command = new System.Windows.Forms.GroupBox();
            this.Txt_Reply = new System.Windows.Forms.TextBox();
            this.Txt_Cmd = new System.Windows.Forms.TextBox();
            this.Grp_Demo = new System.Windows.Forms.GroupBox();
            this.Btn_PrintHalftoneImg = new System.Windows.Forms.Button();
            this.Btn_TrueTypeFont = new System.Windows.Forms.Button();
            this.Btn_BitmappedFont = new System.Windows.Forms.Button();
            this.Btn_AsiaFont = new System.Windows.Forms.Button();
            this.Btn_InternalFont = new System.Windows.Forms.Button();
            this.Btn_PrintShape = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_QRcode_Data = new System.Windows.Forms.TextBox();
            this.Cbo_QR_Encoding = new System.Windows.Forms.ComboBox();
            this.Btn_DataMatrix = new System.Windows.Forms.Button();
            this.Btn_Maxicode = new System.Windows.Forms.Button();
            this.Btn_Aztec = new System.Windows.Forms.Button();
            this.Btn_PDF417 = new System.Windows.Forms.Button();
            this.Btn_QRCode = new System.Windows.Forms.Button();
            this.Btn_PrintCode39 = new System.Windows.Forms.Button();
            this.Grp_UploadTTF = new System.Windows.Forms.GroupBox();
            this.Prog_TTF = new System.Windows.Forms.ProgressBar();
            this.Btn_TTF = new System.Windows.Forms.Button();
            this.Cbo_PosTTF = new System.Windows.Forms.ComboBox();
            this.Grp_UploadImage = new System.Windows.Forms.GroupBox();
            this.Grp_Communication.SuspendLayout();
            this.Grp_Label.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Page)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Copy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Dark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_GapFeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Label_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Label_W)).BeginInit();
            this.Grp_Command.SuspendLayout();
            this.Grp_Demo.SuspendLayout();
            this.Grp_UploadTTF.SuspendLayout();
            this.Grp_UploadImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_PrintText
            // 
            this.Btn_PrintText.Location = new System.Drawing.Point(19, 29);
            this.Btn_PrintText.Name = "Btn_PrintText";
            this.Btn_PrintText.Size = new System.Drawing.Size(122, 23);
            this.Btn_PrintText.TabIndex = 0;
            this.Btn_PrintText.Text = "Print Text";
            this.Btn_PrintText.UseVisualStyleBackColor = true;
            this.Btn_PrintText.Click += new System.EventHandler(this.Btn_PrintText_Click);
            // 
            // Btn_Upload_Int
            // 
            this.Btn_Upload_Int.Location = new System.Drawing.Point(11, 95);
            this.Btn_Upload_Int.Name = "Btn_Upload_Int";
            this.Btn_Upload_Int.Size = new System.Drawing.Size(202, 28);
            this.Btn_Upload_Int.TabIndex = 1;
            this.Btn_Upload_Int.Text = "Upload Image To Internal Memory";
            this.Btn_Upload_Int.UseVisualStyleBackColor = true;
            this.Btn_Upload_Int.Click += new System.EventHandler(this.Btn_Upload_Int_Click);
            // 
            // Btn_UploadTextImage
            // 
            this.Btn_UploadTextImage.Location = new System.Drawing.Point(20, 174);
            this.Btn_UploadTextImage.Name = "Btn_UploadTextImage";
            this.Btn_UploadTextImage.Size = new System.Drawing.Size(122, 23);
            this.Btn_UploadTextImage.TabIndex = 5;
            this.Btn_UploadTextImage.Text = "Upload Text Image";
            this.Btn_UploadTextImage.UseVisualStyleBackColor = true;
            this.Btn_UploadTextImage.Click += new System.EventHandler(this.Btn_UploadTextImage_Click);
            // 
            // Btn_PrintImage
            // 
            this.Btn_PrintImage.Location = new System.Drawing.Point(19, 58);
            this.Btn_PrintImage.Name = "Btn_PrintImage";
            this.Btn_PrintImage.Size = new System.Drawing.Size(122, 23);
            this.Btn_PrintImage.TabIndex = 6;
            this.Btn_PrintImage.Text = "Print Image";
            this.Btn_PrintImage.UseVisualStyleBackColor = true;
            this.Btn_PrintImage.Click += new System.EventHandler(this.Btn_PrintImage_Click);
            // 
            // Btn_HalfToneImage
            // 
            this.Btn_HalfToneImage.Location = new System.Drawing.Point(11, 27);
            this.Btn_HalfToneImage.Name = "Btn_HalfToneImage";
            this.Btn_HalfToneImage.Size = new System.Drawing.Size(202, 28);
            this.Btn_HalfToneImage.TabIndex = 7;
            this.Btn_HalfToneImage.Text = "Upload Image And Do Halftoning";
            this.Btn_HalfToneImage.UseVisualStyleBackColor = true;
            this.Btn_HalfToneImage.Click += new System.EventHandler(this.Btn_HalfToneImage_Click);
            // 
            // Cbo_COM
            // 
            this.Cbo_COM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Cbo_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_COM.FormattingEnabled = true;
            this.Cbo_COM.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.Cbo_COM.Location = new System.Drawing.Point(127, 42);
            this.Cbo_COM.Name = "Cbo_COM";
            this.Cbo_COM.Size = new System.Drawing.Size(431, 20);
            this.Cbo_COM.TabIndex = 1;
            // 
            // Txt_Baud
            // 
            this.Txt_Baud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Baud.Location = new System.Drawing.Point(654, 42);
            this.Txt_Baud.Name = "Txt_Baud";
            this.Txt_Baud.Size = new System.Drawing.Size(84, 22);
            this.Txt_Baud.TabIndex = 0;
            this.Txt_Baud.Text = "9600";
            // 
            // Btn_Upload_Ext
            // 
            this.Btn_Upload_Ext.Location = new System.Drawing.Point(11, 61);
            this.Btn_Upload_Ext.Name = "Btn_Upload_Ext";
            this.Btn_Upload_Ext.Size = new System.Drawing.Size(202, 28);
            this.Btn_Upload_Ext.TabIndex = 17;
            this.Btn_Upload_Ext.Text = "Upload Image To External Memory";
            this.Btn_Upload_Ext.UseVisualStyleBackColor = true;
            this.Btn_Upload_Ext.Click += new System.EventHandler(this.Btn_Upload_Ext_Click);
            // 
            // Btn_Execute
            // 
            this.Btn_Execute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Execute.Location = new System.Drawing.Point(17, 176);
            this.Btn_Execute.Name = "Btn_Execute";
            this.Btn_Execute.Size = new System.Drawing.Size(145, 23);
            this.Btn_Execute.TabIndex = 16;
            this.Btn_Execute.Text = "Execute";
            this.Btn_Execute.UseVisualStyleBackColor = true;
            this.Btn_Execute.Click += new System.EventHandler(this.Btn_Execute_Click);
            // 
            // Btn_GetVersion
            // 
            this.Btn_GetVersion.Location = new System.Drawing.Point(19, 116);
            this.Btn_GetVersion.Name = "Btn_GetVersion";
            this.Btn_GetVersion.Size = new System.Drawing.Size(122, 23);
            this.Btn_GetVersion.TabIndex = 15;
            this.Btn_GetVersion.Text = "Get Dll Version";
            this.Btn_GetVersion.UseVisualStyleBackColor = true;
            this.Btn_GetVersion.Click += new System.EventHandler(this.Btn_GetVersion_Click);
            // 
            // Btn_SendByte
            // 
            this.Btn_SendByte.Location = new System.Drawing.Point(20, 145);
            this.Btn_SendByte.Name = "Btn_SendByte";
            this.Btn_SendByte.Size = new System.Drawing.Size(122, 23);
            this.Btn_SendByte.TabIndex = 14;
            this.Btn_SendByte.Text = "Send Byte Array";
            this.Btn_SendByte.UseVisualStyleBackColor = true;
            this.Btn_SendByte.Click += new System.EventHandler(this.Btn_SendByte_Click);
            // 
            // Grp_Communication
            // 
            this.Grp_Communication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Grp_Communication.Controls.Add(this.Cbo_Driver);
            this.Grp_Communication.Controls.Add(this.Lbl_NetPort);
            this.Grp_Communication.Controls.Add(this.Lbl_Baud);
            this.Grp_Communication.Controls.Add(this.Cbo_LPT);
            this.Grp_Communication.Controls.Add(this.Txt_NetPort);
            this.Grp_Communication.Controls.Add(this.Txt_IP);
            this.Grp_Communication.Controls.Add(this.RBtn_NET);
            this.Grp_Communication.Controls.Add(this.Cbo_COM);
            this.Grp_Communication.Controls.Add(this.RBtn_Driver);
            this.Grp_Communication.Controls.Add(this.Txt_Baud);
            this.Grp_Communication.Controls.Add(this.RBtn_LPT);
            this.Grp_Communication.Controls.Add(this.RBtn_COM);
            this.Grp_Communication.Controls.Add(this.RBtn_USB);
            this.Grp_Communication.Location = new System.Drawing.Point(12, 8);
            this.Grp_Communication.Name = "Grp_Communication";
            this.Grp_Communication.Size = new System.Drawing.Size(756, 143);
            this.Grp_Communication.TabIndex = 12;
            this.Grp_Communication.TabStop = false;
            this.Grp_Communication.Text = " Communication ";
            // 
            // Cbo_Driver
            // 
            this.Cbo_Driver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Cbo_Driver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_Driver.FormattingEnabled = true;
            this.Cbo_Driver.Items.AddRange(new object[] {
            "LPT1",
            "LPT2"});
            this.Cbo_Driver.Location = new System.Drawing.Point(127, 86);
            this.Cbo_Driver.Name = "Cbo_Driver";
            this.Cbo_Driver.Size = new System.Drawing.Size(431, 20);
            this.Cbo_Driver.TabIndex = 26;
            // 
            // Lbl_NetPort
            // 
            this.Lbl_NetPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_NetPort.AutoSize = true;
            this.Lbl_NetPort.Location = new System.Drawing.Point(589, 114);
            this.Lbl_NetPort.Name = "Lbl_NetPort";
            this.Lbl_NetPort.Size = new System.Drawing.Size(24, 12);
            this.Lbl_NetPort.TabIndex = 25;
            this.Lbl_NetPort.Text = "Port";
            // 
            // Lbl_Baud
            // 
            this.Lbl_Baud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_Baud.AutoSize = true;
            this.Lbl_Baud.Location = new System.Drawing.Point(589, 47);
            this.Lbl_Baud.Name = "Lbl_Baud";
            this.Lbl_Baud.Size = new System.Drawing.Size(54, 12);
            this.Lbl_Baud.TabIndex = 24;
            this.Lbl_Baud.Text = "Baud Rate";
            // 
            // Cbo_LPT
            // 
            this.Cbo_LPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Cbo_LPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_LPT.FormattingEnabled = true;
            this.Cbo_LPT.Items.AddRange(new object[] {
            "LPT1",
            "LPT2"});
            this.Cbo_LPT.Location = new System.Drawing.Point(127, 64);
            this.Cbo_LPT.Name = "Cbo_LPT";
            this.Cbo_LPT.Size = new System.Drawing.Size(431, 20);
            this.Cbo_LPT.TabIndex = 22;
            // 
            // Txt_NetPort
            // 
            this.Txt_NetPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_NetPort.Location = new System.Drawing.Point(654, 108);
            this.Txt_NetPort.Name = "Txt_NetPort";
            this.Txt_NetPort.Size = new System.Drawing.Size(84, 22);
            this.Txt_NetPort.TabIndex = 21;
            this.Txt_NetPort.Text = "9100";
            // 
            // Txt_IP
            // 
            this.Txt_IP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_IP.Location = new System.Drawing.Point(127, 109);
            this.Txt_IP.Name = "Txt_IP";
            this.Txt_IP.Size = new System.Drawing.Size(431, 22);
            this.Txt_IP.TabIndex = 20;
            this.Txt_IP.Text = "192.168.102.101";
            // 
            // RBtn_NET
            // 
            this.RBtn_NET.AutoSize = true;
            this.RBtn_NET.Location = new System.Drawing.Point(22, 109);
            this.RBtn_NET.Name = "RBtn_NET";
            this.RBtn_NET.Size = new System.Drawing.Size(63, 16);
            this.RBtn_NET.TabIndex = 4;
            this.RBtn_NET.Text = "Network";
            this.RBtn_NET.UseVisualStyleBackColor = true;
            // 
            // RBtn_Driver
            // 
            this.RBtn_Driver.AutoSize = true;
            this.RBtn_Driver.Location = new System.Drawing.Point(22, 87);
            this.RBtn_Driver.Name = "RBtn_Driver";
            this.RBtn_Driver.Size = new System.Drawing.Size(53, 16);
            this.RBtn_Driver.TabIndex = 3;
            this.RBtn_Driver.Text = "Driver";
            this.RBtn_Driver.UseVisualStyleBackColor = true;
            // 
            // RBtn_LPT
            // 
            this.RBtn_LPT.AutoSize = true;
            this.RBtn_LPT.Location = new System.Drawing.Point(22, 65);
            this.RBtn_LPT.Name = "RBtn_LPT";
            this.RBtn_LPT.Size = new System.Drawing.Size(43, 16);
            this.RBtn_LPT.TabIndex = 2;
            this.RBtn_LPT.Text = "LPT";
            this.RBtn_LPT.UseVisualStyleBackColor = true;
            // 
            // RBtn_COM
            // 
            this.RBtn_COM.AutoSize = true;
            this.RBtn_COM.Location = new System.Drawing.Point(22, 43);
            this.RBtn_COM.Name = "RBtn_COM";
            this.RBtn_COM.Size = new System.Drawing.Size(49, 16);
            this.RBtn_COM.TabIndex = 1;
            this.RBtn_COM.Text = "COM";
            this.RBtn_COM.UseVisualStyleBackColor = true;
            // 
            // RBtn_USB
            // 
            this.RBtn_USB.AutoSize = true;
            this.RBtn_USB.Checked = true;
            this.RBtn_USB.Location = new System.Drawing.Point(22, 21);
            this.RBtn_USB.Name = "RBtn_USB";
            this.RBtn_USB.Size = new System.Drawing.Size(45, 16);
            this.RBtn_USB.TabIndex = 0;
            this.RBtn_USB.TabStop = true;
            this.RBtn_USB.Text = "USB";
            this.RBtn_USB.UseVisualStyleBackColor = true;
            // 
            // Grp_Label
            // 
            this.Grp_Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Grp_Label.Controls.Add(this.Lbl_Page);
            this.Grp_Label.Controls.Add(this.Num_Page);
            this.Grp_Label.Controls.Add(this.Num_Speed);
            this.Grp_Label.Controls.Add(this.Lbl_Speed);
            this.Grp_Label.Controls.Add(this.Lbl_Copy);
            this.Grp_Label.Controls.Add(this.Num_Copy);
            this.Grp_Label.Controls.Add(this.Num_Dark);
            this.Grp_Label.Controls.Add(this.Lbl_Dark);
            this.Grp_Label.Controls.Add(this.Num_GapFeed);
            this.Grp_Label.Controls.Add(this.Lbl_GapFeed);
            this.Grp_Label.Controls.Add(this.Lbl_PaperType);
            this.Grp_Label.Controls.Add(this.Lbl_Label_H);
            this.Grp_Label.Controls.Add(this.Num_Label_H);
            this.Grp_Label.Controls.Add(this.Num_Label_W);
            this.Grp_Label.Controls.Add(this.Cbo_PaperType);
            this.Grp_Label.Controls.Add(this.Lbl_Label_W);
            this.Grp_Label.Location = new System.Drawing.Point(12, 160);
            this.Grp_Label.Name = "Grp_Label";
            this.Grp_Label.Size = new System.Drawing.Size(358, 143);
            this.Grp_Label.TabIndex = 18;
            this.Grp_Label.TabStop = false;
            this.Grp_Label.Text = " Label Setup ";
            // 
            // Lbl_Page
            // 
            this.Lbl_Page.AutoSize = true;
            this.Lbl_Page.Location = new System.Drawing.Point(226, 113);
            this.Lbl_Page.Name = "Lbl_Page";
            this.Lbl_Page.Size = new System.Drawing.Size(44, 12);
            this.Lbl_Page.TabIndex = 40;
            this.Lbl_Page.Text = "Page No";
            // 
            // Num_Page
            // 
            this.Num_Page.Location = new System.Drawing.Point(294, 108);
            this.Num_Page.Name = "Num_Page";
            this.Num_Page.Size = new System.Drawing.Size(78, 22);
            this.Num_Page.TabIndex = 39;
            this.Num_Page.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Num_Speed
            // 
            this.Num_Speed.Location = new System.Drawing.Point(294, 24);
            this.Num_Speed.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Num_Speed.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Num_Speed.Name = "Num_Speed";
            this.Num_Speed.Size = new System.Drawing.Size(78, 22);
            this.Num_Speed.TabIndex = 38;
            this.Num_Speed.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // Lbl_Speed
            // 
            this.Lbl_Speed.AutoSize = true;
            this.Lbl_Speed.Location = new System.Drawing.Point(226, 29);
            this.Lbl_Speed.Name = "Lbl_Speed";
            this.Lbl_Speed.Size = new System.Drawing.Size(57, 12);
            this.Lbl_Speed.TabIndex = 37;
            this.Lbl_Speed.Text = "Speed (ips)";
            // 
            // Lbl_Copy
            // 
            this.Lbl_Copy.AutoSize = true;
            this.Lbl_Copy.Location = new System.Drawing.Point(226, 85);
            this.Lbl_Copy.Name = "Lbl_Copy";
            this.Lbl_Copy.Size = new System.Drawing.Size(48, 12);
            this.Lbl_Copy.TabIndex = 36;
            this.Lbl_Copy.Text = "Copy No";
            // 
            // Num_Copy
            // 
            this.Num_Copy.Location = new System.Drawing.Point(294, 80);
            this.Num_Copy.Name = "Num_Copy";
            this.Num_Copy.Size = new System.Drawing.Size(78, 22);
            this.Num_Copy.TabIndex = 35;
            this.Num_Copy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Num_Dark
            // 
            this.Num_Dark.Location = new System.Drawing.Point(294, 52);
            this.Num_Dark.Maximum = new decimal(new int[] {
            19,
            0,
            0,
            0});
            this.Num_Dark.Name = "Num_Dark";
            this.Num_Dark.Size = new System.Drawing.Size(78, 22);
            this.Num_Dark.TabIndex = 34;
            this.Num_Dark.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Lbl_Dark
            // 
            this.Lbl_Dark.AutoSize = true;
            this.Lbl_Dark.Location = new System.Drawing.Point(226, 57);
            this.Lbl_Dark.Name = "Lbl_Dark";
            this.Lbl_Dark.Size = new System.Drawing.Size(28, 12);
            this.Lbl_Dark.TabIndex = 33;
            this.Lbl_Dark.Text = "Dark";
            // 
            // Num_GapFeed
            // 
            this.Num_GapFeed.Location = new System.Drawing.Point(127, 52);
            this.Num_GapFeed.Name = "Num_GapFeed";
            this.Num_GapFeed.Size = new System.Drawing.Size(78, 22);
            this.Num_GapFeed.TabIndex = 32;
            this.Num_GapFeed.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // Lbl_GapFeed
            // 
            this.Lbl_GapFeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_GapFeed.AutoSize = true;
            this.Lbl_GapFeed.Location = new System.Drawing.Point(20, 57);
            this.Lbl_GapFeed.Name = "Lbl_GapFeed";
            this.Lbl_GapFeed.Size = new System.Drawing.Size(89, 12);
            this.Lbl_GapFeed.TabIndex = 31;
            this.Lbl_GapFeed.Text = "Gap Length (mm)";
            // 
            // Lbl_PaperType
            // 
            this.Lbl_PaperType.AutoSize = true;
            this.Lbl_PaperType.Location = new System.Drawing.Point(20, 29);
            this.Lbl_PaperType.Name = "Lbl_PaperType";
            this.Lbl_PaperType.Size = new System.Drawing.Size(58, 12);
            this.Lbl_PaperType.TabIndex = 30;
            this.Lbl_PaperType.Text = "Paper Type";
            // 
            // Lbl_Label_H
            // 
            this.Lbl_Label_H.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_Label_H.AutoSize = true;
            this.Lbl_Label_H.Location = new System.Drawing.Point(20, 113);
            this.Lbl_Label_H.Name = "Lbl_Label_H";
            this.Lbl_Label_H.Size = new System.Drawing.Size(94, 12);
            this.Lbl_Label_H.TabIndex = 29;
            this.Lbl_Label_H.Text = "Label Height (mm)";
            // 
            // Num_Label_H
            // 
            this.Num_Label_H.Location = new System.Drawing.Point(127, 108);
            this.Num_Label_H.Name = "Num_Label_H";
            this.Num_Label_H.Size = new System.Drawing.Size(78, 22);
            this.Num_Label_H.TabIndex = 28;
            this.Num_Label_H.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // Num_Label_W
            // 
            this.Num_Label_W.Location = new System.Drawing.Point(127, 80);
            this.Num_Label_W.Name = "Num_Label_W";
            this.Num_Label_W.Size = new System.Drawing.Size(78, 22);
            this.Num_Label_W.TabIndex = 27;
            this.Num_Label_W.Value = new decimal(new int[] {
            54,
            0,
            0,
            0});
            // 
            // Cbo_PaperType
            // 
            this.Cbo_PaperType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_PaperType.FormattingEnabled = true;
            this.Cbo_PaperType.Items.AddRange(new object[] {
            "Gap",
            "Continue"});
            this.Cbo_PaperType.Location = new System.Drawing.Point(127, 26);
            this.Cbo_PaperType.Name = "Cbo_PaperType";
            this.Cbo_PaperType.Size = new System.Drawing.Size(77, 20);
            this.Cbo_PaperType.TabIndex = 26;
            this.Cbo_PaperType.SelectedIndexChanged += new System.EventHandler(this.Cbo_PaperType_SelectedIndexChanged);
            // 
            // Lbl_Label_W
            // 
            this.Lbl_Label_W.AutoSize = true;
            this.Lbl_Label_W.Location = new System.Drawing.Point(20, 85);
            this.Lbl_Label_W.Name = "Lbl_Label_W";
            this.Lbl_Label_W.Size = new System.Drawing.Size(92, 12);
            this.Lbl_Label_W.TabIndex = 24;
            this.Lbl_Label_W.Text = "Label Width (mm)";
            // 
            // Btn_AutoSensing
            // 
            this.Btn_AutoSensing.Location = new System.Drawing.Point(19, 87);
            this.Btn_AutoSensing.Name = "Btn_AutoSensing";
            this.Btn_AutoSensing.Size = new System.Drawing.Size(122, 23);
            this.Btn_AutoSensing.TabIndex = 19;
            this.Btn_AutoSensing.Text = "Auto Sensing";
            this.Btn_AutoSensing.UseVisualStyleBackColor = true;
            this.Btn_AutoSensing.Click += new System.EventHandler(this.Btn_AutoSensing_Click);
            // 
            // Cbo_Cmd
            // 
            this.Cbo_Cmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Cbo_Cmd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_Cmd.FormattingEnabled = true;
            this.Cbo_Cmd.Items.AddRange(new object[] {
            "~B",
            "~V",
            "~S,CHECK",
            "^XGET,CONFIG"});
            this.Cbo_Cmd.Location = new System.Drawing.Point(16, 22);
            this.Cbo_Cmd.Name = "Cbo_Cmd";
            this.Cbo_Cmd.Size = new System.Drawing.Size(149, 20);
            this.Cbo_Cmd.TabIndex = 20;
            this.Cbo_Cmd.SelectedIndexChanged += new System.EventHandler(this.Cbo_Cmd_SelectedIndexChanged);
            // 
            // Grp_Command
            // 
            this.Grp_Command.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Grp_Command.Controls.Add(this.Txt_Reply);
            this.Grp_Command.Controls.Add(this.Txt_Cmd);
            this.Grp_Command.Controls.Add(this.Cbo_Cmd);
            this.Grp_Command.Controls.Add(this.Btn_Execute);
            this.Grp_Command.Location = new System.Drawing.Point(586, 311);
            this.Grp_Command.Name = "Grp_Command";
            this.Grp_Command.Size = new System.Drawing.Size(181, 208);
            this.Grp_Command.TabIndex = 21;
            this.Grp_Command.TabStop = false;
            this.Grp_Command.Text = " Terminal (Send And Receive) ";
            // 
            // Txt_Reply
            // 
            this.Txt_Reply.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Reply.BackColor = System.Drawing.SystemColors.Info;
            this.Txt_Reply.Location = new System.Drawing.Point(16, 77);
            this.Txt_Reply.Multiline = true;
            this.Txt_Reply.Name = "Txt_Reply";
            this.Txt_Reply.ReadOnly = true;
            this.Txt_Reply.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Txt_Reply.Size = new System.Drawing.Size(146, 93);
            this.Txt_Reply.TabIndex = 22;
            // 
            // Txt_Cmd
            // 
            this.Txt_Cmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_Cmd.Location = new System.Drawing.Point(17, 47);
            this.Txt_Cmd.Multiline = true;
            this.Txt_Cmd.Name = "Txt_Cmd";
            this.Txt_Cmd.Size = new System.Drawing.Size(146, 26);
            this.Txt_Cmd.TabIndex = 21;
            // 
            // Grp_Demo
            // 
            this.Grp_Demo.Controls.Add(this.Btn_PrintHalftoneImg);
            this.Grp_Demo.Controls.Add(this.Btn_TrueTypeFont);
            this.Grp_Demo.Controls.Add(this.Btn_BitmappedFont);
            this.Grp_Demo.Controls.Add(this.Btn_AsiaFont);
            this.Grp_Demo.Controls.Add(this.Btn_InternalFont);
            this.Grp_Demo.Controls.Add(this.Btn_PrintShape);
            this.Grp_Demo.Controls.Add(this.label1);
            this.Grp_Demo.Controls.Add(this.Txt_QRcode_Data);
            this.Grp_Demo.Controls.Add(this.Cbo_QR_Encoding);
            this.Grp_Demo.Controls.Add(this.Btn_DataMatrix);
            this.Grp_Demo.Controls.Add(this.Btn_Maxicode);
            this.Grp_Demo.Controls.Add(this.Btn_Aztec);
            this.Grp_Demo.Controls.Add(this.Btn_PDF417);
            this.Grp_Demo.Controls.Add(this.Btn_QRCode);
            this.Grp_Demo.Controls.Add(this.Btn_PrintCode39);
            this.Grp_Demo.Controls.Add(this.Btn_PrintText);
            this.Grp_Demo.Controls.Add(this.Btn_GetVersion);
            this.Grp_Demo.Controls.Add(this.Btn_AutoSensing);
            this.Grp_Demo.Controls.Add(this.Btn_PrintImage);
            this.Grp_Demo.Controls.Add(this.Btn_UploadTextImage);
            this.Grp_Demo.Controls.Add(this.Btn_SendByte);
            this.Grp_Demo.Location = new System.Drawing.Point(12, 311);
            this.Grp_Demo.Name = "Grp_Demo";
            this.Grp_Demo.Size = new System.Drawing.Size(568, 208);
            this.Grp_Demo.TabIndex = 22;
            this.Grp_Demo.TabStop = false;
            this.Grp_Demo.Text = " Print Demo ";
            // 
            // Btn_PrintHalftoneImg
            // 
            this.Btn_PrintHalftoneImg.Location = new System.Drawing.Point(432, 171);
            this.Btn_PrintHalftoneImg.Name = "Btn_PrintHalftoneImg";
            this.Btn_PrintHalftoneImg.Size = new System.Drawing.Size(122, 23);
            this.Btn_PrintHalftoneImg.TabIndex = 37;
            this.Btn_PrintHalftoneImg.Text = "Print Halftone Image";
            this.Btn_PrintHalftoneImg.UseVisualStyleBackColor = true;
            this.Btn_PrintHalftoneImg.Click += new System.EventHandler(this.Btn_PrintHalftoneImg_Click);
            // 
            // Btn_TrueTypeFont
            // 
            this.Btn_TrueTypeFont.Location = new System.Drawing.Point(432, 142);
            this.Btn_TrueTypeFont.Name = "Btn_TrueTypeFont";
            this.Btn_TrueTypeFont.Size = new System.Drawing.Size(122, 23);
            this.Btn_TrueTypeFont.TabIndex = 36;
            this.Btn_TrueTypeFont.Text = "True Type Font Print";
            this.Btn_TrueTypeFont.UseVisualStyleBackColor = true;
            this.Btn_TrueTypeFont.Click += new System.EventHandler(this.Btn_TrueTypeFont_Click);
            // 
            // Btn_BitmappedFont
            // 
            this.Btn_BitmappedFont.Location = new System.Drawing.Point(432, 113);
            this.Btn_BitmappedFont.Name = "Btn_BitmappedFont";
            this.Btn_BitmappedFont.Size = new System.Drawing.Size(122, 23);
            this.Btn_BitmappedFont.TabIndex = 35;
            this.Btn_BitmappedFont.Text = "Download Font Print";
            this.Btn_BitmappedFont.UseVisualStyleBackColor = true;
            this.Btn_BitmappedFont.Click += new System.EventHandler(this.Btn_BitmappedFont_Click);
            // 
            // Btn_AsiaFont
            // 
            this.Btn_AsiaFont.Location = new System.Drawing.Point(432, 84);
            this.Btn_AsiaFont.Name = "Btn_AsiaFont";
            this.Btn_AsiaFont.Size = new System.Drawing.Size(122, 23);
            this.Btn_AsiaFont.TabIndex = 34;
            this.Btn_AsiaFont.Text = "Asia Font Print";
            this.Btn_AsiaFont.UseVisualStyleBackColor = true;
            this.Btn_AsiaFont.Click += new System.EventHandler(this.Btn_AsiaFont_Click);
            // 
            // Btn_InternalFont
            // 
            this.Btn_InternalFont.Location = new System.Drawing.Point(432, 55);
            this.Btn_InternalFont.Name = "Btn_InternalFont";
            this.Btn_InternalFont.Size = new System.Drawing.Size(122, 23);
            this.Btn_InternalFont.TabIndex = 33;
            this.Btn_InternalFont.Text = "Internal Font Print";
            this.Btn_InternalFont.UseVisualStyleBackColor = true;
            this.Btn_InternalFont.Click += new System.EventHandler(this.Btn_InternalFont_Click);
            // 
            // Btn_PrintShape
            // 
            this.Btn_PrintShape.Location = new System.Drawing.Point(432, 26);
            this.Btn_PrintShape.Name = "Btn_PrintShape";
            this.Btn_PrintShape.Size = new System.Drawing.Size(122, 23);
            this.Btn_PrintShape.TabIndex = 32;
            this.Btn_PrintShape.Text = "Print Shape";
            this.Btn_PrintShape.UseVisualStyleBackColor = true;
            this.Btn_PrintShape.Click += new System.EventHandler(this.Btn_PrintShape_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "QR Code";
            // 
            // Txt_QRcode_Data
            // 
            this.Txt_QRcode_Data.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_QRcode_Data.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_QRcode_Data.Location = new System.Drawing.Point(294, 60);
            this.Txt_QRcode_Data.Multiline = true;
            this.Txt_QRcode_Data.Name = "Txt_QRcode_Data";
            this.Txt_QRcode_Data.Size = new System.Drawing.Size(121, 108);
            this.Txt_QRcode_Data.TabIndex = 28;
            this.Txt_QRcode_Data.Text = "English\r\n繁體\r\n简体\r\n日本語テスト\r\n한국어 시험";
            // 
            // Cbo_QR_Encoding
            // 
            this.Cbo_QR_Encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_QR_Encoding.FormattingEnabled = true;
            this.Cbo_QR_Encoding.Location = new System.Drawing.Point(210, 29);
            this.Cbo_QR_Encoding.Name = "Cbo_QR_Encoding";
            this.Cbo_QR_Encoding.Size = new System.Drawing.Size(206, 20);
            this.Cbo_QR_Encoding.TabIndex = 27;
            // 
            // Btn_DataMatrix
            // 
            this.Btn_DataMatrix.Location = new System.Drawing.Point(156, 174);
            this.Btn_DataMatrix.Name = "Btn_DataMatrix";
            this.Btn_DataMatrix.Size = new System.Drawing.Size(122, 23);
            this.Btn_DataMatrix.TabIndex = 25;
            this.Btn_DataMatrix.Text = "Print DataMatrix Code";
            this.Btn_DataMatrix.UseVisualStyleBackColor = true;
            this.Btn_DataMatrix.Click += new System.EventHandler(this.Btn_DataMatrix_Click);
            // 
            // Btn_Maxicode
            // 
            this.Btn_Maxicode.Location = new System.Drawing.Point(157, 145);
            this.Btn_Maxicode.Name = "Btn_Maxicode";
            this.Btn_Maxicode.Size = new System.Drawing.Size(122, 23);
            this.Btn_Maxicode.TabIndex = 24;
            this.Btn_Maxicode.Text = "Print Maxicode";
            this.Btn_Maxicode.UseVisualStyleBackColor = true;
            this.Btn_Maxicode.Click += new System.EventHandler(this.Btn_Maxicode_Click);
            // 
            // Btn_Aztec
            // 
            this.Btn_Aztec.Location = new System.Drawing.Point(156, 116);
            this.Btn_Aztec.Name = "Btn_Aztec";
            this.Btn_Aztec.Size = new System.Drawing.Size(122, 23);
            this.Btn_Aztec.TabIndex = 23;
            this.Btn_Aztec.Text = "Print Aztec";
            this.Btn_Aztec.UseVisualStyleBackColor = true;
            this.Btn_Aztec.Click += new System.EventHandler(this.Btn_Aztec_Click);
            // 
            // Btn_PDF417
            // 
            this.Btn_PDF417.Location = new System.Drawing.Point(156, 87);
            this.Btn_PDF417.Name = "Btn_PDF417";
            this.Btn_PDF417.Size = new System.Drawing.Size(122, 23);
            this.Btn_PDF417.TabIndex = 22;
            this.Btn_PDF417.Text = "Print PDF417";
            this.Btn_PDF417.UseVisualStyleBackColor = true;
            this.Btn_PDF417.Click += new System.EventHandler(this.Btn_PDF417_Click);
            // 
            // Btn_QRCode
            // 
            this.Btn_QRCode.Location = new System.Drawing.Point(294, 174);
            this.Btn_QRCode.Name = "Btn_QRCode";
            this.Btn_QRCode.Size = new System.Drawing.Size(122, 23);
            this.Btn_QRCode.TabIndex = 21;
            this.Btn_QRCode.Text = "Print QRCode";
            this.Btn_QRCode.UseVisualStyleBackColor = true;
            this.Btn_QRCode.Click += new System.EventHandler(this.Btn_QRCode_Click);
            // 
            // Btn_PrintCode39
            // 
            this.Btn_PrintCode39.Location = new System.Drawing.Point(156, 58);
            this.Btn_PrintCode39.Name = "Btn_PrintCode39";
            this.Btn_PrintCode39.Size = new System.Drawing.Size(122, 23);
            this.Btn_PrintCode39.TabIndex = 20;
            this.Btn_PrintCode39.Text = "Print 1D Barcode";
            this.Btn_PrintCode39.UseVisualStyleBackColor = true;
            this.Btn_PrintCode39.Click += new System.EventHandler(this.Btn_PrintCode39_Click);
            // 
            // Grp_UploadTTF
            // 
            this.Grp_UploadTTF.Controls.Add(this.Prog_TTF);
            this.Grp_UploadTTF.Controls.Add(this.Btn_TTF);
            this.Grp_UploadTTF.Controls.Add(this.Cbo_PosTTF);
            this.Grp_UploadTTF.Location = new System.Drawing.Point(406, 162);
            this.Grp_UploadTTF.Name = "Grp_UploadTTF";
            this.Grp_UploadTTF.Size = new System.Drawing.Size(135, 141);
            this.Grp_UploadTTF.TabIndex = 23;
            this.Grp_UploadTTF.TabStop = false;
            this.Grp_UploadTTF.Text = "Upload TTF";
            // 
            // Prog_TTF
            // 
            this.Prog_TTF.Location = new System.Drawing.Point(18, 55);
            this.Prog_TTF.Name = "Prog_TTF";
            this.Prog_TTF.Size = new System.Drawing.Size(101, 19);
            this.Prog_TTF.TabIndex = 3;
            this.Prog_TTF.Visible = false;
            // 
            // Btn_TTF
            // 
            this.Btn_TTF.Location = new System.Drawing.Point(18, 99);
            this.Btn_TTF.Name = "Btn_TTF";
            this.Btn_TTF.Size = new System.Drawing.Size(101, 26);
            this.Btn_TTF.TabIndex = 2;
            this.Btn_TTF.Text = "Upload TTF";
            this.Btn_TTF.UseVisualStyleBackColor = true;
            this.Btn_TTF.Click += new System.EventHandler(this.Btn_TTF_Click);
            // 
            // Cbo_PosTTF
            // 
            this.Cbo_PosTTF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_PosTTF.FormattingEnabled = true;
            this.Cbo_PosTTF.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.Cbo_PosTTF.Location = new System.Drawing.Point(18, 27);
            this.Cbo_PosTTF.Name = "Cbo_PosTTF";
            this.Cbo_PosTTF.Size = new System.Drawing.Size(101, 20);
            this.Cbo_PosTTF.TabIndex = 1;
            // 
            // Grp_UploadImage
            // 
            this.Grp_UploadImage.Controls.Add(this.Btn_HalfToneImage);
            this.Grp_UploadImage.Controls.Add(this.Btn_Upload_Ext);
            this.Grp_UploadImage.Controls.Add(this.Btn_Upload_Int);
            this.Grp_UploadImage.Location = new System.Drawing.Point(547, 162);
            this.Grp_UploadImage.Name = "Grp_UploadImage";
            this.Grp_UploadImage.Size = new System.Drawing.Size(222, 141);
            this.Grp_UploadImage.TabIndex = 24;
            this.Grp_UploadImage.TabStop = false;
            this.Grp_UploadImage.Text = " Upload Image ";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 532);
            this.Controls.Add(this.Grp_UploadImage);
            this.Controls.Add(this.Grp_UploadTTF);
            this.Controls.Add(this.Grp_Demo);
            this.Controls.Add(this.Grp_Command);
            this.Controls.Add(this.Grp_Label);
            this.Controls.Add(this.Grp_Communication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EZio32 Demo C#";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Grp_Communication.ResumeLayout(false);
            this.Grp_Communication.PerformLayout();
            this.Grp_Label.ResumeLayout(false);
            this.Grp_Label.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Page)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Copy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Dark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_GapFeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Label_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Label_W)).EndInit();
            this.Grp_Command.ResumeLayout(false);
            this.Grp_Command.PerformLayout();
            this.Grp_Demo.ResumeLayout(false);
            this.Grp_Demo.PerformLayout();
            this.Grp_UploadTTF.ResumeLayout(false);
            this.Grp_UploadImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_PrintText;
        private System.Windows.Forms.Button Btn_Upload_Int;
        private System.Windows.Forms.Button Btn_UploadTextImage;
        private System.Windows.Forms.Button Btn_PrintImage;
        private System.Windows.Forms.Button Btn_HalfToneImage;
        private System.Windows.Forms.Button Btn_SendByte;
        private System.Windows.Forms.Button Btn_GetVersion;
        private System.Windows.Forms.Button Btn_Execute;
        private System.Windows.Forms.Button Btn_Upload_Ext;
        private System.Windows.Forms.ComboBox Cbo_COM;
        private System.Windows.Forms.TextBox Txt_Baud;
        private System.Windows.Forms.GroupBox Grp_Communication;
        private System.Windows.Forms.Label Lbl_NetPort;
        private System.Windows.Forms.Label Lbl_Baud;
        private System.Windows.Forms.ComboBox Cbo_LPT;
        private System.Windows.Forms.TextBox Txt_NetPort;
        private System.Windows.Forms.TextBox Txt_IP;
        private System.Windows.Forms.RadioButton RBtn_NET;
        private System.Windows.Forms.RadioButton RBtn_Driver;
        private System.Windows.Forms.RadioButton RBtn_LPT;
        private System.Windows.Forms.RadioButton RBtn_COM;
        private System.Windows.Forms.RadioButton RBtn_USB;
        private System.Windows.Forms.ComboBox Cbo_Driver;
        private System.Windows.Forms.GroupBox Grp_Label;
        private System.Windows.Forms.ComboBox Cbo_PaperType;
        private System.Windows.Forms.Label Lbl_Label_W;
        private System.Windows.Forms.Label Lbl_Label_H;
        private System.Windows.Forms.Label Lbl_PaperType;
        private System.Windows.Forms.Label Lbl_GapFeed;
        private System.Windows.Forms.Label Lbl_Page;
        private System.Windows.Forms.Label Lbl_Speed;
        private System.Windows.Forms.Label Lbl_Copy;
        private System.Windows.Forms.Label Lbl_Dark;
        private System.Windows.Forms.Button Btn_AutoSensing;
        private System.Windows.Forms.ComboBox Cbo_Cmd;
        private System.Windows.Forms.GroupBox Grp_Command;
        private System.Windows.Forms.TextBox Txt_Cmd;
        private System.Windows.Forms.TextBox Txt_Reply;
        private System.Windows.Forms.GroupBox Grp_Demo;
        private System.Windows.Forms.Button Btn_PrintCode39;
        private System.Windows.Forms.Button Btn_DataMatrix;
        private System.Windows.Forms.Button Btn_Maxicode;
        private System.Windows.Forms.Button Btn_Aztec;
        private System.Windows.Forms.Button Btn_PDF417;
        private System.Windows.Forms.Button Btn_QRCode;
        private System.Windows.Forms.NumericUpDown Num_Page;
        private System.Windows.Forms.NumericUpDown Num_Speed;
        private System.Windows.Forms.NumericUpDown Num_Copy;
        private System.Windows.Forms.NumericUpDown Num_Dark;
        private System.Windows.Forms.NumericUpDown Num_GapFeed;
        private System.Windows.Forms.NumericUpDown Num_Label_H;
        private System.Windows.Forms.NumericUpDown Num_Label_W;
        private System.Windows.Forms.GroupBox Grp_UploadTTF;
        private System.Windows.Forms.Button Btn_TTF;
        private System.Windows.Forms.ComboBox Cbo_PosTTF;
        private System.Windows.Forms.ProgressBar Prog_TTF;
        private System.Windows.Forms.TextBox Txt_QRcode_Data;
        private System.Windows.Forms.ComboBox Cbo_QR_Encoding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Grp_UploadImage;
        private System.Windows.Forms.Button Btn_PrintShape;
        private System.Windows.Forms.Button Btn_PrintHalftoneImg;
        private System.Windows.Forms.Button Btn_TrueTypeFont;
        private System.Windows.Forms.Button Btn_BitmappedFont;
        private System.Windows.Forms.Button Btn_AsiaFont;
        private System.Windows.Forms.Button Btn_InternalFont;
    }
}

