using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Krypto.Operations.File_Operations
{
    public class FileOperations
    {
        private string _dir;
        public FileOperations()
        {
            _dir = AppDomain.CurrentDomain.BaseDirectory;
        }
        /// <summary>
        /// Reads file and returns it as a string
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string readFileString(string path, string name)
        {
            string res = File.ReadAllText(_dir + path + name);

            return res;
        }

        public void parseFile(string path, string name, BigInteger[] numbers)
        {
            int quarter = numbers.Length/4;
            String str1 = "", str2 = "", str3 = "", str4 = "";

            for (int i = 0; i < quarter; i++)
            {
                str1 += numbers[i] + ",";
                str2 += numbers[2*i] + ",";
                str3 += numbers[3*i] + ",";
                str4 += numbers[4*i] + ",";
            }

            saveToFile(path, "1" + name,str1);
            saveToFile(path, "2" + name, str2);
            saveToFile(path, "3" + name, str3);
            saveToFile(path, "4" + name, str4);
        }

        /// <summary>
        /// Saves text to file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public void saveToFile(string path, string name, string text)
        {
            File.WriteAllText(_dir + path + name, text);
        }

        /// <summary>
        /// reads a file with numbers separated by ',' and returns an array of numbers
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public BigInteger[] readFileNumbers(string path, string name)
        {
            string text = readFileString(path, name);


            string[] numbers = text.Split(',');

            BigInteger[] result = new BigInteger[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                result[i] = BigInteger.Parse(numbers[i]);
            }

            return result;
        }


        public String get_dir()
        {
            return _dir;
        }



    }
}
