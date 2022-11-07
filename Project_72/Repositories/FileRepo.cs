using System.IO;

namespace Project_72.Repositories
{
    public class FileRepo
    {
        public FileRepo()
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
        string path = Directory.GetCurrentDirectory() + "/www/Files/";
        public string RandomName(string extenc)
        {
            while (true)
            {
                string name = Path.GetRandomFileName();
                string fileName = path + name + extenc;
                if (!File.Exists(fileName))
                {
                    return fileName;
                }
            }
        }
    }
}
