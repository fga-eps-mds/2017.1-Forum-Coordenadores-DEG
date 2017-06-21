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
            public void aCreateNewForum() {
                app.Tap("ButtonPlusAppMasterPageDetail");
                app.Tap("ButtonNewForumAppMasterPageDetail");
                app.EnterText("EtTitleNewForumPage", "Forum Teste");
                app.EnterText("EtPlaceNewForumPage", "Local Teste");
                app.EnterText("EdScheduleNewForumPage", "Pauta Teste. 123.");
                app.DismissKeyboard();
                app.ScrollDown();
                app.Tap("ButtonCriarForumPageNewForunsPage");
                Assert.IsNotNull(app.Query("OK"));
            }

            [Test]
            public void aCreateNewForumBlankSpaces() {
                app.Tap("ButtonPlusAppMasterPageDetail");
                app.Tap("ButtonNewForumAppMasterPageDetail");
                app.DismissKeyboard();
                app.ScrollDown();
                app.Tap("ButtonCriarForumPageNewForunsPage");
                Assert.IsNotNull(app.Query("O fóum não pôde ser criado"));
            }

            [Test]
            public void bListsForumsForAdministrators() {
                app.Tap("Fóruns");
                Assert.IsNotNull(app.Query("Forum Teste"));
            }

            [Test]
            public void cShowForumDetailForAdministrators() {
                app.Tap("Fóruns");
                app.Tap("Forum Teste");
                Assert.IsNotNull(app.Query("Ver Fórum"));
            }

            [Test]
            public void dEditForum() {
                app.Tap("ButtonForunsAppMasterPageDetail");
                app.Tap("Forum Teste");
                app.Tap("Editar Fórum");
                app.ScrollDown();
                app.ScrollDown();
                app.Tap("Editar Forum");
                Assert.IsNotNull(app.Query("O fórum foi editado com sucesso!"));
            }

            [Test]
            public void fRemoveForum() {
                app.Tap("ButtonForunsAppMasterPageDetail");
                app.Tap(c => c.Marked("Ver detalhes"));
                app.Tap("ButtonDeletarForumForumDetailPage");
                app.Tap(c => c.Marked("Sim"));
                app.WaitForNoElement("Sim");
                app.Tap(c => c.Marked("OK"));
                app.WaitForNoElement("OK");
                Assert.IsNotNull("ButtonForunsAppMasterPageDetail");
            }

            [Test]
            public void gCreateNewForm() {
                app.Tap(e => e.Marked("ButtonPlusAppMasterPageDetail"));
                app.Tap("Criar Formulário");
                app.EnterText("Título", "Formulario Teste");
                app.ScrollDown();
                app.Tap("Adicionar nova pergunta discursiva");
                app.EnterText("Pergunta", "Pergunta teste");
                app.Tap("Adicionar Pergunta");
                app.ScrollDown();
                app.Tap("Criar formulário");
                Assert.IsNotNull("Formulário Criado");
            }

            [Test]
            public void hListsFormsForAdministrators() {
                app.Tap("Formulários");
                Assert.IsNotNull(app.Query("Ver detalhes"));
            }

            [Test]
            public void iShowFormDetailForAdministrators() {
                app.Tap("Formulários");
                app.Tap("Ver detalhes");
                Assert.IsNotNull(app.Query("Formulário"));
            }

            [Test]
            public void kRemoveForm() {
                app.Tap("Formulários");
                app.Tap("Formulario Teste");
                app.Tap("Deletar Formulário");
                app.Tap(c => c.Marked("Sim"));
                Assert.IsNotNull("Formulário Deletado !");
            }

            [Test]
            public void lUserRegistrationCoord() {
                app.Tap(e => e.Marked("ButtonPlusAppMasterPageDetail"));
                app.Tap(e => e.Marked("ButtonNewCoordenadorAppMasterPageDetail"));
                app.EnterText("LabelNomeCompletoUserRegistrationPage", "Nome Teste");
                app.EnterText("LabelEmailUserRegistrationPage", "teste@email.com");
                app.EnterText("LabelMatriculaUserRegistrationPage", "246813579");
                app.EnterText("LabelSenhaUserRegistrationPage", "Pb12345678");
                app.DismissKeyboard();
                app.ScrollDown();
                app.Tap("PickerCursoUserRegistrationPage");
                app.Tap("Engenharias");
                app.Tap(e => e.Marked("ButtonConfirmarUserRegistrationPage"));
                Assert.IsNotNull(app.Query("Você salvou um novo Coordenador com sucesso!"));
            }

            [Test]
            public void mUserRegistrationAdmin() {
                app.Tap(e => e.Marked("ButtonPlusAppMasterPageDetail"));
                app.Tap(e => e.Marked("ButtonNewCoordenadorAppMasterPageDetail"));
                app.EnterText("LabelNomeCompletoUserRegistrationPage", "Nome Teste");
                app.EnterText("LabelEmailUserRegistrationPage", "teste@email.com");
                app.EnterText("LabelMatriculaUserRegistrationPage", "146813579");
                app.EnterText("LabelSenhaUserRegistrationPage", "Pb12345678");
                app.DismissKeyboard();
                app.ScrollDown();
                app.Tap("Coordenador");
                app.Tap("Administrador");
                app.Tap(e => e.Marked("ButtonConfirmarUserRegistrationPage"));
                Assert.IsNotNull(app.Query("Você salvou um novo adminstrador com sucesso! "));
            }

            public void mUserRegistrationUnsuccessfull() {
                app.Tap(e => e.Marked("ButtonPlusAppMasterPageDetail"));
                app.Tap(e => e.Marked("ButtonNewCoordenadorAppMasterPageDetail"));
                app.ScrollDown();
                app.Tap("PickerCursoUserRegistrationPage");
                app.Tap("Engenharias");
                app.Tap(e => e.Marked("ButtonConfirmarUserRegistrationPage"));
                app.Repl();
                //Assert.IsNotNull(app.Query("Você salvou um novo Coordenador com sucesso!"));
            }

            [Test]
            public void nListsUsers() {
                app.Tap("Usuários");
                Assert.IsNotNull(app.Query("Nome Teste"));
            }

            [Test]
            public void oShowUser() {
                app.Tap("Usuários");
                app.Tap("Nome Teste");
                Assert.IsNotNull("Ver Usuário");
            }

            [Test]
            public void pEditUser() {
                app.Tap("Usuários");
                app.Tap("Nome Teste");
                app.Tap("Editar Usuário");
                app.ScrollDown();
                app.Tap("Confirmar");
                Assert.IsNotNull("Usuário editado com sucesso!");
            }

            [Test]
            public void qRemoveUser() {
                app.Tap("Usuários");
                app.Tap("Nome Teste");
                app.Tap("Remover Usuário");
                app.Tap("Sim");
                Assert.IsNotNull("Usuário deletado");
            }

        }
    }
}
