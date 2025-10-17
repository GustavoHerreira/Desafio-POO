namespace Desafio_POO.Dtos;

public record AluguelCalculadoDto(
    Guid ImovelId,
    int PeriodoMeses,
    decimal ValorTotalCalculado,
    string Descricao
);