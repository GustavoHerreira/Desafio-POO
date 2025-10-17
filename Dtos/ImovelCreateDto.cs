using System.ComponentModel.DataAnnotations;

namespace Desafio_POO.Dtos;

public class ImovelCreateDto
{
    [Required(ErrorMessage = "O tipo do imóvel ('Casa' ou 'Apartamento') é obrigatório.")]
    public string TipoImovel { get; set; } = string.Empty;

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    [MaxLength(200)]
    public string Endereco { get; set; } = string.Empty;

    [Required(ErrorMessage = "O número é obrigatório.")]
    [MaxLength(20)]
    public string Numero { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor mensal é obrigatório.")]
    [Range(1, double.MaxValue, ErrorMessage = "O valor mensal deve ser positivo.")]
    public decimal ValorMensal { get; set; }

    [Required(ErrorMessage = "O ID do proprietário é obrigatório.")]
    public Guid ProprietarioId { get; set; }
    
    // Campos específicos Apartamento (nullable)
    public int? NumeroApartamento { get; set; }
    public decimal? ValorCondominio { get; set; }
}