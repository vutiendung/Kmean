﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace K_Mean
{
    class KMeanCore
    {
        public List<VectorDefine> ListItem = new List<VectorDefine>();
        int[] MarkUp;
        public string fileName;
        public int numberCluster;

        public void ReadDataFromExel(RichTextBox Screen)
        {
            int total = 0;
            DataTable dt;
            dt = MoFileExcel.GetDatasetFromExcel(fileName);
            total = dt.Rows.Count;
            int step = total / 100;
            if (step == 0)
                step = 1;
            if (dt != null)
            {
                Screen.Text += "Loading data\n";
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    if((int)i*100/total%step==0)
                    {
                        Screen.Text += "█";
                        SendKeys.Flush();
                    }
                    VectorDefine vtTG = new VectorDefine();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        try
                        {
                            vtTG.Item.Add(int.Parse(dt.Rows[i][j].ToString()));
                        }
                        catch
                        {
                            break;
                        }
                    }
                    ListItem.Add(vtTG);
                    vtTG = null;
                }
                Screen.Text += "\n";
            }
        }

        /// <summary>
        /// ham su ly chinh cua kmean
        /// </summary>
        /// <param name="k"></param>
        public void ProcessKMnean()
        {
            List<VectorDefine> ListCluster = new List<VectorDefine>();
            MarkUp=new int[ListItem.Count];
            for (int i = 0; i < ListItem.Count; i++)
            {
                MarkUp[i] = -1;
            }

            int step = ListItem.Count / numberCluster;
            for (int i = 0; i < ListItem.Count; )
            {
                ListCluster.Add(ListItem[i]);
                i += step;
            }
            int soLanLap = 0;
            bool change = true;
            while (change)
            {
                soLanLap++;
                if (soLanLap == 100)
                {
                    break;
                }
                for (int i = 0; i < ListItem.Count; i++)
                {
                    double minDistance = ListItem[i].getDistance(ListCluster[0]);
                    int pos=0;
                    for (int j = 1; j < ListCluster.Count; j++)
                    {
                        double tg = ListItem[i].getDistance(ListCluster[j]);
                        if (tg < minDistance)
                        {
                            minDistance = tg;
                            pos = j;
                        }
                    }
                    MarkUp[i] = pos;
                }

                //tinh lai toa do tam
                change = false;
                for (int i = 0; i < ListCluster.Count; i++)
                {
                    VectorDefine OldVector = ListCluster[i];
                    int dem = 0;
                    VectorDefine NewVector = new VectorDefine();

                    for (int j = 0; j < ListItem.Count; j++)
                    {
                        if (j == i)
                        {
                            dem++;
                            NewVector.AddNewValue(ListItem[i]);
                        }
                    }

                    NewVector.Average(dem);

                    if (!NewVector.EqualWithOtherVector(OldVector))
                    {
                        change = true;
                    }
                }
            }
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < numberCluster;i++ )
            {
                s += "Nhom "+(i+1)+"\n";
                for (int j = 0; j < ListItem.Count; j++)
                {
                    if(MarkUp[j]==i)
                        s += "\tUser "+(j+1)+"\n";
                }
            }

            return s;
        }

        /// <summary>
        /// Binh thuong hoa du lieu
        /// </summary>
        public void NormelizeData()
        {
            for (int i = 0; i < ListItem.Count; i++)
            {
                ListItem[i].Normalize();
            }
        }

        /// <summary>
        /// Show data to screen
        /// </summary>
        /// <param name="s"></param>
        public void PrintData(RichTextBox s)
        {
            for (int i = 0; i < ListItem.Count; i++)
            {
                for (int j = 0; j < ListItem[i].Item.Count; j++)
                {
                    s.Text += ListItem[i].Item[j] + " ";
                    if (j == ListItem[i].Item.Count - 1)
                    {
                        s.Text += "\n";
                    }
                }
            }
        }

        /// <summary>
        /// Export data to list table
        /// </summary>
        public List<DataTable> ExportData()
        {
            List<DataTable> dt = new List<DataTable>();

            for (int i = 0; i < numberCluster; i++)
            {
                DataTable tg = new DataTable();
                for (int j = 0; j < ListItem[i].Item.Count; j++)
                {
                    tg.Columns.Add();
                }
                for (int j = 0; j < ListItem.Count; j++)
                {
                    if (MarkUp[j] == i)
                    {
                        tg.Rows.Add(ListItem[j].getObject());
                    }
                }
                dt.Add(tg);
                tg = null;
            }
            return dt;
        }
    }
}
