using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Enums;
using SecurionPay.Request;
using SecurionPay.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
        public async Task UploadImageTest()
        {
            var response = await Upload("img.jpg");
            Assert.AreEqual(response.Type, "jpg");
            Assert.AreEqual(response.Purpose, FileUploadPurpose.DisputeEvidence);
        }

        /// <summary>
        /// test for upload file
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task UploadPdfTest()
        {
            var response = await Upload("test.pdf");
            Assert.AreEqual(response.Type, "pdf");
            Assert.AreEqual(response.Purpose, FileUploadPurpose.DisputeEvidence);
        }

        private async Task<FileUpload> Upload(string file)
        {
            var document = GetTestDocument(file);
            var request = new FileUploadRequest() { Purpose = FileUploadPurpose.DisputeEvidence, File = document, FileName = file };
            return await _gateway.CreateFileUpload(request);
        }

        private byte[] GetTestDocument(string file)
        {
            return File.ReadAllBytes(string.Format("TestUploadFiles\\{0}",file));
        }
    }
}
