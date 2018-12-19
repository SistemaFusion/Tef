using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tef.Dominio.Testes
{
    [TestClass]
    public class AcTefDialTeste
    {
        [TestMethod]
        public void TesteAtv()
        {
            var requisicao = new AcTefRequisicaoFake(new ConfigRequisicao
            {
                
            });

            var acTefDial = new AcTefDial(requisicao, new ConfigAcTefDial(
                "teste",
                "versaoTeste",
                "nomeTesteAutomacao",
                "83838",
                false, false, false, false
                ));

            var retorno = acTefDial.Atv();

            Assert.AreEqual(true, retorno != null);
        }
    }
}
