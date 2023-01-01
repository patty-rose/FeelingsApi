using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeelingsApi.Models;

namespace FeelingsApi.Solution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeelingsController : ControllerBase
    {
        private readonly FeelingsApiContext _context;

        public FeelingsController(FeelingsApiContext context)
        {
            _context = context;
        }

        // GET: api/Feelings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feeling>>> GetFeelings()
        {
            return await _context.Feelings.ToListAsync();
        }

        // GET: api/Feelings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feeling>> GetFeeling(int id)
        {
            var feeling = await _context.Feelings.FindAsync(id);

            if (feeling == null)
            {
                return NotFound();
            }

            return feeling;
        }

        // PUT: api/Feelings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeeling(int id, Feeling feeling)
        {
            if (id != feeling.FeelingId)
            {
                return BadRequest();
            }

            _context.Entry(feeling).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeelingExists(id))
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

        // POST: api/Feelings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feeling>> PostFeeling(Feeling feeling)
        {
            _context.Feelings.Add(feeling);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeeling", new { id = feeling.FeelingId }, feeling);
        }

        // DELETE: api/Feelings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeeling(int id)
        {
            var feeling = await _context.Feelings.FindAsync(id);
            if (feeling == null)
            {
                return NotFound();
            }

            _context.Feelings.Remove(feeling);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeelingExists(int id)
        {
            return _context.Feelings.Any(e => e.FeelingId == id);
        }
    }
}
