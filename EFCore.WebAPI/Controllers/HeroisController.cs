using EFCore.Domain;
using EFCore.Repository;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HeroisController : Controller {
        public HeroiContext _context { get; set; }
        public HeroisController(HeroiContext context) {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll() {
            var herois = _context.Herois.ToList();
            return Ok(herois);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            var heroi = _context.Herois.Where(hero => hero.Id == id).FirstOrDefault();
            return Ok(heroi);
        }
        [HttpPost]
        public ActionResult Post([FromBody] Heroi heroi) {
            _context.Herois.Add(heroi);
            _context.SaveChanges();
            return Ok(heroi);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            var hero = _context.Herois.Where(hero => hero.Id == id).Single();
            _context.Herois.Remove(hero);
            _context.SaveChanges();
            return NoContent();
        }
    }

}
