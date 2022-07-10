using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace ConexaoBancoDados.Context
{
    public class ContextSql
    {
        // Criar conexao com banco de dados 
        private SqlConnection criarConexao()
        {
            string strConexao = "";
            return new SqlConnection(strConexao);
        }

        //Criar parametros 
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void limparParametro()
        {
            sqlParameterCollection.Clear();
        }

        public void adicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        // Criar execulte INSERT - UPDATE - DELETE
        public object execultaManipulacao(CommandType commandType, string nomeProcedureOuTextoSQL)
        {
            try
            {
                SqlConnection sqlConnection = criarConexao();

                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeProcedureOuTextoSQL;
                sqlCommand.CommandTimeout = 7200;

                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                sqlConnection.Close();
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception erro)
            {

                throw new Exception("Erro ao Conectar com  Banco" + erro.Message);
            }
        }

        // CRIAR CONSULTA BANCO DE DADOS 
        public DataTable execultaConsultas(CommandType commandType, string nomeProcedureOuTextoSQL)
        {
            try
            {
                SqlConnection sqlConnection = criarConexao();

                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeProcedureOuTextoSQL;
                sqlCommand.CommandTimeout = 7200;

                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
                return dataTable;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}
