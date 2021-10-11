using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Kry_Afinni_sifra
{
    static class Algoritmus
    {        

        static public char[] Abeceda()
        { 
            char[] abeceda = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            return abeceda;
        }
                
        static public int KontrolaDelitelnosti (int a)
        {
            char[] abeceda = Algoritmus.Abeceda();

             int mnA = abeceda.Length;
             //E.A.
            while (a != 0 && mnA != 0)
            {
                if (a > mnA)
                    a = a % mnA;
                else
                    mnA = mnA % a;
            }

            if ((mnA == 1) || (a == 1))
            {
                return 1;
            }
            else return 0;
            
        }

        static public string KorekceTextu(string VstupniRetezec)
        {
            VstupniRetezec = VstupniRetezec.ToUpper();
            VstupniRetezec = VstupniRetezec.Replace('Á', 'A');
            VstupniRetezec = VstupniRetezec.Replace('Č', 'C');
            VstupniRetezec = VstupniRetezec.Replace('É', 'E');
            VstupniRetezec = VstupniRetezec.Replace('Ě', 'E');
            VstupniRetezec = VstupniRetezec.Replace('Í', 'I');
            VstupniRetezec = VstupniRetezec.Replace('Ň', 'N');
            VstupniRetezec = VstupniRetezec.Replace('Ó', 'O');
            VstupniRetezec = VstupniRetezec.Replace('Ř', 'R');
            VstupniRetezec = VstupniRetezec.Replace('Š', 'S');
            VstupniRetezec = VstupniRetezec.Replace('Ť', 'T');
            VstupniRetezec = VstupniRetezec.Replace('Ů', 'U');
            VstupniRetezec = VstupniRetezec.Replace('Ú', 'U');
            VstupniRetezec = VstupniRetezec.Replace('Ý', 'Y');
            VstupniRetezec = VstupniRetezec.Replace('Ž', 'Z');
            

            char[] poleVstupZnaky = VstupniRetezec.ToCharArray();
            for (int i = 0; i < VstupniRetezec.Length; i++)
            {
                int ascii = Convert.ToInt32(poleVstupZnaky[i]);
                if((((ascii < 65)||(ascii>90))&&(ascii!=32)))
                {
                    poleVstupZnaky[i] = '!';
                }
            }

            VstupniRetezec = string.Join("", poleVstupZnaky);

            VstupniRetezec = VstupniRetezec.Replace("!", "");
            return VstupniRetezec;
        }

        static public int[] PrevedZnakNaCisloVstup(string VstupniRetezec)
        {
            char[] abeceda = Algoritmus.Abeceda();
                     
            char[] poleVstupZnaky = VstupniRetezec.ToCharArray();
                                  
            int[] polePoziceZnaku = new int[poleVstupZnaky.Length];

            for (int i = 0; i < poleVstupZnaky.Length; i++)
            {
                int ascii = Convert.ToInt32(poleVstupZnaky[i]);
                if ((ascii >= 65) && (ascii <= 90))
                {
                    for (int j = 0; j < abeceda.Length; j++)
                    {
                        if (poleVstupZnaky[i] == abeceda[j])
                        {
                            int poziceZnaku = Array.IndexOf(abeceda, poleVstupZnaky[i]);
                            polePoziceZnaku[i] = poziceZnaku;                            
                        }
                    }
                }
                else if (ascii == 32) //mezera
                {
                    polePoziceZnaku[i] = -1;
                }
            }
            return polePoziceZnaku;
        }

        static public int[] ZasifrovaniCisel(int[] poleCiselKZasifrovani, int klicA, int klicB)
        {
            int[] poleZasifrovanaCisla = new int[poleCiselKZasifrovani.Length];
            char[] abeceda = Algoritmus.Abeceda();

            for (int i = 0; i < poleCiselKZasifrovani.Length; i++)
            {
                if (poleCiselKZasifrovani[i] == -1) //mezera
                {
                    poleZasifrovanaCisla[i] = -1;
                }
                else
                {
                    int sifraVypocet = (klicA * poleCiselKZasifrovani[i] + klicB) % abeceda.Count();
                    poleZasifrovanaCisla[i] = sifraVypocet;
                }
            }

            return poleZasifrovanaCisla;
        }

        static public string ZasifrovaniZCiselDoRetezce(int[] poleCiselKPrelozeni)
        {
            char[] abeceda = Algoritmus.Abeceda();
            string[] poleZasifrovanyRetezec = new string[poleCiselKPrelozeni.Length];

            for (int i = 0; i < poleCiselKPrelozeni.Length; i++)
            {
                try
                {
                    var znak = abeceda.GetValue(poleCiselKPrelozeni[i]);
                    string pismeno = znak.ToString();
                    poleZasifrovanyRetezec[i] = pismeno;
                }
                catch (System.IndexOutOfRangeException)
                {
                    poleZasifrovanyRetezec[i] = "XQW";
                }
            }

            //mezery
            string text = string.Join("", poleZasifrovanyRetezec);

            for (int i = 5; i < text.Length; i += 5)
            {
                text = text.Insert(i, " ");
                i++;
            }
            return text;
        }

        static public int[] RozsifrovaniCisel(int[] poleCisel, int klicA, int klicB)
        {

            char[] abeceda = Algoritmus.Abeceda();
            int mnA = abeceda.Length;

            //modularni multiplikativni inverze
            int[] poleRozsifrovanaCisla = new int[poleCisel.Length];
            int inverze;

            for (int i = 0; i < abeceda.Length; i++)
            {
                int vypocet = (klicA * i) % mnA;
                if (vypocet == 1)
                {
                    inverze = i;

                    //rozsifrovani cisel
                    for (int j = 0; j < poleCisel.Length; j++)
                    {
                        if (poleCisel[j] == -1)
                        {
                            poleRozsifrovanaCisla[j] = -1;
                        }
                        else
                        {
                            double desifrovani = ((poleCisel[j] - klicB) * inverze);
                            double korekce = desifrovani - Math.Floor(desifrovani / mnA) * mnA;

                            poleRozsifrovanaCisla[j] = Convert.ToInt32(korekce);
                        }
                    }
                    break;
                }
            }
            return poleRozsifrovanaCisla;
        }

        static public string[] RozsifrovaniCiselDoTextu(int[] poleCisel)
        {
            char[] abeceda = Algoritmus.Abeceda();

            //rozsifrovani cisel do textu
            string[] poleRozsifrovanyRetezec = new string[poleCisel.Length];

            for (int i = 0; i < poleCisel.Length; i++)
            {                
               if (poleCisel[i] == -1)
               {
                   poleRozsifrovanyRetezec[i] = " ";
               }
               else
               {
                   var znak = abeceda.GetValue(poleCisel[i]);
                   string pismeno = znak.ToString();
                   poleRozsifrovanyRetezec[i] = pismeno;
               }
              
            }            
            return poleRozsifrovanyRetezec;
        }
    }
}
