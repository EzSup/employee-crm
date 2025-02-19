using AutoMapper;
using HRM_Management.Core.DTOs.PartnerDtos;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;

namespace HRM_Management.Bll.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PartnerEntity> _partnerRepository;
        private readonly IPersonRepository _personRepository;

        public PartnerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _partnerRepository = unitOfWork.GetRepository<PartnerEntity>();
            _personRepository = (IPersonRepository)unitOfWork.GetRepository<PersonEntity>();
        }

        public async Task<int> CreateAsync(PartnerCreateRequest request)
        {
            var partner = _mapper.Map<PartnerEntity>(request);
            var id = (await _partnerRepository.AddAsync(partner)).Id;
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            person.PartnerId = id;
            await _personRepository.UpdateAsync(person);
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            await _partnerRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(PartnerUpdateRequest request)
        {
            var partner = _mapper.Map<PartnerEntity>(request);
            await _partnerRepository.UpdateAsync(partner);
        }
    }
}
