using HRM_Management.Dal.Entities;
using HRM_Management.GraphQl.Resolvers.HRM_Management.GraphQl.Resolvers;

namespace HRM_Management.GraphQl.Types
{
    public class HubType : ObjectType<HubEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<HubEntity> descriptor)
        {
            descriptor.Field("directorName")
                .ResolveWith<HubResolvers>(h => h.GetDirectorNameAsync(default, default));

            descriptor.Field("leaderName")
                .ResolveWith<HubResolvers>(h => h.GetLeaderNameAsync(default, default));

            descriptor.Field("deputyLeaderName")
                .ResolveWith<HubResolvers>(h => h.GetDeputyLeaderNameAsync(default, default));

            descriptor.Field("employeesCount")
                .ResolveWith<HubResolvers>(h => h.GetEmployeesCountAsync(default, default))
                .Type<IntType>();
        }
    }
}
