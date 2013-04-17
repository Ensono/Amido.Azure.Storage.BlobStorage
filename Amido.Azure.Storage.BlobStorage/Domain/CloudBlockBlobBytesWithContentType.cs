namespace Amido.Azure.Storage.BlobStorage.Domain
{
    public class CloudBlockBlobBytesWithContentType
    {
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }

        public CloudBlockBlobBytesWithContentType(byte[] bytes, string contentType)
        {
            Bytes = bytes;
            ContentType = contentType;
        }
    }
}