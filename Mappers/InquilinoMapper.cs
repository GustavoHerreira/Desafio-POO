using Desafio_POO.Dtos;
using Desafio_POO.Models;

namespace Desafio_POO.Mappers;

public static class InquilinoMapper
{
    public static InquilinoReadDto ToReadDto(this Inquilino inquilino)
    {
        return new InquilinoReadDto(
            inquilino.Id,
            inquilino.Nome,
            inquilino.CPF,
            inquilino.Telefone
        );
    }

    public static Inquilino ToModel(this InquilinoCreateDto dto)
    {
        return new Inquilino
        {
            Nome = dto.Nome,
            CPF = dto.CPF,
            Telefone = dto.Telefone
        };
    }

    public static void UpdateFromDto(this Inquilino inquilino, InquilinoUpdateDto dto)
    {
        inquilino.Nome = dto.Nome;
        inquilino.CPF = dto.CPF;
        inquilino.Telefone = dto.Telefone;
    }
}