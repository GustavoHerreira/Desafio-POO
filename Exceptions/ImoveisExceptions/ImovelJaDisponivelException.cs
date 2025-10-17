namespace Desafio_POO.Exceptions.ImoveisExceptions;

public class ImovelJaDisponivelException : InvalidOperationException
{
    public ImovelJaDisponivelException() : base("Operação inválida: O imóvel já se encontra disponível.") { }

    public ImovelJaDisponivelException(string message) : base(message) { }
}