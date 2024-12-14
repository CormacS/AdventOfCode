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

            string filePath = "InputFiles/Day2.txt";

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

        public int Part2(int[][] reports)
        {
            int safeCount = 0;

            foreach(var levels in reports)
            {
                if(IsSafe(levels) || CanBeSafeWithOneRemoval(levels))
                {
                    safeCount++;
                    Console.WriteLine("Safe report: " + string.Join(" ", levels));
                }
                else
                {
                                        
                }
            }
            Console.WriteLine($"Unsafe report total: {1000 - safeCount}");
            Console.WriteLine($"Number of safe reports: {safeCount}");
            return safeCount;
        }

        static bool IsSafe(int[] numbers)
        {
            bool? isIncrease = null;

            for(int i = 0; i < numbers.Length; i++)
            {
                // make sure we dont go out of bounds
                if(i + 1 == numbers.Length)
                {
                    break;
                }

                // if same number, unsafe level
                if(numbers[i] == numbers[i + 1])
                {
                    return false;
                }
                else if(isIncrease == null)
                {
                    isIncrease = numbers[i] < numbers[i + 1];
                }

                if(isIncrease == true)
                {
                    // means we are decreasing when we should be increasing
                    if(numbers[i] > numbers[i + 1])
                    {
                        return false;
                    }
                }
                else
                {
                    // means we are increasing and should be decreasing
                    if(numbers[i] < numbers[i + 1])
                    {
                        return false;
                    }
                }

                // if difference is greater than 3 its unsafe
                if((Math.Abs(numbers[i] - numbers[i + 1])) > 3)
                {
                    return false;
                }
            }
            return true;
        }

        // How to remove 1 number from the jagged array and then compare if list is now safe
        // loops through the list of numbers and removes 1, then checks if list is safe, then try next one
        static bool CanBeSafeWithOneRemoval(int[] levels)
        {
            for(int i = 0; i < levels.Length; i++)
            {
                int[] modifiedLevels = new int[levels.Length - 1];
                int index = 0;

                for(int j = 0; j < levels.Length; j++)
                {
                    if(j != i)
                    {
                        modifiedLevels[index++] = levels[j];
                    }
                }

                if(IsSafe(modifiedLevels))
                    return true;
            }

            return false;
        }
    }
}
