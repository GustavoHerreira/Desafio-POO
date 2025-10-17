namespace Desafio_POO.Dtos;

public record InquilinoReadDto(
    Guid Id,
    string Nome,
    string CPF,
    string Telefone
);