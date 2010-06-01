namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Drawing;
    using System.Text;

    /// <summary>
    /// Su Doku (Japanese meaning number place) is the name given to a popular puzzle concept. Its origin is unclear, but credit must be attributed to Leonhard Euler who invented a similar, and much more difficult, puzzle idea called Latin Squares. The objective of Su Doku puzzles, however, is to replace the blanks (or zeros) in a 9 by 9 grid in such that each row, column, and 3 by 3 box contains each of the digits 1 to 9. Below is an example of a typical starting puzzle grid and its solution grid.
    /// 
    /// +-------+-------+-------+
    /// | 0 0 3 | 0 2 0 | 6 0 0 |
    /// | 9 0 0 | 3 0 5 | 0 0 1 |
    /// | 0 0 1 | 8 0 6 | 4 0 0 |
    /// +-------+-------+-------+
    /// | 0 0 8 | 1 0 2 | 9 0 0 |
    /// | 7 0 0 | 0 0 0 | 0 0 8 |
    /// | 0 0 6 | 7 0 8 | 2 0 0 |
    /// +-------+-------+-------+
    /// | 0 0 2 | 6 0 9 | 5 0 0 |
    /// | 8 0 0 | 2 0 3 | 0 0 9 |
    /// | 0 0 5 | 0 1 0 | 3 0 0 |
    /// +-------+-------+-------+
    /// 
    /// A well constructed Su Doku puzzle has a unique solution and can be solved by logic, although it may be necessary to employ "guess and test" methods in order to eliminate options (there is much contested opinion over this). The complexity of the search determines the difficulty of the puzzle; the example above is considered easy because it can be solved by straight forward direct deduction.
    /// 
    /// The 6K text file, sudoku.txt, contains fifty different Su Doku puzzles ranging in difficulty, but all with unique solutions (the first puzzle in the file is the example above).
    /// 
    /// By solving all fifty puzzles find the sum of the 3-digit numbers found in the top left corner of each solution grid; for example, 483 is the 3-digit number found in the top left corner of the solution grid above.
    /// </summary>
    [ProblemResource("sudoku")]
    [Result(Name = "result", Expected = "")]
    public class Problem096 : Problem
    {
        public override string Solve(string resource)
        {
            var lines = (from l in resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                         select l).ToArray();

            for (int i = 0; i < lines.Length; i += 10)
            {
                var puzzle = new SudokuPuzzle();

                for (int l = 0; l < 9; l++)
                {
                    var line = lines[i + l + 1];
                    for (int j = 0; j < line.Length; j++)
                    {
                        int value = line[j] - '0';
                        if (value != 0)
                        {
                            puzzle[j, l].Set(value);
                        }
                    }
                }

                puzzle.Solve();
            }

            return "";
        }

        private class SudokuPuzzle
        {
            private SudokuCell[] cells;

            public SudokuPuzzle()
            {
                this.cells = new SudokuCell[81];
                for (int i = 0; i < this.cells.Length; i++)
                {
                    this.cells[i] = new SudokuCell();
                }
            }

            public SudokuCell this[int x, int y]
            {
                get
                {
                    if (x >= 9)
                    {
                        throw new ArgumentOutOfRangeException("x");
                    }

                    if (y >= 9)
                    {
                        throw new ArgumentOutOfRangeException("y");
                    }

                    return this.cells[x + 9 * y];
                }
            }

            public SudokuCell this[Point point]
            {
                get
                {
                    return this[point.X, point.Y];
                }
            }

            public void Solve()
            {
                var unsolved = from c in cells
                               where c.Candidates.Count > 1
                               select c;

                while (unsolved.Any())
                {
                    var removed = 0;
                    
                    removed = RemoveConflictingCandidates();

                    if (removed > 0)
                    {
                        continue;
                    }

                    removed = SolveHiddenSingles();

                    if (removed > 0)
                    {
                        continue;
                    }

                    break;
                }
            }

            private int RemoveConflictingCandidates()
            {
                var removed = 0;
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        var cell = this[x, y];

                        if (cell.Candidates.Count > 1)
                        {
                            continue;
                        }

                        var cellValue = cell.Candidates[0];

                        for (var u = 0; u < 9; u++)
                        {
                            var targetCell = this[u, y];
                            if (u != x && targetCell.Candidates.Contains(cellValue))
                            {
                                targetCell.RemoveCandidate(cellValue);
                                removed++;
                            }
                        }
                        
                        for (var v = 0; v < 9; v++)
                        {
                            var targetCell = this[x, v];
                            if (v != y && targetCell.Candidates.Contains(cellValue))
                            {
                                targetCell.RemoveCandidate(cellValue);
                                removed++;
                            }
                        }

                        for (int u = x - (x % 3); u < x - (x % 3) + 3; u++)
                        {
                            for (int v = y - (y % 3); v < y - (y % 3) + 3; v++)
                            {
                                var targetCell = this[u, v];
                                if (u != x && v != y && targetCell.Candidates.Contains(cellValue))
                                {
                                    targetCell.RemoveCandidate(cellValue);
                                    removed++;
                                }
                            }
                        }
                    }
                }

                return removed;
            }

            private int SolveHiddenSingles()
            {
                for (int c = 1; c <= 9; c++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        Point? singlePoint = null;

                        for (int x = 0; x < 9; x++)
                        {
                            var cell = this[x, y];

                            if (cell.Candidates.Count > 1 && cell.Candidates.Contains(c))
                            {
                                if (!singlePoint.HasValue)
                                {
                                    singlePoint = new Point(x, y);
                                }
                                else
                                {
                                    singlePoint = null;
                                    break;
                                }
                            }
                        }

                        if (singlePoint.HasValue)
                        {
                            this[singlePoint.Value].Set(c);
                            return 1;
                        }
                    }

                    for (int x = 0; x < 9; x++)
                    {
                        Point? singlePoint = null;

                        for (int y = 0; y < 9; y++)
                        {
                            var cell = this[x, y];

                            if (cell.Candidates.Count > 1 && cell.Candidates.Contains(c))
                            {
                                if (!singlePoint.HasValue)
                                {
                                    singlePoint = new Point(x, y);
                                }
                                else
                                {
                                    singlePoint = null;
                                    break;
                                }
                            }
                        }

                        if (singlePoint.HasValue)
                        {
                            this[singlePoint.Value].Set(c);
                            return 1;
                        }
                    }

                    for (int q = 0; q < 9; q++)
                    {
                        Point? singlePoint = null;

                        for (int i = 0; i < 9; i++)
                        {
                            var x = (q % 3) * 3 + i % 3;
                            var y = (q / 3) * 3 + i / 3;

                            var cell = this[x, y];

                            if (cell.Candidates.Count > 1 && cell.Candidates.Contains(c))
                            {
                                if (!singlePoint.HasValue)
                                {
                                    singlePoint = new Point(x, y);
                                }
                                else
                                {
                                    singlePoint = null;
                                    break;
                                }
                            }
                        }

                        if (singlePoint.HasValue)
                        {
                            this[singlePoint.Value].Set(c);
                            return 1;
                        }
                    }
                }

                return 0;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("╔═══╤═══╤═══╦═══╤═══╤═══╦═══╤═══╤═══╗");
                for (int y = 0; y < 9; y++)
                {
                    if (y != 0)
                    {
                        if (y % 3 == 0)
                        {
                            sb.AppendLine("╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣");
                        }
                        else
                        {
                            sb.AppendLine("╟───┼───┼───╫───┼───┼───╫───┼───┼───╢");
                        }
                    }

                    for (int q = 1; q < 9; q += 3)
                    {
                        for (int x = 0; x < 9; x++)
                        {
                            sb.Append(x % 3 == 0 ? "║" : "│");

                            for (int c = 0; c < 3; c++)
                            {
                                sb.Append(this[x, y].Candidates.Contains(q + c) ? (q + c).ToString() : " ");
                            }
                        }
                        sb.AppendLine("║");
                    }
                }
                sb.AppendLine("╚═══╧═══╧═══╩═══╧═══╧═══╩═══╧═══╧═══╝");

                return sb.ToString();
            }

            public class SudokuCell
            {
                private List<int> candidates = new List<int>(Enumerable.Range(1, 9));

                public IList<int> Candidates
                {
                    get
                    {
                        return this.candidates.AsReadOnly();
                    }
                }

                public void RemoveCandidate(int candidate)
                {
                    this.candidates.Remove(candidate);
                }

                public void Set(int value)
                {
                    this.candidates.Clear();
                    this.candidates.Add(value);
                }
            }
        }
    }
}
