namespace Exacta
{
    partial class Efficiency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Efficiency));
            this.dgvEfficiency = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pbExcel = new System.Windows.Forms.PictureBox();
            this.pbPdf = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEfficiency)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPdf)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEfficiency
            // 
            this.dgvEfficiency.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvEfficiency.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEfficiency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEfficiency.Location = new System.Drawing.Point(3, 63);
            this.dgvEfficiency.Name = "dgvEfficiency";
            this.dgvEfficiency.Size = new System.Drawing.Size(794, 228);
            this.dgvEfficiency.TabIndex = 0;
            this.dgvEfficiency.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvEfficiency_CellPainting);
            this.dgvEfficiency.SelectionChanged += new System.EventHandler(this.dgvEfficiency_SelectionChanged);
            this.dgvEfficiency.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvEfficiency_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvEfficiency, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 2;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.zedGraphControl1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.zedGraphControl2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 297);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 150);
            this.tableLayoutPanel2.TabIndex = 1;
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
            this.zedGraphControl1.Size = new System.Drawing.Size(589, 144);
            this.zedGraphControl1.TabIndex = 2;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl2.Location = new System.Drawing.Point(598, 3);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(193, 144);
            this.zedGraphControl2.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.82116F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.178841F));
            this.tableLayoutPanel3.Controls.Add(this.pbExcel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pbPdf, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(794, 54);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // pbExcel
            // 
            this.pbExcel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExcel.Image = ((System.Drawing.Image)(resources.GetObject("pbExcel.Image")));
            this.pbExcel.Location = new System.Drawing.Point(742, 3);
            this.pbExcel.Name = "pbExcel";
            this.pbExcel.Size = new System.Drawing.Size(49, 48);
            this.pbExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbExcel.TabIndex = 72;
            this.pbExcel.TabStop = false;
            this.pbExcel.Click += new System.EventHandler(this.pbExcel_Click);
            this.pbExcel.MouseEnter += new System.EventHandler(this.pbExcel_MouseEnter);
            this.pbExcel.MouseLeave += new System.EventHandler(this.pbExcel_MouseLeave);
            // 
            // pbPdf
            // 
            this.pbPdf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPdf.Image = ((System.Drawing.Image)(resources.GetObject("pbPdf.Image")));
            this.pbPdf.Location = new System.Drawing.Point(685, 3);
            this.pbPdf.Name = "pbPdf";
            this.pbPdf.Size = new System.Drawing.Size(49, 48);
            this.pbPdf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPdf.TabIndex = 71;
            this.pbPdf.TabStop = false;
            this.pbPdf.Click += new System.EventHandler(this.pbPdf_Click);
            this.pbPdf.MouseEnter += new System.EventHandler(this.pbPdf_MouseEnter);
            this.pbPdf.MouseLeave += new System.EventHandler(this.pbPdf_MouseLeave);
            // 
            // Efficiency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Efficiency";
            this.Text = "Efficiency";
            this.Load += new System.EventHandler(this.Efficiency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEfficiency)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPdf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEfficiency;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox pbPdf;
        private System.Windows.Forms.PictureBox pbExcel;
    }
}