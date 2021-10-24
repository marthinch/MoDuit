using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoDuit.DTOs;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace MoDuit.Controllers
{
    [Route("api/question1")]
    [ApiController]
    public class Question1Controller : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly string baseURL;
        private readonly static HttpClient httpClient = new HttpClient();

        public Question1Controller(IConfiguration configuration)
        {
            this.configuration = configuration;
            baseURL = configuration["BaseURL"];
        }

        [HttpGet("one")]
        public async Task<ActionResult<Question1DTO>> One()
        {
            Question1DTO item = new Question1DTO();

            try
            {
                string endpoint = "backend/question/one";
                string APIURL = baseURL + endpoint;

                var response = await httpClient.GetAsync(APIURL);
                if (!response.IsSuccessStatusCode)
                {
                    return Ok(response.ReasonPhrase);
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                    return Ok();

                item = JsonSerializer.Deserialize<Question1DTO>(content);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }

            return Ok(item);
        }
    }
}
