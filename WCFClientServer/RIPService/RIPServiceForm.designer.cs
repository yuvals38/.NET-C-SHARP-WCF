namespace RIPService
{
    partial class RIPServiceForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnFireAuto = new System.Windows.Forms.Button();
            this.tmrEvent = new System.Windows.Forms.Timer(this.components);
            this.btnFireAutoStop = new System.Windows.Forms.Button();
            this.txtEventCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEventData = new System.Windows.Forms.TextBox();
            this.txtTopicName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect single client";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SendEvent);
            // 
            // btnFireAuto
            // 
            this.btnFireAuto.Location = new System.Drawing.Point(20, 32);
            this.btnFireAuto.Name = "btnFireAuto";
            this.btnFireAuto.Size = new System.Drawing.Size(143, 23);
            this.btnFireAuto.TabIndex = 3;
            this.btnFireAuto.Text = "Connect multi clients";
            this.btnFireAuto.UseVisualStyleBackColor = true;
            this.btnFireAuto.Click += new System.EventHandler(this.SendAutoEvent);
            // 
            // tmrEvent
            // 
            this.tmrEvent.Tick += new System.EventHandler(this.tmrEvent_Tick);
            // 
            // btnFireAutoStop
            // 
            this.btnFireAutoStop.Location = new System.Drawing.Point(20, 62);
            this.btnFireAutoStop.Name = "btnFireAutoStop";
            this.btnFireAutoStop.Size = new System.Drawing.Size(143, 23);
            this.btnFireAutoStop.TabIndex = 5;
            this.btnFireAutoStop.Text = "Disconnect";
            this.btnFireAutoStop.UseVisualStyleBackColor = true;
            this.btnFireAutoStop.Click += new System.EventHandler(this.StopAutoEvent);
            // 
            // txtEventCount
            // 
            this.txtEventCount.Location = new System.Drawing.Point(425, 215);
            this.txtEventCount.Name = "txtEventCount";
            this.txtEventCount.Size = new System.Drawing.Size(41, 20);
            this.txtEventCount.TabIndex = 6;
            this.txtEventCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(263, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Total Events  Fired :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFireAuto);
            this.groupBox1.Controls.Add(this.btnFireAutoStop);
            this.groupBox1.Location = new System.Drawing.Point(297, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 127);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Event";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Event Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "request Name";
            // 
            // txtEventData
            // 
            this.txtEventData.Location = new System.Drawing.Point(90, 87);
            this.txtEventData.Multiline = true;
            this.txtEventData.Name = "txtEventData";
            this.txtEventData.Size = new System.Drawing.Size(147, 82);
            this.txtEventData.TabIndex = 41;
            // 
            // txtTopicName
            // 
            this.txtTopicName.Location = new System.Drawing.Point(90, 45);
            this.txtTopicName.Name = "txtTopicName";
            this.txtTopicName.Size = new System.Drawing.Size(160, 20);
            this.txtTopicName.TabIndex = 40;
            this.txtTopicName.Text = "RequestMessage1";
            // 
            // RIPServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 284);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEventData);
            this.Controls.Add(this.txtTopicName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEventCount);
            this.Controls.Add(this.button1);
            this.Name = "RIPServiceForm";
            this.Text = "RIP Service";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFireAuto;
        private System.Windows.Forms.Timer tmrEvent;
        private System.Windows.Forms.Button btnFireAutoStop;
        private System.Windows.Forms.TextBox txtEventCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEventData;
        private System.Windows.Forms.TextBox txtTopicName;
    }
}