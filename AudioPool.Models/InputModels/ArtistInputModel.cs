using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.InputModels
{
    public class ArtistInputModel
    {
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? CoverImageUrl { get; set; }
        public DateTime DateOfStart { get; set; }
    }
}