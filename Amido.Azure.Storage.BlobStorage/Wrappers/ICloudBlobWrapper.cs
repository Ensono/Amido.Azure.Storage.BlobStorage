using System;

namespace Amido.Azure.Storage.BlobStorage.Wrappers
{
    public interface ICloudBlobWrapper
    {
        IBlobPropertiesWrapper Properties { get; }

        Uri Uri { get; }

        string Name { get; }

        byte[] ToBytes();
    }
}