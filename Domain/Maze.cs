using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain
{
    public class Maze
    {
        public const char START = 'S';
        public const char END = 'G';
        public const char WALL = 'X';
        public const char EMPTY_SPACE = '_';

        public const string LinuxNewLine = "\n";
        public const string WindowsNewLine = "\r\n";

        public const uint MAX_DIMENSION = 20;

        public readonly string MazeString;
        /// <summary>
        ///- `S`: Start Point (exactly one)
        ///- `G`: Goal Point(exactly one)
        ///- `_`: Empty Space
        ///- `X`: Wall
        /// </summary>
        private List<List<char>>? _MazeArray;
        private Cell? _StartingCell;
        private Cell? _EndCell;

        public Cell StartingCell { 
            get {
                return _StartingCell ?? throw new MazeException(NO_START_POINT_FOUND);
            }  
        }
        public Cell EndCell
        {
            get
            {
                return _EndCell ?? throw new MazeException(NO_END_POINT_FOUND);
            }
        }

        /*Errors*/

        public const string EMPTY_STRING = "Maze string is empty";
        public const string MULTIPLE_STARTING_POINTS = "Multiple start points found";
        public const string MULTIPLE_ENDING_POINTS = "Multiple start points found";
        public const string NO_END_POINT_FOUND = "No end point found";
        public const string NO_START_POINT_FOUND = "No start point found";
        public const string MAZE_ARRAY_IS_EMPTY = "Maze array is empty";
        public const string MAZE_ARRAY_IS_NULL = "Maze array is null";
        public const string MAZE_TOO_SMALL = "Maze is too small";

        /// <summary>
        /// It will create a maze object from a string
        /// </summary>
        /// <param name="mazeString"></param>
        /// <exception cref="MazeException"></exception>
        public Maze(string mazeString)
        {
            this.MazeString = mazeString;
            ConstructArray(mazeString);
            if (_MazeArray == null)
            {
                throw new MazeException(MAZE_ARRAY_IS_EMPTY);
            }
        }

        /// <summary>
        /// It will translate the string to a 2D array and get the start and end points
        /// </summary>
        /// <param name="mazeString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private void ConstructArray(string mazeString)
        {
            if (string.IsNullOrEmpty(mazeString))
            {
                throw new MazeException(EMPTY_STRING);
            }
            this._MazeArray = new();
            string[] rows = mazeString.Split(new[] { WindowsNewLine, LinuxNewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Count() > 20)
            {
                throw new MazeException(MAZE_TOO_LARGE());
            }
            if (rows.Length == 0)
            {
                throw new MazeException("Maze is too small");
            }
            int rowNumber = 0;
            int colNumber;
            foreach (string rowString in rows)
            {
                if (rowString.Count() > 20)
                {
                    throw new MazeException(MAZE_TOO_LARGE());
                }
                List<char> row = new();
                colNumber = 0;
                foreach (char c in rowString)
                {
                    ValidateCharacter(c);
                    if (c == START)
                    {
                        if (_StartingCell != default)
                        {
                            throw new MazeException(MULTIPLE_STARTING_POINTS);
                        }
                        _StartingCell = new Cell(rowNumber, colNumber, IsEmptySpace: false);
                    }
                    if (c == END)
                    {
                        if (_EndCell != default)
                        {
                            throw new MazeException(MULTIPLE_ENDING_POINTS);
                        }
                        _EndCell = new Cell(rowNumber, colNumber, IsEmptySpace: false);
                    }
                    row.Add(c);
                    colNumber++;
                }
                _MazeArray.Add(row);
                rowNumber++;
            }

            if (_EndCell == default)
            {
                throw new MazeException(NO_END_POINT_FOUND);
            }
            if (_StartingCell == default)
            {
                throw new MazeException(NO_START_POINT_FOUND);
            }
        }

        public static string MAZE_TOO_LARGE()
        {
            return $"Maze is too big (max={MAX_DIMENSION})";
        }

        private static void ValidateCharacter(char c)
        {
            if (c != EMPTY_SPACE && c != START && c != END && c != WALL)
            {
                throw new MazeException(Invalid_CHAR(c));
            }
        }

        public static string Invalid_CHAR(char c)
        {
            return $"Invalid character {c} in maze";
        }

        public char GetCharacter(int x, int y)
        {
            if (_MazeArray == null)
            {
                throw new MazeException(MAZE_ARRAY_IS_NULL);
            }
            return _MazeArray[x][y];
        }
        public char this[int x, int y]
        {
            get
            {
                if (_MazeArray == null)
                {
                    throw new MazeException(MAZE_ARRAY_IS_NULL);
                }
                return _MazeArray[x][y];
            }
        }

        /// <summary>
        /// Get the number of rows in the maze
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MazeException"></exception>
        public int GetNumberRows()
        {
            if (_MazeArray == null)
            {
                throw new MazeException(MAZE_ARRAY_IS_NULL);
            }
            return _MazeArray.Count;
        }

        /// <summary>
        /// Get the number of columns in the row x
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MazeException"></exception>
        public int GetNumberColumnsInRow(int x)
        {
            return this[x];
        }

        /// <summary>
        /// Get the number of columns in the row x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <exception cref="MazeException"></exception>
        public int this[int x]
        {
            get
            {
                if (_MazeArray == null)
                {
                    throw new MazeException(MAZE_ARRAY_IS_NULL);
                }
                return _MazeArray[x].Count;
            }
        }
    }
}
