namespace Orla.Core.Models.Categorias;

public class Categoria
{
    public long Id { get; set; }
    public string? Descricao { get; set; }
    public bool Status { get; set; }    
    public bool Checkado { get; set; }
}
