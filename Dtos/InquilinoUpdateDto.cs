using System.ComponentModel.DataAnnotations;

namespace Desafio_POO.Dtos;

public record InquilinoUpdateDto(
    [Required] string Nome,
    [Required] string CPF,
    [Required] string Telefone
);