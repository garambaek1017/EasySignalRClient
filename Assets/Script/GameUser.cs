using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script
{
    public class GameUser
    {
        public string NickName { get; set; }
        public long AccountIdx { get; set; }

        public GameUser()
        {

        }

        public GameUser(long accountIdx, string nickname)
        {
            this.AccountIdx = accountIdx;
            this.NickName = nickname;
        }
    }
}
