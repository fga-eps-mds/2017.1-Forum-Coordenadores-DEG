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
            }

            [Test]
            public void CreateNewForum() {
                app.Tap("ButtonPlusAppMasterPageDetail");
                app.Tap("ButtonNewForumAppMasterPageDetail");

                Assert.IsNotNull("PageNewForunsPage");

                app.EnterText("EtTitleNewForumPage", "Forum Teste");
                app.EnterText("EtPlaceNewForumPage", "Local Teste");
                app.EnterText("EdScheduleNewForumPage", "Pauta Teste. 123.");
                app.Tap("ButtonCriarForumPageNewForunsPage");

                app.Repl();

            }

            [Test]
            public void RemoveForum() {
                app.Tap("ButtonForunsAppMasterPageDetail");

                Assert.IsNotNull("PageForumDetailPage");

                app.Tap(c => c.Marked("Forum Teste"));
                app.Tap("ButtonDeletarForumForumDetailPage");
                app.Tap(c => c.Marked("Sim"));
            }
        }
    }
}
