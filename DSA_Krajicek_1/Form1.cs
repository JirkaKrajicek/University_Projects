using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSA_Krajicek_1
{
    public partial class Form1 : Form
    {
        private static string OpenMessage;
        private static string OpenHash;
        private static BigInteger N;
        private static BigInteger E;

        public Form1()
        {
            InitializeComponent();                        

        }
        private void VacuumCleaner()
        {
            textBox_sha256.Text = String.Empty;
            textBox_delivery.Text = String.Empty;
            textBox_hashCheck.Text = String.Empty;
        }

        private void button_save_text_Click(object sender, EventArgs e)
        {
            VacuumCleaner();
            Sunflower.CleanUp();

            if (textBox_text.Text == String.Empty)
            {
            MessageBox.Show($"Zadejte vstupní text",
                "Chyba při vstupu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            }
            else
            { 
            OpenMessage = textBox_text.Text;
            Sunflower.File_Open_MSG(OpenMessage);
            string huh = Sunflower.pathForMsg;
            string justName = Path.GetFileNameWithoutExtension(huh);
            string justExtension = Path.GetExtension(huh);
            DateTime dt = File.GetLastWriteTime(huh);

            MessageBox.Show($"Vaše zpráva: {justName} \n ve formátu: {justExtension} \n byla uložena do souboru: \n {Sunflower.pathForMsg} \n v čase: {dt}",
                "Soubor uložen",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            /**********************************************************************************************************/

            button_hash.Enabled = true;
            button_save_text.Enabled = false;
            textBox_text.Enabled = false;
            button_unzip.Enabled = false;
            checkBox_saveKeys.Checked = false;
            }
        }

        private async void button_hash_Click(object sender, EventArgs e)
        {
            OpenHash = Sunflower.getSHA256fromFile(Sunflower.pathForMsg);
            textBox_sha256.Text = OpenHash;

            RSA.GetAllKeys();

            /**********************************************************************************************************/

            button_hash.Enabled = false;
            checkBox_saveKeys.Enabled = true;
            await WaitForKeyGen();
        }

        private async void button_zip_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Zvolte soubor se soukromým klíčem";
            ofd.Filter = "PRIMARY_KEY FILES(*.PRI)|*.PRI;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {                
                string reader = File.ReadAllText(ofd.FileName);
                BigInteger[] bi = RSA.SeparateKeys(reader);
                string encryptedHash = RSA.Encrypt(OpenHash,bi[0],bi[1]);

                Sunflower.ZipIt(OpenMessage, encryptedHash);
                MessageBox.Show("Vaše zpráva i zašifrovaný hash jsou zazipovány",
                    "Úspěšná operace",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                /**********************************************************************************************************/


                button_zip.Enabled = false;
                await WaitForZip();
            }            
        }

        private async void button_unzip_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Zvolte soubor k rozbalení";
            ofd.Filter = "ZIP FILES(*.ZIP)|*.ZIP;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Sunflower.UnZipIt(ofd.FileName);
                MessageBox.Show("Soubor byl rozzipován", "Úspěšná operace", MessageBoxButtons.OK, MessageBoxIcon.Information);

                /**********************************************************************************************************/

                button_unzip.Enabled = false;
                await WaitForUnzip();
            }
        }

        private async void button_choose_pub_key_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Zvolte soubor s veřejným klíčem";
            ofd.Filter = "PUBLIC_KEY FILES(*.PUB)|*.PUB;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string reader = File.ReadAllText(ofd.FileName);
                BigInteger[] bi = RSA.SeparateKeys(reader);
                N = bi[0];
                E = bi[1];

                /**********************************************************************************************************/

                button_choose_pub_key.Enabled = false;
                await WaitForKeys();
            }
        }


        private async void button_show_deliv_hash_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Zvolte soubor s přijatou zprávou";
            ofd.Filter = "TEXT FILE(*.MSG)|*.MSG;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox_delivery.Text = Sunflower.getSHA256fromFile(ofd.FileName);

                /**********************************************************************************************************/

                button_show_deliv_hash.Enabled = false;
                await WaitForHitMan();
            }
        }

        private async void button_choose_encr_hash_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Zvolte elektronický podpis";
            ofd.Filter = "DIGITAL SIGNATURE(*.SIGN)|*.SIGN;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string reader = File.ReadAllText(ofd.FileName);
                textBox_hashCheck.Text = RSA.Decrypt(reader, N, E);

                /**********************************************************************************************************/

                button_choose_encr_hash.Enabled = false;
                checkBox_saveKeys.Checked = false;
                await WorkInProgress();
                await WaitForDecrypt();
            }
        }





        /**********************************************************************************************************/
        /**********************************************************************************************************/


        private async Task WaitForKeyGen()
        {
            button_zip.Enabled = true;
        }

        private async Task WaitForZip()
        {
            button_unzip.Enabled = true;
        }


        private async Task WaitForUnzip()
        {
            button_choose_pub_key.Enabled = true;
        }

        private async Task WaitForKeys()
        {
            button_show_deliv_hash.Enabled = true;
        }

        private async Task WaitForHitMan()
        { 
            button_choose_encr_hash.Enabled = true;
        }

        private async Task WaitForDecrypt()
        {
            button_save_text.Enabled = true;
            textBox_text.Enabled = true;
        }

        private void checkBox_saveKeys_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_saveKeys.Checked == true)
            {
                Sunflower.SaveKeys();
                checkBox_saveKeys.Enabled = false;
            }
        }

        private async Task WorkInProgress()
        {
            if (textBox_delivery.Text != textBox_hashCheck.Text)
            {
                MessageBox.Show("Hash doručené zprávy není správný!", "POZOR NA 'EVU'!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            { 
                MessageBox.Show("Hash doručené zprávy je správný :-)", "Zpráva doručena bez problémů", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sunflower.CleanUp();
        }

        private void textBox_delivery_TextChanged(object sender, EventArgs e)
        {

        }



        private void button_decrypt_hash_Click(object sender, EventArgs e)
        {
            //string decryptedHash = RSA.Decrypt();
        }
    }
}
