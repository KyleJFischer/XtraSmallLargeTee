using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XLSTMyWorld
{
    public partial class OutputWindow : Form
    {
        public OutputWindow()
        {
            InitializeComponent();
        }

        private void OutputWindow_Load(object sender, EventArgs e)
        {

        }

        public bool updateText(string xlst, string xml)
        {
            string output = String.Empty;
            try
            {
                using (StringReader srt = new StringReader(xlst)) // xslInput is a string that contains xsl
                using (StringReader sri = new StringReader(xml)) // xmlInput is a string that contains xml
                {
                    using (XmlReader xrt = XmlReader.Create(srt))
                    using (XmlReader xri = XmlReader.Create(sri))
                    {
                        XslCompiledTransform xslt = new XslCompiledTransform();
                        xslt.Load(xrt);
                        using (StringWriter sw = new StringWriter())
                        using (XmlWriter xwo = XmlWriter.Create(sw, xslt.OutputSettings)) // use OutputSettings of xsl, so it can be output as HTML
                        {
                            xslt.Transform(xri, xwo);
                            output = sw.ToString();
                            webBrowser1.DocumentText = output;
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return true;
        }

        private void OutputWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            var form1 = (Form1)MdiParent;
            form1.removeWindow(this);
        }
    }
}
