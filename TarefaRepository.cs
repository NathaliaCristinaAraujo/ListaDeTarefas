using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.Models;

public class TarefaRepository
{
    private string connectionString = "Data Source=.;Initial Catalog=ListaTarefas;Integrated Security=True";

    public List<Tarefa> ObterTodas()
    {
        var lista = new List<Tarefa>();
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Tarefas", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Tarefa
                {
                    Cd_Tarefa = (int)reader["Cd_Tarefa"],
                    Nm_Tarefa = reader["Nm_Tarefa"].ToString(),
                    Fl_Concluida = (bool)reader["Fl_Concluida"]
                });
            }
        }
        return lista;
    }

    public void Adicionar(Tarefa tarefa)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand("INSERT INTO Tarefas (Descricao, Concluida) VALUES (@Descricao, @Concluida)", conn);
            cmd.Parameters.AddWithValue("@Nm_Tarefa", tarefa.Nm_Tarefa);
            cmd.Parameters.AddWithValue("@Fl_Concluida", tarefa.Fl_Concluida);
            cmd.ExecuteNonQuery();
        }
    }

    public void Atualizar(Tarefa tarefa)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand("UPDATE Tarefas SET Descricao = @Descricao, Concluida = @Concluida WHERE Cd_Tarefa = @Cd_Tarefa", conn);
            cmd.Parameters.AddWithValue("@Cd_Tarefa", tarefa.Cd_Tarefa);
            cmd.Parameters.AddWithValue("@Nm_Tarefa", tarefa.Nm_Tarefa);
            cmd.Parameters.AddWithValue("@Fl_Concluida", tarefa.Fl_Concluida);
            cmd.ExecuteNonQuery();
        }
    }

    public void Deletar(int id)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand("DELETE FROM Tarefas WHERE @Cd_Tarefa = @Cd_Tarefa", conn);
            cmd.Parameters.AddWithValue("@Cd_Tarefa", id);
            cmd.ExecuteNonQuery();
        }
    }

    public Tarefa ObterPorId(int id)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Tarefas WHERE @Cd_Tarefa = @Cd_Tarefa", conn);
            cmd.Parameters.AddWithValue("@Cd_Tarefa", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Tarefa
                {
                    Cd_Tarefa = (int)reader["Cd_Tarefa"],
                    Nm_Tarefa = reader["Nm_Tarefa"].ToString(),
                    Fl_Concluida = (bool)reader["Fl_Concluida"]
                };
            }
        }
        return null;
    }
}