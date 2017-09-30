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
            var response = await Upload("img.jpg", FileUploadPurpose.DisputeEvidence);
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
            var response = await Upload("test.pdf", FileUploadPurpose.DisputeEvidence);
            Assert.AreEqual(response.Type, "pdf");
            Assert.AreEqual(response.Purpose, FileUploadPurpose.DisputeEvidence);
        }

        /// <summary>
        /// test for upload file
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ListUploadsTest()
        {
            var result = await _gateway.ListFileUpload();
        }

        /// <summary>
        /// test for upload file
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RetrieveUploadTest()
        {
            var newUpload = await Upload("test.pdf", FileUploadPurpose.DisputeEvidence);

            var retrievedUpload = await _gateway.RetrieveFileUpload(newUpload.Id);

            Assert.AreEqual(newUpload.Type, retrievedUpload.Type);
            Assert.AreEqual(newUpload.Id, retrievedUpload.Id);
            Assert.AreEqual(newUpload.Size, retrievedUpload.Size);

        }

        private async Task<FileUpload> Upload(string file,FileUploadPurpose purpose)
        {
            var document = GetTestDocument(file);
            var request = new FileUploadRequest() { Purpose = purpose, File = document, FileName = file };
            return await _gateway.CreateFileUpload(request);
        }

        private byte[] GetTestDocument(string file)
        {
            return File.ReadAllBytes(string.Format("TestUploadFiles\\{0}",file));
        }
    }
}
