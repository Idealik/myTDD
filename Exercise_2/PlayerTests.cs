using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Moq;

namespace Exercise_2
{
    //// 2.1

    //[TestClass]
    //public class WhenBuyChips
    //{
    //    [TestMethod]
    //    public void Player_Chips()
    //    {
    //        int amount = new Random().Next(0, int.MaxValue);
    //        Chip chips = new Chip(amount);
    //        Player player1 = new Player();

    //        player1.Buy(chips);

    //        Assert.AreEqual(true, player1.Has(chips));
    //    }
    //}

    //// 2.2 через интерфейс
    //public class PlayerTests
    //{
    //    [TestMethod]
    //    public void Play_Winner_SouldGetMoreChips()
    //    {
    //        Player player = new Player();
    //        RollDiceGame game = new RollDiceGame();
    //        Chip chip = new Chip(1);
    //        player.Join(game);

    //        var dice = new DiceFake();
    //        dice.luckyNumber = 5;
    //        Bet bet = new Bet(chip, 5);
    //        player.Bet(bet);

    //        game.Play(dice);

    //        Assert.AreEqual(1 * 6, player.AvailableChips.Amount);
    //    }
    //}

    //public class DiceFake : Idice
    //{
    //    public int luckyNumber { get; set; }

    //    public int GetLuckyScore()
    //    {
    //        return luckyNumber;
    //    }
    //}


    [TestClass]
    public class PlayerTests
    {
        //    [TestMethod]
        //    public void Play_Winner_SouldGetMoreChips()
        //    {
        //        Player player = new Player();
        //        RollDiceGame game = new RollDiceGame();
        //        player.Join(game);
        //        Chip chip = new Chip(1);
        //        Bet bet = new Bet(chip, 5);
        //        player.Bet(bet);

        //var dice = new Mock<Dice>();
        //dice.Setup(_ => _.GetLuckyScore()).Returns(5); //stub

        //        game.Play(dice.Object);

        //        Assert.AreEqual(1 * 6, player.AvailableChips.Amount);


            //DCL

        [TestMethod]
        public void Player_Join_IsInGame()
        {
            var game = Create
                .Game()
                .Please();

            var player = Create
                .Player()
                .InGame(game)
                .Please();

            Assert.AreEqual(true, player.IsInGame);
        }

        [TestMethod]
        public void Player_Leave_IsNotInGame()
        {
            var game = Create
                .Game()
                .Please();

            var player = Create
                .Player()
                .InGame(game)
                .LeaveGame()
                .Please();

            Assert.AreEqual(false, player.IsInGame);
        }

        [TestMethod]
        public void Player_Leave_CantLeave()
        {
            var game = Create
                .Game()
                .Please();
            var player = Create
                .Player()
                .Please();

            try
            {
                player.LeaveGame();
            }
            catch
            {
                Assert.AreEqual(false, player.IsInGame);
            }

        }

        [TestMethod]
        public void Player_Play_OnlyOneGame()
        {
            var game = Create
              .Game()
              .Please();

            var player = Create
                .Player()
                .InGame(game)
                .Please();

            try
            {
                player.Join(game);
            }
            catch
            {
                Assert.AreEqual(true, player.IsInGame);
            }
        }

        [TestMethod]
        public void Game_Enter_NoMoreSix()
        {
            var game = Create
              .Game()
              .Please();

            var player = Create
                       .Player()
                       .Please();

            try
            {
                for (int i = 0; i < 7; i++)
                    game.AddPlayer(player);
            }
            catch
            {

                Assert.AreEqual(false, player.IsInGame);
            }
            
        }

        [TestMethod]      
            public void Player_Chips()
            {
            int amount = 1;

            var player = Create
                .Player()
                .WithChips(amount)
                .Please();                                            
            
                Assert.AreEqual(true, player.Has(new Chip(amount)));
            }
        

        [TestMethod]
        public void Play_Winner_SouldGetMoreChips()
        {

            var dice = new Mock<Dice>();
            dice.Setup(_ => _.GetLuckyScore()).Returns(5); //stub

            var game = Create
                .Game()
                .Please();


            var player = Create
                .Player()
                .InGame(game)
                .WithChips(1)
                .WhithBet(5)
                .Please();

            game.Play(dice.Object);

            Assert.AreEqual(1 * 6, player.AvailableChips.Amount);

        }


    }

    public static class Create
    {
        public static PlayerBuilder Player()
        {
            return new PlayerBuilder();
        }
        public static GameBuilder Game()
        {
            return new GameBuilder();
        }
    }

    public class PlayerBuilder
    {
        //
        private Player player = new Player();
        private Chip chip;
        private Bet bet;
        private int _score;
        //

        public Player Please()
        {
            return player;
        }

        public PlayerBuilder InGame(RollDiceGame game)
        {
            player.Join(game);
            return this;
        }

        public PlayerBuilder LeaveGame()
        {
            player.LeaveGame();
            return this;
        }

        public PlayerBuilder WithChips(int chipsAmount)
        {
            chip = new Chip(chipsAmount);
            player.Buy(chip);
            return this;
        }

        public PlayerBuilder WhithBet(int score)
        {
            _score = score;
            bet = new Bet(chip, score);
            player.Bet(bet);
            return this;
        }

    }

    public class GameBuilder
    {
        RollDiceGame game = new RollDiceGame();

        public RollDiceGame Please()
        {
            return game;
        }
    }

    

}





