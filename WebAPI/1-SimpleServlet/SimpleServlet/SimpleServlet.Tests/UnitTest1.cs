using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using System.Net.Http.Formatting;

namespace SimpleServlet.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMsgEchoing()
        {
            String msg = "1234test";
            String expectedString = "Echo:" + msg;

            var controller = new Controllers.EchoController();

            var result = controller.GetEcho(msg);
            OkNegotiatedContentResult<string> conNegResult = result as OkNegotiatedContentResult<string>;

            Assert.AreEqual(expectedString, conNegResult.Content);
        }
    }
}
