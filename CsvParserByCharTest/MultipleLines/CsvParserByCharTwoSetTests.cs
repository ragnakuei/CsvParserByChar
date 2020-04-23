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
        /// 一欄位二行空字元
        /// </summary>
        [Test]
        public void FirstProperty()
        {
            var str = @"
";
            var stream = StringStreamProvider.ToStream(str);

            var target = new CsvParserByChar(stream);

            var actual = target.Read();
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }
    }
}