using System;
using Amido.Azure.Storage.BlobStorage.Account;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Amido.Azure.Storage.BlobStorage.Domain
{
    public class BlobStorageContext
    {
        public BlobStorageContext(AccountConfiguration accountConfiguration)
        {
            var cloudStorageAccount = GetCloudStorageAccountByConfigurationSetting(accountConfiguration.AccountName);
            CloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        }

        public BlobStorageContext(AccountConnection accountConnection)
        {
            var cloudStorageAccount = GetCloudStorageAccountByConnectionString(accountConnection.ConnectionString);
            CloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        }

        public CloudBlobClient CloudBlobClient { get; private set; }

        /// <summary>
        /// Gets the cloud storage account by configuration setting.
        /// </summary>
        /// <param name="configurationSetting">The configuration setting.</param>
        /// <returns>CloudStorageAccount.</returns>
        /// <exception cref="System.InvalidOperationException">Unable to find cloud storage account</exception>
        private static CloudStorageAccount GetCloudStorageAccountByConfigurationSetting(string configurationSetting)
        {
            try
            {
                return CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(configurationSetting));
            }
            catch (Exception error)
            {
                throw new InvalidOperationException("Unable to find cloud storage account", error);
            }
        }

        /// <summary>
        /// Gets the cloud storage account by connection string.
        /// </summary>
        /// <param name="storageConnectionString">The storage connection string.</param>
        /// <returns>CloudStorageAccount.</returns>
        /// <exception cref="System.InvalidOperationException">Unable to find cloud storage account</exception>
        private static CloudStorageAccount GetCloudStorageAccountByConnectionString(string storageConnectionString)
        {
            try
            {
                return CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (Exception error)
            {
                throw new InvalidOperationException("Unable to find cloud storage account", error);
            }
        }
    }
}