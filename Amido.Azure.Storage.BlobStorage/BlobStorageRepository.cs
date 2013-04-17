using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Amido.Azure.Storage.BlobStorage.Domain;
using Amido.Azure.Storage.BlobStorage.Extensions;
using Amido.Azure.Storage.BlobStorage.RetryPolicy;
using Amido.Azure.Storage.BlobStorage.Wrappers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Amido.Azure.Storage.BlobStorage
{
    public class BlobStorageRepository : IBlobStorageRepositoryContext, IBlobStorageRepository
    {
        private BlobStorageContext blobStorageContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobStorageRepository" /> class.
        /// </summary>
        public BlobStorageRepository()
        {
        }

        public IBlobStorageRepository SetContext(BlobStorageContext blobStorageContext)
        {
            this.blobStorageContext = blobStorageContext;
            return this;
        }

        public void DeleteDirectory(Uri uri, string containerName)
        {
            StorageRetryPolicy.RetryPolicy.ExecuteAction(() =>
            {
                var cloudBlobContainer = blobStorageContext.CloudBlobClient.GetContainerReference(containerName);

                var cloudBlockBlobs = ListBlobs(cloudBlobContainer);

                foreach (var cloudBlockBlob in cloudBlockBlobs)
                {
                    cloudBlockBlob.DeleteIfExists();
                }
            });
        }

        public IEnumerable<CloudBlockBlob> ListBlobs(CloudBlobContainer cloudBlobContainer)
        {
            return cloudBlobContainer.ListBlobs(null, true)
                .OfType<CloudBlockBlob>();
        }

        public IEnumerable<CloudBlobContainer> ListContainers()
        {
            return blobStorageContext.CloudBlobClient.ListContainers();
        }

        public byte[] GetBlobBytes(Uri uri, string containerName, string blobName)
        {
            byte[] blobBytes = null;

            StorageRetryPolicy.RetryPolicy.ExecuteAction(() =>
            {
                CloudBlobContainer container = blobStorageContext.CloudBlobClient.GetContainerReference(containerName);
                CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(blobName);

                using (var memoryStream = new MemoryStream())
                {
                    cloudBlockBlob.EndDownloadToStream(cloudBlockBlob.BeginDownloadToStream(memoryStream, null, null));
                    blobBytes = memoryStream.ToByteArray();
                }
            });

            return blobBytes;
        }

        public ICloudBlobWrapper Get(Uri uri, string containerName, string blobName)
        {
            StorageRetryPolicy.RetryPolicy.ExecuteAction(() =>
            {
                CloudBlobContainer container = blobStorageContext.CloudBlobClient.GetContainerReference(containerName);
                var cloudBlockBlob = container.GetBlockBlobReference(blobName);
                return new CloudBlobWrapper(cloudBlockBlob);
            });

            return null;
        }

        public void Save(string blobName, string containerName, byte[] bytes, string contentType = "application/octect-stream")
        {
            StorageRetryPolicy.RetryPolicy.ExecuteAction(() =>
            {
                CloudBlobContainer container = blobStorageContext.CloudBlobClient.GetContainerReference(containerName);
                CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(blobName);

                cloudBlockBlob.Properties.ContentType = contentType;

                using (var memoryStream = new MemoryStream(bytes))
                {
                    cloudBlockBlob.UploadFromStream(memoryStream);
                }
            });
        }

        private void CreateContainerIfNotExists(string containerName)
        {
            var container = blobStorageContext.CloudBlobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
        } 
    }
}
