using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for editgallery.xaml
    /// </summary>
    public partial class editgallery : Window
    {
        IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".jpg");

        public editgallery()
        {
            InitializeComponent();
      
            MemoryStream data = new MemoryStream(db.gal.gimg);
            IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".jpg");

            RadBitmap Image2 = formatProvider.Import(data);
            this.ImageEditorUI.Image = Image2;
            data.Close();

          

            ContextMenu m = new ContextMenu();


            MenuItem ff = new MenuItem();
            ff.Header = "گۆڕانکاری";
            ff.Click += meniitemclick;
            m.Items.Add(ff);
            this.ImageEditorUI.ContextMenu = m;

        }

        SqlParameter[] p;
        SqlConnection con = db.getcon();
      

        private void meniitemclick(object sender, RoutedEventArgs e)
        {
            ExportImageInEditor();
           // MessageBox.Show("update");

            p = new SqlParameter[3];
            p[0] = new SqlParameter("@1", db.gal.gimg);
            p[1] = new SqlParameter("@2", db.uid);
            p[2] = new SqlParameter("@3", db.gal.gid);
          
           db.docmd("update gallery set gimg=@1,uid=@2 where gid=@3", p);


        }



        private void ExportImageInEditor()
        {
            try
            {



                Stream stream = File.Open("a.jpg", FileMode.OpenOrCreate);


                formatProvider.Export(this.ImageEditorUI.ImageEditor.Image, stream);

                stream.Close();
                db.gal.gimg = File.ReadAllBytes("a.jpg");


               

                stream.Close();
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }
        }

        
    }
}
