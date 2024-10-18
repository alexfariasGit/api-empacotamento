using EmbalagemApi.Models;

using MediatR;

namespace EmbalagemApi.Application.Comand
{
    public class EmpacotarPedidosCommand : IRequest<List<PedidoEmpacotado>>
    {
        public List<Pedido> Pedidos { get; }

        public EmpacotarPedidosCommand(List<Pedido> pedidos)
        {
            Pedidos = pedidos;
        }
    }

    public class PedidoEmpacotado
    {
        public int pedido_id { get; set; }
        public List<(Caixa, List<Produto>)> CaixasEmpacotadas { get; set; }

        public string Erro { get; set; }
    }

}
