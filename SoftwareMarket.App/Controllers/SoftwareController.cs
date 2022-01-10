using Microsoft.AspNetCore.Mvc;
using SoftwareMarket.Core.Entity;
using SoftwareMarket.Core.Services;

namespace SoftwareMarket.App.Controllers;

[ApiController]
[Route("[controller]/")]
public class SoftwareController: ControllerBase
{
    private readonly SoftwareService _softwareService;

    public SoftwareController(SoftwareService softwareService)
    {
        _softwareService = softwareService;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult Get([FromQuery] int id)
    {
        var result = _softwareService.Get(id);
        if (result == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        return new JsonResult(result);
    }
    
    [HttpGet]
    [Route("get-all" +
           "")]
    public IActionResult GetAll()
    {
        return new JsonResult(_softwareService.GetAll());
    }
    
    [HttpPost]
    [Route("update")]
    public IActionResult Update(Software software)
    {
        _softwareService.Update(software);
        return Get(software.Id);
    }

    [HttpPost]
    [Route("create-software")]
    public int Create(Software software)
    {
        return _softwareService.Create(software);
    }
    
    [HttpGet]
    [Route("sort-by-descending")]
    public IActionResult Sort()
    {
        return new JsonResult(_softwareService.GetAll().OrderByDescending(x => x.Id));
    }
    
    [HttpGet]
    [Route("remove")]
    public IActionResult Remove(int id)
    {
        _softwareService.Remove(id);
        return StatusCode(StatusCodes.Status200OK);
    }
}