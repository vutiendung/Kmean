﻿using System;
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
                _kmeanCore.ReadDataFromExel(Screen);
                Screen.Text += "Read file completed!\n";
                button1.Enabled = false;
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
                _kmeanCore.numberCluster = 6;
                _kmeanCore.ProcessKMnean();
                Screen.Text += _kmeanCore.ToString();
            }
            else
            {
                Screen.Text += "Please select file data!\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _kmeanCore.NormelizeData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<DataTable> dt = new List<DataTable>();
            dt=_kmeanCore.ExportData();
            for (int i = 0; i < dt.Count; i++)
            {
                Screen.Text += "Group {"+i+"}\t";
                for(int m=0;m<dt[i].Rows.Count;m++)
                    for (int n = 0; n < dt[i].Columns.Count; n++)
                    {
                        Screen.Text += "  " + dt[i].Rows[m][n].ToString() ;
                        if (n == dt[i].Columns.Count - 1)
                        {
                            Screen.Text += "\n";
                        }
                    }
            }

            return;
        }
    }
}
