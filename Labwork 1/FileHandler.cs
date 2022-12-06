namespace Labwork_1
{
    public class FileHandler
    {
        public static int Mb = 1024 * 1024;
        public static string AfilePath = "A.bin";
        public static string BfilePath = "B.bin";
        public static string CfilePath = "C.bin";
        public static string DfilePath = "D.bin";
        public static string InitialFilePath = "initial.bin";
        public static FileInfo InitialFileInfo = new FileInfo(InitialFilePath);

        public void WriteArrayToFile(BinaryWriter bw, long[] arr)
        {
            foreach (var item in arr)
            {
                bw.Write(item);
            }
        }

        public void CreateFileWithRandomNumbers(int megabytesCount, string fileName)
        {
            long elementsCountMb = Mb / sizeof(long);

            using (var fs = new BinaryWriter(File.Create(fileName)))
            {
                var rd = new Random();

                for (long i = 0; i < elementsCountMb * megabytesCount; i++)
                {
                    fs.Write((long)rd.Next());
                }
            }
        }

        public void CreateFileWithRandomNumbers(string fileName, int elementsCount)
        {
            using (var fs = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
            {
                var rd = new Random();

                for (var i = 0; i < elementsCount; i++)
                {
                    fs.Write((long)rd.Next(1, 1000));
                }
            }
        }

        public long[] GetFileContent(string fileName)
        {
            var result = new List<long>();

            using (var br = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    result.Add(br.ReadInt64());
                }                
            }

            return result.ToArray();
        }
    }
}