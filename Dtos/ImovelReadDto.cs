namespace Desafio_POO.Dtos;

public class ImovelReadDto
{
    public Guid Id { get; set; }
    public string TipoImovel { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public decimal ValorMensal { get; set; }
    public bool IsAlugado { get; set; }

    // Propriedades de Apartamento
    public int? NumeroApartamento { get; set; }
    public decimal? ValorCondominio { get; set; }

    
    public Guid ProprietarioId { get; set; }
    public Guid? InquilinoId { get; set; }
}