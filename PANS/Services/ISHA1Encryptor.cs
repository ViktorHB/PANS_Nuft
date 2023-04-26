namespace PANS.Services
{
    public interface ISHA1Encryptor
    {
        string Encrypt(string ishText, string pass,
            string sol = "doberman", string cryptographicAlgorithm = "SHA1",
            int passIter = 2, string initVec = "a8doSuDitOz1hZe#",
            int keySize = 256);

        string Decrypt(string ciphText, string pass,
            string sol = "doberman", string cryptographicAlgorithm = "SHA1",
            int passIter = 2, string initVec = "a8doSuDitOz1hZe#",
            int keySize = 256);
    }
}

