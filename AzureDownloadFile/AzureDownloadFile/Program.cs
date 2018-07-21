using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AzureDownloadFile
{
    class Program
    {
        const string AccountName = "harikrishnan";
        const string AccountKey = "CzLLQ7Noze4DNaWGEUy1wX9aDHP0685KOGliLsasaMaJfYJoRg11bfDtSVN7UrqZ3tHTYGyomSg/PUyl/s1EiA==";
        static void Main(string[] args)
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(AccountName, AccountKey), true);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("newcontainer");
            var blobs = container.ListBlobs();
            DownloadBlobs(blobs);
            Console.WriteLine("Download is Completed");
        }
        public static void DownloadBlobs(IEnumerable<IListBlobItem> blobs)
        {
            foreach (var file in blobs)
            {
                if (file is CloudBlockBlob blockBlob)
                {
                    blockBlob.DownloadToFileAsync(Path.Combine(@"C:\Users\hari.dileepkumar\Documents\Neudesic\DownloadFile", blockBlob.Name), FileMode.Create);
                    Console.WriteLine(blockBlob.Name);
                }
                else if (file is CloudBlobDirectory blobDirectory)
                {
                    Directory.CreateDirectory(blobDirectory.Prefix);
                    Console.WriteLine("Create Directory : " + blobDirectory.Prefix);
                    DownloadBlobs(blobDirectory.ListBlobs());
                }
            }
            
        }
    }
}
