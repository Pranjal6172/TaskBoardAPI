using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Model
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public int StateId { get; set; }

        public bool IsFavourite { get; set; }
    }
}
