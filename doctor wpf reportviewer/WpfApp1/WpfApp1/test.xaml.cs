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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class test : Window
    {
        private string userNameCache;
        private string emailCache;

        public test()
        {
            InitializeComponent();
            
           
        }


        private void OnExampleUnloaded(object sender, RoutedEventArgs e)
        {
            this.webCam.ShutDown();
        }

        private void OnOpenTakePhotoClick(object sender, RoutedEventArgs e)
        {
            this.StartCamera();
        }

        private void OnStopCameraButtonClick(object sender, RoutedEventArgs e)
        {
            this.StopCamera();
        }

        private void OnDeletePhotoClick(object sender, RoutedEventArgs e)
        {
            this.imageControl.Source = new BitmapImage(new Uri("/WebCam;component/FirstLook/Images/Tori.png", UriKind.RelativeOrAbsolute));
            this.deletePhotoButton.IsEnabled = false;
        }


        private void OnWebCamSnapshotTaken(object sender, SnapshotTakenEventArgs e)
        {
            this.StopCamera();
            this.imageControl.Source = e.Snapshot;
            //BitmapSource snapshot = e.Snapshot;
            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            ////encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            //encoder.QualityLevel = 100;
            //// byte[] bit = new byte[0];
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    encoder.Frames.Add(BitmapFrame.Create(snapshot));
            //    encoder.Save(stream);
            //    byte[] bit = stream.ToArray();

            //    System.Drawing.Image im = System.Drawing.Image.FromStream(stream);
            //    // im.Save(@"D:\a.jpg");
            //    // stream.Close();
            //}
            this.deletePhotoButton.IsEnabled = true;
        }

        private void StartCamera()
        {
            this.takePhotoTextBlock.Visibility = Visibility.Collapsed;
            this.imageControl.Visibility = Visibility.Collapsed;
            this.takePhotoButton.Visibility = Visibility.Collapsed;
            this.deletePhotoButton.Visibility = Visibility.Collapsed;
            this.stopCameraButton.Visibility = Visibility.Visible;
            this.webCam.Visibility = Visibility.Visible;

            this.webCam.Start();
        }

        private void StopCamera()
        {
            this.webCam.Stop();
            if (this.webCam.IsPreviewingSnapshot)
            {
                RadWebCamCommands.DiscardSnapshot.Execute(null, this.webCam);
            }

            this.webCam.Visibility = Visibility.Hidden;
            this.stopCameraButton.Visibility = Visibility.Collapsed;
            this.imageControl.Visibility = Visibility.Visible;
            this.takePhotoButton.Visibility = Visibility.Visible;
            this.deletePhotoButton.Visibility = Visibility.Visible;
            this.takePhotoTextBlock.Visibility = Visibility.Visible;
        }

        private void OnEditUserNameButtonClick(object sender, RoutedEventArgs e)
        {
            this.editUserNameButton.IsEnabled = false;
            this.userNameInput.IsReadOnly = false;
            this.cancelButton.IsEnabled = true;
            this.saveButton.IsEnabled = true;
            this.userNameCache = this.userNameInput.Text;

            if (this.emailInput.IsReadOnly)
            {
                this.emailCache = this.emailInput.Text;
            }

            this.userNameInput.Focus();
        }

        private void OnEditEmailButtonClick(object sender, RoutedEventArgs e)
        {
            this.editEmailButton.IsEnabled = false;
            this.emailInput.IsReadOnly = false;
            this.cancelButton.IsEnabled = true;
            this.saveButton.IsEnabled = true;
            this.emailCache = this.emailInput.Text;

            if (this.userNameInput.IsReadOnly)
            {
                this.userNameCache = this.userNameInput.Text;
            }

            this.emailInput.Focus();
        }

        private void OnCancelOrSaveButtonClick(object sender, RoutedEventArgs e)
        {
            this.editUserNameButton.IsEnabled = true;
            this.userNameInput.IsReadOnly = true;
            this.editEmailButton.IsEnabled = true;
            this.emailInput.IsReadOnly = true;
            this.cancelButton.IsEnabled = false;
            this.saveButton.IsEnabled = false;

            var btn = (RadButton)sender;
            if (btn.Content.Equals("Cancel"))
            {
                this.userNameInput.Text = this.userNameCache;
                this.emailInput.Text = this.emailCache;
                this.userNameCache = string.Empty;
                this.emailCache = string.Empty;
            }
        }

        private void OnExampleUserControlLoaded(object sender, RoutedEventArgs e)
        {
            var parentGrid = this.ParentOfType<Grid>();
            parentGrid.Margin = new Thickness(0, 0, 5, 0);
        }

























      

        private void RadWebCam_CameraError(object sender, RoutedEventArgs e)
        {
            var args = (CameraErrorEventArgs)e;
            if (args.Error.ErrorState == CameraErrorState.NoCamera)
            {
                args.Error.Message = "Cannot detect a camera device.";
            }
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
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

                        //  pictureBox1.Image = img;
                    }
                }
            }
            catch (Exception ex){ RadWindow.Alert(ex.Message); }

        }

        private void RadButton_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
       
      
        private void Window_Activated(object sender, EventArgs e)
        {
           

        }
    }
}
