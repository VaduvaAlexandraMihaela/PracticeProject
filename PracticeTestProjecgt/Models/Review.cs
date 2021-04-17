using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeTestProjecgt.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public DateTime date { get; set; }
        public Movie movie { get; set; }
    }
}
