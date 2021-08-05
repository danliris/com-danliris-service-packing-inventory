namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider
{
    public interface IIdentityProvider
    {
        string Username { get; set; }
        string Token { get; set; }
        int TimezoneOffset { get; set; }
    }
}