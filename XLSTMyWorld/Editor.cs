using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XLSTMyWorld
{
    public partial class Editor : Form
    {
        

        public Editor()
        {
            InitializeComponent();
        }

        public void updateFont(Font font)
        {
            textBox1.Font = font;
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.AcceptsReturn = true;
            textBox1.AcceptsTab = true;

            openFileDialog1.Filter = "XLS Files (*.xslt, *.xslt)|*.xslt; *.xslt|txt files (*.txt)|*.txt|All files (*.*)|*.*";

        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                textBox1.Text = sr.ReadToEnd();
                this.Text = $"Editor {openFileDialog1.FileName}";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var form1 = (Form1)this.MdiParent;
            form1.updateXsltData(this.textBox1.Text);
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = textBox1.Font;
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                updateFont(fontDialog1.Font);
               
            }
        }
    }
}
