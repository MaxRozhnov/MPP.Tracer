using System.IO;

namespace TestMethods.Writer
{
    public class FileWriter : IWriter
    {
        public readonly string FileName;
        public void Write(string text)
        {
            File.WriteAllText(FileName, text);
        }

        public FileWriter(string fileName)
        {
            FileName = fileName;
        }
    }
}