using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Encryption
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

       /* protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (optionsRadios1.Checked == true)
            {
                enResults.InnerHtml = "You choose: Sym";
                    
            }
            else
            {
                enResults.InnerHtml = "You choose: Asym";
            }
        }*/

        protected void Submit_Click(object sender, EventArgs e)
        {
            String encrypt = inputEncrypt.Value.ToString();

            if (select.Value == "SEnAES")
            {
                try
                {

                    string original = inputEncrypt.Value.ToString();

                    // Create a new instance of the AesCryptoServiceProvider
                    // class.  This generates a new key and initialization 
                    // vector (IV).
                    using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
                    {

                        // Encrypt the string to an array of bytes.
                        byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                        // Decrypt the bytes to a string.
                        string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                        string enrp = "";
                        for(int i = 0; i < encrypted.Length; i++)
                        {
                            enrp += encrypted[i];
                        }
                        //Display the original data and the decrypted data.
                        String postIt = "Original: " + original + "<br/> Encrypted: " + enrp + "<br/>Round Trip: " + roundtrip;
                        enResults.InnerHtml = postIt;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }


            }

            if (select.Value == "SEnDES")
            {
                try
                {

                    string original = inputEncrypt.Value.ToString();

                    // Create a new instance of the AesCryptoServiceProvider
                    // class.  This generates a new key and initialization 
                    // vector (IV).
                    using (DESCryptoServiceProvider myDES = new DESCryptoServiceProvider())
                    {

                        // Encrypt the string to an array of bytes.
                        byte[] encrypted = EncryptStringToBytes_Des(original, myDES.Key, myDES.IV);

                        // Decrypt the bytes to a string.
                        string roundtrip = DecryptStringFromBytes_Des(encrypted, myDES.Key, myDES.IV);

                        string enrp = "";
                        for (int i = 0; i < encrypted.Length; i++)
                        {
                            enrp += encrypted[i];
                        }
                        //Display the original data and the decrypted data.
                        String postIt = "Original: " + original + "<br/> Encrypted: " + enrp + "<br/>Round Trip: " + roundtrip;
                        enResults.InnerHtml = postIt;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }

            if(select.Value == "SEnRC2")
            {

                // Create a new instance of the RC2CryptoServiceProvider class
                // and automatically generate a Key and IV.
                RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

                String postIt = "Effective key size is {0} bits." + rc2CSP.EffectiveKeySize + "<br/>";

                // Get the key and IV.
                byte[] key = rc2CSP.Key;
                byte[] IV = rc2CSP.IV;

                // Get an encryptor.
                ICryptoTransform encryptor = rc2CSP.CreateEncryptor(key, IV);

                // Encrypt the data as an array of encrypted bytes in memory.
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                // Convert the data to a byte array.
                string original = inputEncrypt.Value.ToString();
                byte[] toEncrypt = Encoding.ASCII.GetBytes(original);

                // Write all data to the crypto stream and flush it.
                csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                csEncrypt.FlushFinalBlock();

                // Get the encrypted array of bytes.
                byte[] encrypted = msEncrypt.ToArray();

                string enrp = "";
                for (int i = 0; i < encrypted.Length; i++)
                {
                    enrp += encrypted[i];
                }

                postIt += "Encryption:" + enrp + "<br/>";

                //Get a decryptor that uses the same key and IV as the encryptor.
                ICryptoTransform decryptor = rc2CSP.CreateDecryptor(key, IV);

                // Now decrypt the previously encrypted message using the decryptor
                // obtained in the above step.
                MemoryStream msDecrypt = new MemoryStream(encrypted);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                // Read the decrypted bytes from the decrypting stream
                // and place them in a StringBuilder class.

                StringBuilder roundtrip = new StringBuilder();

                int b = 0;

                do
                {
                    b = csDecrypt.ReadByte();

                    if (b != -1)
                    {
                        roundtrip.Append((char)b);
                    }

                } while (b != -1);


                // Display the original data and the decrypted data.
                postIt += "Original:" + original + "<br/>";
                postIt += "Round Trip:" + roundtrip + "<br/>";

                enResults.InnerHtml = postIt;
        }

            if(select.Value == "SEnRijndael")
            {

                try
                {

                    string original = inputEncrypt.Value.ToString();

                    // Create a new instance of the Rijndael
                    // class.  This generates a new key and initialization 
                    // vector (IV).
                    using (Rijndael myRijndael = Rijndael.Create())
                    {
                        // Encrypt the string to an array of bytes.
                        byte[] encrypted = EncryptStringToBytes_Rijndael(original, myRijndael.Key, myRijndael.IV);

                        string enrp = "";
                        for (int i = 0; i < encrypted.Length; i++)
                        {
                            enrp += encrypted[i];
                        }

                        // Decrypt the bytes to a string.
                        string roundtrip = DecryptStringFromBytes_Rijndael(encrypted, myRijndael.Key, myRijndael.IV);

                        //Display the original data and the decrypted data.
                        String postIt = "Original:   " + original + "<br/> Encrypted: " + enrp + "<br/>Round Trip: " + roundtrip;
                        enResults.InnerHtml = postIt;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }

            if(select.Value == "SEnTripleDES")
            {
                // Create a new instance of the RC2CryptoServiceProvider class
                // and automatically generate a Key and IV.
                TripleDESCryptoServiceProvider tdesCSP = new TripleDESCryptoServiceProvider();

                // Get the key and IV.
                byte[] key = tdesCSP.Key;
                byte[] IV = tdesCSP.IV;

                // Get an encryptor.
                ICryptoTransform encryptor = tdesCSP.CreateEncryptor(key, IV);

                // Encrypt the data as an array of encrypted bytes in memory.
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                // Convert the data to a byte array.
                string original = inputEncrypt.Value.ToString();
                byte[] toEncrypt = Encoding.ASCII.GetBytes(original);

                String postIt = "";

                // Write all data to the crypto stream and flush it.
                csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                csEncrypt.FlushFinalBlock();

                // Get the encrypted array of bytes.
                byte[] encrypted = msEncrypt.ToArray();

                string enrp = "";
                for (int i = 0; i < encrypted.Length; i++)
                {
                    enrp += encrypted[i];
                }                

                //Get a decryptor that uses the same key and IV as the encryptor.
                ICryptoTransform decryptor = tdesCSP.CreateDecryptor(key, IV);

                // Now decrypt the previously encrypted message using the decryptor
                // obtained in the above step.
                MemoryStream msDecrypt = new MemoryStream(encrypted);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                // Read the decrypted bytes from the decrypting stream
                // and place them in a StringBuilder class.

                StringBuilder roundtrip = new StringBuilder();

                int b = 0;

                do
                {
                    b = csDecrypt.ReadByte();

                    if (b != -1)
                    {
                        roundtrip.Append((char)b);
                    }

                } while (b != -1);


                // Display the original data and the decrypted data.
                postIt += "Original:" + original + "<br/>";
                postIt += "Encrypted:" + enrp + "<br/>";
                postIt += "Round Trip:" + roundtrip + "<br/>";

                enResults.InnerHtml = postIt;


            }
        
            if(select.Value == "HMD5")
            {
                string source = inputEncrypt.Value.ToString();

                    string hash = getMd5Hash(source);

                    String postIt = "The MD5 hash of " + source + " is: " + hash + ".";
                

                enResults.InnerHtml = postIt;

                }

            if(select.Value == "HSHA1")
            {

                string original = inputEncrypt.Value.ToString();
                byte[] data = Encoding.ASCII.GetBytes(original);

                byte[] result;

                SHA1 sha = new SHA1CryptoServiceProvider();
                // This is one implementation of the abstract class SHA1.

                result = sha.ComputeHash(data);

                String postIt = "The SHA1 hash of " + original + " is ";

                for(int i = 0; i < result.Length; i++)
                {
                    postIt += result[i];

                }

                enResults.InnerHtml = postIt;

            }

            if(select.Value == "HRIPEMD160")
            {
                string original = inputEncrypt.Value.ToString();
                byte[] data = Encoding.ASCII.GetBytes(original);

                byte[] result;

                RIPEMD160Managed rip = new RIPEMD160Managed();
                // This is one implementation of the abstract class SHA1.

                result = rip.ComputeHash(data);

                String postIt = "The RIPEMD160 hash of " + original + " is ";

                for (int i = 0; i < result.Length; i++)
                {
                    postIt += result[i];

                }

                enResults.InnerHtml = postIt;
            }


            }


        protected void Cancel_Click(object sender, EventArgs e)
        {
            inputEncrypt.Value = "";

        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new System.IO.MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        static byte[] EncryptStringToBytes_Des(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an DesCryptoServiceProvider object
            // with the specified key and IV.
            using (DESCryptoServiceProvider DESAlg = new DESCryptoServiceProvider())
            {
                DESAlg.Key = Key;
                DESAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = DESAlg.CreateEncryptor(DESAlg.Key, DESAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new System.IO.MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static string DecryptStringFromBytes_Des(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (DESCryptoServiceProvider DESAlg = new DESCryptoServiceProvider())
            {
                DESAlg.Key = Key;
                DESAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = DESAlg.CreateDecryptor(DESAlg.Key, DESAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    static byte[] EncryptStringToBytes_Rijndael(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;
        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }


        // Return the encrypted bytes from the memory stream.
        return encrypted;

    }

    static string DecryptStringFromBytes_Rijndael(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        }

        return plaintext;

    }

        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}