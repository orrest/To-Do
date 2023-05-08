using Refit;
using System.Threading.Tasks;

namespace To_Do.Services;

internal interface IToDoApi
{
    [Get("/api/todos/get/{id}")]
    Task<IApiResponse> Get(int id);
}
