using Spine.Entities.Aud;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Aud
{
    public interface IErrorRepository
    {
        Task<Error> Crear(Error pobjError);
    }
}
