using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models
{
    public class Pagination<T>
    {
        public List<T> Items { get; set; }
        public decimal TotalRecords { get; set; }
        public int pageSize { get; set; }
    }
}
