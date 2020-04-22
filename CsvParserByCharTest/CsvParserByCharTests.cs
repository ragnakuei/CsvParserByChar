using CsvParserByCharLib;
using NUnit.Framework;

namespace CsvParserByCharTest
{
    public class CsvParserByCharTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// 一個字元
        /// </summary>
        [Test]
        public void Sample01()
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
        public void Sample02()
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