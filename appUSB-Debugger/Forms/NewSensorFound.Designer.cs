namespace appUSB_Debugger
{
    partial class NewSensorFound
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnReplaceSensor1 = new System.Windows.Forms.RadioButton();
            this.rbtnIgnore = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtnReplaceSensor2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(4, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(340, 32);
            this.label4.TabIndex = 19;
            this.label4.Text = "Not a new Sensor? Please reattach the device or restart \r\nthe program.\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(5, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "What would you like to do?\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(345, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "Unrecognized Sensor attached via USB.\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(46, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Requires Pairing\r\n";
            // 
            // rbtnReplaceSensor1
            // 
            this.rbtnReplaceSensor1.AutoSize = true;
            this.rbtnReplaceSensor1.FlatAppearance.BorderSize = 0;
            this.rbtnReplaceSensor1.ForeColor = System.Drawing.Color.White;
            this.rbtnReplaceSensor1.Location = new System.Drawing.Point(12, 91);
            this.rbtnReplaceSensor1.Name = "rbtnReplaceSensor1";
            this.rbtnReplaceSensor1.Size = new System.Drawing.Size(250, 21);
            this.rbtnReplaceSensor1.TabIndex = 15;
            this.rbtnReplaceSensor1.TabStop = true;
            this.rbtnReplaceSensor1.Text = "Replace Sensor 1 with New Sensor\r\n";
            this.rbtnReplaceSensor1.UseVisualStyleBackColor = true;
            this.rbtnReplaceSensor1.Click += new System.EventHandler(this.rbtnReplaceSensor1_Selected);
            // 
            // rbtnIgnore
            // 
            this.rbtnIgnore.AutoSize = true;
            this.rbtnIgnore.Checked = true;
            this.rbtnIgnore.FlatAppearance.BorderSize = 0;
            this.rbtnIgnore.ForeColor = System.Drawing.Color.White;
            this.rbtnIgnore.Location = new System.Drawing.Point(12, 169);
            this.rbtnIgnore.Name = "rbtnIgnore";
            this.rbtnIgnore.Size = new System.Drawing.Size(149, 21);
            this.rbtnIgnore.TabIndex = 14;
            this.rbtnIgnore.TabStop = true;
            this.rbtnIgnore.Text = "Ignore New Sensor";
            this.rbtnIgnore.UseVisualStyleBackColor = true;
            this.rbtnIgnore.Click += new System.EventHandler(this.rbtnIgnore_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(41)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(185, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(165, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(41)))));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(111)))), ((int)(((byte)(130)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(8, 196);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(165, 28);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(46, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "Requires Pairing\r\n";
            // 
            // rbtnReplaceSensor2
            // 
            this.rbtnReplaceSensor2.AutoSize = true;
            this.rbtnReplaceSensor2.FlatAppearance.BorderSize = 0;
            this.rbtnReplaceSensor2.ForeColor = System.Drawing.Color.White;
            this.rbtnReplaceSensor2.Location = new System.Drawing.Point(12, 130);
            this.rbtnReplaceSensor2.Name = "rbtnReplaceSensor2";
            this.rbtnReplaceSensor2.Size = new System.Drawing.Size(250, 21);
            this.rbtnReplaceSensor2.TabIndex = 20;
            this.rbtnReplaceSensor2.TabStop = true;
            this.rbtnReplaceSensor2.Text = "Replace Sensor 2 with New Sensor\r\n";
            this.rbtnReplaceSensor2.UseVisualStyleBackColor = true;
            this.rbtnReplaceSensor2.Click += new System.EventHandler(this.rbtnReplaceSensor2_Selected);
            // 
            // NewSensorFound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(69)))), ((int)(((byte)(81)))));
            this.ClientSize = new System.Drawing.Size(357, 227);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbtnReplaceSensor2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtnReplaceSensor1);
            this.Controls.Add(this.rbtnIgnore);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewSensorFound";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Sensor Found";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtnReplaceSensor1;
        private System.Windows.Forms.RadioButton rbtnIgnore;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtnReplaceSensor2;
    }
}