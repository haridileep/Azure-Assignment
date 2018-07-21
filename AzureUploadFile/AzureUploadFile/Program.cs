using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.IO;

namespace AzureUploadFile
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("newcontainer");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("New.txt");
            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\hari.dileepkumar\Documents\Neudesic\UploadFile\New.txt"))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
    }
}
