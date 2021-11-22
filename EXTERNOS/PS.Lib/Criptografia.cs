using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Criptografia
    {

        #region HASH - UNIDIRECIONAL

        public enum OpcoesHash { SHA1, SHA256, SHA384, SHA512, MD5 }

        public String Hash(OpcoesHash Tipo, String Texto)
        {
            String result = "";

            if (Tipo.Equals(OpcoesHash.SHA1))
            {
                result = HashSHA1(Texto);
            }

            if (Tipo.Equals(OpcoesHash.SHA256))
            {
                result = HashSHA256(Texto);
            }

            if (Tipo.Equals(OpcoesHash.SHA384))
            {
                result = HashSHA384(Texto);
            }

            if (Tipo.Equals(OpcoesHash.SHA512))
            {
                result = HashSHA512(Texto);
            }

            if (Tipo.Equals(OpcoesHash.MD5))
            {
                result = HashMD5(Texto);
            }

            return result;
        }

        private String HashSHA1(String Texto)
        {
            byte[] b = new System.Security.Cryptography.SHA1Managed().ComputeHash(ASCIIEncoding.ASCII.GetBytes(Texto));
            return Convert.ToBase64String(b, 0, b.Length);
        }

        private String HashSHA256(String Texto)
        {
            byte[] b = new System.Security.Cryptography.SHA256Managed().ComputeHash(ASCIIEncoding.ASCII.GetBytes(Texto));
            return Convert.ToBase64String(b, 0, b.Length);
        }

        private String HashSHA384(String Texto)
        {
            byte[] b = new System.Security.Cryptography.SHA384Managed().ComputeHash(ASCIIEncoding.ASCII.GetBytes(Texto));
            return Convert.ToBase64String(b, 0, b.Length);
        }

        private String HashSHA512(String Texto)
        {
            byte[] b = new System.Security.Cryptography.SHA512Managed().ComputeHash(ASCIIEncoding.ASCII.GetBytes(Texto));
            return Convert.ToBase64String(b, 0, b.Length);
        }

        private String HashMD5(String Texto)
        {
            byte[] b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(Texto));
            return Convert.ToBase64String(b, 0, b.Length);
        }

        #endregion

        #region CRIPTOGRAFIA - BIDIRECIONAL (ENCODER/DECODER)

        public enum OpcoesEncoder { Rijndael, RC2, DES, TripleDES }
        private System.Security.Cryptography.SymmetricAlgorithm Algoritmo;

        private void SetAlgoritmo(OpcoesEncoder Tipo)
        {
            if (Tipo.Equals(OpcoesEncoder.Rijndael))
            {
                Algoritmo = new System.Security.Cryptography.RijndaelManaged();
                Algoritmo.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
            }

            if (Tipo.Equals(OpcoesEncoder.RC2))
            {
                Algoritmo = new System.Security.Cryptography.RC2CryptoServiceProvider();
                Algoritmo.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
            }

            if (Tipo.Equals(OpcoesEncoder.DES))
            {
                Algoritmo = new System.Security.Cryptography.DESCryptoServiceProvider();
                Algoritmo.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
            }

            if (Tipo.Equals(OpcoesEncoder.TripleDES))
            {
                Algoritmo = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
                Algoritmo.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
            }
        }

        private byte[] GetChave(String Chave)
        {
            string salt = string.Empty;

            // Ajusta o tamanho da chave se necessário e retorna uma chave válida
            if (Algoritmo.LegalKeySizes.Length > 0)
            {
                // Tamanho das chaves em bits
                int keySize = Chave.Length * 8;
                int minSize = Algoritmo.LegalKeySizes[0].MinSize;
                int maxSize = Algoritmo.LegalKeySizes[0].MaxSize;
                int skipSize = Algoritmo.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Busca o valor máximo da chave
                    Chave = Chave.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Seta um tamanho válido
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Preenche a chave com arterisco para corrigir o tamanho
                        Chave = Chave.PadRight(validSize / 8, '*');
                    }
                }
            }

            System.Security.Cryptography.PasswordDeriveBytes key = new System.Security.Cryptography.PasswordDeriveBytes(Chave, ASCIIEncoding.ASCII.GetBytes(salt));
            return key.GetBytes(Chave.Length);
        }

        public String Encoder(OpcoesEncoder Tipo, String Texto, String _Chave)
        {
            SetAlgoritmo(Tipo);
            Algoritmo.Mode = System.Security.Cryptography.CipherMode.CBC;

            byte[] plainByte = ASCIIEncoding.ASCII.GetBytes(Texto);
            byte[] keyByte = GetChave(_Chave);

            // Seta a chave privada
            Algoritmo.Key = keyByte;

            // Interface de criptografia / Cria objeto de criptografia
            System.Security.Cryptography.ICryptoTransform cryptoTransform = Algoritmo.CreateEncryptor();
            System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream();

            System.Security.Cryptography.CryptoStream _cryptoStream = new System.Security.Cryptography.CryptoStream(_memoryStream, cryptoTransform, System.Security.Cryptography.CryptoStreamMode.Write);

            // Grava os dados criptografados no MemoryStream
            _cryptoStream.Write(plainByte, 0, plainByte.Length);
            _cryptoStream.FlushFinalBlock();

            // Busca o tamanho dos bytes encriptados
            byte[] cryptoByte = _memoryStream.ToArray();

            // Converte para a base 64 string para uso posterior em um xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        public String Decoder(OpcoesEncoder Tipo, String Texto, String _Chave)
        {
            SetAlgoritmo(Tipo);
            Algoritmo.Mode = System.Security.Cryptography.CipherMode.CBC;

            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(Texto);
            byte[] keyByte = GetChave(_Chave);

            // Seta a chave privada
            Algoritmo.Key = keyByte;

            // Interface de criptografia / Cria objeto de descriptografia
            System.Security.Cryptography.ICryptoTransform cryptoTransform = Algoritmo.CreateDecryptor();
            try
            {
                System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream(cryptoByte, 0, cryptoByte.Length);

                System.Security.Cryptography.CryptoStream _cryptoStream = new System.Security.Cryptography.CryptoStream(_memoryStream, cryptoTransform, System.Security.Cryptography.CryptoStreamMode.Read);

                // Busca resultado do CryptoStream
                System.IO.StreamReader _streamReader = new System.IO.StreamReader(_cryptoStream);
                return _streamReader.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
