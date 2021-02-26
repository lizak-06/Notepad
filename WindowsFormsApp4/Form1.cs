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

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public bool isFileChanged = false;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Текстовы документ(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Текстовы документ(*.txt)|*.txt|All files(*.*)|*.*";
            
            fontDialog1.ShowColor = true;//позволяет при изменении выбора шрифта изменять его цвет

            colorDialog1.FullOpen = true; //палитра 

            this.StartPosition = FormStartPosition.CenterScreen;// расположение формы 
        }
      
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isFileChanged = true; //изменение текста

            //if (!isFileChanged)   //при изменении текта в названии бдет добавляться *
            //{
            //    this.Text = this.Text.Replace('*', ' ');
            //    isFileChanged = true;
            //    this.Text = "*" + this.Text;
            //}
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText(richTextBox1.SelectedText);
            richTextBox1.Copy();
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength != 0)
            {
                DialogResult res = MessageBox.Show("Вы хотите сохранить данные изменения?", "Блокнот", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                        MessageBox.Show("Файл сохранен");
                    }

                    richTextBox1.Clear();
                }
                if (res == DialogResult.No)
                {
                    richTextBox1.Clear();
                }
            }
        } 
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
            MessageBox.Show("Файл сохранен");
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void Блокнот_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (richTextBox1.TextLength != 0)
            {
                DialogResult res = MessageBox.Show("Вы хотите сохранить данные изменения?", "Блокнот", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (res == DialogResult.Yes)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            richTextBox1.SelectionFont = fontDialog1.Font;//шрифт
            richTextBox1.ForeColor = fontDialog1.Color;//цфет шрифта
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            richTextBox1.BackColor = colorDialog1.Color;//цвет блокнота 
        }
    }
}
