using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public struct Tram
        {
         public String MaTram;
         public Double Longitude;
         public Double Latitude;
         public Double DoCao;
        }

        struct Pair
        {
            public String MaTram1;
            public String MaTram2;
            public Double KhoangCach;
        }

        
        List<Tram> trams = new List<Tram>();
        List<Pair> D = new List<Pair>();
       
        private void btnReadText_Click(object sender, EventArgs e)
        {
            String[] lines = System.IO.File.ReadAllLines(Application.StartupPath +@"\Inp.txt");
            
            for(int i=0;i<lines.Count(); i++)
            {
                String[] temp = lines[i].Split(' ');
                Tram temp1 = new Tram();
                temp1.MaTram = temp[0];
                temp1.Longitude = Double.Parse(temp[1]);
                temp1.Latitude = Double.Parse(temp[2]);
                temp1.DoCao = Double.Parse(temp[3]);
                trams.Add(temp1);
            }
            MessageBox.Show("Đọc thành công");
        }

        private void btnWriteText_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + @"\Outp.txt");
            for (int i = 0; i < trams.Count; i++)
            {
                String temp = trams[i].MaTram + " " + trams[i].Longitude + " " + trams[i].Latitude + " " + trams[i].DoCao;
                file.WriteLine(temp);
            }
            file.Close();
            MessageBox.Show("Ghi thành công");
        }

        const int P01 = 43;
        const int Lc = 0;
        const int Lf1 = 0;
        const int Ld = 0;
        const int G1 = 18;
        const int Pi2 = -72;
        const int G2 = 0;
        const int Lf2 = 0;

        const Double hm = 1.5;
        const int f = 2100;
        const int fc = f;
        const int cm = 3;
        public Double HeSoHieuChinhDoCaoAnTenDiDong()
        {
            return (1.1 * Math.Log10(f) - 0.7) * hm - (1.56 * Math.Log10(f) - 0.8);
        }
        public void TinhBanKinhPhu(Tram tram)
        {
            int Lp = P01 - Lc - Lf1 - Ld + G1 - Pi2 + G2 - Lf2;
            Double d = Math.Pow(10, (Lp - 46.3 - 33.9 * Math.Log10(f) + 13.82 * Math.Log10(tram.DoCao) + HeSoHieuChinhDoCaoAnTenDiDong() - cm) / ((44.9 - 6.55 * Math.Log10(tram.DoCao))));

        }

        private void btnTinhKC_Click(object sender, EventArgs e)
        {
            int n = trams.Count;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i!=j)
                    {
                        Pair temp = new Pair();
                        temp.MaTram1 = trams[i].MaTram;
                        temp.MaTram2 = trams[j].MaTram;
                        double khoangCach = (Math.Acos(Math.Sin(trams[i].Latitude*Math.PI/180)*Math.Sin(trams[j].Latitude*Math.PI/180)+Math.Cos(trams[i].Latitude*Math.PI/180)*Math.Cos(trams[j].Latitude*Math.PI/180)*Math.Cos((-1*trams[i].Longitude*Math.PI/180)-(-1*trams[j].Longitude*Math.PI/180))))*( 20000000/Math.PI)/1000     ;
                        temp.KhoangCach = khoangCach;
                        D.Add(temp);
                    }
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
