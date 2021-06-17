using Contract;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceivingUpdateApiController : ControllerBase
    {
        [HttpPost(nameof(UpdateOfProperty) + "/{id}")]
        public Task UpdateOfProperty(
            [Required(AllowEmptyStrings = false)] [FromRoute] string id,
            [Required][FromBody] UpdateRequest request,
            CancellationToken cancellationToken)
        {
            // do logic whatever here with received information
            return Task.CompletedTask;
        }
    }
}
