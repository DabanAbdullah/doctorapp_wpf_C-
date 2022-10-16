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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for cameraa.xaml
    /// </summary>
    public partial class cameraa : UserControl
    {
        public cameraa()
        {
            InitializeComponent();
        }

        private void OnDeletePhotoClick(object sender, RoutedEventArgs e)
        {
            this.imageControl.Source = new BitmapImage(new Uri("Image/profile.jpg", UriKind.RelativeOrAbsolute));
            this.deletePhotoButton.IsEnabled = false;
            db.pic = null;
        }
        private void OnWebCamSnapshotTaken(object sender, SnapshotTakenEventArgs e)
        {
            this.StopCamera();
            this.imageControl.Source = e.Snapshot;
          db.pic = db.BitmapSourceToByte(e.Snapshot);

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
        private void OnStopCameraButtonClick(object sender, RoutedEventArgs e)
        {
            this.StopCamera();
        }

        private void OnOpenTakePhotoClick(object sender, RoutedEventArgs e)
        {
            this.StartCamera();
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
    }
}
