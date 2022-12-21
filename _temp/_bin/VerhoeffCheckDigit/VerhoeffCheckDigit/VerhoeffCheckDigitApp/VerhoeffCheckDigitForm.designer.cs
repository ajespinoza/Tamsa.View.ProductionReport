namespace VerhoeffCheckDigitApp
{
    partial class VerhoeffCheckDigitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerhoeffCheckDigitForm));
            this.btnCalculateCheckDigit = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.txtResultWithCheckDigit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCheckResult1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbIntArray = new System.Windows.Forms.RadioButton();
            this.rbLong = new System.Windows.Forms.RadioButton();
            this.rbString = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbInt = new System.Windows.Forms.RadioButton();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCheckDigit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInput2 = new System.Windows.Forms.TextBox();
            this.txtCheckResult2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCalculateCheckDigit
            // 
            this.btnCalculateCheckDigit.Location = new System.Drawing.Point(21, 109);
            this.btnCalculateCheckDigit.Name = "btnCalculateCheckDigit";
            this.btnCalculateCheckDigit.Size = new System.Drawing.Size(139, 23);
            this.btnCalculateCheckDigit.TabIndex = 7;
            this.btnCalculateCheckDigit.Text = "Calcular Digito Verificador";
            this.btnCalculateCheckDigit.UseVisualStyleBackColor = true;
            this.btnCalculateCheckDigit.Click += new System.EventHandler(this.btnCalculateCheckDigit_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(437, 109);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(73, 23);
            this.btnCheck.TabIndex = 8;
            this.btnCheck.Text = "Validar";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtResultWithCheckDigit
            // 
            this.txtResultWithCheckDigit.Location = new System.Drawing.Point(21, 178);
            this.txtResultWithCheckDigit.Name = "txtResultWithCheckDigit";
            this.txtResultWithCheckDigit.Size = new System.Drawing.Size(400, 20);
            this.txtResultWithCheckDigit.TabIndex = 10;
            this.txtResultWithCheckDigit.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Number with check digit appended:";
            this.label2.Visible = false;
            // 
            // txtCheckResult1
            // 
            this.txtCheckResult1.Location = new System.Drawing.Point(432, 178);
            this.txtCheckResult1.Name = "txtCheckResult1";
            this.txtCheckResult1.ReadOnly = true;
            this.txtCheckResult1.Size = new System.Drawing.Size(82, 20);
            this.txtCheckResult1.TabIndex = 12;
            this.txtCheckResult1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(432, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Result of check:";
            this.label3.Visible = false;
            // 
            // rbIntArray
            // 
            this.rbIntArray.AutoSize = true;
            this.rbIntArray.Location = new System.Drawing.Point(336, 12);
            this.rbIntArray.Name = "rbIntArray";
            this.rbIntArray.Size = new System.Drawing.Size(85, 17);
            this.rbIntArray.TabIndex = 4;
            this.rbIntArray.Text = "Integer Array";
            this.rbIntArray.UseVisualStyleBackColor = true;
            // 
            // rbLong
            // 
            this.rbLong.AutoSize = true;
            this.rbLong.Location = new System.Drawing.Point(262, 12);
            this.rbLong.Name = "rbLong";
            this.rbLong.Size = new System.Drawing.Size(49, 17);
            this.rbLong.TabIndex = 3;
            this.rbLong.Text = "Long";
            this.rbLong.UseVisualStyleBackColor = true;
            // 
            // rbString
            // 
            this.rbString.AutoSize = true;
            this.rbString.Checked = true;
            this.rbString.Location = new System.Drawing.Point(102, 12);
            this.rbString.Name = "rbString";
            this.rbString.Size = new System.Drawing.Size(52, 17);
            this.rbString.TabIndex = 1;
            this.rbString.TabStop = true;
            this.rbString.Text = "String";
            this.rbString.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Use values as:";
            // 
            // rbInt
            // 
            this.rbInt.AutoSize = true;
            this.rbInt.Location = new System.Drawing.Point(179, 12);
            this.rbInt.Name = "rbInt";
            this.rbInt.Size = new System.Drawing.Size(58, 17);
            this.rbInt.TabIndex = 2;
            this.rbInt.Text = "Integer";
            this.rbInt.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(21, 65);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(312, 20);
            this.txtInput.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(296, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ingrese el Numero de Tracking a calcular el digito verificador:";
            // 
            // txtCheckDigit
            // 
            this.txtCheckDigit.Location = new System.Drawing.Point(361, 236);
            this.txtCheckDigit.Name = "txtCheckDigit";
            this.txtCheckDigit.Size = new System.Drawing.Size(60, 20);
            this.txtCheckDigit.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(358, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Digito Ver:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Numero de Tracking sin digito verificador:";
            // 
            // txtInput2
            // 
            this.txtInput2.Location = new System.Drawing.Point(21, 236);
            this.txtInput2.Name = "txtInput2";
            this.txtInput2.Size = new System.Drawing.Size(312, 20);
            this.txtInput2.TabIndex = 14;
            // 
            // txtCheckResult2
            // 
            this.txtCheckResult2.Location = new System.Drawing.Point(432, 236);
            this.txtCheckResult2.Name = "txtCheckResult2";
            this.txtCheckResult2.ReadOnly = true;
            this.txtCheckResult2.Size = new System.Drawing.Size(82, 20);
            this.txtCheckResult2.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(432, 220);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Resultado:";
            // 
            // VerhoeffCheckDigitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 280);
            this.Controls.Add(this.txtCheckResult2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtInput2);
            this.Controls.Add(this.txtCheckDigit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rbIntArray);
            this.Controls.Add(this.rbLong);
            this.Controls.Add(this.rbString);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbInt);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.txtCheckResult1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtResultWithCheckDigit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCalculateCheckDigit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VerhoeffCheckDigitForm";
            this.Text = "Verhoeff Check Digit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalculateCheckDigit;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.TextBox txtResultWithCheckDigit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCheckResult1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbIntArray;
        private System.Windows.Forms.RadioButton rbLong;
        private System.Windows.Forms.RadioButton rbString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbInt;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCheckDigit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInput2;
        private System.Windows.Forms.TextBox txtCheckResult2;
        private System.Windows.Forms.Label label7;
    }
}

