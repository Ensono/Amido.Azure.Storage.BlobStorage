using Amido.Azure.Storage.BlobStorage.Account;
using Amido.Azure.Storage.BlobStorage.Domain;

namespace Amido.Azure.Storage.BlobStorage
{
    public interface IBlobStorageRepositoryContext
    {
        IBlobStorageRepository SetContext(BlobStorageContext blobStorageContext);
    }
}