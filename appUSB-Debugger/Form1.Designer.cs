namespace appUSB_Debugger
{
    partial class Form1
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
            this.lbl_sens_serialport = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_sens_sent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_sens_recieved = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_hub_recieved = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_hub_sent = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_hub_serialport = new System.Windows.Forms.Label();
            this.tb_sens_user_send = new System.Windows.Forms.TextBox();
            this.bt_sens_user_send = new System.Windows.Forms.Button();
            this.bt_hub_user_send = new System.Windows.Forms.Button();
            this.tb_hub_user_send = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_sens_serialport
            // 
            this.lbl_sens_serialport.AutoSize = true;
            this.lbl_sens_serialport.BackColor = System.Drawing.Color.Transparent;
            this.lbl_sens_serialport.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sens_serialport.Location = new System.Drawing.Point(334, 5);
            this.lbl_sens_serialport.Name = "lbl_sens_serialport";
            this.lbl_sens_serialport.Size = new System.Drawing.Size(31, 17);
            this.lbl_sens_serialport.TabIndex = 1;
            this.lbl_sens_serialport.Text = "N/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(238, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial Port:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(186)))), ((int)(((byte)(202)))));
            this.panel1.Controls.Add(this.bt_sens_user_send);
            this.panel1.Controls.Add(this.tb_sens_user_send);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tb_sens_recieved);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tb_sens_sent);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 750);
            this.panel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "SENSOR";
            // 
            // tb_sens_sent
            // 
            this.tb_sens_sent.Location = new System.Drawing.Point(12, 51);
            this.tb_sens_sent.Multiline = true;
            this.tb_sens_sent.Name = "tb_sens_sent";
            this.tb_sens_sent.Size = new System.Drawing.Size(362, 163);
            this.tb_sens_sent.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sent Messages";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lbl_sens_serialport);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 25);
            this.panel2.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Recieved Messages";
            // 
            // tb_sens_recieved
            // 
            this.tb_sens_recieved.Location = new System.Drawing.Point(12, 245);
            this.tb_sens_recieved.Multiline = true;
            this.tb_sens_recieved.Name = "tb_sens_recieved";
            this.tb_sens_recieved.Size = new System.Drawing.Size(362, 163);
            this.tb_sens_recieved.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(409, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Recieved Messages";
            // 
            // tb_hub_recieved
            // 
            this.tb_hub_recieved.Location = new System.Drawing.Point(408, 245);
            this.tb_hub_recieved.Multiline = true;
            this.tb_hub_recieved.Name = "tb_hub_recieved";
            this.tb_hub_recieved.Size = new System.Drawing.Size(362, 163);
            this.tb_hub_recieved.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(409, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Sent Messages";
            // 
            // tb_hub_sent
            // 
            this.tb_hub_sent.Location = new System.Drawing.Point(408, 51);
            this.tb_hub_sent.Multiline = true;
            this.tb_hub_sent.Name = "tb_hub_sent";
            this.tb_hub_sent.Size = new System.Drawing.Size(362, 163);
            this.tb_hub_sent.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "HUB";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Info;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.lbl_hub_serialport);
            this.panel3.Location = new System.Drawing.Point(396, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 25);
            this.panel3.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(238, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Serial Port:";
            // 
            // lbl_hub_serialport
            // 
            this.lbl_hub_serialport.AutoSize = true;
            this.lbl_hub_serialport.BackColor = System.Drawing.Color.Transparent;
            this.lbl_hub_serialport.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hub_serialport.Location = new System.Drawing.Point(334, 5);
            this.lbl_hub_serialport.Name = "lbl_hub_serialport";
            this.lbl_hub_serialport.Size = new System.Drawing.Size(31, 17);
            this.lbl_hub_serialport.TabIndex = 1;
            this.lbl_hub_serialport.Text = "N/A";
            // 
            // tb_sens_user_send
            // 
            this.tb_sens_user_send.Location = new System.Drawing.Point(12, 425);
            this.tb_sens_user_send.Name = "tb_sens_user_send";
            this.tb_sens_user_send.Size = new System.Drawing.Size(290, 22);
            this.tb_sens_user_send.TabIndex = 8;
            // 
            // bt_sens_user_send
            // 
            this.bt_sens_user_send.Location = new System.Drawing.Point(308, 424);
            this.bt_sens_user_send.Name = "bt_sens_user_send";
            this.bt_sens_user_send.Size = new System.Drawing.Size(75, 23);
            this.bt_sens_user_send.TabIndex = 9;
            this.bt_sens_user_send.Text = "Send";
            this.bt_sens_user_send.UseVisualStyleBackColor = true;
            // 
            // bt_hub_user_send
            // 
            this.bt_hub_user_send.Location = new System.Drawing.Point(704, 424);
            this.bt_hub_user_send.Name = "bt_hub_user_send";
            this.bt_hub_user_send.Size = new System.Drawing.Size(75, 23);
            this.bt_hub_user_send.TabIndex = 11;
            this.bt_hub_user_send.Text = "Send";
            this.bt_hub_user_send.UseVisualStyleBackColor = true;
            // 
            // tb_hub_user_send
            // 
            this.tb_hub_user_send.Location = new System.Drawing.Point(408, 425);
            this.tb_hub_user_send.Name = "tb_hub_user_send";
            this.tb_hub_user_send.Size = new System.Drawing.Size(290, 22);
            this.tb_hub_user_send.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.bt_hub_user_send);
            this.Controls.Add(this.tb_hub_user_send);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tb_hub_recieved);
            this.Controls.Add(this.tb_hub_sent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_sens_serialport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_sens_sent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_sens_recieved;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_hub_recieved;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_hub_sent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_hub_serialport;
        private System.Windows.Forms.Button bt_sens_user_send;
        private System.Windows.Forms.TextBox tb_sens_user_send;
        private System.Windows.Forms.Button bt_hub_user_send;
        private System.Windows.Forms.TextBox tb_hub_user_send;
    }
}

