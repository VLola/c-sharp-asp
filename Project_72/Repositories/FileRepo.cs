using static System.Net.WebRequestMethods;

namespace Project_72.Repositories
{
    public class FileRepo
    {
        string path = Directory.GetCurrentDirectory() + "/www/Files/";
        public FileRepo()
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
        public IEnumerable<string> GetAll()
        {
            return new DirectoryInfo(path).GetFiles().Select(item => item.Name);
        }
        public bool Add(IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);
            if (file.Length > 0)
            {
                using (var stream = System.IO.File.Create(RandomName(fi.Extension)))
                { 
                    file.CopyToAsync(stream);
                    return true;
                }
            }
            return false;
        }
        public bool Delete(string name)
        {
            if (System.IO.File.Exists(path + name))
            {
                System.IO.File.Delete(path + name);
                return true;
            }
            else return false;
        }
        public byte[] GetFile(string name)
        {
            if (System.IO.File.Exists(path + name))
            {

                return System.IO.File.ReadAllBytes(path + name);
            }
            else return null;
        }
        public string RandomName(string extenc)
        {
            while (true)
            {
                string name = Path.GetRandomFileName();
                string fileName = path + name + extenc;
                if (!System.IO.File.Exists(fileName))
                {
                    return fileName;
                }
            }
        }
    }
}
