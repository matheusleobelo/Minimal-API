using System;

namespace minimal_api.Dominio.ModelView;

public struct Home
{
public string Mensagem { get => "Bem vindos a API de veiculos!!!";}

public string Documentacao { get => "/swagger"; }
}
