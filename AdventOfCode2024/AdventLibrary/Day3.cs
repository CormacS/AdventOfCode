using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventLibrary
{
    public class Day3
    {
        public static void Execute()
        {
            Day3 day3 = new Day3();

            string filePath = "InputFiles/Day3.txt";
            string[] lines = File.ReadAllLines(filePath);

            Console.WriteLine($"Day 3: P1: {day3.Part1(lines)}, P2: {day3.Part2(lines)}");
        }

        public int Part1(string[] lines)
        {
            // I hate regex, gna chatgpt it from now on
            string pattern = @"mul\(\d+,\d+\)";
            List<string> valid = new List<string>();
            int total = 0;

            foreach(string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, pattern);

                foreach(Match match in matches)
                {
                    valid.Add(match.Value);
                }
            }

            foreach(string mul in valid)
            {
                string num1 = mul.Split("(")[1].Split(",")[0].TrimEnd(')');
                string num2 = mul.Split("(")[1].Split(",")[1].TrimEnd(')');

                int a = int.Parse(num1);
                int b = int.Parse(num2);

                total = total + (a * b);
            }

            return total;
        }


        public int Part2(string[] lines)
        {
            // I hate even more regex, gna chatgpt it from now on
            string pattern = @"(?:mul\(\d+,\d+\)|do\(\)|don't\(\))";
            List<string> valid = new List<string>();
            int total = 0;

            foreach(string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, pattern);

                foreach(Match match in matches)
                {
                    valid.Add(match.Value);
                }
            }

            bool yes = false;
            foreach(string mul in valid)
            {
                if(mul.Equals("do()"))
                {
                    yes = true;
                    continue;
                }
                if(mul.Equals("don't()"))
                {
                    yes = false;
                    continue;
                }

                if(yes)
                {
                    string num1 = mul.Split("(")[1].Split(",")[0].TrimEnd(')');
                    string num2 = mul.Split("(")[1].Split(",")[1].TrimEnd(')');

                    int a = int.Parse(num1);
                    int b = int.Parse(num2);

                    total = total + (a * b);
                }
            }

            return total;
        }
    }
}
