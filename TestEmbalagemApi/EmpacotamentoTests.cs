using EmbalagemApi.Application.Comand;
using EmbalagemApi.Controllers;
using EmbalagemApi.Models;
using EmbalagemApi.View;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace EmbalagemApi.Tests
{
    [TestClass]
    public class EmpacotamentoControllerTests
    {
        private EmpacotamentoController _controller;
        private Mock<IMediator> _mediatorMock;

        [TestInitialize]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EmpacotamentoController(_mediatorMock.Object);
        }

        //[TestMethod]
        //public async Task EmpacotarPedidos_DeveRetornarPedidosEmpacotadosNoFormatoEsperado()
        //{
        //    var Pedidos = new List<Pedido>
        //{
        //    new Pedido
        //    {
        //        pedido_id = 1,
        //        produtos = new List<Produto>
        //        {
        //            new Produto { produto_id = "PS5", dimensoes = new Dimensao { altura = 40, largura = 10, comprimento = 25 }},
        //            new Produto { produto_id = "Volante", dimensoes = new Dimensao { altura = 40, largura = 30, comprimento = 30 }}
        //        }
        //    }
        //};

        //    var resultadoEsperado = new List<PedidoEmpacotadoSaida>
        //    {
        //        new PedidoEmpacotadoSaida
        //        {
        //            pedido_Id = 1,
        //            caixas = new List<CaixaEmpacotada>
        //            {
        //                new CaixaEmpacotada
        //                {
        //                    caixa_id = "Caixa 2",
        //                    produtos = new List<string> { "PS5", "Volante" }
        //                }
        //            }
        //        }
        //    };

        //    _mediatorMock
        //        .Setup(m => m.Send(It.IsAny<EmpacotarPedidosCommand>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(resultadoEsperado);

        //    // Act
        //    var result = await _controller.EmpacotarPedidos(Pedido);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        //    var okResult = result as OkObjectResult;
        //    Assert.IsNotNull(okResult);
        //    Assert.AreEqual(new SaidaPedidos { pedidos = resultadoEsperado }, okResult.Value);
        //    _mediatorMock.Verify(m => m.Send(It.IsAny<EmpacotarPedidosCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        //}


        [TestMethod]
        public async Task EmpacotarPedidos_DeveChamarMediatorERetornarResultado()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido
                {
                    pedido_id = 1245,
                    produtos = new List<Produto>
                    {
                        new Produto { produto_id = "PS5", dimensoes = new Dimensao{ altura = 40, largura = 10, comprimento = 25 } },
                        new Produto { produto_id = "Volante",  dimensoes = new Dimensao{ altura = 40, largura = 30, comprimento = 30 } }
                    }
                }
            };



            var resultadoEsperado = new List<PedidoEmpacotado>
            {
                new PedidoEmpacotado
                {
                    pedido_id = 1245,
                    CaixasEmpacotadas = new List<(Caixa, List<Produto>)>
                    {
                        (new Caixa { Id = "Caixa1", Altura = 30, Largura = 40, Comprimento = 80 },
                         new List<Produto>
                         {
                             new Produto { produto_id = "Produto A", dimensoes = new Dimensao{ altura = 30, largura = 20, comprimento = 10 } },
                             new Produto { produto_id = "Produto B",  dimensoes = new Dimensao{ altura = 25, largura = 15, comprimento = 5 } }
                         })
                    }
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<EmpacotarPedidosCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoEsperado);

            var result = await _controller.EmpacotarPedidos(pedidos);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult)); 
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            _mediatorMock.Verify(m => m.Send(It.IsAny<EmpacotarPedidosCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Nenhuma caixa disponível para o produto.")]
        public async Task EmpacotarPedidos_DeveLancarExcecao_SeProdutoNaoCabemNasCaixas()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido
                {
                    pedido_id = 1246,
                    produtos = new List<Produto>
                    {
                        new Produto { produto_id = "Produto C", dimensoes = new Dimensao{ altura = 90, largura = 70, comprimento = 50 } } 
                    }
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<EmpacotarPedidosCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Nenhuma caixa disponível para o produto."));


            await _controller.EmpacotarPedidos(pedidos);
        }
    }
}
