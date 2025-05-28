
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TarefasController : Controller
    {
        string conexao = ConfigurationManager.ConnectionStrings["MinhaConexao"].ConnectionString; 

        public ActionResult Index()
        {
            List<Tarefa> lista = new List<Tarefa>();

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "SELECT CD_TAREFA, NM_TAREFA, FL_CONCLUIDA FROM TAREFAS";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Tarefa
                    {
                        Cd_Tarefa = Convert.ToInt32(dr["CD_TAREFA"]),
                        Nm_Tarefa = dr["NM_TAREFA"].ToString(),
                        Fl_Concluida = Convert.ToBoolean(dr["FL_CONCLUIDA"])
                    });
                }
            }
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
       public ActionResult Create(Tarefa tarefa)
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "INSERT INTO TAREFAS (NM_TAREFA, FL_CONCLUIDA) VALUES (@nome, @concluida)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", tarefa.Nm_Tarefa);
                cmd.Parameters.AddWithValue("@concluida", tarefa.Fl_Concluida);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            TempData["Mensagem"] = "Tarefa criada com sucesso!";
            return RedirectToAction("MensagemSucesso");
        }

        public ActionResult Edit(int id)
        {
            Tarefa tarefa = new Tarefa();

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "SELECT CD_TAREFA, NM_TAREFA, FL_CONCLUIDA FROM TAREFAS WHERE CD_TAREFA = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    tarefa.Cd_Tarefa = Convert.ToInt32(dr["CD_TAREFA"]);
                    tarefa.Nm_Tarefa = dr["NM_TAREFA"].ToString();
                    tarefa.Fl_Concluida = Convert.ToBoolean(dr["FL_CONCLUIDA"]);
                }
            }
            return View(tarefa);
        }

        [HttpPost]
        public ActionResult Edit(Tarefa tarefa)
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "UPDATE TAREFAS SET NM_TAREFA = @nome, FL_CONCLUIDA = @concluida WHERE CD_TAREFA = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", tarefa.Nm_Tarefa);
                cmd.Parameters.AddWithValue("@concluida", tarefa.Fl_Concluida);
                cmd.Parameters.AddWithValue("@id", tarefa.Cd_Tarefa);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }



        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "DELETE FROM TAREFAS WHERE CD_TAREFA = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            //return RedirectToAction("Index");
            TempData["Mensagem"] = "Tarefa foi com excluida com sucesso!";
            return RedirectToAction("MensagemSucesso");

        }
                public ActionResult MensagemSucesso()
        {
            return View();
        }

    }
}
