using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter
    {
        private StreamReader _readerStream;
        private StreamWriter _writerStream;

        [Flags]
        public enum Mode { Read = 1, Write = 2 }; //nested enum

        public void Open(string fileName, Mode mode)
        {
            try
            {
                if (mode == Mode.Read && File.Exists(fileName))
                {
                    _readerStream = new StreamReader(fileName);
                }
                else
                    if (mode == Mode.Write)
                    {
                        FileStream file = new FileStream(fileName, FileMode.OpenOrCreate);

                        _writerStream = new StreamWriter(file);
                    }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Write(params string[] columns)
        {
            try
            {
                string outPut = "";

                for (int i = 0; i < columns.Length; i++)
                {
                    outPut += columns[i];
                    if ((columns.Length - 1) != i)
                    {
                        outPut += "\t";
                    }
                }
                if (!_writerStream.Equals(StreamWriter.Null))
                {
                    _writerStream.WriteLine(outPut);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Read(out string column1, out string column2)
        {
            try
            {
                const int FIRST_COLUMN = 0;
                const int SECOND_COLUMN = 1;

                string line = string.Empty;
                column1 = column2 = null;

                while ((line = _readerStream.ReadLine()) != null) // looping through each line
                {
                    string[] columns = line.Split('\t');
                    if (columns.Length > 0)
                    {
                        column1 = columns[FIRST_COLUMN];
                        column2 = columns[SECOND_COLUMN];

                        return true;
                    }
                    else
                        return false;
                }

                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Close()
        {
            if (_writerStream != null)
            {
                _writerStream.Close();
            }

            if (_readerStream != null)
            {
                _readerStream.Close();
            }
        }
    }
}