using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class FrutasServico : IFrutasServicos
    {
        private IFrutas _frutas;    
        public FrutasServico(IFrutas frutas)
        {
                _frutas = frutas;
        }
        public async Task AdicionarFrutasSemEF(Frutas frutas)
        {
            var validarDescricao = frutas.ValidarPropriedadeString(frutas.Descricao, "Descrição");
            var validarTamanho = frutas.ValidarPropriedadeString(frutas.Tamanho, "Tamanho");
            if (validarDescricao && validarTamanho)
            {
                await _frutas.AdicionarFrutasSemEF(frutas);
            }
        }

        public async Task AtualizarFrutaSemEF(Frutas frutas)
        {
            var validarDescricao = frutas.ValidarPropriedadeString(frutas.Descricao, "Descrição");
            var validarTamanho = frutas.ValidarPropriedadeString(frutas.Tamanho, "Tamanho");
            if (validarDescricao && validarTamanho)
            {
                await _frutas.AtualizarFrutaSemEF(frutas);
            }
        }

        public async Task<Frutas> BuscarPorId(int id)
        {
            if(id == null)
            {
                return null;
            }

            return await _frutas.BuscarPorId(id);

        }

        public async Task DeletarFruta(int id)
        {
            await _frutas.DeletarFruta(id);
        }

        public async Task<List<Frutas>> ListarFrutasSemEF()
        {
            var listaFrutas =  await _frutas.ListarFrutasSemEF();

            if (listaFrutas == null)
            {
                return null;
            }
            return listaFrutas;
        }
    }
}
