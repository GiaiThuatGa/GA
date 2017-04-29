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

        private class Tram
        {
            public String MaTram;
            public Double Longitude;
            private string _Longitude;
            public string getLongitude() { return _Longitude; }
            public void setLongitude(string t) { _Longitude = t; }

            public Double Latitude;
            private string _Latitude;
            public string getLatitude() { return _Latitude; }
            public void setLatitude(string t) { _Latitude = t; }
            public Double DoCao;
            public int sotramtoidaphuduoc;
        }

        struct Pair
        {
            public Tram tram1;
            public Tram tram2;
            public Double KhoangCach;
        }

        List<Tram> trams = new List<Tram>();
        List<Tram> lancans = new List<Tram>();
        List<Pair> D = new List<Pair>();//khoang cach 2 tram
        List<Pair> quanthe = new List<Pair>();
        List<Tram> tams = new List<Tram>();

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
            lancans = trams;
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
        const int cm = 3;//cua dense Urban
        
        private Double HeSoHieuChinhDoCaoAnTenDiDong()
        {
            return (1.1 * Math.Log10(f) - 0.7) * hm - (1.56 * Math.Log10(f) - 0.8);
        }
        private Double BanKinhPhu(Tram tram)
        {
            int Lp = P01 - Lc - Lf1 - Ld + G1 - Pi2 + G2 - Lf2;
            Double d = Math.Pow(10, (Lp - 46.3 - 33.9 * Math.Log10(f) + 13.82 * Math.Log10(tram.DoCao) + HeSoHieuChinhDoCaoAnTenDiDong() - cm) / ((44.9 - 6.55 * Math.Log10(tram.DoCao))));
            return d;
        }

        private void DuavaoQuanThe(Pair pair)
        {
            if(!quanthe.Contains(pair)) quanthe.Add(pair);
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
            Double _kd = 0;
            for(int i = 16; i > 0; i--)
            {
                _kd += (int)(kd[i]-48) * Math.Pow(2, 16-i);
            }
            
        }

        private Double KhoangCachHaiTram(Tram tram1, Tram tram2)
        {
            return (Math.Acos(Math.Sin(tram1.Latitude * Math.PI / 180) * Math.Sin(tram2.Latitude * Math.PI / 180) + Math.Cos(tram1.Latitude * Math.PI / 180) * Math.Cos(tram2.Latitude * Math.PI / 180) * Math.Cos((-1 * tram1.Longitude * Math.PI / 180) - (-1 * tram2.Longitude * Math.PI / 180)))) * (20000000 / Math.PI) / 1000;
        }

        private Boolean DieuKienDung(List<Tram> cons)
        {
            Double bk1 = 0;
            Double bk2 = 0;
            Double KC = 0;
            Boolean flag = true;
            int dem;
            foreach(Tram con in cons)
            {
                dem = 0;
                for (int i = 0; i < 6; i++)
                {
                    bk1 = BanKinhPhu(con);
                    bk2 = BanKinhPhu(lancans[i]);
                    KC = KhoangCachHaiTram(con, lancans[i]);
                    Boolean f = (bk1 + bk2 > KC && KC > 2 / 3 * (bk1 + bk2));
                    if (f == true) dem++;
                    flag &= f;
                }
                if (flag == true)
                {
                    trams.Add(con);
                    return flag;
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
            return flag;
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
                    DuavaoQuanThe(temp);
                }
            }
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

                    Random r = new Random();
                    int _pos;
                    do
                    {
                        _pos = r.Next(0, 16);
                    } while (pos[_pos] == true);
                    pos[_pos] = true;

                    char temp;
                    char[] temp_kd_cha = new char[17];
                    temp_kd_cha = kd_nst_cha.ToCharArray();
                    char[] temp_vd_cha = new char[17];
                    temp_vd_cha = vd_nst_cha.ToCharArray();

                    char[] temp_kd_me = new char[17];
                    temp_kd_me = kd_nst_me.ToCharArray();
                    char[] temp_vd_me = new char[17];
                    temp_vd_me = vd_nst_me.ToCharArray();

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
                    tc.setLatitude(vd_nst_cha);
                    tc.setLongitude(kd_nst_cha);
                    if (!_temp.Contains(tc)) _temp.Add(tc);

                    Tram tm = new Tram();
                    tm.setLatitude(vd_nst_me);
                    tm.setLongitude(kd_nst_me);
                    if (!_temp.Contains(tm)) _temp.Add(tm);

                    Double pm = 0.01;
                    for (int i = 0; i < 17; i++)
                    {
                        Double pi = r.NextDouble() * 0.02;
                        if (pi < pm)
                        {
                            temp_kd_cha[i] = (char)((1 - (int)(temp_kd_cha[i] - 48)) + 48);
                            temp_vd_cha[i] = (char)((1 - (int)(temp_vd_cha[i] - 48)) + 48);
                        }
                    }
                    for (int i = 0; i < 17; i++)
                    {
                        Double pi = r.NextDouble() * 0.02;
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

                    Tram tc_biendoigen = new Tram();
                    tc_biendoigen.setLatitude(vd_nst_cha);
                    tc_biendoigen.setLongitude(kd_nst_cha);
                    if (!_temp.Contains(tc_biendoigen)) _temp.Add(tc_biendoigen);

                    Tram tm_biendoigen = new Tram();
                    tm_biendoigen.setLatitude(vd_nst_me);
                    tm_biendoigen.setLongitude(kd_nst_me);
                    if (!_temp.Contains(tm_biendoigen)) _temp.Add(tm_biendoigen);
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
