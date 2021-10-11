using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Kry_Afinni_sifra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            char[] zdrojAbeceda = Algoritmus.Abeceda();  

            //zobrazeni zdrojove abecedy s indexy
            int[] indexZdrojoveAbecedy = new int[zdrojAbeceda.Length];
            for (int i = 0; i < zdrojAbeceda.Length; i++)
            {
                indexZdrojoveAbecedy[i] = i;
            }

            string[] vypisZdrojAbecedy = new string[zdrojAbeceda.Length];
            for (int i = 0; i < zdrojAbeceda.Length; i++)
            {
                vypisZdrojAbecedy[i] = zdrojAbeceda[i] + "=" + indexZdrojoveAbecedy[i];
            }
            label2.Text = string.Join("| ", vypisZdrojAbecedy);



            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //oznaceni vstupu
            int a = Convert.ToInt32(textBoxVstupniKlicA.Text);
            int b = Convert.ToInt32(textBoxVstupniKlicB.Text);
            a = Math.Abs(a);
            b = Math.Abs(b);

            string retezecVstup = textBoxVstupniRetezec.Text;           
            char[] zdrojAbeceda = Algoritmus.Abeceda();

            

            int kontrola = Algoritmus.KontrolaDelitelnosti(a);
            if (kontrola != 1)
            {
                MessageBox.Show("Chyba při vstupu");
            }
            else
            {
                //zobrazeni zdrojove abecedy s indexy
                int[] indexZdrojoveAbecedy = new int[zdrojAbeceda.Length];
                for (int i = 0; i < zdrojAbeceda.Length; i++)
                {
                    indexZdrojoveAbecedy[i] = i;
                }

                int[] sifrovanaAbeceda = Algoritmus.ZasifrovaniCisel(indexZdrojoveAbecedy, a, b);

                string[] vypisSifrovaneAbecedy = new string[zdrojAbeceda.Length];
                for (int i = 0; i < zdrojAbeceda.Length; i++)
                {
                    vypisSifrovaneAbecedy[i] = zdrojAbeceda[i] + "=" + sifrovanaAbeceda[i];
                }
                label3.Text = string.Join("| ", vypisSifrovaneAbecedy);
                
                




                //uprava vstupniho textu            
                retezecVstup = Algoritmus.KorekceTextu(retezecVstup);
                textBoxUpravenyVstupniText.Text = retezecVstup;



                //prevod vstupniho retezce na ciselne hodnoty           
                int[] polePoziceZnaku = Algoritmus.PrevedZnakNaCisloVstup(retezecVstup);
                string Vypis1 = string.Join(",", polePoziceZnaku);
                Vypis1 = Vypis1.Replace("-1", " ");
                textBoxVstupniHodnoty.Text = Vypis1;




                //zasifrovani cisel            
                int[] poleZasifrovanaCisla = Algoritmus.ZasifrovaniCisel(polePoziceZnaku, a, b);
                string Vypis2 = string.Join(",", poleZasifrovanaCisla);
                Vypis2 = Vypis2.Replace("-1", " ");
                textBoxZasifrovanaCislaVystup.Text = Vypis2;





                //prevedeni zasifrovanych cisel na text
                string Vypis3 = Algoritmus.ZasifrovaniZCiselDoRetezce(poleZasifrovanaCisla);
                textBoxZasifrovanyVystup.Text = Vypis3;                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(textBoxVstupniKlicA.Text);
            int b = Convert.ToInt32(textBoxVstupniKlicB.Text);
            a = Math.Abs(a);
            b = Math.Abs(b);

            string retezecVstup = textBoxVstupniRetezecZasifrovany.Text;            
            retezecVstup = Algoritmus.KorekceTextu(retezecVstup);
            retezecVstup = retezecVstup.Replace(" ", "");
            retezecVstup = retezecVstup.Replace("XQW", " ");
            textBoxUpravenyVstupniTextRozsifrovani.Text = retezecVstup;


            //zjisteni hodnot znaku
            int[] polePoziceZnaku = Algoritmus.PrevedZnakNaCisloVstup(retezecVstup);
            string Vypis1 = string.Join(",", polePoziceZnaku);
            Vypis1 = Vypis1.Replace("-1", " ");
            textBoxVstupniHodnotyRozsifrovani.Text = Vypis1;
            


                        
            //rozsifrovani zasifrovanych cisel
            int[] poleRozsifrovanaCisla = Algoritmus.RozsifrovaniCisel(polePoziceZnaku, a, b);
            string Vypis2 = string.Join(",",poleRozsifrovanaCisla);
            Vypis2 = Vypis2.Replace("-1", " ");
            textBoxRozsifrovanaCislaVystup.Text = Vypis2;



            //prevedeni rozsifrovanych cisel do textu
            string[] poleRozsifrovanyRetezec = Algoritmus.RozsifrovaniCiselDoTextu(poleRozsifrovanaCisla);
            string Vypis3 = string.Join("", poleRozsifrovanyRetezec);
            textBoxRozsifrovanyVystup.Text = Vypis3;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxZasifrovanyVystup.Text != null)
            {
                textBoxVstupniRetezecZasifrovany.Text = textBoxZasifrovanyVystup.Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBoxRozsifrovanyVystup.Text != null)
            {
                textBoxVstupniRetezec.Text = textBoxRozsifrovanyVystup.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                int a = Convert.ToInt32(textBoxVstupniKlicA.Text);
                int b = Convert.ToInt32(textBoxVstupniKlicB.Text);
                a = Math.Abs(a);
                b = Math.Abs(b);

                if ((a == 0) || (b == 0))
                {
                    MessageBox.Show("Vstupní klíče musí být nenulové!");

                }
                else
                { 
                    int kontrola = Algoritmus.KontrolaDelitelnosti(a);
                    if (kontrola == 1)
                    {
                        MessageBox.Show("Klíče jsou platné");
                    }
                    else MessageBox.Show("Zvolte jiný vstupní klíč 'a', např. 3,5,7,9,11 atd...");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Vstupní klíč musí být celé číslo!");
            }
           

           
        }

        private void textBoxVstupniKlicB_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
                             
        private void textBoxZdrojovaAbecedaIndexy_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBoxZdrojovaAbeceda_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBoxVstupniKlicA_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
