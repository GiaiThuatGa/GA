using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace GA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private class Tram
        {
            public string MaTram;
            public double Longitude;
            private string _Longitude;
            public string getLongitude() { return _Longitude; }
            public void setLongitude(string t) { _Longitude = t; }

            public double Latitude;
            private string _Latitude;
            public string getLatitude() { return _Latitude; }
            public void setLatitude(string t) { _Latitude = t; }
            public double DoCao;
            public int sotramtoidaphuduoc;
            public bool daxet;
        }

        struct Pair
        {
            public Tram tram1;
            public Tram tram2;
            public double KhoangCach;
        }

        class Area
        {
            public List<Tram> tramlancans = new List<Tram>();
        }

        List<Tram> trams = new List<Tram>();//quan the ban dau
        List<Pair> D = new List<Pair>();//khoang cach 2 tram
        List<Pair> quanthe = new List<Pair>();//luu cac cap tram thich nghi trong lancan
        List<Tram> tams = new List<Tram>();
        List<Area> vunglancan = new List<Area>();

        Tram tramBatdau;
        Bitmap drawArea;
        Graphics g;

        private void Swap(Tram a, Tram b)
        {
            Tram temp = a;
            a = b;
            b = temp;
        }
        private void SapxepTram()
        {
            int n = trams.Count;
            for(int i = 0; i < n; i++)
            {
                for(int j = i+1; j < n -1; j++)
                {
                    if(trams[i].Latitude > trams[j].Latitude)
                    {
                        Swap(trams[i], trams[j]);
                    }else if(trams[i].Latitude == trams[j].Latitude && trams[i].Longitude > trams[j].Longitude)
                    {
                        Swap(trams[i], trams[j]);
                    }
                }
            }
        }

        private void XacdinhVungLanCan()
        {
            int n = trams.Count;
            for(int i = 0; i < n-6; i++)
            {
                for(int j = i; j < 6+i; j++)
                {
                    Area area = new Area();
                    area.tramlancans.Add(trams[j]);
                    vunglancan.Add(area);
                }
            }
        }

        private void VeVungLanCan()
        {
            //int n = vunglancan.Count;
            //for(int i = 0; i < n; i++)
            //{
                for(int j = 0; j < 6; j++)
                {
                    g.FillEllipse(Brushes.Blue, AnhXaToaDoSangWinForm(vunglancan[0].tramlancans[j].Latitude), AnhXaToaDoSangWinForm(vunglancan[0].tramlancans[j].Longitude), 5, 5);
                }
            //}
        }

        private int AnhXaToaDoSangWinForm(double toado)
        {
            toado = Math.Round(toado, 3);
            string[] t = toado.ToString().Split('.');
            if (Convert.ToInt32(t[1]) < 10) t[1] += "00";
            else if (Convert.ToInt32(t[1]) < 100) t[1] += "0";
            return Convert.ToInt32(t[1]);
        }

        private void btnReadText_Click(object sender, EventArgs e)
        {
            trams.Clear();
            PictureBox ptb = new PictureBox();
            ptb.Height = 1000;
            ptb.Width = 1000;
            drawArea = new Bitmap(ptb.Width, ptb.Height);
            ptb.Image = drawArea;
            
            g = Graphics.FromImage(drawArea);
            string[] lines = File.ReadAllLines(Application.StartupPath +@"\Inp.txt");
            int n = lines.Count();
            for(int i=0;i<n; i++)
            {
                string[] temp = lines[i].Split('\t');
                Tram temp1 = new Tram();
                temp1.MaTram = temp[0];
                temp1.Longitude = Double.Parse(temp[1]);
                temp1.Latitude = Double.Parse(temp[2]);
                temp1.DoCao = Double.Parse(temp[3]);
                //g.FillEllipse(Brushes.Blue, AnhXaToaDoSangWinForm(temp1.Latitude),AnhXaToaDoSangWinForm(temp1.Longitude), 5, 5);
                trams.Add(temp1);
            }
            
            SapxepTram();
            XacdinhVungLanCan();
            VeVungLanCan();
            pnlbitmap.Controls.Add(ptb);
            //LayTramBanDau();
            MessageBox.Show("Đọc thành công");
            
        }

        private void TimVungLanCan(Tram begin)
        {
            
        }

        const int P01 = 43;
        const int Lc = 0;
        const int Lf1 = 0;
        const int Ld = 0;
        const int G1 = 18;
        const int Pi2 = -72;
        const int G2 = 0;
        const int Lf2 = 0;

        const double hm = 1.5;
        const int f = 2100;
        const int fc = f;
        const int cm = 3;//cua dense Urban

        private void DuavaoQuanThe(Pair pair)
        {
            if(!quanthe.Contains(pair)) quanthe.Add(pair);
        }

        private void LayTramBanDau()
        {
            Random r = new Random();
            int n = r.Next(0, trams.Count);
            tramBatdau = trams[n];
        }

        private void MaHoaToaDo(Tram tram)
        {
            string[] kinhdo = Convert.ToString(tram.Longitude).Split('.');
            int kd = Convert.ToInt32(kinhdo[1]);
            string bin = Convert.ToString(kd, 2).PadLeft(17, '0');
            tram.setLongitude(bin);

            string[] vido = Convert.ToString(tram.Latitude).Split('.');
            int vd = Convert.ToInt32(vido[1]);
            bin = Convert.ToString(vd, 2).PadLeft(17, '0');
            tram.setLatitude(bin);
        } 

        
        private void GiaiMaToaDo(Tram tram)
        {
            char[] kd = tram.getLongitude().ToCharArray();
            char[] vd = tram.getLatitude().ToCharArray();
            double _kd = 0;
            double _vd = 0;
            for(int i = 16; i > 0; i--)
            {
                _kd += (int)(kd[i]-48) * Math.Pow(2, 16-i);
                _vd += (int)(vd[i] - 48) * Math.Pow(2, 16 - i);
            }
            string KD = "106." + _kd.ToString();
            string VD = "20." + _vd.ToString();
            tram.Longitude = Convert.ToDouble(KD);
            tram.Latitude = Convert.ToDouble(VD);
        }

        private double HeSoHieuChinhDoCaoAnTenDiDong()
        {
            return (3.2 * Math.Pow(Math.Log10(11.75 * hm), 2) - 4.97);
        }
        private double BanKinhPhu(Tram tram)
        {
            int Lp = P01 - Lc - Lf1 - Ld + G1 - Pi2 + G2 - Lf2;
            double d = Math.Pow(10,
                (Lp - 46.3 - 33.9 * Math.Log10(f) + 13.82 * Math.Log10(tram.DoCao)
                + HeSoHieuChinhDoCaoAnTenDiDong() - cm) / (44.9 - 6.55 * Math.Log10(tram.DoCao)));
            return d;
        }

        private double KhoangCachHaiTram(Tram tram1, Tram tram2)
        {
            return (Math.Acos(Math.Sin(tram1.Latitude * Math.PI / 180) * Math.Sin(tram2.Latitude * Math.PI / 180) + Math.Cos(tram1.Latitude * Math.PI / 180) * Math.Cos(tram2.Latitude * Math.PI / 180) * Math.Cos((-1 * tram1.Longitude * Math.PI / 180) - (-1 * tram2.Longitude * Math.PI / 180)))) * (20000000 / Math.PI) / 1000;
        }

        private bool DieuKienDung(List<Tram> cons)
        {
            double bk1 = 0;
            double bk2 = 0;
            double KC = 0;
            bool flag = true;
            int dem;
            foreach(Tram con in cons)
            {
                dem = 0;
                GiaiMaToaDo(con);
                for (int i = 0; i < 6; i++)
                {
                    bk1 = BanKinhPhu(con);
                    bk2 = BanKinhPhu(vunglancan[0].tramlancans[i]);
                    KC = KhoangCachHaiTram(con, vunglancan[0].tramlancans[i]);
                    bool f = ((bk1 + bk2) > KC && KC > (2 / 3 * (bk1 + bk2)));
                    if (f == true) dem++;
                    flag &= f;
                }
                if (flag == true)
                {
                    Random rd = new Random();
                    con.MaTram = "3HP" + rd.Next(10000, 999999).ToString();
                    trams.Add(con);
                    StreamWriter file = new System.IO.StreamWriter(Application.StartupPath + @"\Oupt.txt");
                    string temp = con.MaTram + " " + con.Longitude + " " + con.Latitude + " " + con.DoCao;
                    file.WriteLine(temp);
                    file.Close();
                    MessageBox.Show("Tìm kiếm tuyệt đối thành công");
                    return true;
                }
                else con.sotramtoidaphuduoc = dem;
            }
            int vt = 0;
            int n = cons.Count;
            for(int i = 1; i < n; i++)
            {
                if (cons[i].sotramtoidaphuduoc > cons[vt].sotramtoidaphuduoc) vt = i;
            }
            if(!tams.Contains(cons[vt]))
            tams.Add(cons[vt]);
            return false;
        }

        private void btnTimTram_Click(object sender, EventArgs e)
        {
            //Them khoang cach cua moi 2 tram trong quan the
            int n = trams.Count;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i!=j)
                    {
                        Pair temp = new Pair();
                        temp.tram1 = trams[i];
                        temp.tram2 = trams[j];
                        double khoangCach = KhoangCachHaiTram(trams[i],trams[j]) ;
                        temp.KhoangCach = khoangCach;
                        D.Add(temp);
                    }
                }
            n = D.Count;
            for (int i = 0; i < n; i++)
            {
                if (D[i].KhoangCach > BanKinhPhu(D[i].tram1) + BanKinhPhu(D[i].tram2)){
                    Pair temp = new Pair();
                    temp.tram1 = D[i].tram1;
                    temp.tram2 = D[i].tram2;
                    DuavaoQuanThe(temp);//dua vao quan the cap tram thich nghi de lai ghep va dot bien
                }
            }

            //Ma hoa toa do cua cac cap trong quan the
            n = quanthe.Count;
            for(int i = 0; i<n; i++)
            {
                MaHoaToaDo(quanthe[i].tram1);
                MaHoaToaDo(quanthe[i].tram2);
            }

            Boolean[] daxet = new Boolean[n];
            for(int i = 0; i < n; i++)
            {
                daxet[i] = false;
            }

            Boolean daTimduocConTotnhat = false;

            for(int k = 1; k <= n; k++)
            {
                //Khoi tao id khong trung lap
                Random random = new Random();
                int id ;
                do
                {
                    id = random.Next(0,n-1);
                } while (daxet[id] == true);
                daxet[id] = true;

                Tram con = new Tram();
                List<Tram> _temp = new List<Tram>();
                Boolean[] pos = new Boolean[n];
                for (int i = 0; i < n; i++)
                {
                    pos[i] = false;
                }
                int dosaulap = 1;
                do
                {
                    _temp.Clear();
                    Tram cha = quanthe[id].tram1;
                    Tram me = quanthe[id].tram2;
                    string kd_nst_cha = cha.getLongitude();
                    string vd_nst_cha = cha.getLatitude();
                    string kd_nst_me = me.getLongitude();
                    string vd_nst_me = me.getLatitude();

                char temp;
                char[] temp_kd_cha = kd_nst_cha.ToCharArray();
                char[] temp_vd_cha = vd_nst_cha.ToCharArray();
                char[] temp_kd_me = kd_nst_me.ToCharArray();
                char[] temp_vd_me = vd_nst_me.ToCharArray();

                int dosaulap = 1;
                List<Tram> _temp = new List<Tram>();
                do
                {
                    _temp.Clear();
                    Random r = new Random();
                    int _pos;
                    do
                    {
                        _pos = r.Next(0, 16);
                    } while (pos[_pos] == true);
                    pos[_pos] = true;

                    for (int i = _pos; i < 17; i++)
                    {
                        temp = temp_kd_me[i];
                        temp_kd_me[i] = temp_kd_cha[i];
                        temp_kd_cha[i] = temp;

                        temp = temp_vd_me[i];
                        temp_vd_me[i] = temp_vd_cha[i];
                        temp_vd_cha[i] = temp;
                    }
                    kd_nst_cha = "";
                    vd_nst_cha = "";
                    kd_nst_me = "";
                    vd_nst_me = "";
                    for (int i = 0; i < 17; i++)
                    {
                        kd_nst_cha += temp_kd_cha[i].ToString();
                        vd_nst_cha += temp_vd_cha[i].ToString();
                        kd_nst_me += temp_kd_me[i].ToString();
                        vd_nst_me += temp_vd_me[i].ToString();
                    }

                    Tram tc = new Tram();
                    tc.DoCao = 27;
                    tc.setLatitude(vd_nst_cha);
                    tc.setLongitude(kd_nst_cha);
                    if (!_temp.Contains(tc)) _temp.Add(tc); //Them con vao

                    Tram tm = new Tram();
                    tm.DoCao = 27;
                    tm.setLatitude(vd_nst_me);
                    tm.setLongitude(kd_nst_me);
                    if (!_temp.Contains(tm)) _temp.Add(tm);

                    double pm = 0.01;
                    for (int i = 0; i < 17; i++)
                    {
                        double pi = r.NextDouble() * 0.019;
                        if (pi < pm)
                        {
                            temp_kd_cha[i] = (char)((1 - (int)(temp_kd_cha[i] - 48)) + 48);
                            temp_vd_cha[i] = (char)((1 - (int)(temp_vd_cha[i] - 48)) + 48);
                        }
                    }
                    for (int i = 0; i < 17; i++)
                    {
                        double pi = r.NextDouble() * 0.019;
                        if (pi < pm)
                        {
                            temp_kd_me[i] = (char)((1 - (int)(temp_kd_me[i] - 48)) + 48);
                            temp_vd_me[i] = (char)((1 - (int)(temp_vd_me[i] - 48)) + 48);
                        }
                    }

                    kd_nst_cha = "";
                    vd_nst_cha = "";
                    kd_nst_me = "";
                    vd_nst_me = "";
                    for (int i = 0; i < 17; i++)
                    {
                        kd_nst_cha += temp_kd_cha[i].ToString();
                        vd_nst_cha += temp_vd_cha[i].ToString();
                        kd_nst_me += temp_kd_me[i].ToString();
                        vd_nst_me += temp_vd_me[i].ToString();
                    }

                    Tram tc_dotbien = new Tram();
                    tc_dotbien.DoCao = 27;
                    tc_dotbien.setLatitude(vd_nst_cha);
                    tc_dotbien.setLongitude(kd_nst_cha);
                    if (!_temp.Contains(tc_dotbien)) _temp.Add(tc_dotbien);

                    Tram tm_dotbien = new Tram();
                    tm_dotbien.DoCao = 27;
                    tm_dotbien.setLatitude(vd_nst_me);
                    tm_dotbien.setLongitude(kd_nst_me);
                    if (!_temp.Contains(tm_dotbien)) _temp.Add(tm_dotbien);

                    daTimduocConTotnhat = DieuKienDung(_temp);
                    dosaulap++;

                } while (daTimduocConTotnhat==false && dosaulap <=17);
                if (daTimduocConTotnhat) return;
            }
            //xu ly mang tams
            int vt = 0;
            int m = tams.Count;
            for (int i = 1; i < n; i++)
            {
                if (tams[i].sotramtoidaphuduoc > tams[vt].sotramtoidaphuduoc) vt = i;
            }
            if(!trams.Contains(tams[vt])) trams.Add(tams[vt]);

        } 

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }
    }
}
