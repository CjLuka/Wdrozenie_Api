namespace Api.Models
{
    public class Message
    {
        //public int Id { get; set; }
        public string Key { get; set; }
        //public User User { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }

        public Message(/*int id ,*/ string key, string userId, string email)
        {
            //Id = id;
            Key = key;
            UserId = userId;
            Email = email;

        }
    }
}
