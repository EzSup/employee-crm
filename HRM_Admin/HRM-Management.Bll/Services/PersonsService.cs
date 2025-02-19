using AutoMapper;
using HRM_Management.Core.AWSS3;
using HRM_Management.Core.DTOs.PersonDtos;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static HRM_Management.Core.Helpers.Constants;

namespace HRM_Management.Bll.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly IRepository<EmployeeEntity> _employeesRepository;
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personsRepository;
        private readonly IRepository<PersonTranslateEntity> _translatesRepository;

        public PersonsService(IUnitOfWork unitOfWork, IFileStorageRepository fileStorage, IMapper mapper)
        {
            _personsRepository = (IPersonRepository)unitOfWork.GetRepository<PersonEntity>();
            _employeesRepository = unitOfWork.GetRepository<EmployeeEntity>(false);
            _fileStorageRepository = fileStorage;
            _mapper = mapper;
        }

        #region Files

        public async Task<string> GetDocumentAsync(int personId, DocumentType documentType)
        {
            var person = await _personsRepository.GetByIdAsync(personId);
            var fileKey = string.Empty;

            fileKey = documentType switch
            {
                DocumentType.PassportScan => person.PassportScan,
                DocumentType.CV => person.CV,
                DocumentType.Photo => person.Photo,
                _ => throw new ArgumentException()
            };

            return await _fileStorageRepository.GetObjectTempUrlAsync(fileKey!);
        }

        #endregion

        #region ApplicationSubmition

        public async Task<int> RegisterApplicationAsync(ApplicationSubmitRequest requestDto)
        {
            var newPerson = _mapper.Map<PersonEntity>(requestDto);

            return (await _personsRepository.AddAsync(newPerson)).Id;
        }

        public async Task AttachDocumentAsync(int personId, IFormFile file, DocumentType documentType)
        {
            var person = await _personsRepository.GetByIdAsync(personId);
            if (file.Length > MAX_FILE_SIZE_BYTES)
                throw new ArgumentException("File exeeds accptable size! Maximum file size is 15MB");
            ValidateFileExtension(file, documentType);
            var fileKey = await _fileStorageRepository.UploadFileAsync(file);

            switch (documentType)
            {
                case DocumentType.PassportScan:
                    person.PassportScan = fileKey;
                    break;
                case DocumentType.CV:
                    person.CV = fileKey;
                    break;
                case DocumentType.Photo:
                    person.Photo = fileKey;
                    break;
            }
            await _personsRepository.UpdateAsync(person);
        }

        public Task<bool> CheckPersonSubmission(long personTgId)
        {
            return _personsRepository
                .GetAllQueryable()
                .Where(x => x.TelegramId == personTgId)
                .AnyAsync();
        }

        #endregion

        #region CRUD

        public async Task<FormResponse> GetFormByIdAsync(int formId)
        {
            var person = await _personsRepository.GetByIdAsync(formId);
            person = (await ReplacePhotoWithLink([person])).First();
            var data = _mapper.Map<FormResponse>(person);
            return _mapper.Map<FormResponse>(person);
        }

        public async Task UpdateFormAsync(FormUpdateRequest request)
        {
            var form = _mapper.Map<PersonEntity>(request);
            await UpdateOnlyMainDataAsync(form);
        }

        private async Task UpdateOnlyMainDataAsync(PersonEntity data)
        {
            var original = await _personsRepository.GetByIdAsync(data.Id);
            data.Photo = original.Photo;
            data.CV = original.CV;
            data.PassportScan = original.PassportScan;
            await _personsRepository.UpdateAsync(data);
        }

        #endregion

        #region FormsApproving

        public async Task<IEnumerable<FormResponse>> GetAllNotApprovedFormsAsync()
        {
            var persons = await _personsRepository.GetWhereAsync(x => x.Employee == null);
            persons = await ReplacePhotoWithLink(persons);
            return _mapper.Map<IEnumerable<FormResponse>>(persons);
        }

        public async Task ApproveFormAsync(int personId, int hirerId)
        {
            if (await IsFormApprovedAsync(personId))
                throw new ArgumentException("The form you tried to approve is already approved!");
            var employee = new EmployeeEntity
            { PersonId = personId, HirerId = hirerId };
            await _employeesRepository.AddAsync(employee);
        }

        public async Task RejectFormAsync(int personId)
        {
            if (await IsFormApprovedAsync(personId))
                throw new ArgumentException("The form you tried to reject is already approved! You can only delete employee!");
            await _personsRepository.DeleteAsync(personId);
        }

        #endregion

        #region privateMethods

        private void ValidateFileExtension(IFormFile file, DocumentType documentType)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            if (!ACCEPTABLE_FILE_EXTENSIONS.TryGetValue(documentType, out var validExtensions))
                throw new ArgumentException("Unsupported document type");

            if (!validExtensions.Contains(fileExtension))
                throw new ArgumentException($"File doesn’t meet the extension requirements for {documentType}!");
        }

        private Task<bool> IsFormApprovedAsync(int personId)
        {
            return _employeesRepository
                .GetAllQueryable()
                .Where(x => x.PersonId == personId)
                .AnyAsync();
        }

        private async Task<IEnumerable<PersonEntity>> ReplacePhotoWithLink(IEnumerable<PersonEntity> persons)
        {
            foreach (var person in persons.Where(x => !string.IsNullOrWhiteSpace(x.Photo)))
            {
                person.Photo = await _fileStorageRepository.GetObjectTempUrlAsync(person.Photo);
            }
            return persons;
        }

        #endregion
    }
}
