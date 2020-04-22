using System.IO;
using System.Text;

namespace CsvParserByCharTest
{
    public static class StringStreamProvider
    {
        public static Stream ToStream(string str)
        {
            var byteArray = Encoding.ASCII.GetBytes( str );
            var stream = new MemoryStream( byteArray );
            return stream;
        }
    }
}