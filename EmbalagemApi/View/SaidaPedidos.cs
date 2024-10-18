namespace EmbalagemApi.View
{
    public class CaixaEmpacotada
    {
        public string caixa_id { get; set; }
        public List<string> produtos { get; set; }
    }

    public class PedidoEmpacotadoSaida
    {
        public int pedido_Id { get; set; }
        public List<CaixaEmpacotada> caixas { get; set; }
        public string Erro { get; set; }
    }

    public class SaidaPedidos
    {
        public List<PedidoEmpacotadoSaida> pedidos { get; set; }
    }

}
