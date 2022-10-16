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
using System.Data.SqlClient;
using System.Data;
using System.IO;
using SourceChord.Lighty;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        string pid = "";
        SqlConnection con = db.getcon();
        private string userNameCache;
        private string emailCache;
        byte[] pic ;
        public UserControl1()
        {
            InitializeComponent();
            
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            resultStack.Children.Clear();
            border.Visibility = System.Windows.Visibility.Collapsed;


            if (Properties.Settings.Default.GP == "0") drxelan.Visibility = Visibility.Hidden;
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
            this.imageControl.Source = new BitmapImage(new Uri("Image/profile.jpg", UriKind.RelativeOrAbsolute));
            this.deletePhotoButton.IsEnabled = false;
        }

        private void OnWebCamSnapshotTaken(object sender, SnapshotTakenEventArgs e)
        {
            this.StopCamera();
            this.imageControl.Source = e.Snapshot;
           pic=db.BitmapSourceToByte(  e.Snapshot);
            
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
           
        
            this.saveButton.IsEnabled = true;
           //// this.userNameCache = this.userNameInput.Text;

           

         //   this.userNameInput.Focus();
        }

        private void OnEditEmailButtonClick(object sender, RoutedEventArgs e)
        {
           
         
           
         
          
        }

        private void OnCancelOrSaveButtonClick(object sender, RoutedEventArgs e)
        {
          
         //   this.userNameInput.IsReadOnly = true;
    
        
           
            this.saveButton.IsEnabled = false;

            var btn = (RadButton)sender;
            if (btn.Content.Equals("Cancel"))
            {
               // this.userNameInput.Text = this.userNameCache;
              
                this.userNameCache = string.Empty;
                this.emailCache = string.Empty;
            }
        }

        private void OnExampleUserControlLoaded(object sender, RoutedEventArgs e)
        {
            var parentGrid = this.ParentOfType<Grid>();
            parentGrid.Margin = new Thickness(0, 0, 5, 0);
        }

        string gender = "", blood = "";

       



        public void Bmical()
        {
            try
            {
                double hieght = double.Parse(bala.Text)/100;
                double wieght = double.Parse(kesh.Text);
                var bmi = wieght / (hieght * hieght);
              

                if(bmi< 18.5)
                {
                    byd.Text = "  underweight";
                }
                else if (bmi >= 18.5 && bmi <= 25)
                {
                    byd.Text = "  healthy weight";
                }
                

              
               else if (bmi > 25 && bmi < 29)
                {
                  
                    byd.Text =  "Overweight";
                }

                else if (bmi >=30 )
                {

                    byd.Text = "obese";
                }
                byd.Text = byd.Text + "   " + bmi;
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                byd.Text = "";
            }
        }
        string sql = "";
        SqlParameter[] p;

        private void Names_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Model.GetData();

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear
                resultStack.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list
            resultStack.Children.Clear();

            // Add the result
            foreach (var obj in data)
            {
                if (obj.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work
                    addItem(obj);
                    found = true;
                }
            }

            if (!found)
            {
                resultStack.Children.Add(new TextBlock() { Text = "No results found." });
              
                resultStack.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
        }



        private void addItem(string text)
        {
            TextBlock block = new TextBlock();

            // Add the text
            block.Text = text;

            // A little style...
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            block.MouseDown+= (sender, e) =>
            {

                names.Text = (sender as TextBlock).Text;
                find("name", names.Text);
                var border = (resultStack.Parent as ScrollViewer).Parent as Border;
                resultStack.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            };
            // Mouse events
            block.MouseLeftButtonUp += (sender, e) =>
            {
             
                //names.Text = (sender as TextBlock).Text;
                //var border = (resultStack.Parent as ScrollViewer).Parent as Border;
                //resultStack.Children.Clear();
                //border.Visibility = System.Windows.Visibility.Collapsed;
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel
            resultStack.Children.Add(block);
        }


        DataTable search = new DataTable();

        public void find( string filedname,string value)
        {
            try
            {
                search.Clear();
                SqlDataAdapter da = new SqlDataAdapter("select * from patient where " + filedname + "=N'" + value + "'", con);
                da.Fill(search);
                if (search.Rows.Count > 0)
                {
                  
                    string gender= search.Rows[0]["Gender"].ToString();
                    if (gender == "نێر") gendertogle.IsChecked = true;
                    else gendertogle.IsChecked = false;
                    pid = search.Rows[0]["code"].ToString();
                    db.pid = pid;
                    names.Text = search.Rows[0]["name"].ToString();
                    job.Text = search.Rows[0]["job"].ToString();
                    notee.Document.Blocks.Add(new Paragraph(new Run(search.Rows[0]["note"].ToString())));
                    bdate.Text =(DateTime.Now.Year- int.Parse(search.Rows[0]["bdate"].ToString())).ToString();
                    Mobile.Text = search.Rows[0]["mobile"].ToString();
                    adress.Text = search.Rows[0]["location"].ToString();
                    email.Text = search.Rows[0]["email"].ToString();
                    bala.Text = search.Rows[0]["bala"].ToString();
                    kesh.Text = search.Rows[0]["kesh"].ToString();
                    foreach (FrameworkElement element in st1.Children)
                    {
                        var name = element.Name;
                        RadRadioButton f = (RadRadioButton)element;
                        if (f.Content.ToString() == search.Rows[0]["blood"].ToString())
                            f.IsChecked = true;
                    }
                    try
                    {
                        PP.Text= search.Rows[0]["p"].ToString();
                        gg.Text = search.Rows[0]["g"].ToString();
                        plus.Text = search.Rows[0]["ab"].ToString();
                    }
                    catch { }
                    Bmical();
                    try
                    {
                        pic = (byte[])search.Rows[0]["pic"];
                    
                    DecodePhoto(pic);
                    }
                    catch { }
                    this.saveButton.IsEnabled = false;
                    this.EditButton.IsEnabled = true;
                    this.delete.IsEnabled = true;
                    
                }
                else
                {
                    db.pid = "";
                       pid = "";
                    notee.Document.Blocks.Clear();
                    //  clear();
                    this.saveButton.IsEnabled = true;
                    this.EditButton.IsEnabled = false;
                    this.delete.IsEnabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public void DecodePhoto(byte[] byteVal)
        {


            MemoryStream strmImg = new MemoryStream(byteVal);
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.StreamSource = strmImg;
            myBitmapImage.DecodePixelWidth = 300;
            myBitmapImage.DecodePixelHeight = 300;
            myBitmapImage.EndInit();
            this.imageControl.Source = myBitmapImage;

        }

        private void Names_TextChanged(object sender, TextChangedEventArgs e)
        {
         //   find("name", names.Text);
        }

        private void Names_LostFocus(object sender, RoutedEventArgs e)
        {
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            resultStack.Children.Clear();
            border.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            RadWindow.Confirm("دڵنیایت?", this.onEdit);

        }


        public void clear()
        {
            foreach (FrameworkElement element in st1.Children)
            {
               
                RadRadioButton f = (RadRadioButton)element;
                f.IsChecked = false;
              
              
            }
            this.saveButton.IsEnabled = true;
            this.EditButton.IsEnabled = false;
            this.imageControl.Source = new BitmapImage(new Uri("Image/profile.jpg", UriKind.RelativeOrAbsolute));
            pid = "";
            db.pid = "";
            pic = null;
            this.delete.IsEnabled = false;
            gendertogle.IsChecked = false;
           
            notee.Document.Blocks.Clear();
       PP.Text=gg.Text=plus.Text=  bdate.Text= byd.Text=  job.Text = email.Text = bala.Text = kesh.Text = byd.Text = Mobile.Text = adress.Text = "";
        }

        private void onEdit(object sender, WindowClosedEventArgs e)
        {
            try
            {
                setage();
                var result2 = e.DialogResult;
                if (result2 == true)
                {
                    foreach (FrameworkElement element in st1.Children)
                    {
                        var name = element.Name;
                        RadRadioButton f = (RadRadioButton)element;
                        if (f.IsChecked == true)
                            blood = f.Content.ToString();
                    }


                    string noteee = new TextRange(notee.Document.ContentStart, notee.Document.ContentEnd).Text;
                    if (gendertogle.IsChecked == true) gender = "نێر";
                    else gender = "مێ";

                    string result = "";

                    if (pic != null)
                    {
                       
                        p = new SqlParameter[17];
                        sql = "update patient set name=@2,bdate=@3,Gender=@4,blood=@5,mobile=@6,location=@7,note=@8,pic=@9,email=@10,job=@11,bala=@12,kesh=@13,uid=@14,g=@15,p=@16,ab=@17    where code=@1";
                        p[0] = new SqlParameter("@1", pid);
                        p[1] = new SqlParameter("@2", names.Text);
                        p[2] = new SqlParameter("@3", bdate.Text);
                        p[3] = new SqlParameter("@4", gender);
                        p[4] = new SqlParameter("@5", blood);
                        p[5] = new SqlParameter("@6", Mobile.Text);
                        p[6] = new SqlParameter("@7", adress.Text);
                        p[7] = new SqlParameter("@8", noteee);
                        p[8] = new SqlParameter("@9", pic);
                        p[9] = new SqlParameter("@10", email.Text);
                        p[10] = new SqlParameter("@11", job.Text);
                        p[11] = new SqlParameter("@12", bala.Text);
                        p[12] = new SqlParameter("@13", kesh.Text);
                        p[13] = new SqlParameter("@14", db.uid);
                        p[14] = new SqlParameter("@15", gg.Text);
                        p[15] = new SqlParameter("@16", PP.Text);
                        p[16] = new SqlParameter("@17", plus.Text);
                        result = db.docmd(sql, p);
                    }


                    else
                    {
                      
                        p = new SqlParameter[16];
                        sql = "update patient set name=@2,bdate=@3,Gender=@4,blood=@5,mobile=@6,location=@7,note=@8,email=@9,job=@10,bala=@11,kesh=@12,uid=@13,g=@14,p=@15,ab=@16    where code=@1";
                        p[0] = new SqlParameter("@1", pid);
                        p[1] = new SqlParameter("@2", names.Text);
                        p[2] = new SqlParameter("@3", bdate.Text);
                        p[3] = new SqlParameter("@4", gender);
                        p[4] = new SqlParameter("@5", blood);
                        p[5] = new SqlParameter("@6", Mobile.Text);
                        p[6] = new SqlParameter("@7", adress.Text);
                        p[7] = new SqlParameter("@8", noteee);
                        p[8] = new SqlParameter("@9", email.Text);
                        p[9] = new SqlParameter("@10", job.Text);
                        p[10] = new SqlParameter("@11", bala.Text);
                        p[11] = new SqlParameter("@12", kesh.Text);
                        p[12] = new SqlParameter("@13", db.uid);
                        p[13] = new SqlParameter("@14", gg.Text);
                        p[14] = new SqlParameter("@15", PP.Text);
                        p[15] = new SqlParameter("@16", plus.Text);
                        result = db.docmd(sql, p);

                    }

                    clear();
                    var manager = new RadDesktopAlertManager();
                    manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter);
                    manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, 5);
                    manager = new RadDesktopAlertManager(AlertScreenPosition.TopRight, new Point(0, 0));
                    manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter, new Point(0, 0), 10);
                    manager.ShowAlert(new DesktopAlertParameters
                    {
                        Header = "گۆڕانکاری نەخۆش",
                        Content = result,
                        //    Icon = new Image { Source = Properties.Resources.logo.ToString() as ImageSource, Width = 48, Height = 48 },
                        // IconColumnWidth = 48,
                        IconMargin = new Thickness(10, 0, 20, 0)
                    });
                }
            }catch(Exception ex)
            {
                var manager = new RadDesktopAlertManager();
                manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter);
                manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, 5);
                manager = new RadDesktopAlertManager(AlertScreenPosition.TopRight, new Point(0, 0));
                manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter, new Point(0, 0), 10);
                manager.ShowAlert(new DesktopAlertParameters
                {
                    Header = "گۆڕانکاری نەخۆش",
                    Content = ex.Message,
                    //    Icon = new Image { Source = Properties.Resources.logo.ToString() as ImageSource, Width = 48, Height = 48 },
                    // IconColumnWidth = 48,
                    IconMargin = new Thickness(10, 0, 20, 0)
                });
            }

            
     }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void Kesh_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void Byd_GotFocus(object sender, RoutedEventArgs e)
        {
           

        }

        private void Kesh_LostFocus(object sender, RoutedEventArgs e)
        {
           
            Bmical();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm("دڵنیایت?", this.ondelete);
        }

        private void ondelete(object sender, WindowClosedEventArgs e)
        {
            string result = "";
            var result2 = e.DialogResult;
            if (result2 == true)
            {

                p = new SqlParameter[1];
                sql = "delete from patient    where code=@1";
                p[0] = new SqlParameter("@1", pid);
               
                result = db.docmd(sql, p);
                var manager = new RadDesktopAlertManager();
                manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter);
                manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, 5);
                manager = new RadDesktopAlertManager(AlertScreenPosition.TopRight, new Point(0, 0));
                manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter, new Point(0, 0), 10);
                manager.ShowAlert(new DesktopAlertParameters
                {
                    Header = "گۆڕانکاری نەخۆش",
                    Content = result,
                    //    Icon = new Image { Source = Properties.Resources.logo.ToString() as ImageSource, Width = 48, Height = 48 },
                    // IconColumnWidth = 48,
                    IconMargin = new Thickness(10, 0, 20, 0)
                });
                clear();
                names.Text = "";

            }
            }

        private void Mobile_TextChanged(object sender, TextChangedEventArgs e)
        {
           

        }

        private void Mobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)

                find("mobile", Mobile.Text);
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            wind.Close();
        }

        private void ImageControl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            var image = new Image();
            image.Source = new BitmapImage(new Uri("Image/test.jpg", UriKind.RelativeOrAbsolute));
            LightBox.Show(this, image);
          
        }


        public void setage()
        {
            if (int.Parse(bdate.Text) < 120)
            {
                bdate.Text = (DateTime.Now.Year - int.Parse(bdate.Text)).ToString();
            }
            else { }
        }

        private void Bdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                setage();
               
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                setage();
                foreach (FrameworkElement element in st1.Children)
                {
                    var name = element.Name;
                    RadRadioButton f = (RadRadioButton)element;
                    if (f.IsChecked == true)
                        blood = f.Content.ToString();
                }


                string noteee = new TextRange(notee.Document.ContentStart, notee.Document.ContentEnd).Text;
                if (gendertogle.IsChecked == true) gender = "نێر";
               else gender = "مێ";
                string id = db.getmax("patient", "code");
                string result = "";


                if (pic != null)
                {

                    p = new SqlParameter[17];
                    sql = "insert into patient values(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17)";
                    p[0] = new SqlParameter("@1", id);
                    p[1] = new SqlParameter("@2", names.Text);
                    p[2] = new SqlParameter("@3", bdate.Text);
                    p[3] = new SqlParameter("@4", gender);
                    p[4] = new SqlParameter("@5", blood);
                    p[5] = new SqlParameter("@6", Mobile.Text);
                    p[6] = new SqlParameter("@7", adress.Text);
                    p[7] = new SqlParameter("@8", noteee);
                    p[8] = new SqlParameter("@9", pic);
                    p[9] = new SqlParameter("@10", email.Text);
                    p[10] = new SqlParameter("@11", job.Text);
                    p[11] = new SqlParameter("@12", bala.Text);
                    p[12] = new SqlParameter("@13", kesh.Text);
                    p[13] = new SqlParameter("@14",  db.uid);

                    p[14] = new SqlParameter("@15", gg.Text);
                    p[15] = new SqlParameter("@16", PP.Text);
                    p[16] = new SqlParameter("@17", plus.Text);
                    result = db.docmd(sql, p);
                }


                else
                {
                    p = new SqlParameter[16];
                    sql = "insert into patient(code,name,bdate,Gender,blood,mobile,location,note,email,job,bala,kesh,uid,g,p,ab) values(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16)";
                    p[0] = new SqlParameter("@1", id);
                    p[1] = new SqlParameter("@2", names.Text);
                    p[2] = new SqlParameter("@3", bdate.Text);
                    p[3] = new SqlParameter("@4", gender);
                    p[4] = new SqlParameter("@5", blood);
                    p[5] = new SqlParameter("@6", Mobile.Text);
                    p[6] = new SqlParameter("@7", adress.Text);
                    p[7] = new SqlParameter("@8", noteee);
                    p[8] = new SqlParameter("@9", email.Text);
                    p[9] = new SqlParameter("@10", job.Text);
                    p[10] = new SqlParameter("@11", bala.Text);
                    p[11] = new SqlParameter("@12", kesh.Text);
                    p[12] = new SqlParameter("@13",db.uid);
                    p[13] = new SqlParameter("@14", gg.Text);
                    p[14] = new SqlParameter("@15", PP.Text);
                    p[15] = new SqlParameter("@16", plus.Text);
                    result = db.docmd(sql, p);

                }


                var manager = new RadDesktopAlertManager();
                manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter);
                manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, 5);
                manager = new RadDesktopAlertManager(AlertScreenPosition.TopRight, new Point(0, 0));
                manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter, new Point(0, 0), 10);
                manager.ShowAlert(new DesktopAlertParameters
                {
                    Header = "زیادکردنی نەخۆش",
                    Content = result,
                    //    Icon = new Image { Source = Properties.Resources.logo.ToString() as ImageSource, Width = 48, Height = 48 },
                    // IconColumnWidth = 48,
                    IconMargin = new Thickness(10, 0, 20, 0)
                });
                clear();
                names.Text = "";

            }catch(Exception ex)
            {
                var manager = new RadDesktopAlertManager();
                manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter);
                manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, 5);
                manager = new RadDesktopAlertManager(AlertScreenPosition.TopRight, new Point(0, 0));
                manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter, new Point(0, 0), 10);
                manager.ShowAlert(new DesktopAlertParameters
                {
                    Header = "زیادکردنی نەخۆش",
                    Content = ex.Message,
                    //    Icon = new Image { Source = Properties.Resources.logo.ToString() as ImageSource, Width = 48, Height = 48 },
                    // IconColumnWidth = 48,
                    IconMargin = new Thickness(10, 0, 20, 0)
                });
            }

        }
    }
}
    

