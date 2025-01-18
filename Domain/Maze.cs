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
        private readonly List<List<char>>? _MazeArray;
        private bool _HasStartPoint = false;
        private bool _HasEndPoint = false;

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
            _MazeArray = ConstructArray(mazeString);
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
        private List<List<char>>? ConstructArray(string mazeString)
        {
            if (string.IsNullOrEmpty(mazeString))
            {
                throw new MazeException(EMPTY_STRING);
            }

            List<List<char>> mazeArray = new();
            string[] rows = mazeString.Split(new[] { WindowsNewLine, LinuxNewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Count() > 20)
            {
                throw new MazeException(MAZE_TOO_LARGE());
            }
            if (rows.Length == 0)
            {
                throw new MazeException("Maze is too small");
            }
            foreach (string rowString in rows)
            {
                if (rowString.Count() > 20)
                {
                    throw new MazeException(MAZE_TOO_LARGE());
                }
                List<char> row = new();
                foreach (char c in rowString)
                {
                    ValidateCharacter(c);
                    if (c == START)
                    {
                        if (_HasStartPoint)
                        {
                            throw new MazeException(MULTIPLE_STARTING_POINTS);
                        }
                        _HasStartPoint = true;
                    }
                    if (c == END)
                    {
                        if (_HasEndPoint)
                        {
                            throw new MazeException(MULTIPLE_ENDING_POINTS);
                        }
                        _HasEndPoint = true;
                    }
                    row.Add(c);
                }
                mazeArray.Add(row);
            }

            if (!_HasEndPoint)
            {
                throw new MazeException(NO_END_POINT_FOUND);
            }
            if (!_HasStartPoint)
            {
                throw new MazeException(NO_START_POINT_FOUND);
            }

            
            return mazeArray;
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
    }
}
