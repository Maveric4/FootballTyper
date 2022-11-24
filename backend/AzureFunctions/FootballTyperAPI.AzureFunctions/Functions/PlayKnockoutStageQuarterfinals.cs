using FootballTyperAPI.AzureFunctions;
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

namespace Company.Function
{
    public static class PlayKnockoutStageQuarterfinals
    {

        [FunctionName("PlayKnockoutStageQuarterfinals")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "PlayKnockoutStageQuarterfinals")] HttpRequest req,
            [Sql("SELECT * FROM [dbo].[Teams]",
                CommandType = System.Data.CommandType.Text,
                ConnectionStringSetting = "SqlConnectionString")] IEnumerable<Team> Teams,
            [Sql("SELECT * FROM [dbo].[Match] WHERE RoundNumber = 5 ORDER BY Id ASC",
                CommandType = System.Data.CommandType.Text,
                ConnectionStringSetting = "SqlConnectionString")] IEnumerable<Match> QuarterfinalMatches,
            [Sql("[dbo].[Match]",
                CommandType = System.Data.CommandType.Text,
                ConnectionStringSetting = "SqlConnectionString")] out MatchDbSave[] outMatches,
            ILogger log)
        {
            if (Teams.Any() && QuarterfinalMatches.Any())
            {
                foreach (var match in QuarterfinalMatches)
                {
                    match.AwayTeamScore = Random.Shared.Next(0, 2);
                    match.HomeTeamScore = Random.Shared.Next(3, 5);
                    match.Date = DateTime.Now;
                    log.LogInformation($"ID of match played: {match.Id}. Result: [{match.AwayTeamId}] {match.AwayTeamScore} - {match.HomeTeamScore} [{match.HomeTeamId}]");
                }
                outMatches = QuarterfinalMatches.Select(x => Mappers.MapMatchDbSave(x)).ToArray();
            }
            else
            {
                log.LogInformation("Cannot play Quarterfinal matches. Teams or Matches table is empty");
                outMatches = null;
            }
            return new OkObjectResult(new { Ok = true });
        }
    }
}
