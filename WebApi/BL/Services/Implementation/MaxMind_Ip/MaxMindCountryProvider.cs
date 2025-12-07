using BL.Services.Interfaces.IMaxMind_Ip;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http;

namespace BL.Services.Implementation.MaxMind_Ip
{
    public class MaxMindCountryProvider : IUserCountryProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DatabaseReader _geoReader;

        public MaxMindCountryProvider(
            IHttpContextAccessor httpContextAccessor,
            DatabaseReader geoReader)
        {
            _httpContextAccessor = httpContextAccessor;
            _geoReader = geoReader;
        }

        public string GetCountryCode()
        {
            var context = _httpContextAccessor.HttpContext;
            var ipAddress = context?.Connection.RemoteIpAddress;
            if (ipAddress == null)
                return "EG";

            var ipString = ipAddress.ToString();

            try
            {
                var response = _geoReader.Country(ipString); // قراءة من GeoLite2 [web:89]
                return response.Country.IsoCode?.ToUpperInvariant() ?? "EG";
            }
            catch
            {
                return "EG";
            }
        }
    }
}
