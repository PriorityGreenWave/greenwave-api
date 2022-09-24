using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace priority_green_wave_api.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
