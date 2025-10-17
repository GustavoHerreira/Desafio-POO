namespace Desafio_POO.Dtos;

public record ProprietarioReadDto(
    Guid Id,
    string Nome,
    string CPF,
    string Telefone,
    string ContatoResumido
);