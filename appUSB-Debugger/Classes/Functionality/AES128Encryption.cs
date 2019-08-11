using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Security.Cryptography;     //used for generating aes 128bit key

namespace appUSB_Debugger
{
    public static class AES128Encryption
    {
        public static byte[] key = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static void GetOrGenerateKey()
        {
            #region networkDetails.txt Format:
            //      1st Line:   Aes128Key[key]
            //                  Sensor1;macAdd[1:2:3:4:5:6]
            //                  Sensor2;macAdd[1:2:3:4:5:6]
            //                  Hub;macAdd[1:2:3:4:5:6]
            #endregion
            string filePath = @"C:\Users\silas\Desktop\appUSB\appUSB-Debugger\networkDetails.txt";
            List<string> linesFromNetDetails = File.ReadAllLines(filePath).ToList();

            foreach(string line in linesFromNetDetails)
            {
                if (line.Contains("Aes128Key"))
                {
                    string keyFromFile = line.Substring(line.IndexOf('[') + 1);
                    keyFromFile = keyFromFile.Trim(']');
                    string[] keyFromFileSplit = keyFromFile.Split(':');                                 //split the key into its bytes.
                    for (int i = 0; i < 16; i++)                                                        //Converts string[] keyFromFileSplit (HEX/base16) to byte[]
                        key[i] = Convert.ToByte(keyFromFileSplit[i], 16);

                    System.Diagnostics.Debug.WriteLine($"Found key in File: {keyFromFile}");
                    //System.Diagnostics.Debug.Write(BitConverter.ToString(key).Replace('-', ':'));

                    return;
                }
            }
            GenerateKey();
        }

        private static void GenerateKey()
        {
            string filePath = @"C:\Users\silas\Desktop\appUSB\appUSB-Debugger\networkDetails.txt";
            List<string> linesFromNetDetails = File.ReadAllLines(filePath).ToList();

            AesManaged aes = new AesManaged();

            aes.KeySize = 128;
            aes.GenerateKey();
            aes.Key.CopyTo(key, 0);     //copy aes.key to byte[] key starting at 0 index


            string keyStr = BitConverter.ToString(aes.Key).Replace('-', ':');
            string keyFile = $"Aes128Key[{keyStr}]";
            linesFromNetDetails.Insert(0, keyFile);
            File.WriteAllLines(filePath, linesFromNetDetails);

            //DEBUGGING
            System.Diagnostics.Debug.Write("AES128_Key Combo:  ");                     //write '\n'
            foreach (byte b in key) System.Diagnostics.Debug.Write(b);  //write key to debugger
            System.Diagnostics.Debug.WriteLine("");                     //write '\n'
        }
    }
}
