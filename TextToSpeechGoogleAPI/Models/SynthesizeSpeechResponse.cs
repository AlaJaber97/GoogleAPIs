using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public partial class SynthesizeSpeechResponse
    {
        [JsonPropertyName("audioContent")]
        public string AudioContent { get; set; }
    }
}
