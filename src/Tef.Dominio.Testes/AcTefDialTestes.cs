using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tef.Dominio.Testes
{
    [TestClass]
    public class AcTefDialTestes
    {
        [TestMethod]
        [TestCategory(nameof(AcTefDial))]
        public void Testar_Requisicao_ATV()
        {
            var acTefDial = CriaAcTefDial();

            acTefDial.Inicializa();

            var retorno = acTefDial.Atv();

            Assert.AreEqual(true, retorno != null);
        }

        [TestMethod]
        [TestCategory(nameof(AcTefDial))]
        public void Testar_Requisicao_CNC()
        {
            var acTefDial = CriaAcTefDial();

            acTefDial.Inicializa();

            var retorno = acTefDial.Cnc("REDECARD", "17230215595", new DateTime(2018, 12, 04, 17, 23, 02), 50);

            Assert.AreEqual(true, retorno != null);
        }

        [TestMethod]
        [TestCategory(nameof(AcTefDial))]
        public void Testar_Requisicao_ADM()
        {
            var acTefDial = CriaAcTefDial();

            acTefDial.Inicializa();

            var retorno = acTefDial.Adm();

            Assert.AreEqual(true, retorno != null);
        }

        [TestMethod]
        [TestCategory(nameof(AcTefDial))]
        public void Testar_Requisicao_CRT()
        {
            var acTefDial = CriaAcTefDial();

            acTefDial.Inicializa();

            var retorno = acTefDial.Crt(10m, "98393");

            Assert.AreEqual(true, retorno != null);
        }

        [TestMethod]
        [TestCategory(nameof(AcTefDial))]
        [ExpectedException(typeof(AcTefException))]
        public void Testar_Operacao_Sem_Inicializar()
        {
            var acTefDial = CriaAcTefDial();

            acTefDial.Cnc("REDECARD", "17230215595", new DateTime(2018, 12, 04, 17, 23, 02), 50);
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