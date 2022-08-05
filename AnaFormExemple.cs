using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Threading;
using System.Net.Sockets;
using AForge.Video;
using System.Diagnostics;
using System.Net;

namespace Yer_istasyon
{
    public partial class form1 : Form
    {
        Thread thr;
        Microsoft.Office.Interop.Excel.Application objExcel = new Microsoft.Office.Interop.Excel.Application();
        public static Microsoft.Office.Interop.Excel.Workbook objbook;
        Microsoft.Office.Interop.Excel.Worksheet objSheet;
        // Main Variables
        int satir;
        bool connection = false;
        int port; //Port number of server
        string message; //Message to send
        string message2="0*0*0"; //Message to send
        Socket clientSocket;
        MJPEGStream VideoStream;
        Bitmap bmp;
        //public VideoFileWriter FileWriter = new VideoFileWriter();

        int sayac = 1;
        string[] veri3 = new string[7]{ "88898.00", "88897.00", "89495.00", "88897.00", "88845.00", "89155.00", "88894.00" };//Pa
        string[] veri4 = new string[7]{ "302.06", "305.07", "302.43", "301.56", "303.32", "307.15", "301.54" };//m
        string[] veri5 = new string[7]{ "0.17", "0.15", "0.17", "0.16", "0.15", "0.12", "0.17" };//m/s
        string[] veri6 = new string[7] { "24.5", "24.6", "24.3", "24.8", "24.7", "24.8", "24.2" };//Sıcaklık
        string[] veri7 = new string[7] { "12.1", "12.1", "12.0", "12.0", "12.1", "12.1", "12.0" };//V
        string[] veri8 = new string[7] { "37.8652173590674", "37.8652173590574", "37.8652173590258", "37.8652173590195", "37.8652173590741", "37.8652173590625", "37.8652173590631" };//N
        string[] veri9 = new string[7] { "32.422268680724363", "32.422268680724377", "32.422268680724324", "32.422268680724346", "32.422268680724372", "32.422268680724471", "32.422268680724354" };//S
        string[] veri10 = new string[7] { "1087.6", "1087.0", "1087.3", "1087.2", "1087.1", "1087.8", "1087.4" };//m
        
        public static double lat = 37.8652173590674, longt = 32.422268680724386; 
        int zoom = 5;
        public static bool tam_ekran_control = true;
        string takimNo = "397723"; //Kazgan Takım No 54206
        public int gyro_x=0, gyro_y=0, gyro_z=0;
        int hour = Convert.ToUInt16(DateTime.Now.Hour.ToString());
        int minute = Convert.ToUInt16(DateTime.Now.Minute.ToString());
        int saniye = Convert.ToUInt16(DateTime.Now.Second.ToString());
        int chartcount;
        public form1()
        {
            InitializeComponent();
            label27.Hide();
            VideoStream = new MJPEGStream("http://192.168.43.178:8080/?action=stream");
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.ChartAreas[1].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.ChartAreas[2].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.ChartAreas[3].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.ChartAreas[4].AxisX.LabelStyle.Format = "HH:mm:ss";
          

            this.chart1.Series[0].Points.AddXY(DateTime.Now, veri3[0]);
            this.chart1.Series[1].Points.AddXY(DateTime.Now, veri4[0]);
            this.chart1.Series[2].Points.AddXY(DateTime.Now, veri5[0]);
            this.chart1.Series[3].Points.AddXY(DateTime.Now, veri6[0]);
            this.chart1.Series[4].Points.AddXY(DateTime.Now, veri7[0]);
            Color siyah = Color.FromArgb(40, 40, 40), acksiyah = Color.FromArgb(70, 70, 70), Beyaz = Color.White ;
            sag_panel.BackColor = siyah;
            alt_panel.BackColor = siyah;
            solpanel.BackColor = siyah;
            ustpanel.BackColor = siyah;
            pnlsgalt1.BackColor = acksiyah;
            pnlsgalt2.BackColor = acksiyah;
            pnlsgalt3.BackColor = acksiyah;
            pnlsgalt4.BackColor = acksiyah;
            pnlsgalt5.BackColor = acksiyah;
            pnlsgalt6.BackColor = acksiyah;
            pnlsgalt7.BackColor = acksiyah;
            pnlsgalt8.BackColor = acksiyah;
            tabPage1.BackColor = Beyaz;
            
            this.BackColor = Color.FromArgb(30,30,30);//Color.FromArgb(205, 201 ,201); //Color.FromArgb(202 ,225 ,255); //Color.FromArgb(154, 192, 205);159 182 205
            
            dataGridView1.BackgroundColor = this.BackColor;
            dataGridView1.ForeColor = Color.Black;
            tabPage2.BackColor = Beyaz;
            chart1.BackColor = Beyaz;
            tabPage1.Text = "Grafikler";
            tabPage2.Text = "Exel Formatında Veriler";

            VideoStream.NewFrame += GetNewFrame;
            //pcbxVideo.BackColor = Color.LimeGreen;
            // this.TransparencyKey = Color.LimeGreen;
        }

        void GetNewFrame(object sender, NewFrameEventArgs eventarg)
        {
            bmp = (Bitmap)eventarg.Frame.Clone();
            pcbxVideo.Image = bmp;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Environment.Exit(0);
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            objExcel.Visible = false;
            objbook = objExcel.Workbooks.Add(System.Reflection.Missing.Value);
            objSheet = (Microsoft.Office.Interop.Excel.Worksheet)objbook.Worksheets.get_Item(1);

            chart1.ChartAreas[3].AxisX.ScrollBar.Enabled = false;
            chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            chart1.ChartAreas[1].AxisX.ScrollBar.Enabled = false;
            chart1.ChartAreas[2].AxisX.ScrollBar.Enabled = false;
            chart1.ChartAreas[4].AxisX.ScrollBar.Enabled = false;
            txtboxHostname.Text = GetLocalIPAddress(); //"192.168.43.178";
            txtbxPort.Text = "12345";
            port = Convert.ToInt32(txtbxPort.Text);
            txtBaglanti.Hide();
            chart1.ForeColor = Color.White;
         /*   this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 11);
            for (int i = 0; i <= 16; i++)
               this.dataGridView1.Columns[i].HeaderCell.Style.Font= new Font("Tahoma", 11);*/

            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 11);
            for (int i = 0; i <= 16; i++)
                this.dataGridView1.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 11);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            /*foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 11.0F, GraphicsUnit.Pixel);
            }*/
            GL.ClearColor(Color.FromArgb(40, 40, 40));//(this.BackColor);
            btnDurdur.Enabled = false;
           // btnvideodurdur.Enabled = false;

            tarih_lbl.ForeColor = Color.White;
            tarih_lbl.Text = DateTime.Today.Date.ToString("dd:MM:yyyy");
            saat_lbl.ForeColor = Color.White;
            if (minute < 10 && hour < 10)
                saat_lbl.Text = "0" + hour.ToString() + ":0" + minute.ToString();
            else if (hour < 10)
                saat_lbl.Text = "0" + hour.ToString() + ":" + minute.ToString();
            else if (minute < 10)
                saat_lbl.Text = hour.ToString() + ":0" + minute.ToString();
            else
                saat_lbl.Text = hour.ToString() + ":" + minute.ToString();
        }


        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);//sonradan yazdık
        }
        private void silindir(float step, float topla, float radius, float dikey1, float dikey2)
        {
            float eski_step = 0.1f;
            GL.Begin(PrimitiveType.Quads);//Y EKSEN CIZIM DAİRENİN
            while (step <= 360)
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(255, 255, 255));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(255, 255, 255));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(255, 255, 255));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(255, 255, 255));


                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 2) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 2) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            step = eski_step;
            topla = step;
            while (step <= 180)// UST KAPAK
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(250, 250, 200));


                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);

                GL.Vertex3(ciz1_x, dikey1, ciz1_y);
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);
                step += topla;
            }
            step = eski_step;
            topla = step;
            while (step <= 180)//ALT KAPAK
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(250, 250, 200));

                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey2, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();
        }
        private void koni(float step, float topla, float radius1, float radius2, float dikey1, float dikey2)
        {
            float eski_step = 0.1f;
            GL.Begin(PrimitiveType.Lines);//Y EKSEN CIZIM DAİRENİN
            while (step <= 360)
            {
                if (step < 45)
                    GL.Color3(1.0, 1.0, 1.0);
                else if (step < 90)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 135)
                    GL.Color3(1.0, 1.0, 1.0);
                else if (step < 180)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 225)
                    GL.Color3(1.0, 1.0, 1.0);
                else if (step < 270)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 315)
                    GL.Color3(1.0, 1.0, 1.0);
                else if (step < 360)
                    GL.Color3(Color.FromArgb(69, 101, 165));


                float ciz1_x = (float)(radius1 * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius1 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius2 * Math.Cos(step * Math.PI / 180F));
                float ciz2_y = (float)(radius2 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            step = eski_step;
            topla = step;
            while (step <= 180)// UST KAPAK
            {
                if (step < 45)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 90)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 135)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 180)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 225)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 270)
                    GL.Color3(Color.FromArgb(250, 250, 200));
                else if (step < 315)
                    GL.Color3(Color.FromArgb(69, 101, 165));
                else if (step < 360)
                    GL.Color3(Color.FromArgb(250, 250, 200));


                float ciz1_x = (float)(radius2 * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius2 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey2, ciz1_y);

                float ciz2_x = (float)(radius2 * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius2 * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            step = eski_step;
            topla = step;
            GL.End();
        }
        private void Pervane(float yukseklik, float uzunluk, float kalinlik, float egiklik)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.FromArgb(69, 101, 165));
            GL.Vertex3(uzunluk, yukseklik, kalinlik);
            GL.Vertex3(uzunluk, yukseklik + egiklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik + egiklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik, kalinlik);

            GL.Color3(Color.FromArgb(69, 101, 165));
            GL.Vertex3(-uzunluk, yukseklik + egiklik, kalinlik);
            GL.Vertex3(-uzunluk, yukseklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik, -kalinlik);
            GL.Vertex3(0.0, yukseklik + egiklik, kalinlik);

            GL.Color3(Color.White);
            GL.Vertex3(kalinlik, yukseklik, -uzunluk);
            GL.Vertex3(-kalinlik, yukseklik + egiklik, -uzunluk);
            GL.Vertex3(-kalinlik, yukseklik + egiklik, 0.0);//+
            GL.Vertex3(kalinlik, yukseklik, 0.0);//-

            GL.Color3(Color.White);
            GL.Vertex3(kalinlik, yukseklik + egiklik, +uzunluk);
            GL.Vertex3(-kalinlik, yukseklik, +uzunluk);
            GL.Vertex3(-kalinlik, yukseklik, 0.0);
            GL.Vertex3(kalinlik, yukseklik + egiklik, 0.0);

            GL.End();

        }
        private void xyzEksen()
        {
            GL.Begin(PrimitiveType.Lines);

            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex3(-30.0, 0.0, 0.0);
            GL.Vertex3(30.0, 0.0, 0.0);


            GL.Color3(Color.Black);
            GL.Vertex3(0.0, 30.0, 0.0);
            GL.Vertex3(0.0, -30.0, 0.0);

            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex3(0.0, 0.0, 30.0);
            GL.Vertex3(0.0, 0.0, -30.0);

            GL.End();
        }

         private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            float step = 1.0f;
            float topla = step;
            float radius = 5.0f;
            float dikey1 = radius, dikey2 = -radius;
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(1.04f, 4 / 3, 1, 10000);
            Matrix4 lookat = Matrix4.LookAt(25, 0, 0, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.LoadMatrix(ref lookat);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Rotate(gyro_x, 1.0, 0.0, 0.0);//ÖNEMLİ
            GL.Rotate(gyro_z, 0.0, 1.0, 0.0);
            GL.Rotate(gyro_y, 0.0, 0.0, 1.0);

            //xyzEksen();

            silindir(step, topla, radius, 3, -5);
            silindir(0.01f, topla, 0.5f, 9, 9.7f);
            silindir(0.01f, topla, 0.1f, 5, dikey1 + 5);
            koni(0.01f, 0.01f, radius, 3.0f, 3, 5);
            koni(0.01f, 0.01f, radius, 2.0f, -5.0f, -10.0f);
            Pervane(9.0f, 11.0f, 0.2f, 0.5f);
     
            //GraphicsContext.CurrentContext.VSync = true;
            glControl1.SwapBuffers();
        }

        private void BtnBasla_Click(object sender, EventArgs e)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(GetLocalIPAddress(), port);
                thr = new Thread(job1);
                thr.Start();
                timer1.Start();
                btnDurdur.Enabled = true;
                BtnBasla.Enabled = false;
                connection = true;
                txtBaglanti.Text = "Connected";
            }
            catch(System.Net.Sockets.SocketException)
            {
                txtBaglanti.Show();
                txtBaglanti.Text = "Connection Failed";
            }
        }

        private void Btnvideobasla_Click(object sender, EventArgs e)
        {
            VideoStream.Start();
        }

        private void btnvideodurdur_Click(object sender, EventArgs e)
        {
            // instantiate AVI writer, use WMV3 codec
            VideoStream.Stop();
        }
        private void btnVideoAktarim_Click(object sender, EventArgs e)
        {
            //videoaktarim = true;
            Process.Start(@"C:\Program Files (x86)\WinSCP\WinSCP.exe");
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            objExcel.Visible = true;
        }


        private void GPSBUL_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(lat, longt);//lat longt
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 10;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(connection==true)
            {
                if (KomutTxt.Text != "")
                    message = KomutTxt.Text;//"From client"; //Set message variable to input
                else
                    message = "Mesaj yok";
                clientSocket.Send(Encoding.UTF8.GetBytes(message));
            }
            else
            {
                MessageBox.Show("Bağlantı yok!");
            }
        }
        private void GPSpossitive_Click(object sender, EventArgs e)
        {
            if (zoom < 100)
                zoom += 1;
            gMapControl1.MinZoom = zoom;
            gMapControl1.Zoom = zoom;
        }

        private void GPSnegatif_Click(object sender, EventArgs e)
        {
            if (zoom > 0)
                zoom -= 1;
            gMapControl1.MinZoom = zoom;
            gMapControl1.Zoom = zoom;
        }

        private void lbltakimno_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(connection==true)
                {
                    try
                    {
                        clientSocket.Send(Encoding.UTF8.GetBytes("43+j&f+3V+f2eCR=E%4h"));
                    }
                    catch { }
                    thr.Abort();
                    timer1.Stop();
                    clientSocket.Close();
                }
                for (int s = 0; s < dataGridView1.Columns.Count; s++)
                {
                    Microsoft.Office.Interop.Excel.Range myrange = (Microsoft.Office.Interop.Excel.Range)objSheet.Cells[1, s + 1];
                    myrange.Value2 = dataGridView1.Columns[s].HeaderText;
                }
                BtnBasla.Enabled = true;
                btnDurdur.Enabled = false;
                txtBaglanti.Text = "Bağlantı Kesildi";
                connection = false;       
            }
            catch(Exception hata1)
            {
                MessageBox.Show("Hata:" + hata1);
            }
        }
        private void job1()
        {
            while(true)
            {
                try
                {
                    byte[] buffer = new byte[250];
                    clientSocket.Receive(buffer);
                    message2 = Encoding.UTF8.GetString(buffer);
                    try
                    {
                        string[] paket = message2.Split('*');
                        gyro_x = Convert.ToInt32(paket[0]);
                        gyro_y = Convert.ToInt32(paket[1]);
                        gyro_z = Convert.ToInt32(paket[2]);
                        glControl1.Invalidate();

                    }
                    catch
                    {
                        txtBaglanti.Text = "Bağlantı kesildi";
                    }
                    Thread.Sleep(400);//100
                }
                catch
                {
                    timer1.Stop();
                    clientSocket.Close();
                    MessageBox.Show("Connection not installised");
                    thr.Abort();
                }
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(DateTime.Now.AddSeconds(-30).ToOADate(), DateTime.Now.ToOADate());
            chart1.ChartAreas[1].AxisX.ScaleView.Zoom(DateTime.Now.AddSeconds(-30).ToOADate(), DateTime.Now.ToOADate());
            chart1.ChartAreas[2].AxisX.ScaleView.Zoom(DateTime.Now.AddSeconds(-30).ToOADate(), DateTime.Now.ToOADate());
            chart1.ChartAreas[3].AxisX.ScaleView.Zoom(DateTime.Now.AddSeconds(-30).ToOADate(), DateTime.Now.ToOADate());
            chart1.ChartAreas[4].AxisX.ScaleView.Zoom(DateTime.Now.AddSeconds(-30).ToOADate(), DateTime.Now.ToOADate());
            int saat = DateTime.Now.Hour;
            int dk = DateTime.Now.Minute;
            int sn = DateTime.Now.Second;
            int ms = DateTime.Now.Millisecond;
            string b_saat = saat.ToString() + ':' + dk.ToString() + ':' + sn.ToString() + ':' + ms.ToString();
            tarih_lbl.Text = DateTime.Today.Date.ToString("dd:MM:yyyy");
            saat_lbl.Text = DateTime.Now.ToString("HH:mm:ss");
            try
            {
                if (sayac >= 7)
                    sayac = 0;
                this.chart1.Series[0].Points.AddXY(DateTime.Now, veri3[sayac]);
                this.chart1.Series[1].Points.AddXY(DateTime.Now, veri4[sayac]);
                this.chart1.Series[2].Points.AddXY(DateTime.Now, veri5[sayac]);
                this.chart1.Series[3].Points.AddXY(DateTime.Now, veri6[sayac]);
                this.chart1.Series[4].Points.AddXY(DateTime.Now, veri7[sayac]);

                satir = dataGridView1.Rows.Add();
                dataGridView1.Rows[satir].Cells[0].Value = takimNo;
                dataGridView1.Rows[satir].Cells[1].Value = satir;
                dataGridView1.Rows[satir].Cells[2].Value = DateTime.Today.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "," + DateTime.Now.Hour + "/" + DateTime.Now.Minute + "/" + DateTime.Now.Second;
                dataGridView1.Rows[satir].Cells[3].Value = veri3[sayac] + "Pa";
                dataGridView1.Rows[satir].Cells[4].Value = veri4[sayac] + "m";
                dataGridView1.Rows[satir].Cells[5].Value = veri5[sayac] + "m/s";
                dataGridView1.Rows[satir].Cells[6].Value = veri6[sayac] + Convert.ToChar(0x2103).ToString();
                dataGridView1.Rows[satir].Cells[7].Value = veri7[sayac] + "V";
                dataGridView1.Rows[satir].Cells[8].Value = veri8[sayac] + "N";
                dataGridView1.Rows[satir].Cells[9].Value = veri9[sayac] + "S";
                dataGridView1.Rows[satir].Cells[10].Value = veri10[sayac] + "m";
                dataGridView1.Rows[satir].Cells[10].Value = veri10[sayac] + "m";
                dataGridView1.Rows[satir].Cells[11].Value = "Beklemede";
                dataGridView1.Rows[satir].Cells[12].Value = "" + gyro_x + Convert.ToChar(0x00B0).ToString();
                dataGridView1.Rows[satir].Cells[13].Value = "" + gyro_y + Convert.ToChar(0x00B0).ToString();
                dataGridView1.Rows[satir].Cells[14].Value = "" + gyro_z + Convert.ToChar(0x00B0).ToString();
                dataGridView1.Rows[satir].Cells[15].Value = 1;
                if (Btnvideobasla.Enabled == false)
                    dataGridView1.Rows[satir].Cells[16].Value = "Evet";
                else
                    dataGridView1.Rows[satir].Cells[16].Value = "Hayır";
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Index;
                for (int s = 0; s < dataGridView1.Columns.Count; s++)
                {
                    Microsoft.Office.Interop.Excel.Range myrange = (Microsoft.Office.Interop.Excel.Range)objSheet.Cells[satir + 2, s + 1];
                    myrange.Value2 = dataGridView1.Rows[satir].Cells[s].Value;
                }

                sayac++;
                chartcount++;
            }
            catch
            {
            }
            //float y= float.Parse(paket[0]);
            //string Sicaklik = y.ToString()+ Convert.ToChar(0x2103).ToString();//Unicode ifadeyi karaktere dönüştürmek. DERECE İÇİN
            //this.chart1.Series[3].Points.AddXY((minm + maksm) / 2, paket[0]);         
                                  
        }
    }
}
