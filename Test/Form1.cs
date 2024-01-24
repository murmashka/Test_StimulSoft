using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stimulsoft.Report;

namespace Test
{
    public partial class Form1 : Form
    {
        string FilePathJson;
        public Form1()
        {
            InitializeComponent();
        }



        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            FilePathJson = PathJSON.Text;

            if (String.IsNullOrEmpty(FilePathJson))
            {
                Form Form4 = new Form4();
                Form4.ShowDialog();
                PathJSON.Clear();
                return;
            }
            else
            { 
                if (File.Exists(FilePathJson))
                {
                    Form Form3 = new Form3();
                    Form3.ShowDialog();
                    PathJSON.Clear();
                }
                else
                {
                    Form Form2 = new Form2();
                    Form2.ShowDialog();
                    PathJSON.Clear();
                }
            }
        }






        private void ConvertButton_Click(object sender, EventArgs e)
        {
            //var rep = new StiReport();
            //rep.Design();

            if (String.IsNullOrEmpty(FilePathJson))
            {
                Form Form4 = new Form4();
                Form4.ShowDialog();
                PathJSON.Clear();
                return;
            }

            StiReport report = new StiReport();
            report.Load("Report.mrt");
            
            string JSON = File.ReadAllText(FilePathJson);
            DataTable DTfromJSON = JsonConvert.DeserializeObject<DataTable>(JSON);

            report.RegData(DTfromJSON);
            report.Render();

            string directoryName = Path.GetDirectoryName(FilePathJson);



            report.ExportDocument(StiExportFormat.Pdf, Path.Combine(directoryName,"Report.pdf"));
            MessageBox.Show("Отчет создан. Путь к отчету: " + Path.Combine(directoryName, "Report.pdf"), "Результат");

        }


    }
}
