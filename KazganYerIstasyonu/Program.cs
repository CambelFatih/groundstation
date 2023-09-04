using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace KazganYerIstasyonu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new form1());
            try
            {
                if (!File.Exists(@"C:\Users\Fatih Furkan Çambel\Desktop\A_Telemetri.xlsx"))
                {
                    form1.objbook.SaveAs(@"C:\Users\Fatih Furkan Çambel\Desktop\A_Telemetri.xlsx");
                }
                else
                {
                    File.Delete(@"C:\Users\Fatih Furkan Çambel\Desktop\A_Telemetri.xlsx");
                    form1.objbook.SaveAs(@"C:\Users\Fatih Furkan Çambel\Desktop\A_Telemetri.xlsx");
                }
                form1.objbook.Close();
                form1.VideoStream.Stop();
                form1.FileWriter.Close();
            }
            catch
            {

            }
        }
    }
}

