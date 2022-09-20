namespace CodeNinjas.Tests.Bailey
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DeutscheWelleWrapperTests
    {
        [TestMethod]
        public void DeutscheWelleWrapper_ctor()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();
            var sut = new DeutscheWelleWrapper(loggerFactory.CreateLogger<DeutscheWelleWrapper>());

            sut.Should().NotBeNull();
            sut.Should().BeOfType<DeutscheWelleWrapper>();
            sut.HttpClient.Should().NotBeNull();
            sut.HttpClient.DefaultRequestHeaders.Count().Should().BeGreaterThan(1);
            sut.Logger.Should().NotBeNull();
            sut.Logger.Should().BeOfType<Logger<DeutscheWelleWrapper>>();

            sut = new DeutscheWelleWrapper();

            sut.Should().NotBeNull();
            sut.Should().BeOfType<DeutscheWelleWrapper>();
            sut.HttpClient.Should().NotBeNull();
            sut.HttpClient.DefaultRequestHeaders.Count().Should().BeGreaterThan(1);
            sut.Logger.Should().BeNull();
        }

        [TestMethod]
        public async Task DeutscheWelleWrapper_GetLatestArticlesAsync_ShouldThrowForInvalidLanguageId()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();
            var sut = new DeutscheWelleWrapper(loggerFactory.CreateLogger<DeutscheWelleWrapper>());

            var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await sut.GetLatestArticlesAsync(LanguageId.None));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidOperationException>();
            exception.Message.Should().NotBeNullOrWhiteSpace();
            exception.Message.Should().Be("LanguageId 0 has not been implemented on the Deutsche Welle API");

            sut = new DeutscheWelleWrapper();

            exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await sut.GetLatestArticlesAsync(LanguageId.None));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidOperationException>();
            exception.Message.Should().NotBeNullOrWhiteSpace();
            exception.Message.Should().Be("LanguageId 0 has not been implemented on the Deutsche Welle API");
        }

        [TestMethod]
        public async Task DeutscheWelleWrapper_GetLatestArticlesAsync_ShouldThrowStatusException()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();
            var sut = new DeutscheWelleWrapper(loggerFactory.CreateLogger<DeutscheWelleWrapper>())
            {
                HttpClient = new HttpClient(new TestingHttpMessageHandler())
            };

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await sut.GetLatestArticlesAsync(LanguageId.English));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().NotBeNullOrWhiteSpace();
            exception.Message.Should().Be($"Invalid status given in response: NotFound - Resource Not Found from 'https://api.dw.com/api/search/global?terms=*&contentTypes=Article&languageId=2&sortByDate=true&startDate={DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")}&endDate={DateTime.UtcNow.ToString("yyyy-MM-dd")}'");

            sut = new DeutscheWelleWrapper()
            {
                HttpClient = new HttpClient(new TestingHttpMessageHandler())
            };

            exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await sut.GetLatestArticlesAsync(LanguageId.English));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().NotBeNullOrWhiteSpace();
            exception.Message.Should().Be($"Invalid status given in response: NotFound - Resource Not Found from 'https://api.dw.com/api/search/global?terms=*&contentTypes=Article&languageId=2&sortByDate=true&startDate={DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd")}&endDate={DateTime.UtcNow.ToString("yyyy-MM-dd")}'");
        }

        [TestMethod]
        public async Task DeutscheWelleWrapper_GetLatestArticlesAsync_Faked_ShouldSucceed()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();
            var sut = new DeutscheWelleWrapper(loggerFactory.CreateLogger<DeutscheWelleWrapper>())
            {
                HttpClient = new HttpClient(new TestingHttpMessageHandler())
            };

            var searchResult = await sut.GetLatestArticlesAsync(LanguageId.Hausa);

            searchResult.Should().NotBeNull();
            searchResult.LanguageId.Should().Be(15);
            searchResult.Items.Count().Should().BeGreaterThan(0);

            sut = new DeutscheWelleWrapper()
            {
                HttpClient = new HttpClient(new TestingHttpMessageHandler())
            };

            searchResult = await sut.GetLatestArticlesAsync(LanguageId.Hausa);

            searchResult.Should().NotBeNull();
            searchResult.LanguageId.Should().Be(15);
            searchResult.Items.Count().Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task DeutscheWelleWrapper_GetLatestArticlesAsync_Real_ShouldSucceed()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();
            var sut = new DeutscheWelleWrapper(loggerFactory.CreateLogger<DeutscheWelleWrapper>());

            var searchResult = await sut.GetLatestArticlesAsync(LanguageId.Deutsch);

            searchResult.Should().NotBeNull();
            searchResult.LanguageId.Should().Be(1);
            searchResult.Items.Count().Should().BeGreaterThan(0);
        }
    }
}