using Desafio_POO.Data;
using Desafio_POO.Dtos;
using Desafio_POO.Exceptions.ProprietariosException;
using Desafio_POO.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Desafio_POO.Services;

public class ProprietariosService(AppDbContext context)
{
    public async Task<IEnumerable<ProprietarioReadDto>> GetAllAsync()
    {
        var proprietarios = await context.Proprietarios.ToListAsync();
        return proprietarios.Select(p => p.ToReadDto());
    }

    public async Task<ProprietarioReadDto> GetByIdAsync(Guid id)
    {
        var proprietario = await context.Proprietarios.FindAsync(id);
        return proprietario is null
            ? throw new ProprietarioNaoEncontradoException(id)
            : proprietario.ToReadDto();
    }
    
    public async Task<IEnumerable<ImovelReadDto>> GetImoveisByProprietarioIdAsync(Guid proprietarioId)
    {
        var proprietarioExiste = await context.Proprietarios.AnyAsync(p => p.Id == proprietarioId);
        if (!proprietarioExiste)
        {
            throw new ProprietarioNaoEncontradoException(proprietarioId);
        }

        var imoveis = await context.Imoveis
            .Where(i => i.ProprietarioId == proprietarioId)
            .ToListAsync();

        return imoveis.Select(imovel => imovel.ToReadDto());
    }
    public async Task<ProprietarioReadDto> CreateAsync(ProprietarioCreateDto dto)
    {
        var proprietario = dto.ToModel();
        context.Proprietarios.Add(proprietario);
        await context.SaveChangesAsync();
        return proprietario.ToReadDto();
    }

    public async Task UpdateAsync(Guid id, ProprietarioUpdateDto dto)
    {
        var proprietario = await context.Proprietarios.FindAsync(id);
        if (proprietario is null)
        {
            throw new ProprietarioNaoEncontradoException(id);
        }
        proprietario.UpdateFromDto(dto);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var proprietario = await context.Proprietarios.FindAsync(id);
        if (proprietario is null)
        {
            throw new ProprietarioNaoEncontradoException(id);
        }
        context.Proprietarios.Remove(proprietario);
        await context.SaveChangesAsync();
    }
}