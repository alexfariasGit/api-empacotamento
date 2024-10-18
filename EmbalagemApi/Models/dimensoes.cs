namespace EmbalagemApi.Models
{
    public class Dimensao
    {
        public int altura { get; set; }
        public int largura { get; set; }
        public int comprimento { get; set; }

        public virtual int Volume => altura * largura * comprimento;
    }
}
