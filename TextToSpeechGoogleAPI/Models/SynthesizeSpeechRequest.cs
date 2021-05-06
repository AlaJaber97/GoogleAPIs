using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public partial class SynthesizeSpeechRequest
    {
        [JsonPropertyName("input")]
        public Input Input { get; set; }

        [JsonPropertyName("voice")]
        public Voice Voice { get; set; }

        [JsonPropertyName("audioConfig")]
        public AudioConfig AudioConfig { get; set; }
    }

    public partial class AudioConfig
    {
        [JsonPropertyName("audioEncoding")]
        public Enums.AudioEncoding AudioEncoding { get; set; }
    }

    public partial class Input
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public partial class Voice
    {
        [JsonPropertyName("languageCode")]
        public string LanguageCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ssmlGender")]
        public Enums.SsmlVoiceGender SsmlGender { get; set; }
    }
}
