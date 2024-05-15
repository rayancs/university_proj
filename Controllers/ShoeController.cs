using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using university_proj.DataModels;
using university_proj.DataTransferObj;
using university_proj.Service;

namespace university_proj.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoeController : ControllerBase
{
    private readonly ShoeService _shoeService;

    public ShoeController(ShoeService shoeService)
    {
        _shoeService = shoeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShoesAsync()
    {
        var shoes = await _shoeService.GetAllShoeAsync();
        return Ok(shoes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShoeByIdAsync(string id)
    {
        var shoe = await _shoeService.GetShoeByID(id);
        if (shoe == null)
        {
            return NotFound();
        }
        return Ok(shoe);
    }
    [HttpPut("updateBySerial")]
    public async Task<IActionResult>UpdateShoeWithSerialAndAval([FromBody]SerialUpdateModel x)
    {
        var res = await _shoeService.UpdateStatusBySerialNumber(x);
        if (res == 0)
        {
            return BadRequest();
        }
        if (res == null) {
            return NotFound(x);
        }
        return Ok(res);
    }
    [HttpGet("byType/{size}")]
    public async Task<IActionResult> GetShoesByTypeAsync(string size)
    {
        var shoes = await _shoeService.ShoeByType(size);
        if (shoes == null)
        {
            return NotFound();
        }
        return Ok(shoes);
    }
    [HttpGet("bySerialId/{id}")]
    public async Task<IActionResult> GetShoesBySerialAsync(string id)
    {
        var shoes = await _shoeService.GetBySerial(id);
        if (shoes == null)
        {
            return NotFound();
        }
        return Ok(shoes);
    }


    [HttpGet("byAvailability/{aval}")]
    public async Task<IActionResult> GetShoesByAvailabilityAsync(string aval)
    {
        var shoes = await _shoeService.GetByAval(aval);
        if (shoes == null)
        {
            return NotFound();
        }
        return Ok(shoes);
    }
    [HttpGet("rent/{x}")]
    public async Task<IActionResult>RentShoe(string x)
    {
        var res =await _shoeService.RentShoe(x);
        if (res == 0 ) { return NotFound(); }
        if (res == null) {  return NotFound(); }
        return Ok(res);
    }
    [HttpGet("return/{x}")]
    public async Task<IActionResult> ReturnShoe(string x)
    {
        var res = await _shoeService.ReturnShoe(x);
        if (res == 0) { return NotFound(); }
        if (res == null) { return NotFound(); }
        return Ok(res);
    }

    [HttpGet("lost/{x}")]
    public async Task<IActionResult> LostShoe(string x)
    {
        var res = await _shoeService.Lost(x);
        if (res == 0) { return NotFound(); }
        if (res == null) { return NotFound(); }
        return Ok(res);
    }
}
