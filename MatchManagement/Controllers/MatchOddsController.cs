using MatchManagement.Models;
using MatchManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchManagement.Controllers
{
    [Route("api/MatchOdds")]
    [ApiController]
    public class MatchOddsController : ControllerBase
    {
        private readonly MatchContext _context;

        public MatchOddsController(MatchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get all odds
        /// </summary>
        /// <returns> Returns a list of all odds for all matches</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchOdd>>> GetMatchOdds()
        {
            var matchOdds = await _context.MatchOdds.ToListAsync();
            return Ok(matchOdds);
        }

        /// <summary>
        /// Get a specific odd
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the specified match odd</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchOdd>> GetMatchOdds(int id)
        {
            try
            {
                var matchOdds = await _context.MatchOdds.FindAsync(id);
                if (matchOdds == null)
                    return NotFound();
                return Ok(matchOdds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create a new odd for a match. A match can only have one odd for a specifier!
        /// </summary>
        /// <param name="matchOddDTO"></param>
        /// <returns>Returns the new match odd</returns>
        [HttpPost]
        public async Task<ActionResult<MatchOdd>> CreateMatchOdds(MatchOddDTO matchOddDTO)
        {
            try
            {
                var dbMatch = await _context.Matches.FindAsync(matchOddDTO.MatchID);
                if (dbMatch == null)
                    return NotFound("Match not found!");
                if(_context.MatchOdds.Any(c => c.Match.ID == matchOddDTO.MatchID && c.Specifier.ToUpper() == matchOddDTO.Specifier.ToUpper()))
                    return BadRequest("Match Odd already exists for the specified match!");
                var newMatchOdds = new MatchOdd
                {
                    Match = dbMatch,
                    Specifier = matchOddDTO.Specifier,
                    Odd = matchOddDTO.Odd
                };
                _context.MatchOdds.Add(newMatchOdds);
                await _context.SaveChangesAsync();
                var createdMatchOdds = await _context.MatchOdds.FindAsync(newMatchOdds.ID);
                return Ok(createdMatchOdds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a specific odd for a match
        /// </summary>
        /// <param name="id"></param>
        /// <param name="matchOddDTO"></param>
        /// <returns>Returns the updated match odd</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatchOdds(int id, MatchOddDTO matchOddDTO)
        {
            try
            {
                var matchOdd = await _context.MatchOdds.FindAsync(id);
                if (matchOdd == null)
                    return NotFound("MatchOdd not found!");
                matchOdd.Specifier = matchOddDTO.Specifier;
                matchOdd.Odd = matchOddDTO.Odd;
                await _context.SaveChangesAsync();
                var updatedMatchOdds = await _context.MatchOdds.FindAsync(matchOdd.ID);
                return Ok(updatedMatchOdds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// delete a specific odd for a match
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="id"></param>
        /// <returns> Returns an ok result if the delete was succeded</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchOdds(int id)
        {
            try
            {
                var matchOdds = await _context.MatchOdds.FindAsync(id);
                if (matchOdds == null)
                    return NotFound();
                _context.MatchOdds.Remove(matchOdds);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
