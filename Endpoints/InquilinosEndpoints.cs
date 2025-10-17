using Desafio_POO.Dtos;
using Desafio_POO.Exceptions.InquilinosException;
using Desafio_POO.Services;

namespace Desafio_POO.Endpoints;

public static class InquilinosEndpoints
{
    public static void MapInquilinosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/inquilinos").WithTags("Inquilinos");

        group.MapGet("/", async (InquilinosService service) => 
            Results.Ok(await service.GetAllAsync())
            ).WithSummary("Lista todos os inquilinos.");

        group.MapGet("/{id:guid}", async (Guid id, InquilinosService service) =>
        {
            try
            {
                return Results.Ok(await service.GetByIdAsync(id));
            }
            catch (InquilinoNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithName("GetInquilinoById")
            .WithSummary("Lista inquilino pelo Id.");
    
        group.MapGet("/{id:guid}/imoveis", async (Guid id, InquilinosService service) =>
        {
            try
            {
                var imoveis = await service.GetImoveisByInquilinoIdAsync(id);
                return Results.Ok(imoveis);
            }
            catch (InquilinoNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithSummary("Lista todos os imóveis alugados por um inquilino específico.");
        
        group.MapPost("/", async (InquilinoCreateDto dto, InquilinosService service) =>
        {
            var inquilino = await service.CreateAsync(dto);
            return Results.CreatedAtRoute("GetInquilinoById", new { id = inquilino.Id }, inquilino);
        }).WithSummary("Cria inquilino.");

        group.MapPut("/{id:guid}", async (Guid id, InquilinoUpdateDto dto, InquilinosService service) =>
        {
            try
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            }
            catch (InquilinoNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithSummary("Modifica dados do inquilino.");

        group.MapDelete("/{id:guid}", async (Guid id, InquilinosService service) =>
        {
            try
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            }
            catch (InquilinoNaoEncontradoException e)
            {
                return Results.NotFound(e.Message);
            }
        }).WithSummary("Deleta inquilino.");
    }
}