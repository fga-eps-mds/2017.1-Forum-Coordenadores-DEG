using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;
using Xamarin.UITest;

namespace UnitTest.AutomatedTests {
    class AdministratorAcceptionTest {

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

                // Opening Administrator account
                app.EnterText("EtRegistrationLoginPage", "12345678");
                app.EnterText("EtPasswordLoginPage", "Pb1234567");
                app.Tap("BtLoginLoginPage");
                app.WaitForNoElement("BtLoginLoginPage");
            }

            [Test]
            public void CreateNewForum() {
                app.Tap("ButtonPlusAppMasterPageDetail");
                app.Tap("ButtonNewForumAppMasterPageDetail");

                app.EnterText("EtTitleNewForumPage", "Forum Teste");
                app.EnterText("EtPlaceNewForumPage", "Local Teste");
                app.EnterText("EdScheduleNewForumPage", "Pauta Teste. 123.");
                app.DismissKeyboard();
                app.ScrollDown();
                app.Tap("ButtonCriarForumPageNewForunsPage");
                app.WaitForElement("alertTitle");

                //app.Repl();
                Assert.IsNotNull(app.Query("OK"));
            }

            [Test]
            public void RemoveForum() {
                app.Tap("ButtonForunsAppMasterPageDetail");
                app.Tap(c => c.Marked("Ver detalhes"));
                app.Tap("ButtonDeletarForumForumDetailPage");
                app.Tap(c => c.Marked("Sim"));
                app.WaitForNoElement("Sim");
                Assert.IsNotNull("Fóruns");
            }

            [Test]
            public void ListsFormsForAdministrators() {
                app.Tap("Formulários");
                Assert.IsNotNull(app.Query("Ver detalhes"));
            }
        }
    }
}
