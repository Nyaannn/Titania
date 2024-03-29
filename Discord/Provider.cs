﻿using Newtonsoft.Json;
using System;

namespace Webhook
{
    [Serializable]
    public class Provider
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}