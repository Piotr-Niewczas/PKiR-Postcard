using Microsoft.AspNetCore.Mvc;

namespace Postcards.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(IPostcardRequestHandler postcardRequestHandler) : ControllerBase
{
    [HttpGet("{name}", Name = "gRPC Test")]
    public async Task<IActionResult> Get(string name)
    {
        var response = await new GreetHandler().GetGreet(name);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(string baseImgName, string text, string userId)
    {
        var response = await postcardRequestHandler.AddPostcard(baseImgName, text, userId);

        return Ok(response);
    }
}