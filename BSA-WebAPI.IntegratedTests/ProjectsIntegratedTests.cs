using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using BSA_WebAPI.DTO;
using BSA_WebAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace BSA_WebAPI.IntegratedTest;

public class ProjectsIntegratedTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProjectsIntegratedTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact]
    public void CreateProject()
    {
        using var client = _factory.CreateDefaultClient();
        var toCreate = new ProjectDTO()
        {
            AuthorId = 1,
            Description = "TestProject",
            TeamId = 2,
            Name = "Test",
            Deadline = DateTime.Now + TimeSpan.FromDays(200)
        };
        var response = client.PostAsync("api/Projects", new StringContent(JsonConvert.SerializeObject(toCreate), Encoding.UTF8, "application/json")).Result;
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        var response2 = client.GetAsync("api/Projects").Result.Content.ReadAsStringAsync().Result;
        var list = JsonConvert.DeserializeObject<List<Project>>(response2);
        Assert.NotNull(list);
        toCreate.Id = list!.Count;
        toCreate.CreatedAt = list.Last().CreateAt;
        Assert.Equal(new ProjectDTO(list.Last()), toCreate);
    }
}