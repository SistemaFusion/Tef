using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tef.Dominio.Testes
{
    [TestClass]
    public class AcTefDialTestes
    {
        [TestMethod]
        [TestCategory("AcTefDial")]
        public void Testar_Requisicao_ATV()
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

            acTefDial.Inicializa();

            var retorno = acTefDial.Atv();

            Assert.AreEqual(true, retorno != null);
        }
    }
}