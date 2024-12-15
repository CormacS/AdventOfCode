using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary
{
    public class Day14
    {
        public class Robot
        {
            public int x;
            public int y;
            public int xMove;
            public int yMove;

            public Robot(int _x, int _y, int _xMove, int _yMove)
            {
                x = _x;
                y = _y;
                xMove = _xMove;
                yMove = _yMove;
            }
        }
        public static void Execute()
        {
            Day14 day14 = new Day14();

            string filePath = "InputFiles/Day14.txt";
            string[] lines = File.ReadAllLines(filePath);

            string[] list;

            List<Robot> robotList = new List<Robot>();

            foreach(string line in lines)
            {
                list = (line.Split(' '));
                string[] coords = list[0].Split("=")[1].Split(",");
                string[] movement = list[1].Split("=")[1].Split(",");
                robotList.Add(new Robot(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(movement[0]), int.Parse(movement[1])));
            }

            int result = day14.Part1(robotList);

            Console.WriteLine($"Day 14: P1: {result}"); 
            
            //day12.Part2(grid)
        }

        public int Part1(List<Robot> robotList)
        {
            int mapWidth = 101;
            int mapHeight = 103;

            int quad1 = 0;
            int quad2 = 0;
            int quad3 = 0;
            int quad4 = 0;

            int total = 0;

            foreach(Robot robot in robotList)
            {
                int finalX = (robot.x + robot.xMove * 100) % mapWidth;
                int finalY = (robot.y + robot.yMove * 100) % mapHeight;

                if(finalX < 0)
                {
                    finalX = finalX + mapWidth;
                }
                if(finalY < 0)
                {
                    finalY = finalY + mapHeight;
                }

                if(finalX < 50 && finalY < 51)
                {
                    quad1++;
                }
                if(finalX > 50 && finalY < 51)
                {
                    quad2++;
                }
                if(finalX < 50 && finalY > 51)
                {
                    quad3++;
                }
                if(finalX > 50 && finalY > 51)
                {
                    quad4++;
                }
            }

            total = quad1 * quad2 * quad3 * quad4;

            return total;
        }
    }
}
