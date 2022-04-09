using Newtonsoft.Json;
using System;

namespace Webhook
{
    [Serializable]
    public class Field
    {
        [JsonProperty("name")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("inline")]
        public bool Inline { get; set; }
    }
}