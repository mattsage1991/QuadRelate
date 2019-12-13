﻿using QuadRelate.Contracts;
using QuadRelate.Types;
using System;
using System.Collections.Generic;

namespace QuadRelate.Models
{
    public class GamePlayer : IGamePlayer
    {
        private readonly IBoardDrawer _boardDrawer;
        private readonly IMessageWriter _messageWriter;

        public GamePlayer(IBoardDrawer boardDrawer, IMessageWriter messageWriter)
        {
            _boardDrawer = boardDrawer;
            _messageWriter = messageWriter;
        }

        private Score PlayGame(IPlayer playerOne, IPlayer playerTwo)
        {
            var board = new Board();
            var score = new Score();
            var moves = new List<int>();

            board.Fill(Counter.Empty);

            while (!board.IsGameOver())
            {
                var move = playerOne.NextMove(board.Clone(), Counter.Yellow);
                moves.Add(move);
                board.PlaceCounter(move, Counter.Yellow);

                if (board.IsGameOver())
                {
                    var result = new GameResult(Counter.Yellow, moves);
                    playerOne.GameOver(result);
                    playerTwo.GameOver(result);
                    score.PlayerOne = 1;

                    return score;
                }

                move = playerTwo.NextMove(board.Clone(), Counter.Red);
                moves.Add(move);
                board.PlaceCounter(move, Counter.Red);

                if (board.IsGameOver())
                {
                    GameResult result;
                    var isWin = board.DoesWinnerExist();
                    if (isWin)
                    {
                        score.PlayerTwo = 1;
                        result = new GameResult(Counter.Red, moves);
                    }
                    else
                    {
                        score.PlayerOne = 0.5f;
                        score.PlayerTwo = 0.5f;
                        result = new GameResult(Counter.Empty, moves);
                    }
                    playerOne.GameOver(result);
                    playerTwo.GameOver(result);

                    return score;
                }
            }

            throw new InvalidOperationException("Game not handled");
        }

        public Score PlayOneGame(IPlayer playerOne, IPlayer playerTwo)
        {
            var board = new Board();
            var score = new Score();
            var moves = new List<int>();

            board.Fill(Counter.Empty);
            _boardDrawer.DrawBoard(board);
            _messageWriter.WriteLine($"'{playerOne.Name}' vs '{playerTwo.Name}'");

            while (!board.IsGameOver())
            {
                var move = playerOne.NextMove(board.Clone(), Counter.Yellow);
                moves.Add(move);
                board.PlaceCounter(move, Counter.Yellow);
                _boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var message = $"YELLOW WINS! ('{playerOne.Name}')";
                    _messageWriter.WriteLine(message);

                    score.PlayerOne = 1;
                    var result = new GameResult(Counter.Yellow, moves);
                    playerOne.GameOver(result);
                    playerTwo.GameOver(result);

                    return score;
                }

                move = playerTwo.NextMove(board.Clone(), Counter.Red);
                moves.Add(move);
                board.PlaceCounter(move, Counter.Red);
                _boardDrawer.DrawBoard(board);

                if (board.IsGameOver())
                {
                    var isWin = board.DoesWinnerExist();

                    var message = isWin ? $"RED WINS! ('{playerTwo.Name}')" : "DRAW!";
                    _messageWriter.WriteLine(message);

                    if (isWin)
                    {
                        score.PlayerTwo = 1;
                        var result = new GameResult(Counter.Red, moves);
                        playerOne.GameOver(result);
                        playerTwo.GameOver(result);
                    }
                    else
                    {
                        score.PlayerOne = 0.5f;
                        score.PlayerTwo = 0.5f;
                        var result = new GameResult(Counter.Empty, moves);
                        playerOne.GameOver(result);
                        playerTwo.GameOver(result);
                    }

                    return score;
                }
            }

            throw new InvalidOperationException("Game not handled");
        }

        public Score PlayMultipleGames(IPlayer playerOne, IPlayer playerTwo, int numberOfGames)
        {
            var totalScore = new Score();
            var yellowPlayer = playerOne;
            var redPlayer = playerTwo;

            for (var i = 0; i < numberOfGames; i++)
            {
                var gameScore = PlayGame(yellowPlayer, redPlayer);

                if (i % 2 == 0)
                {
                    totalScore.PlayerOne += gameScore.PlayerOne;
                    totalScore.PlayerTwo += gameScore.PlayerTwo;
                }
                else
                {
                    totalScore.PlayerOne += gameScore.PlayerTwo;
                    totalScore.PlayerTwo += gameScore.PlayerOne;
                }

                var tempPlayer = yellowPlayer;
                yellowPlayer = redPlayer;
                redPlayer = tempPlayer;

                if (i % 10 == 0)
                    _messageWriter.Write(".");
            }

            _messageWriter.WriteLine(".");

            return totalScore;
        }
    }
}
