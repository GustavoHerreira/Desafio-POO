using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Desafio_POO.Exceptions.ImoveisExceptions;

namespace Desafio_POO.Models;

// Clecio, substitui a lógica proposta de "bool Alugado" para que esse valor fosse determinado a partir da existencia (ou não) de um inquilino.
// Mantive o pedido de "O atributo Alugado deve ser alterado apenas por métodos próprios (Alugar() e Disponibilizar())."
// Essas funções ainda são responsáveis pela manipulação do atributo "isAlugado".
public abstract class Imovel
{
    [Key]
    public Guid Id { get; private set; }

    [Required]
    [MaxLength(200)]
    public string Endereco { get; protected set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Numero { get; protected set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorMensal { get; set; }

    [Required]
    public Guid ProprietarioId { get; private set; }

    [ForeignKey("ProprietarioId")]
    public virtual Proprietario Proprietario { get; private set; } = null!;

    public Guid? InquilinoId { get; private set; }

    [ForeignKey("InquilinoId")]
    public virtual Inquilino? Inquilino { get; private set; }

    [NotMapped]
    public bool IsAlugado => InquilinoId.HasValue;

    // Construtor para o EF Core
    protected Imovel() { }

    // Construtor para garantir a criação de objetos válidos
    protected Imovel(string endereco, string numero, decimal valorMensal, Proprietario proprietario)
    {
        if (string.IsNullOrWhiteSpace(endereco)) throw new ArgumentNullException(nameof(endereco));
        if (string.IsNullOrWhiteSpace(numero)) throw new ArgumentNullException(nameof(numero));

        Id = Guid.NewGuid();
        Endereco = endereco;
        Numero = numero;
        ValorMensal = valorMensal;
        Proprietario = proprietario ?? throw new ArgumentNullException(nameof(proprietario));
        ProprietarioId = proprietario.Id;
    }

    // Métodos

    public abstract decimal CalcularAluguel(int meses);
    
    public virtual string ObterStatusAluguel()
    {
        return IsAlugado 
            ? "O imóvel está alugado." 
            : "O imóvel está disponível.";
    }
    
    public void AtualizarDadosBase(string novoEndereco, string novoNumero)
    {
        if (string.IsNullOrWhiteSpace(novoEndereco)) throw new ArgumentNullException(nameof(novoEndereco));
        if (string.IsNullOrWhiteSpace(novoNumero)) throw new ArgumentNullException(nameof(novoNumero));

        Endereco = novoEndereco;
        Numero = novoNumero;
    }
    
    public void Alugar(Inquilino inquilino)
    {
        if (IsAlugado)
            throw new ImovelJaAlugadoException();

        Inquilino = inquilino ?? throw new ArgumentNullException(nameof(inquilino), "É necessário fornecer um inquilino.");
        InquilinoId = inquilino.Id;
    }

    public void Disponibilizar()
    {
        if (!IsAlugado)
            throw new ImovelJaDisponivelException();

        Inquilino = null;
        InquilinoId = null;
    }
}