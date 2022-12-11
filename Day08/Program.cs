using System.Text;

const string filePath = @"input.txt";

var lines = File.ReadAllLines(filePath, Encoding.UTF8);

var trees = lines.Select(x => x.ToCharArray()).Select(chars => chars.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

var height = trees.Length;
var width = trees.First().Length;

Console.WriteLine($"Part1: {Part1()}");
Console.WriteLine($"Part2: {Part2()}");

int Part1()
{
    var visibleTrees = new bool[height, width];

    int CompareVisibleHeightAndStoreResult(int rowIndex, int columnIndex, int visibleHeight)
    {
        var treeHeight = trees[rowIndex][columnIndex];

        if (visibleHeight >= treeHeight) return visibleHeight;

        visibleTrees[rowIndex, columnIndex] = true;
        visibleHeight = treeHeight;

        return visibleHeight;
    }

    void SearchVisibleFromLeftEdge()
    {
        for (var rowIndex = 0; rowIndex < height; rowIndex++)
        {
            var visibleHeight = -1;
            for (var columnIndex = 0; columnIndex < width; columnIndex++)
            {
                visibleHeight = CompareVisibleHeightAndStoreResult(rowIndex, columnIndex, visibleHeight);
            }
        }
    }
    void SearchVisibleFromRightEdge()
    {
        for (var rowIndex = 0; rowIndex < height; rowIndex++)
        {
            var visibleHeight = -1;
            for (var columnIndex = width - 1; columnIndex >= 0; columnIndex--)
            {
                visibleHeight = CompareVisibleHeightAndStoreResult(rowIndex, columnIndex, visibleHeight);
            }
        }
    }
    void SearchVisibleFromBottomEdge()
    {
        for (var columnIndex = 0; columnIndex < width; columnIndex++)
        {
            var visibleHeight = -1;
            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                visibleHeight = CompareVisibleHeightAndStoreResult(rowIndex, columnIndex, visibleHeight);
            }
        }
    }
    void SearchVisibleFromTopEdge()
    {
        for (var columnIndex = 0; columnIndex < width; columnIndex++)
        {
            var visibleHeight = -1;
            for (var rowIndex = height - 1; rowIndex >= 0; rowIndex--)
            {
                visibleHeight = CompareVisibleHeightAndStoreResult(rowIndex, columnIndex, visibleHeight);
            }
        }
    }

    SearchVisibleFromLeftEdge();
    SearchVisibleFromRightEdge();
    SearchVisibleFromBottomEdge();
    SearchVisibleFromTopEdge();

    return visibleTrees.OfType<bool>().Count(x => x);
}

long Part2()
{
    var visibleTrees = new long[height, width];

    void CalculateScenicScoreLeftwards()
    {
        for (var rowIndex = 0; rowIndex < height; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < width; columnIndex++)
            {
                var tree = trees[rowIndex][columnIndex];
                var viewDistance = 0;
                while (true)
                {
                    if (viewDistance == columnIndex)
                    {
                        visibleTrees[rowIndex, columnIndex] = viewDistance;
                        break;
                    }
                    if (tree <= trees[rowIndex][columnIndex - viewDistance -1])
                    {
                        viewDistance++;
                        visibleTrees[rowIndex, columnIndex] = viewDistance;
                        break;
                    }
                    viewDistance++;
                }
            }
        }
    }
    void CalculateScenicScoreRightwards()
    {
        for (var rowIndex = 0; rowIndex < height; rowIndex++)
        {
            for (var columnIndex = width - 1; columnIndex >= 0; columnIndex--)
            {
                var tree = trees[rowIndex][columnIndex];
                var viewDistance = 0;
                while (true)
                {
                    if (viewDistance + columnIndex == width-1)
                    {
                        visibleTrees[rowIndex, columnIndex] *= viewDistance;
                        break;
                    }
                    if (tree <= trees[rowIndex][columnIndex + viewDistance + 1])
                    {
                        viewDistance++;
                        visibleTrees[rowIndex, columnIndex] *= viewDistance;
                        break;
                    }
                    viewDistance++;
                }
            }
        }
    }

    void CalculateScenicScoreDownwards()
    {
        for (var columnIndex = 0; columnIndex < width; columnIndex++)
        {
            for (var rowIndex = height - 1; rowIndex >= 0; rowIndex--)
            {
                var tree = trees[rowIndex][columnIndex];
                var viewDistance = 0;
                while (true)
                {
                    if (viewDistance + rowIndex == height-1)
                    {
                        visibleTrees[rowIndex, columnIndex] *= viewDistance;
                        break;
                    }
                    if (tree <= trees[rowIndex + viewDistance + 1][columnIndex])
                    {
                        viewDistance++;
                        visibleTrees[rowIndex, columnIndex] *= viewDistance;
                        break;
                    }
                    viewDistance++;
                }
            }
        }
    }

    void CalculateScenicScoreUpwards()
    {
        for (var columnIndex = 0; columnIndex < width; columnIndex++)
        {
            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                var tree = trees[rowIndex][columnIndex];
                var viewDistance = 0;
                while (true)
                {
                    if (viewDistance == rowIndex)
                    {
                        visibleTrees[rowIndex, columnIndex] *= viewDistance;
                        break;
                    }
                    if (tree <= trees[rowIndex - viewDistance - 1][columnIndex])
                    {
                        viewDistance++;
                        visibleTrees[rowIndex, columnIndex] *= viewDistance;
                        break;
                    }
                    viewDistance++;
                }
            }
        }
    }

    CalculateScenicScoreLeftwards();
    CalculateScenicScoreRightwards();
    CalculateScenicScoreDownwards();
    CalculateScenicScoreUpwards();


    return visibleTrees.OfType<long>().Max();
}
