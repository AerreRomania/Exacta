namespace Exacta
{
    partial class Intervals
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Intervals));
            this.dgvIntervals = new System.Windows.Forms.DataGridView();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCuts = new System.Windows.Forms.Button();
            this.btnKnitt = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPrep = new System.Windows.Forms.Button();
            this.pbExcel = new System.Windows.Forms.PictureBox();
            this.pbPdf = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntervals)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPdf)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvIntervals
            // 
            this.dgvIntervals.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvIntervals.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIntervals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIntervals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIntervals.Location = new System.Drawing.Point(3, 63);
            this.dgvIntervals.Name = "dgvIntervals";
            this.dgvIntervals.Size = new System.Drawing.Size(950, 221);
            this.dgvIntervals.TabIndex = 0;
            this.dgvIntervals.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIntervals_CellDoubleClick);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 3);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(706, 140);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgvIntervals, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(956, 439);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.zedGraphControl1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.zedGraphControl2, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 290);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(950, 146);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl2.Location = new System.Drawing.Point(715, 3);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(232, 140);
            this.zedGraphControl2.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel2.Controls.Add(this.btnCuts, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnKnitt, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnStop, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnPrep, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbExcel, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbPdf, 6, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(950, 54);
            this.tableLayoutPanel2.TabIndex = 2;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // btnCuts
            // 
            this.btnCuts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCuts.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCuts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCuts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCuts.Location = new System.Drawing.Point(245, 31);
            this.btnCuts.Margin = new System.Windows.Forms.Padding(1);
            this.btnCuts.Name = "btnCuts";
            this.btnCuts.Size = new System.Drawing.Size(77, 22);
            this.btnCuts.TabIndex = 62;
            this.btnCuts.Text = "Qty";
            this.btnCuts.UseVisualStyleBackColor = false;
            this.btnCuts.Click += new System.EventHandler(this.btnCuts_Click);
            // 
            // btnKnitt
            // 
            this.btnKnitt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKnitt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnKnitt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKnitt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKnitt.Location = new System.Drawing.Point(2, 31);
            this.btnKnitt.Margin = new System.Windows.Forms.Padding(1);
            this.btnKnitt.Name = "btnKnitt";
            this.btnKnitt.Size = new System.Drawing.Size(77, 22);
            this.btnKnitt.TabIndex = 61;
            this.btnKnitt.Text = "Knitt";
            this.btnKnitt.UseVisualStyleBackColor = false;
            this.btnKnitt.Click += new System.EventHandler(this.btnKnitt_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(163, 31);
            this.btnStop.Margin = new System.Windows.Forms.Padding(1);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(77, 22);
            this.btnStop.TabIndex = 63;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPrep
            // 
            this.btnPrep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrep.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrep.Location = new System.Drawing.Point(83, 31);
            this.btnPrep.Margin = new System.Windows.Forms.Padding(1);
            this.btnPrep.Name = "btnPrep";
            this.btnPrep.Size = new System.Drawing.Size(77, 22);
            this.btnPrep.TabIndex = 60;
            this.btnPrep.Text = "Prep";
            this.btnPrep.UseVisualStyleBackColor = false;
            this.btnPrep.Click += new System.EventHandler(this.btnPrep_Click);
            // 
            // pbExcel
            // 
            this.pbExcel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExcel.Image = ((System.Drawing.Image)(resources.GetObject("pbExcel.Image")));
            this.pbExcel.Location = new System.Drawing.Point(898, 3);
            this.pbExcel.Name = "pbExcel";
            this.pbExcel.Size = new System.Drawing.Size(49, 48);
            this.pbExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbExcel.TabIndex = 68;
            this.pbExcel.TabStop = false;
            this.pbExcel.Click += new System.EventHandler(this.pbExcel_Click_1);
            this.pbExcel.MouseEnter += new System.EventHandler(this.pbExcel_MouseEnter);
            this.pbExcel.MouseLeave += new System.EventHandler(this.pbExcel_MouseLeave);
            // 
            // pbPdf
            // 
            this.pbPdf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPdf.Image = ((System.Drawing.Image)(resources.GetObject("pbPdf.Image")));
            this.pbPdf.Location = new System.Drawing.Point(840, 3);
            this.pbPdf.Name = "pbPdf";
            this.pbPdf.Size = new System.Drawing.Size(51, 48);
            this.pbPdf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPdf.TabIndex = 69;
            this.pbPdf.TabStop = false;
            this.pbPdf.Click += new System.EventHandler(this.pbPdf_Click);
            this.pbPdf.MouseEnter += new System.EventHandler(this.pbPdf_MouseEnter);
            this.pbPdf.MouseLeave += new System.EventHandler(this.pbPdf_MouseLeave);
            // 
            // Intervals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(956, 439);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Intervals";
            this.Text = "Intervals";
            this.Load += new System.EventHandler(this.Intervals_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntervals)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPdf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvIntervals;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCuts;
        private System.Windows.Forms.Button btnKnitt;
        private System.Windows.Forms.Button btnPrep;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.PictureBox pbExcel;
        private System.Windows.Forms.PictureBox pbPdf;
        // System.Windows.Forms.Button btnExcel;
    }
}