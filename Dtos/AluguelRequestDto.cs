using System.ComponentModel.DataAnnotations;

namespace Desafio_POO.Dtos;

public record AluguelRequestDto(
    [Required(ErrorMessage = "O ID do inquilino é obrigatório.")]
    Guid InquilinoId
);