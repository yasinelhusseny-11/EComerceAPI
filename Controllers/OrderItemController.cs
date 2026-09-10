using AutoMapper;
using EComerceAPI.DTOs;
using EComerceAPI.Models;
using EComerceAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EComerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemController(
            IOrderItemRepository orderItemRepository,
            IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orderItems = _orderItemRepository.GetAll();

            var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);

            return Ok(orderItemDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var orderItem = _orderItemRepository.GetById(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);

            return Ok(orderItemDto);
        }

        [HttpPost]
        public IActionResult Add(OrderItemDto dto)
        {
            var orderItem = _mapper.Map<OrderItem>(dto);

            _orderItemRepository.Add(orderItem);

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);

            return Ok(orderItemDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderItemDto dto)
        {
            var orderItem = _orderItemRepository.GetById(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.OrderId = dto.OrderId;
            orderItem.ProductId = dto.ProductId;
            orderItem.Quantity = dto.Quantity;
            orderItem.UnitPrice = dto.UnitPrice;

            _orderItemRepository.Update(orderItem);

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);

            return Ok(orderItemDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderItem = _orderItemRepository.GetById(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            _orderItemRepository.Delete(orderItem);

            return NoContent();
        }
    }
}
