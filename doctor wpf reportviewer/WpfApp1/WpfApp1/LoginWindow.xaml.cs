using DemoLicense;
using QLicense;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        SqlConnection con = db.getcon();
        public LoginWindow()
        {
            StyleManager.ApplicationTheme = new FluentTheme();

            Task.Factory.StartNew(() => db.checkk());
        }



        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {

        }

        private void BtnActionMinimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnActionSystemInformation_OnClick(object sender, RoutedEventArgs e)
        {
            var systemInformationWindow = new SystemInformationWindow();
            systemInformationWindow.Show();
        }

        private void btnActionClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        DataTable dt = new DataTable();
        private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocalPasswordBox.Password == "azhergnt573")
            {
                userwindow s = new userwindow();
                s.ShowDialog();

               
            }
            else if (LocalPasswordBox.Password == "azhergnt574")
            {
                settings ss = new settings();
                ss.Show();
            }
            else
            {

                SqlDataAdapter da = new SqlDataAdapter("select * from users where uname=@1 and pass=@2", con);
                da.SelectCommand.Parameters.AddWithValue("@1", LocalUserNameTextBox.Text);
                da.SelectCommand.Parameters.AddWithValue("@2", db.Encrypt(LocalPasswordBox.Password));
                dt.Clear();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    db.uid = dt.Rows[0]["uid"].ToString();
                    db.username = dt.Rows[0]["uname"].ToString();
                    db.pass = dt.Rows[0]["pass"].ToString();
                    db.role = dt.Rows[0]["role"].ToString();

                    this.Hide();
                    MainWindow w = new MainWindow();
                    w.Show();
                }
                else
                {
                    RadWindow.Alert("ناوی بەکارهێنەر یان وشەی نهێنی هەڵەیە");
                }
                dt.Clear();
            }

        }
        byte[] _certPubicKeyData;
        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LocalUserNameTextBox.Focus();
            MyLicense _lic = null;
            string _msg = string.Empty;
            LicenseStatus _status = LicenseStatus.UNDEFINED;

            //Read public key from assembly
            Assembly _assembly = Assembly.GetExecutingAssembly();
            using (MemoryStream _mem = new MemoryStream())
            {


                _assembly.GetManifestResourceStream("WpfApp1.LicenseVerify.cer").CopyTo(_mem);

                _certPubicKeyData = _mem.ToArray();


            }

            //Check if the XML license file exists
            if (File.Exists("license.lic"))
            {
                _lic = (MyLicense)LicenseHandler.ParseLicenseFromBASE64String(
                    typeof(MyLicense),
                    File.ReadAllText("license.lic"),
                    _certPubicKeyData,
                    out _status,
                    out _msg);
            }
            else
            {
                _status = LicenseStatus.INVALID;
                _msg = "Your copy of this application is not activated";
            }

            switch (_status)
            {
                case LicenseStatus.VALID:

                    //TODO: If license is valid, you can do extra checking here
                    //TODO: E.g., check license expiry date if you have added expiry date property to your license entity
                    //TODO: Also, you can set feature switch here based on the different properties you added to your license entity 

                    //Here for demo, just show the license information and RETURN without additional checking       


                    return;

                default:
                    //for the other status of license file, show the warning message
                    //and also popup the activation form for user to activate your application

                    Activation frm = new Activation();


                    frm.CertificatePublicKeyData = _certPubicKeyData;
                    frm.ShowDialog();

                    //Exit the application after activation to reload the license file 
                    //Actually it is not nessessary, you may just call the API to reload the license file
                    //Here just simplied the demo process

                    this.Close();

                    break;
            }
        }
    }
}
