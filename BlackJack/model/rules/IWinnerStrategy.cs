using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IWinnerStrategy
    {
        bool WinnerStrategy(int playerScore, int dealerScore, int maxScore);
    }
}
