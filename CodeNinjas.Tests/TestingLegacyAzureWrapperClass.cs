namespace CodeNinjas.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class TestingLegacyAzureWrapperClass : LegacyAzureBase<TestingLegacyAzureWrapperClass>
    {
        public TestingLegacyAzureWrapperClass([Optional] ILogger<TestingLegacyAzureWrapperClass> logger)
            : base("CodeNinjas.Tests.TestingLegacyAzureWrapperClass", logger)
        {

        }
    }
}