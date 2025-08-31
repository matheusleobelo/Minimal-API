using System;
using System.Collections.Generic;
using minimal_api.Dominio.Entidades;
using minimal_api.Interfaces;

namespace Test.Mocks;

public class VeiculoServicoMock : iVeiculoServicos
{
    private static List<Veiculo> veiculos = new List<Veiculo>();

    public void Apagar(Veiculo veiculo)
    {
        veiculos.Remove(veiculo);
    }

    public void Atualizar(Veiculo veiculo)
    {
        var existente = veiculos.Find(v => v.Id == veiculo.Id);
        if (existente != null)
        {
            // Atualiza os campos desejados
            existente.Nome = veiculo.Nome;
            existente.Marca = veiculo.Marca;
            existente.Ano = veiculo.Ano;
        }
    }

    public Veiculo? BuscarPorId(int id)
    {
        return veiculos.Find(v => v.Id == id);
    }

public void Incluir(Veiculo veiculo)
{
    veiculo.Id = veiculos.Count + 1;
    veiculos.Add(veiculo);
}


    public List<Veiculo> Todos(int pagina = 1, string? nome = null, string? marca = null)
    {
        var query = veiculos.AsEnumerable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(v => v.Nome != null && v.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(marca))
            query = query.Where(v => v.Marca != null && v.Marca.Contains(marca, StringComparison.OrdinalIgnoreCase));

        // Paginação simples: 10 itens por página
        int pageSize = 10;
        return query.Skip((pagina - 1) * pageSize).Take(pageSize).ToList();
    }
}
