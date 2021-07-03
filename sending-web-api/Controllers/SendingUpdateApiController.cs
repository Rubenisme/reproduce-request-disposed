using ClientLib;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SendingWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendingUpdateApiController : ControllerBase
    {
        private readonly TheClient _theClient;

        public SendingUpdateApiController(TheClient theClient)
        {
            _theClient = theClient;
        }

        [HttpPost(nameof(UpdateOfProperty) + "/{id}")]
        public async Task UpdateOfProperty([FromRoute] string id, CancellationToken cancellationToken)
        {
            await _theClient.UpdateMethod(id, cancellationToken);
        }
    }
}

