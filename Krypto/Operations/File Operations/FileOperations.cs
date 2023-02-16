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



    }
}
