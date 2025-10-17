namespace Desafio_POO.Models;

public class Apartamento : Imovel
{
    public int NumeroApartamento { get; set; }
    public decimal ValorCondominio { get; set; }
    
    // Construtor para o EF Core
    private Apartamento() { } 

    // Construtor para garantir a criação de objetos válidos
    public Apartamento(
        string endereco, 
        string numero, 
        decimal valorMensal, 
        Proprietario proprietario, 
        int numeroApartamento, 
        decimal valorCondominio) 
        : base(endereco, numero, valorMensal, proprietario)
    {
        NumeroApartamento = numeroApartamento;
        ValorCondominio = valorCondominio;
    }
    
    public void AtualizarDadosApartamento(int novoNumeroApartamento, decimal novoValorCondominio)
    {
        // Validações para garantir a consistência
        if (novoNumeroApartamento <= 0) throw new ArgumentException("O número do apartamento deve ser positivo.");
        if (novoValorCondominio < 0) throw new ArgumentException("O valor do condomínio não pode ser negativo.");

        NumeroApartamento = novoNumeroApartamento;
        ValorCondominio = novoValorCondominio;
    }
    
    public override string ObterStatusAluguel()
    {
        return IsAlugado
            ? $"Apartamento de número {NumeroApartamento} está alugado."
            : "Apartamento está disponível para aluguel.";
    }

    
    // O desconto para "casa" e "apartamento" são diferentes.
    // casa: desconto de 10% caso alugado por 24 meses
    // apartamento: desconto de 10% caso alugado por 36 meses
    public override decimal CalcularAluguel(int meses)
    {
        if (meses <= 0)
        {
            throw new ArgumentException("A quantidade de meses deve ser positiva.", nameof(meses));
        }
        
        var custoMensalTotal = ValorMensal + ValorCondominio;
        
        var valorTotalBruto = custoMensalTotal * meses;
        
        // Desconto de 10% para quem alugar por mais de 3 anos (36 meses).
        
        if (meses <= 36) return valorTotalBruto; // Retorna bruto se for menor que 36 meses.
        
        var valorDoDesconto = valorTotalBruto * 0.10m; // 0.10m representa 10%
        return valorTotalBruto - valorDoDesconto;
    }
}