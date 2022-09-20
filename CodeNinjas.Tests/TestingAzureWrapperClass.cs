namespace CodeNinjas.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestingAzureWrapperClass : AzureBase<TestingAzureWrapperClass>
    {
        public TestingAzureWrapperClass([Optional] ILogger<TestingAzureWrapperClass> logger)
            : base("CodeNinjas.Tests.TestingAzureWrapperClass", logger)
        {
        }
    }
}