using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication11
{
    public partial class ToolBlock : Form
    {
        Cognex.VisionPro.ToolBlock.CogToolBlock tb;
        string SaveDoc;
        public ToolBlock(Cognex.VisionPro.ToolBlock.CogToolBlock tb_, string SaveDoc_)
        {
            SaveDoc=SaveDoc_;
            InitializeComponent();
            tb = tb_;
            this.cogToolBlockEditV21.Subject = tb;

        }

        private void ToolBlock_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cognex.VisionPro.CogSerializer.SaveObjectToFile(tb,SaveDoc);
            
        }

        private void cogToolBlockEditV21_Load(object sender, EventArgs e)
        {

        }
    }
}
