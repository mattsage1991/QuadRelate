﻿using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;
using QuadRelate.IocContainer;
using QuadRelate.Contracts;

namespace QuadRelateApp
{
    class Program
    {
        static void Main()
        {
            var messageWriter = AppContainer.Resolve<IMessageWriter>();
            var gamePlayer = AppContainer.Resolve<IGamePlayer>();
            var factory = AppContainer.Resolve<IPlayerFactory>();
            var playerOne = factory.CreatePlayer(nameof(CpuPlayerVince));
            var playerTwo = factory.CreatePlayer(nameof(CpuPlayer02));

            messageWriter.WriteMessage($"{playerOne.Name} vs {playerTwo.Name}\n");

            var score = gamePlayer.PlayMultipleGames(playerOne, playerTwo, 1000);
            //var score = gamePlayer.PlayOneGame(playerOne, playerTwo);

            messageWriter.WriteMessage($"\n\n{playerOne.Name} {score.PlayerOne} : {score.PlayerTwo} {playerTwo.Name}\n\n");
        }
    }
}