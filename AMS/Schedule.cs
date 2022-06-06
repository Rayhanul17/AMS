using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{
    public class Schedule
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string StartDate { get; set; }
        public int TotalClass { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }

    }
}
