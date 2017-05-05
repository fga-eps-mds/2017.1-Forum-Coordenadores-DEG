using ForumDEG.ViewModels;
using ForumDEG.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest {
    class CoordinatorMasterPageTests {
        private CoordinatorMasterPageViewModel _viewModel;
        List<Forum> list1;
        List<Forum> list2;
        Forum tomorrow;
        Forum f2;
        Forum f3;

    [SetUp]
        public void Setup() {
            _viewModel = new CoordinatorMasterPageViewModel();

            list1 = new List<Forum>();
            list2 = new List<Forum>();

            tomorrow = new Forum();
            f2 = new Forum();
            f3 = new Forum();

            tomorrow.Date = DateTime.Now.AddDays(1);
            list1.Add(tomorrow);

            f2.Date = DateTime.Now.AddDays(2);
            list1.Add(f2);

            f3.Date = DateTime.Now.AddDays(-1);
            list2.Add(f3);
        }

        private List<Forum> GetList1() {
            return list1;
        }

        [Test()]
        public void SelectNextForum_ReturnTheNextForum() { 
            Assert.AreSame(_viewModel.SelectNextForum(list1),tomorrow);
        }

        [Test()]
        public void SelectNextForum_ThereIsNoNextForum() {
            Assert.Null(_viewModel.SelectNextForum(list2));
        }
    }
}
