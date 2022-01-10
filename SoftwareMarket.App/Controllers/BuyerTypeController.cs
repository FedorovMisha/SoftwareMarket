using Microsoft.AspNetCore.Mvc;
using SoftwareMarket.Core.Entity;
using SoftwareMarket.Core.Services;

namespace SoftwareMarket.App.Controllers;

[ApiController]
[Route("[controller]/")]
public class BuyerTypeController : ControllerBase
{
    private readonly BuyerTypeService _buyerTypeService;

    public BuyerTypeController(BuyerTypeService buyerTypeService)
    {
        _buyerTypeService = buyerTypeService;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult Get([FromQuery] int id)
    {
        var result = _buyerTypeService.Get(id);
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
        return new JsonResult(_buyerTypeService.GetAll());
    }
    
    [HttpPost]
    [Route("update-types")]
    public IActionResult Update(BuyerType buyerType)
    {
        _buyerTypeService.Update(buyerType);
        return Get(buyerType.Id);
    }

    [HttpPost]
    [Route("create-type")]
    public int Create(string name)
    {
        return _buyerTypeService.Create(name);
    }
    
    [HttpGet]
    [Route("sort-by-descending")]
    public IActionResult Sort()
    {
        return new JsonResult(_buyerTypeService.GetAll().OrderByDescending(x => x.Id));
    }
    
    [HttpGet]
    [Route("remove")]
    public IActionResult Remove(int id)
    {
        _buyerTypeService.Remove(id);
        return StatusCode(StatusCodes.Status200OK);
    }
}