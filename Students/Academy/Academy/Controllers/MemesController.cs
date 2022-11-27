using Api.Models.Request.Meme;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemesController : ControllerBase
    {
        private readonly IMemeService _memeService;

        public MemesController(IMemeService memeService) =>
            _memeService = memeService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken) =>
             Ok(await _memeService.GetAllAsync(cancellationToken));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateMemeRequestModel request) =>
            Ok(await _memeService.CreateAsync(request));
    }
}
