namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Combinatorics;
    using System.Text;

    /// <summary>
    /// Each character on a computer is assigned a unique code and the preferred standard is ASCII (American Standard Code for Information Interchange). For example, uppercase A = 65, asterisk (*) = 42, and lowercase k = 107.
    /// 
    /// A modern encryption method is to take a text file, convert the bytes to ASCII, then XOR each byte with a given value, taken from a secret key. The advantage with the XOR function is that using the same encryption key on the cipher text, restores the plain text; for example, 65 XOR 42 = 107, then 107 XOR 42 = 65.
    /// 
    /// For unbreakable encryption, the key is the same length as the plain text message, and the key is made up of random bytes. The user would keep the encrypted message and the encryption key in different locations, and without both "halves", it is impossible to decrypt the message.
    /// 
    /// Unfortunately, this method is impractical for most users, so the modified method is to use a password as a key. If the password is shorter than the message, which is likely, the key is repeated cyclically throughout the message. The balance for this method is using a sufficiently long password key for security, but short enough to be memorable.
    /// 
    /// Your task has been made easy, as the encryption key consists of three lower case characters. Using cipher1.txt, a file containing the encrypted ASCII codes, and the knowledge that the plain text must contain common English words, decrypt the message and find the sum of the ASCII values in the original text.
    /// </summary>
    [ProblemResource("cipher1")]
    [Result(Name = "sum", Expected = "107359")]
    public class Problem059 : Problem
    {
        public override string Solve(string resource)
        {
            var bytes = (from l in resource.Split(',')
                         select (byte)int.Parse(l)).ToArray();

            var passwords = new Variations<byte>(Enumerable.Range(0, 26).Select(l => (byte)l).ToList(), 3, GenerateOption.WithRepetition);

            var maxLatinCharacters = 0;
            var maxLatinMessage = "";
            foreach (var pass in passwords)
            {
                var passArray = pass.ToArray();
                for (int i = 0; i < passArray.Length; i++)
                {
                    passArray[i] += (byte)'a';
                }

                var sb = new StringBuilder();

                var latinCharacters = 0;
                for (int i = 0; i < bytes.Length; i++)
                {
                    var character = (char)(byte)(passArray[i % passArray.Length] ^ bytes[i]);
                    if ((character >= 'A' && character <= 'Z') ||
                        (character >= 'a' && character <= 'z') ||
                        (character >= '0' && character <= '9') ||
                        (character == ' ') ||
                        (character == '.') ||
                        (character == ','))
                    {
                        latinCharacters++;
                    }

                    sb.Append((char)character);
                }

                if (latinCharacters > maxLatinCharacters)
                {
                    maxLatinCharacters = latinCharacters;
                    maxLatinMessage = sb.ToString();
                }
            }

            var sum = maxLatinMessage.ToCharArray().Select(c => (int)c).Sum();
            return sum.ToString();
        }
    }
}
