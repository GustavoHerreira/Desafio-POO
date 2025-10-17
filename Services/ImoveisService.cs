using Desafio_POO.Data;
using Desafio_POO.Dtos;
using Desafio_POO.Exceptions.ImoveisExceptions;
using Desafio_POO.Exceptions.InquilinosException;
using Desafio_POO.Exceptions.ProprietariosException;
using Desafio_POO.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Desafio_POO.Services;

public class ImoveisService(AppDbContext context)
{
    public async Task<IEnumerable<ImovelReadDto>> GetAllAsync()
    {
        var imoveis = await context.Imoveis.ToListAsync();
        return imoveis.Select(imovel => imovel.ToReadDto());
    }

    public async Task<ImovelReadDto> GetByIdAsync(Guid id)
    {
        var imovel = await context.Imoveis.FindAsync(id);
        return imovel is null ? throw new ImovelNaoEncontradoException(id) : imovel.ToReadDto();
    }

    public async Task<ImovelReadDto> CreateAsync(ImovelCreateDto dto)
    {
        var proprietario = await context.Proprietarios.FindAsync(dto.ProprietarioId);
        if (proprietario is null)
        {
            throw new ProprietarioNaoEncontradoException(dto.ProprietarioId);
        }

        var novoImovel = dto.ToModel(proprietario);

        context.Imoveis.Add(novoImovel);
        await context.SaveChangesAsync();

        return novoImovel.ToReadDto();
    }
    
    public async Task UpdateAsync(Guid id, ImovelUpdateDto dto)
    {
        var imovel = await context.Imoveis.FindAsync(id);
        if (imovel is null)
        {
            throw new ImovelNaoEncontradoException(id);
        }

        imovel.UpdateFromDto(dto);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var imovel = await context.Imoveis.FindAsync(id);
        if (imovel is null)
        {
            throw new ImovelNaoEncontradoException(id);
        }

        context.Imoveis.Remove(imovel);
        await context.SaveChangesAsync();
    }
    
    public async Task<AluguelCalculadoDto> CalcularValorAluguelAsync(Guid imovelId, int meses)
    {
        var imovel = await context.Imoveis.FindAsync(imovelId);
        if (imovel is null)
        {
            throw new ImovelNaoEncontradoException(imovelId);
        }
        
        var valorCalculado = imovel.CalcularAluguel(meses);
        
        var descricao = $"Valor do aluguel para o imóvel ID {imovelId} por {meses} meses.";
    
        return new AluguelCalculadoDto(imovelId, meses, valorCalculado, descricao);
    }
    
    public async Task AlugarAsync(Guid imovelId, Guid inquilinoId)
    {
        var imovel = await context.Imoveis.FindAsync(imovelId);
        if (imovel is null)
        {
            throw new ImovelNaoEncontradoException(imovelId);
        }

        var inquilino = await context.Inquilinos.FindAsync(inquilinoId);
        if (inquilino is null)
        {
            throw new InquilinoNaoEncontradoException(inquilinoId);
        }

        imovel.Alugar(inquilino);
        await context.SaveChangesAsync();
    }

    public async Task DisponibilizarAsync(Guid imovelId)
    {
        var imovel = await context.Imoveis.FindAsync(imovelId);
        if (imovel is null)
        {
            throw new ImovelNaoEncontradoException(imovelId);
        }
        
        imovel.Disponibilizar();
        await context.SaveChangesAsync();
    }
}