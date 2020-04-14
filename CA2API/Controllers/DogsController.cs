using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CA2API;
using CA2API.Data;
using Microsoft.AspNetCore.JsonPatch;

namespace CA2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogContext _context;

        public DogsController(DogContext context)
        {
            _context = context;
        }

        // GET: api/Dogs
        [HttpGet]
        public IEnumerable<Dog> GetDogs()
        {
            return _context.Dogs;
        }

        // GET: api/Dogs/5
        [HttpGet("{breed}")]
        public IActionResult GetDogBreed( string breed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dog =  _context.Dogs.FirstOrDefault(e => e.Breed.ToUpper() == breed.ToUpper());

            if (dog == null)
            {
                return NotFound();
            }

            return Ok(dog);
        }

        // GET: api/Dogs/5
        [HttpGet("status/{IsAdopted}")]
        public IActionResult GetDogAdoptionStatus(bool IsAdopted)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dog = _context.Dogs.Where(e => e.IsAdopted == IsAdopted);

            if (dog == null)
            {
                return NotFound();
            }

            return Ok(dog);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDog([FromRoute] string id, [FromBody] Dog dog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dog.ID)
            {
                return BadRequest();
            }

            _context.Entry(dog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogExists(id))
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



        // POST: api/Dogs
        [HttpPost]
        public async Task<IActionResult> PostDog([FromBody] Dog dog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dogs.Add(dog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDog", new { id = dog.ID }, dog);
        }

        // DELETE: api/Dogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDog([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dog = await _context.Dogs.FindAsync(id);
            if (dog == null)
            {
                return NotFound();
            }

            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();

            return Ok(dog);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch([FromRoute] string id, [FromBody] JsonPatchDocument<Dog> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var authorFromDB = await _context.Dogs.FirstOrDefaultAsync(x => x.ID == id);

            if (authorFromDB == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(authorFromDB, ModelState);

            var isValid = TryValidateModel(authorFromDB);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool DogExists(string id)
        {
            return _context.Dogs.Any(e => e.ID == id);
        }



 





    }


}