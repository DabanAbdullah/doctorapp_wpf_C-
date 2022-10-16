using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing;
using WIA;
using System.Windows;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;
using backup;

namespace WpfApp1
{
    public class roless
    {

        public bool check { get; set; }
        public string role { get; set; }
        public string rname { get; set; }
    }

    public class galleryy
    {

        public string gid { get; set; }
        public string pid { get; set; }
        public string gdate { get; set; }

        public byte[] gimg { get; set; }

    }


    public class usersclass
    {

        public string uid { get; set; }
        public string uname { get; set; }
        public string upass { get; set; }
        public string role { get; set; }
    }

    public class ImageExampleHelper
    {
        private static string SampleImageFolder = "Image/";

        public static void LoadSampleImage(RadImageEditorUI imageEditorUI, string image)
        {
           // MessageBox.Show(GetResourceUri(SampleImageFolder + image).ToString());
            using (Stream stream = Application.GetResourceStream(GetResourceUri(SampleImageFolder + image)).Stream)
            {
                imageEditorUI.Image = new Telerik.Windows.Media.Imaging.RadBitmap(stream);
                imageEditorUI.ApplyTemplate();
                imageEditorUI.ImageEditor.ScaleFactor = 0;
            }
        }

        public static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ImageExampleHelper).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
        public static RadBitmap GetRadBitmap(string resource, IImageFormatProvider provider)
        {
            using (Stream stream = Application.GetResourceStream(new Uri(resource, UriKind.RelativeOrAbsolute)).Stream)
            {
                return provider.Import(stream);
            }
        }
    }


    public class tresulttt
    {
        public string rid { get; set; }
        public string ptid { get; set; }
        public byte[] pic { get; set; }
    }

    public class drug
    {
        public string pdrug { get; set; }

        public string drugname { get; set; }
        public string note { get; set; }


    }


    public class visitt
    {
       
        public string vdate { get; set; }


    }


    public class Test
    {
        public string ptid { get; set; }
      
        public string code { get; set; }
        public string name { get; set; }
        public string result { get; set; }
    }



    public class ViewModel
    {
     public ObservableCollection<drug> contactsList { get; set; }
        static DataTable dt = new DataTable();
        public ViewModel()
        {
            this.contactsList = new ObservableCollection<drug>();
            SqlDataAdapter da = new SqlDataAdapter("select * from drugs ", db.getcon());
            dt.Clear();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                contactsList.Add(new drug()
                {
                    drugname = dr["drug"].ToString(),

                });
            }



        }
    }



    public class ViewModel2
    {
        public ObservableCollection<Test> contactsList { get; set; }
        static DataTable dt = new DataTable();
        public ViewModel2()
        {
            this.contactsList = new ObservableCollection<Test>();
            SqlDataAdapter da = new SqlDataAdapter("select * from test ", db.getcon());
            dt.Clear();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                contactsList.Add(new Test()
                {
                    name = dr["name"].ToString(),
                    code= dr["code"].ToString(),
                  

                });
            }



        }
    }





    public class themename
    {
        public string theme { get; set; }
    }


    class Model
    {

        public string name { get; set; }
      static  DataTable names = new DataTable();
        static public List<string> GetData()
        {
            names.Clear();
            List<string> data = new List<string>();
            SqlDataAdapter da = new SqlDataAdapter("select name from [patient]  ", db.getcon());
           
            da.Fill(names);
       foreach(DataRow dr in names.Rows)
            {
                data.Add(dr["name"].ToString());
            }

          

            return data;
        }
    }


    public class db
    {

        public static void checkk()
        {
            try
            {
                DateTime dt2 = DateTime.Now;

                if ((dt2 - Properties.Settings.Default.last).TotalDays > 20)
                {

                    string test = check.checkinternet();
                    if (test == "Success")
                    {

                        check.backupdb(Properties.Settings.Default.name, Properties.Resources.email, db.getcon().ConnectionString,"doctors" , System.IO.Directory.GetCurrentDirectory() + @"\db.bak");
                        if (check.getdoc(Properties.Settings.Default.name, db.Decrypt(Properties.Resources.conn)) == "0")
                        {
                            Properties.Settings.Default.enabled = "0";
                        }

                        Properties.Settings.Default.last = DateTime.Now;
                        Properties.Settings.Default.Save();

                    }
                }
            }
            catch (Exception ex) { }
        }


        public static galleryy gal=null;

        public static byte[] pic = null;

        public static void SaveClipboardImageToFile(string filePath,BitmapImage im)
        {
           
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(im));
                encoder.Save(fileStream);
            }
        }

        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }


        public static string pid = "",ptid="";

        public static byte[] BitmapSourceToByte(System.Windows.Media.Imaging.BitmapSource source)
        {
            var encoder = new System.Windows.Media.Imaging.JpegBitmapEncoder();
            var frame = System.Windows.Media.Imaging.BitmapFrame.Create(source);
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();

            encoder.Save(stream);
            return stream.ToArray();
        }


        




        // public static string  app = System.AppDomain.CurrentDomain.BaseDirectory + "/";

        public static string image = System.AppDomain.CurrentDomain.BaseDirectory + "/";
        //  public static string config = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"\db.txt");
        public static string config = Properties.Settings.Default.constr;

        public static string uid = "", role = "", username = "", pass = "";
        public static SqlConnection con = new SqlConnection(config);




        public static string lid = "", per = "", name = "";
        public static SqlConnection getcon()
        {
            return con;
        }


        static DataTable dt = new DataTable();
        public static string getmax(string tablename, string field)
        {
            dt.Clear();
            string max = "";
            SqlDataAdapter da = new SqlDataAdapter("select isnull(max(" + field + "),0)+1 as max from " + tablename + "", con);
            da.Fill(dt);
            max = dt.Rows[0]["max"].ToString();

            return max;
        }


        static DataTable dt2 = new DataTable();

        public static string getrecored(string tablename, string field, string value)
        {
            dt2.Clear();

            SqlDataAdapter da = new SqlDataAdapter("select *  from " + tablename + " where " + field + "=" + value + "", con);
            da.Fill(dt2);


            return dt2.Rows.Count.ToString();
        }

        public static byte[] FileToByteArray(string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

        public static byte[] resizeimg(byte[] myBytes, int newWidth, int newHeight)
        {
            System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(myBytes);
            System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
            System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
            System.IO.MemoryStream myResult = new System.IO.MemoryStream();
            newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Gif);  //Or whatever format you want.
            return myResult.ToArray();
        }
      

        public static string docmd(string sql, SqlParameter[] param)
        {
            string result = "";
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddRange(param);
                cmd.ExecuteNonQuery();
                result = "سەرکەوتوو بوو";
                if (con.State == ConnectionState.Closed) con.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }



        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "RESV2PRZHA99795";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }




        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "RESV2PRZHA99795";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }









        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {

                        Type type = temp.GetTypeInfo();

                        object value = GetValue(dr[column.ColumnName], type);

                        pro.SetValue(obj, value, null);

                    }
                    else
                        continue;
                }
            }
            return obj;
        }








        public static object GetValue(object ob, Type targetType)
        {
            if (targetType == null)
            {
                return null;
            }
            else if (targetType == typeof(String))
            {
                return ob + "";
            }
            else if (targetType == typeof(int))
            {
                int i = 0;
                int.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(short))
            {
                short i = 0;
                short.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(long))
            {
                long i = 0;
                long.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(ushort))
            {
                ushort i = 0;
                ushort.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(decimal))
            {
                decimal i = 0;
                decimal.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(ulong))
            {
                ulong i = 0;
                ulong.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(double))
            {
                double i = 0;
                double.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(DateTime))
            {
                DateTime i;
                DateTime.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(Decimal))
            {
                return ob + "";
            }
            else if (targetType == typeof(decimal))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(float))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(byte))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(sbyte))
            {
                // do the parsing here...
            }

            return ob;
        }



      public static ImageSource  ConvertDrawingImageToWPFImage(System.Drawing.Image gdiImg)
        {


            System.Windows.Controls.Image img = new System.Windows.Controls.Image();

            //convert System.Drawing.Image to WPF image
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(gdiImg);
            IntPtr hBitmap = bmp.GetHbitmap();
            System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            img.Source = WpfBitmap;
            img.Width = 500;
            img.Height = 600;
            img.Stretch = System.Windows.Media.Stretch.Fill;
            return WpfBitmap;
        }

        public static Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }

        public static void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel,

        int scanWidthPixels, int scanHeightPixels, int brightnessPercents, int contrastPercents)

        {

            const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";

            const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";

            const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";

            const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";

            const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";

            const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";

            const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";

            const string WIA_SCAN_CONTRAST_PERCENTS = "6155";

            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);

            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);

            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);

            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);

            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, scanWidthPixels);

            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, scanHeightPixels);

            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);

            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);

        }



        private static void SetWIAProperty(IProperties properties, object propName, object propValue)

        {

            Property prop = properties.get_Item(ref propName);

            prop.set_Value(ref propValue);

        }



        private static void SaveImageToPNGFile(ImageFile image, string fileName)

        {

            ImageProcess imgProcess = new ImageProcess();

            object convertFilter = "Convert";

            string convertFilterID = imgProcess.FilterInfos.get_Item(ref convertFilter).FilterID;

            imgProcess.Filters.Add(convertFilterID, 0);

            SetWIAProperty(imgProcess.Filters[imgProcess.Filters.Count].Properties, "FormatID", WIA.FormatID.wiaFormatPNG);

            image = imgProcess.Apply(image);

            image.SaveFile(fileName);

        }







    }



    static class NumberToText

    {



        public static string numtoarab(string num)
        {
            return num.Replace('0', '٠').Replace('1', '١').Replace('2', '٢').Replace('3', '٣').Replace('4', '٤').Replace('5', '٥').Replace('6', '٦').Replace('7', '٧').Replace('8', '٨').Replace('9', '٩');
        }



        private static string[] _ones =
        {
            "سفر",
            "یەک",
            "دوو",
            "سێ",
            "چوار",
            "پێنج",
            "شەش",
            "حەوت",
            "هەشت",
            "نۆ"
        };

        private static string[] _teens =
        {
            "دە",
            "یانزە",
            "دوانزە",
            "سیانزە",
            "جواردە",
            "پانزە",
            "شانزە",
            "حەڤە",
            "هەژدە",
            "نۆزدە"
        };

        private static string[] _tens =
        {
            "",
            "دە",
            "بیست",
            "سی",
            "چل",
            "پەنجا",
            "شەست",
            "حەفتا",
            "هەشتا",
            "نەوە"
        };

        // US Nnumbering:
        private static string[] _thousands =
        {
            "",
            "هەزار",
            "ملێۆن",
            "بلێۆن",
            "تریلوێن",
            "کوادریلیۆن"
        };

        /// <summary>
        /// Converts a numeric value to words suitable for the portion of
        /// a check that writes out the amount.
        /// </summary>
        /// <param name="value">Value to be converted</param>
        /// <returns></returns>
        public static string Convert(decimal value)
        {
            string digits, temp;
            bool showThousands = false;
            bool allZeros = true;

            // Use StringBuilder to build result
            StringBuilder builder = new StringBuilder();
            // Convert integer portion of value to string
            digits = ((long)value).ToString();
            // Traverse characters in reverse order
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int ndigit = (int)(digits[i] - '0');
                int column = (digits.Length - (i + 1));

                // Determine if ones, tens, or hundreds column
                switch (column % 3)
                {
                    case 0:        // Ones position
                        showThousands = true;
                        if (i == 0)
                        {
                            // First digit in number (last in loop)
                            temp = String.Format("{0} ", _ones[ndigit]);
                        }
                        else if (digits[i - 1] == '1')
                        {
                            // This digit is part of "teen" value
                            temp = String.Format("{0} ", _teens[ndigit]);
                            // Skip tens position
                            i--;
                        }
                        else if (ndigit != 0)
                        {
                            // Any non-zero digit
                            temp = String.Format("{0} ", _ones[ndigit]);
                        }
                        else
                        {
                            // This digit is zero. If digit in tens and hundreds
                            // column are also zero, don't show "thousands"
                            temp = String.Empty;
                            // Test for non-zero digit in this grouping
                            if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
                                showThousands = true;
                            else
                                showThousands = false;
                        }

                        // Show "thousands" if non-zero in grouping
                        if (showThousands)
                        {
                            if (column > 0)
                            {
                                temp = String.Format("{0}{1}{2}",
                                    temp,
                                    _thousands[column / 3],
                                    allZeros ? " " : " و ");
                            }
                            // Indicate non-zero digit encountered
                            allZeros = false;
                        }
                        builder.Insert(0, temp);
                        break;

                    case 1:        // Tens column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0}{1}",
                                _tens[ndigit],
                                (digits[i + 1] != '0') ? "و" : " ");
                            builder.Insert(0, temp);
                        }
                        break;

                    case 2:        // Hundreds column
                        if (ndigit > 0)
                        {

                            if (_ones[ndigit].ToString() != "یەک")
                            {
                                temp = String.Format("{0} سەد و", _ones[ndigit]);
                                builder.Insert(0, temp);
                            }
                            else
                            {
                                temp = String.Format("{0} سەد و   ", "");
                                builder.Insert(0, temp);
                            }
                        }
                        break;
                }
            }

            // Append fractional portion/cents
            builder.AppendFormat("", (value - (long)value) * 100);

            // Capitalize first letter
            return String.Format("{0}{1}",
                Char.ToUpper(builder[0]),
                builder.ToString(1, builder.Length - 1));
        }
    }


    public static class UpdateExtensions
    {
        public delegate void Func<TArg0>(TArg0 element);

        public static int Update<TSource>(this IEnumerable<TSource> source, Func<TSource> update)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (update == null) throw new ArgumentNullException("update");
            if (typeof(TSource).IsValueType)
                throw new NotSupportedException("value type elements are not supported by update.");

            int count = 0;
            foreach (TSource element in source)
            {
                update(element);
                count++;
            }
            return count;
        }
    }






    [System.Windows.Data.ValueConversion(typeof(byte[]), typeof(ImageSource))]
    public class BinaryJPEGToImageSourceConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] binaryimagedata = value as byte[];
            using (var ms = new System.IO.MemoryStream(binaryimagedata))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
