using Microsoft.AspNetCore.Mvc;
using Petty.Helpers;

namespace Petty.Controllers;
[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]
public class BaseController: ControllerBase
{
    
}