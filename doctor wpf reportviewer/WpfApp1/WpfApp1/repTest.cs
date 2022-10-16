using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class repTest : Form
    {
        public repTest()
        {
            InitializeComponent();
        }

        private void repTest_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.VTest' table. You can move, or remove it, as needed.
            this.VTestTableAdapter.Fill(this.DataSet1.VTest);

            this.reportViewer1.RefreshReport();
        }
    }
}
