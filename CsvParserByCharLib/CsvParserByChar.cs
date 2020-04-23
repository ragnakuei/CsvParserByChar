using System;
using System.IO;

namespace CsvParserByCharLib
{
    public class CsvParserByChar : IDisposable
    {
        public CsvParserByChar(Stream stream)
        {
            _streamReader = new StreamReader(stream);
        }

        public string Read()
        {
            if (_isReadNextResult)
            {
                _isReadNextResult = false;
                return _nextResult;
            }

            var result = string.Empty;

            var peekNext = (char)_streamReader.Peek();

            switch (peekNext)
            {
                case _doubleQuote:
                    result = ParseStartWithDoubleQuota();
                    break;
                case _carriageReturn:
                    result = ParseStartWithCarriageReturn();
                    break;
                case _endOfFile:
                {
                    // ,[end of file]
                    if (_isPreviousIsDelimiter)
                    {
                        AssignNextResultIsEndOfFile();
                        break;
                    }

                    // 最後一行為空白行時才會執行到這
                    result = null;
                    break;
                }
                default:
                    result = ParseNormal();
                    break;
            }
            
            return result;
        }

        /// <summary>
        /// 解析正常字串
        /// </summary>
        private string ParseNormal()
        {
            _isPreviousIsDelimiter = false;
            
            var next = (char)_streamReader.Read();
            var result = string.Empty;

            while (true)
            {
                if (next == _endOfFile)
                {
                    AssignNextResultIsEndOfFile();
                    break;
                }

                if (next == _delimiter)
                {
                    _isPreviousIsDelimiter = true;
                    break;
                }

                if (next == _carriageReturn)
                {
                    var peekNext = (char)_streamReader.Peek();
                    if (peekNext == _lineFeed)
                    {
                        // 把 LineFeed 讀出來
                        _streamReader.Read();
                        
                        _nextResult = _newLine;
                        _isReadNextResult = true;
                        break;
                    }
                }

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
            _isPreviousIsDelimiter = false;
            
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
                    {
                        // 連續二個雙引號
                        _streamReader.Read(); // 濾掉第二個雙引號
                        result += next;
                    }
                    else if (next == _doubleQuote
                          && peekNext == _endOfFile)
                    {
                        // 結尾雙引號
                        _streamReader.Read(); // 濾掉結尾雙引號

                        AssignNextResultIsEndOfFile();

                        break;
                    }
                    else if (next == _doubleQuote
                          && peekNext == _delimiter)
                    {
                        // ",
                        _streamReader.Read(); // 濾掉 Comma
                        _isPreviousIsDelimiter = true;
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

            var peekNext = _streamReader.Peek();
            if (peekNext == _lineFeed)
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


        private void AssignNextResultIsEndOfFile()
        {
            _isReadNextResult = true;
            _nextResult = null;
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }

        private readonly StreamReader _streamReader;
        private const Char _endOfFile = (Char)65535;
        private const Char _delimiter = ',';
        private const Char _doubleQuote = '"';
        private const Char _carriageReturn = '\r';
        private const Char _lineFeed = '\n';
        private const string _newLine = "\r\n";

        /// <summary>
        /// 用來判斷 ,[end of file] 情境 
        /// </summary>
        private bool _isPreviousIsDelimiter;
        
        private bool _isReadNextResult;
        private string _nextResult;
    }
}