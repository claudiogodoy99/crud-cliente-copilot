// Controller class for Cliente implementing cliente repository

using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace Controllers {
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clientes = await _repository.GetAll();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var cliente = await _repository.Get(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteModel cliente)
        {
            try
            {
                await _repository.Add(cliente);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClienteModel cliente)
        {
            try
            {
                await _repository.Update(cliente);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}