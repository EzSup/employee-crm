using HRM_Management.Dal.Entities;

namespace HRM_Management.GraphQl.Types
{
    public class EmployeeType : ObjectType<EmployeeEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<EmployeeEntity> descriptor)
        {
            base.Configure(descriptor);
        }
    }
}
