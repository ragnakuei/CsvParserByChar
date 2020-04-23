using CsvParserByCharLib;
using NUnit.Framework;

namespace CsvParserByCharTest
{
    public class SpecialCases
    {
        [Test]
        public void OnlyNewLineTwoLines()
        {
            var str = @"A,B,""""""""""""""www.google.com""""""""
""""""""我就是要,來搞你"""""""""""""",D
";
            var stream = StringStreamProvider.ToStream(str);

            var target = new CsvParserByChar(stream);

            var actual = target.Read();
            string expected = "A";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = "B";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = @"""""""www.google.com""""
""""我就是要,來搞你""""""";
            Assert.AreEqual(expected, actual);
            
            actual = target.Read();
            expected = "D";
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