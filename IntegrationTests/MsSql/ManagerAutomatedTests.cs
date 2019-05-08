using OneData.DAO;
using OneData.Models;
using OneData.Models.Test;
using OneData.Tools.Test;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace OneData.IntegrationTests.MsSql
{
    [SingleThreaded]
    [TestFixture]
    class ManagerAutomatedTests
    {
        Guid newObjId;

        [OneTimeSetUp]
        public void PerformSetupForTesting_DoesNotThrow()
        {
            newObjId = TestTools.GetBlogModel(true).Id.GetValueOrDefault();

            Assert.DoesNotThrow(() => Manager<Blog>.Insert(TestTools.GetBlogModel(false), null));
        }

        [Test, Order(1)]
        public void Update_FullAutomation_DoesNotThrow()
        {
            TestTools.GetBlogModel(false).Name = "Parametros Editados";

            Assert.DoesNotThrow(() => Manager<Blog>.Update(TestTools.GetBlogModel(false), null));
        }

        [Test, Order(3)]
        public void SelectAll_FullAutomation_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Blog.SelectAll());
        }

        [Test, Order(4)]
        public void Delete_FullAutomation_DoesNotThrow()
        {
            TestTools.GetBlogModel(false).Id = newObjId;

            Assert.DoesNotThrow(() => Manager<Blog>.Delete(TestTools.GetBlogModel(false), null));
        }
    }
}
