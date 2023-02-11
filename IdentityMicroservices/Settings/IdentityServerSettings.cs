using IdentityServer4.Models;

namespace IdentityMicroservices.Settings
{
    public class IdentityServerSettings
    {
        /// <summary>
        /// Scope 
        /// </summary>
        public IReadOnlyCollection<ApiScope> ApiScopes { get; init; }
        /// <summary>
        /// Client có thể là thirds-party app chứ không nhất thiết là người dùng
        /// </summary>
        public IReadOnlyCollection<Client> Clients { get; init; }

        public IReadOnlyCollection<ApiResource> ApiResources { get; init ; }
        /// <summary>
        /// Scope
        /// </summary>
        public IReadOnlyCollection<IdentityResource> IdentityResources => new IdentityResource[]
        {
            //Cho phép xác định danh tính với Id Token
            new IdentityResources.OpenId(),
            //Thông tin bổ sung thêm
            new IdentityResources.Profile()
        };
    }
}
