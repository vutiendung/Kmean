using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Mean
{
    class VectorDefine
    {
        public List<double> Item = new List<double>();

        public double getDistance(VectorDefine input)
        {
            double tong = 0;
            for (int i = 0; i < Item.Count; i++)
            {
                tong += (Item[i] - input.Item[i]) * (Item[i] - input.Item[i]);
            }
            return Math.Sqrt(tong);
        }

        public override string ToString()
        {
            string s="";
            for (int i = 0; i < Item.Count; i++)
            {
                s += Item[i] + "\t";
            }
            s += "\n";
            return s;
        }

        public void AddNewValue(VectorDefine input)
        {
            for (int i = 0; i < Item.Count; i++)
            {
                Item[i] += input.Item[i];
            }
        }

        public bool EqualWithOtherVector(VectorDefine input)
        {
            if (Item.Count != input.Item.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < input.Item.Count; i++)
                {
                    if (input.Item[i] != Item[i])
                        return false;
                }
                return true;
            }
        }

        public void Average(int k)
        {
            for (int i = 0; i < Item.Count; i++)
            {
                Item[i] /= k;
            }
        }

        public void Normalize()
        {
            double colSum=0;
            for(int i=0;i<Item.Count;i++)
            {
                colSum += Item[i];
            }
            double mean = (double)colSum / Item.Count;

            double sum = 0;
            for (int i = 0; i < Item.Count; i++)
            {
                sum += (Item[i] - mean) * (Item[i] - mean);
            }

            double sd = sum / Item.Count;

            //binh thuong hoa du lieu o doan nay

            for (int i = 0; i < Item.Count; i++)
            {
                Item[i]=(Item[i] - mean) / sd;
            }

        }
    }
}
