using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Enums;
using SecurionPay.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    [TestClass]
    public class UploadTests : IntegrationTest
    {
        /// <summary>
        /// test for upload file
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task UploadTest()
        {
            var document = PrepareTestDocument();
            var request = new FileUploadRequest() { Purpose=FileUploadPurpose.DisputeEvidence,File= document };
            var response = await _gateway.CreateFileUpload(request);
        }

        private byte[] PrepareTestDocument()
        {
            return File.ReadAllBytes("TestUploadFiles\\Test.pdf");
        }
    }
}
