using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walks> AllWalks { get; set; }
        public int TotalWalkTime
        {
            get
            {
                int totalTime = 0;
                foreach (Walks w in AllWalks)
                {
                    totalTime += w.Duration;
                }
                return totalTime;
            }
        }
    }
}
