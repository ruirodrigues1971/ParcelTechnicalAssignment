using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain
{
    public class Maze
    {


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

        public Cell? StartingCell
        {
            get
            {
                return _StartingCell;
            }
        }
        public Cell? EndCell
        {
            get
            {
                return _EndCell;
            }
        }

        /// <summary>
        /// To avoid any work in the maze if don't have a start and end point
        /// </summary>
        public bool IsPossibleToSolve
        {
            get
            {
                return _StartingCell != default && _EndCell != default;
            }
        }

        /// <summary>
        /// Get reason why the maze is not possible to solve
        /// </summary>
        public string PossibleReasonForNotToSolve
        {
            get
            {
                string reason = string.Empty;
                if (_StartingCell == default)
                {
                    reason += NO_START_POINT_FOUND_ERROR;
                }
                if (_EndCell == default)
                {
                    reason += string.IsNullOrEmpty(reason) ? NO_END_POINT_FOUND_ERROR : ";" + NO_END_POINT_FOUND_ERROR;
                }
                return reason;
            }
        }

        /*Errors*/

        public const string EMPTY_STRING_ERROR = "Maze string is empty";
        public const string MULTIPLE_STARTING_POINTS_ERROR = "Multiple start points found";
        public const string MULTIPLE_ENDING_POINTS_ERROR = "Multiple start points found";
        public const string NO_END_POINT_FOUND_ERROR = "No end point found";
        public const string NO_START_POINT_FOUND_ERROR = "No start point found";
        public const string MAZE_ARRAY_IS_EMPTY_ERROR = "Maze array is empty";
        public const string MAZE_ARRAY_IS_NULL_ERROR = "Maze array is null";
        private const string MAZE_TOO_SMALL_ERROR = "Maze is too small";
        private const string MAZE_TOO_BIG_ERROR = "Maze is too big";



        private const int MAX_DIMENSITION = 20;

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
                throw new MazeException(MAZE_ARRAY_IS_EMPTY_ERROR);
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
                throw new MazeException(EMPTY_STRING_ERROR);
            }
            this._MazeArray = [];
            string[] rows = mazeString.Split([WindowsNewLine, LinuxNewLine], StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length > MAX_DIMENSITION)
            {
                throw new MazeException(MAZE_TOO_LARGE_ERROR_MESSAGE());
            }
            if (rows.Length == 0)
            {
                throw new MazeException(MAZE_TOO_SMALL_ERROR);
            }
            int rowNumber = 0;
            int colNumber;
            foreach (string rowString in rows)
            {
                if (rowString.Length > 20)
                {
                    throw new MazeException(MAZE_TOO_LARGE_ERROR_MESSAGE());
                }
                List<char> row = [];
                colNumber = 0;
                foreach (char c in rowString)
                {
                    ValidateCharacter(c);
                    if (c == Cell.START)
                    {
                        if (_StartingCell != default)
                        {
                            throw new MazeException(MULTIPLE_STARTING_POINTS_ERROR);
                        }
                        _StartingCell = new Cell(rowNumber, colNumber, IsEmptySpace: false);
                    }
                    if (c == Cell.END)
                    {
                        if (_EndCell != default)
                        {
                            throw new MazeException(MULTIPLE_ENDING_POINTS_ERROR);
                        }
                        _EndCell = new Cell(rowNumber, colNumber, IsEmptySpace: false);
                    }
                    row.Add(c);
                    colNumber++;
                }
                _MazeArray.Add(row);
                rowNumber++;
            }
        }

        public static string MAZE_TOO_LARGE_ERROR_MESSAGE()
        {
            return $"{MAZE_TOO_BIG_ERROR} (max={MAX_DIMENSION})";
        }

        private static void ValidateCharacter(char c)
        {
            if (c != Cell.EMPTY_SPACE && c != Cell.START && c != Cell.END && c != Cell.WALL)
            {
                throw new MazeException(INVALID_CHAR_ERROR_MESSAGE(c));
            }
        }

        public static string INVALID_CHAR_ERROR_MESSAGE(char c)
        {
            return $"Invalid character {c} in maze";
        }

        public char GetCharacter(int x, int y)
        {
            if (_MazeArray == null)
            {
                throw new MazeException(MAZE_ARRAY_IS_NULL_ERROR);
            }
            return _MazeArray[x][y];
        }
        public char this[int x, int y]
        {
            get
            {
                if (_MazeArray == null)
                {
                    throw new MazeException(MAZE_ARRAY_IS_NULL_ERROR);
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
                throw new MazeException(MAZE_ARRAY_IS_NULL_ERROR);
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
                    throw new MazeException(MAZE_ARRAY_IS_NULL_ERROR);
                }
                return _MazeArray[x].Count;
            }
        }
    }
}
