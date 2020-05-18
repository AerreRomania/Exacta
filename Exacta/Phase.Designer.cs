namespace Exacta
    {
    partial class Phase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Phase));
            this.dgvFase = new System.Windows.Forms.DataGridView();
            this.pnl_Edit_Fase = new System.Windows.Forms.Panel();
            this.pbPDF = new System.Windows.Forms.PictureBox();
            this.txt_Fase = new System.Windows.Forms.TextBox();
            this.btn_Delete_Fase = new System.Windows.Forms.Button();
            this.btn_Cancel_Fase = new System.Windows.Forms.Button();
            this.txt_FaseID = new System.Windows.Forms.TextBox();
            this.btn_Save_Fase = new System.Windows.Forms.Button();
            this.btn_Modify_Fase = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Add_Fase = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cbPhase = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFase)).BeginInit();
            this.pnl_Edit_Fase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFase
            // 
            this.dgvFase.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvFase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFase.Location = new System.Drawing.Point(226, 67);
            this.dgvFase.Margin = new System.Windows.Forms.Padding(1);
            this.dgvFase.Name = "dgvFase";
            this.dgvFase.RowTemplate.Height = 46;
            this.dgvFase.Size = new System.Drawing.Size(300, 332);
            this.dgvFase.TabIndex = 23;
            this.dgvFase.SelectionChanged += new System.EventHandler(this.dgvFase_SelectionChanged);
            // 
            // pnl_Edit_Fase
            // 
            this.pnl_Edit_Fase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_Edit_Fase.Controls.Add(this.pbPDF);
            this.pnl_Edit_Fase.Controls.Add(this.txt_Fase);
            this.pnl_Edit_Fase.Controls.Add(this.btn_Delete_Fase);
            this.pnl_Edit_Fase.Controls.Add(this.btn_Cancel_Fase);
            this.pnl_Edit_Fase.Controls.Add(this.txt_FaseID);
            this.pnl_Edit_Fase.Controls.Add(this.btn_Save_Fase);
            this.pnl_Edit_Fase.Controls.Add(this.btn_Modify_Fase);
            this.pnl_Edit_Fase.Controls.Add(this.label7);
            this.pnl_Edit_Fase.Controls.Add(this.btn_Add_Fase);
            this.pnl_Edit_Fase.Controls.Add(this.label8);
            this.pnl_Edit_Fase.Location = new System.Drawing.Point(10, 62);
            this.pnl_Edit_Fase.Margin = new System.Windows.Forms.Padding(1);
            this.pnl_Edit_Fase.Name = "pnl_Edit_Fase";
            this.pnl_Edit_Fase.Size = new System.Drawing.Size(204, 337);
            this.pnl_Edit_Fase.TabIndex = 54;
            this.pnl_Edit_Fase.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Edit_Fase_Paint);
            // 
            // pbPDF
            // 
            this.pbPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPDF.Image = ((System.Drawing.Image)(resources.GetObject("pbPDF.Image")));
            this.pbPDF.Location = new System.Drawing.Point(141, 185);
            this.pbPDF.Name = "pbPDF";
            this.pbPDF.Size = new System.Drawing.Size(48, 48);
            this.pbPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbPDF.TabIndex = 55;
            this.pbPDF.TabStop = false;
            this.pbPDF.Click += new System.EventHandler(this.pbPDF_Click);
            this.pbPDF.MouseEnter += new System.EventHandler(this.pbPDF_MouseEnter);
            this.pbPDF.MouseLeave += new System.EventHandler(this.pbPDF_MouseLeave);
            // 
            // txt_Fase
            // 
            this.txt_Fase.Location = new System.Drawing.Point(73, 46);
            this.txt_Fase.Margin = new System.Windows.Forms.Padding(1);
            this.txt_Fase.Name = "txt_Fase";
            this.txt_Fase.Size = new System.Drawing.Size(116, 20);
            this.txt_Fase.TabIndex = 8;
            // 
            // btn_Delete_Fase
            // 
            this.btn_Delete_Fase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Delete_Fase.BackgroundImage = global::Exacta.Properties.Resources.discard_48;
            this.btn_Delete_Fase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Delete_Fase.Location = new System.Drawing.Point(151, 237);
            this.btn_Delete_Fase.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Delete_Fase.Name = "btn_Delete_Fase";
            this.btn_Delete_Fase.Size = new System.Drawing.Size(38, 41);
            this.btn_Delete_Fase.TabIndex = 52;
            this.btn_Delete_Fase.UseVisualStyleBackColor = false;
            this.btn_Delete_Fase.Click += new System.EventHandler(this.btn_Delete_Fase_Click);
            // 
            // btn_Cancel_Fase
            // 
            this.btn_Cancel_Fase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Cancel_Fase.Location = new System.Drawing.Point(124, 295);
            this.btn_Cancel_Fase.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Cancel_Fase.Name = "btn_Cancel_Fase";
            this.btn_Cancel_Fase.Size = new System.Drawing.Size(65, 25);
            this.btn_Cancel_Fase.TabIndex = 9;
            this.btn_Cancel_Fase.Text = "Cancel";
            this.btn_Cancel_Fase.UseVisualStyleBackColor = false;
            this.btn_Cancel_Fase.Click += new System.EventHandler(this.btn_Cancel_Fase_Click);
            // 
            // txt_FaseID
            // 
            this.txt_FaseID.Location = new System.Drawing.Point(73, 15);
            this.txt_FaseID.Margin = new System.Windows.Forms.Padding(1);
            this.txt_FaseID.Name = "txt_FaseID";
            this.txt_FaseID.Size = new System.Drawing.Size(116, 20);
            this.txt_FaseID.TabIndex = 2;
            // 
            // btn_Save_Fase
            // 
            this.btn_Save_Fase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Save_Fase.Location = new System.Drawing.Point(57, 295);
            this.btn_Save_Fase.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Save_Fase.Name = "btn_Save_Fase";
            this.btn_Save_Fase.Size = new System.Drawing.Size(65, 25);
            this.btn_Save_Fase.TabIndex = 8;
            this.btn_Save_Fase.Text = "Save";
            this.btn_Save_Fase.UseVisualStyleBackColor = false;
            this.btn_Save_Fase.Click += new System.EventHandler(this.btn_Save_Fase_Click);
            // 
            // btn_Modify_Fase
            // 
            this.btn_Modify_Fase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Modify_Fase.BackgroundImage = global::Exacta.Properties.Resources.image;
            this.btn_Modify_Fase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Modify_Fase.Location = new System.Drawing.Point(112, 237);
            this.btn_Modify_Fase.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Modify_Fase.Name = "btn_Modify_Fase";
            this.btn_Modify_Fase.Size = new System.Drawing.Size(38, 41);
            this.btn_Modify_Fase.TabIndex = 51;
            this.btn_Modify_Fase.UseVisualStyleBackColor = false;
            this.btn_Modify_Fase.Click += new System.EventHandler(this.btn_Modify_Fase_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 15);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "PhaseID";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Add_Fase
            // 
            this.btn_Add_Fase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Add_Fase.BackgroundImage = global::Exacta.Properties.Resources.add_48;
            this.btn_Add_Fase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Add_Fase.Location = new System.Drawing.Point(73, 237);
            this.btn_Add_Fase.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Add_Fase.Name = "btn_Add_Fase";
            this.btn_Add_Fase.Size = new System.Drawing.Size(38, 41);
            this.btn_Add_Fase.TabIndex = 50;
            this.btn_Add_Fase.UseVisualStyleBackColor = false;
            this.btn_Add_Fase.Click += new System.EventHandler(this.btn_Add_Fase_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 46);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Phase";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbPhase
            // 
            this.cbPhase.FormattingEnabled = true;
            this.cbPhase.Location = new System.Drawing.Point(226, 42);
            this.cbPhase.Name = "cbPhase";
            this.cbPhase.Size = new System.Drawing.Size(101, 21);
            this.cbPhase.TabIndex = 55;
            this.cbPhase.SelectedValueChanged += new System.EventHandler(this.cbPhase_SelectedValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(342, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(468, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(120, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 60;
            this.pictureBox2.TabStop = false;
            // 
            // Phase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 425);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbPhase);
            this.Controls.Add(this.dgvFase);
            this.Controls.Add(this.pnl_Edit_Fase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Phase";
            this.Text = "Phase";
            this.Load += new System.EventHandler(this.Phase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFase)).EndInit();
            this.pnl_Edit_Fase.ResumeLayout(false);
            this.pnl_Edit_Fase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.Button btn_Delete_Fase;
        private System.Windows.Forms.DataGridView dgvFase;
        private System.Windows.Forms.Button btn_Modify_Fase;
        private System.Windows.Forms.Button btn_Add_Fase;
        private System.Windows.Forms.Panel pnl_Edit_Fase;
        private System.Windows.Forms.Button btn_Cancel_Fase;
        private System.Windows.Forms.Button btn_Save_Fase;
        private System.Windows.Forms.TextBox txt_Fase;
        private System.Windows.Forms.TextBox txt_FaseID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pbPDF;
        private System.Windows.Forms.ComboBox cbPhase;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
    }