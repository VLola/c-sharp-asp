namespace Project_72.Models
{
    public class File
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public long Size { get; set; }
        public File(string name, DateTime time, long size)
        {
            Name = name;
            Time = time;
            Size = size / 1024;
            if (Size < 1) Size = 1;
        }
    }
}
