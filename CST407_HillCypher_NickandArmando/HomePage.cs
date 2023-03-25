using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST407_HillCypher_NickandArmando
{
    public partial class HomePage : Form
    {
        HillCypherHelper helper = new HillCypherHelper();
        public HomePage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PlainTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CipherTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void KeyBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            string plainText = helper.PlainTextPadding(textBox1.Text.ToString());
            string key = textBox3.Text.ToString();
            int[,] keyMatrix = new int[3, 3];
            
            if(helper.KeyChecker(key) && plainText.Length != 0)
            {
                //Error Event Handler & Clear TextBoxes
                key = helper.KeyPadding(key);
                keyMatrix = helper.GetDisplayedKeyMatrix(key);
                if(!helper.HomePageCheckDet(0,keyMatrix) && plainText.All(char.IsLetter))
                {                    
                    keyMatrix = null;

                    //this.Hide();
                    EncryptionPage ep = new EncryptionPage(plainText, key);
                    ep.Show();
                }
                else
                {
                    MessageBox.Show("Key Was Not Valid Or Plain Text Was Not All Letters", "Key or Plain Text Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Key Was Not Between Length Of 1 and 9 Or There Was No Input For Plain Text", "Length of Key Or Plain Text Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            keyMatrix = null;
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            string cipherText = textBox2.Text.ToString();
            string key = textBox3.Text.ToString();
            int[,] keyMatrix = new int[3, 3];

            if (helper.KeyChecker(key) && helper.CipherTextChecker(cipherText) && cipherText.Length != 0)
            {
                key = helper.KeyPadding(key);
                keyMatrix = helper.GetDisplayedKeyMatrix(key);
                if (!helper.HomePageCheckDet(0, keyMatrix) && cipherText.All(char.IsLetter))
                {
                    //Error Event Handler & Clear TextBoxes
                    keyMatrix = null;

                    //this.Hide();
                    DecryptionPage dp = new DecryptionPage(cipherText, key);
                    dp.Show();
                }
                else
                {
                    MessageBox.Show("Key Was Not Valid Or Cipher Text Was Not All Letters", "Key or Plain Text Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Key Length Was Not Between Length Of 1 and 9 Or Cipher Text Was Not Divisble By 3 (Not Including 0)", "Length of Key Or Cipher Text Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            keyMatrix = null;
        }
    }
}
