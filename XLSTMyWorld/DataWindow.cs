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
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                var xml = sr.ReadToEnd();
                var name = openFileDialog1.FileName;
                sr.Close();
                xmlPaths.Add(name, xml);
                listBox1.Items.Clear();
                foreach(var item in xmlPaths)
                {
                    listBox1.Items.Add(item.Key);
                }
            }
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
    }
}
