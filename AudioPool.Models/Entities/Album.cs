using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioPool.Models.Entities;

/*
○ Id (int)
○ Name (string)
○ ReleaseDate (datetime)
○ CoverImageUrl (string) NULL
○ Description (string) NULL
○ DateCreated (code-generated)
○ DateModified (code-generated) NULL
○ ModifiedBy (code-generated) NULL
○ Foreign keys
■ Artist (many-to-many)
■ Song (one-to-many)
*/

namespace AudioPool.Models.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
        public ICollection<Artist> Artists { get; set; } = [];
    }
}