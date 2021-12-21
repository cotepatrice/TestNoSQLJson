using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNoSQLJson.Common;
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
        private IConverter _converters;

        public ProfilInvestisseurController(ProfilInvestisseurContext context, IConverter converters)
        {
            _context = context;
            _converters = converters;
        }

        // GET: api/<ProfilInvestisseurController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(_converters.GetDtoList(await _context.ProfilInvestisseur.Include(p => p.Subscriber).ToListAsync()));
        }

        // GET api/<ProfilInvestisseurController>/5
        [HttpGet("{subscriberId}")]
        public async Task<IActionResult> GetAsync(int subscriberId)
        {
            try
            {
                var modelList = await _context.ProfilInvestisseur.Include(p => p.Subscriber)
                        .Where(p => p.Subscriber.SubscriberId == subscriberId).ToListAsync();

                return Ok(_converters.GetDtoList(modelList));
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
                Content = BuildContentAsync(value)
            };

            await _context.AddAsync(profil);
            await _context.SaveChangesAsync();
            return Ok(_converters.ConvertModelToDto(profil));
        }

        private ProfilLine[] BuildContentAsync(ProfilInvestisseurDto value)
        {

            var contentList = new List<ProfilLine>();
            foreach (var line in value.Content)
            {
                var profilLine = _converters.ConvertDtoLineToModelLine(line);
                contentList.Add(profilLine);
            }

            return contentList.ToArray();
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
