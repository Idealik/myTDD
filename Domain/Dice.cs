using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Dice:Idice
    {

        public int luckyScore { get; private set; }
        
        public virtual int GetLuckyScore()
        {
            luckyScore = new Random(DateTime.Now.Millisecond).Next(1, 6);
            return luckyScore;
        }
    }
}
