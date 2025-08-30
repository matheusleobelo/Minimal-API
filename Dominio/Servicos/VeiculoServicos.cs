using System;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;
using minimal_api.Infraestrutura.db;
using minimal_api.Interfaces;

namespace minimal_api.Dominio.Servicos;

public class VeiculoServicos : iVeiculoServicos
{
    private readonly DbContexto _contexto;
    public VeiculoServicos(DbContexto contexto)
    {
        _contexto = contexto;
    }
    public void Apagar(Veiculo veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();
    }

    public void Atualizar(Veiculo veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();
    }

    public Veiculo? BuscarPorId(int id)
    {
        return _contexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
    }

    public void Incluir(Veiculo veiculo)
    {
        _contexto.Veiculos.Add(veiculo);
        _contexto.SaveChanges();
    }

    public List<Veiculo> Todos(int pagina = 1, string? nome = null, string? marca = null)
    {
        var querry = _contexto.Veiculos.AsQueryable();
        if (!string.IsNullOrEmpty(nome))
        {
            querry = querry.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
        }
        int itensPorPagina = 10;
        querry.Skip((pagina - 1) * itensPorPagina).Take(itensPorPagina);
        return querry.ToList();
    }
}
