using Facebook.DAL.Responses.Newfeed;
using System.Collections.Generic;

namespace FacebookMessengerCsharp.Helper
{
    public class ComparerCustom : IEqualityComparer<NewfeedDTO>
    {
        public bool Equals(NewfeedDTO x, NewfeedDTO y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(NewfeedDTO obj)
        {
            return 1;
        }
    }
}