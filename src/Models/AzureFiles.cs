using Microsoft.Azure.Storage.File;

public class AzureFileShareClient : IFileClient
{
    private CloudFileClient _fileClient;
 
    public AzureFileShareClient(string connectionString)
    {
        var account = CloudStorageAccount.Parse(connectionString);
        _fileClient = account.CreateCloudFileClient();
    }
 
    public async Task DeleteFile(string storeName, string filePath)
    {
        var share = _fileClient.GetShareReference(storeName);
        var folder = share.GetRootDirectoryReference();
        var pathParts = filePath.Split('/');
        var fileName = pathParts[pathParts.Length - 1];
 
        for (var i = 0; i < pathParts.Length - 2; i++)
        {
            folder = folder.GetDirectoryReference(pathParts[i]);
            if(! await folder.ExistsAsync())
            {
                return;
            }
        }
 
        var fileRef = folder.GetFileReference(fileName);
 
        await fileRef.DeleteIfExistsAsync();
    }
 
    public async Task<bool> FileExists(string storeName, string filePath)
    {
        var share = _fileClient.GetShareReference(storeName);
        var folder = share.GetRootDirectoryReference();
        var pathParts = filePath.Split('/');
        var fileName = pathParts[pathParts.Length - 1];
 
        for (var i = 0; i < pathParts.Length - 2; i++)
        {
            folder = folder.GetDirectoryReference(pathParts[i]);
            if (!await folder.ExistsAsync())
            {
                return await Task.FromResult(false);
            }
        }
 
        var fileRef = folder.GetFileReference(fileName);
 
        return await fileRef.ExistsAsync();
    }
 
    public async Task<Stream> GetFile(string storeName, string filePath)
    {
        var share = _fileClient.GetShareReference(storeName);
        var folder = share.GetRootDirectoryReference();
        var pathParts = filePath.Split('/');
        var fileName = pathParts[pathParts.Length - 1];
 
        for (var i = 0; i < pathParts.Length - 2; i++)
        {
            folder = folder.GetDirectoryReference(pathParts[i]);
            if (!await folder.ExistsAsync())
            {
                return null;
            }
        }
 
        var fileRef = folder.GetFileReference(fileName);
        if(!await fileRef.ExistsAsync())
        {
            return null;
        }
 
        return await fileRef.OpenReadAsync();
    }
 
    public async Task<string> GetFileUrl(string storeName, string filePath)
    {
        return await Task.FromResult((string)null);
    }
  
    public async Task SaveFile(string storeName, string filePath, Stream fileStream)
    {
        var share = _fileClient.GetShareReference(storeName);
        var folder = share.GetRootDirectoryReference();
        var pathParts = filePath.Split('/');
        var fileName = pathParts[pathParts.Length - 1];
 
        for (var i = 0; i < pathParts.Length - 2; i++)
        {
            folder = folder.GetDirectoryReference(pathParts[i]);
 
            await folder.CreateIfNotExistsAsync();
        }
 
        var fileRef = folder.GetFileReference(fileName);
 
        await fileRef.UploadFromStreamAsync(fileStream);
    }
}