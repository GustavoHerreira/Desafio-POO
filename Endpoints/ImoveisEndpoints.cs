using Desafio_POO.Dtos;
using Desafio_POO.Exceptions.ImoveisExceptions;
using Desafio_POO.Exceptions.InquilinosException;
using Desafio_POO.Exceptions.ProprietariosException;
using Desafio_POO.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_POO.Endpoints;

public static class ImoveisEndpoints
{
    public static void MapImoveisEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/imoveis").WithTags("Imoveis");

        group.MapGet("/", async (ImoveisService service) => 
            Results.Ok(await service.GetAllAsync())).WithSummary("Lista todos os imóveis.");

        group.MapGet("/{id:guid}", async (Guid id, ImoveisService service) =>
        {
            try
            {
                return Results.Ok(await service.GetByIdAsync(id));
            }
            catch (ImovelNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithName("GetImovelById")
        .WithSummary("Lista imóvel pelo Id");

        group.MapPost("/", async (ImovelCreateDto dto, ImoveisService service) =>
        {
            try
            {
                var imovel = await service.CreateAsync(dto);
                return Results.CreatedAtRoute("GetImovelById", new { id = imovel.Id }, imovel);
            }
            catch (ProprietarioNaoEncontradoException e)
            {
                return Results.BadRequest(e.Message);
            }
        }).WithName("CreateImovel")
        .WithSummary("Cria imóvel.");

        group.MapPut("/{id:guid}", async (Guid id, ImovelUpdateDto dto, ImoveisService service) =>
        {
            try
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            }
            catch (ImovelNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithSummary("Modifica dados do imóvel.");

        group.MapDelete("/{id:guid}", async (Guid id, ImoveisService service) =>
        {
            try
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            }
            catch (ImovelNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithSummary("Deleta imóvel.");
        
        group.MapGet("/{id:guid}/calcular-aluguel", 
            async (Guid id, [FromQuery] int meses, ImoveisService service) =>
            {
                try
                {
                    if (meses <= 0)
                    {
                        return Results.BadRequest("A quantidade de meses deve ser um número positivo.");
                    }
        
                    var resultado = await service.CalcularValorAluguelAsync(id, meses);
                    return Results.Ok(resultado);
                }
                catch (ImovelNaoEncontradoException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (ArgumentException e)
                {
                    return Results.BadRequest(e.Message);
                }
            }).WithSummary("Calcula o valor total do aluguel para um imóvel por um período em meses.");
        
        
        group.MapPost("/{id:guid}/alugar", async (Guid id, AluguelRequestDto dto, ImoveisService service) =>
        {
            try
            {
                await service.AlugarAsync(id, dto.InquilinoId);
                return Results.Ok("Imóvel alugado com sucesso.");
            }
            catch (ImovelNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (InquilinoNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (ImovelJaAlugadoException e)
            {
                return Results.Conflict(e.Message);
            }
        }).WithSummary("Aluga o imóvel para um inquilino.");

        group.MapPost("/{id:guid}/disponibilizar", async (Guid id, ImoveisService service) =>
        {
            try
            {
                await service.DisponibilizarAsync(id);
                return Results.Ok("Imóvel disponibilizado com sucesso.");
            }
            catch (ImovelNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (ImovelJaDisponivelException e)
            {
                return Results.Conflict(e.Message);
            }
        }).WithSummary("Torna o imóvel disponível para alugar.");
    }
}