namespace Todo.Service.Configuration
{
    public class StatusConfiguration
    {
        public StatusConfiguration(int i, string d)
        {
            Id = i;
            Description = d;
        }
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
