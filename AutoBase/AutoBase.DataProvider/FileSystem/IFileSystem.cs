using AutoBase.DAL.AutoBaseEntities;

namespace AutoBase.DataProvider.FileSystem
{
    public interface IFileSystem
    {
        string UploadFileToDestination(string filePath, Dump dump);
        void DeleteFile(Dump dump);
        void GetFile(string dest, Dump dump);
    }
}
