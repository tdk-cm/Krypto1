using ClosedXML.Excel;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Krypto1
{
    internal class Program
    {
        static int iterations = 10000;
        static void Main(string[] args)
        {
            //StartTests();
            CypherUp();
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        static void CypherUp()
        {
            string initial = "Hello from California";
            Console.WriteLine("Cypher!");
            Console.WriteLine($"Initial string: {initial}");

            string encrypted = Cypher.EncryptString(initial);
            Console.WriteLine($"Encrypted: {encrypted}");

            string decrypted = Cypher.DecryptString(encrypted);
            Console.WriteLine($"Decrypted: {decrypted}");
        }

        static void StartTests()
        {
            Stopwatch randomWatch = Stopwatch.StartNew();
            List<int> randomList = TestRandom();
            randomWatch.Stop();

            Stopwatch cryptoWatch = Stopwatch.StartNew();
            List<int> cryptoList = TestRngProvider();
            cryptoWatch.Stop();


            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("cryptoTestData");
            ws.Cell(1, 1).SetValue("System.Random");
            ws.Cell(2, 1).SetValue($"{iterations.ToString()} iterations");
            ws.Cell(3, 1).SetValue($"Time elapsed: {randomWatch.ElapsedMilliseconds} ms");
            ws.Cell(1, 2).InsertData(randomList);

            ws.Cell(1, 3).SetValue("RNGCryptoServiceProvider");
            ws.Cell(2, 3).SetValue($"{iterations.ToString()} iterations");
            ws.Cell(3, 3).SetValue($"Time elapsed: {cryptoWatch.ElapsedMilliseconds} ms");
            ws.Cell(1, 4).InsertData(cryptoList);
            wb.SaveAs(@"C:\Skole\Crypto\testData.xlsx");
        }

        static List<int> TestRandom()
        {
            List<int> returnList = new List<int>();

            Random rand = new Random();

            for (int i = 0; i < iterations; i++)
            {
                returnList.Add(rand.Next());
            }

            return returnList;
        }

        static List<int> TestRngProvider()
        {
            List<int> returnList = new List<int>();

            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] number = new byte[4];

                for (int i = 0; i < iterations; i++)
                {
                    provider.GetBytes(number);

                    returnList.Add(BitConverter.ToInt32(number));
                }
            }

            return returnList;
        }
    }
}