namespace Desafio_POO.Exceptions.ImoveisExceptions;

public class ImovelNaoEncontradoException : Exception
{
    public ImovelNaoEncontradoException() { }
    
    public ImovelNaoEncontradoException(string message) : base(message) { }
    
    public ImovelNaoEncontradoException(Guid imovelId) : base($"O imóvel com o ID '{imovelId}' não foi encontrado.") { }
}