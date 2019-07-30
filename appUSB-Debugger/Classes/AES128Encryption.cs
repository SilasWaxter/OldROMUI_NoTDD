using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;     //used for generating aes 128bit key

namespace appUSB_Debugger
{
    class AES128Encryption
    {
        public byte[] key = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public void GenerateKey()
        {
            AesManaged aes = new AesManaged();

            aes.KeySize = 128;
            aes.GenerateKey();
            aes.Key.CopyTo(key, 0);     //copy aes.key to byte[] key starting at 0 index
            
            //DEBUGGING
            System.Diagnostics.Debug.Write("AES128_Key Combo:  ");                     //write '\n'
            foreach (byte b in key) System.Diagnostics.Debug.Write(b);  //write key to debugger
            System.Diagnostics.Debug.WriteLine("");                     //write '\n'
            System.Diagnostics.Debug.WriteLine("AES128_Key Length:  " + key.Length.ToString());
        }
    }
}
