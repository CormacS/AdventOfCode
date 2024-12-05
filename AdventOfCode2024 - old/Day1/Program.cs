using System.Text.RegularExpressions;


string[] list = File.ReadAllLines("input.txt");

List<int> list2 = new List<int>();
List<int> list3 = new List<int>();

int counter = 0;

for (int i = 0; i < list.Length; i++)
{
    string[] split = Regex.Split(list[i].Trim(), @"\s+");
    list2.Add(int.Parse(split[0]));
    list3.Add(int.Parse(split[1]));
}

list2.Sort();
list3.Sort();

for(int i = 0;i < list2.Count; i++)
{
    counter = counter + Math.Abs(list2[i] - list3[i]);
}

Console.WriteLine(counter);


public void Hello(







