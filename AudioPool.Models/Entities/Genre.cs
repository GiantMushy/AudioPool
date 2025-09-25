using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioPool.Models.Entities;

/*
● Genre
○ Id (int)
○ Name (string)
○ DateCreated (code-generated)
○ DateModified (code-generated) NULL
○ ModifiedBy (code-generated) NULL
○ Foreign keys
■ Artist (many-to-many)
*/

namespace AudioPool.Models.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
        public ICollection<Artist> Artists { get; set; } = [];
    }
}