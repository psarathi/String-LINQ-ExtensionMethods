using System.Diagnostics.CodeAnalysis;

namespace StringExtensions.UnitTest;

[TestClass]
[ExcludeFromCodeCoverage]
public class TestOfStringExtensions
{
    [TestMethod]
    public void StringExtensions_Left()
    {
        var leftString = "This is a sample string".Left(4);
        Assert.IsTrue(leftString == "This", "The value should be 'This'");
    }

    [TestMethod]
    public void StringExtensions_Left_SourceNull()
    {
        string? source = null;
        Assert.IsTrue(source.Left(4) == null, "The value should be null");
    }

    [TestMethod]
    public void StringExtensions_Left_NegativeNumberOfCharacters()
    {
        var source = "This is a sample string";
        Assert.IsTrue(source.Left(-4) == "ring", "The value should be 'ring'");
    }

    [TestMethod]
    public void StringExtensions_Left_ZeroNumberOfCharacters()
    {
        var source = "This is a sample string";
        Assert.IsTrue(source.Left(0) == string.Empty, "The value should be empty");
    }

    [TestMethod]
    public void StringExtensions_Left_NumberOfCharactersMoreThanStringLength()
    {
        var source = "This is a sample string";
        Assert.IsTrue(source.Left(1000) == source, "The value should be the same as the source");
    }

    [TestMethod]
    public void StringExtensions_Left_SubString()
    {
        var source = "This is a sample string";
        Assert.IsTrue(source.Left("sample") == "This is a ", "The value should be the same as the 'This is a '");
    }

    [TestMethod]
    public void StringExtensions_Left_SubStringInclude()
    {
        var source = "This is a sample string";
        Assert.IsTrue(source.Left("sample", true) == "This is a sample", "The value should be the same as the 'This is a sample'");
    }

    [TestMethod]
    public void StringExtensions_Left_SubStringNull()
    {
        var source = "This is a sample string";
        Assert.IsNull(source.Left(""), "The value should be null");
        source = null;
        Assert.IsNull(source.Left("sample"), "The value should be the same as empty");
    }

    [TestMethod]
    public void StringExtensions_Left_SubStringNotFound()
    {
        var source = "This is a sample string";
        Assert.IsTrue(source.Left("random") == string.Empty, "The value should be the same as empty");
    }
}
