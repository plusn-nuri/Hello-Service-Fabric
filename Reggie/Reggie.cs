using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Reggie.Interfaces;

namespace Reggie
{
  
    [StatePersistence(StatePersistence.Persisted)]
    internal class Reggie : Actor, IReggie
    {
        private const string PersonKey = "person";

        public Reggie(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task<Person> Get()
        {
            var result= this.StateManager.GetStateAsync<Person>(PersonKey);
            return result;
        }

        public Task Register(Person person)
        {
            return this.StateManager.SetStateAsync(PersonKey, person);
        }

        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            return this.StateManager.TryAddStateAsync<Person>(PersonKey, null);
        }
    }
}
