using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MatchManagement.Models.DTO
{
    public class MatchDTO
    {
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime MatchDate { get; set; }

        public string MatchTime { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public Sport Sport { get; set; }
    }
}
