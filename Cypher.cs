using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypto1
{
    internal class Cypher
    {
        static int amountToShift = 1;
        public static string EncryptString(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                c += (char)1;
                output += c;
            }

            return output;
        }

        public static string DecryptString(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                c -= (char)1;
                output += c;
            }

            return output;
        }
    }
}
