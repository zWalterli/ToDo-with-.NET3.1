namespace Todo.Service.Configuration
{
    public class PerfilConfiguration
    {
        public PerfilConfiguration(int i, string d)
        {
            Id = i;
            Description = d;
        }

        public int Id { get; set; }
        public string Description { get; set; }
    }
}
