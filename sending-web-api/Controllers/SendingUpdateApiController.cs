using ClientLib;
using Contract;
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
        public async Task UpdateOfProperty([FromRoute] string id, [FromBody] UpdateRequest request, CancellationToken cancellationToken)
        {
            var updateRequest = new UpdateRequest
            {
                SomeProperty = request.SomeProperty
            };

            await _theClient.UpdateMethod(id, updateRequest, cancellationToken);
        }
    }
}

