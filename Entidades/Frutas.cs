using Entidades.Notificacoes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Frutas: Notifica
    {
        [Column("SLT_ID")]
        public int Id { get; set; }
        [Column("SLT_Descricao")]
        public string? Descricao { get; set; }
        [Column("SLT_Tamanho")]
        public string? Tamanho { get; set; }
        [Column("SLT_Cor")]
        public string? Cor { get; set; }
   
    }
}
