using HRM_Management.Core.AWSS3;
using HRM_Management.Dal.Entities;
using System.Linq.Expressions;

namespace HRM_Management.GraphQl.Types
{
    public class PersonType : ObjectType<PersonEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<PersonEntity> descriptor)
        {
            ConfigureFieldWithTempUrl(descriptor, p => p.Photo);
            ConfigureFieldWithTempUrl(descriptor, p => p.CV);
            ConfigureFieldWithTempUrl(descriptor, p => p.PassportScan!);
        }

        private void ConfigureFieldWithTempUrl<T>(IObjectTypeDescriptor<PersonEntity> descriptor, Expression<Func<PersonEntity, T>> fieldExpression)
        {
            descriptor.Field(fieldExpression!)
                .Type<StringType>()
                .Use(next => async context =>
                {
                    await next(context);

                    var key = context.Result as string;
                    if (!string.IsNullOrEmpty(key))
                    {
                        var fileStorageRepository = context.Service<IFileStorageRepository>();
                        context.Result = await fileStorageRepository.GetObjectTempUrlAsync(key);
                    }
                });
        }
    }
}
