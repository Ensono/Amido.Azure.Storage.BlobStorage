using System;
using System.Collections.Generic;
using Amido.Azure.Storage.BlobStorage.Wrappers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Amido.Azure.Storage.BlobStorage
{
    public interface IBlobStorageRepository 
    {
        void DeleteDirectory(Uri uri, string containerName);
        IEnumerable<CloudBlockBlob> ListBlobs(CloudBlobContainer cloudBlobContainer);
        IEnumerable<CloudBlobContainer> ListContainers();
        byte[] GetBlobBytes(Uri uri, string containerName, string blobName);
        void Save(string blobName, string containerName, byte[] bytes, string contentType = "application/octect-stream");
        ICloudBlobWrapper Get(Uri uri, string containerName, string blobName);
    }
}