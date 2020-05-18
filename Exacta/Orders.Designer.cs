namespace Exacta
{
    partial class Orders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Orders));
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnCancelOrder = new System.Windows.Forms.Button();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.btnEditOrder = new System.Windows.Forms.Button();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dtpDateArrival = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNrOrder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbClient = new System.Windows.Forms.ComboBox();
            this.cbArticle = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrders
            // 
            this.dgvOrders.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrders.Location = new System.Drawing.Point(12, 12);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(509, 294);
            this.dgvOrders.TabIndex = 0;
            this.dgvOrders.SelectionChanged += new System.EventHandler(this.dgvOrders_SelectionChanged);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDeleteOrder.BackgroundImage = global::Exacta.Properties.Resources.discard_48;
            this.btnDeleteOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteOrder.Location = new System.Drawing.Point(688, 239);
            this.btnDeleteOrder.Margin = new System.Windows.Forms.Padding(1);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(38, 41);
            this.btnDeleteOrder.TabIndex = 84;
            this.btnDeleteOrder.UseVisualStyleBackColor = false;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // btnCancelOrder
            // 
            this.btnCancelOrder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancelOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelOrder.Location = new System.Drawing.Point(659, 282);
            this.btnCancelOrder.Margin = new System.Windows.Forms.Padding(1);
            this.btnCancelOrder.Name = "btnCancelOrder";
            this.btnCancelOrder.Size = new System.Drawing.Size(67, 24);
            this.btnCancelOrder.TabIndex = 81;
            this.btnCancelOrder.Text = "Cancel";
            this.btnCancelOrder.UseVisualStyleBackColor = false;
            this.btnCancelOrder.Click += new System.EventHandler(this.btnCancelOrder_Click);
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSaveOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveOrder.Location = new System.Drawing.Point(590, 282);
            this.btnSaveOrder.Margin = new System.Windows.Forms.Padding(1);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(67, 24);
            this.btnSaveOrder.TabIndex = 80;
            this.btnSaveOrder.Text = "Save";
            this.btnSaveOrder.UseVisualStyleBackColor = false;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // btnEditOrder
            // 
            this.btnEditOrder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEditOrder.BackgroundImage = global::Exacta.Properties.Resources.image;
            this.btnEditOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditOrder.Location = new System.Drawing.Point(648, 239);
            this.btnEditOrder.Margin = new System.Windows.Forms.Padding(1);
            this.btnEditOrder.Name = "btnEditOrder";
            this.btnEditOrder.Size = new System.Drawing.Size(38, 41);
            this.btnEditOrder.TabIndex = 83;
            this.btnEditOrder.UseVisualStyleBackColor = false;
            this.btnEditOrder.Click += new System.EventHandler(this.btnEditOrder_Click);
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAddOrder.BackgroundImage = global::Exacta.Properties.Resources.add_48;
            this.btnAddOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddOrder.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnAddOrder.Location = new System.Drawing.Point(608, 239);
            this.btnAddOrder.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(38, 41);
            this.btnAddOrder.TabIndex = 82;
            this.btnAddOrder.UseVisualStyleBackColor = false;
            this.btnAddOrder.Click += new System.EventHandler(this.btnAddOrder_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(533, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 91;
            this.pictureBox1.TabStop = false;
            // 
            // dtpDateArrival
            // 
            this.dtpDateArrival.CustomFormat = "dd/MM/yyyy";
            this.dtpDateArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateArrival.Location = new System.Drawing.Point(616, 198);
            this.dtpDateArrival.Margin = new System.Windows.Forms.Padding(1);
            this.dtpDateArrival.Name = "dtpDateArrival";
            this.dtpDateArrival.Size = new System.Drawing.Size(110, 20);
            this.dtpDateArrival.TabIndex = 97;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(539, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 96;
            this.label3.Text = "Date Arrival";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(538, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 95;
            this.label2.Text = "Article";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(538, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 94;
            this.label1.Text = "Client";
            // 
            // txtNrOrder
            // 
            this.txtNrOrder.Location = new System.Drawing.Point(616, 79);
            this.txtNrOrder.Name = "txtNrOrder";
            this.txtNrOrder.Size = new System.Drawing.Size(110, 20);
            this.txtNrOrder.TabIndex = 98;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(538, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 99;
            this.label4.Text = "Nr Order";
            // 
            // cbClient
            // 
            this.cbClient.FormattingEnabled = true;
            this.cbClient.Location = new System.Drawing.Point(616, 118);
            this.cbClient.Name = "cbClient";
            this.cbClient.Size = new System.Drawing.Size(110, 21);
            this.cbClient.TabIndex = 100;
            // 
            // cbArticle
            // 
            this.cbArticle.FormattingEnabled = true;
            this.cbArticle.Location = new System.Drawing.Point(616, 157);
            this.cbArticle.Name = "cbArticle";
            this.cbArticle.Size = new System.Drawing.Size(110, 21);
            this.cbArticle.TabIndex = 101;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(659, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(120, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 102;
            this.pictureBox2.TabStop = false;
            // 
            // Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 318);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cbArticle);
            this.Controls.Add(this.cbClient);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNrOrder);
            this.Controls.Add(this.dtpDateArrival);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDeleteOrder);
            this.Controls.Add(this.btnCancelOrder);
            this.Controls.Add(this.btnSaveOrder);
            this.Controls.Add(this.btnEditOrder);
            this.Controls.Add(this.btnAddOrder);
            this.Controls.Add(this.dgvOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Orders";
            this.Text = "Orders";
            this.Load += new System.EventHandler(this.Orders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnCancelOrder;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnEditOrder;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DateTimePicker dtpDateArrival;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNrOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbClient;
        private System.Windows.Forms.ComboBox cbArticle;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}