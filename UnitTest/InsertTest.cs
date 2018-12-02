﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class InsertTest
    {
        public InsertTest()
        {
            TORM.Debug = true;
        }

        [TestMethod]
        public void InsertOneTest()
        {
            var result = TORM.Insert(new rules
            {
                created_at = DateTime.Now.AddDays(-3)
            });
            Debug.Assert(result == 1);
        }

        [TestMethod]
        public void InsertBatchTest()
        {
            var result = TORM.InsertBatch(new[]
                                          {
                                                  new rules
                                                  {
                                                      created_at = DateTime.Now.AddDays(-3)
                                                  },
                                                  new rules
                                                  {
                                                      created_at = DateTime.Now.AddDays(-2)
                                                  },
                                                  new rules
                                                  {
                                                      created_at = DateTime.Now.AddDays(-1)
                                                  }
                                              });
            Debug.Assert(result == 3);
        }
    }
}