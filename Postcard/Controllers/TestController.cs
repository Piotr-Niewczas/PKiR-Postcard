﻿
using Microsoft.AspNetCore.Mvc;

namespace Postcard.Controllers;
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet(template:"{name}", Name = "gRPC Test")]
    public async Task<IActionResult> Get(string name)
    {
        var response = await new GreetHandler().GetGreet(name);
        
        return Ok( response);
    }
}