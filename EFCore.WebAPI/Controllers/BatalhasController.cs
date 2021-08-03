using EFCore.Domain;
using EFCore.Repository;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhasController : Controller {
        public HeroiContext _context { get; set; }
        public BatalhasController(HeroiContext context) {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll() {
            try {
                var batalhas = _context.Batalhas.ToList();
                return Ok(batalhas);
            } catch(ArgumentException e) {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}", Name = "GetBatalha")]
        public ActionResult Get(int id) {
            try {
                var batalha = _context.Batalhas.Where(b => b.Id == id).FirstOrDefault();
                return Ok(batalha);
            } catch(ArgumentException e) {
                return BadRequest(e);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Batalha model) {
            try {
                _context.Batalhas.Add(model);
                _context.SaveChanges();
                return Ok(model);
            }catch(ArgumentException e) {
                return BadRequest(e);
            }

        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, Batalha model) {
            try {
                if(_context.Batalhas.AsNoTracking().FirstOrDefault(b => b.Id == id) == null) {
                    return NotFound();
                }
                _context.Batalhas.Update(model);
                _context.SaveChanges();
                return Ok(model);
            } catch (ArgumentException e) {
                return BadRequest(e);
            }

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            try {
                var batalha = _context.Batalhas.Where(hero => hero.Id == id).Single();
                _context.Batalhas.Remove(batalha);
                _context.SaveChanges();
                return NoContent();
            } catch(ArgumentException e) {
                return BadRequest(e);
            }
        }
    }

}
