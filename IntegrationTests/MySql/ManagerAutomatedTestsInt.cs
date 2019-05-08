﻿using OneData.DAO;
using OneData.Models;
using OneData.Models.Test;
using OneData.Tools.Test;
using NUnit.Framework;
using System;

namespace OneData.IntegrationTests.MySql
{
    [TestFixture]
    class ManagerAutomatedTestsInt
    {
        [OneTimeSetUp]
        public void PerformSetupForTesting_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Manager<LogTestInt>.Insert(TestTools.GetLogTestIntModel(false), null));
        }

        [Test, Order(1)]
        public void Update_FullAutomation_DoesNotThrow()
        {
            TestTools.GetLogTestIntModel(false).Parametros = "Parametros Editados";

            Assert.DoesNotThrow(() => Manager<LogTestInt>.Update(TestTools.GetLogTestIntModel(false), null));
        }

        [Test, Order(3)]
        public void SelectAll_FullAutomation_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Manager<LogTestInt>.SelectAll( null));
        }

        [Test, Order(4)]
        public void Delete_FullAutomation_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Manager<LogTestInt>.Delete(TestTools.GetLogTestIntModel(true), null));
        }
    }
}
