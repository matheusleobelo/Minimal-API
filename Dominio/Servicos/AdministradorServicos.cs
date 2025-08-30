using System;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Infraestrutura.db;
using minimal_api.Interfaces;

namespace minimal_api.Dominio.Servicos;

public class AdministradorServicos : iAdministradorServicos
{
    private readonly DbContexto _contexto;
    public AdministradorServicos(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public Administrador Incluir(Administrador administrador)
    {
        _contexto.Administradores.Add(administrador);
        _contexto.SaveChanges();
        return administrador;
    }

    public Administrador? Login(LoginDTO loginDTO)
    {
        var adm = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
        return adm;
    }

    public List<Administrador> Todos(int? pagina)
    {
        throw new NotImplementedException();
    }
}
