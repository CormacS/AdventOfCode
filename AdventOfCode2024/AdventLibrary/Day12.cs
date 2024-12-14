using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary
{
    public class Day12
    {
        public static void Execute()
        {
            Day12 day12 = new Day12();

            string filePath = "InputFiles/Day12.txt";
            string[] lines = File.ReadAllLines(filePath);

            // turn list into grid of chars
            char[,] grid = new char[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    grid[i,j] = lines[i][j];
                }
            }

            day12.Part1(grid);
            // part 2 TBD, still a bug in it
            //day12.Part2(grid)
        }

        public void Part1(char[,] grid)
        {
            Console.WriteLine("Grid:");
            PrintGrid(grid);

            var visited = new bool[grid.GetLength(0), grid.GetLength(1)];
            var price = 0;

            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    char letter = grid[i, j];
                    if(!visited[i, j])
                    {
                        var (area, perimeter) = CalculateAreaAndPerimeter(grid, visited, i, j, letter);
                        Console.WriteLine($"Letter: {letter}, Area: {area}, Perimeter: {perimeter}");

                        price = price + (area * perimeter);
                    }
                }
            }

            Console.WriteLine($"Total Price: {price}");
        }

        public void Part2(char[,] grid)
        {
            Console.WriteLine("Grid:");
            PrintGrid(grid);

            var visited = new bool[grid.GetLength(0), grid.GetLength(1)];
            var price = 0;

            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    char letter = grid[i, j];
                    if(!visited[i, j])
                    {
                        var (area, perimeter) = CalculateAreaAndPerimeter(grid, visited, i, j, letter);
                        Console.WriteLine($"Letter: {letter}, Area: {area}, Perimeter: {perimeter}");

                        price = price + (area * perimeter);
                    }
                }
            }

            Console.WriteLine($"Total Price: {price}");
        }

        static (int area, int perimeter) CalculateAreaAndPerimeter(char[,] grid, bool[,] visited, int x, int y, char target)
        {
            int area = 0, perimeter = 0;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            var queue = new Queue<(int, int)>();
            queue.Enqueue((x, y));

            // Directions for neighbors (down, up, right, left)
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            while(queue.Count > 0)
            {
                var (currX, currY) = queue.Dequeue();

                // Skip if already visited or not part of the target region
                if(currX < 0 || currX >= rows || currY < 0 || currY >= cols || visited[currX, currY] || grid[currX, currY] != target)
                    continue;

                // Mark the cell as visited
                visited[currX, currY] = true;

                // Increment area for each visited cell
                area++;

                // Check neighbors to calculate perimeter
                for(int i = 0; i < 4; i++)
                {
                    int newX = currX + dx[i];
                    int newY = currY + dy[i];

                    // Out of bounds or neighboring a different character contributes to the perimeter
                    if(newX < 0 || newX >= rows || newY < 0 || newY >= cols || grid[newX, newY] != target)
                    {
                        perimeter++;
                    }
                    else if(!visited[newX, newY])
                    {
                        queue.Enqueue((newX, newY));
                    }
                }
            }

            return (area, perimeter);
        }
        static (int area, int perimeter) CalculateAreaAndPerimeterPart2(char[,] grid, bool[,] visited, int x, int y, char target)
        {
            int area = 0, perimeter = 0;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            var queue = new Queue<(int, int)>();
            queue.Enqueue((x, y));

            // Directions for neighbors (down, up, right, left)
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            while(queue.Count > 0)
            {
                var (currX, currY) = queue.Dequeue();

                // Skip if already visited or not part of the target region
                if(currX < 0 || currX >= rows || currY < 0 || currY >= cols || visited[currX, currY] || grid[currX, currY] != target)
                    continue;

                // Mark the cell as visited
                visited[currX, currY] = true;

                // Increment area for each visited cell
                area++;

                // Check neighbors to calculate perimeter
                for(int i = 0; i < 4; i++)
                {
                    int newX = currX + dx[i];
                    int newY = currY + dy[i];

                    // Out of bounds or neighboring a different character contributes to the perimeter
                    if(newX < 0 || newX >= rows || newY < 0 || newY >= cols || grid[newX, newY] != target)
                    {
                        // for part 2 before we say add, check if there is another of the same letter around it
                        if(i == 0)
                        {
                            //down
                            if(Down(grid, currX, currY, target, rows, cols))
                            {
                                perimeter++;
                            }
                        }

                        if(i == 1)
                        {
                            if(Top(grid, currX, currY, target, rows, cols))
                            {
                                perimeter++;
                            }

                        }

                        if(i == 2)
                        {
                            if(Right(grid, currX, currY, target, rows, cols))
                            {
                                perimeter++;
                            }
                        }

                        if(i == 3)
                        {
                            if(Left(grid, currX, currY, target, rows, cols))
                            {
                                perimeter++;
                            }
                        }

                    }
                    else if(!visited[newX, newY])
                    {
                        queue.Enqueue((newX, newY));
                    }
                }
            }

            return (area, perimeter);
        }
        public static bool Top(char[,] grid, int currX, int currY, char target, int rows, int cols)
        {
            // if we believe a fence can be added on top, check if another target is to the right
            // if there is, wait until the end as we want sides now
            int[] topX = {0};
            int[] topY = {1};

            for(int i = 0; i < 1; i++)
            {
                int newX = currX + topX[i];
                int newY = currY + topY[i];

                // nothing above || nothing right || next is target
                if(newX < 0 || newX >= rows || newY < 0 || newY >= cols || grid[newX, newY] == target)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Down(char[,] grid, int currX, int currY, char target, int rows, int cols)
        {
            // if we believe a fence can be added on bottom, check if another target is to the right
            // if there is, wait until the end as we want sides now
            int[] topX = { 0 };
            int[] topY = { 1 };

            for(int i = 0; i < 1; i++)
            {
                int newX = currX + topX[i];
                int newY = currY + topY[i];

                // nothing above || nothing right || next is target
                if(newX < 0 || newX >= rows || newY < 0 || newY >= cols || grid[newX, newY] == target)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Right(char[,] grid, int currX, int currY, char target, int rows, int cols)
        {
            // if we believe a fence can be added on top, check if another target is to the down and bottom right
            // if there is, wait until the end as we want sides now
            int[] topX = { 1, 1 };
            int[] topY = { 0, 1 };

            for(int i = 0; i < 1; i++)
            {
                int newX = currX + topX[i];
                int newY = currY + topY[i];

                // nothing above || nothing right || next is target
                if(newX < 0 || newX >= rows || newY < 0 || newY >= cols || grid[newX, newY] == target)
                {
                    newX = currX + topX[1];
                    newY = currY + topY[1];

                    //check bottom right char
/*                    if(newX < 0 || newX >= rows || newY < 0 || newY >= cols || grid[newX, newY] == target)
                    {
                        return true;
                    }*/

                    return false;
                }
            }
            return true;
        }

        public static bool Left(char[,] grid, int currX, int currY, char target, int rows, int cols)
        {
            // if we believe a fence can be added on top, check if another target is to the right
            // if there is, wait until the end as we want sides now
            int[] topX = { 1, -1 };
            int[] topY = { 0, -1 };

            for(int i = 0; i < 1; i++)
            {
                int newX = currX + topX[i];
                int newY = currY + topY[i];

                // nothing above || nothing right || next is target
                if(newX < 0 || newX >= rows || newY < 0 || newY >= cols || grid[newX, newY] == target)
                {
                    newX = currX + topX[1];
                    newY = currY + topY[1];

                    // check bttom
/*                    if(newX < 0 || newX >= rows || newY < 0 || newY >= cols || grid[newX, newY] == target)
                    {
                        return true;
                    }*/

                    return false;
                }
            }
            return true;
        }

        static void PrintGrid(char[,] grid)
        {
            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
