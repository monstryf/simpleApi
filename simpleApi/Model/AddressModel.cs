namespace simpleApi.Model
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public virtual UserModel User { get; set; } = default!;
    }
}
