﻿using FootballTyperAPI.Common;
using FootballTyperAPI.Models;
using System.Text.Json;

namespace FootballTyperAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FootballTyperAPIContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (!context.Teams.Any())
                FillTeamsTable(context);
            if (!context.Matches.Any())
                FillMatchesTable(context);
            if (!context.Bets.Any())
                FillBetsTable(context);
            if (context.TyperUser.Any(x => x.Username == "User1" || x.Username == "User2"))
                context.TyperUser.RemoveRange(context.TyperUser.Where(x => x.Username == "User1" || x.Username == "User2"));
            //FillTyperUserTable(context);
        }


        public static void CleanDb(FootballTyperAPIContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Bets.Any())
                context.RemoveRange(context.Bets);
            if (context.Matches.Any())
                context.RemoveRange(context.Matches);
            if (context.Teams.Any())
                context.RemoveRange(context.Teams);
            context.SaveChanges();
        }

        public static void FillMatchesTable(FootballTyperAPIContext context)
        {
            var matches = MatchHelper.GetAllMatches(context.Teams.ToList());
            //InitializeGroupStage(matches);
            context.AddRange(matches);
            context.SaveChanges();

        }

        private static void FillTeamsTable(FootballTyperAPIContext context)
        {
            var teams = TeamHelper.GetAllValidTeams();
            context.AddRange(teams);
            context.SaveChanges();
        }

        private static void FillBetsTable(FootballTyperAPIContext context)
        {
            var bets = new List<Bet>
            {
                new Bet()
                {
                    AwayTeamScoreBet = 2,
                    AwayTeamWin = true,
                    BetDate = DateTime.Now,
                    BettorUserName = "User1",
                    HomeAwayDrawn = false,
                    HomeTeamScoreBet = 0,
                    HomeTeamWin = false,
                    Match = context.Matches.Skip(0).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 0,
                    AwayTeamWin = false,
                    BetDate = DateTime.Now,
                    BettorUserName = "User1",
                    HomeAwayDrawn = false,
                    HomeTeamScoreBet = 2,
                    HomeTeamWin = true,
                    Match = context.Matches.Skip(1).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 2,
                    AwayTeamWin = false,
                    BetDate = DateTime.Now,
                    BettorUserName = "User1",
                    HomeAwayDrawn = true,
                    HomeTeamScoreBet = 2,
                    HomeTeamWin = false,
                    Match = context.Matches.Skip(2).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 2,
                    AwayTeamWin = false,
                    BetDate = DateTime.Now,
                    BettorUserName = "User1",
                    HomeAwayDrawn = false,
                    HomeTeamScoreBet = 3,
                    HomeTeamWin = true,
                    Match = context.Matches.Skip(3).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 2,
                    AwayTeamWin = false,
                    BetDate = DateTime.Now,
                    BettorUserName = "User1",
                    HomeAwayDrawn = false,
                    HomeTeamScoreBet = 3,
                    HomeTeamWin = true,
                    Match = context.Matches.Skip(4).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 0,
                    AwayTeamWin = false,
                    BetDate = DateTime.Now,
                    BettorUserName = "User2",
                    HomeAwayDrawn = false,
                    HomeTeamScoreBet = 2,
                    HomeTeamWin = true,
                    Match = context.Matches.Skip(0).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 2,
                    AwayTeamWin = true,
                    BetDate = DateTime.Now,
                    BettorUserName = "User2",
                    HomeAwayDrawn = false,
                    HomeTeamScoreBet = 0,
                    HomeTeamWin = false,
                    Match = context.Matches.Skip(1).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 1,
                    AwayTeamWin = false,
                    BetDate = DateTime.Now,
                    BettorUserName = "User2",
                    HomeAwayDrawn = true,
                    HomeTeamScoreBet = 1,
                    HomeTeamWin = false,
                    Match = context.Matches.Skip(2).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 3,
                    AwayTeamWin = true,
                    BetDate = DateTime.Now,
                    BettorUserName = "User2",
                    HomeAwayDrawn = false,
                    HomeTeamScoreBet = 2,
                    HomeTeamWin = false,
                    Match = context.Matches.Skip(3).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
                new Bet()
                {
                    AwayTeamScoreBet = 3,
                    AwayTeamWin = true,
                    BetDate = DateTime.Now,
                    BettorUserName = "User2",
                    HomeAwayDrawn = false,
                    HomeTeamScoreBet = 2,
                    HomeTeamWin = false,
                    Match = context.Matches.Skip(4).Take(1).FirstOrDefault(),
                    PointsFactor = 1,
                },
            };
            context.AddRange(bets);
            context.SaveChanges();
        }
        private static void FillTyperUserTable(FootballTyperAPIContext context)
        {
            var defaultLeagues = new string[] { "main", "clownLeague", "randomLeague" };
            var users = new List<TyperUser>()
            {
                new TyperUser
                {
                    Username = "User1",
                    Email = "User1@gmail.com",
                    FullName = "User1 FullName",
                    PasswordHash = "$2a$11$K3jhOqkDOoCpcYsoVyBluOHTibnNx24bZxl39OUSF0spmSJeuUrF.",
                    ImgLink = "https://footballtypersa.blob.core.windows.net/imgs/User1.jpg",
                    LeaguesStr = JsonSerializer.Serialize(defaultLeagues.Take(2))
                },
                new TyperUser
                {
                    Username = "User2",
                    Email = "User2@gmail.com",
                    FullName = "User2 FullName",
                    PasswordHash = "$2a$11$K3jhOqkDOoCpcYsoVyBluOHTibnNx24bZxl39OUSF0spmSJeuUrF.",
                    ImgLink = "https://footballtypersa.blob.core.windows.net/imgs/User2.jpg",
                    LeaguesStr = JsonSerializer.Serialize(defaultLeagues.Take(3))
                }
            };
            context.AddRange(users);
            context.SaveChanges();
        }

        private static void InitializeGroupStage(List<Match> matches)
        {
            foreach (var match in matches.Where(x => x.RoundNumber <= 3))
            {
                match.AwayTeamScore = Random.Shared.Next(0, 5);
                match.HomeTeamScore = Random.Shared.Next(0, 5);
                match.Date = DateTime.Now;
            }
        }
    }
}
