﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosColombia.ModeloDatos.ModelosGenerales
{
    public class DataTableParameter
    {
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public string customFilter { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
        public List<DTColumn> columns { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
    public class DTColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }
}
