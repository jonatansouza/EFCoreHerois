﻿using EFCore.Domain;
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
    public class HeroisController : Controller {
        public HeroiContext _context { get; set; }
        public HeroisController(HeroiContext context) {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll() {
            try {
                var herois = _context.Herois.ToList();
                return Ok(herois);
            } catch(ArgumentException e) {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            try {
                var heroi = _context.Herois.Where(h => h.Id == id).FirstOrDefault();
                return Ok(heroi);
            } catch(ArgumentException e) {
                return BadRequest(e);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Heroi model) {
            try {
                _context.Herois.Add(model);
                _context.SaveChanges();
                return Ok(model);
            }catch(ArgumentException e) {
                return BadRequest(e);
            }

        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Heroi model) {
            try {
                if (_context.Herois.AsNoTracking().FirstOrDefault(h => h.Id == id) == null) {
                    return NotFound();
                }
                _context.Herois.Update(model);
                _context.SaveChanges();
                return Ok(model);
            } catch (ArgumentException e) {
                return BadRequest(e);
            }

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            try {
                var heroi = _context.Herois.Where(h => h.Id == id).Single();
                _context.Herois.Remove(heroi);
                _context.SaveChanges();
                return NoContent();
            } catch(ArgumentException e) {
                return BadRequest(e);
            }
        }
    }

}
