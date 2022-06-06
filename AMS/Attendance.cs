using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{
    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TimingId { get; set; }
        public bool IsAttend { get; set; }
    }
}
