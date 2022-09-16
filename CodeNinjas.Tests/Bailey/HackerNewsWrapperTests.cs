namespace CodeNinjas.Tests.Bailey
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HackerNewsWrapperTests
    {
        [TestMethod]
        public void HackerNewsWrapper_ctor()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var sut = new HackerNewsWrapper(loggerFactory.CreateLogger<HackerNewsWrapper>());

            sut.Should().NotBeNull();
            sut.Logger.Should().NotBeNull();
            sut.HttpClient.Should().NotBeNull();

            sut = new HackerNewsWrapper();

            sut.Should().NotBeNull();
            sut.Logger.Should().BeNull();
            sut.HttpClient.Should().NotBeNull();
        }

        [TestMethod]
        public async Task HackerNewsWrapper_GetTopStoriesAsync_Faked_Succeeds()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var sut = new HackerNewsWrapper(loggerFactory.CreateLogger<HackerNewsWrapper>())
            {
                HttpClient = new HttpClient(new TestingHttpMessageHandler())
            };

            var results = await sut.GetTopStoriesAsync();

            results.Should().NotBeNullOrEmpty();
            results.Count().Should().Be(1);
            var result = results.FirstOrDefault();
            result?.By.Should().NotBeNullOrWhiteSpace();
            result?.By.Should().Be("vinnyglennon");
            result?.Id.Should().Be(32649091);
            result?.Descendants.Should().BeGreaterThan(0);
            result?.Descendants.Should().Be(36);
            result?.Kids.Should().NotBeNull();
            result?.Time.Should().Be(1661859463);
            result?.Title.Should().NotBeNullOrWhiteSpace();
            result?.Title.Should().Be("Wikipedia Recent Changes Map");
            result?.Type.Should().NotBeNullOrWhiteSpace();
            result?.Type.Should().Be("story");

            sut = new HackerNewsWrapper()
            {
                HttpClient = new HttpClient(new TestingHttpMessageHandler())
            };

            results = await sut.GetTopStoriesAsync();

            results.Should().NotBeNullOrEmpty();
            results.Count().Should().Be(1);
            result = results.FirstOrDefault();
            result?.By.Should().NotBeNullOrWhiteSpace();
            result?.By.Should().Be("vinnyglennon");
            result?.Id.Should().Be(32649091);
            result?.Descendants.Should().BeGreaterThan(1);
            result?.Descendants.Should().Be(36);
            result?.Kids.Should().NotBeNull();
            result?.Time.Should().Be(1661859463);
            result?.Title.Should().NotBeNullOrWhiteSpace();
            result?.Title.Should().Be("Wikipedia Recent Changes Map");
            result?.Type.Should().NotBeNullOrWhiteSpace();
            result?.Type.Should().Be("story");
        }

        [TestMethod]
        public async Task HackerNewsWrapper_GetTopStoriesAsync_Real_Succeeds()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var sut = new HackerNewsWrapper(loggerFactory.CreateLogger<HackerNewsWrapper>());

            var results = await sut.GetTopStoriesAsync(3);

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThan(1);
            results.Count.Should().BeLessThan(4);

            sut = new HackerNewsWrapper();

            results = await sut.GetTopStoriesAsync(3);

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThan(1);
            results.Count.Should().BeLessThan(4);
        }

        [TestMethod]
        public async Task HackerNewsWrapper_ShowTopStoriesAsync_Real_Succeeds()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var sut = new HackerNewsWrapper(loggerFactory.CreateLogger<HackerNewsWrapper>());

            var results = await sut.ShowTopStoriesAsync(3);

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThan(1);
            results.Count.Should().BeLessThan(4);

            sut = new HackerNewsWrapper();

            results = await sut.ShowTopStoriesAsync(3);

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThan(1);
            results.Count.Should().BeLessThan(4);
        }
    }
}