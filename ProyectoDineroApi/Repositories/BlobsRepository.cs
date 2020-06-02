using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDineroApi.Repositories
{
    public class BlobsRepository
    {
        private CloudBlobContainer container;
        public BlobsRepository()
        {
            String keys = "UseDevelopmentStorage=true";
            CloudStorageAccount account = CloudStorageAccount.Parse(keys);
            CloudBlobClient client = account.CreateCloudBlobClient();
            this.container = client.GetContainerReference("moneystorage");
            this.container.CreateIfNotExistsAsync();
        }

        public String GetToken()
        {
            SharedAccessBlobPolicy authorization = new SharedAccessBlobPolicy();
            authorization.SharedAccessExpiryTime = DateTime.Now.AddHours(1);
            authorization.Permissions = SharedAccessBlobPermissions.Read
                | SharedAccessBlobPermissions.List
                | SharedAccessBlobPermissions.Write;
            String token = this.container.GetSharedAccessSignature(authorization);
            return this.container.Uri + token;
        }
    }
}
