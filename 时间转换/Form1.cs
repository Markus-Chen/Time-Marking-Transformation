using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 时间转换
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        public void clear()//清除所有数据
        {
            foreach (Control i in groupBox1.Controls)
            {
                if (i is TextBox)
                {
                    i.Text = "";
                }
            }
            foreach (Control i in groupBox2.Controls)
            {
                if (i is TextBox)
                {
                    i.Text = "";
                }
            }
            foreach (Control i in groupBox3.Controls)
            {
                if (i is TextBox)
                {
                    i.Text = "";
                }
            }
            foreach (Control i in groupBox4.Controls)
            {
                if (i is TextBox)
                {
                    i.Text = "";
                }
            }
        }

        private void buttonCL1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void buttonCL2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void buttonCL3_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void buttonCL4_Click(object sender, EventArgs e)
        {
            clear();
        }

        public int ETyear,ETmonth,ETday,EThour,ETminute,ETsecond,GPSTweek,GPSTsecond,JD,JDsecond,DOYyear,DOY,DOYsecond;

        private void textETyear_TextChanged(object sender, EventArgs e)
        {

        }

        public double MJD;

        public int DOYday(int y,int m,int d)//年积日
        {
            int doyday=0,flag=0;
            if((y%400)==0||((y%4)==0&&(y%100)!=0))
                flag=1;
            switch(m)
            {
                case 1: doyday = d; break;
                case 2: doyday = d + 31; break;
                case 3: doyday = d + flag + 59; break;
                case 4: doyday = d + flag + 90; break;
                case 5: doyday = d + flag + 120; break;
                case 6: doyday = d + flag + 151; break;
                case 7: doyday = d + flag + 181; break;
                case 8: doyday = d + flag + 212; break;
                case 9: doyday = d + flag + 243; break;
                case 10: doyday = d + flag + 273; break;
                case 11: doyday = d + flag + 304; break;
                case 12: doyday = d + flag + 334; break;
                default: Console.WriteLine("Wrong!"); break;
            }
            return doyday;
        }
        public int DOYsec(int h, int m, int s)//日内秒
        {
            int sec=0;
            sec = 3600 * h + 60 * m + s;
            return sec;
        }
        public int DOY2ETday(int y, int doyday)
        {
            int m, d,flag=0;
            if ((y % 400) == 0 || ((y % 4) == 0 && (y % 100) != 0))
                flag = 1;
            if (doyday<=31){ m = 1; d = doyday; }
            else if (doyday <= 59 + flag) { m = 2; d = doyday - 31; }
            else if (doyday <= 90 + flag) { m = 3; d = doyday - 59; }
            else if (doyday <= 120 + flag) { m = 4; d = doyday - 90; }
            else if (doyday <= 151 + flag) { m = 5; d = doyday - 120; }
            else if (doyday <= 181 + flag) { m = 6; d = doyday - 151; }
            else if (doyday <= 212 + flag) { m = 7; d = doyday - 181; }
            else if (doyday <= 243 + flag) { m = 8; d = doyday - 212; }
            else if (doyday <= 273 + flag) { m = 9; d = doyday - 243; }
            else if (doyday <= 304 + flag) { m = 10; d = doyday - 273; }
            else if (doyday <= 334 + flag) { m = 11; d = doyday - 304; }
            else { m = 12; d = doyday - 334; }
            return (m * 100 + d);
        }
        public int JD2ET(int jd, int jdsec)
        {
            int b,c,d,e,D,M,Y;
            double a;
            a=jd+jdsec/86400.0;
            b=(int)(a+0.5)+1537;
            c=(int)((b-122.1)/365.25);
            d=(int)(365.25*c);
            e=(int)((b-d)/30.600);
            D=(int)(b-d-(int)(30.6001*e)+(a-(int)a));
            M=e-1-12*(e/14);
            Y=c-4715-(M+7)/10;
            return (D + M * 100 + Y * 10000);
        }

        private void buttonET_Click(object sender, EventArgs e)
        {
            ETyear = int.Parse(textETyear.Text);
            ETmonth = int.Parse(textETmonth.Text);
            ETday = int.Parse(textETday.Text);
            EThour = int.Parse(textEThour.Text);
            ETminute = int.Parse(textETminute.Text);
            ETsecond = int.Parse(textETsecond.Text);
            //ET2DOY
            DOYyear = ETyear;
            DOY = DOYday(ETyear, ETmonth, ETday);
            DOYsecond = DOYsec(EThour, ETminute, ETsecond);
            textDOYyear.Text = DOYyear.ToString();
            textDOY.Text = DOY.ToString();
            textDOYsecond.Text = DOYsecond.ToString();
            //ET2JD
            JD =(int)((int)(365.25*ETyear)+(int)(30.6001*(ETmonth+1))+ETday+EThour/24.0+1720981.5);
            JDsecond = (DOYsecond+43200)%86400;
            MJD = JD + JDsecond / 86400.0 - 2400000.5;
            textJD.Text = JD.ToString();
            textJDsecond.Text = JDsecond.ToString();
            textMJD.Text = MJD.ToString("f6");
            //JD2GPST
            GPSTweek = (int)(MJD - 44244) / 7;
            GPSTsecond = (int)(MJD - 44244) % 7*86400+DOYsecond;
            textGPSTweek.Text=GPSTweek.ToString();
            textGPSTsecond.Text = GPSTsecond.ToString();
        }

        private void button5GPST_Click(object sender, EventArgs e)
        {
            int etime;
            int GPSTweek = int.Parse(textGPSTweek.Text);
            int GPSTsecond = int.Parse(textGPSTsecond.Text);
            //GPST2JD
            MJD = GPSTweek * 7 +44244+ GPSTsecond / 86400.0;
            DOYsecond = GPSTsecond % 86400;
            JDsecond = (DOYsecond + 43200) % 86400;
            JD = (int)(MJD + 2400000.5);
            textJD.Text = JD.ToString();
            textJDsecond.Text = JDsecond.ToString();
            textMJD.Text = MJD.ToString("f6");
            //JD2ET
            etime = JD2ET(JD, JDsecond);
            ETyear =etime / 10000;
            ETmonth = (etime - ETyear * 10000) / 100;
            ETday = etime - ETyear * 10000 - ETmonth * 100;
            EThour = DOYsecond / 3600;
            ETminute = (DOYsecond - EThour * 3600) / 60;
            ETsecond = DOYsecond - EThour * 3600 - ETminute * 60;
            textETyear.Text = ETyear.ToString();
            textETmonth.Text = ETmonth.ToString();
            textETday.Text = ETday.ToString();
            textEThour.Text = EThour.ToString();
            textETminute.Text = ETminute.ToString();
            textETsecond.Text = ETsecond.ToString();
            //ET2DOY
            DOYyear = ETyear;
            DOY = DOYday(ETyear, ETmonth, ETday);
            textDOYyear.Text = DOYyear.ToString();
            textDOY.Text = DOY.ToString();
            textDOYsecond.Text = DOYsecond.ToString();
        }

        private void buttonJD_Click(object sender, EventArgs e)
        {
            int JD = int.Parse(textJD.Text);
            int JDsecond = int.Parse(textJDsecond.Text);
            int etime;
            DOYsecond = (JDsecond + 43200) % 86400;
            //MJD
            MJD = JD + JDsecond / 86400.0 - 2400000.5;
            textMJD.Text = MJD.ToString("f6");
            //JD2GPST
            GPSTweek = (int)(MJD - 44244) / 7;
            GPSTsecond = (int)(MJD - 44244) % 7 * 86400 + DOYsecond;
            textGPSTweek.Text = GPSTweek.ToString();
            textGPSTsecond.Text = GPSTsecond.ToString();
            //JD2ET
            etime = JD2ET(JD, JDsecond);
            ETyear = etime / 10000;
            ETmonth = (etime - ETyear * 10000) / 100;
            ETday = etime - ETyear * 10000 - ETmonth * 100;
            EThour = DOYsecond / 3600;
            ETminute = (DOYsecond - EThour * 3600) / 60;
            ETsecond = DOYsecond - EThour * 3600 - ETminute * 60;
            textETyear.Text = ETyear.ToString();
            textETmonth.Text = ETmonth.ToString();
            textETday.Text = ETday.ToString();
            textEThour.Text = EThour.ToString();
            textETminute.Text = ETminute.ToString();
            textETsecond.Text = ETsecond.ToString();
            //ET2DOY
            DOYyear = ETyear;
            DOY = DOYday(ETyear, ETmonth, ETday);
            textDOYyear.Text = DOYyear.ToString();
            textDOY.Text = DOY.ToString();
            textDOYsecond.Text = DOYsecond.ToString();
        }

        private void buttonDOY_Click(object sender, EventArgs e)
        {
            int DOYyear = int.Parse(textDOYyear.Text);
            int DOY = int.Parse(textDOY.Text);
            int DOYsecond = int.Parse(textDOYsecond.Text);
            int etime;
            etime = DOY2ETday(DOYyear, DOY);
            //DOY2ET
            ETyear = DOYyear;
            ETmonth = etime / 100;
            ETday = etime - ETmonth * 100;
            EThour = DOYsecond / 3600;
            ETminute = (DOYsecond - EThour * 3600) / 60;
            ETsecond = DOYsecond - EThour * 3600 - ETminute * 60;
            textETyear.Text = ETyear.ToString();
            textETmonth.Text = ETmonth.ToString();
            textETday.Text = ETday.ToString();
            textEThour.Text = EThour.ToString();
            textETminute.Text = ETminute.ToString();
            textETsecond.Text = ETsecond.ToString();
            //ET2JD
            JD = (int)((int)(365.25 * ETyear) + (int)(30.6001 * (ETmonth + 1)) + ETday + EThour / 24.0 + 1720981.5);
            JDsecond = (DOYsecond + 43200) % 86400;
            MJD = JD + JDsecond / 86400.0 - 2400000.5;
            textJD.Text = JD.ToString();
            textJDsecond.Text = JDsecond.ToString();
            textMJD.Text = MJD.ToString("f6");
            //JD2GPST
            GPSTweek = (int)(MJD - 44244) / 7;
            GPSTsecond = (int)(MJD - 44244) % 7 * 86400 + DOYsecond;
            textGPSTweek.Text = GPSTweek.ToString();
            textGPSTsecond.Text = GPSTsecond.ToString();
        }

        private void buttonToday_Click(object sender, EventArgs e)
        {
            ETyear = DateTime.Now.Year;
            ETmonth = DateTime.Now.Month;
            ETday = DateTime.Now.Day;
            EThour = DateTime.Now.Hour-8;
            ETminute = DateTime.Now.Minute;
            ETsecond = DateTime.Now.Second;
            textETyear.Text = ETyear.ToString();
            textETmonth.Text = ETmonth.ToString();
            textETday.Text = ETday.ToString();
            textEThour.Text = EThour.ToString();
            textETminute.Text = ETminute.ToString();
            textETsecond.Text = ETsecond.ToString();
            //ET2DOY
            DOYyear = ETyear;
            DOY = DOYday(ETyear, ETmonth, ETday);
            DOYsecond = DOYsec(EThour, ETminute, ETsecond);
            textDOYyear.Text = DOYyear.ToString();
            textDOY.Text = DOY.ToString();
            textDOYsecond.Text = DOYsecond.ToString();
            //ET2JD
            JD = (int)((int)(365.25 * ETyear) + (int)(30.6001 * (ETmonth + 1)) + ETday + EThour / 24.0 + 1720981.5);
            JDsecond = (DOYsecond + 43200) % 86400;
            MJD = JD + JDsecond / 86400.0 - 2400000.5;
            textJD.Text = JD.ToString();
            textJDsecond.Text = JDsecond.ToString();
            textMJD.Text = MJD.ToString("f6");
            //JD2GPST
            GPSTweek = (int)(MJD - 44244) / 7;
            GPSTsecond = (int)(MJD - 44244) % 7 * 86400 + DOYsecond;
            textGPSTweek.Text = GPSTweek.ToString();
            textGPSTsecond.Text = GPSTsecond.ToString();
        }
       
    }
}
