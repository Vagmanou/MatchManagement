using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Globalization;
using MatchManagement.Models.DTO;

namespace MatchManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly MatchContext _context;

        public MatchesController(MatchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all matches
        /// </summary>
        /// <returns> Returns a list with all matches</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            var matches = await _context.Matches.ToListAsync();
            return Ok(matches);
        }

        /// <summary>
        /// Get a match by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the specified match</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            try
            {
                var match = await _context.Matches.FirstOrDefaultAsync(c => c.ID == id);
                if (match == null)
                    return NotFound();
                return Ok(match);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create a new match
        /// </summary>
        /// <param name="match"></param>
        /// <returns> Returns the new match</returns>
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(MatchDTO matchDTO)
        {
            try
            {
                if (DateTime.TryParseExact(matchDTO.MatchTime, "HH:mm", null, DateTimeStyles.None, out var time))
                {
                    var match = new Match
                    {
                        MatchDate = matchDTO.MatchDate,
                        MatchTime = matchDTO.MatchTime,
                        Description = matchDTO.Description,
                        Sport = matchDTO.Sport,
                        TeamA = matchDTO.TeamA,
                        TeamB = matchDTO.TeamB
                    };
                    _context.Matches.Add(match);
                    await _context.SaveChangesAsync();
                    var createdMatch = await _context.Matches.FirstOrDefaultAsync(c => c.ID == match.ID);
                    return Ok(createdMatch);
                }
                return BadRequest("Invalid time format! Expected format HH:mm");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a match
        /// </summary>
        /// <param name="id"></param>
        /// <param name="match"></param>
        /// <returns>Returns the updated match</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, MatchDTO matchDTO)
        {
            try
            {
                if (DateTime.TryParseExact(matchDTO.MatchTime, "HH:mm", null, DateTimeStyles.None, out var time))
                {
                    var matchDb = await _context.Matches.FindAsync(id);
                    if (matchDb == null)
                        return NotFound("Match not found!");
                    matchDb.MatchDate = matchDTO.MatchDate;
                    matchDb.MatchTime = matchDTO.MatchTime;
                    matchDb.Description = matchDTO.Description;
                    matchDb.Sport = matchDTO.Sport;
                    matchDb.TeamA = matchDTO.TeamA;
                    matchDb.TeamB = matchDTO.TeamB;
                    await _context.SaveChangesAsync();
                    var updatedMatch = await _context.Matches.FirstOrDefaultAsync(c => c.ID == matchDb.ID);
                    return Ok(updatedMatch);
                }
                return BadRequest("Invalid time format! Expected format HH:mm");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a match
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an ok result if the delete succeded</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            try
            {
                var match = await _context.Matches.FindAsync(id);
                if (match == null)
                    return NotFound();
                _context.Matches.Remove(match);
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
