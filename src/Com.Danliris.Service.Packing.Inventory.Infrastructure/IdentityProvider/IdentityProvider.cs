namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider
{
    public class IdentityProvider : IIdentityProvider
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public int TimezoneOffset { get; set; }
    }
}