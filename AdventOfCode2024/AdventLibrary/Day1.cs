using System.Text.RegularExpressions;

namespace AdventLibrary
{
    public class Day1
    {
        public static void Execute()
        {
            Day1 day1 = new Day1();

            string filePath = "InputFiles/Day1.txt";

            try
            {
                Console.WriteLine($"Day 1 - {day1.Start(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public string Start(string filePath)
        {
            string[] list = File.ReadAllLines(filePath);

            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();

            for (int i = 0; i < list.Length; i++)
            {
                string[] split = Regex.Split(list[i].Trim(), @"\s+");
                list2.Add(int.Parse(split[0]));
                list3.Add(int.Parse(split[1]));
            }

            list2.Sort();
            list3.Sort();


            var part1 = Part1(list2, list3);

            var part2 = Part2(list2, list3);

            var result = $"Answers are: P1:{part1} and P2:{part2}";
            return result;
        }

        public int Part1(List<int> list2, List<int> list3)
        {
            int counter = 0;

            for (int i = 0; i < list2.Count; i++)
            {
                counter = counter + Math.Abs(list2[i] - list3[i]);
            }

            return counter;
        }

        public int Part2(List<int> list2, List<int> list3)
        {
            int counter = 0;

            for (int i = 0; i < list2.Count; i++)
            {
                int occurences = list3.Where(x => x.Equals(list2[i])).Count();

                int result = list2[i] * occurences;
                counter = counter + result;
            }

            return counter;
        }
    }
}
