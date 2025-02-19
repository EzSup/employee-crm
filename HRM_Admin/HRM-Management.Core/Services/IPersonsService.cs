using HRM_Management.Core.DTOs.PersonDtos;
using HRM_Management.Core.Helpers.Enums;
using Microsoft.AspNetCore.Http;

namespace HRM_Management.Core.Services
{
    public interface IPersonsService
    {
        Task<bool> CheckPersonSubmission(long personTgId);
        Task<int> RegisterApplicationAsync(ApplicationSubmitRequest requestDto);
        Task AttachDocumentAsync(int personId, IFormFile file, DocumentType documentType);
        Task<IEnumerable<FormResponse>> GetAllNotApprovedFormsAsync();
        Task<FormResponse> GetFormByIdAsync(int formId);
        Task ApproveFormAsync(int personId, int hirerId);
        Task RejectFormAsync(int personId);
        Task<string> GetDocumentAsync(int personId, DocumentType documentType);
        Task UpdateFormAsync(FormUpdateRequest request);
    }
}
