using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;
using Xamarin.UITest;

namespace UnitTest.AutomatedTests {
    class CoordinatorAutomatedTest {

        [TestFixture(Platform.Android)]
        [TestFixture(Platform.iOS)]
        public class Tests {
            IApp app;
            Platform platform;

            public Tests(Platform platform) {
                this.platform = platform;

            }

            [SetUp]
            public void BeforeEachTest() {
                app = AppInitializer.StartApp(platform);

                // Opening coordinator account
                app.EnterText("EtRegistrationLoginPage", "123456789");
                app.EnterText("EtPasswordLoginPage", "Pb1234567");
                app.Tap("BtLoginLoginPage");
            }

            [Test]
            public void bListsForumsForCoordinators() {
                app.Tap("Fóruns");
                Assert.IsNotNull(app.Query("Forum Teste"));
            }

            [Test]
            public void cShowForumDetailForCoordinators() {
                app.Tap("Fóruns");
                app.Tap("Forum Teste");
                Assert.IsNotNull(app.Query("Ver Fórum"));
            }

            [Test]
            public void eInformPresence() {
                app.Tap(c=>c.Marked("Fóruns"));
                app.Tap("Forum Teste");
                app.Tap("ButtonConfirmPresenceForumDetailPage");
                Assert.IsNotNull(app.Query("PageForumDetailPage"));
            }

            [Test]
            public void hListsFormsForCoordinators() {
                app.Tap("Formulários");
                Assert.IsNotNull(app.Query("Ver detalhes"));
            }

            [Test]
            public void iShowFormDetailForCoordinators() {
                app.Tap("Formulários");
                app.Tap("Ver detalhes");
                Assert.IsNotNull(app.Query("Formulário"));
            }

            /*
            [Test]
            public void jSubmitForm() {
                app.Tap("Formulários");
                app.Tap("Ver detalhes");
                app.EnterText("Resposta", "Resposta");
                app.ScrollDown();
                app.ScrollDown();
                app.ScrollDown();
                app.ScrollDown();
                app.Tap("Submeter");
                Assert.IsNotNull(app.Query("Submeter"));
            }
            */
        }
    }
}
