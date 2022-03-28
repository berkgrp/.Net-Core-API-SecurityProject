using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Models
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }
        public string LogDescription { get; set; }
        public string LogType { get; set; }
        public string TableID { get; set; }
        public string TableName { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
