namespace WEBApiREST
{
    public class UserEntity
    {
        //public UserEntity (string? username)
        //{
        //    username = username;
        //}
        public Guid id { get; set; }
        public string username { get; set; } = "";
        public string? avatarUrl { get; set; } = "";
        public int? subscribersAmount { get; set; } = 0;
        public string? firstName { get; set; } = "";
        public string? lastName { get; set; } = "";
        public bool? isActive { get; set; }
        public string[]? stack { get; set; } = [];
        public string? city { get; set; } = "";
        public string? description { get; set; } = "";
        public string? token { get; set; } = null;
        public string passwordHash { get; set; }

        
    }
}
