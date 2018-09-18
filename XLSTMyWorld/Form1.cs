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
    public partial class Form1 : Form
    {
        public Editor EditorWindow = null;
        public OutputWindow outputWindow = null;
        public DataWindow dataWindow = null;

        private string xmlData = "";
        private string xsltStuff = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
        }

        private void fIleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outputWindow == null)
            {
                outputWindow = new OutputWindow();

                outputWindow.MdiParent = this;
            }
            outputWindow.Show();
        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditorWindow == null)
            {
                EditorWindow = new Editor();

                EditorWindow.MdiParent = this;
            }
            EditorWindow.Show();
        }

        private void xMLLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataWindow == null)
            {
                dataWindow = new DataWindow();

                dataWindow.MdiParent = this;
            }
            dataWindow.Show();
        }

        public bool updateXMLData(string text)
        {
            xmlData = text;
            return triggerUpdate();
        }

        public bool updateXsltData(string text)
        {
            xsltStuff = text;
            return triggerUpdate();
        }
        internal bool triggerUpdate()
        {
            if (outputWindow != null)
            {
                return outputWindow.updateText(xsltStuff, xmlData);
            }
            return false;
        }
    }
}
