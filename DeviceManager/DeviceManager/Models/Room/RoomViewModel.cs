using DeviceManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Status { get; set; }
    }
}
