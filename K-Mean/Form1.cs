using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Mean
{
    public partial class Form1 : Form
    {
        private KMeanCore _kmeanCore = new KMeanCore();

        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                _kmeanCore.fileName = op.FileName;
                Screen.Text += _kmeanCore.fileName + "\n";
                _kmeanCore.ReadDataFromExel();
                Screen.Text += "Read file completed!\n";
            }
        }

        public void ShowData()
        {
            for (int i = 0; i < _kmeanCore.ListItem.Count; i++)
            {
                Screen.Text += _kmeanCore.ListItem[i].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_kmeanCore.fileName != string.Empty)
            {
                _kmeanCore.ProcessKMnean(5);
                Screen.Text += _kmeanCore.ToString();
            }
            else
            {
                Screen.Text += "Please select file data!\n";
            }
        }
    }
}
