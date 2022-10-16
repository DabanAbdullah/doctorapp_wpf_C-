using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.Shared;
using System.Windows.Threading;
using Microsoft.Reporting.WebForms;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for visit.xaml
    /// </summary>
    /// 

    public partial class visit : Window
    {
       public DispatcherTimer dtClockTime = new DispatcherTimer();
        DataTable dt = new DataTable();
        
        ObservableCollection<visitt> visitlist = new ObservableCollection<visitt>();
        ObservableCollection<Test> testlist = new ObservableCollection<Test>();
        ObservableCollection<drug> druglist = new ObservableCollection<drug>();


        ObservableCollection<Test> testlist2 = new ObservableCollection<Test>();
        ObservableCollection<drug> druglist2 = new ObservableCollection<drug>();


        public visit()
        {
            InitializeComponent();


            list1.ItemsSource = druglist;
            list2.ItemsSource = testlist;
            list3.ItemsSource = visitlist;

            dtClockTime.Interval = new TimeSpan(0, 0, 0); //in Hour, Minutes, Second.
            dtClockTime.Tick += dtClockTime_Tick;
            dateet.Text = DateTime.Now.ToShortDateString();
            datee = DateTime.Now;

            ContextMenu m = new ContextMenu();
            MenuItem f = new MenuItem();
            f.Header = "Refresh Information";
            m.Items.Add(f);
            drugtext.ContextMenu = m;
            testtext.ContextMenu = m;
            f.Click += RefreshDrug;


            getdata();
            drugtext.ItemsSource = druglist2;
            testtext.ItemsSource = testlist2;
        }


        SqlDataAdapter da1;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public void getdata()
        {
            testlist2.Clear();
            druglist2.Clear();
            da1 = new SqlDataAdapter("select * from test ", db.getcon());
            dt1.Clear();
            da1.Fill(dt1);
          
            foreach (DataRow dr in dt1.Rows)
            {
                testlist2.Add(new Test()
                {
                    name = dr["name"].ToString(),
                    code = dr["code"].ToString(),


                });
            }



            da1 = new SqlDataAdapter("select * from drugs ", db.getcon());
            dt2.Clear();
            da1.Fill(dt2);

            foreach (DataRow dr in dt2.Rows)
            {
                druglist2.Add(new drug()
                {
                    drugname = dr["drug"].ToString(),

                });
            }

        }


        private void RefreshDrug(object sender, RoutedEventArgs e)
        {
            getdata();
         
            //

        }

        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            getvist();
            getrecord(datee.ToShortDateString());
           
            dtClockTime.Stop();
        }

        public void getvist()
        {
            visitlist.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select *  from visit1 where pid=" + db.pid + "", db.getcon());
            dt.Clear();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                visitlist.Add(new visitt() { vdate = dr["pdate"].ToString() });
            }
            da.Dispose();
        }
        private void Drugtext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {  
                druglist.Clear();

                foreach (drug s in this.drugtext.SelectedItems)
                {
                    druglist.Add(s);
                    //MessageBox.Show(s.drugname.ToString());
                }

                

                }
            catch { }


        }

        

        private void Tebene_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                
                var button = (TextBox)sender;
                var counter = (drug)button.DataContext;

                if (counter.pdrug == null)
                {
                  
                    var item = druglist.FirstOrDefault(i => i.drugname==counter.drugname);
                    item.note = button.Text;
                    list1.ItemsSource = null;
                    list1.ItemsSource = druglist;
                }

                else
                {
                    
                    //update database

                   
                }
                list1.ItemsSource = null;
                list1.ItemsSource = druglist;
            }

        }

        string pdrug = "";
        private void Delitem_Click_1(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var counter = (drug)button.DataContext;
            if (counter.pdrug == null)
            {
               
            }
            else
            {
                var item = druglist.FirstOrDefault(i => i.pdrug == counter.pdrug );
                pdrug = item.pdrug;
                RadWindow.Confirm("دڵنیایت لە سڕینەوە؟", this.ondelete);

            }

         
           
           
        }

        private void ondelete(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                deletedrug(pdrug);
            }
        }

        public void deletedrug(string pdrug)
        {

            p = new SqlParameter[0];

           db.docmd("delete from pdrug where pdrug=" + pdrug + "", p);
            drug dr = druglist.FirstOrDefault(i => i.pdrug == pdrug);
            druglist.Remove(dr);
            pdrug = "";
        }



        SqlParameter[] p;
        DataTable drugprint = new DataTable();
        drugrep cr;
        private void Save_Click(object sender, RoutedEventArgs e)
        {

            p = new SqlParameter[5];


            foreach (drug s in druglist)
            {
                if (s.pdrug == null)
                {
                    string pdrug = db.getmax("pdrug", "pdrug");
                    string sql = "insert into pdrug(pdrug,drug,pid,pdate,uid) values(@1,@2,@3,@4,@5)";
                    p[0] = new SqlParameter("@1", pdrug);
                    p[1] = new SqlParameter("@2", s.drugname+" - "+s.note);
                    p[2] = new SqlParameter("@3", db.pid);
                    p[3] = new SqlParameter("@4", datee);
                    p[4] = new SqlParameter("@5", db.uid);


                    db.docmd(sql, p);
                  
                }

            }


            RadWindow.Confirm("do you want to print?", printhis);

         

            p = null;
            druglist.Clear();
            getvist();
            this.drugtext.SelectedItems = null;
        }

        private void printhis(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                try
                {


                    SqlDataAdapter da = new SqlDataAdapter("select * from VDrug where pid=@1 and pdate=@2", con);
                    drugprint.Clear();
                    da.SelectCommand.Parameters.AddWithValue("@1", db.pid);
                    da.SelectCommand.Parameters.AddWithValue("@2", datee.ToShortDateString());
                    da.Fill(drugprint);
                    // cr = new drugrep();
                    //cr.SetDataSource(drugprint);
                    // cr.Refresh();
                    // PageMargins margins;
                    // // Get the PageMargins structure and set the 
                    // // margins for the report.
                    // margins = cr.PrintOptions.PageMargins;
                    // margins.bottomMargin = 0;
                    // margins.leftMargin = 0;
                    // margins.rightMargin = 0;
                    // margins.topMargin = 0;
                    // cr.PrintOptions.ApplyPageMargins(margins);
                    // cr.PrintToPrinter(1, true, 0, 0);



                    ReportDataSource reportDataSource = new ReportDataSource();
                    // Must match the DataSource in the RDLC
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = drugprint;

                    LocalReport rep = new LocalReport();
                    rep.ReportPath = System.IO.Directory.GetCurrentDirectory() + @"\" + Properties.Settings.Default.Drugrep;
                    rep.DataSources.Add(reportDataSource);
                    rep.Refresh();
                    rep.PrintToPrinter();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Tebene_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = (TextBox)sender;
                var counter = (drug)button.DataContext;

                if (counter.pdrug == null)
                {

                    var item = druglist.FirstOrDefault(i => i.drugname == counter.drugname);
                    item.note = button.Text;
                    list1.ItemsSource = null;
                    list1.ItemsSource = druglist;
                }

                else
                {

                    //update database

                  
                }
                list1.ItemsSource = null;
                list1.ItemsSource = druglist;
            }
            catch { }
        }

        private void Testtext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                testlist.Clear();

                foreach (Test s in this.testtext.SelectedItems)
                {
                  
                    testlist.Add(s);
                    //MessageBox.Show(s.drugname.ToString());
                }



            }
            catch { }
        }

        private void Result_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.Key == Key.Escape)
                {

                    var button = (TextBox)sender;
                    var counter = (Test)button.DataContext;

                    if (counter.ptid != null)
                    {
                        var item = testlist.FirstOrDefault(i => i.ptid == counter.ptid);
                        item.result = button.Text;

                        p = new SqlParameter[3];
                        p[0] = new SqlParameter("@1", button.Text);
                        p[1] = new SqlParameter("@2", counter.ptid);
                    p[2] = new SqlParameter("@3",db.uid);

                    string sql = "update ptest set result=@1 ,uid=@3 where ptid=@2";
                        db.docmd(sql, p);
                        p = null;
                        list1.ItemsSource = null;
                        list1.ItemsSource = druglist;
                    }

                    else
                    {




                    }
                    list1.ItemsSource = null;
                    list1.ItemsSource = druglist;
                }
           

        }

        private void Result_LostFocus(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    var button = (TextBox)sender;
            //    var counter = (Test)button.DataContext;

            //    if (counter.ptid != null)
            //    {
            //        var item = testlist.FirstOrDefault(i => i.ptid == counter.ptid);
            //        item.result = button.Text;
            //        list2.ItemsSource = null;
            //        list2.ItemsSource = druglist;
            //        p = new SqlParameter[2];
            //        p[0] = new SqlParameter("@1", button.Text);
            //        p[1] = new SqlParameter("@2", counter.ptid);

            //        string sql = "update ptest set result=@1 where ptid=@2";
            //        db.docmd(sql, p);
            //        p = null;
            //        list1.ItemsSource = null;
            //        list1.ItemsSource = druglist;
            //    }

            //    else
            //    {

                   

                   
            //    }
            //    list1.ItemsSource = null;
            //    list1.ItemsSource = druglist;
            //}
            //catch { }
        }

        string ptid = "";
        private void Testdelete_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var counter = (Test)button.DataContext;
            if (counter.ptid == null)
            {

            }
            else
            {
                var item = testlist.FirstOrDefault(i => i.ptid == counter.ptid);
                ptid = item.ptid;
                RadWindow.Confirm("دڵنیایت لە سڕینەوە؟", this.ondelete2);

            }

        }

        private void ondelete2(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                delettest(ptid);
            }
        }

        public void delettest(string ptid)
        {
            p = new SqlParameter[1];
            string sql = "delete from ptest where ptid=@1";
            p[0] = new SqlParameter("@1", ptid);
            db.docmd(sql, p);
            var item = testlist.FirstOrDefault(i => i.ptid == ptid);
            testlist.Remove(item);
            ptid = "";
        }


        testrep cr2;
        DataTable testrep = new DataTable();
        private void Save2_Click(object sender, RoutedEventArgs e)
        {
            p = new SqlParameter[5];


            foreach (Test s in testlist)
            {
                if (s.ptid == null)
                {
                    string ptid = db.getmax("ptest", "ptid");
                    string sql = "insert into ptest(ptid,tcode,pid,tdate,uid) values(@1,@2,@3,@4,@5)";
                    p[0] = new SqlParameter("@1", ptid);
                    p[1] = new SqlParameter("@2", s.code);
                    p[2] = new SqlParameter("@3", db.pid);
                    p[3] = new SqlParameter("@4", datee);
                    p[4] = new SqlParameter("@5", db.uid);



                    db.docmd(sql, p);

                }
            }

            RadWindow.Confirm("do you want to print?", printtest);


       






            p = null;
            testlist.Clear();
            getvist();
        }

        private void printtest(object sender, WindowClosedEventArgs e)
        { var result = e.DialogResult;
            if (result == true)
            {
                //try
                //{
                  SqlDataAdapter  da = new SqlDataAdapter("select * from VTest where pid=@1 and tdate=@2", con);
                    testrep.Clear();
                    da.SelectCommand.Parameters.AddWithValue("@1", db.pid);
                    da.SelectCommand.Parameters.AddWithValue("@2", datee.ToShortDateString());
                    da.Fill(testrep);
                    //cr2 = new testrep();
                    //cr2.SetDataSource(testrep);
                    //cr2.Refresh();
                    //PageMargins margins;
                    //// Get the PageMargins structure and set the 
                    //// margins for the report.
                    //margins = cr2.PrintOptions.PageMargins;
                    //margins.bottomMargin = 0;
                    //margins.leftMargin = 0;
                    //margins.rightMargin = 0;
                    //margins.topMargin = 0;
                    //cr2.PrintOptions.ApplyPageMargins(margins);
                    //cr2.PrintToPrinter(1, true, 0, 0);


                    ReportDataSource reportDataSource = new ReportDataSource();
                    // Must match the DataSource in the RDLC
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = testrep;

                    LocalReport rep = new LocalReport();
                    rep.ReportPath = System.IO.Directory.GetCurrentDirectory() + @"\" + Properties.Settings.Default.TestRep;
                    rep.DataSources.Add(reportDataSource);
                    rep.Refresh();
                    rep.PrintToPrinter();

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }
        }

        DateTime datee;

        private void Now_Click(object sender, RoutedEventArgs e)
        {
            this.drugtext.SelectedItems = null;
            this.testtext.SelectedItems = null;
            datee = DateTime.Now;
            testlist.Clear();
            druglist.Clear();
            getrecord(datee.ToShortDateString());
            dateet.Text = datee.ToShortDateString();
        }

  

        SqlConnection con = db.getcon();
        DataTable dr1 = new DataTable();
        DataTable dr2= new DataTable();
        SqlDataAdapter da;
        public void getrecord(string datetime)
        {
           // this.drugtext.SearchText = "select * from drugsv where pdate=" + datetime + " and pid=" + db.pid + "";
            dr1.Clear(); druglist.Clear();
            da = new SqlDataAdapter("select * from drugsv where pdate='" + datetime + "' and pid="+db.pid+"",con );
            da.Fill(dr1);
            //MessageBox.Show(dr1.Rows.Count.ToString());
            foreach(DataRow dr in dr1.Rows)
            {
               
                druglist.Add(new drug() {drugname=dr["drug"].ToString(),pdrug=dr["pdrug"].ToString() });
            }

            testlist.Clear();
            dr2.Clear();
            da = new SqlDataAdapter("select * from testv where tdate='" + datetime + "' and pid=" + db.pid + "", con);
            da.Fill(dr2);
            //MessageBox.Show(dr1.Rows.Count.ToString());
            foreach (DataRow dr in dr2.Rows)
            {
              
                testlist.Add(new Test() { name = dr["name"].ToString(), ptid = dr["ptid"].ToString(),result=dr["result"].ToString() });
            }

            infor.Text = "";
            info.Clear();
            da = new SqlDataAdapter("select * from visit where vdate=@1 and pid=@2", con);
            da.SelectCommand.Parameters.AddWithValue("@1", datee.ToShortDateString());
            da.SelectCommand.Parameters.AddWithValue("@2", db.pid); da.Fill(info);
            da.Fill(info);
            if (info.Rows.Count > 0)
            {
                infor.Text = info.Rows[0]["note"].ToString();
                report.Text = info.Rows[0]["report"].ToString();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.drugtext.SelectedItems=null;
                this.testtext.SelectedItems = null;
                var button = (TextBlock)sender;
                var counter = (visitt)button.DataContext;
                testlist.Clear();
                druglist.Clear();
               
                datee = DateTime.Parse(counter.vdate);

                dateet.Text = datee.ToShortDateString();
                getrecord(datee.ToShortDateString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        DataTable info = new DataTable();
      
        private void Infor_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        Scan s = new Scan();

        RadWindow ww = new RadWindow();
        
        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            ww.Closed += closedwindow;
            var button = (Button)sender;
            var counter = (Test)button.DataContext;
            if (counter.ptid != null)
            {
                // MessageBox.Show(counter.ptid);



                db.ptid = counter.ptid;
                ww.Header = "سکان";
            ww.FlowDirection = FlowDirection.RightToLeft;
            ww.Width = 900;
            ww.Height = 800;
            ww.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ww.Content = s.Content;
            ww.ShowDialog();
            }


        }

        private void closedwindow(object sender, WindowClosedEventArgs e)
        {
            db.ptid = "";
           
        }

        private void Infor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                info.Clear();
                da = new SqlDataAdapter("select * from visit where vdate=@1 and pid=@2", con);
                da.SelectCommand.Parameters.AddWithValue("@1", datee.ToShortDateString());
                da.SelectCommand.Parameters.AddWithValue("@2", db.pid);

                da.Fill(info);
                p = new SqlParameter[4];
                p[0] = new SqlParameter("@1", datee.ToShortDateString());
                p[1] = new SqlParameter("@2", infor.Text);
                p[2] = new SqlParameter("@3", db.pid);
                p[3] = new SqlParameter("@4", db.uid);
                string sql = "";
                if (info.Rows.Count > 0)
                {
                    sql = "update visit set note=@2,uid=@4 where pid=@3 and vdate=@1";
                }
                else
                {
                    sql = "insert into visit values(@1,@2,@3,@4)";
                }
                db.docmd(sql, p);
                p = null;
            }

        }

        RadWindow fd1 = new RadWindow();
        testresults ss = new testresults();
        private void View_Click(object sender, RoutedEventArgs e)
        {
           
          
            fd1.Closed += closedwindow2;
            var button = (Button)sender;
            var counter = (Test)button.DataContext;
            if (counter.ptid != null)
            {
              


              
                db.ptid = counter.ptid;

                ss.dtClockTime.Start();
                fd1.Header = "ئەنجامی پشکنین";
                fd1.FlowDirection = FlowDirection.RightToLeft;
                fd1.Width = 900;
                fd1.Height = 800;
                fd1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                fd1.Content = ss.Content;
                fd1.ShowDialog();
            }
        }

        private void closedwindow2(object sender, WindowClosedEventArgs e)
        {
            db.ptid = "";
        }

        private void Window_Activated(object sender, EventArgs e)
        {
           

        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void Savedetail_Click(object sender, RoutedEventArgs e)
        {
           
                info.Clear();
                da = new SqlDataAdapter("select * from visit where vdate=@1 and pid=@2", con);
                da.SelectCommand.Parameters.AddWithValue("@1", datee.ToShortDateString());
                da.SelectCommand.Parameters.AddWithValue("@2", db.pid);

                da.Fill(info);
                p = new SqlParameter[4];
                p[0] = new SqlParameter("@1", datee.ToShortDateString());
                p[1] = new SqlParameter("@2", infor.Text);
                p[2] = new SqlParameter("@3", db.pid);
                p[3] = new SqlParameter("@4", db.uid);
                string sql = "";
                if (info.Rows.Count > 0)
                {
                    sql = "update visit set note=@2,uid=@4 where pid=@3 and vdate=@1";
                }
                else
                {
                    sql = "insert into visit(vdate,note,pid,uid) values(@1,@2,@3,@4)";
                }
                db.docmd(sql, p);
                p = null;
            getvist();


        }

        private void Savereport_Click(object sender, RoutedEventArgs e)
        {
            info.Clear();
            da = new SqlDataAdapter("select * from visit where vdate=@1 and pid=@2", con);
            da.SelectCommand.Parameters.AddWithValue("@1", datee.ToShortDateString());
            da.SelectCommand.Parameters.AddWithValue("@2", db.pid);

            da.Fill(info);
            p = new SqlParameter[4];
            p[0] = new SqlParameter("@1", datee.ToShortDateString());
            p[1] = new SqlParameter("@2", report.Text);
            p[2] = new SqlParameter("@3", db.pid);
            p[3] = new SqlParameter("@4", db.uid);
            string sql = "";
            if (info.Rows.Count > 0)
            {
                sql = "update visit set report=@2,uid=@4 where pid=@3 and vdate=@1";
            }
            else
            {
                sql = "insert into visit(vdate,report,pid,uid) values(@1,@2,@3,@4)";
            }
            db.docmd(sql, p);
            p = null;
            getvist();
        }
    }
}
