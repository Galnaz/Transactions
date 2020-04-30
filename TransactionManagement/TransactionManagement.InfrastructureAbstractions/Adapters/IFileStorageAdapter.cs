using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManagement.InfrastructureAbstractions.Adapters
{
    public interface IFileStorageAdapter
    {
        Task<Stream> DownloadFile(string name);
        Task<string> DownloadAsString(string name);
        Task<string> Upload(Stream fileStream, string name);
    }

}
