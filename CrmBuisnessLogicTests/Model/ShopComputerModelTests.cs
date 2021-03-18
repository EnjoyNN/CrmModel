using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBuisnessLogic.Model;
using System.Threading;

namespace CrmBuisnessLogic.Model.Tests
{
    [TestClass()]
    public class ShopComputerModelTests
    {
        [TestMethod()]
        public void StartTest()
        {
            var model = new ShopComputerModel();
            model.Start();
            Thread.Sleep(10000);
        }
    }
}