namespace EmbalagemApi.Models
{
    public static class TiposCaixa
    {
        public static Caixa Caixa1 => new Caixa { Id = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 };
        public static Caixa Caixa2 => new Caixa { Id = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 };
        public static Caixa Caixa3 => new Caixa { Id = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 };
    }

}
