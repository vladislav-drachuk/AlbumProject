using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WebLayer.Controllers;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class StoreControllerTest
        {
            [TestMethod]
            public void IndexViewResultNotNull()
            {
               NavigationController controller = new NavigationController();

                ViewResult result = controller.Search() as ViewResult;

                Assert.IsNotNull(result);
            }

            [TestMethod]
            public void IndexViewEqualIndexCshtml()
            {
                NavigationController controller = new NavigationController();

                ViewResult result = controller.Search() as ViewResult;

                Assert.AreEqual("NavigationImageView", result.ViewName);
            }

 
        }
    }
}
