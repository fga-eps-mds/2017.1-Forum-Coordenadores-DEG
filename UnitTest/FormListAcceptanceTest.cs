using NUnit.Framework;
using System;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;


namespace UnitTest
{
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
        }

        [Test]
        public void ListsFormsForCoordinators() {
            app.EnterText("EtRegistrationLoginPage", "123456789");
            app.EnterText("EtPasswordLoginPage", "Pb1234567");
            app.Tap("BtLoginLoginPage");
            app.WaitForNoElement("BtLoginLoginPage");
            app.Tap("Formulários");
            Assert.IsNotNull(app.Query("Ver detalhes"));
        }

        [Test]
        public void ListsFormsForAdministrators() {
            app.EnterText("EtRegistrationLoginPage", "12345678");
            app.EnterText("EtPasswordLoginPage", "Pb1234567");
            app.Tap("BtLoginLoginPage");
            app.WaitForNoElement("BtLoginLoginPage");
            app.Tap("Formulários");
            Assert.IsNotNull(app.Query("Ver detalhes"));
        }

    }
}
