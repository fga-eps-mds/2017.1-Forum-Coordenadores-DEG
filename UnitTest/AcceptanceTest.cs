using NUnit.Framework;
using System;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;


namespace UnitTest
{
    [TestFixture]
    class AcceptanceTest
    {
        private string path = "";
        private AndroidApp app;

        [SetUp]
        public void Setup()
        {
            app = ConfigureApp.Android.ApkFile(path).StartApp();
        }

        [Test]
        public void CanSeeLoginButton()
        {
            app.Repl();
        }
    }
}
