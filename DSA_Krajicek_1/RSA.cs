using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Krajicek_1
{
    class RSA
    {
        private static BigInteger[] ArrayCleanNums;
        public static List<BigInteger[]> ListEuclid = new List<BigInteger[]>();
        private static int bytesForRNG = 128;
        private static int Sectors;

        public static BigInteger N { get; set; }
        public static BigInteger E { get; set; }
        public static BigInteger D { get; set; }



        public static void GetAllKeys()
        {
            BigInteger p, q;

            do
            {
                p = RSA.GeneratePrimeNumber();
                q = RSA.GeneratePrimeNumber();
            }
            while (p == q);

            BigInteger n = p * q;
            BigInteger fiN = (p - 1) * (q - 1);

            bool bull = false;
            BigInteger e = 0;
            BigInteger d = 0;            

            while (bull == false)
            {
                e = GenerateBigBoy();
                if (e < fiN && e > 1)
                {
                    ListEuclid.Clear();
                    bool opinion = euclidsAlgorithm(fiN, e);                    

                    if (opinion == true)
                    {
                        d = ExtendEuclidAlgor();

                        BigInteger Ed = e * d;
                        BigInteger PlzBeOne = Ed % fiN;
                        if (PlzBeOne == 1) bull = true;
                        else bull = false;
                    }
                }
            }
            N = n;
            E = e;
            D = d;

            Sunflower.File_PRI(n, d);
            Sunflower.File_PUB(n, e);         
        }

        private static BigInteger GenerateBigBoy()
        {
            BigInteger e = 0;
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] random = new byte[bytesForRNG - 1];
                rg.GetBytes(random);
                e = new BigInteger(random);
                e = BigInteger.Abs(e);
            }
            return e;
        }



        private static bool euclidsAlgorithm(BigInteger b, BigInteger a)
        {
            if (a == 0) return false;
            BigInteger r = 0;
            BigInteger q = 0;

            if (b < a)
            {
                BigInteger tmp = a;
                a = b;
                b = tmp;
            }
            r = b % a;
            q = b / a;
            BigInteger j = 1;
            BigInteger[] array = { b, q, a, j, r };

            ListEuclid.Add(array);
            if (r == 0 && a == 1)
            {
                return true;
            }
            return euclidsAlgorithm(a, r);
        }



        private static BigInteger ExtendEuclidAlgor()
        {
            List<BigInteger[]> listEA = transformListArray();

            BigInteger[] autoPilot = new BigInteger[5];
            BigInteger[] blackBox = new BigInteger[5];
            int count = 0;
            BigInteger a = 0;
            BigInteger b = 0;
            autoPilot[0] = 1;

            for (int i = listEA.Count - 1; i >= 1; i--)
            {
                if (i == listEA.Count - 1 && count == 0)
                {
                    BigInteger[] current = listEA[i];
                    BigInteger[] next = listEA[i - 1];

                    a = (current[3] * next[3]) + current[1];
                    autoPilot[1] = a;
                    autoPilot[2] = next[4];
                    autoPilot[3] = current[3];
                    autoPilot[4] = next[2];
                }
                else if (count % 2 == 1)
                {
                    BigInteger[] current = listEA[i - 1];
                    blackBox = autoPilot;

                    a = blackBox[1] * current[1];
                    b = (blackBox[1] * current[3]) + blackBox[3];

                    autoPilot[1] = a;
                    autoPilot[2] = current[2];
                    autoPilot[3] = b;
                    autoPilot[4] = blackBox[4];
                }
                else
                {
                    BigInteger[] current = listEA[i - 1];
                    blackBox = autoPilot;

                    a = blackBox[1] + (blackBox[3] * current[3]);
                    b = blackBox[3] * current[1];

                    autoPilot[1] = a;
                    autoPilot[2] = blackBox[2];
                    autoPilot[3] = b;
                    autoPilot[4] = current[2];

                }
                count++;
            }
            return autoPilot[1];
        }




        private static List<BigInteger[]> transformListArray()
        {
            int indexRemoval = ListEuclid.Count - 1;
            ListEuclid.RemoveAt(indexRemoval);

            List<BigInteger[]> transList = new List<BigInteger[]>();
            foreach (BigInteger[] arr in ListEuclid)
            {
                BigInteger[] aux = new BigInteger[5];
                aux[0] = arr[4];
                aux[1] = arr[3];
                aux[2] = arr[0];
                aux[3] = -arr[1];
                aux[4] = arr[2];

                transList.Add(aux);
            }
            return transList;
        }



        private static BigInteger GeneratePrimeNumber()
        {
            bool red = false;

            while (red == false)
            {

                using (RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider())
                {
                    byte[] random = new byte[bytesForRNG / 2];
                    rnd.GetBytes(random);
                    BigInteger hitOrMiss = new BigInteger(random);
                    hitOrMiss = BigInteger.Abs(hitOrMiss);

                    red = PrimeChecker(hitOrMiss);

                    if (red == true)
                    {
                        return hitOrMiss;
                    }

                }
            }

            return 0;
        }


        private static bool PrimeChecker(BigInteger source) //Miller–Rabin primality test
        {
            int certainty = 16;
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d = d / 2;
                s = s + 1;
            }

            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {

                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }


        private static void LetsBuildAWall(string msg)
        {
            int blockLength = 5;

            if (msg.Length > blockLength)
            {
                double castleOfGlass = Math.Ceiling((double)msg.Length / (double)blockLength);
                Sectors = (int)castleOfGlass;
                string[] msgToNums = new string[Sectors];

                string subM = "";

                for (int i = 0; i < castleOfGlass; i++)
                {                   
                    if (i < castleOfGlass - 1)
                    {
                        subM = msg.Substring(i * blockLength, blockLength);
                        msgToNums[i] = subM;
                    }
                    else
                    {
                        int tail = msg.Length - (i * blockLength);
                        subM = msg.Substring(i * blockLength, tail);
                        while (subM.Length != blockLength)
                        {
                            subM = subM + " ";
                        }
                        msgToNums[i] = subM;
                    }
                }
                AbrakaDabraMsg(msgToNums);

            }
            else
            {
                Sectors = 1;
                string[] msgToNums = new string[Sectors];

                string subM2 = msg;
                
                while (subM2.Length != blockLength)
                {
                    subM2 = subM2 + " ";
                }
                msgToNums[0] = subM2;
                AbrakaDabraMsg(msgToNums);
            }
        }
        //private static BigInteger StrgToBigInt(string input)
        //{
        //    BigInteger m = BigInteger.Parse(input);
        //    return m;
        //}


        private static void AbrakaDabraMsg(string[] blocks)
        {
            ArrayCleanNums = new BigInteger[Sectors];
            for (int j = 0; j < blocks.Length; j++)
            {
                byte[] msg = new byte[blocks[j].Length];

                for (int i = 0; i < blocks[j].Length; i++)
                {
                    msg[i] = Convert.ToByte(Convert.ToChar(blocks[j].Substring(i, 1)));
                    if (i == blocks[i].Length - 1)
                    {
                        BigInteger m = new BigInteger(msg);
                        ArrayCleanNums[j] = m;
                    }
                }
            }
        }

        public static string Encrypt(string msg, BigInteger n, BigInteger d)
        {
            LetsBuildAWall(msg);
            string op = "";
            //ArrayEncrypted = new BigInteger[Sectors];

            for (int i = 0; i < ArrayCleanNums.Length; i++)
            {
                BigInteger c = BigInteger.ModPow(ArrayCleanNums[i], d, n);
                if (i == 0)
                {
                    op = c.ToString();
                }
                else
                { 
                    op = op +" "+ c.ToString();
                }
            }
            return op;
        }

        public static string Decrypt(string text, BigInteger n, BigInteger e)
        {

            BigInteger[] arrayToDecrypt = CutItAlready(text);
            BigInteger[] arrayDecrypted = new BigInteger[arrayToDecrypt.Length];
            for (int i = 0; i < arrayToDecrypt.Length; i++)
            {
                BigInteger mDecrypted = BigInteger.ModPow(arrayToDecrypt[i], e, n);
                arrayDecrypted[i] = mDecrypted;
            }

            return GimmeItAll(arrayDecrypted);
        }

        private static string GimmeItAll(BigInteger[] arrayToTranslate)
        {
            string text = String.Empty;

            for (int i = 0; i < arrayToTranslate.Length; i++)
            {
                byte[] bytes = arrayToTranslate[i].ToByteArray();
                for (int j = 0; j < bytes.Length; j++)
                {
                    string toAdd =Convert.ToString(Convert.ToChar(bytes[j]));
                    if (toAdd != " ")
                    {
                        text = text + toAdd;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return text;
        }

        //public static bool TestIfNumsOnly(string txt)
        //{
        //    foreach (char znak in txt)
        //    {
        //        int number;
        //        bool sccs = Int32.TryParse(znak.ToString(), out number);
        //        if (!sccs)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        public static BigInteger[] SeparateKeys(string keysFromFile)
        {            
            BigInteger a, b;
            int border = 0;
            char[] chain = keysFromFile.ToCharArray();

            for (int i = 0; i < chain.Length; i++)
            {
                if (chain[i] == ' ')
                {
                    border = i;
                    break;
                }
            }

            a = BigInteger.Parse(keysFromFile.Substring(0,border));
            int tail = (keysFromFile.Length-1) - (border);
            b = BigInteger.Parse(keysFromFile.Substring(border+1,tail));

            BigInteger[] array = { a, b };
            return array;
        }

        private static BigInteger[] CutItAlready(string text)
        {
            List<string> almostThere = new List<string>();
            string subT;
            int lastIndex = 0;
            char[] chTxt = text.ToCharArray();
            for (int i = 0; i < text.Length; i++)
            {
                if (chTxt[i] == ' ')
                {
                    int tail = i - lastIndex;
                    subT = text.Substring(lastIndex, tail);
                    almostThere.Add(subT);
                    lastIndex = i + 1;
                }
                else if (i == text.Length - 1)
                {
                    int tail = i - lastIndex + 1;
                    subT = text.Substring(lastIndex, tail);
                    almostThere.Add(subT);
                }
            }
            BigInteger[] bigArray = new BigInteger[almostThere.Count];
            for (int i = 0; i < bigArray.Length; i++)
            {
                BigInteger annonymous = BigInteger.Parse(almostThere[i]);
                bigArray[i] = annonymous;
            }

            return bigArray;
        }
    }
}

