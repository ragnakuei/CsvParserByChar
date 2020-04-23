using System;
using System.IO;

namespace CsvParserByCharLib
{
    public class CsvParserByChar : IDisposable
    {
        private readonly StreamReader _streamReader;

        private const Char _endOfFile = (Char)65535;
        private const Char _delimiter = ',';
        private const Char _doubleQuote = '"';
        private const Char _carriageReturn = '\r';
        private const Char _lineFeed = '\n';

        public CsvParserByChar(Stream stream)
        {
            _streamReader = new StreamReader(stream);
        }

        public string Read()
        {
            var result = string.Empty;

            var peekNext = (char)_streamReader.Peek();

            if (peekNext == _doubleQuote)
            {
                result = ParseStartWithDoubleQuota();
            }
            else if (peekNext == _carriageReturn)
            {
                result = ParseStartWithCarriageReturn();
            }
            else 
            {
                result = ParseNormal();
            }

            return result;
        }

        /// <summary>
        /// 解析正常字串
        /// </summary>
        private string ParseNormal()
        {
            var next = (char)_streamReader.Read();
            var result = string.Empty;
            
            while (next != _endOfFile
                && next != _delimiter)
            {
                result += next;

                next = (char)_streamReader.Read();
            }

            return result;
        }

        /// <summary>
        /// 解析雙引號
        /// </summary>
        private string ParseStartWithDoubleQuota()
        {
            var result = string.Empty;
            
            // 判斷 + 直接濾掉起始雙引號
            if (_streamReader.Read() == _doubleQuote)
            {
                var next = (char)_streamReader.Read();
                var peekNext = (char)_streamReader.Peek();

                while (true)
                {
                    if (next == _doubleQuote
                     && peekNext == _doubleQuote)
                    {   // 連續二個雙引號
                        _streamReader.Read(); // 濾掉第二個雙引號
                        result += next;
                    }
                    else if (next == _doubleQuote
                     && peekNext == _endOfFile)
                    {   // 結尾雙引號
                        _streamReader.Read(); // 濾掉結尾雙引號
                        break;
                    }
                    else if (next == _doubleQuote
                          && peekNext == _delimiter)
                    {   // ",
                        _streamReader.Read(); // 濾掉 Comma
                        break;
                    }
                    else
                    {
                        result += next;
                    }
                    
                    next = (char)_streamReader.Read();
                    peekNext = (char)_streamReader.Peek();
                }
            }

            return result;
        }

        /// <summary>
        /// 以 CarriageReturn 開頭
        /// <para>如果是換行 \r\n 就回傳 null</para>
        /// </summary>
        private string ParseStartWithCarriageReturn()
        {
            var carriageReturn = ((char)_streamReader.Read()).ToString();
            
            var next = _streamReader.Peek();
            if (next == _lineFeed)
            {
                _streamReader.Read();
                return null;
            }
            else
            {
                var result = carriageReturn + Read();
                return result;
            }
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }
    }
}