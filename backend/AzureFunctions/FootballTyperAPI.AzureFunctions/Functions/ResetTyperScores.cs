using FootballTyperAPI.Common;
using FootballTyperAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace FootballTyperAPI.AzureFunctions
{
    public static class ResetTyperScores
    {
        [FunctionName("ResetTyperScores")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "ResetTyperScores")] HttpRequest req,
            [Sql("SELECT * FROM [dbo].[TyperUser]",
                CommandType = System.Data.CommandType.Text,
                ConnectionStringSetting = "SqlConnectionString")] IEnumerable<TyperUser> Users,
            [Sql("[dbo].[TyperUser]",
                CommandType = System.Data.CommandType.Text,
                ConnectionStringSetting = "SqlConnectionString")] out TyperUser[] outUsers,
            [Sql("SELECT * FROM [dbo].[Bets]",
                CommandType = System.Data.CommandType.Text,
                ConnectionStringSetting = "SqlConnectionString")] IEnumerable<Bet> Bets,
            [Sql("[dbo].[Bets]",
                CommandType = System.Data.CommandType.Text,
                ConnectionStringSetting = "SqlConnectionString")] out BetDbSave[] outBets,
            ILogger log)
        {
            log.LogInformation($"-------------------------------------------------------------------------");
            log.LogInformation($"Execution date: {DateTime.Now}");
            log.LogInformation($"Starting execution of: ResetTyperScores");

            foreach (var user in Users)
            {
                user.TotalPoints = 0;
                user.TotalCorrectWinnerBets = 0;
                user.TotalExactScoreBets = 0;
                user.TotalWrongBets = 0;
                user.LastFiveBets = "";

                var positionDict = new Dictionary<string, int>();
                var rankDict = new Dictionary<string, int>();
                foreach (var league in JsonSerializer.Deserialize<string[]>(user.LeaguesStr))
                {
                    positionDict.Add(league, 1);
                    rankDict.Add(league, 0);
                }
                user.PositionStr = JsonSerializer.Serialize(positionDict);
                user.RankStatus = JsonSerializer.Serialize(rankDict);
            }

            foreach (var bet in Bets)
            {
                bet.IsBetProcessed = false;
                bet.BetProcessedDate = DateTime.MinValue;
                bet.BetResult = null;
            }

            outUsers = Users.ToArray();
            outBets = Bets.Select(x => Mappers.MapBet(x)).ToArray();

            log.LogInformation($"Ending execution of: ResetTyperScores");
            log.LogInformation($"-------------------------------------------------------------------------");

            return new OkObjectResult(new { Ok = true });
        }
    }
}