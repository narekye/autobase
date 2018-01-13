using AutoBase.DAL.AutoBaseEntities;

namespace AutoBase.DataProvider.FileSystem
{
    public interface IFileSystem
    {
        string UploadFileToDestination(string filePath, Dump dump);
    }
}
