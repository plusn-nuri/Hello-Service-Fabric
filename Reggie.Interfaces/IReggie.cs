using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace Reggie.Interfaces
{
    /// <summary>
    /// A registration component
    /// </summary>
    public interface IReggie: IActor
    {

        Task<Person> Get();

        Task Register(Person person);
    }
}
