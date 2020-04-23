using CsvParserByCharLib;
using NUnit.Framework;

namespace CsvParserByCharTest.OneLine
{
    /// <summary>
    /// 一組字元
    /// </summary>
    public class CsvParserByCharOnePropertyTests
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
            
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 一個中文字
        /// </summary>
        [Test]
        public void FirstChineseProperty()
        {
            var str = "一";
            var stream = StringStreamProvider.ToStream(str);
            var target = new CsvParserByChar(stream);
            
            var actual = target.Read();
            var expected = "一";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = null;
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
              
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 包含換行字元
        /// </summary>
        [Test]
        public void MultipleCharsWithNewLine()
        {
            var str = @"""A
B""";
            var stream = StringStreamProvider.ToStream(str);
            var target = new CsvParserByChar(stream);
            
            var actual = target.Read();
            var expected = @"A
B";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 用雙引號包住一個字元
        /// </summary>
        [Test]
        public void EncloseOneCharInDoubleQuotes()
        {
            var str = @"""A""";
            var stream = StringStreamProvider.ToStream(str);
            var target = new CsvParserByChar(stream);
            
            var actual = target.Read();
            var expected = "A";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 用雙引號包住逗號字元
        /// </summary>
        [Test]
        public void EncloseCommaInDoubleQuotes()
        {
            var str = @""",""";
            var stream = StringStreamProvider.ToStream(str);
            var target = new CsvParserByChar(stream);
            
            var actual = target.Read();
            var expected = ",";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 用雙引號包住雙引號字元
        /// </summary>
        [Test]
        public void EncloseDoubleQuoteInDoubleQuotes()
        {
            var str = @"""" + @"""" + @"""" + @"""";
            var stream = StringStreamProvider.ToStream(str);
            var target = new CsvParserByChar(stream);
            
            var actual = target.Read();
            var expected = @"""";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// 用雙引號包住換行字元
        /// </summary>
        [Test]
        public void EncloseNewLineInDoubleQuotes()
        {
            var str = @"""" + "\r" + "\n" + @"""";
            var stream = StringStreamProvider.ToStream(str);
            var target = new CsvParserByChar(stream);
            
            var actual = target.Read();
            var expected = @"
";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = null;
            Assert.AreEqual(expected, actual);
        }
    }
}