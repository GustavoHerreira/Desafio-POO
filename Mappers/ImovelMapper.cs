using Desafio_POO.Dtos;
using Desafio_POO.Models;

namespace Desafio_POO.Mappers;

public static class ImovelMapper
{
    public static ImovelReadDto ToReadDto(this Imovel imovel)
    {
        var dto = new ImovelReadDto
        {
            Id = imovel.Id,
            Endereco = imovel.Endereco,
            Numero = imovel.Numero,
            ValorMensal = imovel.ValorMensal,
            IsAlugado = imovel.IsAlugado,
            ProprietarioId = imovel.ProprietarioId,
            InquilinoId = imovel.InquilinoId,
        };

        switch (imovel)
        {
            case Apartamento apartamento:
                dto.TipoImovel = "Apartamento";
                dto.NumeroApartamento = apartamento.NumeroApartamento;
                dto.ValorCondominio = apartamento.ValorCondominio;
                break;
            case Casa:
                dto.TipoImovel = "Casa";
                break;
        }

        return dto;
    }

    public static Imovel ToModel(this ImovelCreateDto dto, Proprietario proprietario)
    {
        return dto.TipoImovel.ToUpperInvariant() switch
        {
            "APARTAMENTO" => new Apartamento(
                dto.Endereco,
                dto.Numero,
                dto.ValorMensal,
                proprietario,
                dto.NumeroApartamento ?? 0,
                dto.ValorCondominio ?? 0
            ),
            "CASA" => new Casa(
                dto.Endereco,
                dto.Numero,
                dto.ValorMensal,
                proprietario
            ),
            _ => throw new ArgumentException("Tipo de imóvel inválido fornecido."),
        };
    }

    public static void UpdateFromDto(this Imovel imovel, ImovelUpdateDto dto)
    {
        imovel.ValorMensal = dto.ValorMensal;
        
        imovel.AtualizarDadosBase(dto.Endereco, dto.Numero);


        if (imovel is not Apartamento apartamento) return;
        
        if (dto.NumeroApartamento.HasValue && dto.ValorCondominio.HasValue)
        {
            apartamento.AtualizarDadosApartamento(
                dto.NumeroApartamento.Value,
                dto.ValorCondominio.Value
            );
        }
    }
}