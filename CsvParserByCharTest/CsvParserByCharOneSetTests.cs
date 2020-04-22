using CsvParserByCharLib;
using NUnit.Framework;

namespace CsvParserByCharTest
{
    /// <summary>
    /// 一組字元
    /// </summary>
    public class CsvParserByCharOneSetTests
    {
        /// <summary>
        /// 一個字元
        /// </summary>
        [Test]
        public void OneChar()
        {
            var str = "A";
            var stream = StringStreamProvider.ToStream(str);
            
            var target = new CsvParserByChar(stream);
            var actual = target.Read();

            var expected = "A";
            
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 二個字元
        /// </summary>
        [Test]
        public void TwoChars()
        {
            var str = "AB";
            var stream = StringStreamProvider.ToStream(str);
            
            var target = new CsvParserByChar(stream);
            var actual = target.Read();

            var expected = "AB";
            
            Assert.AreEqual(expected, actual);
        }
    }
}