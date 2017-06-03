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
            public void EnterForumDetailAndConfirmPresence() {
                app.Tap(c=>c.Marked("Foruns"));
                app.Tap(c => c.Marked("Funciona"));
                app.Tap("ButtonConfirmPresenceForumDetailPage");
                Assert.IsNotNull(app.Query("PageForumDetailPage"));
            }
        }
    }
}
