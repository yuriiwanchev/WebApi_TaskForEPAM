using NUnit.Framework;

namespace DataAccess.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        
        // private TestClass obj;
        // private Mock<ILogger<TestClass>> logger;
        //
        // [SetUp]
        // public void Setup()
        // {
        //     logger = new Mock<ILogger<TestClass>>();
        //     obj = new TestClass(logger.Object);
        // }
        //
        // [Test]
        // public void GetInt_NotANumber_ThrowsInvalidOperationException()
        // {
        //     // Arrange
        //     string str = "1d223dfg";
        //     // Act & Assert
        //     Assert.That(() => obj.GetInt(str), Throws.InvalidOperationException);
        // }
        //
        // [TestCase("100000000000000000000")]
        // [TestCase("-100000000000000000000")]
        // public void GetInt_IntBiggerThanMax_ThrowsOverflowException(string input)
        // {
        //     // Act & Assert
        //     var ex = Assert.Throws<OverflowException>(() => obj.GetInt(input));
        //     Assert.That(ex.Message, Is.EqualTo("Arithmetic operation resulted in an overflow."));
        // }
        //
        // [Test]
        // public void GetInt_GivenNullArguments_ThrowsArgumentNullException()
        // {
        //     // Arrange
        //     string? str = null;
        //     // Act & Assert
        //     Assert.That(() => obj.GetInt(str), Throws.Exception);
        // }
        //
        //
        // [TestCase("123",123)]
        // [TestCase("-123",-123)]
        // public void GetInt_TestCases(string input, int expextedResult)
        // {
        //     var result = obj.GetInt(input);
        //     
        //     Assert.That(result, Is.EqualTo(expextedResult));
        // }
    }
}