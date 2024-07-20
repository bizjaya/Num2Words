namespace Num2Words.Tests;

using Num2Words.Services;
using NUnit.Framework;


[TestFixture]
public class NumConvSvcTests
{
    private NumConvSvc _service;

    [SetUp]
    public void Setup()
    {
        _service = new NumConvSvc();
    }

    [Test]
    public async Task ConvertAsync_ShouldReturnCorrectWords_ForValidCurrency()
    {
        var input = "$ 123.45";
        var expected = "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS";

        var result = await _service.ConvertAsync(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task ConvertAsync_ShouldReturnCorrectWords_ForValidNumber()
    {
        var input = "1234.567";
        var expected = "ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR POINT FIVE SIX SEVEN";

        var result = await _service.ConvertAsync(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ConvertAsync_ShouldThrowArgumentException_ForInvalidInput()
    {
        var input = "$ invalid";

        Assert.ThrowsAsync<ArgumentException>(async () => await _service.ConvertAsync(input));
    }
}