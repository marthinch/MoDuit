using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoDuit.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoDuit.Controllers
{
    [Route("api/question2")]
    [ApiController]
    public class Question2Controller : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly string baseURL;
        private readonly static HttpClient httpClient = new HttpClient();

        public Question2Controller(IConfiguration configuration)
        {
            this.configuration = configuration;
            baseURL = configuration["BaseURL"];
        }

        [HttpGet("two")]
        public async Task<ActionResult<IEnumerable<Question2DTO>>> Two()
        {
            List<Question2DTO> items = new List<Question2DTO>();

            try
            {
                string endpoint = "backend/question/two";
                string APIURL = baseURL + endpoint;

                var response = await httpClient.GetAsync(APIURL);
                if (!response.IsSuccessStatusCode)
                {
                    return Ok(response.ReasonPhrase);
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                    return Ok(items);

                items = JsonSerializer.Deserialize<List<Question2DTO>>(content);

                string firstFilter = "Ergonomic";
                string secondFilter = "Sports";
                items = items.Where(a => (a.description.Contains(firstFilter) || a.title.Contains(firstFilter)) && a.tags.Contains(secondFilter))
                             .OrderByDescending(a => a.id)
                             .TakeLast(3)
                             .ToList();
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }

            return Ok(items);
        }

    }
}
