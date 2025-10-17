using Desafio_POO.Data;
using Desafio_POO.Dtos;
using Desafio_POO.Exceptions.InquilinosException;
using Desafio_POO.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Desafio_POO.Services;

public class InquilinosService(AppDbContext context)
{
    public async Task<IEnumerable<InquilinoReadDto>> GetAllAsync()
    {
        var inquilinos = await context.Inquilinos.ToListAsync();
        return inquilinos.Select(i => i.ToReadDto());
    }

    public async Task<InquilinoReadDto> GetByIdAsync(Guid id)
    {
        var inquilino = await context.Inquilinos.FindAsync(id);
        return inquilino is null
            ? throw new InquilinoNaoEncontradoException(id)
            : inquilino.ToReadDto();
    }
    
    public async Task<IEnumerable<ImovelReadDto>> GetImoveisByInquilinoIdAsync(Guid inquilinoId)
    {
        var inquilinoExiste = await context.Inquilinos.AnyAsync(i => i.Id == inquilinoId);
        if (!inquilinoExiste)
        {
            throw new InquilinoNaoEncontradoException(inquilinoId);
        }

        var imoveis = await context.Imoveis
            .Where(i => i.InquilinoId == inquilinoId)
            .ToListAsync();

        return imoveis.Select(imovel => imovel.ToReadDto());
    }

    public async Task<InquilinoReadDto> CreateAsync(InquilinoCreateDto dto)
    {
        var inquilino = dto.ToModel();
        context.Inquilinos.Add(inquilino);
        await context.SaveChangesAsync();
        return inquilino.ToReadDto();
    }

    public async Task UpdateAsync(Guid id, InquilinoUpdateDto dto)
    {
        var inquilino = await context.Inquilinos.FindAsync(id);
        if (inquilino is null)
        {
            throw new InquilinoNaoEncontradoException(id);
        }
        inquilino.UpdateFromDto(dto);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var inquilino = await context.Inquilinos.FindAsync(id);
        if (inquilino is null)
        {
            throw new InquilinoNaoEncontradoException(id);
        }
        context.Inquilinos.Remove(inquilino);
        await context.SaveChangesAsync();
    }
}