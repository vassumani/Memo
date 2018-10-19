using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memo.Models
{
    public class MemoValidator : Memo
    {
        public bool isValidTitle()
        {
            if ((Title.Count() > 0) && (Title.Count() <= 200))
                return true;
            else
                return false;
        }

        public bool isValidDateTime()
        {
            if (typeof(DateTime) == Date.GetType())
                return true;
            else
                return false;
            
        }
    }
}
