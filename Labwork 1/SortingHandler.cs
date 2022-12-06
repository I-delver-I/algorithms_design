using System.IO.MemoryMappedFiles;

namespace Labwork_1
{
    public class SortingHandler
    {
        private FileHandler _fileHandler;

        public SortingHandler(FileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        public void QuickSort(long[] a, int lo, int hi)
        {
            if (lo >= hi)
            {
                return;
            }

            var lt = lo; var i = lo + 1; var gt = hi;
            var t = a[lo];

            while (i <= gt)
            {
                var cmp = a[i].CompareTo(t);
                if (cmp < 0) 
                { 
                    Exchange(a, i++, lt++); 
                }
                else if (cmp > 0) 
                { 
                    Exchange(a, i, gt--); 
                }
                else 
                { 
                    i++; 
                }
            }

            QuickSort(a, lo, lt - 1);
            QuickSort(a, gt + 1, hi);
        }

        public void Exchange(long[] a, int i, int j)
        {
            var t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public void QuickSort(long[] a)
        {
            for (var i = 0; i < a.Length - 1; i++)
            {
                if (a[i] > a[i + 1])
                {
                    QuickSort(a, 0, a.Length - 1);
                    break;
                }
            }            
        }

        /// <param name="sectionSize">count of Mb (numbers count in DEBUG mode)</param>
        public void DistributeFileOnSeries(int sectionSize)
        {
            int elementsCount;   

            if (Program.Debug)
            {
                elementsCount = sectionSize; 
                sectionSize *= sizeof(long);
            }
            else
            {
                sectionSize *= FileHandler.Mb;
                elementsCount = sectionSize / sizeof(long);
            }
            
            using var afile = new FileStream(FileHandler.AfilePath, FileMode.Create);
            using var bfile = new FileStream(FileHandler.BfilePath, FileMode.Create);
            var currentFile = new BinaryWriter(afile);
            
            using (var initialFile = MemoryMappedFile
                .CreateFromFile(FileHandler.InitialFilePath, FileMode.Open))
            {
                using (var accessor = initialFile.CreateViewAccessor())
                {
                    var fileLength = FileHandler.InitialFileInfo.Length;

                    for (long i = 0; i < fileLength; i += (long)sectionSize)
                    {
                        var section = new long[elementsCount];
                        accessor.ReadArray<long>(i, section, 0, elementsCount);
                        QuickSort(section);
                        _fileHandler.WriteArrayToFile(currentFile, section);

                        if (currentFile.BaseStream == afile)
                        {
                            currentFile = new BinaryWriter(bfile);
                        }
                        else
                        {
                            currentFile = new BinaryWriter(afile);
                        }
                    }   
                }
            }
        }			
        
        /// <param name="sectionSize">count of Mb (numbers count in DEBUG mode)</param>
        public void PolyPhaseMerge(int sectionSize)
        {
            int sectionElementsCount;   

            if (Program.Debug)
            {
                sectionElementsCount = sectionSize; 
                sectionSize *= sizeof(long);
            }
            else
            {
                sectionSize *= FileHandler.Mb;
                sectionElementsCount = sectionSize / sizeof(long);
            }

            long serieSize = sectionSize;
            var inputFileSize = FileHandler.InitialFileInfo.Length / 2;

            var input1Name = FileHandler.AfilePath;
            var input1Map = MemoryMappedFile.CreateFromFile(input1Name);
            var input1Accessor = input1Map.CreateViewAccessor();

            var input2Name = FileHandler.BfilePath;
            var input2Map = MemoryMappedFile.CreateFromFile(input2Name);
            var input2Accessor = input2Map.CreateViewAccessor();

            File.Create(FileHandler.CfilePath).Close();
            File.Create(FileHandler.DfilePath).Close();

            var currentOutputName = FileHandler.CfilePath;
            var currentOutput = new BinaryWriter(File.Open(currentOutputName, FileMode.Open));
            var fileLength = FileHandler.InitialFileInfo.Length;

            while (serieSize != fileLength)
            {
                var input1Info = new FileInfo(input1Name);
                var input2Info = new FileInfo(input2Name);
                var currentOutputInfo = new FileInfo(currentOutputName);

                long firstMapPosition = 0; 
                long secondMapPosition = 0;
                var serieElementsCount = serieSize / sizeof(long);
                var input1Section = new long[sectionElementsCount];
                var input2Section = new long[sectionElementsCount];
                input1Accessor
                    .ReadArray(firstMapPosition, input1Section, 0, sectionElementsCount);
                input2Accessor
                    .ReadArray(secondMapPosition, input2Section, 0, sectionElementsCount);

                do
                {                    
                    MergeSeries(sectionElementsCount, input1Section, input2Section, currentOutput,
                        ref firstMapPosition, ref secondMapPosition, sectionSize, serieSize, 
                        input1Accessor, input2Accessor, inputFileSize);

                    currentOutput.Close();

                    ChangeOutput(ref currentOutputName, ref currentOutput);
                }
                while ((firstMapPosition != inputFileSize) && (secondMapPosition != inputFileSize));

                serieSize *= 2;

                currentOutput.Close();
                input1Accessor.Dispose();
                input1Map.Dispose();
                input2Accessor.Dispose();
                input2Map.Dispose();

                File.Open(input1Name, FileMode.Truncate).Close();
                File.Open(input2Name, FileMode.Truncate).Close();

                if (serieSize != fileLength)
                {
                    if (input1Name == FileHandler.AfilePath)
                    {
                        SwapFiles(ref input1Name, ref input1Map, ref input1Accessor, ref input2Name, 
                            ref input2Map, ref input2Accessor, ref currentOutputName, ref currentOutput, 
                            FileHandler.CfilePath, FileHandler.DfilePath, FileHandler.AfilePath);
                    }
                    else
                    {
                        SwapFiles(ref input1Name, ref input1Map, ref input1Accessor, ref input2Name, 
                            ref input2Map, ref input2Accessor, ref currentOutputName, ref currentOutput, 
                            FileHandler.AfilePath, FileHandler.BfilePath, FileHandler.CfilePath);
                    }
                }
            }          
        }

        public void ChangeOutput(ref string currentOutputName, ref BinaryWriter currentOutput)
        {
            if (currentOutputName == FileHandler.AfilePath)
            {
                currentOutputName = FileHandler.BfilePath;
                currentOutput = new BinaryWriter(File.Open(currentOutputName, FileMode.Append));
            }
            else if (currentOutputName == FileHandler.BfilePath)
            {
                currentOutputName = FileHandler.AfilePath;
                currentOutput = new BinaryWriter(File.Open(currentOutputName, FileMode.Append));
            }
            else if (currentOutputName == FileHandler.CfilePath)
            {
                currentOutputName = FileHandler.DfilePath;
                currentOutput = new BinaryWriter(File.Open(currentOutputName, FileMode.Append));
            }
            else if (currentOutputName == FileHandler.DfilePath)
            {
                currentOutputName = FileHandler.CfilePath;
                currentOutput = new BinaryWriter(File.Open(currentOutputName, FileMode.Append));
            }
        }

        public void SwapFiles(ref string input1Name, ref MemoryMappedFile input1Map,
            ref MemoryMappedViewAccessor input1Accessor, ref string input2Name,
            ref MemoryMappedFile input2Map, ref MemoryMappedViewAccessor input2Accessor, 
            ref string currentOutputName, ref BinaryWriter currentOutput, string newInput1Name,
            string newInput2Name, string newOutputName)
        {
            input1Name = newInput1Name;
            input1Map = MemoryMappedFile.CreateFromFile(input1Name);
            input1Accessor = input1Map.CreateViewAccessor();

            input2Name = newInput2Name;
            input2Map = MemoryMappedFile.CreateFromFile(input2Name);
            input2Accessor = input2Map.CreateViewAccessor();

            currentOutputName = newOutputName;
            currentOutput = new BinaryWriter(File.Open(currentOutputName, FileMode.Open));
        }

        public void MergeSeries(int sectionElementsCount, long[] input1Section, long[] input2Section,
            BinaryWriter currentOutput, ref long firstMapPosition, ref long secondMapPosition,
            long sectionSize, long serieSize, MemoryMappedViewAccessor input1Accessor,
            MemoryMappedViewAccessor input2Accessor, long inputFileSize)
        {
            long firstSectionIndex = 0;
            long secondSectionIndex = 0;
            long tempPosition1 = 0;
            long tempPosition2 = 0;

            do
            {
                if ((firstSectionIndex != sectionElementsCount) 
                    && (secondSectionIndex != sectionElementsCount))
                {
                    if (input1Section[firstSectionIndex] < input2Section[secondSectionIndex])
                    {
                        currentOutput.Write(input1Section[firstSectionIndex]);
                        firstSectionIndex++;
                    }
                    else
                    {
                        currentOutput.Write(input2Section[secondSectionIndex]);
                        secondSectionIndex++;
                    }
                }
                else if (firstSectionIndex == sectionElementsCount)
                {
                    firstSectionIndex = 0;

                    ProcessSectionEnding(sectionElementsCount, 
                        ref firstMapPosition, sectionSize, serieSize, input1Accessor, 
                        input1Section, ref secondSectionIndex, currentOutput, input2Section, 
                        ref secondMapPosition, input2Accessor, 
                        ref tempPosition1, ref tempPosition2, inputFileSize);
                }
                else if (secondSectionIndex == sectionElementsCount)
                {
                    secondSectionIndex = 0;

                    ProcessSectionEnding(sectionElementsCount, 
                        ref secondMapPosition, sectionSize, serieSize, input2Accessor, 
                        input2Section, ref firstSectionIndex, currentOutput, input1Section, 
                        ref firstMapPosition, input1Accessor, 
                        ref tempPosition2, ref tempPosition1, inputFileSize);
                }
            }
            while ((tempPosition1 != serieSize) && (tempPosition2 != serieSize));
        }

        public void ProcessSectionEnding(int sectionElementsCount, 
            ref long firstMapPosition, long sectionSize, long serieSize, 
            MemoryMappedViewAccessor input1Accessor, long[] input1Section,
            ref long secondSectionIndex, BinaryWriter currentOutput, long[] input2Section,
            ref long secondMapPosition, MemoryMappedViewAccessor input2Accessor, 
            ref long tempPosition1, ref long tempPosition2, long inputFileSize)
        {
            tempPosition1 = firstMapPosition % serieSize;
            tempPosition2 = secondMapPosition % serieSize;

            if (tempPosition1 != serieSize)
            {
                tempPosition1 += sectionSize;
            }

            firstMapPosition += sectionSize;

            if (firstMapPosition != inputFileSize)
            {
                input1Accessor.ReadArray(firstMapPosition, input1Section, 0, sectionElementsCount);
            }

            if (tempPosition1 == serieSize)
            {
                do
                {
                    for (var i = secondSectionIndex; i < sectionElementsCount; i++)
                    {
                        currentOutput.Write(input2Section[i]);
                    }

                    secondSectionIndex = 0;

                    if (tempPosition2 != serieSize)
                    {
                        tempPosition2 += sectionSize;
                    }

                    secondMapPosition += sectionSize;

                    if (secondMapPosition != inputFileSize)
                    {
                        input2Accessor.ReadArray(secondMapPosition, input2Section, 0, 
                            sectionElementsCount);
                    }
                }
                while (tempPosition2 != serieSize);
            }
        }        
    }
}