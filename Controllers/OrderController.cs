using AutoMapper;
using EComerceAPI.DTOs;
using EComerceAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EComerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderRepository.GetAll();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpGet("my-orders")]
        public IActionResult GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var orders = _orderRepository.GetByUserId(
                int.Parse(userId)
            );

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(orderDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var order = await _orderRepository.CreateOrderFromCartAsync(
                int.Parse(userId)
            );

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateOrderDto dto)
        {
            var order = _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = dto.Status;

            _orderRepository.Update(order);

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(order);

            return NoContent();
        }
    }
}