using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for repwindow.xaml
    /// </summary>
    public partial class repwindow : Window
    {
        SqlConnection con = db.getcon();
        public repwindow()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        private void Report_Click(object sender, RoutedEventArgs e)
        {
           

            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select * from summary2 where  vdate between @1 and @2", con);
            da.SelectCommand.Parameters.AddWithValue("@1", start.SelectedDate.ToString());
            da.SelectCommand.Parameters.AddWithValue("@2", end.SelectedDate.ToString());
            da.Fill(dt);     // MessageBox.Show();

            ReportDataSource reportDataSource = new ReportDataSource();

            reportDataSource.Name = "DataSet1"; // Name of the DataSet we set in .rdlc

            reportDataSource.Value = dt;

          reportViewer.LocalReport.ReportPath = System.IO.Directory.GetCurrentDirectory() + @"\" + Properties.Settings.Default.summary;



            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            reportViewer.RefreshReport();
        }
    }
}
