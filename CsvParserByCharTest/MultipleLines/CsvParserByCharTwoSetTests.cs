using CsvParserByCharLib;
using NUnit.Framework;

namespace CsvParserByCharTest.MultipleLines
{
    /// <summary>
    /// 二行
    /// </summary>
    public class CsvParserByCharTwoLinesTests
    {
        /// <summary>
        /// 只有換行字元
        /// </summary>
        [Test]
        public void OnlyNewLineTwoLines()
        {
            var str = @"
";
            var stream = StringStreamProvider.ToStream(str);

            var target = new CsvParserByChar(stream);

            var actual = target.Read();
            string expected = null;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 一個值一組換行符號
        /// </summary>
        [Test]
        public void OneValueOneNewLine()
        {
            var str = @"A
";
            var stream = StringStreamProvider.ToStream(str);

            var target = new CsvParserByChar(stream);

            var actual = target.Read();
            string expected = "A";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = "\r\n";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
        }
    }
}