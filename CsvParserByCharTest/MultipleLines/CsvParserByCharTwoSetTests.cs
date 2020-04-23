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
    }
}