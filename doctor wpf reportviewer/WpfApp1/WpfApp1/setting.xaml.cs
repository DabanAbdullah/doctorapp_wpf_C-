using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for setting.xaml
    /// </summary>
    public partial class setting : UserControl
    {
        public List<themename> them = new List<themename>();
        public setting()
        {
            InitializeComponent();
            them.Add(new themename { theme = "Office_Black" });
            them.Add(new themename { theme = "Office_Blue" });
            them.Add(new themename { theme = "Office2016" });
            them.Add(new themename { theme = "Office2016Touch" });
            them.Add(new themename { theme = "Office2013" });
            them.Add(new themename { theme = "Material" });
            them.Add(new themename { theme = "Fluent" });
            them.Add(new themename { theme = "Green" });
            them.Add(new themename { theme = "Crystal" });
            list1.ItemsSource = them;


            Properties.Settings.Default.SettingsSaving += SettingChanging;
        }

        private void SettingChanging(object sender, CancelEventArgs e)
        {

            RadWindow.Confirm("Apply theme Now Restart?", this.confirm);
        }

        private void confirm(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }

        }

        public void settheme()
        {
            if (Properties.Settings.Default.theme == "Windows8TouchTheme")
            {
                StyleManager.ApplicationTheme = new Windows8TouchTheme();
            }
            else if (Properties.Settings.Default.theme == "Expression_DarkTheme")
            {
                StyleManager.ApplicationTheme = new Expression_DarkTheme();
            }
            else if (Properties.Settings.Default.theme == "Office2016") { StyleManager.ApplicationTheme = new Office2016Theme(); }
            else if (Properties.Settings.Default.theme == "Office2016Touch")
            {
                StyleManager.ApplicationTheme = new Office2016TouchTheme();
            }
            else if (Properties.Settings.Default.theme == "Office2013") { }
            else if (Properties.Settings.Default.theme == "Material") { StyleManager.ApplicationTheme = new MaterialTheme(); }
            else if (Properties.Settings.Default.theme == "Fluent") { StyleManager.ApplicationTheme = new FluentTheme(); }
            else if (Properties.Settings.Default.theme == "Crystal") { StyleManager.ApplicationTheme = new CrystalTheme(); }
            else if (Properties.Settings.Default.theme == "Green") { }
        }

        private void List1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            themename a = (themename)(sender as ComboBox).SelectedItem;

            Properties.Settings.Default.theme = a.theme;
            Properties.Settings.Default.Save();
        }

    }
}
