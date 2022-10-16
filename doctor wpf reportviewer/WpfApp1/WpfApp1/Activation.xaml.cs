using QLicense;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Activation.xaml
    /// </summary>
    public partial class Activation : Window
    {
        public string AppName { get; set; }

        public byte[] CertificatePublicKeyData { private get; set; }

        public bool ShowMessageAfterValidation { get; set; }

        public Type LicenseObjectType { get; set; }

        public string LicenseBASE64String
        {
            get
            {
                return key.Text.Trim();
            }
        }






        public Activation()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {


            if (ValidateLicense())
            {
                //If license if valid, save the license string into a local file
                File.WriteAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "license.lic"), LicenseBASE64String);

                MessageBox.Show("License accepted, the application will be close. Please restart it later", string.Empty);

                this.Close();
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            //licActCtrl.AppName = "DemoWinFormApp";
            //licActCtrl.LicenseObjectType = typeof(DemoLicense.MyLicense);
            //licActCtrl.CertificatePublicKeyData = this.CertificatePublicKeyData;
            ////Display the device unique ID
            //licActCtrl.ShowUID();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //licActCtrl.AppName = "DemoWinFormApp";
            //licActCtrl.LicenseObjectType = typeof(DemoLicense.MyLicense);
            //licActCtrl.CertificatePublicKeyData = this.CertificatePublicKeyData;
            ////Display the device unique ID
            //licActCtrl.ShowUID();

            LicenseObjectType = typeof(DemoLicense.MyLicense);

            id.Text = LicenseHandler.GenerateUID("Dashboard1");

        }





        public bool ValidateLicense()
        {

            if (string.IsNullOrWhiteSpace(key.Text))
            {
                MessageBox.Show("Please input license");
                return false;
            }

            //Check the activation string
            LicenseStatus _licStatus = LicenseStatus.UNDEFINED;
            string _msg = string.Empty;
            X509Certificate2 cert = new X509Certificate2(this.CertificatePublicKeyData);
            RSACryptoServiceProvider rsaKey = (RSACryptoServiceProvider)cert.PublicKey.Key;

            try
            {

                LicenseEntity _lic = LicenseHandler.ParseLicenseFromBASE64String(LicenseObjectType, key.Text.Trim(), this.CertificatePublicKeyData, out _licStatus, out _msg);

            }
            catch (Exception ex) { MessageBox.Show("Invalid"); }

            switch (_licStatus)
            {
                case LicenseStatus.VALID:
                    if (ShowMessageAfterValidation)
                    {
                        MessageBox.Show(_msg, "License is valid");
                    }

                    return true;

                case LicenseStatus.CRACKED:
                case LicenseStatus.INVALID:
                case LicenseStatus.UNDEFINED:
                    if (ShowMessageAfterValidation)
                    {
                        MessageBox.Show(_msg, "License is INVALID");
                    }

                    return false;

                default:
                    return false;
            }
        }




    }
}
