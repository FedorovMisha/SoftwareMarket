using Microsoft.AspNetCore.Mvc;
using SoftwareMarket.Core.Entity;
using SoftwareMarket.Core.Services;

namespace SoftwareMarket.App.Controllers;

[ApiController]
[Route("[controller]/")]
public class SoftwareTypeController : ControllerBase
{
    private readonly SoftwareTypeService _softwareTypeService;

    public SoftwareTypeController(SoftwareTypeService softwareTypeService)
    {
        _softwareTypeService = softwareTypeService;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult Get([FromQuery] int id)
    {
        var result = _softwareTypeService.Get(id);
        if (result == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        return new JsonResult(result);
    }
    
    [HttpGet]
    [Route("get-all-software-types")]
    public IActionResult GetAll()
    {
        return new JsonResult(_softwareTypeService.GetAll());
    }
    
    [HttpPost]
    [Route("update-types")]
    public IActionResult Update(SoftwareType softwareType)
    {
        _softwareTypeService.Update(softwareType);
        return Get(softwareType.Id);
    }

    [HttpPost]
    [Route("create-type")]
    public int Create(string name)
    {
        return _softwareTypeService.Create(name);
    }

    [HttpGet]
    [Route("sort-by-descending")]
    public IActionResult Sort()
    {
        return new JsonResult(_softwareTypeService.GetAll().OrderByDescending(x => x.Id));
    }
    
    [HttpGet]
    [Route("remove")]
    public IActionResult Remove(int id)
    {
        _softwareTypeService.Remove(id);
        return StatusCode(StatusCodes.Status200OK);
    }
}