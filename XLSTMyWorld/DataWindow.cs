using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XLSTMyWorld

{
    public partial class DataWindow : Form
    {
        public static Dictionary<string, string> xmlPaths = new Dictionary<string, string>();
        public static string mainKey = "";

        public DataWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browse();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var actualListBox = (ListBox)sender;
            var key = (string)actualListBox.SelectedItem;
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            textBox1.Text = xmlPaths[key];
            makeMainData(key);
        }

        private void makeMainData(string key)
        {
            var form1 = (Form1)this.MdiParent;
            form1.updateXMLData(xmlPaths[key]);
            mainKey = key;
        }

        private void DataWindow_Load(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.AcceptsReturn = true;
            textBox1.AcceptsTab = true;
            openFileDialog1.Filter = "XML Files (*.xml)|*.xml;|txt files (*.txt)|*.txt|All files (*.*)|*.*";
        }

        public void updateFont(Font font)
        {
            textBox1.Font = font;
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browse();
        }

        internal void browse()
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                var xml = sr.ReadToEnd();
                var name = openFileDialog1.SafeFileName;
                sr.Close();
                xmlPaths.Add(name, xml);
                listBox1.Items.Clear();
                foreach (var item in xmlPaths)
                {
                    listBox1.Items.Add(item.Key);
                }
                textBox1.Text = xml;
                makeMainData(name);
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = textBox1.Font;
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                updateFont(fontDialog1.Font);
            }
        }

        private void DataWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form1 = (Form1)MdiParent;
            form1.removeWindow(this);
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripComboBox2_TextUpdate(object sender, EventArgs e)
        {
            
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox2.Text.ToUpper())
            {
                case "TREE":
                    textBox1.Visible = false;
                    treeView1.Visible = true;
                    break;

                case "TEXT":
                    textBox1.Visible = true;
                    treeView1.Visible = false;
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // SECTION 1. Create a DOM Document and load the XML data into it.
                XmlDocument dom = new XmlDocument();
                dom.LoadXml(textBox1.Text);

                // SECTION 2. Initialize the TreeView control.
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(new TreeNode(dom.DocumentElement.Name));
                TreeNode tNode = new TreeNode();
                tNode = treeView1.Nodes[0];

                // SECTION 3. Populate the TreeView with the DOM nodes.
                AddNode(dom.DocumentElement, tNode);
                treeView1.CollapseAll();
                ExpandOne(tNode);
            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            var form1 = (Form1)this.MdiParent;
            form1.updateXMLData(textBox1.Text);


        }

        private void ExpandOne(TreeNode tNode)
        {
            tNode.Expand();
            var pastNode = tNode;
            var temp = tNode.FirstNode;
            while (temp != null)
            {
                temp.Expand();
                if (temp.FirstNode == null)
                {
                    temp.ExpandAll();
                }
                pastNode = temp;
                temp = temp.FirstNode;
            }
        }

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;

            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                // Here you need to pull the data from the XmlNode based on the
                // type of node, whether attribute values are required, and so forth.
                inTreeNode.Text = (inXmlNode.OuterXml).Trim();
            }
        }
    }
}
