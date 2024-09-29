using ConsoleApp2.Entities;

namespace ConsoleApp2
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Worker> Workers { get; set; }
    }

}