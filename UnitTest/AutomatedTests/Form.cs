using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;
using Xamarin.UITest;

namespace UnitTest.AutomatedTests {
    class Form {
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
                AutomatedTests.AdministratorAcceptionTest.Tests test = new AdministratorAcceptionTest.Tests(Platform.Android);
                test.BeforeEachTest();
            }

            [Test]
            public void ShowFormDetail() {
                app.Tap(e => e.Marked("ButtonFormulariosAppMasterPageDetail"));
                app.Tap("Form que vai dar bom");
                Assert.IsNotNull(app.Query("Chama, chama, chama"));
                app.ScrollDown();
                app.ScrollDown();
                Assert.IsNotNull(app.Query("Submeter"));
            }

            [Test]
            public void DeleteForm() {
                app.Tap("Formulários");
                app.Tap("xablau");
                app.Tap("Deletar Formulário");
                app.Tap("Sim");
                Assert.IsNotNull(app.Query("OK"));
            }
        }
    }
}
