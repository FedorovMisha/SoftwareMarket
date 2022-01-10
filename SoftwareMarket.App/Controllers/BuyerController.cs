using Microsoft.AspNetCore.Mvc;
using SoftwareMarket.Core.Entity;
using SoftwareMarket.Core.Services;

namespace SoftwareMarket.App.Controllers;

[ApiController]
[Route("[controller]/")]
public class BuyerController : Controller
{
    private readonly BuyerService _buyerService;

    public BuyerController(BuyerService buyerTypeService)
    {
        _buyerService = buyerTypeService;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult Get([FromQuery] int id)
    {
        var result = _buyerService.Get(id);
        if (result == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        return new JsonResult(result);
    }
    
    [HttpGet]
    [Route("get-all")]
    public IActionResult GetAll()
    {
        return new JsonResult(_buyerService.GetAll());
    }
    
    [HttpPost]
    [Route("update")]
    public IActionResult Update(Buyer buyer)
    {
        _buyerService.Update(buyer);
        return Get(buyer.Id);
    }

    [HttpPost]
    [Route("create-type")]
    public int Create(Buyer buyer)
    {
        return _buyerService.Create(buyer);
    }
    
    [HttpGet]
    [Route("sort-by-descending")]
    public IActionResult Sort()
    {
        return new JsonResult(_buyerService.GetAll().OrderByDescending(x => x.Id));
    }
    
    [HttpGet]
    [Route("remove")]
    public IActionResult Remove(int id)
    {
        _buyerService.Remove(id);
        return StatusCode(StatusCodes.Status200OK);
    }
}