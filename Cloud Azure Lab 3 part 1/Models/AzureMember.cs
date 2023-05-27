using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Azure_Lab_3_part_1.Models
{
    internal class AzureMember
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Age { get; set; }
        public int Division { get; set; } = 1;

        public AzureMember(string _Name, int _Age, int _Division)
        {
            Name = _Name;
            Age = _Age;
            Division = _Division;
        }
    }
}
