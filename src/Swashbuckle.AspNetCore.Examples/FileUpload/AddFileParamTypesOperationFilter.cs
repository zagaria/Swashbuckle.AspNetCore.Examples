using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;

namespace Swashbuckle.AspNetCore.Examples
{ 
    public class AddFileParamTypesOperationFilter : IOperationFilter
    {
        private static readonly string[] fileParameters = new[] { "ContentType", "ContentDisposition", "Headers", "Length", "Name", "FileName" };

        public void Apply(Operation operation, OperationFilterContext context)
        {
            var actionAttributes = context.ApiDescription.ActionAttributes();
            var operationHasFileUploadButton = actionAttributes.Any(r => r.GetType() == typeof(AddSwaggerFileUploadButtonAttribute));

            if (!operationHasFileUploadButton)
            {
                return;
            }

            operation.Consumes.Add("multipart/form-data");

            operation.Parameters.Clear();

            operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "file",
                    Required = true,
                    In = "formData",
                    Type = "file",
                    Description = "A file to upload"
                }
            );
        }
    }
}
