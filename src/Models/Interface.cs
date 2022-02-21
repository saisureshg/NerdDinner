using System;
using System.Collections.Generic;

public interface IFileClient
{
    Task DeleteFile(string storeName, string filePath);        
    Task<bool> FileExists(string storeName, string filePath);
    Task<Stream> GetFile(string storeName, string filePath);
    Task<string> GetFileUrl(string storeName, string filePath);
    Task SaveFile(string storeName, string filePath, Stream fileStream);
}