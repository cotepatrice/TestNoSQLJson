using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNoSQLJson.DTOs;
using TestNoSQLJson.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestNoSQLJson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilInvestisseurController : ControllerBase
    {
        private ProfilInvestisseurContext _context;

        public ProfilInvestisseurController(ProfilInvestisseurContext context)
        {
            _context = context;
        }

        // GET: api/<ProfilInvestisseurController>
        [HttpGet]
        public async Task<IList<ProfilInvestisseur>> GetAsync()
        {
            return await _context.ProfilInvestisseur.ToListAsync();
        }

        // GET api/<ProfilInvestisseurController>/5
        [HttpGet("{subscriberId}")]
        public async Task<IActionResult> GetAsync(int subscriberId)
        {
            try
            {
                var profil = await _context.ProfilInvestisseur
                        .Where(p => p.Subscriber.SubscriberId == subscriberId)
                        .SingleAsync();

                return Ok(profil);
            }
            catch (Exception ex)
            {
                if(ex.Message == "Sequence contains no elements.")
                    return NotFound();

                return BadRequest();
            }
        }

        // POST api/<ProfilInvestisseurController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProfilInvestisseurDto value)
        {
            var subscriber = _context.Subscriber.FirstOrDefault(x => x.SubscriberId == value.SubscriberId);
            if (subscriber is null)
                return NotFound($"The Subscriber with Id {value.SubscriberId} does not exist");

            var profil = new ProfilInvestisseur()
            {
                Subscriber = _context.Subscriber.First(x => x.SubscriberId == value.SubscriberId),
                Content = await BuildContentAsync(value)
            };

            await _context.AddAsync(profil);
            await _context.SaveChangesAsync();
            return Ok(profil);
        }

        private Task<ProfilLine[]> BuildContentAsync(ProfilInvestisseurDto value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<ProfilInvestisseurController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProfilInvestisseurController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
