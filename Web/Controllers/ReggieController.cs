using Microsoft.AspNetCore.Mvc;
using Reggie.Interfaces;
using Microsoft.ServiceFabric.Actors.Client;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ReggieController: Controller
    {

        [HttpGet("{id}")]
        public async Task<Person> Get(string id)
        {
            var actor = ActorProxy.Create<IReggie>(new ActorId(id), applicationName: "HelloFab1");
            return await actor.Get();
        }


        [HttpPost("{id}")]
        public async Task Post(string id)
        {
            var actor = ActorProxy.Create<IReggie>(new ActorId(id));

            await actor.Register(new Person { Email = id });
        }

    }
}
