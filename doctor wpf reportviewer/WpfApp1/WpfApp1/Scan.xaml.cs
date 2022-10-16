using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;
using Telerik.Windows.Media.Imaging.Tools;


using System;
using System.Collections.Generic;
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
using Telerik.Windows.Controls;
using WIA;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Scan.xaml
    /// </summary>
    public partial class Scan : UserControl
    {

        public SqlConnection con = db.getcon();
     
        public Scan()
        {
            InitializeComponent();
          
          //  ImageExampleHelper.LoadSampleImage(this.ImageEditorUI, "logo.jpg");
          
        }


       

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          
          
            // this.ImageEditorUI.ImageEditor.ExecuteTool(new ResizeTool());
        }
        byte[] _selectedPhoto = null;
        IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".jpg");
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ImageEditorUI.ImageEditor.CommitTool();

                ExportImageInEditor();

                _selectedPhoto = null;

                this.ImageEditorUI.Image = null;
            }catch(Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }

        }


        SqlParameter[] p;
        private void ExportImageInEditor()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Images (.png)|.png|BMP Images (.bmp)|.bmp;|All images|.*";
                sfd.FilterIndex = 3;


                Stream stream = File.Open("a.jpg", FileMode.OpenOrCreate);


                formatProvider.Export(this.ImageEditorUI.ImageEditor.Image, stream);

                stream.Close();
                _selectedPhoto = File.ReadAllBytes("a.jpg");


                p = new SqlParameter[4];
                string rid = db.getmax("result", "rid");
                p[0] = new SqlParameter("@1", rid);
                p[1] = new SqlParameter("@2", db.ptid);
                p[2] = new SqlParameter("@3", _selectedPhoto);
                p[3] = new SqlParameter("@4", db.uid);
                db.docmd("insert into result values(@1,@2,@3,@4)", p);
                stream.Close();
                p = null;
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }
        }


            //save to file

            // BitmapImage bmm = db.LoadImage(_selectedPhoto);
            // SaveFileDialog f = new SaveFileDialog();
            //f.ShowDialog();
            // db.SaveClipboardImageToFile(f.FileName, bmm);
            // }
        


        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            try { 
            OpenFileDialog ofd = new OpenFileDialog();
          //  ofd.Filter = "PNG Images (.png)|.png|JPEG Images (.jpg,.jpeg)|.jpg;.jpeg|All images|.";
            ofd.FilterIndex = 3;
            ofd.ShowDialog();
           
                string extension = System.IO.Path.GetExtension(ofd.SafeFileName).ToLower();
                Stream stream = ofd.OpenFile();
                IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(extension);
                if (formatProvider == null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Unable to find format provider for extension: ")
                      .Append(extension).Append(" .");
                    return;
                }
                else
                {
                    this.ImageEditorUI.Image = formatProvider.Import(stream);
                }
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }
        }

        private void Save_Click_1(object sender, RoutedEventArgs e)
        {
            try { 
            
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

                       
                            MemoryStream data = new MemoryStream(imageBytes);
                            IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".jpg");

                        RadBitmap    Image2 = formatProvider.Import(data);
                            this.ImageEditorUI.Image = Image2;

                      
                    }
                }
            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
            }

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            _selectedPhoto = null;
            this.ImageEditorUI.Image = null;
        }
    }
    }

