using BedeBackEndTestProject.BaseActions;
using NUnit.Framework;
using RestSharp;

namespace BedeBackEndTestProject.Tests
{
    public abstract class BaseStepsTest
    {
        protected RestClient restClient;
        protected Actions actions;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            restClient = Actions.NewRestClient();
        }

        [SetUp]
        public void SetUp()
        {
            actions = new Actions();
        }
    }
}
