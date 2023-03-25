using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST407_HillCypher_NickandArmando
{
    public class HillCypherHelper
    {
        public string plainText { get; set; }
        public string cipherText { get; set; }
        public string key { get; set; }
        public int[,] keyMatrix { get; set; }
        public int[,] inverseKey { get; set; }
     
        public string Encryption(string plainText, string key)
        {
            plainText = PlainTextPadding(plainText);
            key = KeyPadding(key);
            plainText = plainText.ToUpper();
            key = key.ToUpper();

            //Get Key Matrix
            bool badKey = false;
            
            //Sets Key Matri
            int det = 0;
            keyMatrix = new int[3, 3];
            GetKeyMatrix(key);

            /*false on badKey means that the determinant is not 0 and that the determinant and 26 have a gcd of 1, 
             * so the key should work. If it comes up true though, the key will not work and the key is inversible. */
            badKey = CheckDeterminant(det);
            

            //Seperates plaintext into sections of 3 and turns them into values of 0 to 25
            int sections = plainText.Length / 3;
            int[,] messageVector = new int[3, sections];
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(plainText);
            int count = 0;

            for (int i = 0; i < sections; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    messageVector[j, i] = ASCIIValues[count] - 65;
                    count++;
                }
            }

            //encrypts plaintext into cipher text
            int[,] cipherMatrix = new int[plainText.Length, sections];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < sections; j++)
                {
                    cipherMatrix[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        cipherMatrix[i, j] += keyMatrix[i, k] * messageVector[k, j];
                    }
                    cipherMatrix[i, j] = cipherMatrix[i, j] % 26;
                }
            }

            //Converts to Capital letter ascii values
            string cipherText = "";
            for(int i = 0; i  < sections; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cipherText += (char)(cipherMatrix[j, i] + 65);
                }
            }

            cipherText = cipherText.ToLower();
            return cipherText;
        }

        public bool CheckDeterminant(int det)
        {
            //gets and checks to see if determinent works
            det = Determinant(det, 3, keyMatrix);
            if (det == 0 || (det % 13) == 0 || (det % 2) == 0)
                return true;
            else
                return false;
        }

        public bool HomePageCheckDet(int det, int[,] keyMatrix)
        {
            //gets and checks to see if determinent works
            det = Determinant(det, 3, keyMatrix);
            if (det == 0 || (det % 13) == 0 || (det % 2) == 0)
                return true;
            else
                return false;
        }
        public int Determinant(int det, int length, int[,] matrix)
        {
            //recursive function to get determinant
            int count1, count2;
            int[,] subMatrix = new int[length, length];
            if (length == 2)
            {
                return (matrix[0, 0] * matrix[1, 1]) - (matrix[1, 0] * matrix[0, 1]);
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    count1 = 0;
                    for (int j = 1; j < length; j++)
                    {
                        count2 = 0;
                        for (int k = 0; k < length; k++)
                        {
                            if (k == i)
                            {
                                continue;
                            }
                            subMatrix[count1, count2] = matrix[j, k];
                            count2++;
                        }
                        count1++;
                    }
                    det += (Pow(-1, i) * matrix[0, i] * Determinant(det, length - 1, subMatrix));
                }
            }
            return det;
        }
        public int Pow(int x, int y)
        {
            int result = 1;
            for (int i = 0; i < y; i++)
            {
                result = result * x;
            }
            return result;
        }
        
        public string Decryption(string cipherText, string key)
        {
            key = KeyPadding(key);
            cipherText = cipherText.ToUpper();
            key = key.ToUpper();

            //Get Key Matrix
            keyMatrix = new int[3, 3];
            inverseKey = new int[3, 3];
            GetKeyMatrix(key);
            GetInverseKeyMatrix(key);

            //Seperates ciphertext in sections of 3
            int sections = cipherText.Length / 3;
            int[,] messageVector = new int[3, sections];
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(cipherText);
            int count = 0;

            for (int i = 0; i < sections; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    messageVector[j, i] = ASCIIValues[count] - 65;
                    count++;
                }
            }

            //encrypts cipher text into plain text
            int[,] plainMatrix = new int[cipherText.Length, sections];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < sections; j++)
                {
                    plainMatrix[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        plainMatrix[i, j] += inverseKey[i, k] * messageVector[k, j];
                    }
                    plainMatrix[i, j] = plainMatrix[i, j] % 26;
                }
            }

            //Converts to Capital letter ascii values
            string plainText = "";
            for (int i = 0; i < sections; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    plainText += (char)(plainMatrix[j, i] + 65);
                }
            }

            plainText = plainText.ToLower();
            return plainText;
        }

        public void GetKeyMatrix(string key)
        {
            //sets key matrix with values between 0 and 25
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(key);
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    keyMatrix[i, j] = ASCIIValues[count] - 65;
                    count++;
                }
            }
        }
        public int[,] GetDisplayedKeyMatrix(string key)
        {
            //sets key matrix with values between 0 and 25
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(key);
            int count = 0;
            int[,] temp = new int[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = ASCIIValues[count] - 65;
                    count++;
                }
            }
            return temp;
        }
        public void GetInverseKeyMatrix(string key)
        {
            int det = 0;
            int inverseDet = 1;
            int result = 0;

            //find mod inverse of the determinant
            det = Determinant(det, 3, keyMatrix);
            det = det % 26;
            result = (det * inverseDet) % 26;
            while (result != 1 % 26)
            {
                inverseDet++;
                result = (det * inverseDet) % 26;
            }

            //gets adjugate matrix to calculate inverse matrix
            int[,] adjugate = new int[3, 3];
            GetAdjugate(adjugate, keyMatrix);

            //sets inverse key
            inverseKey = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    inverseKey[i, j] = (inverseDet * adjugate[i, j]) % 26;
                }
            }
        }
        public int[,] GetDisplayedInverseKeyMatrix(string key, int[,] keyMatrix)
        {
            int det = 0;
            int inverseDet = 1;
            int result = 0;

            //find mod inverse of the determinant
            det = Determinant(det, 3, keyMatrix);
            det = det % 26;
            result = (det * inverseDet) % 26;
            while (result != 1 % 26)
            {
                inverseDet++;
                result = (det * inverseDet) % 26;
            }

            //gets adjugate matrix to calculate inverse matrix
            int[,] adjugate = new int[3, 3];
            GetAdjugate(adjugate, keyMatrix);

            //sets inverse key
            int[,] temp = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = (inverseDet * adjugate[i, j]) % 26;
                }
            }
            //returns temp
            return temp;
        }
        public void GetAdjugate(int[,] Adjugate, int[,] keyMatrix)
        {
            int det = 0;
            int signx = 1;
            int signy = 1;
            int[,] temp = new int[2, 2];

            for (int i = 0; i < 3; i++)
            {
                signy = 1;
                for (int j = 0; j < 3; j++)
                {
                    det = 0;
                    for (int k = 0; k < 2; k++)
                    {
                        for (int l = 0; l < 2; l++)
                        {
                            if (k < j)
                            {
                                if (l < i)
                                    temp[k, l] = keyMatrix[k, l];
                                else
                                    temp[k, l] = keyMatrix[k, l + 1];
                            }
                            else
                            {
                                if (l < i)
                                    temp[k, l] = keyMatrix[k + 1, l];
                                else
                                    temp[k, l] = keyMatrix[k + 1, l + 1];
                            }
                        }
                    }
                    Adjugate[i, j] = (signy * signx * Determinant(det, 2, temp)) % 26;
                    if (Adjugate[i, j] < 0)
                        Adjugate[i, j] += 26;
                    signy = -signy;
                }
                signx = -signx;
            }

        }
        public int[,] GetDisplayedMessageVector(int [,]keyMatrix, string plainText)
        {
            int sections = plainText.Length / 3;
            int[,] messageVector = new int[3, sections];
            plainText = plainText.ToUpper();
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(plainText);
            int count = 0;

            for (int i = 0; i < sections; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    messageVector[j, i] = ASCIIValues[count] - 65;
                    count++;
                }
            }

            return messageVector;
        }

        public int[,] GetDisplayedCipherMatrix(int[,] keyMatrix, int[,] messageVector, int sections, string plainText)
        {
            int[,] cipherMatrix = new int[plainText.Length, sections];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < sections; j++)
                {
                    cipherMatrix[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        cipherMatrix[i, j] += keyMatrix[i, k] * messageVector[k, j];
                    }
                    cipherMatrix[i, j] = cipherMatrix[i, j] % 26;
                }
            }
            return cipherMatrix;
        }

        public string PlainTextPadding(string plainText)
        {
            if (plainText.Length % 3 != 0)
            {
                int temp = plainText.Length % 3;
                temp = 3 - temp;

                for (int i = 0; i < temp; i++)
                {
                    plainText += 'x';
                }
            }
            return plainText;
        }

        public string KeyPadding(string key)
        {
            string keyFiller = "abcdefghi";

            if (key.Length != 9)
            {
                int temp = key.Length;
                temp = 9 - temp;

                for (int i = 0; i < temp; i++)
                {
                    key += keyFiller[i];
                }
            }
            return key;
        }

        public bool CipherTextChecker(string cipherText)
        {
            if (cipherText.Count() % 3 != 0)
                return false;
            else
                return true;
        }
        public bool KeyChecker(string keyText)
        {
            if (keyText.Count() < 1 || keyText.Count() > 9)
                return false;
            else
                return true;
        }
        internal string GetInverseKeyMatrixAsString(string key)
        {
            key.ToUpper();
            int det = 0;
            int inverseDet = 1;
            int result = 0;
            keyMatrix = new int[3,3];
            GetKeyMatrix(key);

            //find mod inverse of the determinant
            det = Determinant(det, 3, keyMatrix);
            det = det % 26;
            result = (det * inverseDet) % 26;
            while (result != 1 % 26)
            {
                inverseDet++;
                result = (det * inverseDet) % 26;
            }

            //gets adjugate matrix to calculate inverse matrix
            int[,] adjugate = new int[3, 3];
            GetAdjugate(adjugate, keyMatrix);

            //sets inverse key
            int [,]temp = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = (inverseDet * adjugate[i, j]) % 26;
                }
            }

            string invKey = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    invKey += (char)(temp[j, i] + 65);
                }
            }
            keyMatrix = null;
            invKey.ToList();
            return invKey;
        }
    }
}
