using Microsoft.AspNetCore.DataProtection;
namespace ApiProject.Helpers
{
    public interface ICipherService
    {
        string Encrypt(string cipherText);
        string Decrypt(string cipherText);
    }

    public class Security : ICipherService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string Key = "BerkGaripNetCoreSecurityProject";

        public Security(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(input);
        }
    }
}
