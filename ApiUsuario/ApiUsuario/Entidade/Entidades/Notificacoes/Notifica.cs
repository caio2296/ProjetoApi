namespace Entidades.Notificacoes
{
    public class Notifica
    {
        public Notifica()
        {
                Notificacoes = new List<Notifica>();
        }
        protected string? NomeDaPropriedade { get; set; }

        protected string? Mensagem { get; set; }

        protected List<Notifica> Notificacoes { get; set; }
        
        public bool ValidarPropriedadeString(string valor, string nomeDaPropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomeDaPropriedade))
            {
                Notificacoes.Add(new Notifica()
                {
                    Mensagem= "Campo Obrigatório!",
                    NomeDaPropriedade = nomeDaPropriedade
                });
                return false;
            }
            return true;
        }

        public bool ValidarPropriedadeDecimal(decimal valor, string nomeDaPropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomeDaPropriedade))
            {
                Notificacoes.Add(new Notifica()
                {
                    Mensagem = "Campo Obrigatório!",
                    NomeDaPropriedade = nomeDaPropriedade

                });
                return false;
            }
            return true;
        }
    }
}
