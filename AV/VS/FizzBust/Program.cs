using System;

namespace FizzBust
{
    public class Program
    {
        private static Int32 maxNum = 20;
        private static bool found = false;
        static void Main(string[] args)
        {
            for(Int32 iCnt = 1; iCnt <= maxNum; iCnt++)
            {
                if(3 * 5 == iCnt)
                {
                    Console.WriteLine("FizzBust");
                    found = true;
                }

                if (iCnt % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                } else {
                    if (iCnt % 5 == 0)
                    {
                        Console.WriteLine("bust");
                        found = true;
                    }

                    if(!found)
                    {
                        Console.WriteLine(iCnt.ToString());
                    }
                }
                found = false;
            }
            Console.ReadLine();
        }
    }
}
