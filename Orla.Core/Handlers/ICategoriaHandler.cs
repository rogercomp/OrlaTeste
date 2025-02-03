using Orla.Core.Models.Categorias;
using Orla.Core.Requests.Categoria;
using Orla.Core.Responses;

namespace Orla.Core.Handlers;

public interface ICategoriaHandler
{
    Task<Response<List<Categoria>?>> GetAllAsync(GetAllCategoriaRequest request);
    Task<Response<string>> UpdateAsync(List<UpdateCategoriaRequest> request);
}
