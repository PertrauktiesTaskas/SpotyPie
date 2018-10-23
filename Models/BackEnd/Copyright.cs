using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackEnd
{
    public partial class Copyright
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string Type { get; set; }
    }
}
