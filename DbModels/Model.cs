using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KataAPI.DbModels
{
     
    public class Salle
    { 
        public string Id { get; set; }
        public string Label { get; set; }
    }
    public class Reservaion
    {
        public string Id { get; set; }
        public string username { get; set; }
        public string idsalle { get; set; }
        public string idHeurReservation { get; set; }
         public string dateReservation { get; set; }
    }

    public class Heur
    {
        public string Id { get; set; }
        public string Label { get; set; }
    }
}
