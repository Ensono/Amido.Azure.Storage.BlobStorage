using System;
using System.IO;
using Amido.Azure.Storage.BlobStorage.Extensions;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Amido.Azure.Storage.BlobStorage.Wrappers
{
    public class CloudBlobWrapper : ICloudBlobWrapper
    {
        private readonly CloudBlockBlob cloudBlockBlob;

        public CloudBlobWrapper(CloudBlockBlob cloudBlockBlob)
        {
            this.cloudBlockBlob = cloudBlockBlob;
        }

        public IBlobPropertiesWrapper Properties
        {
            get { return new BlobPropertiesWrapper(cloudBlockBlob.Properties); }
        }

        public Uri Uri
        {
            get { return cloudBlockBlob.Uri; }
        }

        public string Name
        {
            get { return cloudBlockBlob.Name; }
        }

        public byte[] ToBytes()
        {
            using (var memoryStream = new MemoryStream())
            {
                cloudBlockBlob.EndDownloadToStream(cloudBlockBlob.BeginDownloadToStream(memoryStream, null, null));
                return memoryStream.ToByteArray();
            }
        }

        public string ToText()
        {
            using (var memoryStream = new MemoryStream())
            {
                cloudBlockBlob.EndDownloadToStream(cloudBlockBlob.BeginDownloadToStream(memoryStream, null, null));
                return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}