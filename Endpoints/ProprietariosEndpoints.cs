using Desafio_POO.Dtos;
using Desafio_POO.Exceptions.ProprietariosException;
using Desafio_POO.Services;

namespace Desafio_POO.Endpoints;

public static class ProprietariosEndpoints
{
    public static void MapProprietariosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/proprietarios").WithTags("Proprietarios");

        group.MapGet("/", async (ProprietariosService service) => 
            Results.Ok(await service.GetAllAsync())).WithSummary("Lista todos os proprietários.");

        group.MapGet("/{id:guid}", async (Guid id, ProprietariosService service) =>
        {
            try
            {
                return Results.Ok(await service.GetByIdAsync(id));
            }
            catch (ProprietarioNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithName("GetProprietarioById")
        .WithSummary("Lista proprietário por Id.");

        group.MapPost("/", async (ProprietarioCreateDto dto, ProprietariosService service) =>
        {
            var proprietario = await service.CreateAsync(dto);
            return Results.CreatedAtRoute("GetProprietarioById", new { id = proprietario.Id }, proprietario);
        }).WithSummary("Cria proprietário.");

        group.MapPut("/{id:guid}", async (Guid id, ProprietarioUpdateDto dto, ProprietariosService service) =>
        {
            try
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            }
            catch (ProprietarioNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithSummary("Modifica dados do proprietário.");

        group.MapDelete("/{id:guid}", async (Guid id, ProprietariosService service) =>
        {
            try
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            }
            catch (ProprietarioNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithSummary("Deleta proprietário.");
    }
}