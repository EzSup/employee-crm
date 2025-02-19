using HRM_Management.Dal.Entities;

namespace HRM_Management.Tests.Helpers;

public static class HubHelperMethods
{
    public static HubEntity SetSupervisorsToMembers(this HubEntity hubEntity)
    {
        if (hubEntity == null) throw new ArgumentNullException(nameof(hubEntity));

        var supervisors = new List<EmployeeEntity>
        {
            hubEntity.Director,
            hubEntity.Leader,
            hubEntity.DeputyLeader
        };
        hubEntity.Employees.AddRange(supervisors.Where(s => s != null));
        return hubEntity;
    }
}