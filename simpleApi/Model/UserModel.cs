﻿namespace simpleApi.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? AddressId { get; set; }
        public virtual AddressModel? Address { get; set; } = default!;
    }
}
