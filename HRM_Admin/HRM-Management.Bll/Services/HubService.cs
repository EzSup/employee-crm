using AutoMapper;
using HRM_Management.Core.DTOs.HubDtos;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Extensions;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HRM_Management.Bll.Services
{
    public class HubService : IHubService
    {
        private readonly IEmployeeRepository _employeesRepository;
        private readonly IRepository<HubEntity> _hubRepository;
        private readonly IMapper _mapper;

        public HubService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _hubRepository = unitOfWork.GetRepository<HubEntity>();
            _employeesRepository = (IEmployeeRepository)unitOfWork.GetRepository<EmployeeEntity>();
            _mapper = mapper;
        }

        public async Task<HubResponse> GetByIdWithJoinsAsync(int id)
        {
            var entity = await GetEntityByIdWithJoinsAsync(id);
            return _mapper.Map<HubResponse>(entity);
        }

        public async Task<int> AddAsync(HubCreateRequest request)
        {
            var members = request.MemberIds;
            var entity = _mapper.Map<HubEntity>(request);
            var hub = await _hubRepository.AddAsync(entity);
            try
            {
                await AssignMembersAsync(hub, members);
                ValidateForeignKeys(hub);
            }
            catch
            {
                await _hubRepository.DeleteAsync(hub.Id);
                throw;
            }
            return hub.Id;
        }

        public async Task UpdateAsync(HubUpdateRequest request)
        {
            var currentEmployeeIds = (await _employeesRepository
                                          .GetWhereAsync(x => x.HubId == request.Id)).Select(x => x.Id);

            var employeeIdsToDismiss = currentEmployeeIds.Except(request.MemberIds).ToArray();

            var entity = _mapper.Map<HubEntity>(request);
            entity.Employees = (await _employeesRepository
                                    .GetWhereAsync(x => request.MemberIds.Contains(x.Id))).ToList();

            ValidateForeignKeys(entity);

            if (employeeIdsToDismiss.Any())
                await DismissMembersAsync(request.Id, employeeIdsToDismiss);
            await _hubRepository.UpdateAsync(entity);
        }


        public Task DeleteAsync(int id)
        {
            return _hubRepository.DeleteAsync(id);
        }

        public async Task AssignMembersAsync(int hubId, int[] employeesIds)
        {
            var hub = await GetEntityByIdWithJoinsAsync(hubId);
            await AssignMembersAsync(hub, employeesIds);
        }

        public async Task DismissMembersAsync(int hubId, int[] employeesIds)
        {
            var hub = await GetEntityByIdWithJoinsAsync(hubId);

            var employeesToRemove = hub.Employees?
                                       .Where(emp => employeesIds.Contains(emp.Id))
                                       .ToList();

            if (employeesToRemove != null)
            {
                foreach (var employee in employeesToRemove)
                {
                    employee.HubId = null;
                    employee.Hub = null;
                }
                await _employeesRepository.UpdateRangeAsync(employeesToRemove);
                await UpdateHubPositions(hub, employeesIds);
            }
        }

        private async Task AssignMembersAsync(HubEntity hub, int[] employeesIds)
        {
            var newEmployeesIds = hub.Employees?
                                     .Select(emp => emp.Id)
                                     .Concat(employeesIds)
                                     .ToHashSet() ?? employeesIds.ToHashSet();
            hub.Employees = await _employeesRepository
                                  .GetAllQueryable()
                                  .Where(emp => newEmployeesIds.Contains(emp.Id))
                                  .ToListAsync();
            await _hubRepository.UpdateAsync(hub);
        }

        private async Task<HubEntity> GetEntityByIdWithJoinsAsync(int id)
        {
            var entity = await _hubRepository
                               .GetAllQueryable()
                               .Include(x => x.Employees)!
                               .ThenInclude(x => x.Person)
                               .Include(x => x.Director)
                               .ThenInclude(dir => dir!.Person)
                               .Include(x => x.Leader)
                               .ThenInclude(l => l!.Person)
                               .Include(x => x.DeputyLeader)
                               .ThenInclude(dl => dl!.Person)
                               .FirstOrDefaultAsync(x => x.Id == id);
            return entity ?? throw new NullReferenceException($"Hub with ID = {id} not found.");
        }

        private void ValidateForeignKeys(HubEntity entity)
        {
            var positions = new[] { entity.DirectorId, entity.LeaderId, entity.DeputyLeaderId };

            var validNotNullPositions = positions
                .Where(x => x.HasValue && x > 0)
                .Select(x => x!.Value)
                .ToList();

            var employeeIds = entity.Employees?
                .Select(x => x.Id)
                .ToHashSet();

            if (validNotNullPositions.Any() && !employeeIds.IsSupersetOf(validNotNullPositions))

                throw new ArgumentException("Employee who is assigned as hub supervisor has to be this hub member!");

            if (validNotNullPositions.Distinct().Count() < validNotNullPositions.Count())
                throw new ArgumentException("Same person can't take more than one head position in hub!");

            var employeesFromOtherHubs = entity.Employees
                                               .Where(x => !new int?[] { entity.Id, 0, null }
                                                               .Contains(x.HubId))
                                               .ToList();

            if (employeesFromOtherHubs?.Count() > 0)
                throw new ArgumentException($"Employees with IDs: {string.Join(", ", employeesFromOtherHubs.Select(x => x.Id))} " +
                                            $"are assigned to another hub! You can`t assign them to this hub!");
        }

        private async Task UpdateHubPositions(HubEntity hub, int[] employeesIds)
        {
            
            var isUpdated = false;
            (isUpdated, hub.DirectorId) = hub.DirectorId != null && employeesIds.Contains((int)hub.DirectorId)
                                              ? (true, null) : (isUpdated, hub.DirectorId);
            (isUpdated, hub.LeaderId) = hub.LeaderId != null && employeesIds.Contains((int)hub.LeaderId)
                                            ? (true, null) : (isUpdated, hub.LeaderId);
            (isUpdated, hub.DeputyLeaderId) = hub.DeputyLeaderId != null && employeesIds.Contains((int)hub.DeputyLeaderId)
                                                  ? (true, null) : (isUpdated, hub.DeputyLeaderId);
            if (isUpdated)
                await UpdateAsync(_mapper.Map<HubUpdateRequest>(hub));
        }
    }
}