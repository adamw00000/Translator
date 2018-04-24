using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Translator
{
    public partial class mainWindow : Form
    {
        Dictionary<string, string> innerDictionary = new Dictionary<string, string>();

        int sortingColumn = 0;
        bool ascending = true;

        public mainWindow()
        {
            InitializeComponent();
            CreateFontMenu();
        }

        #region layoutHelpers
        private void ResizeDictionaryColumns()
        {
            dictionary.Columns[0].Width = (int)(0.5 * dictionary.ClientSize.Width);
            dictionary.Columns[1].Width = (int)(0.5 * dictionary.ClientSize.Width);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            ResizeDictionaryColumns();
        }
        #endregion

        #region load
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult ret = loadFile.ShowDialog();

            if (ret != DialogResult.OK)
                return;

            StreamReader reader = new StreamReader(loadFile.OpenFile());

            ReadDictionaryFromStreamReader(reader);
        }

        private void ReadDictionaryFromStreamReader(StreamReader reader)
        {
            innerDictionary = new Dictionary<string, string>();
            dictionary.Items.Clear();
            bool firstLine = true;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                bool flag = false;
                foreach (var c in line)
                    if (!char.IsLetter(c) && !(c == ' '))
                        flag = true;

                if (flag) continue;

                string[] columns = line.Split(' ');
                if (columns[0] == "" || columns[1] == "")
                    continue;

                if (firstLine)
                {
                    firstLine = false;
                    dictionary.Columns[0].Text = columns[0].ToLower();
                    dictionary.Columns[1].Text = columns[1].ToLower();
                    continue;
                }

                AddNewItemToDictionary(columns[0], columns[1]);
            }

            ResizeDictionaryColumns();

            reader.Close();
        }

        public void AddNewItemToDictionary(string word1, string word2)
        {
            if (!innerDictionary.ContainsKey(word1))
            {
                word1 = word1.ToLower();
                word2 = word2.ToLower();
                string[] item = { word1, word2 };
                innerDictionary.Add(word1, word2);
                var listViewItem = new ListViewItem(item);
                dictionary.Items.Add(listViewItem);
            }
        }
        #endregion

        #region export
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult ret = saveFile.ShowDialog();

            if (ret != DialogResult.OK)
            {
                return;
            }
            
            StreamWriter writer = new StreamWriter(saveFile.FileName);

            writer.WriteLine($"{dictionary.Columns[0].Text}  {dictionary.Columns[1].Text}");
            foreach(var item in innerDictionary)
            {
                writer.WriteLine($"{item.Key} {item.Value}");
            }

            writer.Close();
        }
        #endregion

        #region translation
        private void translateButton_Click(object sender, EventArgs e)
        {
            translatedBox.ResetText();

            string word = "";
            int pos = 0;

            while (pos < untranslatedBox.Text.Length)
            {
                char c = untranslatedBox.Text[pos];
                if (!char.IsLetter(c))
                {
                    CheckWord(word);
                    translatedBox.AppendText(c.ToString());
                    word = "";
                }
                else
                {
                    word += c;
                }
                pos++;
            }
            CheckWord(word);
        }

        private void CheckWord(string word)
        {
            if (innerDictionary.ContainsKey(word.ToLower()))
            {
                translatedBox.AppendText(innerDictionary[word.ToLower()]);
            }
            else
            {
                translatedBox.SelectionStart = translatedBox.TextLength;
                translatedBox.SelectionLength = 0;

                translatedBox.SelectionColor = Color.FromArgb(255, 0, 0);
                translatedBox.AppendText(word);
                translatedBox.SelectionColor = translatedBox.ForeColor;
            }
        }
        #endregion

        #region delete
        private void dictionary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach(ListViewItem item in dictionary.SelectedItems)
                {
                    dictionary.Items.Remove(item);
                    innerDictionary.Remove(item.SubItems[0].Text);
                }
            }
        }
        #endregion

        #region sorting
        private void dictionary_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == sortingColumn)
            {
                if (ascending)
                {
                    dictionary.ListViewItemSorter = new ListViewItemComparerDescending(sortingColumn);
                }
                else
                {
                    dictionary.ListViewItemSorter = new ListViewItemComparerAscending(sortingColumn);
                }
                ascending = !ascending;
            }
            else
            {
                sortingColumn = e.Column;
                dictionary.ListViewItemSorter = new ListViewItemComparerAscending(sortingColumn);
                ascending = true;
            }
            dictionary.Sort();
        }
        #endregion

        #region newWord
        private void newWordButton_Click(object sender, EventArgs e)
        {
            AddWordWindow addWordWindow = new AddWordWindow();
            addWordWindow.firstLanguage = dictionary.Columns[0].Text;
            addWordWindow.secondLanguage = dictionary.Columns[1].Text;
            addWordWindow.OnOKButtonPressed += AddNewItemToDictionary;
            addWordWindow.ShowDialog();
        }
        

        private void addWordMenuEntry_Click(object sender, EventArgs e)
        {
            AddWordWindow addWordWindow = new AddWordWindow();
            addWordWindow.englishWord.Text = addWordMenuEntry.Text.Split(' ')[1];
            addWordWindow.englishWord.Enabled = false;
            addWordWindow.firstLanguage = dictionary.Columns[0].Text;
            addWordWindow.secondLanguage = dictionary.Columns[1].Text;
            addWordWindow.OnOKButtonPressed += AddNewItemToDictionary;
            addWordWindow.ShowDialog();
        }
        #endregion

        #region rightClickMenu
        private void translatedBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (translatedBox.TextLength == 0) return;

            int pos = translatedBox.GetCharIndexFromPosition(e.Location);

            if (!char.IsLetter(translatedBox.Text[pos]))
                return;

            int start = pos, end = pos;
            while (start > -1 && char.IsLetter(translatedBox.Text[start]))
            {
                start--;
            }
            start++;
            while (end < translatedBox.TextLength && char.IsLetter(translatedBox.Text[end]))
            {
                end++;
            }

            string word = translatedBox.Text.Substring(start, end - start);

            if (innerDictionary.ContainsValue(word.ToLower()))
                return;

            addWordMenu.Items[0].Text = $"Add {word}";

            addWordMenu.Show(translatedBox.PointToScreen(e.Location));
        }
        
        private void translatedBox_MouseUp(object sender, MouseEventArgs e)
        {
            addWordMenu.Focus();
        }
        #endregion

        #region fonts
        private void boldButton_Click(object sender, EventArgs e)
        {

            Font f = untranslatedBox.SelectionFont;
            Font newFont = new Font(f, (f.Bold ? 0 : FontStyle.Bold) | (f.Italic ? FontStyle.Italic : 0) | (f.Underline ? FontStyle.Underline : 0));

            boldButton.BackColor = newFont.Bold ? SystemColors.ActiveCaption : SystemColors.Control;
            ChangeFontStyle(newFont);
        }

        private void ChangeFontStyle(Font newFont)
        {
            int oldSelectionStart = untranslatedBox.SelectionStart;

            untranslatedBox.SelectAll();
            untranslatedBox.SelectionFont = newFont;

            untranslatedBox.DeselectAll();
            untranslatedBox.SelectionStart = oldSelectionStart;
        }

        private void italicButton_Click(object sender, EventArgs e)
        {
            Font f = untranslatedBox.SelectionFont;
            Font newFont = new Font(f, (f.Bold ? FontStyle.Bold : 0) | (f.Italic ? 0 : FontStyle.Italic) | (f.Underline ? FontStyle.Underline : 0));

            italicButton.BackColor = newFont.Italic ? SystemColors.ActiveCaption : SystemColors.Control;
            ChangeFontStyle(newFont);
        }

        private void underlineButton_Click(object sender, EventArgs e)
        {
            Font f = untranslatedBox.SelectionFont;
            Font newFont = new Font(f, (f.Bold ? FontStyle.Bold : 0) | (f.Italic ? FontStyle.Italic : 0) | (f.Underline ? 0 : FontStyle.Underline));

            underlineButton.BackColor = newFont.Underline ? SystemColors.ActiveCaption : SystemColors.Control;
            ChangeFontStyle(newFont);
        }
        
        private void fontColorButton_Click(object sender, EventArgs e)
        {
            int oldSelectionStart = untranslatedBox.SelectionStart;

            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;

            untranslatedBox.SelectAll();
            untranslatedBox.SelectionColor = colorPicker.Color;
            untranslatedBox.DeselectAll();

            untranslatedBox.SelectionStart = oldSelectionStart;
        }

        private void backgroundColorButton_Click(object sender, EventArgs e)
        {
            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;

            untranslatedBox.BackColor = colorPicker.Color;
        }

        void CreateFontMenu()
        {
            foreach (FontFamily f in FontFamily.Families)
            {
                fontButton.Items.Add(f.Name);
            }
        }

        private void fontButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            Font f = untranslatedBox.SelectionFont;
            Font newFont = new Font(fontButton.SelectedItem as string, f.SizeInPoints, (f.Bold ? FontStyle.Bold : 0) | (f.Italic ? 0 : FontStyle.Italic) | (f.Underline ? FontStyle.Underline : 0));

            ChangeFontStyle(newFont);
            fontButton.Text = fontButton.SelectedItem as string;
        }
        #endregion

        #region dragNDrop
        private void dictionary_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = e.Data.GetData(DataFormats.FileDrop) as string[];
            StreamReader reader = new StreamReader(fileName[0]);

            ReadDictionaryFromStreamReader(reader);
        }

        private void dictionary_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        #endregion
    }

    #region sortHelpers
    class ListViewItemComparerAscending : IComparer
    {
        private int col;
        public ListViewItemComparerAscending(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }
    class ListViewItemComparerDescending : IComparer
    {
        private int col;
        public ListViewItemComparerDescending(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return -String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }
    #endregion
}
