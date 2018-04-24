namespace Translator
{
    partial class AddWordWindow
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
            this.englishWord = new System.Windows.Forms.TextBox();
            this.polishWord = new System.Windows.Forms.TextBox();
            this.firstLanguageLabel = new System.Windows.Forms.Label();
            this.secondLanguageLabel = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.OKButton = new System.Windows.Forms.Button();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // englishWord
            // 
            this.englishWord.Location = new System.Drawing.Point(152, 32);
            this.englishWord.Name = "englishWord";
            this.englishWord.Size = new System.Drawing.Size(100, 20);
            this.englishWord.TabIndex = 0;
            this.englishWord.TextChanged += new System.EventHandler(this.englishWord_TextChanged);
            this.englishWord.Leave += new System.EventHandler(this.englishWord_Leave);
            // 
            // polishWord
            // 
            this.polishWord.Location = new System.Drawing.Point(152, 69);
            this.polishWord.Name = "polishWord";
            this.polishWord.Size = new System.Drawing.Size(100, 20);
            this.polishWord.TabIndex = 1;
            this.polishWord.TextChanged += new System.EventHandler(this.polishWord_TextChanged);
            this.polishWord.Leave += new System.EventHandler(this.polishWord_Leave);
            // 
            // firstLanguageLabel
            // 
            this.firstLanguageLabel.AutoSize = true;
            this.firstLanguageLabel.Location = new System.Drawing.Point(39, 35);
            this.firstLanguageLabel.Name = "firstLanguageLabel";
            this.firstLanguageLabel.Size = new System.Drawing.Size(41, 13);
            this.firstLanguageLabel.TabIndex = 2;
            this.firstLanguageLabel.Text = "English";
            this.firstLanguageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // secondLanguageLabel
            // 
            this.secondLanguageLabel.AutoSize = true;
            this.secondLanguageLabel.Location = new System.Drawing.Point(39, 72);
            this.secondLanguageLabel.Name = "secondLanguageLabel";
            this.secondLanguageLabel.Size = new System.Drawing.Size(35, 13);
            this.secondLanguageLabel.TabIndex = 3;
            this.secondLanguageLabel.Text = "Polish";
            this.secondLanguageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(177, 107);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // errorProvider2
            // 
            this.errorProvider2.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider2.ContainerControl = this;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(42, 107);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddWordWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 142);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.secondLanguageLabel);
            this.Controls.Add(this.firstLanguageLabel);
            this.Controls.Add(this.polishWord);
            this.Controls.Add(this.englishWord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWordWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add new word";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox polishWord;
        private System.Windows.Forms.Label firstLanguageLabel;
        private System.Windows.Forms.Label secondLanguageLabel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        public System.Windows.Forms.Button OKButton;
        public System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.TextBox englishWord;
    }
}