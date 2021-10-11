using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Krajicek_1
{
    class Sunflower
    {
        public static string pathForMsg = AppDomain.CurrentDomain.BaseDirectory + "message.msg";
        private static string pathForPRI = AppDomain.CurrentDomain.BaseDirectory + "nothingImportant.pri";
        private static string pathForPUB = AppDomain.CurrentDomain.BaseDirectory + "defNotKey.pub";
        private static string pathForDirToZip = AppDomain.CurrentDomain.BaseDirectory + "dirToZip";
        private static string pathForZip = AppDomain.CurrentDomain.BaseDirectory + "zipFile.zip";
        public static string pathForDirToUnzip = AppDomain.CurrentDomain.BaseDirectory + "UnzippedDir";
        private static string pathForDirToSaveKeys = AppDomain.CurrentDomain.BaseDirectory + "DirSavedKeys";
        /**********************************************************************************************************/




        public static void File_PRI(BigInteger n, BigInteger d)
        {
            string output = n.ToString() + " " + d.ToString();
            File.WriteAllText(pathForPRI, output);
        }

        public static void File_PUB(BigInteger n, BigInteger e)
        {
            string output = n.ToString() + " " + e.ToString();
            File.WriteAllText(pathForPUB, output);
        }

        public static void File_Open_MSG(string message)
        {
            File.WriteAllText(pathForMsg, message);
        }

        //public static void File_SIGN(string hash)
        //{
        //    File.WriteAllText(pathForSIGN, hash);
        //}

        public static void ZipIt(string message, string encryptedHash)
        {
            Directory.CreateDirectory(pathForDirToZip);
            string filepathForMsg = pathForDirToZip + "\\" + "message.msg";
            string filepathForHash = pathForDirToZip + "\\" + "justSomeBoringWork.sign";

            File.WriteAllText(filepathForMsg, message);
            File.WriteAllText(filepathForHash, encryptedHash);


            if (File.Exists(pathForZip))
            {
                File.Delete(pathForZip);
            }

            ZipFile.CreateFromDirectory(pathForDirToZip, pathForZip);

            if (File.Exists(pathForMsg))
            {
                File.Delete(pathForMsg);
            }

            if (Directory.Exists(pathForDirToZip))
            {
                Directory.Delete(pathForDirToZip,true);
            }
        }

        public static void UnZipIt(string pathToUnzip)
        {
            //string fileName = Path.GetFileName(pathToUnzip);

            if (Directory.Exists(pathForDirToUnzip))
            {                
                Directory.Delete(pathForDirToUnzip, true);
            }

            ZipFile.ExtractToDirectory(pathToUnzip, pathForDirToUnzip);

            if (File.Exists(pathToUnzip))
            {
                File.Delete(pathToUnzip);
            }
        }


        public static string getSHA256fromFile(string path)
        {
            //string path = pathForMsg;
            string sha256file = "";
            FileStream fs = File.OpenRead(path);
            SHA256Managed sha256mngd = new SHA256Managed();
            byte[] sha256Bytes = sha256mngd.ComputeHash(fs);
            sha256file = BitConverter.ToString(sha256Bytes).Replace("-", String.Empty).ToLower();

            fs.Close();
            sha256mngd.Clear();

            return sha256file;
        }

        public static void SaveKeys()
        {
            if (!Directory.Exists(pathForDirToSaveKeys))
            {
                Directory.CreateDirectory(pathForDirToSaveKeys);
            }

            if (File.Exists(pathForDirToSaveKeys + "\\" + "SavedPubKey.pub"))
            {
                File.Delete(pathForDirToSaveKeys + "\\" + "SavedPubKey.pub");
            }
            if (File.Exists(pathForDirToSaveKeys + "\\" + "SavedPriKey.pri"))
            {
                File.Delete(pathForDirToSaveKeys + "\\" + "SavedPriKey.pri");
            }

            string pathForPUBkeySave = pathForDirToSaveKeys + "\\" + "SavedPubKey.pub";
            string pathForPRIkeySave = pathForDirToSaveKeys + "\\" + "SavedPriKey.pri";

            string readerPub = File.ReadAllText(pathForPUB);
            string readerPri = File.ReadAllText(pathForPRI);

            File.WriteAllText(pathForPUBkeySave, readerPub);
            File.WriteAllText(pathForPRIkeySave, readerPri);

            //ZipFile.CreateFromDirectory(pathForDirToSaveKeys, pathForSavedKeys);

            //if (Directory.Exists(pathForDirToSaveKeys))
            //{
            //    Directory.Delete(pathForDirToSaveKeys, true);
            //}
        }

        public static void CleanUp()
        {
            if (File.Exists(pathForMsg))
            {
                File.Delete(pathForMsg);
            }

            if (File.Exists(pathForPUB))
            {
                File.Delete(pathForPUB);
            }

            if (File.Exists(pathForPRI))
            {
                File.Delete(pathForPRI);
            }

            if (File.Exists(pathForZip))
            {
                File.Delete(pathForZip);
            }

            if (Directory.Exists(pathForDirToZip))
            {
                Directory.Delete(pathForDirToZip, true);
            }

            if (Directory.Exists(pathForDirToUnzip))
            {
                Directory.Delete(pathForDirToUnzip, true);
            }
        }
    }
}
