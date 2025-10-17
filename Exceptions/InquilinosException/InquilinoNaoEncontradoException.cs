namespace Desafio_POO.Exceptions.InquilinosException;

public class InquilinoNaoEncontradoException : Exception
{
    public InquilinoNaoEncontradoException() : base("Inquilino não encontrado.") { }

    public InquilinoNaoEncontradoException(string message) : base(message) { }
    
    public InquilinoNaoEncontradoException(Guid inquilinoId) : base($"Inquilino de ID {inquilinoId} não encontrado.") { }
}