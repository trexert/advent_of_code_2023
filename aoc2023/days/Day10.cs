using System.Data;
using System.Diagnostics;
using static Day10.Direction;

public class Day10
{
    public static string Part1()
    {
        var (grid, (row, col)) = ParseInput();
        var start = grid[row][col];
        var current = start;
        var fromDir = North;
        int loopLength = 1;
        foreach (var dir in new List<Direction>([North, South, East, West]))
        {
            var (rowDiff, colDiff) = Move(dir);
            Direction requiredLink = DirToFrom(dir);
            var possibleMove = grid[row + rowDiff][col + colDiff];

            if (possibleMove.connections.Contains(requiredLink))
            {
                current = possibleMove;
                fromDir = requiredLink;
                (row, col) = (row + rowDiff, col + colDiff);
                break;
            }
        }

        while (current != start)
        {
            var dir = current.OtherConnection(fromDir);
            var (rowDiff, colDiff) = Move(dir);
            fromDir = DirToFrom(dir);
            (row, col) = (row + rowDiff, col + colDiff);
            current = grid[row][col];
            loopLength++;
        }

        return (loopLength / 2).ToString();
    }

    private static (List<List<Pipe>>, (int, int)) ParseInput()
    {
        (int, int)? sPos = null;
        List<List<Pipe>> grid = [];
        using (var sr = new StreamReader("inputs/Day10.txt"))
        {
            int row = 0;
            for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                List<Pipe> pipeRow = [];
                int col = 0;
                foreach (var c in line)
                {
                    Pipe pipe = c switch
                    {
                        '|' => new([North, South]),
                        '-' => new([East, West]),
                        'L' => new([North, East]),
                        'J' => new([North, West]),
                        '7' => new([South, West]),
                        'F' => new([South, East]),
                        '.' => new([]),
                        'S' => new([North, South, East, West]),
                        _ => throw new Exception($"Unexpected character {c}"),
                    };
                    if (c == 'S')
                    {
                        sPos = (row, col);
                    }
                    pipeRow.Add(pipe);

                    col++;
                }
                grid.Add(pipeRow);

                row++;
            }
        }
        return (grid, sPos.Value);
    }

    private class Pipe(Direction[] connections)
    {
        public Direction[] connections = connections;

        public Direction OtherConnection(Direction from)
        {
            Debug.Assert(connections.Contains(from));
            return connections[0] == from ? connections[1] : connections[0];
        }
    }

    private static (int, int) Move(Direction dir)
    {
        return dir switch
        {
            North => (-1, 0),
            South => (1, 0),
            East => (0, 1),
            West => (0, -1),
        };
    }

    private static Direction DirToFrom(Direction dir)
    {
        return dir switch
        {
            North => South,
            South => North,
            East => West,
            West => East,
        };
    }

    internal enum Direction
    {
        North,
        South,
        East,
        West,
    }
}