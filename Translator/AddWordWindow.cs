using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Translator
{
    public partial class AddWordWindow : Form
    {
        public event Action<string, string> OnOKButtonPressed;

        public string firstLanguage
        {
            get
            {
                return firstLanguageLabel.Text;
            }
            set
            {
                firstLanguageLabel.Text = value;
            }
        }
        public string secondLanguage
        {
            get
            {
                return secondLanguageLabel.Text;
            }
            set
            {
                secondLanguageLabel.Text = value;
            }
        }

        public AddWordWindow()
        {
            InitializeComponent();
        }

        private void englishWord_TextChanged(object sender, EventArgs e)
        {
            ValidateText(sender as TextBox, errorProvider1);
        }

        private void ValidateText(TextBox sender, ErrorProvider errorProvider)
        {
            if (sender.Text == "")
            {
                errorProvider.SetError(sender, "Cannot be empty!");
            }
            else if (sender.Text.Any(x => !char.IsLetter(x)))
            {
                char c = sender.Text.First(x => !char.IsLetter(x));
                errorProvider.SetError(sender, char.IsWhiteSpace(c) ? "Word contains whitespaces!" : $"{c} is not a letter!");
            }
            else
            {
                errorProvider.Clear();
            }
        }

        private void englishWord_Leave(object sender, EventArgs e)
        {
            ValidateText(sender as TextBox, errorProvider1);
        }

        private void polishWord_Leave(object sender, EventArgs e)
        {
            ValidateText(sender as TextBox, errorProvider2);
        }

        private void polishWord_TextChanged(object sender, EventArgs e)
        {
            ValidateText(sender as TextBox, errorProvider2);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ValidateText(englishWord, errorProvider1);
            ValidateText(polishWord, errorProvider2);
            if (errorProvider1.GetError(englishWord) == "" && errorProvider2.GetError(polishWord) == "")
            {
                OnOKButtonPressed(englishWord.Text, polishWord.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Validation error!");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
