using AutoMapper;
using EComerceAPI.DTOs;
using EComerceAPI.Models;
using EComerceAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public CartController(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var carts = _cartRepository.GetAll();

        var cartDtos = _mapper.Map<IEnumerable<CartDto>>(carts);

        return Ok(cartDtos);
    }

    [HttpGet]
    public IActionResult GetMyCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var cart = _cartRepository.GetByUserId(int.Parse(userId));

        if (cart == null)
        {
            return NotFound();
        }

        var cartDto = _mapper.Map<CartDto>(cart);

        return Ok(cartDto);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem(CartItemDto cartItemDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        await _cartRepository.AddItemAsync(
            int.Parse(userId),
            cartItemDto.ProductId,
            cartItemDto.Quantity
        );

        return Ok(new
        {
            message = "Item added to cart successfully"
        });
    }

    [HttpPut("items/{id}")]
    public IActionResult UpdateQuantity(int id, UpdateCartItemDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var cartItem = _cartRepository.GetItemById(
            id,
            int.Parse(userId)
        );

        if (cartItem == null)
        {
            return NotFound();
        }

        cartItem.Quantity = dto.Quantity;

        _cartRepository.UpdateQuantity(cartItem);

        var responseDto = _mapper.Map<CartItemResponseDto>(cartItem);

        return Ok(responseDto);
    }

    [HttpDelete("items/{id}")]
    public IActionResult RemoveItem(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var cartItem = _cartRepository.GetItemById(
            id,
            int.Parse(userId)
        );

        if (cartItem == null)
        {
            return NotFound();
        }

        _cartRepository.RemoveItem(cartItem);

        return NoContent();
    }
}