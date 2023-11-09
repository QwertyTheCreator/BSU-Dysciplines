using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorialColloquium
{
    public static class FactorialCollector
    {
        /// <summary>
        /// Calculate first n factorials.
        /// From 1 to n.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<long> GetFactorialRow(long upperBorder)
        {
            List<long> factorialContainer = new List<long>();

            if(upperBorder < 0)
            {
                throw new ArgumentException();
            }

            for(int i = 1; i <= upperBorder; i++) 
            {
                factorialContainer.Add(GetFactorial(i));
            }

            return factorialContainer;
        }

        private static long GetFactorial(long n)
        {
            if (n < 0)
            {
                throw new ArgumentException();
            }

            if (n == 0 || n == 1)
            {
                return 1;
            }

            return n * GetFactorial(n - 1);
        }
    }
}
