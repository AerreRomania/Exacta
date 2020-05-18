namespace Exacta
    {
    partial class Production
        {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
            {
            if (disposing && (components != null))
                {
                components.Dispose();
                }
            base.Dispose(disposing);
            }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
            {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Production));
            this.btn_ByArticle = new System.Windows.Forms.Button();
            this.btn_AllRecords = new System.Windows.Forms.Button();
            this.dgv_Time = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.cbArt = new System.Windows.Forms.ComboBox();
            this.cbPh = new System.Windows.Forms.ComboBox();
            this.cbLine = new System.Windows.Forms.ComboBox();
            this.btnSortByLine = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbPdf = new System.Windows.Forms.PictureBox();
            this.pbExcel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Time)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ByArticle
            // 
            this.btn_ByArticle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ByArticle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_ByArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ByArticle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btn_ByArticle.Location = new System.Drawing.Point(124, 28);
            this.btn_ByArticle.Margin = new System.Windows.Forms.Padding(1);
            this.btn_ByArticle.Name = "btn_ByArticle";
            this.btn_ByArticle.Size = new System.Drawing.Size(77, 25);
            this.btn_ByArticle.TabIndex = 58;
            this.btn_ByArticle.Text = "ByArticle";
            this.btn_ByArticle.UseVisualStyleBackColor = false;
            this.btn_ByArticle.Click += new System.EventHandler(this.btn_ByArticle_Click);
            // 
            // btn_AllRecords
            // 
            this.btn_AllRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AllRecords.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_AllRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AllRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btn_AllRecords.Location = new System.Drawing.Point(1, 28);
            this.btn_AllRecords.Margin = new System.Windows.Forms.Padding(1);
            this.btn_AllRecords.Name = "btn_AllRecords";
            this.btn_AllRecords.Size = new System.Drawing.Size(99, 25);
            this.btn_AllRecords.TabIndex = 57;
            this.btn_AllRecords.Text = "AllRecords";
            this.btn_AllRecords.UseVisualStyleBackColor = false;
            this.btn_AllRecords.Click += new System.EventHandler(this.btn_AllRecords_Click);
            // 
            // dgv_Time
            // 
            this.dgv_Time.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Time.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgv_Time.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Time.Location = new System.Drawing.Point(1, 61);
            this.dgv_Time.Margin = new System.Windows.Forms.Padding(1);
            this.dgv_Time.Name = "dgv_Time";
            this.dgv_Time.RowHeadersWidth = 35;
            this.dgv_Time.RowTemplate.Height = 46;
            this.dgv_Time.Size = new System.Drawing.Size(1039, 303);
            this.dgv_Time.TabIndex = 0;
            this.dgv_Time.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_Time_CellPainting);
            this.dgv_Time.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_Time_DataBindingComplete);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(203, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 25);
            this.button1.TabIndex = 59;
            this.button1.Text = "Pieces";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbArt
            // 
            this.cbArt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbArt.FormattingEnabled = true;
            this.cbArt.Location = new System.Drawing.Point(477, 30);
            this.cbArt.Name = "cbArt";
            this.cbArt.Size = new System.Drawing.Size(110, 21);
            this.cbArt.TabIndex = 62;
            this.cbArt.SelectedValueChanged += new System.EventHandler(this.cbArt_SelectedValueChanged);
            // 
            // cbPh
            // 
            this.cbPh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPh.FormattingEnabled = true;
            this.cbPh.Location = new System.Drawing.Point(326, 30);
            this.cbPh.Name = "cbPh";
            this.cbPh.Size = new System.Drawing.Size(110, 21);
            this.cbPh.TabIndex = 63;
            this.cbPh.SelectedValueChanged += new System.EventHandler(this.cbPh_SelectedValueChanged);
            // 
            // cbLine
            // 
            this.cbLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLine.FormattingEnabled = true;
            this.cbLine.Location = new System.Drawing.Point(627, 30);
            this.cbLine.Name = "cbLine";
            this.cbLine.Size = new System.Drawing.Size(110, 21);
            this.cbLine.TabIndex = 64;
            this.cbLine.SelectedValueChanged += new System.EventHandler(this.cbLine_SelectedValueChanged);
            // 
            // btnSortByLine
            // 
            this.btnSortByLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSortByLine.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSortByLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortByLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnSortByLine.Location = new System.Drawing.Point(741, 28);
            this.btnSortByLine.Margin = new System.Windows.Forms.Padding(1);
            this.btnSortByLine.Name = "btnSortByLine";
            this.btnSortByLine.Size = new System.Drawing.Size(85, 25);
            this.btnSortByLine.TabIndex = 65;
            this.btnSortByLine.Text = "SortByLine";
            this.btnSortByLine.UseVisualStyleBackColor = false;
            this.btnSortByLine.Click += new System.EventHandler(this.btnSortByLine_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgv_Time, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1041, 569);
            this.tableLayoutPanel1.TabIndex = 66;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 368);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(1035, 198);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 12;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel2.Controls.Add(this.btn_AllRecords, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_ByArticle, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSortByLine, 9, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbLine, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbArt, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbPh, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbPdf, 10, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbExcel, 11, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1035, 54);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(442, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 23);
            this.label1.TabIndex = 66;
            this.label1.Text = "Art.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(593, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 23);
            this.label3.TabIndex = 68;
            this.label3.Text = "Lin.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(284, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 23);
            this.label2.TabIndex = 67;
            this.label2.Text = "Ph.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbPdf
            // 
            this.pbPdf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPdf.Image = ((System.Drawing.Image)(resources.GetObject("pbPdf.Image")));
            this.pbPdf.Location = new System.Drawing.Point(927, 3);
            this.pbPdf.Name = "pbPdf";
            this.pbPdf.Size = new System.Drawing.Size(49, 48);
            this.pbPdf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPdf.TabIndex = 70;
            this.pbPdf.TabStop = false;
            this.pbPdf.Click += new System.EventHandler(this.pbPdf_Click);
            this.pbPdf.MouseEnter += new System.EventHandler(this.pbPdf_MouseEnter);
            this.pbPdf.MouseLeave += new System.EventHandler(this.pbPdf_MouseLeave);
            // 
            // pbExcel
            // 
            this.pbExcel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExcel.Image = ((System.Drawing.Image)(resources.GetObject("pbExcel.Image")));
            this.pbExcel.Location = new System.Drawing.Point(983, 3);
            this.pbExcel.Name = "pbExcel";
            this.pbExcel.Size = new System.Drawing.Size(49, 48);
            this.pbExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbExcel.TabIndex = 71;
            this.pbExcel.TabStop = false;
            this.pbExcel.Click += new System.EventHandler(this.pbExcel_Click);
            this.pbExcel.MouseEnter += new System.EventHandler(this.pbExcel_MouseEnter);
            this.pbExcel.MouseLeave += new System.EventHandler(this.pbExcel_MouseLeave);
            // 
            // Production
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 569);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Production";
            this.Text = "Production";
            this.Load += new System.EventHandler(this.Production_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Time)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.Button btn_ByArticle;
        private System.Windows.Forms.Button btn_AllRecords;
        private System.Windows.Forms.DataGridView dgv_Time;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbArt;
        private System.Windows.Forms.ComboBox cbPh;
        private System.Windows.Forms.ComboBox cbLine;
        private System.Windows.Forms.Button btnSortByLine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbPdf;
        private System.Windows.Forms.PictureBox pbExcel;
    }
    }