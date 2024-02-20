using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MatchManagement.Models
{
    public class Match
    {
        public int ID { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime MatchDate { get; set; }

        public string MatchTime { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public Sport Sport { get; set; }
    }

    public enum Sport
    {
        Football = 1,
        Basketball = 2
    }
}
