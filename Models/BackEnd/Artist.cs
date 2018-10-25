using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackEnd
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //[Json] List<string>
        public string Genres { get; set; }

        public List<Image> Images { get; set; }

        public long Popularity { get; set; }
    }
}
