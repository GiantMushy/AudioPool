using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.InputModels
{
    public class GenreInputModel
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
    }
}