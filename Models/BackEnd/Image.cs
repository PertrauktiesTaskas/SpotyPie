﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackEnd
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public long Height { get; set; }

        public long Width { get; set; }
    }

}
