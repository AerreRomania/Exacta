namespace Exacta
{
    partial class Messagebox
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_yes = new System.Windows.Forms.Button();
            this.btn_no = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(2, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Licence Expired!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Do you want to reactivate your product?";
            // 
            // btn_yes
            // 
            this.btn_yes.BackColor = System.Drawing.Color.DimGray;
            this.btn_yes.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btn_yes.FlatAppearance.BorderSize = 2;
            this.btn_yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_yes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_yes.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_yes.Location = new System.Drawing.Point(275, 116);
            this.btn_yes.Name = "btn_yes";
            this.btn_yes.Size = new System.Drawing.Size(102, 28);
            this.btn_yes.TabIndex = 3;
            this.btn_yes.Text = "Yes";
            this.btn_yes.UseVisualStyleBackColor = false;
            this.btn_yes.Click += new System.EventHandler(this.btn_yes_Click);
            // 
            // btn_no
            // 
            this.btn_no.BackColor = System.Drawing.Color.DimGray;
            this.btn_no.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btn_no.FlatAppearance.BorderSize = 2;
            this.btn_no.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_no.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_no.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_no.Location = new System.Drawing.Point(154, 116);
            this.btn_no.Name = "btn_no";
            this.btn_no.Size = new System.Drawing.Size(102, 28);
            this.btn_no.TabIndex = 4;
            this.btn_no.Text = "No";
            this.btn_no.UseVisualStyleBackColor = false;
            this.btn_no.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(13, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 34);
            this.label3.TabIndex = 5;
            this.label3.Text = "Provided by\r\n Aerre Romania";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 42);
            this.label4.TabIndex = 6;
            this.label4.Text = "NOTE: internet conection is \r\nrequired for activation!\r\n";
            // 
            // Messagebox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(389, 168);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_no);
            this.Controls.Add(this.btn_yes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Messagebox";
            this.Text = "Messagebox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_yes;
        private System.Windows.Forms.Button btn_no;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}