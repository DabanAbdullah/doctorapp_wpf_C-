using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;
using Telerik;
using Telerik.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for testresults.xaml
    /// </summary>
    public partial class testresults : UserControl
    {
        public DispatcherTimer dtClockTime = new DispatcherTimer();
        ObservableCollection<tresulttt> resultlist = new ObservableCollection<tresulttt>();
        public testresults()
        {
            InitializeComponent();
            dtClockTime.Interval = new TimeSpan(0, 0, 0); //in Hour, Minutes, Second.
            dtClockTime.Tick += dtClockTime_Tick;

           
        }

        private void dtClockTime_Tick(object sender, EventArgs e)
        {
         
            list1.ItemsSource = resultlist;

            getresult();
            dtClockTime.Stop();
        }

        SqlConnection con = db.getcon();
        DataTable dt = new DataTable();
        public void getresult()
        {
            try
            {
              
                resultlist.Clear();
                SqlDataAdapter da = new SqlDataAdapter("select * from [result] where ptid=" + db.ptid + "", con);
                dt.Clear();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    resultlist.Add(new tresulttt() { rid = dr["rid"].ToString(), ptid = dr["ptid"].ToString(), pic = (byte[])dr["result"] });
                }
              
            }
            catch(Exception ex) {// MessageBox.Show("select * from [result] where ptid=" + db.ptid + "");
            }
        }


        byte[] _selectedPhoto = null;
        IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".jpg");
        string rid = "";
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = (Image)sender;
            var counter = (tresulttt)button.DataContext;

            _selectedPhoto = counter.pic;
            rid = counter.rid;
            MemoryStream data = new MemoryStream(_selectedPhoto);
            IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".jpg");

            RadBitmap Image2 = formatProvider.Import(data);
            this.ImageEditorUI.Image = Image2;



        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm("دڵنیایت؟", this.confirm);
        }

        private void confirm(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                ExportImageInEditor();
               
            }

        }

        SqlParameter[] p;
        private void ExportImageInEditor()
        {
            try
            {
              


                Stream stream = File.Open("a.jpg", FileMode.OpenOrCreate);


                formatProvider.Export(this.ImageEditorUI.ImageEditor.Image, stream);

                stream.Close();
                _selectedPhoto = File.ReadAllBytes("a.jpg");


                p = new SqlParameter[3];
               
                p[0] = new SqlParameter("@2", rid);
                p[1] = new SqlParameter("@1", _selectedPhoto);
                p[2] = new SqlParameter("@3", db.uid);
                db.docmd("update result set result=@1,uid=@3 where rid=@2", p);
                getresult();

                stream.Close();
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm("دڵنیایت؟", this.confirm2);
        }

        private void confirm2(object sender, WindowClosedEventArgs e)
        { var result = e.DialogResult;
            if (result == true)
            {
                p = new SqlParameter[1];

                p[0] = new SqlParameter("@2", rid);


                db.docmd("delete from result where rid=@2", p);
                getresult();

                _selectedPhoto = null;
                rid = "";
                this.ImageEditorUI.Image = null;
            }

        }
    }
}
