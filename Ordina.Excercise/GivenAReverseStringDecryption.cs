using Xunit;

namespace Ordina.Excercise
{
    public class GivenAReverseStringDecryption
    {
        [Fact]
        public void WhenWeSupplyASingleLineSampleText_ItShouldBeReturnedReversed()
        {
            var decryption = new ReverseStringDecryption();
            var decrypt = decryption.Decrypt("abc");
            Assert.Equal("cba", decrypt);
        }
        [Fact]
        public void WhenWeSupplyAMultiLIneSampleText_ItShouldBeReturnedReversed()
        {
            var decryption = new ReverseStringDecryption();
            var decrypt = decryption.Decrypt("abc"+System.Environment.NewLine+"123");
            Assert.Equal("321"+System.Environment.NewLine+"cba", decrypt);
        }

        [Fact]
        public void WhenWeSupplyANull_ResultShouldBeNull()
        {
            var decryption = new ReverseStringDecryption();
            var decrypt = decryption.Decrypt(null);
            Assert.Null(decrypt);
        }
    }
}