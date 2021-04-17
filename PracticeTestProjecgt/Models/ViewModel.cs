using PracticeTestProjecgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeTestProjecgt.Models
{
    public class ViewModel
    {
        public ICollection<Review> reviews { get; set; } 
        public string AuthorName { get; set; }
        public string ReviewDesc { get; set; }
        public Movie movie { get; set; }
    }
}
