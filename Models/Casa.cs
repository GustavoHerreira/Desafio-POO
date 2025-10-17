namespace Desafio_POO.Models;

public class Casa : Imovel
{
    // Construtor para o EF Core
    private Casa() { } 

    // Construtor para garantir a criação de objetos válidos
    public Casa(
        string endereco, 
        string numero, 
        decimal valorMensal, 
        Proprietario proprietario) 
        : base(endereco, numero, valorMensal, proprietario) { }
    
    public override string ObterStatusAluguel()
    {
        return IsAlugado
            ? "Casa está alugada."
            : "Casa está disponível para aluguel.";
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
        
        var valorTotalBruto = ValorMensal * meses;
        
        // Desconto de 10% para quem alugar por mais de 2 anos (24 meses).
        
        if (meses <= 24) return valorTotalBruto; // Retorna bruto se for menor que 24 meses.
        
        var valorDoDesconto = valorTotalBruto * 0.10m; // 0.10m representa 10%
        return valorTotalBruto - valorDoDesconto;
    }
}