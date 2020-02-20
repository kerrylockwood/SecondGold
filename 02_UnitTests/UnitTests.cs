using System;
using System.Collections.Generic;
using System.Security.Claims;
using _02_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _02_UnitTests
{
    [TestClass]
    public class UnitTests  
    {
        private readonly ClaimReposit _claimRepo = new ClaimReposit();

        //========================================
        [TestMethod]
        public void TestRtnAllClaims()
        {
            List<CustClaim> claimList = new List<CustClaim>();

            _claimRepo.SeedQue();

            int beforeCnt = _claimRepo._claimQue.Count;

            claimList = _claimRepo.RtnAllClaims();

            int afterCnt = _claimRepo._claimQue.Count;

            Assert.AreEqual(beforeCnt, afterCnt);

            int cnt = 0;
            foreach (CustClaim claimInfo in claimList)
            {
                cnt += 1;
                Assert.AreEqual(cnt, claimInfo.ClaimId);
            }
        }

        //========================================
        [TestMethod]
        public void TestRtnPeekNextClaim()
        {
            CustClaim claimInfo = new CustClaim();

            _claimRepo.SeedQue();

            int beforeCnt = _claimRepo._claimQue.Count;

            claimInfo = _claimRepo.RtnPeekNextClaim();

            int afterCnt = _claimRepo._claimQue.Count;

            Assert.AreEqual(beforeCnt, afterCnt);
        }

        //========================================
        [TestMethod]
        public void TestRtnDeQNextClaim()
        {
            CustClaim claimInfo = new CustClaim();

            _claimRepo.SeedQue();

            int beforeCnt = _claimRepo._claimQue.Count;

            claimInfo = _claimRepo.RtnDeQNextClaim();

            int afterCnt = _claimRepo._claimQue.Count;

            Assert.AreEqual(beforeCnt - 1, afterCnt);
        }

        //========================================
        [TestMethod]
        public void TestAddClaim()
        {
            CustClaim newClaim = new CustClaim(2, TypeOfClaim.Car, "Wreck on I-70.", 2000, DateTime.Parse("2018/04/27"), DateTime.Parse("2018/04/28"));
            CustClaim addedClaim = new CustClaim();

            _claimRepo.SeedQue();

            int beforeCnt = _claimRepo._claimQue.Count;

            addedClaim = _claimRepo.AddClaim(newClaim);

            int afterCnt = _claimRepo._claimQue.Count;

            Assert.AreEqual(beforeCnt + 1, afterCnt);
        }
        //========================================
    }
}
