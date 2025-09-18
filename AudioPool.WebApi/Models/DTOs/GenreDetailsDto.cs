using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.WebApi.Models.DTOs
{
    public class GenreDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfArtists { get; set; }
    }
}