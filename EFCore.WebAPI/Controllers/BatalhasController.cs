using EFCore.Domain;
using EFCore.Repository;
using EFCore.Repository.Interfaces;
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
        private readonly IEFCoreRepository _repo;

        public BatalhasController(IEFCoreRepository repo) {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                var batalhas = await _repo.GetAllBatalhas(true);
                return Ok(batalhas);
            } catch (ArgumentException e) {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id) {
            try {
                var herois = await _repo.GetBatalhaById(id, true);

                return Ok(herois);
            } catch (ArgumentException e) {
                return BadRequest(e);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Batalha model) {
            try {
                _repo.Add(model);

                if (await _repo.SaveChangeAsync()) {
                    return Ok("BAZINGA");
                }
                return BadRequest();
            } catch (ArgumentException e) {
                return BadRequest(e);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha model) {
            try {
                var batalha = await _repo.GetBatalhaById(id);
                if (batalha == null) {
                    return NotFound();
                }
                _repo.Update(model);

                if (await _repo.SaveChangeAsync()) {
                    return Ok("BAZINGA");
                }
                return BadRequest();

            } catch (ArgumentException e) {
                return BadRequest(e);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                var heroi = await _repo.GetBatalhaById(id);
                if (heroi == null) {
                    return NotFound();
                }
                _repo.Delete(heroi);

                if (await _repo.SaveChangeAsync()) {

                    return Ok("BAZINGA");
                }
                return BadRequest();
            } catch (ArgumentException e) {
                return BadRequest(e);
            }
        }
    }

}
