using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;



// Before I start was just going to loop through until found answer
/*
 * Example: Goal - 8400 AX - 94 and BX - 22
 * 8400/94 = 89 (Whole Number)
 * 94 * 89 = 8366
 * 8400 - 8366 = 34
 * 34 / 22 =  1.54
 * I would repeat this process by lowering 89 by 1 everytime until the final number divided by 22 goes evenly
 * This would be 94 * 80 = 7520    8400 - 7520 = 880 / 22   = 40, meaning A is 80 times and B is 40 times.
 * However there must be a formula for finding intersect will have to find it. Good luck future me!
 */
namespace AdventLibrary
{
    public class Day13
    {
        public static void Execute()
        {
            Day13 day13 = new Day13();

            string filePath = "InputFiles/Day13.txt";
            string[] lines = File.ReadAllLines(filePath)
                                 .Where(line => !string.IsNullOrEmpty(line)).ToArray();
            long total1 = 0;
            long total2 = 0;

            for(int i = 0; i < lines.Length; i += 3)
            {
                if(!string.IsNullOrEmpty(lines[i]))
                {
                    var aCoords = lines[i].Split(':')[1].Split(',');
                    var bCoords = lines[i+1].Split(':')[1].Split(',');
                    var prizeTargets = lines[i+2].Split(':')[1].Split(',');

                    int aX = int.Parse(aCoords[0].Trim().Substring(2));
                    int aY = int.Parse(aCoords[1].Trim().Substring(2));
                    int bX = int.Parse(bCoords[0].Trim().Substring(2));
                    int bY = int.Parse(bCoords[1].Trim().Substring(2));
                    long prizeX = long.Parse(prizeTargets[0].Trim().Substring(2));
                    long prizeY = long.Parse(prizeTargets[1].Trim().Substring(2));

                    long result1 = day13.Part1(aX, bX, prizeX, aY, bY, prizeY);
                    //long result2 = day13.Part2(aX, bX, prizeX, aY, bY, prizeY);

                    total1 = total1 + result1;
                    total2 = total2;
                }
            }

            Console.WriteLine($"Day 13: P1:{total1}");
        }

        public long Part1(int aX, int bX, long prizeX, int aY, int bY, long prizeY)
        {
            List<(long x, long y)> sols = new List<(long x, long y)>();

            var gcd = GCD(aX, bX);

            aX = aX / gcd;
            bX = bX / gcd;
            prizeX = prizeX / gcd;
            var t = 100;

            while(t != 0)
            {
                var sum = aX * t;
                long remainder = prizeX - sum;
                if(remainder < 0)
                {
                    t--;
                    continue;
                }
                var v = remainder % bX;

                if(v == 0)
                {
                    long totalA = t;
                    long totalB = remainder / bX;

                    if(true)
                    {
                        // YOU FORGOT TO CHECK IF IT ALSO WORKED FOR Y YOU DINGUS ITS 2:15AM!!!!!!!
                        var yCheck = (aY * totalA) + (bY * totalB);
                        if(yCheck == prizeY)
                        {
                            sols.Add((totalA, totalB));
                        }
                        
                    }
                    
                    t--;
                }
                else
                {
                    t--;
                }
            }
            var cheapestTotal = FindCheapest(sols);
            return cheapestTotal;
        }

        public long Part2(int aX, int bX, long prizeX, int aY, int bY, long prizeY)
        {
            prizeX = prizeX + 10000000000000;
            prizeY = prizeY + 10000000000000;

            List<(long x, long y)> sols = new List<(long x, long y)>();

            var gcd = GCD(aX, bX);

            aX = aX / gcd;
            bX = bX / gcd;
            prizeX = prizeX / gcd;
            var t = prizeX / aX; //t = 100

            while(t != 0)
            {
                var sum = aX * t;
                long remainder = prizeX - sum;
                if(remainder < 0)
                {
                    t--;
                    continue;
                }
                var v = remainder % bX;

                if(v == 0)
                {
                    long totalA = t;
                    long totalB = remainder / bX;

                    if(true)
                    {
                        var yCheck = (aY * totalA) + (bY * totalB);
                        if(yCheck == prizeY)
                        {
                            sols.Add((totalA, totalB));
                        }

                    }

                    t--;
                }
                else
                {
                    t--;
                }
            }
            var cheapestTotal = FindCheapest(sols);
            return cheapestTotal;
        }

        public int GCD(int a, int b)
        {
            while(a != 0 && b != 0)
            {
                if(a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            return a | b;
        }

        public long FindCheapest(List<(long x, long y)> sols)
        {
            long cheapest = 0;
            foreach(var sol in sols)
            {
                var total = (sol.x * 3) + (sol.y * 1);

                if(cheapest == 0)
                {
                    cheapest = total;
                }

                if (total < cheapest)
                {
                    cheapest = total;
                }
            }

            return cheapest;
        }
    }
}
