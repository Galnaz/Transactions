using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TransactionManagement.InfrastructureAbstractions.Adapters;

namespace TransactionManagement.Infrastructure.FileStorage
{
    public class BlobStorageAdapter : IFileStorageAdapter
    {
        private StorageFactory _storageFactory;
        private readonly string _containerName;

        public BlobStorageAdapter(StorageFactory factory, IOptions<StorageConfig> options)
        {
            _containerName = options.Value.BlobContainer;
            _storageFactory = factory;
        }

        public async Task<string> Upload(Stream fileStream, string name)
        {
            var _cloudBlobContainer = await _storageFactory.GetCloudBlobContainer(_containerName);
            var blob = _cloudBlobContainer.GetBlockBlobReference(name);
            
            await blob.UploadFromStreamAsync(fileStream);

            return $"{_cloudBlobContainer.Uri.AbsoluteUri}/{name}";
        }

        public async Task<Stream> DownloadFile(string name)
        {
            var _cloudBlobContainer = await _storageFactory.GetCloudBlobContainer(_containerName);
            var resultStream = new MemoryStream();
            await _cloudBlobContainer.CreateIfNotExistsAsync();
            var blobFile = _cloudBlobContainer.GetBlobReference(name);
            var fileExists = await blobFile.ExistsAsync();
            if (!fileExists)
                return null;
            await blobFile.DownloadToStreamAsync(resultStream);
            
            return await blobFile.OpenReadAsync();
        }

        public async Task<string> DownloadAsString(string name)
        {
            var _cloudBlobContainer = await _storageFactory.GetCloudBlobContainer(_containerName);
            await _cloudBlobContainer.CreateIfNotExistsAsync();
            var blobFile = _cloudBlobContainer.GetBlockBlobReference(name);
            var fileExists = await blobFile.ExistsAsync();
            if (!fileExists)
                return null;
            return await blobFile.DownloadTextAsync();
        }
    }
}
