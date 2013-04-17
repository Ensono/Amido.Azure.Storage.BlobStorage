using System;

namespace Amido.Azure.Storage.BlobStorage.Wrappers
{
    public interface IBlobPropertiesWrapper
    {
        string ContentType { get; }

        DateTime LastModifiedUtc { get; }
    }
}