namespace Exacta
{
    partial class Operators
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Operators));
            this.dgvOperators = new System.Windows.Forms.DataGridView();
            this.btnDeleteOperator = new System.Windows.Forms.Button();
            this.btnCancelOperator = new System.Windows.Forms.Button();
            this.btnSaveOperator = new System.Windows.Forms.Button();
            this.btnEditOperator = new System.Windows.Forms.Button();
            this.btnAddOperator = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbLine = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperators)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOperators
            // 
            this.dgvOperators.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOperators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOperators.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOperators.Location = new System.Drawing.Point(12, 12);
            this.dgvOperators.Name = "dgvOperators";
            this.dgvOperators.Size = new System.Drawing.Size(566, 376);
            this.dgvOperators.TabIndex = 0;
            this.dgvOperators.SelectionChanged += new System.EventHandler(this.dgvOperators_SelectionChanged);
            // 
            // btnDeleteOperator
            // 
            this.btnDeleteOperator.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDeleteOperator.BackgroundImage = global::Exacta.Properties.Resources.discard_48;
            this.btnDeleteOperator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteOperator.Location = new System.Drawing.Point(773, 321);
            this.btnDeleteOperator.Margin = new System.Windows.Forms.Padding(1);
            this.btnDeleteOperator.Name = "btnDeleteOperator";
            this.btnDeleteOperator.Size = new System.Drawing.Size(38, 41);
            this.btnDeleteOperator.TabIndex = 89;
            this.btnDeleteOperator.UseVisualStyleBackColor = false;
            this.btnDeleteOperator.Click += new System.EventHandler(this.btnDeleteOperator_Click);
            // 
            // btnCancelOperator
            // 
            this.btnCancelOperator.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancelOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelOperator.Location = new System.Drawing.Point(744, 364);
            this.btnCancelOperator.Margin = new System.Windows.Forms.Padding(1);
            this.btnCancelOperator.Name = "btnCancelOperator";
            this.btnCancelOperator.Size = new System.Drawing.Size(67, 24);
            this.btnCancelOperator.TabIndex = 86;
            this.btnCancelOperator.Text = "Cancel";
            this.btnCancelOperator.UseVisualStyleBackColor = false;
            this.btnCancelOperator.Click += new System.EventHandler(this.btnCancelOperator_Click);
            // 
            // btnSaveOperator
            // 
            this.btnSaveOperator.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSaveOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveOperator.Location = new System.Drawing.Point(675, 364);
            this.btnSaveOperator.Margin = new System.Windows.Forms.Padding(1);
            this.btnSaveOperator.Name = "btnSaveOperator";
            this.btnSaveOperator.Size = new System.Drawing.Size(67, 24);
            this.btnSaveOperator.TabIndex = 85;
            this.btnSaveOperator.Text = "Save";
            this.btnSaveOperator.UseVisualStyleBackColor = false;
            this.btnSaveOperator.Click += new System.EventHandler(this.btnSaveOperator_Click);
            // 
            // btnEditOperator
            // 
            this.btnEditOperator.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEditOperator.BackgroundImage = global::Exacta.Properties.Resources.image;
            this.btnEditOperator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditOperator.Location = new System.Drawing.Point(733, 321);
            this.btnEditOperator.Margin = new System.Windows.Forms.Padding(1);
            this.btnEditOperator.Name = "btnEditOperator";
            this.btnEditOperator.Size = new System.Drawing.Size(38, 41);
            this.btnEditOperator.TabIndex = 88;
            this.btnEditOperator.UseVisualStyleBackColor = false;
            this.btnEditOperator.Click += new System.EventHandler(this.btnEditOperator_Click);
            // 
            // btnAddOperator
            // 
            this.btnAddOperator.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAddOperator.BackgroundImage = global::Exacta.Properties.Resources.add_48;
            this.btnAddOperator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddOperator.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnAddOperator.Location = new System.Drawing.Point(693, 321);
            this.btnAddOperator.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddOperator.Name = "btnAddOperator";
            this.btnAddOperator.Size = new System.Drawing.Size(38, 41);
            this.btnAddOperator.TabIndex = 87;
            this.btnAddOperator.UseVisualStyleBackColor = false;
            this.btnAddOperator.Click += new System.EventHandler(this.btnAddOperator_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(584, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 90;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(629, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 100;
            this.label5.Text = "Line";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(629, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 99;
            this.label4.Text = "Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(629, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 98;
            this.label3.Text = "Nr Tel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(629, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Surname";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(629, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 96;
            this.label1.Text = "Name";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(701, 110);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(110, 20);
            this.txtSurname.TabIndex = 95;
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(701, 145);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(110, 20);
            this.txtTel.TabIndex = 94;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(701, 180);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(110, 20);
            this.txtAddress.TabIndex = 93;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(701, 75);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(110, 20);
            this.txtName.TabIndex = 91;
            // 
            // cbLine
            // 
            this.cbLine.FormattingEnabled = true;
            this.cbLine.Location = new System.Drawing.Point(701, 215);
            this.cbLine.Name = "cbLine";
            this.cbLine.Size = new System.Drawing.Size(110, 21);
            this.cbLine.TabIndex = 101;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(710, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(120, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 102;
            this.pictureBox2.TabStop = false;
            // 
            // Operators
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 400);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cbLine);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDeleteOperator);
            this.Controls.Add(this.btnCancelOperator);
            this.Controls.Add(this.btnSaveOperator);
            this.Controls.Add(this.btnEditOperator);
            this.Controls.Add(this.btnAddOperator);
            this.Controls.Add(this.dgvOperators);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Operators";
            this.Text = "Operators";
            this.Load += new System.EventHandler(this.Operators_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperators)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOperators;
        private System.Windows.Forms.Button btnDeleteOperator;
        private System.Windows.Forms.Button btnCancelOperator;
        private System.Windows.Forms.Button btnSaveOperator;
        private System.Windows.Forms.Button btnEditOperator;
        private System.Windows.Forms.Button btnAddOperator;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cbLine;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}