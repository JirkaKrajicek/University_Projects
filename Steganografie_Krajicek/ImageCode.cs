using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Steganografie_Krajicek
{
    static class ImageCode
    {

        public static string PathToEditedImage { get; set; }

        public static void EncodeImage(Bitmap myBitMap, string text)
        {
            int count = 0;
            int queue = 0;
            Color pixel;
            //List<int> aux = new List<int>();

            for (int y = 0; y < myBitMap.Height; y++) //radek aka vyska
            {
                for (int x = 0; x < myBitMap.Width; x++) //sloupecek aka sirka
                {

                    if (count == 8)
                    {
                        count = 0;
                        queue++;
                    }

                    if (queue >= text.Length) break;

                    int charToDec = Convert.ToInt32(Convert.ToChar(text.Substring(queue, 1)));
                    int[] oneCharArr = { charToDec };
                    BitArray charInBinary = new BitArray(oneCharArr);

                    pixel = myBitMap.GetPixel(x, y);
                    int[] blue = { pixel.B };
                    BitArray cell = new BitArray(blue);

                    if (charInBinary[count] == true)
                    {
                        cell[0] = true;
                        //aux.Add(1);
                        int[] cisloBinarni = new int[1];
                        cell.CopyTo(cisloBinarni, 0);
                        myBitMap.SetPixel(x, y, Color.FromArgb(pixel.R, pixel.G, Convert.ToInt32(cisloBinarni[0])));
                    }
                    else
                    {
                        cell[0] = false;
                        //aux.Add(0);
                        int[] cisloBinarni = new int[1];
                        cell.CopyTo(cisloBinarni, 0);
                        myBitMap.SetPixel(x, y, Color.FromArgb(pixel.R, pixel.G, Convert.ToInt32(cisloBinarni[0])));
                    }
                    count++;
                }
            }

            try //dvojte kodovani?
            {
                string nameOfFile = Form1.FileNameNonExten;
                string pathToEditedImageFile = AppDomain.CurrentDomain.BaseDirectory + $"{nameOfFile}_edited.bmp";
                myBitMap.Save(pathToEditedImageFile);
                PathToEditedImage = pathToEditedImageFile;
            }
            catch (Exception)
            {
                MessageBox.Show("Pro opakované kódování do stejného vstupního obrázku zmáčkněte prosím tlačítko pro RESTART", "Chyba funkčnosti", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public static string DecodeImage(string pathToEditedImageFile)
        {
           
            Bitmap myBitMapDekode = new Bitmap(pathToEditedImageFile);
            string bigBro = "";
            Color pixelDecode;
            List<int> binary = new List<int>();
            int[] binArray = new int[8];

            for (int j = 0; j < myBitMapDekode.Height; j++)
            {
                for (int i = 0; i < myBitMapDekode.Width; i++)
                {
                    pixelDecode = myBitMapDekode.GetPixel(i, j);
                    int[] blueDekode = { pixelDecode.B };
                    BitArray cell = new BitArray(blueDekode);


                    if (cell[0] == true)
                    {
                        binary.Add(1);
                    }
                    else
                    {
                        binary.Add(0);
                    }

                    if (binary.Count == 8)
                    {
                        binArray = binary.ToArray();
                        int[] arr = binArray.Reverse().ToArray();
                        string toConvert = string.Join("", arr);
                        int intChar = Convert.ToInt32(toConvert, 2);
                        string pismeno = Convert.ToString(Convert.ToChar(intChar));
                        if (pismeno == "0")
                        {
                            return bigBro;
                        }
                        else
                        {
                            bigBro = bigBro + pismeno;
                        }
                        

                        binary.Clear();

                    }
                }                
            }
            return bigBro;
        }


        public static bool EnsureImageIsEdited(string fileName)
        {
            fileName = fileName.ToUpper();
            string redCar;
            bool outcome = false;

            for (int i = 0; i < fileName.Length-5; i++)
            {
                redCar = fileName.Substring(i, 6);

                if (redCar == "EDITED")
                {
                    outcome = true;
                    break;
                }
            }

            return outcome;
        }
    }
}
