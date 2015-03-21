using System;
using System.Collections.Generic;
using System.Text;

namespace Wbs.Protocol
{
    public class RSA
    {
        private const int PublicKey = 50969;   // you cannot change this value
        private const int PrivateKey = 27689;  // you cannot change this value
        private const int Modulus = 37469;     // you cannot change this value

        private static int Crypt(int pLngMessage, int pLngKey)
        {
            int lLngMod;
            int lLngResult;
            int tempKey;

            if (pLngKey % 2 == 0)
            {
                tempKey = pLngKey / 2;
                lLngResult = 1;
            }
            else
            {
                tempKey = pLngKey / 2 + 1;  //round
                lLngResult = pLngMessage;
            }

            for (int i = 1; i < tempKey; i++)
            {
                lLngMod = (pLngMessage * pLngMessage) % Modulus;
                lLngResult = (lLngMod * lLngResult) % Modulus;
            }
            return lLngResult;
        }

        /// <summary>
        /// Encode processing.
        /// </summary>
        /// <param name="bstrMessage">The plaintext to encrypt.</param>
        /// <returns>Return the encrypted ciphertext.</returns>
        public static String Encode(String bstrMessage)
        {
            int BytASCII;
            int tempEncrypted;
            Char tempMessage;
            String strEncode = "";

            int Messagelen = bstrMessage.Length;
            if (Messagelen > 0)
            {
                for (int i = 0; i < Messagelen; i++)
                {
                    tempMessage = Char.Parse(bstrMessage.Substring(i, 1));
                    BytASCII = (int)tempMessage;
                    tempEncrypted = Crypt(BytASCII, PublicKey);
                    String MyHex = tempEncrypted.ToString("X4");// convert the crypt's result to 4 length hex string, ex: 100.ToString("X4") = 0064
                    strEncode += MyHex;
                }
            }
            return strEncode;
        }


        /// <summary>
        /// Decode processing.
        /// </summary>
        /// <param name="bstrMessage">The encrypted ciphertext.</param>
        /// <returns>Return the plaintext.</returns>
        public static String Decode(String bstrMessage)
        {
            String tempMessage;
            String strHex;
            int tempNumber;
            int BytASCII;
            String MyCode;
            String strDecode = "";

            int Messagelen = bstrMessage.Length;
            for (int i = 0; i < Messagelen; i += 4)
            {
                tempMessage = bstrMessage.Substring(i, 4);      //
                strHex = "0x" + tempMessage;                    // Hex values in 4 length string.
                tempNumber = Convert.ToInt32(strHex, 16);       // Convert the hex value to int32 value.
                BytASCII = Crypt(tempNumber, PrivateKey);       // 
                MyCode = Convert.ToString((Char)BytASCII);      // Convert the crypted value to string value.
                strDecode += MyCode;
            }
            return strDecode;
        }
    }
}
