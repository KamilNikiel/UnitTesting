using System.Collections.Generic;

namespace UnitTesting.Fundamentals
{
    public class MathClass
    {
        public int Add(int a, int b)
        { 
            return a + b;
            //return 0;
        }
        
        public int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        public IEnumerable<int> GetOddNumbers(int limit)
        {
            if (limit < 0)
                for (var i = 0; i >= limit; i--)
                    if (i % 2 != 0)
                        yield return i;

            for (var i = 0; i <= limit; i++)
                if (i % 2 != 0)
                    yield return i; 
        }
    }
}