using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;
using static System.Environment;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/20</summary>
    public class Day20
    {
        [Result(27798062994017L)]
        public static long GetAnswer1(string input)
        {
            var tiles = ParseTiles(input);

            var allEdgesWithTileId = tiles.SelectMany(tile => tile.NormalizedEdges.Select(e => (tile, edge: e)));

            var matchingEdges = allEdgesWithTileId.GroupBy(t => t.edge, t => t.tile);

            var groupsWithSingleEdge = matchingEdges.Where(g => g.Count() == 1);

            var nonMatchedEdgesPerTile = groupsWithSingleEdge.GroupBy(m => m.Single(), m=> m.Key);

            var cornerTiles = nonMatchedEdgesPerTile.Where(g => g.Count() == 2).Select(g => (long)g.Key.id);

            return cornerTiles.Aggregate((a, b) => a * b);
        }

        public static Tile[] ParseTiles(string input) =>
            input.Split(NewLine + NewLine).Select(ParseTile).ToArray();

        public record Tile(int id, string[] edges, string[] inverseEdges)
        {
            public string[] NormalizedEdges = edges.Zip(inverseEdges)
                .Select(t => t.First.CompareTo(t.Second) < 0 ? t.First : t.Second).ToArray();
        }

        public static Tile ParseTile(string chunk)
        {
            var lines = chunk.Split(NewLine);
            var tileNumber = int.Parse(lines[0][5..^1]);
            var grid = lines[1..];

            var edges = new char[][] 
            {
                grid[0].ToCharArray(),
                Range(0, 10).Select(i=> grid[i][9]).ToArray(),
                grid[9].Reverse().ToArray(),
                Range(0, 10).Select(i => grid[9 - i][0]).ToArray()
            };

            var edgeStrings = edges.Select(e => new string(e)).ToArray();
            var inverseEdgeStrings = edges.Select(e => new string(e.Reverse().ToArray())).ToArray();

            return new Tile(tileNumber, edgeStrings, inverseEdgeStrings);
        }
    }
}