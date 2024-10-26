using AutoService.API.Contracts;
using AutoService.Core.Abstractions;
using AutoService.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IService<Cars> _service;

        public CarController(IService<Cars> service)
        {
            _service = service;
        }

        [HttpGet("carsList")]
        public async Task<ActionResult<List<CarResponse>>> GetCars()
        {
            var cars = await _service.GetAll();
            var response = cars.Select(b => new CarResponse(b.Id, b.Name, b.Price, b.IsAvailable)).ToList();
            return Ok(response);
        }

        [HttpGet("car/{name}")]
        public async Task<ActionResult<CarResponse>> GetCar([FromRoute] string name)
        {
            var car = await _service.GetByName(name);
            if (car == null)
            {
                return NotFound($"Car with name '{name}' not found.");
            }
            var response = new CarResponse(car.Id, car.Name, car.Price, car.IsAvailable);
            return Ok(response);
        }

        [HttpPost("addCar")]
        public async Task<ActionResult> AddCar([FromBody] CarRequest request)
        {
            var (car, error) = Cars.Create(request.CarName, request.Price, request.IsAvailable);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            await _service.Add(car);
            return Ok($"Car '{request.CarName}' has been added.");
        }

        [HttpDelete("deleteCar/{name}")]
        public async Task<ActionResult> DeleteCar([FromRoute] string name)
        {
            var car = await _service.GetByName(name);
            if (car == null)
            {
                return NotFound($"Car with name '{name}' not found.");
            }

            await _service.Delete(car);
            return Ok($"Car '{name}' has been deleted.");
        }

        [HttpPut("updateCar")]
        public async Task<ActionResult> UpdateCar([FromBody] CarRequest request)
        {
            var car = Cars.Create(request.CarName, request.Price, request.IsAvailable).Car;
            await _service.Update(car);

            return Ok($"Car '{request.CarName}' has been updated.");
        }
    }
}
