using System.Text;

namespace AndroidXRecyclerViewSelection
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Vin { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder()
                .Append(Year)
                .Append(' ')
                .Append(Make)
                .Append(' ')
                .Append(Model);
            return sb.ToString();
        }
    }
}
