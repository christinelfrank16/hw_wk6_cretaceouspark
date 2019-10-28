using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CretaceousPark.Models;
using Microsoft.EntityFrameworkCore;

namespace CretaceousPark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController :ControllerBase
    {
        private CretaceousParkContext _db;
        public AnimalsController(CretaceousParkContext db)
        {
            _db = db;
        }

        //GET api/animals
        [HttpGet]
        public ActionResult<IEnumerable<Animal>> Get(string species, string gender, string name)
        {
            var query = _db.Animals.AsQueryable();
            if (species != null)
            {
                query = query.Where(en => en.Species == species);
            }
            if(gender != null)
            {
                query = query.Where(en => en.Gender == gender);
            }
            if (name != null)
            {
                query = query.Where(entry => entry.Name == name);
            }
            return query.ToList();
        }

        [HttpPost]
        //need to include the [FromBody] annotation so that we can actually put the details of a new animal in the body of a POST API call
        public void Post([FromBody] Animal animal)
        {
            _db.Animals.Add(animal);
            _db.SaveChanges();
        }

        // GET api/animals/5
        [HttpGet("{id}")]
        public ActionResult<Animal> Get(int id)
        {
            return _db.Animals.FirstOrDefault(en => en.AnimalId == id);
        }

        // PUT api/animals/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Animal animal)
        {
            animal.AnimalId = id;
            _db.Entry(animal).State = EntityState.Modified;
            _db.SaveChanges();
        }

        // DELETE api/animals/5
        // forms in HTML5 don't allow for PUT, PATCH or DELETE verbs
        //The benefits of RESTful standards become more readily apparent with an API. Developers don't need to search through documentation in order to surmise the correct URLs for an API
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var animalToDelete = _db.Animals.FirstOrDefault(en => en.AnimalId == id);
            _db.Animals.Remove(animalToDelete);
            _db.SaveChanges();
        }

        

    }
}