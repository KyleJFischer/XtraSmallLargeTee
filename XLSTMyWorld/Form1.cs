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
        private string xsltData = "";

        public Form1()
        {
            InitializeComponent();
        }

        internal void CreateEditorWindow()
        {
            if (EditorWindow == null)
            {
                EditorWindow = new Editor();

                EditorWindow.MdiParent = this;
            }
            EditorWindow.Show();
        }

        internal void CreateXMLWindow()
        {
            if (dataWindow == null)
            {
                dataWindow = new DataWindow();

                dataWindow.MdiParent = this;
            }
            dataWindow.Show();
        }

        internal void CreateOutputWindow()
        {
            if (outputWindow == null)
            {
                outputWindow = new OutputWindow();

                outputWindow.MdiParent = this;
            }
            outputWindow.Show();
            outputWindow.updateText(xsltData, xmlData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
            CreateEditorWindow();
            CreateOutputWindow();
            CreateXMLWindow();
        }

        private void fIleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateOutputWindow();
        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateEditorWindow();
        }

        private void xMLLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateXMLWindow();
        }

        public bool updateXMLData(string text)
        {
            xmlData = text;
            return triggerUpdate();
        }

        public bool updateXsltData(string text)
        {
            xsltData = text;
            return triggerUpdate();
        }

        internal bool triggerUpdate()
        {
            if (outputWindow != null)
            {
                return outputWindow.updateText(xsltData, xmlData);
            }
            return false;
        }

        public void removeWindow(object window)
        {
            var isEditorWindow = window as Editor;
            if (isEditorWindow != null)
            {
                EditorWindow = null;
                return;
            }
            var isOutputWindow = window as OutputWindow;
            if (isOutputWindow != null)
            {
                outputWindow = null;
                return;
            }
            var isDataWindow = window as DataWindow;
            if (isDataWindow != null)
            {
                dataWindow = null;
                return;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            EditorWindow.saveFile();
        }
    }
}
