namespace CodeNinjas.Tests.Bailey
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class WarningsWrapperTests
    {
        [TestMethod]
        public void WarningsWrapper_ctor()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var sut = new WarningsWrapper(loggerFactory.CreateLogger<WarningsWrapper>());

            sut.Should().NotBeNull();
            sut.HttpClient.Should().NotBeNull();
            sut.Logger.Should().NotBeNull();
            sut.Logger.Should().BeOfType<Logger<WarningsWrapper>>();

            sut = new WarningsWrapper();

            sut.Should().NotBeNull();
            sut.Should().NotBeNull();
            sut.HttpClient.Should().NotBeNull();
            sut.Logger.Should().BeNull();
        }

        [TestMethod]
        public async Task WarningsWrapper_GetRecentWarningsAsync_ShouldSucceed()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var sut = new WarningsWrapper(loggerFactory.CreateLogger<WarningsWrapper>());

            var result = await sut.GetRecentWarningsAsync();

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThanOrEqualTo(0);

            sut = new WarningsWrapper();

            result = await sut.GetRecentWarningsAsync();

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThanOrEqualTo(0);
        }
    }
}