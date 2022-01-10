using Microsoft.AspNetCore.Mvc;
using SoftwareMarket.Core.Entity;
using SoftwareMarket.Core.Services;

namespace SoftwareMarket.App.Controllers;

[ApiController]
[Route("sales/[controller]/")]
public class SaleController: ControllerBase
{
    private readonly SaleService _saleService;

    public SaleController(SaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult Get([FromQuery] int id)
    {
        var result = _saleService.Get(id);
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
        return new JsonResult(_saleService.GetAll());
    }
    
    [HttpPost]
    [Route("update")]
    public IActionResult Update(Sale buyerType)
    {
        _saleService.Update(buyerType);
        return Get(buyerType.Id);
    }

    [HttpPost]
    [Route("create")]
    public int Create(Sale sale)
    {
        return _saleService.Create(sale);
    }

    [HttpGet]
    [Route("sort-by-descending")]
    public IActionResult Sort()
    {
        return new JsonResult(_saleService.GetAll().OrderByDescending(x => x.Id));
    }

    [HttpGet]
    [Route("remove")]
    public IActionResult Remove(int id)
    {
        _saleService.Remove(id);
        return StatusCode(StatusCodes.Status200OK);
    }
}