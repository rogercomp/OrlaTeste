namespace Orla.Core.Requests.Categoria;

public class UpdateCategoriaRequest : Request
{
    public long Id { get; set; }
    public bool Checkado { get; set; }
}
