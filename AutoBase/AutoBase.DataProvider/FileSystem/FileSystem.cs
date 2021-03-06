﻿using AutoBase.Common;
using AutoBase.DAL.AutoBaseEntities;
using System;
using System.IO;
using System.Linq;

namespace AutoBase.DataProvider.FileSystem
{
    public class FileSystem : IFileSystem, IDisposable
    {
        private IDataProvider _dataProvider;
        public FileSystem(IDataProvider provider)
        {
            _dataProvider = provider;
        }

        public string UploadFileToDestination(string filePath, Dump dump)
        {
            // make/year/model/module/memory/
            CheckDump(dump);

            var directory = dump.Make.Name;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            directory += $"/{dump.Year}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            directory += $"/{dump.Model.ModelName}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            directory += $"/{dump.Module.Name}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            directory += $"/{dump.Memory}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // file upload 
            var fileName = filePath.Split('\\').Last();
            File.Copy(filePath, directory + $"/{fileName}");
            return $"{directory}/{fileName}";
        }

        private void CheckDump(Dump dump)
        {
            if (dump == null) throw new Exception(Constants.Errors.InvalidDump);

            if (dump.Year <= 0) throw new Exception(Constants.Errors.InvalidYear);

            if (dump.Make == null)
            {
                var makeName = _dataProvider.Dal.Find<Make>(dump.MakeId).Name;
                if (makeName != null) dump.Make = new Make { Name = makeName };
                else throw new Exception(Constants.Errors.InvalidMake);

                if (dump.Model == null)
                {
                    var modelName = _dataProvider.Dal.Find<Model>(dump.ModelId).ModelName;
                    if (modelName != null) dump.Model = new Model { ModelName = modelName };
                    else throw new Exception(Constants.Errors.InvalidModel);
                }
            }

            if (dump.Memory == null)
            {
                throw new Exception(Constants.Errors.InvalidMemory);
            }

            if (dump.Module == null)
            {
                var module = _dataProvider.Dal.Find<Module>(dump.ModuleId);
                if (module != null)
                    dump.Module = module;
                else
                    throw new Exception();
            }
        }

        public void Dispose()
        {
            _dataProvider.Dispose();
            GC.SuppressFinalize(this);
        }

        public async void DeleteFile(Dump dump)
        {
            if (File.Exists(dump.Path))
            {
                File.Delete(dump.Path);
                await _dataProvider.RemoveDump(dump);
            }
        }

        public void GetFile(string dest, Dump dump)
        {
            if (File.Exists(dump.Path))
            {
                var fileName = dump.Path.Split('/').Last();
                File.Copy(dump.Path, dest + $"\\{fileName}");
            }
        }
    }
}