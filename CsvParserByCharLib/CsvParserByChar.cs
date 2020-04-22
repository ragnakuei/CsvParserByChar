using System;
using System.IO;

namespace CsvParserByCharLib
{
    public class CsvParserByChar : IDisposable
    {
        private readonly StreamReader _streamReader;

        private const Char _endOfFile = (Char)65535;
        
        public CsvParserByChar(Stream stream)
        {
            _streamReader = new StreamReader(stream);
        }

        public string Read()
        {
            var result = string.Empty;
            
            var next = (char)_streamReader.Read();

            while (next != _endOfFile)
            {
                result += next;
                
                next = (char)_streamReader.Read();
            }

            return result;
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }
    }
}