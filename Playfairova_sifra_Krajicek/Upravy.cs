using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfairova_sifra_Krajicek
{
    static class Upravy
    {

        static public bool ZkontrolujVstupniKlic(string klic)
        {
            char[] poleVstupZnaky = klic.ToCharArray();
            List<char> pocetZnaku = new List<char>();

            for (int i = 0; i < klic.Length; i++)
            {
                int ascii = Convert.ToInt32(poleVstupZnaky[i]);
                if ((ascii >= 65) && (ascii <= 90))
                {
                    pocetZnaku.Add(poleVstupZnaky[i]);
                }
            }

            if (pocetZnaku.Count >= 6)
            {
                return true;
            }
            else return false;
           
        }

        static public string DoplnKlicNaMatici(string VstupniRetezec)
        {
            string abeceda = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
            
            string core = VstupniRetezec + abeceda;
            core = new string(core.Distinct().ToArray());
            
            return core;
        }


        static public string ZasifrovaniSpecialnichZnaku(string VstupniRetezec)
        {
            VstupniRetezec = VstupniRetezec.Replace("!", "QVYKRICNIKZ");
            VstupniRetezec = VstupniRetezec.Replace("?", "QOTAZNIKZ");
            VstupniRetezec = VstupniRetezec.Replace("*", "QHVEZDICKAZ");
            VstupniRetezec = VstupniRetezec.Replace("/", "QLOMENOZ");
            VstupniRetezec = VstupniRetezec.Replace("-", "QMINUSZ");
            VstupniRetezec = VstupniRetezec.Replace("+", "QPLUSZ");
            VstupniRetezec = VstupniRetezec.Replace("=", "QROVNASEZ");
            VstupniRetezec = VstupniRetezec.Replace(",", "QCARKAZ");
            VstupniRetezec = VstupniRetezec.Replace(".", "QTECKAZ");
            VstupniRetezec = VstupniRetezec.Replace(":", "QDVOJTECKAZ");
            VstupniRetezec = VstupniRetezec.Replace("(", "QLZAVORKAZ");
            VstupniRetezec = VstupniRetezec.Replace(")", "QPZAVORKAZ");
            VstupniRetezec = VstupniRetezec.Replace("#", "QHASHTAGZ");
            VstupniRetezec = VstupniRetezec.Replace("&", "QANDZ");

            return VstupniRetezec;
        }



        static public string ZasifrovaniCisel(string VstupniRetezec)
        {
            VstupniRetezec = VstupniRetezec.Replace("0", "QNULAZ");
            VstupniRetezec = VstupniRetezec.Replace("1", "QJEDNAZ");
            VstupniRetezec = VstupniRetezec.Replace("2", "QDVAZ");
            VstupniRetezec = VstupniRetezec.Replace("3", "QTRIZ");
            VstupniRetezec = VstupniRetezec.Replace("4", "QCTYRIZ");
            VstupniRetezec = VstupniRetezec.Replace("5", "QPETZ");
            VstupniRetezec = VstupniRetezec.Replace("6", "QSESTZ");
            VstupniRetezec = VstupniRetezec.Replace("7", "QSEDMZ");
            VstupniRetezec = VstupniRetezec.Replace("8", "QOSMZ");
            VstupniRetezec = VstupniRetezec.Replace("9", "QDEVETZ");

            return VstupniRetezec;
        }

        static public string KorekceTextuKlic(string VstupniRetezec)
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

            VstupniRetezec = VstupniRetezec.Replace(" ", "");
            VstupniRetezec = VstupniRetezec.Replace('W', 'V');


            return VstupniRetezec;
        }

        static public string KorekceTextuSifrovani(string VstupniRetezec)
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

            VstupniRetezec = VstupniRetezec.Replace(" ", "QMEZERAZ");
            VstupniRetezec = VstupniRetezec.Replace('W', 'V');

            char[] retezec = VstupniRetezec.ToCharArray();
           
                if (retezec[VstupniRetezec.Length - 1] == 'X')
                {
                    VstupniRetezec = VstupniRetezec + "GZQL";
                }
  


            return VstupniRetezec;
        }



        static public string ZahodSkaredeZnakyICislovky(string VstupniRetezec)
        {
            char[] poleVstupZnaky = VstupniRetezec.ToCharArray();
            for (int i = 0; i < VstupniRetezec.Length; i++)
            {
                int ascii = Convert.ToInt32(poleVstupZnaky[i]);
                if ((ascii < 65) || (ascii > 90))
                {
                    poleVstupZnaky[i] = '§';
                }
            }

            VstupniRetezec = string.Join("", poleVstupZnaky);

            VstupniRetezec = VstupniRetezec.Replace("§","");
            return VstupniRetezec;
        }



        static public string VyresParadox(string vstupniRetezec)
        {   
            string text = vstupniRetezec;
            char[] poleText = text.ToCharArray();


            goto proces0;


            proces0:

            poleText = text.ToCharArray();

            for (int i = 0; i < poleText.Length; i++)
            {                
                if (i < poleText.Length - 1)
                {

                    if (poleText[i] == poleText[i + 1]) //i+1 jde mimo pole!
                    {
                        if ((poleText[i] == 'X') && (poleText[i + 1] == 'X'))
                        {
                            text = text.Replace(Convert.ToString(poleText[i]) + Convert.ToString(poleText[i + 1]), Convert.ToString(poleText[i]) + "Q" + Convert.ToString(poleText[i + 1]));
                        }
                        else
                        {
                            text = text.Replace(Convert.ToString(poleText[i]) + Convert.ToString(poleText[i + 1]), Convert.ToString(poleText[i]) + "X" + Convert.ToString(poleText[i + 1]));
                        }
                    }
                }
                else if (i == poleText.Length) //osetreni predposledniho a posledniho indexu v poli
                {
                    if (poleText[i] == poleText[i - 1])
                    {
                        if ((poleText[i] == 'X') && (poleText[i + 1] == 'X'))
                        {
                            text = text.Replace(Convert.ToString(poleText[i]) + Convert.ToString(poleText[i + 1]), Convert.ToString(poleText[i]) + "Q" + Convert.ToString(poleText[i + 1]));
                        }
                        else
                        {
                            text = text.Replace(Convert.ToString(poleText[i]) + Convert.ToString(poleText[i + 1]), Convert.ToString(poleText[i]) + "X" + Convert.ToString(poleText[i + 1]));
                        }
                    }
                }
            }
            

            if ((text.Length % 2) != 0)
            {
                goto proces1; 
            }
            else goto zaver;
          




            proces1:

            poleText = text.ToCharArray();
            if ((poleText.Length % 2) != 0)
            {
                goto proces2;
            }
            else goto proces0;




            proces2:

            for (int i = poleText.Length - 1; i > 0; i--)
            {
                if (poleText[i] == 'X')
                {
                    text = string.Join("", poleText);
                    text = text + "Q";
                    goto proces1;
                }
                else
                {
                    text = string.Join("", poleText);
                    text = text + "X";
                    goto proces1;
                }
            }


            zaver:
            return text;
        }


        static public string KorekceTextuDesifrovani(string VstupniRetezec)
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
            
            VstupniRetezec = VstupniRetezec.Replace('W', 'V');


            return VstupniRetezec;
        }

        static public string DesifrovaniSpecialnichZnaku(string VstupniRetezec)
        {
            VstupniRetezec = VstupniRetezec.Replace("QVYKRICNIKZ", "!");
            VstupniRetezec = VstupniRetezec.Replace("QOTAZNIKZ", "?");
            VstupniRetezec = VstupniRetezec.Replace("QHVEZDICKAZ", "*");
            VstupniRetezec = VstupniRetezec.Replace("QLOMENOZ", "/");
            VstupniRetezec = VstupniRetezec.Replace("QMINUSZ", "-");
            VstupniRetezec = VstupniRetezec.Replace("QPLUSZ", "+");
            VstupniRetezec = VstupniRetezec.Replace("QROVNASEZ", "=");
            VstupniRetezec = VstupniRetezec.Replace("QCARKAZ", ",");
            VstupniRetezec = VstupniRetezec.Replace("QTECKAZ", ".");
            VstupniRetezec = VstupniRetezec.Replace("QDVOJTECKAZ", ":");
            VstupniRetezec = VstupniRetezec.Replace("QLZAVORKAZ", "(");
            VstupniRetezec = VstupniRetezec.Replace("QPZAVORKAZ", ")");
            VstupniRetezec = VstupniRetezec.Replace("QHASHTAGZ", "#");
            VstupniRetezec = VstupniRetezec.Replace("QANDZ", "&");
            VstupniRetezec = VstupniRetezec.Replace("QMEZERAZ", " ");


            return VstupniRetezec;
        }


        static public string DesifrovaniCisel(string VstupniRetezec)
        {
            VstupniRetezec = VstupniRetezec.Replace("QNULAZ", "0");
            VstupniRetezec = VstupniRetezec.Replace("QJEDNAZ", "1");
            VstupniRetezec = VstupniRetezec.Replace("QDVAZ", "2");
            VstupniRetezec = VstupniRetezec.Replace("QTRIZ", "3");
            VstupniRetezec = VstupniRetezec.Replace("QCTYRIZ", "4");
            VstupniRetezec = VstupniRetezec.Replace("QPETZ", "5");
            VstupniRetezec = VstupniRetezec.Replace("QSESTZ", "6");
            VstupniRetezec = VstupniRetezec.Replace("QSEDMZ", "7");
            VstupniRetezec = VstupniRetezec.Replace("QOSMZ", "8");
            VstupniRetezec = VstupniRetezec.Replace("QDEVETZ", "9");

            return VstupniRetezec;
        }

        static public string ZbavSePrebytecnychXaQ(string vstupniRetezec)
        {
            char[] pismena = vstupniRetezec.ToCharArray();
            string text = vstupniRetezec;


            for (int i = 0; i < vstupniRetezec.Length -2; i++)
            {
                if ((pismena[i] == pismena[i + 2]) && (pismena[i + 1] == 'X')) //i+2 mimo pole!
                {
                    text = text.Replace(Convert.ToString(pismena[i]) + Convert.ToString(pismena[i + 1]) + Convert.ToString(pismena[i + 2]), Convert.ToString(pismena[i]) + Convert.ToString(pismena[i + 2]));
                }
                else if ((pismena[i] == 'X') && (pismena[i] == pismena[i + 2]) && (pismena[i + 1] == 'Q'))
                {
                    text = text.Replace(Convert.ToString(pismena[i]) + Convert.ToString(pismena[i + 1]) + Convert.ToString(pismena[i + 2]), Convert.ToString(pismena[i]) + Convert.ToString(pismena[i + 2]));
                }
                

            }
                       
            if ((pismena[pismena.Length - 1] == 'Q') && (pismena[pismena.Length - 2] == 'X'))
            {
                text = text.Replace(Convert.ToString(pismena[pismena.Length - 2]) + Convert.ToString(pismena[pismena.Length - 1]), Convert.ToString(pismena[pismena.Length - 2]));
            }
            else if (pismena[pismena.Length - 1] == 'X')
            {
                text = text.Replace(Convert.ToString(pismena[pismena.Length - 2]) + Convert.ToString(pismena[pismena.Length - 1]), Convert.ToString(pismena[pismena.Length - 2]));
            }




            return text;
        }

        static public string VytvorDvojicky(string vstupniRetezec)
        {
            string vypis = vstupniRetezec;
            double delkaVstupu = vypis.Length;
            double delkaOpakovani = delkaVstupu + (Math.Floor(delkaVstupu / 2));

            for (int i = 2; i < delkaOpakovani; i = i + 2)
            {
                vypis = vypis.Insert(i, " ");
                i++;
            }

            return vypis;
        }

        static public string VytvorPetice(string vstupniRetezec)
        {
            double delkaVstupu = vstupniRetezec.Length;
            double delkaOpakovani = delkaVstupu + (Math.Floor(delkaVstupu / 5));

            for (int i = 5; i < delkaOpakovani; i = i + 5)
            {
                vstupniRetezec = vstupniRetezec.Insert(i, " ");
                i++;
            }

            return vstupniRetezec;
        }
    }
}
