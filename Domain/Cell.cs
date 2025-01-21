using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public record Cell
    {
        public const char START = 'S';
        public const char END = 'G';
        public const char WALL = 'X';
        public const char EMPTY_SPACE = '_';

        public int X { get; init; }
        public int Y { get; init; }

        [JsonIgnore]
        public bool IsEmptySpace { get; init; }
        public Cell(Maze maze, int x, int y)
        {
            if (!IsValidCell(maze, x, y))
            {
                throw new ArgumentOutOfRangeException($"{nameof(Cell)}:{x},{y}");
            }
            X = x;
            Y = y;
            IsEmptySpace = maze[x, y] == Cell.EMPTY_SPACE;
        }

        public Cell(int x, int y, bool IsEmptySpace)
        {
            X = x;
            Y = y;
            this.IsEmptySpace = IsEmptySpace;
        }
        public static List<Cell> GetNeighbors(Maze maze, int x, int y)
        {
            var neighbors = new List<Cell>();
            //upper cell
            if (IsValidCell(maze, x - 1, y) && (maze[x - 1, y] == Cell.EMPTY_SPACE || maze[x - 1, y] == Cell.END))
            {
                neighbors.Add(new Cell(maze, x - 1, y));
            }
            //lower cell
            if (IsValidCell(maze, x + 1, y) && (maze[x + 1, y] == Cell.EMPTY_SPACE || maze[x + 1, y] == Cell.END))
            {
                neighbors.Add(new Cell(maze, x + 1, y));
            }
            //left cell
            if (IsValidCell(maze, x, y - 1) && (maze[x, y - 1] == Cell.EMPTY_SPACE || maze[x, y - 1] == Cell.END))
            {
                neighbors.Add(new Cell(maze, x, y - 1));
            }
            //right cell
            if (IsValidCell(maze, x, y + 1) && (maze[x, y + 1] == Cell.EMPTY_SPACE|| maze[x, y + 1] ==Cell.END))
            {
                neighbors.Add(new Cell(maze, x, y + 1));
            }
            return neighbors;
        }

        public static bool IsValidCell(Maze maze, int x, int y)
        {
            return !(x < 0 || y < 0 || x >= maze.GetNumberRows() || y >= maze.GetNumberColumnsInRow(x));
        }
    }
}
