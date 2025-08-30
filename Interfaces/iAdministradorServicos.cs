using System;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Interfaces;

public interface iAdministradorServicos
{
    Administrador? Login(LoginDTO loginDTO);
}
