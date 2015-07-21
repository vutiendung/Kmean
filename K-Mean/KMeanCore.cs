using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace K_Mean
{
    class KMeanCore
    {
        public List<VectorDefine> ListItem = new List<VectorDefine>();
        int[] MarkUp;
        public string fileName;

        public void ReadDataFromExel()
        {
            DataTable dt;
            dt = MoFileExcel.GetDatasetFromExcel(fileName); 
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
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
            }
        }

        public void ProcessKMnean(int k)
        {
            List<VectorDefine> ListCluster = new List<VectorDefine>();
            MarkUp=new int[ListItem.Count];
            for (int i = 0; i < ListItem.Count; i++)
            {
                MarkUp[i] = -1;
            }

            int step = ListItem.Count / k;
            for (int i = 0; i < ListItem.Count; )
            {
                ListCluster.Add(ListItem[i]);
                i += step;
            }

            bool change = true;
            while (change)
            {
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
                    if (NewVector.EqualWithOtherVector(OldVector))
                    {
                        change = true;
                    }
                }
            }
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < ListItem.Count; i++)
            {
                s += "User ["+i+"] nhóm :"+MarkUp[i]+"\n";
            }

            return s;
        }
    }
}
