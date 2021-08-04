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
    public class HeroisController : Controller {
        private readonly IEFCoreRepository _repo;

        public HeroisController(IEFCoreRepository repo) {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get() {
            try {
                var herois = await _repo.GetAllHerois(true);

                return Ok(herois);
            } catch (Exception ex) {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpGet("{id}", Name = "GetHeroi")]
        public async Task<IActionResult> Get(int id) {
            try {
                var herois = await _repo.GetHeroiById(id, true);

                return Ok(herois);
            } catch (Exception ex) {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Heroi model) {
            try {
                _repo.Add(model);

                if (await _repo.SaveChangeAsync()) {
                    return Ok("BAZINGA");
                }
            } catch (Exception ex) {
                return BadRequest($"Erro: {ex}");
            }

            return BadRequest("Não Salvou");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Heroi model) {
            try {
                var heroi = await _repo.GetHeroiById(id);
                if (heroi != null) {
                    _repo.Update(model);

                    if (await _repo.SaveChangeAsync())
                        return Ok("BAZINGA");
                }
            } catch (Exception ex) {
                return BadRequest($"Erro: {ex}");
            }

            return BadRequest($"Não Deletado!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                var heroi = await _repo.GetHeroiById(id);
                if (heroi != null) {
                    _repo.Delete(heroi);

                    if (await _repo.SaveChangeAsync())
                        return Ok("BAZINGA");
                }
            } catch (Exception ex) {
                return BadRequest($"Erro: {ex}");
            }

            return BadRequest($"Não Deletado!");
        }
    }

}
