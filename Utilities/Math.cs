using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Utilities
{
    public static class Math
    {
        public static int Sum(string message)
        {
            string[] splitted = message.Split(' ');
            List<int> filteredInts = Parse(splitted);
            return Count(filteredInts);
            

        }
        private static List<int> Parse(string[] message)
        {
            List<int> ints = new List<int>();
            foreach (string item in message)
            {
                try
                {
                    ints.Add(int.Parse(item));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ints;
        }

        private static int Count(List<int> ints)
        {
            int result = 0;
            foreach (int i in ints)
            {
                result+=i;
            }
            return result;
        }
    }
}
