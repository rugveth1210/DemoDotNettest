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
    public class EmployeeItemsController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeItemsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeItem>>> GetEmployeeItems()
        {
              //include the foreign key for every entry
            foreach(EmployeeItem employee in _context.EmployeeItems){
                _context.EmployeeItems.Include(e=>e.Department).Where(e=>e.Id==employee.Id).First();
            }
            return await _context.EmployeeItems.ToListAsync();
        }

        // GET: api/EmployeeItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeItem>> GetEmployeeItem(long id)
        {
             _context.EmployeeItems.Include(e=>e.Department).Where(e=>e.Id==id).First();
            var employeeItem = await _context.EmployeeItems.FindAsync(id);

            if (employeeItem == null)
            {
                return NotFound();
            }

            return employeeItem;
        }

        // PUT: api/EmployeeItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{did}")]
        public async Task<IActionResult> PutEmployeeItem(long id,long did,EmployeeItem employeeItem)
        {
            if (id != employeeItem.Id)
            {
                return BadRequest();
            }

            employeeItem.Department=(DepartmentItem)_context.DepartmentItems.Where(d=>d.Id==did).First();
            _context.Entry(employeeItem).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeItemExists(id))
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

        // POST: api/EmployeeItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{did}")]
        public async Task<ActionResult<EmployeeItem>> PostEmployeeItem(long did,EmployeeItem employeeItem)
        {
        //make sure to attach the foreign entity to the employee entity before saving otherwise it will return null
            employeeItem.Department=(DepartmentItem)_context.DepartmentItems.Where(d=>d.Id==did).First();
            _context.EmployeeItems.Add(employeeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeItem", new { id = employeeItem.Id }, employeeItem);
        }

        // DELETE: api/EmployeeItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeItem(long id)
        {
            var employeeItem = await _context.EmployeeItems.FindAsync(id);
            if (employeeItem == null)
            {
                return NotFound();
            }

            _context.EmployeeItems.Remove(employeeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeItemExists(long id)
        {
            return _context.EmployeeItems.Any(e => e.Id == id);
        }
    }
}

