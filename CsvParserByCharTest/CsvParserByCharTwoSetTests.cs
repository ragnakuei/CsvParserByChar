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
        public void FirstSet()
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
        public void SecondSet()
        {
            var str = "AA,BB";
            var stream = StringStreamProvider.ToStream(str);

            var target = new CsvParserByChar(stream);
            target.Read();
            var actual = target.Read();

            var expected = "BB";

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 第三組字元含換行符號
        /// </summary>
        [Test]
        public void ThreeSetWithNewLine()
        {
            var str = @"AA,BB,C
C";
            var stream = StringStreamProvider.ToStream(str);

            var target = new CsvParserByChar(stream);
            target.Read();
            target.Read();
            var actual = target.Read();

            var expected = @"C
C";

            Assert.AreEqual(expected, actual);
        }
    }
}