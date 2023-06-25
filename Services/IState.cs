using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UtilityBot.Models;

namespace UtilityBot.Services
{
    public interface IState
    {

        StateMode GetStateCurrentMode(long chatId);

        void SetStateCurrentMode(StateMode state, long chatId);
    }
}
