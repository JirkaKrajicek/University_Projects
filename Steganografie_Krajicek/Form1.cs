using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganografie_Krajicek
{
    public partial class Form1 : Form
    {
        public static string FileNameNonExten { get; set; }
        private Bitmap BitMapToEncode { get; set; }

        Dictionary<char, char> diacriticsDictionary = new Dictionary<char, char>()
        {
            { 'á','a'},
            { 'č','c'},
            { 'ď','d'},
            { 'é','e'},
            { 'ě','e'},
            { 'í','i'},
            { 'ň','n'},
            { 'ó','o'},
            { 'ř','r'},
            { 'š','s'},
            { 'ť','t'},
            { 'ú','u'},
            { 'ů','u'},
            { 'ý','y'},
            { 'ž','z'},
            { 'Á','A'},
            { 'Č','C'},
            { 'Ď','D'},
            { 'É','E'},
            { 'Ě','E'},
            { 'Í','I'},
            { 'Ň','N'},
            { 'Ó','O'},
            { 'Ř','R'},
            { 'Š','S'},
            { 'Ť','T'},
            { 'Ú','U'},
            { 'Ů','U'},
            { 'Ý','Y'},
            { 'Ž','Z'},
        };


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //cesta k obrazku
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.PNG)|*.BMP;*.PNG;";
            ofd.Title = "Zvolte obrázek";            

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                FileNameNonExten = Path.GetFileNameWithoutExtension(path);

                BitMapToEncode = new Bitmap(path);
               
                textBox1.Text = path;

                pictureBox1.Load(path);

                double imageWidth = BitMapToEncode.Width;
                double imageHeight = BitMapToEncode.Height;
                string resolution = $"{imageWidth}x{imageHeight} pixelů";
                textBox3.Text = resolution;

                double maxLength = Math.Floor(((imageWidth * imageHeight) / 8) - 1);
                if (maxLength < 0) maxLength = 0;
                textBox4.Text = maxLength.ToString();
            }
            
        }

        private void button2_Click(object sender, EventArgs e) //encode
        {
            string toEncode = textBox2.Text;
            int length = 0;

            try
            {
                length = Convert.ToInt32(textBox4.Text);
            }
            catch (Exception)
            {
                MessageBox.Show($"Nejprve zvolte obrázek", "Vstupní chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (toEncode.Length > length)
            {
                MessageBox.Show($"Text k zakódování je příliš dlouhý. Maximální počet znaků je: {length}", "Vstupní chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                char[] charArray = toEncode.ToCharArray();
                for (int i = 0; i < charArray.Length; i++)
                {
                    if (charArray[i] == '0')
                    {
                        MessageBox.Show("Ve vstupním řetězci se nesmí objevit znak '0'", "Vstupní chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button3_Click(sender, e);
                    }

                    foreach (var charakter in diacriticsDictionary)
                    {
                        if (charArray[i] == charakter.Key)
                        {
                            charArray[i] = charakter.Value;
                        }
                    }
                    
                }

                toEncode = string.Join("", charArray);
                toEncode = toEncode + "0";
                textBox5.Text = toEncode;
                
                ImageCode.EncodeImage(BitMapToEncode,toEncode);
                string pathToEditedImage = ImageCode.PathToEditedImage;
                pictureBox2.Load(pathToEditedImage);
            }
        }


        private void button3_Click(object sender, EventArgs e) //restart
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button4_Click(object sender, EventArgs e) //cesta k zakodovanemo obrazku
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.PNG)|*.BMP;*.PNG;";
            ofd.Title = "Zvolte obrázek";
            


            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                string encodedFileName = Path.GetFileNameWithoutExtension(path);

                BitMapToEncode = new Bitmap(path);

                bool youSure = ImageCode.EnsureImageIsEdited(encodedFileName);

                if (youSure == false)
                {
                    MessageBox.Show("Není zaručeno, že vybraný obrázek lze dekódovat", "Vstupní chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox7.Text = "";
                }
                else
                {
                    textBox7.Text = path;
                }
            }

        }




        private void button5_Click(object sender, EventArgs e) //decode
        {
            string decodedText;

            if (checkBox1.Checked && ImageCode.PathToEditedImage != null)
            {
                decodedText = ImageCode.DecodeImage(ImageCode.PathToEditedImage);
                textBox7.Text = ImageCode.PathToEditedImage;
                textBox6.Text = decodedText;

            }
            else if (checkBox1.Checked == false && textBox7.Text != "")
            {
                pictureBox2.Load(textBox7.Text);
                decodedText = ImageCode.DecodeImage(textBox7.Text);
                textBox6.Text = decodedText;
            }
            else if (checkBox1.Checked && textBox7.Text != "")
            {
                try
                {
                    decodedText = ImageCode.DecodeImage(ImageCode.PathToEditedImage);
                    textBox7.Text = ImageCode.PathToEditedImage;
                    textBox6.Text = decodedText;
                }
                catch (Exception)
                {
                    pictureBox2.Load(textBox7.Text);
                    decodedText = ImageCode.DecodeImage(textBox7.Text);
                    textBox6.Text = decodedText;
                }
            }
            else
            {
                MessageBox.Show("Není zadaná cesta k obrázku", "Vstupní chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
