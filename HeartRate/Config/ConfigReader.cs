using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HeartRate
{
    public static class ConfigReader
    {
        public static UIConfig GetUIConfig()
        {
            UIConfig uiConfig = null;
            using (StreamReader file = File.OpenText(@"Config/UIConfig.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                uiConfig = (UIConfig)serializer.Deserialize(file, typeof(UIConfig));
            }

            return uiConfig;
        }
    }
}
