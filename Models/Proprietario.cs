using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio_POO.Models;

public class Proprietario
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Telefone { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(11)]
    public string CPF { get; set; } = string.Empty;

    // Navigation property
    public virtual ICollection<Imovel> Imoveis { get; set; } = new List<Imovel>();
    
    public string ContatoProprietario()
    {
        return $"Nome: {Nome}, Telefone: {Telefone}";
    }
}