using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using UtilityBot.Models;
using static System.Collections.Specialized.BitVector32;

namespace UtilityBot.Services
{
    internal class StateMachine : IState
    {
        private readonly ConcurrentDictionary<long, State> _sessions;
        public StateMachine() 
        {
            _sessions = new ConcurrentDictionary<long, State>();
        }

        private State GetState(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
            {
                return _sessions[chatId];
            }
            State state = new State()
            {
                CurrentMode = StateMode.None
            };
            _sessions.TryAdd(chatId, state);
            return state;
        }
        public StateMode GetStateCurrentMode(long chatID)
        {
            return GetState(chatID).CurrentMode;
        }
        public void SetStateCurrentMode(StateMode stateMode, long chatId)
        {
            if (_sessions.ContainsKey(chatId))
            {
                _sessions[chatId].CurrentMode = stateMode;
                return;
            }
            State state = new State()
            {
                CurrentMode = stateMode
            };
            _sessions.TryAdd(chatId, state);
        }
    }
}
