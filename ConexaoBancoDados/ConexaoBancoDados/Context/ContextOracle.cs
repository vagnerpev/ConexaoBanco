using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ConexaoBancoDados.Context
{
    public class ContextOracle
    {
        // Criar conexao com banco de dados 
        private OracleConnection criarConexao()
        {
            string strConexao = "User ID=USUATIO_ORACLE;Password=SENHA_ORACLE;Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = SERVIDOR_ORACLE )(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = BASE_DADOS)));Pooling=true;Connection Lifetime=300;Max Pool Size=20;";
            return new OracleConnection(strConexao);
        }

        //Criar parametros 
        private OracleParameterCollection oracleParameterCollection = new OracleCommand().Parameters;

        public void limparParametro()
        {
            oracleParameterCollection.Clear();
        }

        public void adicionarParametros(string nomeParametro, object valorParametro)
        {
            oracleParameterCollection.Add(new OracleParameter(nomeParametro, valorParametro));
        }

        // Criar execulte INSERT - UPDATE - DELETE
        public object execultaManipulacao(CommandType commandType, string nomeProcedureOuTextoSQL)
        {
            try
            {
                OracleConnection oracleConnection = criarConexao();

                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandType = commandType;
                oracleCommand.CommandText = nomeProcedureOuTextoSQL;
                oracleCommand.CommandTimeout = 7200;

                foreach (OracleParameter oracleParameter in oracleParameterCollection)
                {
                    oracleCommand.Parameters.Add(new OracleParameter(oracleParameter.ParameterName, oracleParameter.Value));
                }

                oracleConnection.Close();

                return oracleCommand.ExecuteScalar();
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
                OracleConnection oracleConnection = criarConexao();

                oracleConnection.Open();
                OracleCommand oracleCommand = oracleConnection.CreateCommand();
                oracleCommand.CommandType = commandType;
                oracleCommand.CommandText = nomeProcedureOuTextoSQL;
                oracleCommand.CommandTimeout = 7200;

                foreach (OracleParameter oracleParameter in oracleParameterCollection)
                {
                    oracleCommand.Parameters.Add(new OracleParameter(oracleParameter.ParameterName, oracleParameter.Value));
                }

                OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
                DataTable dataTable = new DataTable();
                oracleDataAdapter.Fill(dataTable);
                oracleConnection.Close();
                return dataTable;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}
