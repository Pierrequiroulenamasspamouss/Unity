using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Kampai.Util; // Pour ILogger

public class EncryptionService : IEncryptionService
{
    private int Iterations = 2;
    private int KeySize = 256;
    private string Salt = "s499bgcalptrefxe";
    private string Vector = "087gbfgx3278kmnu";

    // IMPORTANT : On garde l'injection du Logger pour éviter l'erreur de compilation
    [Inject]
    public global::Kampai.Util.ILogger logger { get; set; }

    public string Encrypt(string plainText, string password)
    {
        // =================================================================
        // FIX ANTI-CRASH : Si le texte est null, on renvoie une chaine vide
        // au lieu de laisser GetBytes planter.
        // =================================================================
        if (string.IsNullOrEmpty(plainText))
        {
            return string.Empty;
        }
        if (string.IsNullOrEmpty(password))
        {
            password = "mock_default_pass";
        }

        try
        {
            byte[] bytes = global::System.Text.Encoding.ASCII.GetBytes(Vector);
            byte[] bytes2 = global::System.Text.Encoding.ASCII.GetBytes(Salt);
            byte[] bytes3 = global::System.Text.Encoding.UTF8.GetBytes(plainText);
            byte[] inArray;

            using (global::System.Security.Cryptography.SymmetricAlgorithm symmetricAlgorithm = new global::System.Security.Cryptography.AesManaged())
            {
                global::System.Security.Cryptography.Rfc2898DeriveBytes rfc2898DeriveBytes = new global::System.Security.Cryptography.Rfc2898DeriveBytes(password, bytes2, Iterations);
                byte[] bytes4 = rfc2898DeriveBytes.GetBytes(KeySize / 8);
                symmetricAlgorithm.Mode = global::System.Security.Cryptography.CipherMode.CBC;

                using (global::System.Security.Cryptography.ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor(bytes4, bytes))
                {
                    using (global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream())
                    {
                        using (global::System.Security.Cryptography.CryptoStream cryptoStream = new global::System.Security.Cryptography.CryptoStream(memoryStream, transform, global::System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytes3, 0, bytes3.Length);
                            cryptoStream.FlushFinalBlock();
                            inArray = memoryStream.ToArray();
                        }
                    }
                }
                symmetricAlgorithm.Clear();
            }
            return global::System.Convert.ToBase64String(inArray);
        }
        catch (Exception ex)
        {
            // Si le chiffrement échoue quand même, on log et on retourne le texte clair (ou vide) pour ne pas bloquer
            if (logger != null) logger.Error("Encryption failed: " + ex.Message);
            return string.Empty;
        }
    }

    public bool TryDecrypt(string cipherText, string password, out string plainText)
    {
        // FIX : Protection contre le null en entrée
        if (string.IsNullOrEmpty(cipherText))
        {
            plainText = string.Empty;
            return false;
        }
        if (string.IsNullOrEmpty(password))
        {
            password = "mock_default_pass";
        }

        try
        {
            byte[] bytes = global::System.Text.Encoding.ASCII.GetBytes(Vector);
            byte[] bytes2 = global::System.Text.Encoding.ASCII.GetBytes(Salt);

            // Si ce n'est pas du Base64 valide, ça va planter ici, donc on met un try/catch
            byte[] array = global::System.Convert.FromBase64String(cipherText);

            int count = 0;
            byte[] array2;
            using (global::System.Security.Cryptography.SymmetricAlgorithm symmetricAlgorithm = new global::System.Security.Cryptography.AesManaged())
            {
                global::System.Security.Cryptography.Rfc2898DeriveBytes rfc2898DeriveBytes = new global::System.Security.Cryptography.Rfc2898DeriveBytes(password, bytes2, Iterations);
                byte[] bytes3 = rfc2898DeriveBytes.GetBytes(KeySize / 8);
                symmetricAlgorithm.Mode = global::System.Security.Cryptography.CipherMode.CBC;
                try
                {
                    using (global::System.Security.Cryptography.ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor(bytes3, bytes))
                    {
                        using (global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream(array))
                        {
                            using (global::System.Security.Cryptography.CryptoStream cryptoStream = new global::System.Security.Cryptography.CryptoStream(stream, transform, global::System.Security.Cryptography.CryptoStreamMode.Read))
                            {
                                array2 = new byte[array.Length];
                                count = cryptoStream.Read(array2, 0, array2.Length);
                            }
                        }
                    }
                }
                catch (global::System.Security.Cryptography.CryptographicException ex)
                {
                    if (logger != null) logger.Error("failed to decrypt data " + ex.Message);
                    plainText = string.Empty;
                    return false;
                }
                symmetricAlgorithm.Clear();
            }
            plainText = global::System.Text.Encoding.UTF8.GetString(array2, 0, count);
            return true;
        }
        catch (Exception)
        {
            // Catch global au cas où cipherText n'est pas du Base64 valide
            plainText = string.Empty;
            return false;
        }
    }
}