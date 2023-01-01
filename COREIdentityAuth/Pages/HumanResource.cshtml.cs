using COREIdentityAuth.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using COREIdentityAuth.Pages.Account;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Headers;

namespace COREIdentityAuth.Pages
{
    [Authorize(Policy = "MustBelongtoHR")]
    [Authorize(Policy = "MustClearProbation")]
    public class authPageModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public authPageModel(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        [BindProperty]
        public List<WeatherForecastDTO> WeatherForecasts { get; set; }
        public async Task OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient("OurWebAPI");
            var res = await httpClient.PostAsJsonAsync("Auth", new Credential{UserName = "admin", Password="password"});
            res.EnsureSuccessStatusCode();
            var strtoken = await res.Content.ReadAsStringAsync();
            var jwt = JsonSerializer.Deserialize<AuthenticateRespDTO>(strtoken);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt.access_token);

            WeatherForecasts = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("weatherForecast");
        }
        
        public ActionResult Method()
        {
            return Content($"Hello world from {HttpContext.User.FindFirstValue("Email")}");
        }
    }
}
