using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Azure_Lab_3_part_1.Models
{
    public class AzureMember
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Age { get; set; }
        public int Power { get; set; } = 1;

        public AzureMember(string _Name, int _Age, int _Power = 100)
        {
            Name = _Name;
            Age = _Age;
            Power = _Power;
        }
    }
}
