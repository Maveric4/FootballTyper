﻿using FootballTyperAPI.Data;
using FootballTyperAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;

namespace FootballTyperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly FootballTyperAPIContext _context;

        public StatisticsController(FootballTyperAPIContext context)
        {
            _context = context;
        }

        // GET: api/Statistics/TopScorers/
        [HttpGet("TopScorers")]
        public IEnumerable<TopScorerDb> GetFiveTopScorers()
        {
            var topScorers = _context.TopScorers
                .GroupBy(x => x.Group)
                .Select(y => new
                {
                    Group = y.Key,
                    OrderedPlayersInGroup = y.Where(player => player.Group == y.Key)
                        .OrderByDescending(z => z.Goals)
                        .ThenByDescending(p => p.Assists)
                        .ThenByDescending(w => w.YellowCards)
                        .ThenByDescending(u => u.RedCards)
                        .Take(5)
                }).ToList();

            return topScorers.SelectMany(op => op.OrderedPlayersInGroup).ToList();
        }


        // GET: api/Statistics/TopScorers/Group/B
        [HttpGet("TopScorers/Group/{group}")]
        public IEnumerable<TopScorerDb> GetFiveTopScorersByGroup(string group)
        {
            if (group.Length == 1)
                group = "Group " + group;
            return GetFiveTopScorers().Where(x => x.Group == group).ToList();
        }
    }
}

//// GET: api/Statistics
//[HttpGet]
//public async Task<ActionResult<IEnumerable<TopScorer>>> GetTopScorer()
//{
//    return await _context.TopScorer.ToListAsync();
//}

//// GET: api/Statistics/5
//[HttpGet("{id}")]
//public async Task<ActionResult<TopScorer>> GetTopScorer(int id)
//{
//    var topScorer = await _context.TopScorer.FindAsync(id);

//    if (topScorer == null)
//    {
//        return NotFound();
//    }

//    return topScorer;
//}

//// PUT: api/Statistics/5
//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//[HttpPut("{id}")]
//public async Task<IActionResult> PutTopScorer(int id, TopScorer topScorer)
//{
//    if (id != topScorer.Id)
//    {
//        return BadRequest();
//    }

//    _context.Entry(topScorer).State = EntityState.Modified;

//    try
//    {
//        await _context.SaveChangesAsync();
//    }
//    catch (DbUpdateConcurrencyException)
//    {
//        if (!TopScorerExists(id))
//        {
//            return NotFound();
//        }
//        else
//        {
//            throw;
//        }
//    }

//    return NoContent();
//}

//// POST: api/Statistics
//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//[HttpPost]
//public async Task<ActionResult<TopScorer>> PostTopScorer(TopScorer topScorer)
//{
//    _context.TopScorer.Add(topScorer);
//    await _context.SaveChangesAsync();

//    return CreatedAtAction("GetTopScorer", new { id = topScorer.Id }, topScorer);
//}

//// DELETE: api/Statistics/5
//[HttpDelete("{id}")]
//public async Task<IActionResult> DeleteTopScorer(int id)
//{
//    var topScorer = await _context.TopScorer.FindAsync(id);
//    if (topScorer == null)
//    {
//        return NotFound();
//    }

//    _context.TopScorer.Remove(topScorer);
//    await _context.SaveChangesAsync();

//    return NoContent();
//}

//private bool TopScorerExists(int id)
//{
//    return _context.TopScorer.Any(e => e.Id == id);
//}
