using System.Collections.Generic;
using IdentityServer4.Models;

namespace core.mvc.ids.Configurations
{
    public class ApiResources
    {
        public static List<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource()
                {
                    Name = "myApi",
                    Description = "some rough description",
                    Scopes = new List<Scope> { new Scope { Name = "myApi", Description = "this API scope" } },
                    Enabled = true
                }
            };
        }
    }
}
