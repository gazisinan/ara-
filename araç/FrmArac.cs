using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace araç
{
    public partial class FrmArac : Form
    {
        public FrmArac()
        {
            InitializeComponent();
        }

        private void OkClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public Arac arac = null;
             
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            propertyGrid1.SelectedObject = arac;
        }


        private void Cancelclicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
