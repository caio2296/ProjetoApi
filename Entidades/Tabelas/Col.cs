using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tabelas
{
    public class Col
    {
        public int id_col_schema { get; set; }

        public string? text { get; set; }

        public int? order { get; set; }

        public int level { get; set; }

        public int? parent { get; set; }

        public bool? collapsed { get; set; }

        public ICollection<Col> cols { get; set; } = new List<Col>();

        public bool HasChildren => cols.Any();

        /// <summary>
        /// Retorna o maior level encontrado entre esta coluna e todas as filhas recursivamente.
        /// </summary>
        public int MaxLevel
        {
            get
            {
                if (!HasChildren)
                    return level;

                return cols.Max(c => c.MaxLevel);
            }
        }


        /// <summary>
        /// Retorna todos os descendentes recursivamente.
        /// </summary>
        public IEnumerable<Col> Flatten()
        {
            yield return this;

            foreach (var child in cols)
            {
                foreach (var descendant in child.Flatten())
                {
                    yield return descendant;
                }
            }
        }

        /// <summary>
        /// Retorna os elementos agrupados por nível.
        /// </summary>
        public Dictionary<int, List<Col>> GroupByLevel()
        {
            return Flatten()
                .GroupBy(c => c.level)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList()
                );
        }
    }

}
