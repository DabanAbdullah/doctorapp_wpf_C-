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
using SourceChord.Lighty;
using Telerik.Windows;
using Telerik.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;
using WIA;
using Microsoft.Win32;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for gallery.xaml
    /// </summary>
    public partial class gallery : Window
    {
        public DispatcherTimer dtClockTime = new DispatcherTimer();
        SqlConnection con = db.getcon();
        SqlParameter[] p = null;
   

        RadWindow r = new RadWindow();

        ObservableCollection<galleryy> gal = new ObservableCollection<galleryy>();

        private void New_Click(object sender, RoutedEventArgs e)
        {
            db.pic = null;
            cameraa aa = new cameraa();
            r.PreviewClosed += previewclosed;
            r.Content = aa.Content;
            r.Width = 400;
            r.Height = 500;
            r.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            r.ShowDialog();
        }

        SqlDataAdapter da;
        DataTable dt = new DataTable();
        public void getgallery()
        {
            dt.Clear();
            gal.Clear();

            da = new SqlDataAdapter("select * from gallery where pid=@1", con);
            da.SelectCommand.Parameters.AddWithValue("@1", db.pid);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    gal.Add(new galleryy() { gid = dr["gid"].ToString(),gdate=dr["gdate"].ToString(),gimg=(byte[])dr["gimg"],pid=db.pid });
                }
            }



        }


        private void previewclosed(object sender, WindowPreviewClosedEventArgs e)
        {
            if (db.pic == null)
            {
               

            }
            else
            {
                p = new SqlParameter[4];
                p[0] = new SqlParameter("@1", db.getmax("gallery", "gid"));
                p[1] = new SqlParameter("@2", db.pid);
                p[2] = new SqlParameter("@3", db.pic);
                p[3] = new SqlParameter("@4", db.uid);
                db.docmd("insert into gallery(gid,pid,gimg,uid)values(@1,@2,@3,@4)", p);
                p = null;

                getgallery();


            }
        }

        public gallery()
        {
            InitializeComponent();

            dtClockTime.Interval = new TimeSpan(0, 0, 0); //in Hour, Minutes, Second.
            dtClockTime.Tick += dtClockTime_Tick;

            
        }

        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            productlist.ItemsSource = gal;
            getgallery();
            dtClockTime.Stop();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = (Image)sender;
            var counter = (galleryy)button.DataContext;
            db.gal = counter;


            ContextMenu m = new ContextMenu();
           
           
                MenuItem ff = new MenuItem();
                ff.Header = "View";
                ff.Click += meniitemclick;
                m.Items.Add(ff);


             ff = new MenuItem();
            ff.Header = "Edit";
            ff.Click += meniitemclick2;
            m.Items.Add(ff);

            ff = new MenuItem();
            ff.Header = "Delete";
            ff.Click += delete;
            m.Items.Add(ff);

            button.ContextMenu = m;
            button.ContextMenu.IsOpen = true;
            e.Handled = true;



            // show UserControl(SampleDialog is derived from UserControl.)
            //  LightBox.Show(this, new SampleDialog());



            // show FrameworkElement.
          
            
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm("دڵنیایت لە سڕینەوە؟", this.delcon);
           
        }

        private void delcon(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                p = new SqlParameter[1];
                p[0] = new SqlParameter("@1", db.gal.gid);


                db.docmd("delete from gallery where gid=@1", p);
                getgallery();

            }
        }

        editgallery aa;
        private void meniitemclick2(object sender, RoutedEventArgs e)
        {
           aa = new editgallery();

            r.PreviewClosed += previewclosed2;
            r.Content = aa.Content;
            r.Width = 900;
            r.Height = 800;
            r.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            r.ShowDialog();
        }

        private void previewclosed2(object sender, WindowPreviewClosedEventArgs e)
        {


            aa.Close();
          

            getgallery();

        }

        private void meniitemclick(object sender, RoutedEventArgs e)
        {
            var image = new Image();
            image.Source = db.LoadImage(db.gal.gimg);

            LightBox.Show(this, image);
        }

        private void Scane_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                CommonDialogClass commonDialogClass = new CommonDialogClass();

                Device scannerDevice = commonDialogClass.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, false, false);

                if (scannerDevice != null)

                {

                    Item scannnerItem = scannerDevice.Items[1];

                    db.AdjustScannerSettings(scannnerItem, 300, 0, 400, 2080, 2200, 0, 0);

                    object scanResult = commonDialogClass.ShowTransfer(scannnerItem, WIA.FormatID.wiaFormatPNG, false);

                    if (scanResult != null)

                    {

                        ImageFile image = (ImageFile)scanResult;
                        var imageBytes = (byte[])image.FileData.get_BinaryData();
                        var ms = new MemoryStream(imageBytes);
                        var imageSource = new BitmapImage();
                        imageSource.BeginInit();
                        imageSource.StreamSource = ms;
                        imageSource.EndInit();
                        // image1.Source = imageSource;
                        //  img = db.resizeImage(img, pictureBox1.Height, pictureBox1.Width);


                       
                        p = new SqlParameter[4];
                        p[0] = new SqlParameter("@1", db.getmax("gallery", "gid"));
                        p[1] = new SqlParameter("@2", db.pid);
                        p[2] = new SqlParameter("@3", imageBytes);
                        p[3] = new SqlParameter("@4", db.uid);
                        db.docmd("insert into gallery(gid,pid,gimg,uid)values(@1,@2,@3,@4)", p);
                        p = null;

                        getgallery();
                        imageBytes=null;


                    }
                }
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                //  ofd.Filter = "PNG Images (.png)|.png|JPEG Images (.jpg,.jpeg)|.jpg;.jpeg|All images|.";
                ofd.FilterIndex = 3;
                ofd.ShowDialog();

                string extension = System.IO.Path.GetExtension(ofd.SafeFileName).ToLower();
                Stream stream = ofd.OpenFile();


                var imagedata = File.ReadAllBytes(ofd.FileName);
                p = new SqlParameter[4];
                p[0] = new SqlParameter("@1", db.getmax("gallery", "gid"));
                p[1] = new SqlParameter("@2", db.pid);
                p[2] = new SqlParameter("@3", imagedata);
                p[3] = new SqlParameter("@4", db.uid);
                db.docmd("insert into gallery(gid,pid,gimg,uid)values(@1,@2,@3,@4)", p);
                p = null;

                getgallery();
                imagedata = null;
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }
        }
    }
}
