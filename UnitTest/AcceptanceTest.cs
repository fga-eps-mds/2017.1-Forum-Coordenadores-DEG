using NUnit.Framework;
using System;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;


namespace UnitTest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.StartApp();
            }
            return ConfigureApp.iOS.StartApp();
        }
    }

    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]

    public class AcceptanceTest
    {
        IApp app;
        Platform platform;

        [SetUp]
        public void Setup()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void CanSeeLoginButton()
        {
            app.Repl();
        }
    }
}
