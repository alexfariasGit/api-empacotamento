using EmbalagemApi.Models;

namespace EmbalagemApi.Services
{
    public class ServicoEmpacotamento
    {
        private List<Caixa> _caixasDisponiveis = new List<Caixa> { TiposCaixa.Caixa1, TiposCaixa.Caixa2, TiposCaixa.Caixa3 };

        public List<(Caixa, List<Produto>)> EmpacotarPedido(Pedido pedido)
        {
            var caixasEmpacotadas = new List<(Caixa, List<Produto>)>();

            foreach (var produto in pedido.produtos.OrderByDescending(p => p.dimensoes.Volume))
            {
                var caixaExistente = EncontrarCaixaExistente(produto, caixasEmpacotadas);

                if (caixaExistente != null)
                {
                    caixaExistente.Value.Item2.Add(produto);
                }
                else
                {
                    var novaCaixa = EncontrarCaixaAdequada(produto);

                    if (novaCaixa == null)
                    {
                        throw new Exception("Nenhuma caixa disponível para o produto.");
                    }
                    caixasEmpacotadas.Add((novaCaixa, new List<Produto> { produto }));
                }
            }

            return caixasEmpacotadas;
        }

        private (Caixa, List<Produto>)? EncontrarCaixaExistente(Produto produto, List<(Caixa, List<Produto>)> caixasEmpacotadas)
        {
            foreach (var (caixa, produtos) in caixasEmpacotadas)
            {
                double volumeOcupado = produtos.Sum(p => p.dimensoes.Volume);

                if (produto.dimensoes.altura <= caixa.Altura &&
                    produto.dimensoes.largura <= caixa.Largura &&
                    produto.dimensoes.comprimento <= caixa.Comprimento &&
                    (volumeOcupado + produto.dimensoes.Volume) <= caixa.Volume)
                {
                    return (caixa, produtos);
                }
            }

            return null;
        }

        private Caixa EncontrarCaixaAdequada(Produto produto)
        {
            return _caixasDisponiveis
                .FirstOrDefault(caixa => caixa.Altura >= produto.dimensoes.altura
                                      && caixa.Largura >= produto.dimensoes.largura
                                      && caixa.Comprimento >= produto.dimensoes.comprimento);
        }
    }


}
