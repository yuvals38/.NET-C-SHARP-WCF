namespace WcfServerForm
{
    partial class ServerForm
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
            this.label6 = new System.Windows.Forms.Label();
            this.ClientIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.LoadServerButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ServerStatusTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Client ID";
            // 
            // ClientIdTextBox
            // 
            this.ClientIdTextBox.Location = new System.Drawing.Point(157, 71);
            this.ClientIdTextBox.Name = "ClientIdTextBox";
            this.ClientIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.ClientIdTextBox.TabIndex = 21;
            this.ClientIdTextBox.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(125, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Client Status";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(200, 245);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 19;
            this.textBox3.Text = "Disconnect";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Port";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(331, 140);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(100, 20);
            this.PortTextBox.TabIndex = 17;
            this.PortTextBox.Text = "7001";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "IP";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(157, 140);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(100, 20);
            this.IPTextBox.TabIndex = 15;
            this.IPTextBox.Text = "localhost";
            // 
            // LoadServerButton
            // 
            this.LoadServerButton.Location = new System.Drawing.Point(492, 140);
            this.LoadServerButton.Name = "LoadServerButton";
            this.LoadServerButton.Size = new System.Drawing.Size(122, 23);
            this.LoadServerButton.TabIndex = 23;
            this.LoadServerButton.Text = "Load Server";
            this.LoadServerButton.UseVisualStyleBackColor = true;
            this.LoadServerButton.Click += new System.EventHandler(this.LoadServerButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(489, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Server Status";
            // 
            // ServerStatusTextBox
            // 
            this.ServerStatusTextBox.Location = new System.Drawing.Point(564, 188);
            this.ServerStatusTextBox.Name = "ServerStatusTextBox";
            this.ServerStatusTextBox.ReadOnly = true;
            this.ServerStatusTextBox.Size = new System.Drawing.Size(100, 20);
            this.ServerStatusTextBox.TabIndex = 24;
            this.ServerStatusTextBox.Text = "Disconnect";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ServerStatusTextBox);
            this.Controls.Add(this.LoadServerButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ClientIdTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPTextBox);
            this.Name = "Form1";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ClientIdTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Button LoadServerButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ServerStatusTextBox;
    }
}

