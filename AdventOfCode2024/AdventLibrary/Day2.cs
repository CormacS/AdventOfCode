using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventLibrary
{
    public class Day2
    {
        public static void Execute()
        {
            Day2 day2 = new Day2();

            string filePath = "InputFiles/DummyData.txt";

            string[] lines = File.ReadAllLines(filePath);

            int[][] numbers = new int[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] split = lines[i].Split();
                numbers[i] = new int[split.Length];
                for(int j = 0; j < split.Length; j++)
                {
                    numbers[i][j] = int.Parse(split[j]);
                }                
            }

            var part1 = day2.Part2(numbers);
        }

        public int Part1(int[][] numbers)
        {
            var counter = 0;
            var safeRow = false;

            for(int i = 0;i < numbers.Length; i++)
            {
                safeRow = false;
                bool increase = false;
                bool decrease = false;

                for (int j = 0; j < numbers[i].Length; j++)
                {
                    // make sure we dont go out of bounds
                    if(j + 1 == numbers[i].Length)
                    {
                        break;
                    }

                    // if same number we can leave
                    if(numbers[i][j] == numbers[i][j + 1])
                    {
                        safeRow = false;
                        break;                        
                    }

                    // checking if we are increasing or decreasing in this row
                    if(increase == false && decrease == false)
                    {
                        if (numbers[i][j] < numbers[i][j + 1])
                        {
                            increase = true;
                        }
                        else
                        {
                            decrease = true;
                        }
                    }

                    // means we are meant to be decreasing, not safe
                    if (numbers[i][j] < numbers[i][j + 1] && decrease)
                    {
                        safeRow = false;
                        break;
                    }

                    if (numbers[i][j] > numbers[i][j + 1] && increase)
                    {
                        safeRow = false;
                        break;
                    }

                    var diff = Math.Abs(numbers[i][j] - numbers[i][j + 1]);

                    if (diff > 3)
                    {
                        safeRow = false;                       
                        break;
                    }

                    safeRow = true;

                }

                if(safeRow)
                {
                    counter++;
                }

            }

            Console.WriteLine($"safe: {counter}");
            return counter;
        }

        public int Part2(int[][] numbers)
        {
            var counter = 0;

            for(int i = 0; i < numbers.Length; i++)
            {
                bool? isIncrease = null;
                var badLevel = 0;

                for(int j = 0; j < numbers[i].Length; j++)
                {
                    var safe = true;

                    // make sure we dont go out of bounds
                    if(j + 1 == numbers[i].Length)
                    {
                        break;
                    }

                    // if same number, unsafe level
                    if(numbers[i][j] == numbers[i][j + 1])
                    {
                        safe = false;
                    }
                    else if(isIncrease == null)
                    {
                        isIncrease = numbers[i][j] < numbers[i][j + 1];
                    }

                    if(isIncrease == true)
                    {
                        // means we are decreasing when we should be increasing
                        if(numbers[i][j] > numbers[i][j + 1])
                        {
                            safe = false;
                        }
                    }
                    else
                    {
                        // means we are increasing and should be decreasing
                        if(numbers[i][j] < numbers[i][j + 1])
                        {
                            safe = false;
                        }
                    }

                    // if difference is greater than 3 its unsafe
                    if((Math.Abs(numbers[i][j] - numbers[i][j + 1])) > 3)
                    {
                        safe = false;
                    }

                    if (!safe)
                    {
                        badLevel++;
                    }
                }

                if(badLevel < 2)
                {
                    counter++;
                }

            }

            Console.WriteLine($"safe: {counter}");
            return counter;
        }
    }
}
