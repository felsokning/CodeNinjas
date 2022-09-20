namespace CodeNinjas.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AzureClassBaseTests
    {
        [TestMethod]
        public void AzureClassBase_ctor()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var sut = new TestingAzureWrapperClass(loggerFactory.CreateLogger<TestingAzureWrapperClass>());

            sut.Should().NotBeNull();
            sut.Logger.Should().NotBeNull();
            sut.HttpClient.Should().NotBeNull();
            sut.HttpClient.DefaultRequestHeaders.Should().HaveCountGreaterThan(1);
            sut.HttpClient.DefaultRequestVersion.Should().Be(HttpVersion.Version20);
            sut.HttpClient.DefaultVersionPolicy.Should().Be(HttpVersionPolicy.RequestVersionOrHigher);
            sut.HttpClient.DefaultRequestHeaders.UserAgent.Should().Contain(new ProductInfoHeaderValue(Uri.EscapeDataString("CodeNinjas.Tests.TestingAzureWrapperClass"), "1.0.0"));
            sut.HttpClient.DefaultRequestHeaders.UserAgent.Should().Contain(new ProductInfoHeaderValue("Contact", Uri.EscapeDataString("nuget@felsokning.se")));
            sut.HttpClient.DefaultRequestHeaders.UserAgent.Should().Contain(new ProductInfoHeaderValue("Website", Uri.EscapeDataString("https://www.nuget.org/profiles/felsokning")));
            sut.HttpClient.DefaultRequestHeaders.Contains("X-Correlation-ID").Should().BeTrue();
            sut.Dispose();
            sut.HttpClient.DefaultRequestHeaders.Should().BeEmpty(); // Dispose happens on a whim, so test clearing headers.
        }

        [TestMethod]
        public void LegacyAzureClassBase_ctor()
        {
            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var sut = new TestingLegacyAzureWrapperClass(loggerFactory.CreateLogger<TestingLegacyAzureWrapperClass>());

            sut.Should().NotBeNull();
            sut.Logger.Should().NotBeNull();
            sut.HttpClient.Should().NotBeNull();
            sut.HttpClient.DefaultRequestHeaders.Should().HaveCountGreaterThan(1);
            sut.HttpClient.DefaultRequestVersion.Should().Be(HttpVersion.Version11);
            sut.HttpClient.DefaultVersionPolicy.Should().Be(HttpVersionPolicy.RequestVersionOrHigher);
            sut.HttpClient.DefaultRequestHeaders.UserAgent.Should().Contain(new ProductInfoHeaderValue(Uri.EscapeDataString("CodeNinjas.Tests.TestingLegacyAzureWrapperClass"), "1.0.0"));
            sut.HttpClient.DefaultRequestHeaders.UserAgent.Should().Contain(new ProductInfoHeaderValue("Contact", Uri.EscapeDataString("nuget@felsokning.se")));
            sut.HttpClient.DefaultRequestHeaders.UserAgent.Should().Contain(new ProductInfoHeaderValue("Website", Uri.EscapeDataString("https://www.nuget.org/profiles/felsokning")));
            sut.HttpClient.DefaultRequestHeaders.Contains("X-Correlation-ID").Should().BeTrue();
            sut.Dispose();
            sut.HttpClient.DefaultRequestHeaders.Should().BeEmpty(); // Dispose happens on a whim, so test clearing headers.
        }
    }
}