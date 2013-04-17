using System;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Amido.Azure.Storage.BlobStorage.Wrappers
{
    public class BlobPropertiesWrapper : IBlobPropertiesWrapper
    {
        private readonly BlobProperties blobProperties;

        public BlobPropertiesWrapper(BlobProperties blobProperties)
        {
            this.blobProperties = blobProperties;
        }

        public string ContentType
        {
            get { return blobProperties.ContentType; }
        }

        public DateTime LastModifiedUtc
        {
            get
            {
                if (blobProperties.LastModified.HasValue)
                {
                    return blobProperties.LastModified.Value.UtcDateTime;
                }

                return DateTime.MinValue;
            }
        }
    }
}