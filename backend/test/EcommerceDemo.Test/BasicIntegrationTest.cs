namespace EcommerceDemo.Test;

public class BasicIntegrationTest : IClassFixture<IntegrationTestFactory>
{
    protected BasicIntegrationTest(IntegrationTestFactory factory)
    {
        HttpClient = factory.CreateClient();
    }

    protected HttpClient HttpClient { get; }
}
