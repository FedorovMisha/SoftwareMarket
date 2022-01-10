using Microsoft.AspNetCore.Mvc;
using SoftwareMarket.Core.Entity;
using SoftwareMarket.Core.Services;

namespace SoftwareMarket.App.Controllers;

[ApiController]
[Route("[controller]/")]
public class DiscountController: ControllerBase
{
    private readonly DiscountService _discountTypeService;

    public DiscountController(DiscountService discountService)
    {
        _discountTypeService = discountService;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult Get([FromQuery] int id)
    {
        var result = _discountTypeService.Get(id);
        if (result == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        return new JsonResult(result);
    }
    
    [HttpGet]
    [Route("get-all-buyer-types")]
    public IActionResult GetAll()
    {
        return new JsonResult(_discountTypeService.GetAll());
    }
    
    [HttpPost]
    [Route("update-types")]
    public IActionResult Update(Discount buyerType)
    {
        _discountTypeService.Update(buyerType);
        return Get(buyerType.Id);
    }

    [HttpPost]
    [Route("create-type")]
    public int Create(decimal value)
    {
        return _discountTypeService.Create(value);
    }
    
    [HttpGet]
    [Route("sort-by-descending")]
    public IActionResult Sort()
    {
        return new JsonResult(_discountTypeService.GetAll().OrderByDescending(x => x.Id));
    }
    
    [HttpGet]
    [Route("remove")]
    public IActionResult Remove(int id)
    {
        _discountTypeService.Remove(id);
        return StatusCode(StatusCodes.Status200OK);
    }
}