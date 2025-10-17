using Desafio_POO.Dtos;
using Desafio_POO.Models;

namespace Desafio_POO.Mappers;

public static class ProprietarioMapper
{
    public static ProprietarioReadDto ToReadDto(this Proprietario proprietario)
    {
        return new ProprietarioReadDto(
            proprietario.Id,
            proprietario.Nome,
            proprietario.CPF,
            proprietario.Telefone,
            proprietario.ContatoProprietario()
        );
    }

    public static Proprietario ToModel(this ProprietarioCreateDto dto)
    {
        return new Proprietario
        {
            Nome = dto.Nome,
            Telefone = dto.Telefone
        };
    }

    public static void UpdateFromDto(this Proprietario proprietario, ProprietarioUpdateDto dto)
    {
        proprietario.Nome = dto.Nome;
        proprietario.Telefone = dto.Telefone;
    }
}