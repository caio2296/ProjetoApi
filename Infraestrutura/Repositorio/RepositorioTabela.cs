using Dominio.Interface;
using Entidades.Tabelas;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace Infraestrutura.Repositorio
{
    public class RepositorioTabela : ITabela
    {
        private readonly string _connectionString;

        public RepositorioTabela(string connectionString)
        {
                _connectionString = connectionString;
        }

        public Task Adicionar(Root Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Root Objecto)
        {
            throw new NotImplementedException();
        }

        public Task<Root?> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Root>> BuscarTabela(int id)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))

                using (var com = new SqlCommand("sp_MontaJsonPorPaginaTabela", con))
                {
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.Add("@IdPagina", SqlDbType.Int)
                        .Value = id;

                    var outputParam = new SqlParameter("@JsonFinal", SqlDbType.NVarChar, -1)
                    {
                        Direction = ParameterDirection.Output
                    };

                    com.Parameters.Add(outputParam);

                    await con.OpenAsync().ConfigureAwait(false);
                    await com.ExecuteNonQueryAsync().ConfigureAwait(false);

                    var resultado = outputParam.Value?.ToString();

                    if (!string.IsNullOrWhiteSpace(resultado))
                    {
                        var tabela = JsonSerializer.Deserialize<List<Root>>(resultado);

                        if (tabela != null)
                        {
                            foreach (var root in tabela)
                            {
                                if (root.tablabis != null)
                                {
                                    foreach (var tablabis in root.tablabis)
                                    {
                                        if (tablabis.templates != null)
                                        {
                                            foreach (var template in tablabis.templates)
                                            {
                                                if (template.cols != null)
                                                {
                                                    template.cols = OrganizarPorNivel(template.cols.ToList());
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            return tabela != null ? tabela : new List<Root>();
                        }

                        return new List<Root>();
                    }    
                }
            }
            catch (JsonException ex)
            {
                throw new Exception("Erro ao desserializar as tabelas.", ex);
            }

            return new List<Root>();
        }

        private static List<Col> OrganizarPorNivel(
            List<Col> allCols)
        {
            return allCols
                .OrderBy(c => c.level)
                .ThenBy(c => c.parent)
                .ThenBy(c => c.order)
                .ToList();
        }

        public Task Excluir(Root Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Root>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
