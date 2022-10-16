using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for userwindow.xaml
    /// </summary>
    public partial class userwindow : Window
    {
        SqlConnection con = db.getcon();
    
        public ObservableCollection<usersclass> userlist = new ObservableCollection<usersclass>();
        public ObservableCollection<roless> rolelist = new ObservableCollection<roless>();
        public userwindow()
        {
            InitializeComponent();
            productlist.ItemsSource = rolelist;
            list1.ItemsSource = userlist;
        }
        DataTable dt = new DataTable();
        public void getrole()
        {
            rolelist.Clear();
            dt.Clear();

            SqlDataAdapter da = new SqlDataAdapter("select * from roles", con);
            da.Fill(dt);
            //or.Add(new order("pizza peparoni", "image/1.jpg", 2, 3000));
            //or.Add(new order("pizza peparoni", "image/1.jpg", 2, 4000));
            //list1.ItemsSource = or;


            foreach (DataRow dr in dt.Rows)
            {
                rolelist.Add(new roless() { role = dr["role"].ToString(), rname = dr["rolename"].ToString(), check = false });/**/ ;

            }
            dt.Clear();
        }

        DataTable dt2 = new DataTable();
        public void getusers()
        {
            userlist.Clear();
            dt2.Clear();

            SqlDataAdapter da = new SqlDataAdapter("select * from users", con);
            da.Fill(dt2);
            //or.Add(new order("pizza peparoni", "image/1.jpg", 2, 3000));
            //or.Add(new order("pizza peparoni", "image/1.jpg", 2, 4000));
            //list1.ItemsSource = or;


            foreach (DataRow dr in dt2.Rows)
            {
                userlist.Add(new usersclass() { uid = dr["uid"].ToString(), uname = dr["uname"].ToString(), role = dr["role"].ToString(), upass = dr["pass"].ToString() });/**/ ;

            }
            dt2.Clear();
        }

        string cuid = "";
        public void getuid()
        {
            if (db.uid == "")
            {
               cuid = "0";

            }
            else
            {
                cuid = db.uid;
            }
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            role = "";
          
            foreach (roless rr in rolelist)
            {
                if (rr.check == true)
                {
                    role = role + "," + rr.rname;
                }
            }

            //MessageBox.Show(role.TrimStart(',').TrimEnd(','));


            if (uname.Text.Length == 0 || upass.Text.Length == 0 || role.TrimStart(',').TrimEnd(',').Length == 0 || selecteduser == null)
            {
                RadWindow.Alert("تکایە زانیاریەکان پڕ بکەرەوە");
            }
            else
            {


                RadWindow.Confirm("دڵنیایت لە گۆڕانکاری؟", this.updateuser);
            }

        }

        private void updateuser(object sender, WindowClosedEventArgs e)
        {
            getuid();
            var result = e.DialogResult;
            if (result == true)
            {

                string sql = "update users set uname=@1,pass=@2,role=@3,cuid=@5 where uid=@4";




                SqlParameter[] aa = new SqlParameter[5];
                aa[0] = new SqlParameter("@1", uname.Text);
                aa[1] = new SqlParameter("@2", db.Encrypt(upass.Password));
                aa[2] = new SqlParameter("@3", role.TrimStart(',').TrimEnd(','));
                aa[3] = new SqlParameter("@4", selecteduser.uid);
                aa[4] = new SqlParameter("@4", cuid);
                RadWindow.Alert(db.docmd(sql, aa));
                uname.Text = upass.Password = "";
                getrole();
                getusers();
            }
        }

        private void Trashdel_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm("دلنیایت لە سڕینەوە؟", this.deleteuser);
        }

        private void deleteuser(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {

                string sql = "delete from users where uid=@1";




                SqlParameter[] aa = new SqlParameter[1];
                aa[0] = new SqlParameter("@1", selecteduser.uid);

                RadWindow.Alert(db.docmd(sql, aa));
                uname.Text = upass.Password = "";
                getrole();
                getusers();
            }
        }

        string role = "";
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            role = "";
            getuid();
            foreach (roless rr in rolelist)
            {
                if (rr.check == true)
                {
                    role = role + "," + rr.rname;
                }
            }

            //MessageBox.Show(role.TrimStart(',').TrimEnd(','));


            if (uname.Text.Length == 0 || upass.Text.Length == 0 || role.TrimStart(',').TrimEnd(',').Length == 0)
            {
                RadWindow.Alert("تکایە زانیاریەکان پڕ بکەرەوە");
            }
            else
            {
                string sql = "insert into users(uname,pass,role,uid,cuid)values(@1,@2,@3,@4,@5)";
                string uid = db.getmax("users", "uid");
                SqlParameter[] aa = new SqlParameter[5];
                aa[0] = new SqlParameter("@1", uname.Text);
                aa[1] = new SqlParameter("@2", db.Encrypt(upass.Password));
                aa[2] = new SqlParameter("@3", role.TrimStart(',').TrimEnd(','));
                aa[3] = new SqlParameter("@4", uid);
                aa[4] = new SqlParameter("@5", cuid);
                RadWindow.Alert(db.docmd(sql, aa));
                uname.Text = upass.Password = "";
                getrole();
                getusers();
            }

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            getrole();
            getusers();
        }



        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var button = (CheckBox)sender;
            var counter = (roless)button.DataContext;
            var item = rolelist.FirstOrDefault(i => i.rname == counter.rname);
            if (button.IsChecked == true)
            {
                item.check = true;
            }
            else
            {
                item.check = false;
            }

        }


        usersclass selecteduser;
        private void List1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selecteduser = (usersclass)list1.SelectedItem;
                uname.Text = selecteduser.uname;
                upass.Password = db.Decrypt(selecteduser.upass);


                
                }
            catch (Exception ex)
            {
               
                selecteduser = null;
            }

            //string[] arr = selecteduser.role.Split(',');

            //foreach(roless rr in rolelist)
            //{
            //   foreach(string role in arr)
            //    {

            //        if (rr.rname == role)
            //        {
            //            var item = rolelist.FirstOrDefault(i => i.rname == role);
            //            if (item != null)
            //            {
            //                MessageBox.Show(rr.rname + "" + role);
            //                rr.check = true;
            //            }


            //        }
            //    }
            //}




        }
    }
}
