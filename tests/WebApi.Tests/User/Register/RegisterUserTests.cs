using CommonTestUtilities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Exception;
using RecipeBook.Infrastructure.DataAccess;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.User.Register;

public class RegisterUserTests : IClassFixture<RecipeBookApplicationFactory>
{
    private const string REQUEST_URI = "/api/users";

    private readonly HttpClient _httpClient;
    private readonly IServiceScopeFactory _scopeFactory;

    public RegisterUserTests(RecipeBookApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    private async Task<bool> UserExists(string name, string email)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RecipeBookDbContext>();

        return await dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Active && user.Name == name && user.Email == email);
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserBuilder.Build();
        var response = await _httpClient.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldBeEmpty();

        var userExists = await UserExists(request.Name, request.Email);
        userExists.ShouldBeTrue();
    }

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Validate_ShouldBeAnErrorResponse_WhenNameIsEmpty(string culture)
    {
        var request = RequestRegisterUserBuilder.Build();
        request.Name = string.Empty;

        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd(culture);

        var response = await _httpClient.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var expectedMessage = ResourceMessagesExceptions.ResourceManager.GetString("VALIDATION_NAME_REQUIRED", new CultureInfo(culture))!;

        var errorsList = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errorsList.ShouldContain(error => error.GetString()!.Equals(expectedMessage));

        var userExists = await UserExists(request.Name, request.Email);
        userExists.ShouldBeFalse();
    }
}
