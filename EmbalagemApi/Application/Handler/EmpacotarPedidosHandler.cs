using EmbalagemApi.Application.Comand;
using EmbalagemApi.Services;
using MediatR;


namespace EmbalagemApi.Application.Handler
{
    public class EmpacotarPedidosHandler : IRequestHandler<EmpacotarPedidosCommand, List<PedidoEmpacotado>>
    {
        private readonly ServicoEmpacotamento _servicoEmpacotamento;

        public EmpacotarPedidosHandler(ServicoEmpacotamento servicoEmpacotamento)
        {
            _servicoEmpacotamento = servicoEmpacotamento;
        }

        public Task<List<PedidoEmpacotado>> Handle(EmpacotarPedidosCommand request, CancellationToken cancellationToken)
        {
            var pedidosEmpacotados = new List<PedidoEmpacotado>();

            foreach (var pedido in request.Pedidos)
            {
                var caixasEmpacotadas = new List<(Models.Caixa, List<Models.Produto>)>();
                string Erro = string.Empty;

                try
                {
                    caixasEmpacotadas = _servicoEmpacotamento.EmpacotarPedido(pedido);
                }
                catch (Exception ex)
                {

                    Erro = ex.Message;
                }

                pedidosEmpacotados.Add(new PedidoEmpacotado { pedido_id = pedido.pedido_id, CaixasEmpacotadas = caixasEmpacotadas, Erro = Erro });
            }

            return Task.FromResult(pedidosEmpacotados);
        }
    }

}
