namespace Api.Models
{
    public class Message
    {
        //public int Id { get; set; }
        public string Context { get; set; }
        //public User User { get; set; }
        public int UserId { get; set; }

        public Message(/*int id ,*/ string context, int userId)
        {
            //Id = id;
            Context = context;
            UserId = userId;
        }
    }
}
