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
        public void LoginSuccess() {
            app.EnterText("EtRegistrationLoginPage", "123456789");
            app.EnterText("EtPasswordLoginPage", "Pb1234567");
            app.Tap("BtLoginLoginPage");

            Assert.IsNotNull(app.Query("PageUsersPage"));
        }

        //[Test]
        //public void LoginFailure() {

        //    app.Tap("BtLoginLoginPage");

        //}
    }
}
