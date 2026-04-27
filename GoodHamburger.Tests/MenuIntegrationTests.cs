using FluentAssertions;
using GoodHamburger.API.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

public class MenuIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public MenuIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetMenu_Deve_Retornar_200_E_Lista_De_Itens()
    {
        // Act
        var response = await _client.GetAsync("/menu");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var items = await response.Content.ReadFromJsonAsync<List<MenuItemResponseDTO>>();

        items.Should().NotBeNull();
        items.Should().HaveCountGreaterThan(0);
        items.Should().Contain(i => i.Name == "X Burger");
    }
}