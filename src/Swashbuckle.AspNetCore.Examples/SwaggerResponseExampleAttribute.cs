using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Swashbuckle.AspNetCore.Examples
{
    /// <inheritdoc />
    /// <summary>
    /// This is used for generating Swagger documentation. Should be used in conjuction with SwaggerResponse - will add examples to SwaggerResponse.
    /// See https://github.com/mattfrear/Swashbuckle.AspNetCore.Examples
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerResponseExampleAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="statusCode">The HTTP status code, e.g. 200. If &gt; 0, then the example will be set in the responses section of the Swagger doc.</param>
        /// <param name="examplesProviderType">A type that inherits from IExamplesProvider</param>
        /// <param name="responseType">If set, then the example will be set on the type in the definitions section of the Swagger doc.</param>
        /// <param name="contractResolver">If null then the CamelCasePropertyNamesContractResolver will be used. For PascalCase you can pass in typeof(DefaultContractResolver)</param>
        /// <param name="jsonConverter">An optional jsonConverter to use, e.g. typeof(StringEnumConverter) will render strings as enums</param>
        public SwaggerResponseExampleAttribute(int statusCode, Type examplesProviderType, Type responseType = null, Type contractResolver = null, Type jsonConverter = null)
        {
            if (examplesProviderType.GetTypeInfo().GetInterface(nameof(IExamplesProvider)) == null)
            {
                throw new InvalidTypeException(
                    paramName: nameof(examplesProviderType),
                    invalidType: examplesProviderType,
                    expectedType: typeof(IExamplesProvider));
            }

            StatusCode = statusCode;
            ResponseType = responseType;
            ExamplesProviderType = examplesProviderType;
            JsonConverter = jsonConverter == null ? null : (JsonConverter)Activator.CreateInstance(jsonConverter);
            ContractResolver = (IContractResolver)Activator.CreateInstance(contractResolver ?? typeof(CamelCasePropertyNamesContractResolver));
        }

        public Type ExamplesProviderType { get; }

        public JsonConverter JsonConverter { get; }

        public int StatusCode { get; }

        public IContractResolver ContractResolver { get; private set; }

        public Type ResponseType { get; }
    }
}
