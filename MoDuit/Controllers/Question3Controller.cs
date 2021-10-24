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
    [Route("api/question3")]
    [ApiController]
    public class Question3Controller : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly string baseURL;
        private readonly static HttpClient httpClient = new HttpClient();

        public Question3Controller(IConfiguration configuration)
        {
            this.configuration = configuration;
            baseURL = configuration["BaseURL"];
        }

        [HttpGet("three")]
        public async Task<ActionResult<IEnumerable<Question3DTO>>> Three()
        {
            List<Question3DTO> items = new List<Question3DTO>();

            try
            {
                string endpoint = "backend/question/three";
                string APIURL = baseURL + endpoint;

                var response = await httpClient.GetAsync(APIURL);
                if (!response.IsSuccessStatusCode)
                {
                    return Ok(response.ReasonPhrase);
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                    return Ok(items);

                var temporaryItems = JsonSerializer.Deserialize<List<Question3TempDTO>>(content);
                if (temporaryItems == null || !temporaryItems.Any())
                    return Ok(items);

                foreach (var temporaryItem in temporaryItems)
                {
                    if (temporaryItem.items != null && temporaryItem.items.Any())
                    {
                        foreach (var item in temporaryItem.items)
                        {
                            Question3DTO question3 = new Question3DTO
                            {
                                id = temporaryItem.id,
                                category = temporaryItem.category,
                                title = item.title,
                                description = item.description,
                                footer = item.footer,
                                createdAt = temporaryItem.createdAt
                            };
                            items.Add(question3);
                        }
                    }
                    else
                    {
                        Question3DTO question3 = new Question3DTO
                        {
                            id = temporaryItem.id,
                            category = temporaryItem.category,
                            createdAt = temporaryItem.createdAt
                        };
                        items.Add(question3);
                    }
                }
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }

            return Ok(items);
        }
    }
}
