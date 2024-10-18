using EmbalagemApi.Application.Comand;
using EmbalagemApi.View;

namespace EmbalagemApi.Extension
{
    public static class ListaPedidosExension
    {
        public static List<PedidoEmpacotadoSaida> ConvertToViewlModel(this ICollection<PedidoEmpacotado> item)
        {

            List<PedidoEmpacotadoSaida> result = new List<PedidoEmpacotadoSaida>();

            if (item is List<PedidoEmpacotado>)
            {
                result = item.Select(s => new PedidoEmpacotadoSaida()
                {
                   pedido_Id = s.pedido_id,
                   Erro = s.Erro,
                    caixas = s.CaixasEmpacotadas.Select(f => new CaixaEmpacotada()
                    {
                        caixa_id = f.Item1.Id,
                        produtos = f.Item2.Select(p => p.produto_id).ToList() 
                    }).ToList()
                }).ToList();
            }

            return result;
        }
    }
}
