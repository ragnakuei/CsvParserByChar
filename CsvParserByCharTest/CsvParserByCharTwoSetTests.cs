using CsvParserByCharLib;
using NUnit.Framework;

namespace CsvParserByCharTest
{
    /// <summary>
    /// 二組字元
    /// </summary>
    public class CsvParserByCharTwoSetTests
    {
        /// <summary>
        /// 第一組字元
        /// </summary>
        [Test]
        public void OneSet()
        {
            var str = "AA,BB";
            var stream = StringStreamProvider.ToStream(str);
            
            var target = new CsvParserByChar(stream);
            var actual = target.Read();

            var expected = "AA";
            
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 第二組字元
        /// </summary>
        [Test]
        public void TwoSets()
        {
            var str = "AA,BB";
            var stream = StringStreamProvider.ToStream(str);
            
            var target = new CsvParserByChar(stream);
            var actual = target.Read();
            actual = target.Read();

            var expected = "BB";
            
            Assert.AreEqual(expected, actual);
        }
    }
}