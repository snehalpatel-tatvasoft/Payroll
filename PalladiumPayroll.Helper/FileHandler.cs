using Microsoft.AspNetCore.Http;

namespace PalladiumPayroll.Helper
{
    public static class FileHandler
    {
        public static bool CreateDirectory(string directory)
        {
            if (!DirectoryExists(directory))
            {
                Directory.CreateDirectory(directory);
                return true;
            }
            return false;
        }

        public static bool DeleteDirectory(string directory)
        {
            if (DirectoryExists(directory))
            {
                Directory.Delete(directory);
                return true;
            }
            return false;
        }

        public static bool DirectoryExists(string directoryName)
        {
            return Directory.Exists(directoryName);
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static async Task<bool> UploadFile(string filePath, IFormFile file)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return true;
            }
        }

        public static async Task<bool> UploadAllFiles(string filePath, List<IFormFile> file)
        {
            foreach (var singleFile in file)
            {
                var singleFilePath = Path.Combine(filePath, singleFile.FileName);
                DeleteFile(singleFilePath);
                await UploadFile(singleFilePath, singleFile);
            }
            return true;
        }

        public static bool RenameFile(string oldFilePath, string newFilePath)
        {
            DeleteFile(newFilePath);
            File.Move(oldFilePath, newFilePath);
            return true;
        }

        public static bool DeleteFile(string filePath)
        {
            if (FileExists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        public static string CombinePath(string basePath, string filePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), basePath, filePath).Replace("/", Path.DirectorySeparatorChar.ToString());
        }

        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static async Task<byte[]> ReadFileBytes(string filePath)
        {
            byte[] fileBytes = { };
            if (FileExists(filePath))
            {
                fileBytes = await File.ReadAllBytesAsync(filePath);
            }
            return fileBytes;
        }

    }
}
