using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
            {
                Label l = new Label();
                l.Name = "a" + currentProperty.Name;
                l.Text = currentProperty.Name;
                flowLayoutPanel1.Controls.Add(l);

                TextBox t = new TextBox();
                t.Width = 200;
                t.Name = currentProperty.Name;
                t.Text = Properties.Settings.Default[currentProperty.Name].ToString();

                flowLayoutPanel1.Controls.Add(t);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control txt in flowLayoutPanel1.Controls.Cast<Control>().OrderBy(c => c.TabIndex))
            {
                if (txt is TextBox)
                {
                    //  MessageBox.Show(txt.Name + " = " + txt.Text);
                    if (txt.Name == "last")
                    {
                        Properties.Settings.Default[txt.Name] = DateTime.Parse(txt.Text);
                    }
                    else if (txt.Name == "them2")
                    {
                        Properties.Settings.Default[txt.Name] = int.Parse(txt.Text);
                    }
                    else
                    {
                        Properties.Settings.Default[txt.Name] = txt.Text;
                    }
                }

            }

            Properties.Settings.Default.Save();


            flowLayoutPanel1.Controls.Clear();

            foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
            {
                Label l = new Label();
                l.Name = "a" + currentProperty.Name;
                l.Text = currentProperty.Name;
                flowLayoutPanel1.Controls.Add(l);

                TextBox t = new TextBox();
                t.Width = 200;
                t.Name = currentProperty.Name;
                t.Text = Properties.Settings.Default[currentProperty.Name].ToString();

                flowLayoutPanel1.Controls.Add(t);

            }

        }
    }
}
