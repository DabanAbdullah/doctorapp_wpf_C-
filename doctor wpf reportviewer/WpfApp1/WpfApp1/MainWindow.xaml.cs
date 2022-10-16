using backup;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using updater2;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static void test()
        {
            try
            {
                if (check.getdoc(Properties.Settings.Default.name, db.Decrypt(Properties.Resources.conn)) == "0")
                {
                    Properties.Settings.Default.enabled = "0";

                }
                else
                {
                    Properties.Settings.Default.enabled = "1";

                }
                Properties.Settings.Default.Save();
            }
            catch { }

            if (Properties.Settings.Default.enabled == "0")
                Environment.Exit(0);
        }

        public MainWindow()
        {
            //  StyleManager.ApplicationTheme = new Office2016Theme();
            // StyleManager.ApplicationTheme = new MaterialTheme();
            //  StyleManager.ApplicationTheme = new FluentTheme();
            //  StyleManager.ApplicationTheme = new CrystalTheme();
            //    StyleManager.ApplicationTheme = new GreenTheme();
            //     StyleManager.ApplicationTheme = new Office2016TouchTheme();

            //   StyleManager.ApplicationTheme = new Expression_DarkTheme();

            //  StyleManager.ApplicationTheme = new Office2013Theme();

            //    StyleManager.ApplicationTheme = new SummerTheme();

            //  StyleManager.ApplicationTheme = new Windows8TouchTheme();



            settheme();
            InitializeComponent();




            foreach (RadNavigationViewItem child in this.navigationView.Items)
            {


                if (db.role.Contains(child.Name) || child.Name=="aboutm" || child.Name=="exitm" || db.role.Contains("admin"))
                {
                    child.IsEnabled = true;
                }
                else child.IsEnabled = false;
            }


          

        }

        private void SettingChanging(object sender, CancelEventArgs e)
        {
           // System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            //Application.Current.Shutdown();
            //  RadWindow.Confirm("Apply theme Now Restart?", this.confirm);
        }

        private void confirm(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }

        }

        public void settheme()
        {
            if (Properties.Settings.Default.theme == "Windows8TouchTheme")
            {
                StyleManager.ApplicationTheme = new Windows8TouchTheme();
            }
            else if (Properties.Settings.Default.theme == "Expression_DarkTheme")
            {
                StyleManager.ApplicationTheme = new Expression_DarkTheme();
            }
            else if (Properties.Settings.Default.theme == "Office2016") { StyleManager.ApplicationTheme = new Office2016Theme(); }
            else if (Properties.Settings.Default.theme == "Office2016Touch")
            {
                StyleManager.ApplicationTheme = new Office2016TouchTheme();
            }
            else if (Properties.Settings.Default.theme == "Office2013") { }
            else if (Properties.Settings.Default.theme == "Material") { StyleManager.ApplicationTheme = new MaterialTheme(); }
            else if (Properties.Settings.Default.theme == "Fluent") { StyleManager.ApplicationTheme = new FluentTheme(); }
            else if (Properties.Settings.Default.theme == "Crystal") { StyleManager.ApplicationTheme = new CrystalTheme(); }
            else if (Properties.Settings.Default.theme == "Green") { }
        }
        UserControl1 t = new UserControl1();
        private void RadNavigationViewItem_Click(object sender, RoutedEventArgs e)
        {
            t.clear();
            t.names.Text = "";
            frame1.Content = t.Content;
            db.pid = "";

        }

        private void Window_Initialized(object sender, EventArgs e)
        {

           
          //  Appfor.Text = Properties.Settings.Default.dr;

        }

        private void Tablecombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

     

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RadNavigationViewItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {


                RadWindow.Prompt("لینکی نوێکردنەوە", this.update, "");

            }
            catch { }
        }

        private void update(object sender, WindowClosedEventArgs e)
        {
            var result = e.PromptResult;


            if (!string.IsNullOrEmpty(result))
            {
                string input = result;


                Updater.url = input;
                string a = updater2.Updater.checkforupdate();


                if (a != "you already have the latest version")
                {


                    updater2.Updater.UpdateApp();


                }
                else if (a == "you already have the latest version")
                {

                    MessageBox.Show("تازەترین ڤێرژنت هەیە");
                }
            }
        }
        About tt = new About();
        private void RadNavigationViewItem_Click_2(object sender, RoutedEventArgs e)
        {
           
            frame1.Content = tt.Content;
        }


        visit v = new visit();
        private void RadNavigationViewItem_Click_3(object sender, RoutedEventArgs e)
        {


            if (db.pid == "")
            {
                RadWindow.Alert("نەخۆش هەڵبژێرە");
            }
            else
            {

                v.dtClockTime.Start();
                frame1.Content = v.Content;
                Task.Factory.StartNew(() => db.checkk());
            }
        }
        RadWindow w = new RadWindow();
        
        private void RadNavigationViewItem_Click_4(object sender, RoutedEventArgs e)
        {


            setting s = new setting();

            w.Header = "ڕێکخستن";
            w.FlowDirection = FlowDirection.RightToLeft;
            w.Width = 700;
            w.Height = 500;
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Content = s.Content;
            w.ShowDialog();
        }

        private void RadNavigationViewItem_Click_5(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RadNavigationViewItem_Click_6(object sender, RoutedEventArgs e)
        {
            backupdb();
        }

       

        public void restore()
        {
            RadOpenFileDialog saveFileDialog = new RadOpenFileDialog();

            saveFileDialog.ShowDialog();
            if (saveFileDialog.DialogResult == true)
            {
                string folderName = saveFileDialog.FileName;


                try
                {



                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string str = "USE master;";
                    string str1 = "ALTER DATABASE doctors SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                    string str3 = "RESTORE DATABASE doctors FROM DISK = '" + folderName + "' WITH REPLACE,   NOUNLOAD,  STATS = 5 ";


                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    SqlCommand cmd3 = new SqlCommand(str3, con);
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    RadWindow.Alert("RECOVERED");
                    if (con.State == ConnectionState.Open) con.Close();

                }
                catch (Exception ex) { RadWindow.Alert(ex.Message); }

            }
        }
        System.Data.SqlClient.SqlConnection con = db.getcon();


        public void backupdb()
        {
            RadSaveFileDialog saveFileDialog = new RadSaveFileDialog();

            saveFileDialog.ShowDialog();
            if (saveFileDialog.DialogResult == true)
            {
                string selectedFileName = saveFileDialog.FileName;
                var connectionString = con.ConnectionString;

                // read backup folder from config file ("C:/temp/")
                var backupFolder = selectedFileName + ".bak";

                var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

                // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
                var backupFileName = String.Format("{0}{1}-{2}.bak",
                    backupFolder, sqlConStrBuilder.InitialCatalog,
                    DateTime.Now.ToString("yyyy-MM-dd"));


                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    command.ExecuteNonQuery();


                    if (con.State == ConnectionState.Open) con.Close();
                }
                RadWindow.Alert("Backup was successfull");
            }
        }


       
        private void RadNavigationViewItem_Click_7(object sender, RoutedEventArgs e)
        {

            drugandtestwindow abc = new drugandtestwindow();
            frame1.Content = abc.Content;
        }
        userwindow abcc = new userwindow();
        private void RadNavigationViewItem_Click_8(object sender, RoutedEventArgs e)
        {

            
            frame1.Content = abcc.Content;
        }
        gallery l = new gallery();
        private void RadNavigationViewItem_Click_9(object sender, RoutedEventArgs e)
        {
            if (db.pid == "")
            {
                RadWindow.Alert("نەخۆش هەڵبژێرە");
            }
            else
            {
                l.dtClockTime.Start();
                frame1.Content = l.Content;
            }
        }

        private void Reportm_Click(object sender, RoutedEventArgs e)
        {
          
            //frame1.Content = l.Content;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show("dsds");
            try
            {
                Task.Factory.StartNew(() => test());
            }
            catch
            {

            }

           

        }

        private void exitm2_Click(object sender, RoutedEventArgs e)
        {
           
          
            SqlConnection cn = new SqlConnection(db.getcon().ConnectionString);
            SqlDataAdapter da;
         

            da = new SqlDataAdapter("select * from VDrug where pid=1", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ReportDataSource reportDataSource = new ReportDataSource();
            // Must match the DataSource in the RDLC
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = dt;

            LocalReport rep = new LocalReport();
            rep.ReportPath = System.IO.Directory.GetCurrentDirectory() + @"\Report1.rdlc";
            rep.DataSources.Add(reportDataSource);
            rep.Refresh();
         rep.PrintToPrinter();

           // testreport pp = new WpfApp1.testreport();
           // da.Fill(pp.DataSet1.VDrug );
           // pp.reportViewer1.RefreshReport();
          


        }
    }
}
