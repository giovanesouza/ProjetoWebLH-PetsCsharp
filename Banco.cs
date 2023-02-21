using System.Data.SqlClient;

namespace Projeto_Web_Lh_Pets_versão_1
{

    // Class
    class Banco
    {
        // Instância e criação de lista vazia
        private List<Clientes> lista = new List<Clientes>();

        // Retorna lista criada
        public List<Clientes> GetLista()
        {
            return lista;
        }

        // Construtor da class
        public Banco()
        {
            try
            {

                // CÓDIGO DE CONEXÃO NÃO FUNCIONAL
                /*
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(
                "User ID=sa;Password=;" +
                "Server=localhost\\SQLEXPRESS;" +
                "Database=vendas;" +
                "Trusted_Connection=False;"
                );
                */


                //    CÓDIGO DE CONEXÃO FUNCIONAL
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(
               "Data Source=LAPTOP-76HIR06F\\SQLEXPRESS;Initial Catalog=vendas;Integrated Security=True"
                );


                using (SqlConnection conexao = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "SELECT * FROM tblclientes";
                    using (SqlCommand comando = new SqlCommand(sql, conexao))
                    {
                        conexao.Open();
                        using (SqlDataReader tabela = comando.ExecuteReader())
                        {

                            while (tabela.Read())
                            {
                                lista.Add(new Clientes()
                                {
                                    cpf_cnpj = tabela["cpf_cnpj"].ToString(),
                                    nome = tabela["nome"].ToString(),
                                    endereco = tabela["endereco"].ToString(),
                                    rg_ie = tabela["rg_ie"].ToString(),
                                    tipo = tabela["tipo"].ToString(),
                                    valor = (float)Convert.ToDecimal(tabela["valor"]),
                                    valor_imposto = (float)Convert.ToDecimal(tabela["valor_imposto"]),
                                    total = (float)Convert.ToDecimal(tabela["total"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        // Constrói page e formata com base nos dados recebidos - view -> client
        public String GetListaString()
        {
            string enviar = "<!DOCTYPE html>\n<html>\n<head>\n<meta charset='utf-8' />\n" +
                          "<title>Cadastro de Clientes</title>\n</head>\n<body>";
            enviar = enviar + "<b>   CPF / CNPJ    -      Nome    -    Endereço    -   RG / IE   -   Tipo  -   Valor   - Valor Imposto -   Total  </b>";

            int i = 0;
            string corfundo = "", cortexto = "";

            foreach (Clientes cli in GetLista())
            {

                if (i % 2 == 0)
                { corfundo = "#6f47ff"; cortexto = "white"; }
                else
                { corfundo = "#ffffff"; cortexto = "#6f47ff"; }
                i++;


                enviar = enviar +
               $"\n<br><div style='background-color:{corfundo};color:{cortexto};'>" +
                cli.cpf_cnpj + " - " +
                cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                cli.tipo + " - " + cli.valor.ToString("C") + " - " +
                cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C") + "<br>" +
                 "</div>";
            }
            return enviar;
        }

        // Impressão dentro do servidor
        public void imprimirListaConsole()
        {

            Console.WriteLine("   CPF / CNPJ   " + " - " + "    Nome   " +
                " - " + "   Endereço   " + " - " + "  RG / IE  " + " - " +
                "  Tipo " + " - " + "  Valor  " + " - " + "Valor Imposto" +
                " - " + "  Total  ");

            foreach (Clientes cli in GetLista())
            {
                Console.WriteLine(cli.cpf_cnpj + " - " +
                cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                cli.tipo + " - " + cli.valor.ToString("C") + " - " +
                cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C"));
            }
        }


    }
}