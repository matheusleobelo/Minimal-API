using System;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Interfaces;

public interface iVeiculoServicos
{
    List<Veiculo> Todos(int pagina = 1, string? nome = null, string? marca = null);
    Veiculo? BuscarPorId(int id);
    void Incluir(Veiculo veiculo);
    void Atualizar(Veiculo veiculo);
    void Apagar(Veiculo veiculo);
}
