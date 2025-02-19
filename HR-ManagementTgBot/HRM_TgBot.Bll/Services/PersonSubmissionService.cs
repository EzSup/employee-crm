using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AutoMapper;
using HRM_TgBot.Core.DTOs;
using HRM_TgBot.Core.Helpers.MappingProfiles;
using HRM_TgBot.Core.Models;
using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static HRM_TgBot.Bll.TgBotMessageHandlers.UserCollectionService;

namespace HRM_TgBot.Bll.Services;

public class PersonSubmissionService : IPersonSubmissionService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PersonSubmissionService> _logger;

    public PersonSubmissionService(IHttpClientFactory httpClientFactor, ILogger<PersonSubmissionService> logger)
    {
        _httpClient = httpClientFactor.CreateClient("PersonSubmissionHttpClient");
        _logger = logger;
    }

    public async Task SubmitPersonAsync(BotUser user)
    {
        try
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AppMappingProfiles>());
            var mapper = mapperConfig.CreateMapper();

            var person = mapper.Map<PersonDto>(user);

            user.PersonID = await SubmitPersonForm(person);

            await AttachDocument(user.PersonID, user.Photo, "attachphoto");
            await AttachDocument(user.PersonID, user.CV, "attachcv");
            await AttachDocument(user.PersonID, user.PassportScan, "attachpassportscan");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error submitting person: {ex.Message}");
        }
    }

    public async Task CheckUserSubmissionAsync(BotUser botUser)
    {
        var response = await _httpClient.GetAsync($"CheckUserSubmission/{botUser.UserTgID}");
        var user = GetUser(botUser.UserTgID);
        if (response.StatusCode == HttpStatusCode.OK)
            user.Verified = true;
        else
            FormQueueHandler.BuildQueue(user);
    }

    private async Task<int> SubmitPersonForm(PersonDto personDto)
    {
        var response = await _httpClient.PostAsJsonAsync("apply", personDto);
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException("Error submitting person: " + responseBody);

        if (!int.TryParse(responseBody, out var result))
        {
            throw new FormatException("Error submitting person: " + responseBody);
        }

        return result;
    }

    private async Task AttachDocument(int personID, IFormFile file, string resourceURI)
    {
        try
        {
            using var content = new MultipartFormDataContent();
            using var fileStream = file.OpenReadStream();
            var fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
            content.Add(fileContent, "file", file.FileName);

            var response = await _httpClient.PostAsync($"{resourceURI}/{personID}", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error attaching document: {ex.Message}");
        }
    }
}