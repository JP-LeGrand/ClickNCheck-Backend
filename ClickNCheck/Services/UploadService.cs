using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.IO;

namespace ClickNCheck.Services
{
    public class UploadService
    {
        //The method below uploads a file onto azure blob storage
        public async Task<bool> UploadToBlob(string filename, byte[] docBuffer = null, Stream stream = null)
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
            new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
            "clicknchecksite",
            "khXgiAz2xr1NeLQj6MzrHDPiW4wKGFMC02jGdUYfzsa47jjL2qpBXMOvT0dXmMwiwPguypyxmT2qGoUUM5ZwQA=="), true);
            CloudBlobContainer cloudBlobContainer = null;

            try
            {
                // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'organisation-contracts' and append a GUID value to it to make the name unique. 
                cloudBlobContainer = cloudBlobClient.GetContainerReference("organisation-contract");

                // Set the permissions so the blobs are public. 
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);

                // Get a reference to the blob address, then upload the file to the blob.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);

                if (docBuffer != null)
                {
                    // OPTION A: use imageBuffer (converted from memory stream)
                    await cloudBlockBlob.UploadFromByteArrayAsync(docBuffer, 0, docBuffer.Length);
                }
                else if (stream != null)
                {
                    // OPTION B: pass in memory stream directly
                    await cloudBlockBlob.UploadFromStreamAsync(stream);
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (StorageException ex)
            {
                return false;
            }
            //finally
            //{
            //    // OPTIONAL: Clean up resources, e.g. blob container
            //    //if (cloudBlobContainer != null)
            //    //{
            //    //    await cloudBlobContainer.DeleteIfExistsAsync();
            //    //}
            //}
            //}
            //else
            //{
            //    return false;
            //}

        }

        //The method below downloads a file from azure blob storage
        public async Task DownloadFromBlob(string filename, byte[] imageBuffer = null, Stream stream = null)
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
            new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
            "clicknchecksite",
            "khXgiAz2xr1NeLQj6MzrHDPiW4wKGFMC02jGdUYfzsa47jjL2qpBXMOvT0dXmMwiwPguypyxmT2qGoUUM5ZwQA=="), true);

            CloudBlobContainer cloudBlobContainer = null;

            try
            {
                // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'organisation-contracts' and append a GUID value to it to make the name unique. 
                cloudBlobContainer = cloudBlobClient.GetContainerReference("organisation-contract" + Guid.NewGuid().ToString());
                await cloudBlobContainer.CreateAsync();

                // Set the permissions so the blobs are public. 
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);

                // Get a reference to the blob address, then upload the file to the blob.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);

                // Save the blob contents to a file named "myfile".
                using (var fileStream = System.IO.File.OpenWrite(@"path\myfile"))
                {
                    await cloudBlockBlob.DownloadToStreamAsync(fileStream);
                }

            }
            catch (StorageException ex)
            {

            }
        }

        public async Task<string> UploadImage(Guid userGuid, Guid orgGuid, string filename, byte[] docBuffer = null, Stream stream = null)
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
            new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
            "clicknchecksite",
            "khXgiAz2xr1NeLQj6MzrHDPiW4wKGFMC02jGdUYfzsa47jjL2qpBXMOvT0dXmMwiwPguypyxmT2qGoUUM5ZwQA=="), true);
            CloudBlobContainer container = null;
            string blobUri = "";
            try
            {
                // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'organisation-contracts' and append a GUID value to it to make the name unique. 
                container = blobClient.GetContainerReference($"{orgGuid.ToString()}");

                await container.CreateIfNotExistsAsync();
                // Set the permissions so the blobs are public. 
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await container.SetPermissionsAsync(permissions);
                // Get a reference to the blob address, then upload the file to the blob.
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"Images/{userGuid.ToString() + filename}");

                if (docBuffer != null)
                {
                    // OPTION A: use imageBuffer (converted from memory stream)
                    await blockBlob.UploadFromByteArrayAsync(docBuffer, 0, docBuffer.Length);
                    blobUri = blockBlob.Uri.AbsoluteUri;
                }
                else if (stream != null)
                {
                    // OPTION B: pass in memory stream directly
                    await blockBlob.UploadFromStreamAsync(stream);
                }
                else
                {
                    return blobUri;
                }

                return blobUri;
            }
            catch (StorageException ex)
            {
                return blobUri;
            }
        }
    }
}
