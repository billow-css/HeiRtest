using System;
using System.Windows.Forms;

namespace HeiResturant.Forms
{
    public partial class InputDialog : Form
    {
        public string InputText => txtInput.Text.Trim();

        public InputDialog(string title, string prompt)
        {
            InitializeComponent(title, prompt);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
