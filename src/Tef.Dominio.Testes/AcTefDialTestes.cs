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
            var acTefDial = CriaAcTefDial();

            acTefDial.Inicializa();

            var retorno = acTefDial.Atv();

            Assert.AreEqual(true, retorno != null);
        }

        private static AcTefDial CriaAcTefDial()
        {
            var requisicao = new AcTefRequisicaoFake(new ConfigRequisicao());

            var acTefDial = new AcTefDial(requisicao, new ConfigAcTefDial(
                "teste",
                "versaoTeste",
                "nomeTesteAutomacao",
                "83838",
                false, false, false, false
            ));
            return acTefDial;
        }
    }
}