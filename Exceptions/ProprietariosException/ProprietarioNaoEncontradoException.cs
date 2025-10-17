namespace Desafio_POO.Exceptions.ProprietariosException;

public class ProprietarioNaoEncontradoException : Exception
{
    public ProprietarioNaoEncontradoException() : base("Proprietário não encontrado.") { }

    public ProprietarioNaoEncontradoException(string message) : base(message) { }
    
    public ProprietarioNaoEncontradoException(Guid proprietarioId) : base($"Proprietário de ID {proprietarioId} não encontrado.") { }
}