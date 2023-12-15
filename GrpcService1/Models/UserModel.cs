namespace GrpcService1.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? surnane { get; set; }
        public string? emailAdress { get; set; } = "test";
    }
}
