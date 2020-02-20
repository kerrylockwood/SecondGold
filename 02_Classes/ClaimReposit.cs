using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Classes
{
    public class ClaimReposit
    {
        public Queue<CustClaim> _claimQue = new Queue<CustClaim>();

        //=========================================
        public void SeedQue()
        {
            _claimQue.Enqueue(new CustClaim(1, TypeOfClaim.Car, "Car accident on 465.", 400.00m, DateTime.Parse("2018/04/25"), DateTime.Parse("2018/04/27")));
            _claimQue.Enqueue(new CustClaim(2, TypeOfClaim.Home, "House fire in kitchen.", 4000.00m, DateTime.Parse("2018/04/11"), DateTime.Parse("2018/04/12")));
            _claimQue.Enqueue(new CustClaim(3, TypeOfClaim.Theft, "Stolen pancakes.", 4.00m, DateTime.Parse("2018/04/27"), DateTime.Parse("2018/06/01")));
        }

        //=========================================
        public List<CustClaim> RtnAllClaims()
        {
            return new List<CustClaim>(_claimQue);
        }

        //=========================================
        public CustClaim RtnPeekNextClaim()
        {
            return _claimQue.Peek();
        }

        //=========================================
        public CustClaim RtnDeQNextClaim()
        {
            return _claimQue.Dequeue();
        }

        //=========================================
        public CustClaim AddClaim(CustClaim claimInfo)
        {
            List<CustClaim> claimList = new List<CustClaim>();

            _claimQue.Enqueue(new CustClaim(claimInfo.ClaimId, claimInfo.ClaimType, claimInfo.Description, claimInfo.ClaimAmount, claimInfo.DateOfClaim, claimInfo.DateOfIncident));

            claimList = RtnAllClaims();
            foreach (CustClaim existClaimInfo in claimList)
            {
                if (existClaimInfo.ClaimId == claimInfo.ClaimId)
                {
                    return existClaimInfo;
                }
            }

            return new CustClaim();
        }
    }
}
