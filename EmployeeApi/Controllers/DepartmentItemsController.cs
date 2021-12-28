using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.Models;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentItemsController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public DepartmentItemsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/DepartmentItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentItem>>> GetDepartmentItems()
        {
            return await _context.DepartmentItems.ToListAsync();
        }

        // GET: api/DepartmentItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentItem>> GetDepartmentItem(long id)
        {
            var departmentItem = await _context.DepartmentItems.FindAsync(id);

            if (departmentItem == null)
            {
                return NotFound();
            }

            return departmentItem;
        }

        // PUT: api/DepartmentItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentItem(long id, DepartmentItem departmentItem)
        {
            if (id != departmentItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(departmentItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentItemExists(id))
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

        // POST: api/DepartmentItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DepartmentItem>> PostDepartmentItem(DepartmentItem departmentItem)
        {
            _context.DepartmentItems.Add(departmentItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentItem", new { id = departmentItem.Id }, departmentItem);
        }

        // DELETE: api/DepartmentItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentItem(long id)
        {
            var departmentItem = await _context.DepartmentItems.FindAsync(id);
            if (departmentItem == null)
            {
                return NotFound();
            }

            _context.DepartmentItems.Remove(departmentItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentItemExists(long id)
        {
            return _context.DepartmentItems.Any(e => e.Id == id);
        }
    }
}
