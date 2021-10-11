using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfairova_sifra_Krajicek
{
    static class Matrix
    {
        static public int IndexZnakuRadek(char znak, char[,] matrix)
        {
            int indexRadku = -1;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == znak)
                    {
                        indexRadku = i;
                    }
                }
            }
            return indexRadku;
        }

        static public int IndexZnakuSloupec(char znak, char[,] matrix)
        {
            int indexSloupce = -1;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == znak)
                    {
                        indexSloupce = j;
                    }
                }
            }
            return indexSloupce;
        }
      
        static public char[] ZasifrujRetezec(char[,] matrix, char[] poleVstup)
        {
            char[] vystupniZnaky = new char[poleVstup.Length];
                       

            for (int i = 0; i < poleVstup.Length; i = i + 2)
            {
                int radek1 = Matrix.IndexZnakuRadek(poleVstup[i], matrix);
                int sloupec1 = Matrix.IndexZnakuSloupec(poleVstup[i], matrix);

                int radek2 = Matrix.IndexZnakuRadek(poleVstup[i + 1], matrix);
                int sloupec2 = Matrix.IndexZnakuSloupec(poleVstup[i + 1], matrix);

                if (radek1 == radek2)
                {
                    vystupniZnaky[i] = matrix[radek1, (sloupec1 + 1) % matrix.GetLength(1)];
                    vystupniZnaky[i + 1] = matrix[radek2, (sloupec2 + 1) % matrix.GetLength(1)];
                }
                else if (sloupec1 == sloupec2)
                {
                    vystupniZnaky[i] = matrix[(radek1 + 1) % matrix.GetLength(0), sloupec1];
                    vystupniZnaky[i + 1] = matrix[(radek2 + 1) % matrix.GetLength(0), sloupec2];
                }
                else
                {
                    vystupniZnaky[i] = matrix[radek1, sloupec2];
                    vystupniZnaky[i + 1] = matrix[radek2, sloupec1];
                }
            }
            return vystupniZnaky;
        }

        static public char[] RozsifrujRetezec(char[,] matrix, char[] poleVstup)
        {
            char[] vystupniZnaky = new char[poleVstup.Length];

            for (int i = 0; i < poleVstup.Length; i = i + 2)
            {
                int radek1 = Matrix.IndexZnakuRadek(poleVstup[i], matrix);
                int sloupec1 = Matrix.IndexZnakuSloupec(poleVstup[i], matrix);

                int radek2 = Matrix.IndexZnakuRadek(poleVstup[i + 1], matrix);
                int sloupec2 = Matrix.IndexZnakuSloupec(poleVstup[i + 1], matrix);

                if (radek1 == radek2)
                {
                    double mod = matrix.GetLength(1);

                    double s1 = sloupec1 - 1;                    
                    double modulo_sloupec1 = s1 - Math.Floor(s1/mod)*mod;

                    double s2 = sloupec2 - 1;
                    double modulo_sloupec2 = s2 - Math.Floor(s2 / mod) * mod;

                    vystupniZnaky[i] = matrix[radek1, Convert.ToInt32(modulo_sloupec1)];
                    vystupniZnaky[i + 1] = matrix[radek2, Convert.ToInt32(modulo_sloupec2)];
                }
                else if (sloupec1 == sloupec2)
                {
                    double mod = matrix.GetLength(0);

                    double r1 = radek1 - 1;
                    double modulo_radek1 = r1 - Math.Floor(r1 / mod) * mod;

                    double r2 = radek2 - 1;
                    double modulo_radek2 = r2 - Math.Floor(r2 / mod) * mod;

                    vystupniZnaky[i] = matrix[Convert.ToInt32(modulo_radek1), sloupec1];
                    vystupniZnaky[i + 1] = matrix[Convert.ToInt32(modulo_radek2), sloupec2];
                }
                else
                {
                    vystupniZnaky[i] = matrix[radek1, sloupec2];
                    vystupniZnaky[i + 1] = matrix[radek2, sloupec1];
                }
            }
            return vystupniZnaky;
        }
    }
}
