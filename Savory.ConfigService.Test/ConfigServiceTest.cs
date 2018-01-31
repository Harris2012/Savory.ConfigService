using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using Savory.ConfigService.Contract.GetConfig;
using Savory.ConfigService.Contract;

namespace Savory.ConfigService.Test
{
    [TestClass]
    public class ConfigServiceTest
    {

        [TestMethod]
        public void GetConfigList()
        {
            var request = new GetAppConfigRequest();
            request.AppId = 100002003;
            request.FileName = "app.properties";

            var client = new ConfigServiceClient();

            var actual = client.GetAppConfig(request);

            Assert.IsNotNull(actual);

            Assert.IsNotNull(actual.SavoryConfig);

            Assert.IsNotNull(actual.SavoryConfig.Properties);

            Assert.AreNotEqual(0, actual.SavoryConfig.Properties.Count);
        }

    }
}
