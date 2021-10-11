using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Playfairova_sifra_Krajicek
{
    public partial class Form1 : Form
    {
        //3paar#za xxdo(zx
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string klic = textBox1.Text;

            if (textBox6.Text.Length == 0)
            {
                MessageBox.Show("Před zašifrováním je nutné nechat si zkontrolovat vstupní klíč!","Chyba při vstupu");
            }
            else
            {
                string vstupniRetezec = textBox2.Text;

                if (vstupniRetezec.Length < 2)
                {
                    MessageBox.Show("Řetězec k zašifrování musí mít více jak 1 písmeno!","Chyba při vstupu");
                }
                else
                {

                    klic = Upravy.KorekceTextuKlic(klic);
                    klic = Upravy.ZahodSkaredeZnakyICislovky(klic);
                    klic = new string(klic.Distinct().ToArray());

                    textBox6.Text = klic;

                    string klicA = Upravy.DoplnKlicNaMatici(klic);


                    vstupniRetezec = Upravy.KorekceTextuSifrovani(vstupniRetezec);
                    

                    if (checkBox1.Checked && checkBox2.Checked)
                    {
                        vstupniRetezec = Upravy.ZasifrovaniCisel(vstupniRetezec);
                        vstupniRetezec = Upravy.ZasifrovaniSpecialnichZnaku(vstupniRetezec);
                        goto pokracuj;
                    }
                    else if (checkBox1.Checked)
                    {
                        vstupniRetezec = Upravy.ZasifrovaniCisel(vstupniRetezec);
                        goto pokracuj;
                    }
                    else if (checkBox2.Checked)
                    {
                        vstupniRetezec = Upravy.ZasifrovaniSpecialnichZnaku(vstupniRetezec);
                        goto pokracuj;
                    }
                    else goto pokracuj;


                    pokracuj:
                    vstupniRetezec = Upravy.ZahodSkaredeZnakyICislovky(vstupniRetezec);
                    textBox7.Text = vstupniRetezec;

                    vstupniRetezec = Upravy.VyresParadox(vstupniRetezec);



                    //mezery po 2
                    
                    string vypis = Upravy.VytvorDvojicky(vstupniRetezec);                    
                    textBox8.Text = vypis;


                    char[] poleZnaku = vstupniRetezec.ToCharArray();

                    char[,] matrix = new char[5, 5];


                    //zarazeni klice do matice
                    for (int i = 0; i < matrix.GetLength(0); i++) //radek
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++) //sloupec
                        {
                            matrix[i, j] = Convert.ToChar(klicA.Substring(matrix.GetLength(0) * i + j, 1));
                        }
                    }
                    
                    //vytvoreni tabulky
                    Table t = Table.VratInstanci(matrix, klic);
                    t.Show();

                    char[] retezec = Matrix.ZasifrujRetezec(matrix, poleZnaku);
                    string vyslednyRetezec = string.Join("", retezec);

                    vyslednyRetezec = Upravy.VytvorPetice(vyslednyRetezec);                    
                    
                    textBox3.Text = vyslednyRetezec;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }






        private void button2_Click(object sender, EventArgs e)
        {
            string klic = textBox1.Text;

            if (textBox6.Text.Length == 0)
            {
                MessageBox.Show("Před dešifrováním je nutné nechat si zkontrolovat vstupní klíč!","Chyba při vstupu");
            }
            else
            {
                string vstupniRetezec = textBox4.Text;

                if (vstupniRetezec.Length < 2)
                {
                    MessageBox.Show("Řetězec k zašifrování musí mít více jak 1 písmeno!","Chyba při vstupu");
                }
                else
                {
                    klic = Upravy.KorekceTextuKlic(klic);
                    klic = Upravy.ZahodSkaredeZnakyICislovky(klic);
                    klic = new string(klic.Distinct().ToArray());
                    textBox6.Text = klic;


                    string klicA = Upravy.DoplnKlicNaMatici(klic);
                                        
                    vstupniRetezec = Upravy.KorekceTextuDesifrovani(vstupniRetezec);
                    vstupniRetezec = Upravy.ZahodSkaredeZnakyICislovky(vstupniRetezec);
                    textBox9.Text = vstupniRetezec;
                    

                    char[] poleZnaku = vstupniRetezec.ToCharArray();

                    char[,] matrix = new char[5, 5];

                    for (int i = 0; i < matrix.GetLength(0); i++) //radek
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++) //sloupec
                        {
                            matrix[i, j] = Convert.ToChar(klicA.Substring(matrix.GetLength(0) * i + j, 1));
                        }
                    }

                    Table t = Table.VratInstanci(matrix, klic);
                    t.Show();

                    
                    try
                    {
                        char[] retezec = Matrix.RozsifrujRetezec(matrix, poleZnaku);

                        string vystupniRetezec = string.Join("", retezec);

                        string vypis = Upravy.VytvorDvojicky(vystupniRetezec);
                        textBox10.Text = vypis;

                        vystupniRetezec = Upravy.ZbavSePrebytecnychXaQ(vystupniRetezec);
                        vystupniRetezec = Upravy.DesifrovaniSpecialnichZnaku(vystupniRetezec);
                        vystupniRetezec = Upravy.DesifrovaniCisel(vystupniRetezec);
                        vystupniRetezec = vystupniRetezec.Replace("XGZQL", "X");
                        textBox5.Text = vystupniRetezec;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Zadaný text není korektní!","Chyba při vstupu");                       
                    }
                    
                    
                    
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string klic = textBox1.Text;
            klic = Upravy.KorekceTextuKlic(klic);
            bool odezva = Upravy.ZkontrolujVstupniKlic(klic);

            if (odezva == true)
            {
                klic = Upravy.ZahodSkaredeZnakyICislovky(klic);
                klic = new string(klic.Distinct().ToArray());
                textBox6.Text = klic;
            }
            else
            {
                MessageBox.Show("Vstupní klíč je nevhodný! Musí obsahovat alespoň 6 písmen.","Chyba při vstupu");
            }

            
        }




        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
