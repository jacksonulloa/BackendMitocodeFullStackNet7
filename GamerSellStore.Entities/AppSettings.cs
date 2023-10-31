#nullable disable
namespace GamerSellStore.Entities
{
    public class AppSettings
    {
        public StorageConfiguration storageConfiguration {  get; set; }
        public Jwt jwt { get; set; }

        public EmailSenderOptions emailSenderOptions { get; set; }
    }

    public class Jwt
    {
        public string SecretKey { get; set; } = "";
        public string Audience { get; set; } = "";
        public string Issuer { get; set; } = "";

    }

    public class StorageConfiguration
    {
        public string PublicUrl { get; set; } = "";
        public string Path { get; set; } = "";
    }

    public class EmailSenderOptions
    {
        public int Port { get; set; } = 0;
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool EnableSsl { get; set; } = true;
        public string Host { get; set; } = "";
    }
}
