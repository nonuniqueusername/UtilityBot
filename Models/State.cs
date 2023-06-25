using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Models
{
    public class State
    {
        public StateMode CurrentMode { get; set; }
    }
    public enum StateMode
    {
        CountWords,
        CountSum,
        None
    }
}
