namespace Desafio_POO.Exceptions.ImoveisExceptions;

public class ImovelJaAlugadoException : InvalidOperationException
{
    public ImovelJaAlugadoException() : base("Operação inválida: O imóvel já se encontra alugado.") { }

    public ImovelJaAlugadoException(string message) : base(message) { }
}