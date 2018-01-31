using Savory.ConfigService.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Savory.ConfigService.Contract.GetConfig;
using Savory.ConfigService.Contract.GetConfig.Resonse;
using Savory.ConfigService.Biz.Repository;

namespace Savory.ConfigService
{
    public partial class ConfigService : ServiceBase, IConfigService
    {
        ServiceHost host;

        #region ServiceBase Members
        protected override void OnStart(string[] args)
        {
            host = new ServiceHost(this.GetType());

            host.Open();
        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
                host = null;
            }
        }
        #endregion

        #region IConfigService Members

        public GetAppConfigResponse GetAppConfig(GetAppConfigRequest request)
        {
            var response = new GetAppConfigResponse();

            var config = new SavoryConfig();

            config.AppId = request.AppId;
            config.FileName = request.FileName;

            #region Properties
            using (SavoryConfigDBContext configDB = new SavoryConfigDBContext())
            {
                var configFile = (from item in configDB.ConfigFile
                                  where item.DataStatus == 1
                                  where item.AppId == request.AppId
                                  where item.FileName.Equals(request.FileName, StringComparison.OrdinalIgnoreCase)
                                  select item).FirstOrDefault();

                if (configFile != null)
                {
                    var properties = new Dictionary<string, string>();

                    if (!string.IsNullOrEmpty(configFile.FileContent))
                    {
                        var lines = configFile.FileContent.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var line in lines)
                        {
                            var pair = line.Split('=');

                            properties.Add(pair[0], pair[1]);
                        }
                    }

                    config.Properties = properties;
                }
            }
            #endregion

            response.SavoryConfig = config;

            return response;
        }

        #endregion
    }
}
