
namespace 灵犀_串口助手
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkblank = new System.Windows.Forms.CheckBox();
            this.rtbcmds = new System.Windows.Forms.RichTextBox();
            this.chkfindcmd = new System.Windows.Forms.CheckBox();
            this.btnclear = new CCWin.SkinControl.SkinButton();
            this.chkautoclear = new System.Windows.Forms.CheckBox();
            this.chksave = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbfilter = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnselcancel = new CCWin.SkinControl.SkinButton();
            this.btnselall = new CCWin.SkinControl.SkinButton();
            this.chkfilter = new System.Windows.Forms.CheckBox();
            this.sbtnstop = new CCWin.SkinControl.SkinButton();
            this.sbtnstart = new CCWin.SkinControl.SkinButton();
            this.label6 = new System.Windows.Forms.Label();
            this.rtbinfo = new System.Windows.Forms.RichTextBox();
            this.cbbstop = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbpb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbbit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbbr = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbname = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fctb = new FastColoredTextBoxNS.FastColoredTextBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.group3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbrtbsend = new System.Windows.Forms.ComboBox();
            this.btnsend = new CCWin.SkinControl.SkinButton();
            this.lstcmds = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.group3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(951, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkblank);
            this.groupBox1.Controls.Add(this.rtbcmds);
            this.groupBox1.Controls.Add(this.chkfindcmd);
            this.groupBox1.Controls.Add(this.btnclear);
            this.groupBox1.Controls.Add(this.chkautoclear);
            this.groupBox1.Controls.Add(this.chksave);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.sbtnstop);
            this.groupBox1.Controls.Add(this.sbtnstart);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.rtbinfo);
            this.groupBox1.Controls.Add(this.cbbstop);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbbpb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbbit);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbbbr);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbbname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(590, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 603);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            // 
            // chkblank
            // 
            this.chkblank.AutoSize = true;
            this.chkblank.Location = new System.Drawing.Point(139, 246);
            this.chkblank.Name = "chkblank";
            this.chkblank.Size = new System.Drawing.Size(84, 16);
            this.chkblank.TabIndex = 19;
            this.chkblank.Text = "不显示数据";
            this.chkblank.UseVisualStyleBackColor = true;
            // 
            // rtbcmds
            // 
            this.rtbcmds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtbcmds.ForeColor = System.Drawing.Color.Lime;
            this.rtbcmds.Location = new System.Drawing.Point(115, 283);
            this.rtbcmds.Name = "rtbcmds";
            this.rtbcmds.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbcmds.Size = new System.Drawing.Size(96, 89);
            this.rtbcmds.TabIndex = 10;
            this.rtbcmds.Text = "命令列表：";
            // 
            // chkfindcmd
            // 
            this.chkfindcmd.AutoSize = true;
            this.chkfindcmd.Checked = true;
            this.chkfindcmd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkfindcmd.Location = new System.Drawing.Point(115, 267);
            this.chkfindcmd.Name = "chkfindcmd";
            this.chkfindcmd.Size = new System.Drawing.Size(72, 16);
            this.chkfindcmd.TabIndex = 17;
            this.chkfindcmd.Text = "抽取命令";
            this.chkfindcmd.UseVisualStyleBackColor = true;
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnclear.BaseColor = System.Drawing.Color.SteelBlue;
            this.btnclear.BorderColor = System.Drawing.Color.Green;
            this.btnclear.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnclear.DownBack = null;
            this.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnclear.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnclear.ForeColor = System.Drawing.Color.Navy;
            this.btnclear.GlowColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnclear.Location = new System.Drawing.Point(84, 242);
            this.btnclear.MouseBack = null;
            this.btnclear.Name = "btnclear";
            this.btnclear.NormlBack = null;
            this.btnclear.Size = new System.Drawing.Size(44, 22);
            this.btnclear.TabIndex = 16;
            this.btnclear.Text = "清空";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // chkautoclear
            // 
            this.chkautoclear.AutoSize = true;
            this.chkautoclear.Checked = true;
            this.chkautoclear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkautoclear.Location = new System.Drawing.Point(12, 246);
            this.chkautoclear.Name = "chkautoclear";
            this.chkautoclear.Size = new System.Drawing.Size(72, 16);
            this.chkautoclear.TabIndex = 15;
            this.chkautoclear.Text = "自动清除";
            this.chkautoclear.UseVisualStyleBackColor = true;
            // 
            // chksave
            // 
            this.chksave.AutoSize = true;
            this.chksave.Location = new System.Drawing.Point(11, 228);
            this.chksave.Name = "chksave";
            this.chksave.Size = new System.Drawing.Size(150, 16);
            this.chksave.TabIndex = 8;
            this.chksave.Text = "保存数据文件(RXD.log)";
            this.chksave.UseVisualStyleBackColor = true;
            this.chksave.CheckedChanged += new System.EventHandler(this.chksave_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbfilter);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Location = new System.Drawing.Point(14, 378);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 182);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据过滤";
            // 
            // tbfilter
            // 
            this.tbfilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbfilter.Location = new System.Drawing.Point(3, 43);
            this.tbfilter.Multiline = true;
            this.tbfilter.Name = "tbfilter";
            this.tbfilter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbfilter.Size = new System.Drawing.Size(194, 136);
            this.tbfilter.TabIndex = 15;
            this.tbfilter.Text = "AA 55 05 80*CC 33 C3 3C\r\nAA 55 05 50*CC 33 C3 3C\r\nAA 55 05 51*CC 33 C3 3C\r\nAA 55 " +
    "05 52*CC 33 C3 3C\r\nAA 55 05 53*CC 33 C3 3C\r\nAA 55 05 54*CC 33 C3 3C";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnselcancel);
            this.panel1.Controls.Add(this.btnselall);
            this.panel1.Controls.Add(this.chkfilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 26);
            this.panel1.TabIndex = 16;
            // 
            // btnselcancel
            // 
            this.btnselcancel.BackColor = System.Drawing.Color.Transparent;
            this.btnselcancel.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnselcancel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnselcancel.DownBack = null;
            this.btnselcancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnselcancel.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnselcancel.Location = new System.Drawing.Point(105, 2);
            this.btnselcancel.MouseBack = null;
            this.btnselcancel.Name = "btnselcancel";
            this.btnselcancel.NormlBack = null;
            this.btnselcancel.Size = new System.Drawing.Size(36, 23);
            this.btnselcancel.TabIndex = 17;
            this.btnselcancel.Text = "反选";
            this.btnselcancel.UseVisualStyleBackColor = false;
            this.btnselcancel.Click += new System.EventHandler(this.btnselcancel_Click);
            // 
            // btnselall
            // 
            this.btnselall.BackColor = System.Drawing.Color.Transparent;
            this.btnselall.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnselall.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnselall.DownBack = null;
            this.btnselall.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnselall.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnselall.Location = new System.Drawing.Point(58, 2);
            this.btnselall.MouseBack = null;
            this.btnselall.Name = "btnselall";
            this.btnselall.NormlBack = null;
            this.btnselall.Size = new System.Drawing.Size(36, 23);
            this.btnselall.TabIndex = 16;
            this.btnselall.Text = "全选";
            this.btnselall.UseVisualStyleBackColor = false;
            this.btnselall.Click += new System.EventHandler(this.btnselall_Click);
            // 
            // chkfilter
            // 
            this.chkfilter.AutoSize = true;
            this.chkfilter.Location = new System.Drawing.Point(6, 6);
            this.chkfilter.Name = "chkfilter";
            this.chkfilter.Size = new System.Drawing.Size(48, 16);
            this.chkfilter.TabIndex = 15;
            this.chkfilter.Text = "过滤";
            this.chkfilter.UseVisualStyleBackColor = true;
            // 
            // sbtnstop
            // 
            this.sbtnstop.BackColor = System.Drawing.Color.Transparent;
            this.sbtnstop.BaseColor = System.Drawing.Color.Red;
            this.sbtnstop.BorderColor = System.Drawing.Color.Red;
            this.sbtnstop.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.sbtnstop.DownBack = null;
            this.sbtnstop.ForeColor = System.Drawing.Color.White;
            this.sbtnstop.Location = new System.Drawing.Point(115, 176);
            this.sbtnstop.MouseBack = null;
            this.sbtnstop.Name = "sbtnstop";
            this.sbtnstop.NormlBack = null;
            this.sbtnstop.Size = new System.Drawing.Size(75, 46);
            this.sbtnstop.TabIndex = 7;
            this.sbtnstop.Text = "停  止";
            this.sbtnstop.UseVisualStyleBackColor = false;
            this.sbtnstop.Click += new System.EventHandler(this.sbtnstop_Click);
            // 
            // sbtnstart
            // 
            this.sbtnstart.BackColor = System.Drawing.Color.Transparent;
            this.sbtnstart.BaseColor = System.Drawing.Color.Lime;
            this.sbtnstart.BorderColor = System.Drawing.Color.Green;
            this.sbtnstart.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.sbtnstart.DownBack = null;
            this.sbtnstart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sbtnstart.ForeColor = System.Drawing.Color.Black;
            this.sbtnstart.Location = new System.Drawing.Point(14, 176);
            this.sbtnstart.MouseBack = null;
            this.sbtnstart.Name = "sbtnstart";
            this.sbtnstart.NormlBack = null;
            this.sbtnstart.Size = new System.Drawing.Size(75, 46);
            this.sbtnstart.TabIndex = 6;
            this.sbtnstart.Text = "开  始";
            this.sbtnstart.UseVisualStyleBackColor = false;
            this.sbtnstart.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "提示信息";
            // 
            // rtbinfo
            // 
            this.rtbinfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtbinfo.ForeColor = System.Drawing.Color.Lime;
            this.rtbinfo.Location = new System.Drawing.Point(12, 283);
            this.rtbinfo.Name = "rtbinfo";
            this.rtbinfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbinfo.Size = new System.Drawing.Size(96, 89);
            this.rtbinfo.TabIndex = 9;
            this.rtbinfo.Text = "";
            // 
            // cbbstop
            // 
            this.cbbstop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbstop.Enabled = false;
            this.cbbstop.FormattingEnabled = true;
            this.cbbstop.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cbbstop.Location = new System.Drawing.Point(71, 139);
            this.cbbstop.Name = "cbbstop";
            this.cbbstop.Size = new System.Drawing.Size(121, 20);
            this.cbbstop.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位";
            // 
            // cbbpb
            // 
            this.cbbpb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbpb.Enabled = false;
            this.cbbpb.FormattingEnabled = true;
            this.cbbpb.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd",
            "Mark",
            "Space"});
            this.cbbpb.Location = new System.Drawing.Point(71, 113);
            this.cbbpb.Name = "cbbpb";
            this.cbbpb.Size = new System.Drawing.Size(121, 20);
            this.cbbpb.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "校验位";
            // 
            // cbbbit
            // 
            this.cbbbit.FormattingEnabled = true;
            this.cbbbit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbbbit.Location = new System.Drawing.Point(71, 87);
            this.cbbbit.Name = "cbbbit";
            this.cbbbit.Size = new System.Drawing.Size(121, 20);
            this.cbbbit.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据位";
            // 
            // cbbbr
            // 
            this.cbbbr.FormattingEnabled = true;
            this.cbbbr.Items.AddRange(new object[] {
            "4800",
            "9600",
            "57600",
            "115200"});
            this.cbbbr.Location = new System.Drawing.Point(71, 61);
            this.cbbbr.Name = "cbbbr";
            this.cbbbr.Size = new System.Drawing.Size(121, 20);
            this.cbbbr.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率";
            // 
            // cbbname
            // 
            this.cbbname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbname.FormattingEnabled = true;
            this.cbbname.Location = new System.Drawing.Point(71, 35);
            this.cbbname.Name = "cbbname";
            this.cbbname.Size = new System.Drawing.Size(278, 20);
            this.cbbname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口名称";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(587, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 603);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fctb);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 603);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据接收区域";
            // 
            // fctb
            // 
            this.fctb.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctb.AutoIndent = false;
            this.fctb.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*" +
    "(?<range>:)\\s*(?<range>[^;]+);";
            this.fctb.AutoScrollMinSize = new System.Drawing.Size(0, 34);
            this.fctb.BackBrush = null;
            this.fctb.CharHeight = 16;
            this.fctb.CharWidth = 9;
            this.fctb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctb.DelayedEventsInterval = 1000;
            this.fctb.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctb.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.fctb.IsReplaceMode = false;
            this.fctb.LeftBracket = '(';
            this.fctb.LeftPadding = 3;
            this.fctb.LineNumberColor = System.Drawing.Color.Green;
            this.fctb.Location = new System.Drawing.Point(3, 17);
            this.fctb.Name = "fctb";
            this.fctb.PaddingBackColor = System.Drawing.Color.WhiteSmoke;
            this.fctb.Paddings = new System.Windows.Forms.Padding(1);
            this.fctb.PreferredLineWidth = 500;
            this.fctb.RightBracket = ')';
            this.fctb.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctb.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctb.ServiceColors")));
            this.fctb.ShowFoldingLines = true;
            this.fctb.Size = new System.Drawing.Size(584, 583);
            this.fctb.TabIndex = 3;
            this.fctb.Text = "\r\n";
            this.fctb.TextAreaBorder = FastColoredTextBoxNS.TextAreaBorderType.Single;
            this.fctb.WordWrap = true;
            this.fctb.Zoom = 100;
            this.fctb.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fctb_TextChanged);
            this.fctb.TextChanging += new System.EventHandler<FastColoredTextBoxNS.TextChangingEventArgs>(this.fctb_TextChanging);
            this.fctb.VisibleRangeChangedDelayed += new System.EventHandler(this.fctb_VisibleRangeChangedDelayed);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // group3
            // 
            this.group3.Controls.Add(this.label7);
            this.group3.Controls.Add(this.cbrtbsend);
            this.group3.Controls.Add(this.btnsend);
            this.group3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.group3.Location = new System.Drawing.Point(0, 778);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(951, 49);
            this.group3.TabIndex = 4;
            this.group3.TabStop = false;
            this.group3.Text = "命令发送";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "文本内容";
            // 
            // cbrtbsend
            // 
            this.cbrtbsend.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbrtbsend.FormattingEnabled = true;
            this.cbrtbsend.Items.AddRange(new object[] {
            "左管连接仪器命令-AA 55 05 20 00 00 00 00 25 CC 33 C3 3C",
            "左管零点校正-AA 55 05 00 00 00 00 00 05 CC 33 C3 3C",
            "左管体积校正-AA 55 05 01 00 00 00 00 04 CC 33 C3 3C",
            "左管启动实验-AA 55 05 02 00 00 00 00 07 CC 33 C3 3C",
            "左管停止实验-AA 55 05 03 00 00 00 00 06 CC 33 C3 3C",
            "右管连接仪器命令-AA 55 05 20 00 00 00 00 25 CC 33 C3 3C",
            "右管零点校正-AA 55 05 10 00 00 00 00 15 CC 33 C3 3C",
            "右管体积校正-AA 55 05 11 00 00 00 00 14 CC 33 C3 3C",
            "右管启动实验-AA 55 05 12 00 00 00 00 17 CC 33 C3 3C",
            "右管停止实验-AA 55 05 13 00 00 00 00 16 CC 33 C3 3C",
            resources.GetString("cbrtbsend.Items"),
            "42 00 00 80 3F 00 00 80 3F 00 00 00 40 00 00 C8 43 00 00 00 00 00 00 80 3F 00 00 " +
                "00 00 A6 CC 33 C3 3C",
            resources.GetString("cbrtbsend.Items1"),
            "42 00 00 80 3F 00 00 80 3F 00 00 00 40 00 00 C8 43 00 00 00 00 00 00 80 3F 00 00 " +
                "00 00 B6 CC 33 C3 3C"});
            this.cbrtbsend.Location = new System.Drawing.Point(75, 18);
            this.cbrtbsend.MaxDropDownItems = 80;
            this.cbrtbsend.Name = "cbrtbsend";
            this.cbrtbsend.Size = new System.Drawing.Size(685, 22);
            this.cbrtbsend.TabIndex = 12;
            this.cbrtbsend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbrtbsend_KeyDown);
            // 
            // btnsend
            // 
            this.btnsend.BackColor = System.Drawing.Color.Transparent;
            this.btnsend.BaseColor = System.Drawing.Color.Green;
            this.btnsend.BorderColor = System.Drawing.Color.Green;
            this.btnsend.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnsend.DownBack = null;
            this.btnsend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnsend.ForeColor = System.Drawing.Color.White;
            this.btnsend.Location = new System.Drawing.Point(778, 18);
            this.btnsend.MouseBack = null;
            this.btnsend.Name = "btnsend";
            this.btnsend.NormlBack = null;
            this.btnsend.Size = new System.Drawing.Size(54, 25);
            this.btnsend.TabIndex = 13;
            this.btnsend.Text = "发 送";
            this.btnsend.UseVisualStyleBackColor = false;
            this.btnsend.Click += new System.EventHandler(this.btnsend_Click);
            // 
            // lstcmds
            // 
            this.lstcmds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3});
            this.lstcmds.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstcmds.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstcmds.FullRowSelect = true;
            this.lstcmds.GridLines = true;
            this.lstcmds.HideSelection = false;
            this.lstcmds.Location = new System.Drawing.Point(0, 631);
            this.lstcmds.MultiSelect = false;
            this.lstcmds.Name = "lstcmds";
            this.lstcmds.Size = new System.Drawing.Size(951, 147);
            this.lstcmds.TabIndex = 21;
            this.lstcmds.UseCompatibleStateImageBehavior = false;
            this.lstcmds.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "命令";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "含    义";
            this.columnHeader4.Width = 300;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "数值";
            this.columnHeader3.Width = 400;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 628);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(951, 3);
            this.splitter2.TabIndex = 22;
            this.splitter2.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 827);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.lstcmds);
            this.Controls.Add(this.group3);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "灵犀串口助手";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbbstop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbpb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbbit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbbr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.RichTextBox rtbinfo;
        public FastColoredTextBoxNS.FastColoredTextBox fctb;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        public CCWin.SkinControl.SkinButton sbtnstop;
        public CCWin.SkinControl.SkinButton sbtnstart;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox tbfilter;
        private System.Windows.Forms.CheckBox chksave;
        private System.Windows.Forms.CheckBox chkautoclear;
        public CCWin.SkinControl.SkinButton btnclear;
        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinButton btnselcancel;
        private CCWin.SkinControl.SkinButton btnselall;
        private System.Windows.Forms.CheckBox chkfilter;
        private System.Windows.Forms.CheckBox chkfindcmd;
        public System.Windows.Forms.RichTextBox rtbcmds;
        private System.Windows.Forms.CheckBox chkblank;
        private System.Windows.Forms.GroupBox group3;
        private System.Windows.Forms.ComboBox cbrtbsend;
        public CCWin.SkinControl.SkinButton btnsend;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ListView lstcmds;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

