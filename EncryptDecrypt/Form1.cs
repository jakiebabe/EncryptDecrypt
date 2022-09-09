using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncryptDecrypt.Controller;

namespace EncryptDecrypt
{
    public partial class Form1 : Form
    {
        public string Encryptor(string encryptText)
        {
            try
            {
                if (txtValue.Text == null || txtValue.Text == "")
                {
                    btnClip.Enabled = false;
                    return ("No text detected. Try adding some.");
                }
                else
                {
                    btnClip.Enabled = true;
                }
                var encrypt = new AesController();
                encryptText = encrypt.EncryptString(txtValue.Text.Trim());
            }
            catch
            {
                return ("Error Encypting");
            }
            return (encryptText);
        }

        public string Decryptor(string decryptText)
        {
            try
            {
                if (txtValue.Text == null || txtValue.Text == "")
                {
                    btnClip.Enabled = false;
                    return ("No text detected. Try adding some.");
                }
                else
                {
                    btnClip.Enabled = true;
                }
                var decrypt = new AesController();
                decryptText = decrypt.DecryptString(txtValue.Text.Trim());
            }
            catch
            {
                btnClip.Enabled = false;
                return ("Error Decrypting");
            }
            return (decryptText);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            txtResult.Text = Encryptor(txtValue.Text.Trim());
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {

            txtResult.Text = Decryptor(txtValue.Text.Trim());
        }

        private void btnClip_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtResult.Text.Trim());
            }
            catch
            {
                MessageBox.Show("No text to copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

