using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextToSpeechController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public TextToSpeechController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        /// <summary>
        /// convert text into audio file
        /// </summary>
        /// <param name="languageCode">e.g: Arabic(ar-AX), English(en-US)</param>
        /// <param name="text">sentence to be converted into an audio file</param>
        /// <returns>audio file</returns>
        /// <response code="200">Returns audio file</response>
        /// <response code="500">If an error occurs</response>     
        [HttpGet]
        public async Task<IActionResult> TTSConverter([FromQuery] string languageCode = "en-US", [FromQuery] string text = "Hello BravoBravo")
        {
            try
            {
                var api_key = Configuration.GetValue<string>("GoogleApiKey"); 
                if (api_key == null) return Problem("Api Key was not provided");

                using var httpClient = new HttpClient();
                var UrlRequest = $"https://texttospeech.googleapis.com/v1/text:synthesize?key={api_key}";
                var RequestBody = new Models.SynthesizeSpeechRequest()
                {
                    Input = new Models.Input { Text = text },
                    AudioConfig = new Models.AudioConfig
                    {
                        AudioEncoding = Enums.AudioEncoding.Mp3
                    },
                    Voice = new Models.Voice
                    {
                        LanguageCode = languageCode,
                        Name = $"{languageCode}-Standard-A",
                        SsmlGender = Enums.SsmlVoiceGender.Female
                    },
                };

                var Response = await httpClient.PostAsJsonAsync(UrlRequest, RequestBody);
                var Result = await Response.Content.ReadAsStringAsync();

                if (Response.IsSuccessStatusCode)
                {
                    var SynthesizeSpeechResponse = JsonSerializer.Deserialize<Models.SynthesizeSpeechResponse>(Result);
                    var FileBytes = Convert.FromBase64String(SynthesizeSpeechResponse.AudioContent);
                    return File(FileBytes, "audio/wav");
                }
                else
                {
                    return Problem(Result);
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }
    }
}
