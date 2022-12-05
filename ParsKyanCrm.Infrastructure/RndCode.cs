using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomHelper
{
    public class RndCode
    {
        public static int minVal { get; set; }
        public static int MaxVal { get; set; }
        public static string RndCount { get; set; }

        public static Random generator = new Random();

        public RndCode()
        {
            minVal = 10000;
            MaxVal = 99999;
            RndCount = "5";
        }

        public static string Generate()
        {
            String rnd;
            while (true) {
                rnd = generator.Next(minVal, 99999).ToString("D" + RndCount);
                if (rnd.Length == 5) {
                    break;
                }
            }
            return rnd;
        }

        public static string Generate(int minVal, int MaxVal, string RndCount) {
            Random generator = new Random();
            String rnd = generator.Next(minVal, MaxVal).ToString("D" + RndCount);

            return rnd;
        }
        public static int GenerateInt(int minVal, int MaxVal) {
            
            return generator.Next(minVal, MaxVal);
        }

        public static string GenratePassNew()
        {
            Random ran = new Random();

            String b = "abcde0f1g2hij3k4l6m5n7o8p9qr$st@uvwxyz";

            int length = 8;

            String random = "";

            for (int i = 0; i < length; i++)
            {
                int a = ran.Next(26);
                random = random + b.ElementAt(a);
            }
            return random;

        }
    }
}
