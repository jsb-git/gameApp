using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameApp.Models
{
    public class Game
    {
        
        public int GameId { get; set; }

        [Required(ErrorMessage = "Specify the game")]
        [DisplayName("Title")]
        public string Name { get; set; }

        public string Genre { get; set; }
        public string Platform { get; set; }

        //[Range(1980, 2023, ErrorMessage ="Year release must be from 1980-2023")]
        [DataType(DataType.Date)] 
        [DisplayName("Date Bought")]
        public DateTime dateBought { get; set; }

        [DisplayName("Hours Played")]
        public int hoursPlayed { get; set; }

        [DisplayName("Game Status")]
        public Status GameStatus { get; set; }

        [Range(1,5, ErrorMessage ="Ratings must be from 1 to 5. 5 is the highest.")]
        [DisplayName("Ratiing")]
        public int rating { get; set; }

        
    }



    public enum Status
    {
        Almost_Done,
        Not_played_yet,
        Got_bored,
        Completed,
        New_Game_plus
    }
}