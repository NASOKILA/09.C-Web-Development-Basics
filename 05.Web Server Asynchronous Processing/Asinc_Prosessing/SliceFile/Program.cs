namespace SliceFile
{
    using System;
    using System.IO;
    using System.Drawing;
    using static System.Net.Mime.MediaTypeNames;

    public class Program
    {
        static void Main(string[] args)
        {
        
        }

        static void Slice(string sourceFile, string destinationPath) {

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (FileStream source = new FileStream(sourceFile, FileMode.Open)) 
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                long partLength = (source.Length / parts) + 1;
            }
        }
    }
}
