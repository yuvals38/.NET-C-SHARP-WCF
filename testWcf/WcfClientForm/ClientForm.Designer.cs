namespace WcfClientForm
{
    partial class ClientForm
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
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.ClientConnectButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ClientStatusTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StringToSendTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PathFileToSendTextBox = new System.Windows.Forms.TextBox();
            this.StringToSendButton = new System.Windows.Forms.Button();
            this.FileToSendButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ClientIdTextBox = new System.Windows.Forms.TextBox();
            this.MessageRecievedTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BrowsButton = new System.Windows.Forms.Button();
            this.RetrieveRipDadaButton = new System.Windows.Forms.Button();
            this.BrowsButtonPathTo = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.PathToTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(146, 100);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(100, 20);
            this.IPTextBox.TabIndex = 0;
            this.IPTextBox.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(320, 100);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(100, 20);
            this.PortTextBox.TabIndex = 2;
            this.PortTextBox.Text = "7001";
            // 
            // ClientConnectButton
            // 
            this.ClientConnectButton.Location = new System.Drawing.Point(443, 98);
            this.ClientConnectButton.Name = "ClientConnectButton";
            this.ClientConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ClientConnectButton.TabIndex = 4;
            this.ClientConnectButton.Text = "Connect";
            this.ClientConnectButton.UseVisualStyleBackColor = true;
            this.ClientConnectButton.Click += new System.EventHandler(this.ClientConnectButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Client Status";
            // 
            // ClientStatusTextBox
            // 
            this.ClientStatusTextBox.Location = new System.Drawing.Point(186, 182);
            this.ClientStatusTextBox.Name = "ClientStatusTextBox";
            this.ClientStatusTextBox.ReadOnly = true;
            this.ClientStatusTextBox.Size = new System.Drawing.Size(100, 20);
            this.ClientStatusTextBox.TabIndex = 5;
            this.ClientStatusTextBox.Text = "Disconnect";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "String to send";
            // 
            // StringToSendTextBox
            // 
            this.StringToSendTextBox.Location = new System.Drawing.Point(204, 268);
            this.StringToSendTextBox.Name = "StringToSendTextBox";
            this.StringToSendTextBox.Size = new System.Drawing.Size(100, 20);
            this.StringToSendTextBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 314);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "File to Retrieve";
            // 
            // PathFileToSendTextBox
            // 
            this.PathFileToSendTextBox.Location = new System.Drawing.Point(96, 311);
            this.PathFileToSendTextBox.Name = "PathFileToSendTextBox";
            this.PathFileToSendTextBox.Size = new System.Drawing.Size(100, 20);
            this.PathFileToSendTextBox.TabIndex = 9;
            // 
            // StringToSendButton
            // 
            this.StringToSendButton.Location = new System.Drawing.Point(345, 265);
            this.StringToSendButton.Name = "StringToSendButton";
            this.StringToSendButton.Size = new System.Drawing.Size(75, 23);
            this.StringToSendButton.TabIndex = 11;
            this.StringToSendButton.Text = "Send";
            this.StringToSendButton.UseVisualStyleBackColor = true;
            this.StringToSendButton.Click += new System.EventHandler(this.StringToSendButton_Click);
            // 
            // FileToSendButton
            // 
            this.FileToSendButton.Location = new System.Drawing.Point(421, 304);
            this.FileToSendButton.Name = "FileToSendButton";
            this.FileToSendButton.Size = new System.Drawing.Size(75, 23);
            this.FileToSendButton.TabIndex = 12;
            this.FileToSendButton.Text = "Send";
            this.FileToSendButton.UseVisualStyleBackColor = true;
            this.FileToSendButton.Click += new System.EventHandler(this.FileToSendButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(93, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Client ID";
            // 
            // ClientIdTextBox
            // 
            this.ClientIdTextBox.Location = new System.Drawing.Point(146, 31);
            this.ClientIdTextBox.Name = "ClientIdTextBox";
            this.ClientIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.ClientIdTextBox.TabIndex = 13;
            this.ClientIdTextBox.Text = "1";
            // 
            // MessageRecievedTextBox
            // 
            this.MessageRecievedTextBox.Location = new System.Drawing.Point(517, 206);
            this.MessageRecievedTextBox.Multiline = true;
            this.MessageRecievedTextBox.Name = "MessageRecievedTextBox";
            this.MessageRecievedTextBox.Size = new System.Drawing.Size(255, 139);
            this.MessageRecievedTextBox.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(514, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Message Recieved";
            // 
            // BrowsButton
            // 
            this.BrowsButton.Location = new System.Drawing.Point(204, 309);
            this.BrowsButton.Name = "BrowsButton";
            this.BrowsButton.Size = new System.Drawing.Size(75, 23);
            this.BrowsButton.TabIndex = 17;
            this.BrowsButton.Text = "Browse";
            this.BrowsButton.UseVisualStyleBackColor = true;
            this.BrowsButton.Click += new System.EventHandler(this.BrowsButton_Click);
            // 
            // RetrieveRipDadaButton
            // 
            this.RetrieveRipDadaButton.Location = new System.Drawing.Point(340, 389);
            this.RetrieveRipDadaButton.Name = "RetrieveRipDadaButton";
            this.RetrieveRipDadaButton.Size = new System.Drawing.Size(107, 23);
            this.RetrieveRipDadaButton.TabIndex = 19;
            this.RetrieveRipDadaButton.Text = "RetrieveRipDada";
            this.RetrieveRipDadaButton.UseVisualStyleBackColor = true;
            this.RetrieveRipDadaButton.Click += new System.EventHandler(this.RetrieveRipDadaButton_Click);
            // 
            // BrowsButtonPathTo
            // 
            this.BrowsButtonPathTo.Location = new System.Drawing.Point(204, 337);
            this.BrowsButtonPathTo.Name = "BrowsButtonPathTo";
            this.BrowsButtonPathTo.Size = new System.Drawing.Size(75, 23);
            this.BrowsButtonPathTo.TabIndex = 22;
            this.BrowsButtonPathTo.Text = "Browse";
            this.BrowsButtonPathTo.UseVisualStyleBackColor = true;
            this.BrowsButtonPathTo.Click += new System.EventHandler(this.BrowsButtonPathTo_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 342);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Where to Save";
            // 
            // PathToTextBox
            // 
            this.PathToTextBox.Location = new System.Drawing.Point(96, 339);
            this.PathToTextBox.Name = "PathToTextBox";
            this.PathToTextBox.Size = new System.Drawing.Size(100, 20);
            this.PathToTextBox.TabIndex = 20;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BrowsButtonPathTo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PathToTextBox);
            this.Controls.Add(this.RetrieveRipDadaButton);
            this.Controls.Add(this.BrowsButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MessageRecievedTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ClientIdTextBox);
            this.Controls.Add(this.FileToSendButton);
            this.Controls.Add(this.StringToSendButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PathFileToSendTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.StringToSendTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ClientStatusTextBox);
            this.Controls.Add(this.ClientConnectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPTextBox);
            this.Name = "ClientForm";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Button ClientConnectButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ClientStatusTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StringToSendTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PathFileToSendTextBox;
        private System.Windows.Forms.Button StringToSendButton;
        private System.Windows.Forms.Button FileToSendButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ClientIdTextBox;
        private System.Windows.Forms.TextBox MessageRecievedTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BrowsButton;
        private System.Windows.Forms.Button RetrieveRipDadaButton;
        private System.Windows.Forms.Button BrowsButtonPathTo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox PathToTextBox;
    }
}

