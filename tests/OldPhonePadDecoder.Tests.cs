using Xunit;

public class OldPhonePadDecoderTests
{
    [Theory]
    [InlineData("33#", "E")]
    [InlineData("227*#", "B")]
    [InlineData("4433555 555666#", "HELLO")]
    [InlineData("8 88777444666*664#", "TURING")]
    [InlineData("222 2 22#", "CAB")]
    [InlineData("2*2#", "A")]          // type A, backspace, type A again
    [InlineData("0#", " ")]           // zero as a space
    [InlineData("7777#", "S")]        // wrap-around on '7' (PQRS)
    public void OldPhonePad_ReturnsExpectedText(string input, string expected)
    {
        var actual = OldPhonePadDecoder.OldPhonePad(input);
        Assert.Equal(expected, actual);
    }
}
