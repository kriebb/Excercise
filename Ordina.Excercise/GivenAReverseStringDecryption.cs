using Xunit;

namespace Ordina.Excercise
{
    public class GivenAReverseStringDecryption
    {
        [Fact]
        public void WhenWeSupplyASampleText_ItShouldBeReturnedReversed()
        {
            var decryption = new ReverseStringDecryption();
            var decrypt = decryption.Decrypt("abc");
            Assert.Equal("cba", decrypt);
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