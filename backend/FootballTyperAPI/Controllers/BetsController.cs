﻿using FootballTyperAPI.Data;
using FootballTyperAPI.Helpers;
using FootballTyperAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballTyperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly FootballTyperAPIContext _context;

        public BetsController(FootballTyperAPIContext context)
        {
            _context = context;
        }

        // GET: api/Bets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bet>>> GetAllBets()
        {
            return (await _context.GetAllBets()).ToList();
        }

        // GET: api/Bets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bet>> GetBet(int id)
        {
            var bet = (await _context.GetAllBets()).FirstOrDefault(x => x.Id == id);

            if (bet == null)
            {
                return NotFound();
            }

            return bet;
        }

        // PUT: api/Bets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBet(int id, Bet bet)
        {
            if (id != bet.Id)
            {
                return BadRequest();
            }

            _context.Entry(bet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bet>> PostBet(Bet bet)
        {
            _context.Bets.Add(bet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBet", new { id = bet.Id }, bet);
        }

        // DELETE: api/Bets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBet(int id)
        {
            var bet = await _context.Bets.FindAsync(id);
            if (bet == null)
            {
                return NotFound();
            }

            _context.Bets.Remove(bet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BetExists(int id)
        {
            return _context.Bets.Any(e => e.Id == id);
        }

        // GET: api/Bets/User/{id}
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Bet>>> GetBetByUserId(int userId)
        {
            var user = _context.TyperUser.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return NotFound();

            var userBets = (await _context.GetAllBets()).Where(x => x.BettorUserName == user.Username).ToList();

            if (!userBets.Any())
            {
                return NotFound();
            }

            return userBets;
        }

        // GET: api/Bets/User/{username}
        [HttpGet("User/{username}")]
        public async Task<ActionResult<IEnumerable<Bet>>> GetBetByUsername(string username)
        {
            var userBets = (await _context.GetAllBets()).Where(x => x.BettorUserName == username).ToList();

            if (!userBets.Any())
            {
                return NotFound();
            }

            return userBets;
        }

        // GET: api/Bets/User/LastFive/{username}
        [HttpGet("User/LastFive/{username}")]
        public async Task<List<ScoreEnum>> GetLastFiveBetScoresByUsername(string username)
        {
            var lastFiveBets = (await _context.GetAllBets())
                .Where(x => x.BettorUserName == username && x.Match.Date <= DateTime.Now)
                .OrderByDescending(t => t.Match.Date)
                .Take(5)
                .ToList();
            return MatchHelper.ConvertBetsToScoreEnums(lastFiveBets);
        }
    }
}
