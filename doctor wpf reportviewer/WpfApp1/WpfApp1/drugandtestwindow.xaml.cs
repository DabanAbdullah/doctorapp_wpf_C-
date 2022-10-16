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
using System.Data.SqlClient;
using System.Data;
using Telerik.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for drugandtestwindow.xaml
    /// </summary>
    public partial class drugandtestwindow : Window
    {
        SqlParameter[] p;
        SqlConnection con = db.getcon();
        ObservableCollection<Test> testlist = new ObservableCollection<Test>();
        ObservableCollection<drug> druglist = new ObservableCollection<drug>();
        //ObservableCollection<drug> druglist2 = new ObservableCollection<drug>();
        public drugandtestwindow()
        {
            InitializeComponent();

            
           list2.ItemsSource = druglist;
            this.drugtext.ItemsSource = druglist;
            list1.ItemsSource = testlist;
            this.testtext.ItemsSource = testlist;
            getdata();
        }

        SqlDataAdapter da;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        public void getdata()
        {
            testlist.Clear();
            druglist.Clear();
           da = new SqlDataAdapter("select * from test ", db.getcon());
            dt.Clear();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                testlist.Add(new Test()
                {
                    name = dr["name"].ToString(),
                    code = dr["code"].ToString(),


                });
            }



            da = new SqlDataAdapter("select * from drugs ", db.getcon());
            dt2.Clear();
            da.Fill(dt2);

            foreach (DataRow dr in dt2.Rows)
            {
                druglist.Add(new drug()
                {
                    drugname = dr["drug"].ToString(),

                });
            }

        }
       

        private void RadMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            p = new SqlParameter[1];
            p[0] = new SqlParameter("@1", this.drugtext.SearchText);
            db.docmd("insert into drugs values(@1)", p);
            p = null;
            this.drugtext.SelectedItems = null;
            this.drugtext.SearchText = "";

            getdata();
        }


        string drugname = "",code="";
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = (TextBlock)sender;
            var counter = (drug)button.DataContext;
            drugname = counter.drugname;

            this.drugtext.SearchText = drugname;

        }

        private void RadMenuItem_Click_1(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadWindow.Confirm("دڵنیایت لە گۆڕانکاری؟", this.confirm1);
        }

        private void confirm1(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                p = new SqlParameter[2];
                p[0] = new SqlParameter("@1", this.drugtext.SearchText);
                p[1] = new SqlParameter("@2", drugname);
                db.docmd("update drugs set drug=@1 where drug=@2", p);
                p = null;
                this.drugtext.SelectedItems = null;
                this.drugtext.SearchText = "";

                getdata();
            }
            drugname = "";
            this.drugtext.SearchText = "";
        }

        private void RadMenuItem_Click_2(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (drugname == "")
            {
                RadWindow.Alert("دەرمان هەڵبژێرە پێش سڕینەوە");
            }
            else
            {
                RadWindow.Confirm("دڵنیایت لە سڕینەوە؟", this.confirm2);
            }
        }

        private void confirm2(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                p = new SqlParameter[1];
                p[0] = new SqlParameter("@1", drugname);
                
                db.docmd("delete from drugs where drug=@1", p);
                p = null;
                this.drugtext.SelectedItems = null;
                this.drugtext.SearchText = "";

                getdata();
            }
            drugname = "";
            this.drugtext.SearchText = "";
        }

        private void RadMenuItem_Click_3(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            p = new SqlParameter[2];
            string code = db.getmax("test", "code");
            p[0] = new SqlParameter("@1", code);
            p[1] = new SqlParameter("@2", this.testtext.SearchText);

        db.docmd("insert into test values(@1,@2)", p);
            p = null;
            this.testtext.SelectedItems = null;
            this.testtext.SearchText = "";

            getdata();
        }

        private void RadMenuItem_Click_4(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadWindow.Confirm("دڵنیایت لە گۆڕانکاری؟", this.confirm3);
        }

        private void confirm3(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                p = new SqlParameter[2];
                p[0] = new SqlParameter("@1", this.testtext.SearchText);
                p[1] = new SqlParameter("@2", code);
                db.docmd("update test set name=@1 where code=@2", p);
                p = null;
                this.testtext.SelectedItems = null;
                this.testtext.SearchText = "";

                getdata();
            }
            code = "";
            this.testtext.SearchText = "";
        }

        private void RadMenuItem_Click_5(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (code == "")
            {
                RadWindow.Alert("پشکنین هەڵبژێرە پێش سڕینەوە");
            }
            else
            {
                RadWindow.Confirm("دڵنیایت لە سڕینەوە؟", this.confirm4);
            }

        }

        private void confirm4(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                p = new SqlParameter[1];
                p[0] = new SqlParameter("@1", code);

                db.docmd("delete from test where code=@1", p);
                p = null;
                this.testtext.SelectedItems = null;
                this.testtext.SearchText = "";

                getdata();
            }
            code = "";
            this.testtext.SearchText = "";
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var button = (TextBlock)sender;
            var counter = (Test)button.DataContext;
            code = counter.code;

            this.testtext.SearchText = counter.name;

        }
    }
}
