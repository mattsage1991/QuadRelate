﻿using QuadRelate.Types;

namespace QuadRelate.Players.Vince.Helpers
{
    internal static class ScoreEvaluator
    {
        private const int _four = 1024;
        private const int _fullyOpenThree = 64;
        private const int _halfOpenThree = 32;
        private const int _middleOpenThree = 16;

        public static int GetScore(Board board, Counter colour)
        {
            var lines = LineFinder.FindAllLines(board);
            var score = 0;
            foreach (var line in lines)
            {
                var count = PatternMatcher.CountMatches(line, new[] { colour, colour, colour, colour });
                score += count * _four;
                count = PatternMatcher.CountMatches(line, new[] { Counter.Empty, colour, colour, colour, Counter.Empty });
                score += count * _fullyOpenThree;
                count = PatternMatcher.CountMatches(line, new[] { colour, colour, colour, Counter.Empty });
                score += count * _halfOpenThree;
                count = PatternMatcher.CountMatches(line, new[] { Counter.Empty, colour, colour, colour });
                score += count * _halfOpenThree;
                count = PatternMatcher.CountMatches(line, new[] { colour, Counter.Empty, colour, colour });
                score += count * _middleOpenThree;
                count = PatternMatcher.CountMatches(line, new[] { colour, colour, Counter.Empty, colour });
                score += count * _middleOpenThree;
            }
            return score;
        }
    }
}