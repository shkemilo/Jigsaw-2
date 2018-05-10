SELECT cc1.Couple1, cc1.Couple2, cc1.Couple3, cc1.Couple4, cc1.Couple5, cc1.Couple6, cc1.Couple7, cc1.Couple8,
       cc2.Couple1, cc2.Couple2, cc2.Couple3, cc2.Couple4, cc2.Couple5, cc2.Couple6, cc2.Couple7, cc2.Couple8
FROM Couplings c
INNER JOIN CouplesColumn1 cc1 ON c.CoupleColumn1Id = cc1.Id
INNER JOIN CouplesColumn2 cc2 ON c.CoupleColumn2Id = cc2.Id;
