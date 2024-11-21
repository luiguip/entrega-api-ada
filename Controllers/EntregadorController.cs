using Microsoft.AspNetCore.Mvc;

namespace entrega_api.Controllers;

[ApiController]
[Route("[controller]")]
public class EntregadorController : ControllerBase
{
    private static readonly List<Entregador> entregadores =
    [
        new() {
            Nome= "Ricado",
            Cpf="12345667881"
        },
        new() {
            Nome="Paulo",
            Cpf="09809809812"
        }
    ];

    private readonly ILogger<EntregadorController> _logger;

    public EntregadorController(ILogger<EntregadorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/cpf/{cpf}")]
    public Entregador GetByCpf(string cpf)
    {
        if (!entregadores.Where(e => e.Cpf == cpf).Any())
        {
            throw new NotFoundException("Entregador não existe!");
        }
        return entregadores.Where(e => e.Cpf == cpf).First();
    }

    [HttpGet()]
    public List<Entregador> Get()
    {
        return entregadores;
    }

    [HttpPost()]
    public List<Entregador> Save(Entregador entregador)
    {
        if (entregadores.Where(e => e.Cpf == entregador.Cpf).Any())
        {
            throw new AlreadyExistsException("Entregador já existe!");
        }
        entregadores.Add(entregador);
        return entregadores;
    }

}
