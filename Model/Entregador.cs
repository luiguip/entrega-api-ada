using System.ComponentModel.DataAnnotations;

public class Entregador
{
    [Required]
    public string Nome {get; set;}
    [Required]
    [StringLength(11, MinimumLength =11)]
    public string Cpf {get;set;}
}