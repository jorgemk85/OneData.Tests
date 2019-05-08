﻿using OneData.Extensions;
using OneData.Models;
using OneData.Models.Test;
using OneData.Tools.Test;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using OneData.DAO;

namespace OneData.IntegrationTests.MsSql
{
    [TestFixture]
    class ManagerTests
    {
        [Test]
        public void SelectGuid_DataFromCache_ReturnsTrue()
        {
            List<Post> list = Post.SelectAll();
            Result<Post> result = Post.SelectResult(x => x.Id == list[0].Id);

            Assert.IsTrue(result.IsFromCache);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreNotEqual(result, null);
        }

        [Test]
        public void InsertMassive_ReturnsNoError()
        {
            List<Post> list = Post.SelectAll(new QueryOptions() { MaximumResults = 5 });
            foreach (Post post in list)
            {
                post.Id = Guid.NewGuid();
            }
            Assert.DoesNotThrow(() => list.InsertMassive());
        }

        [Test]
        public void UpdateMassive_ReturnsNoError()
        {
            Random random = new Random();
            List<Post> list = Post.SelectAll(new QueryOptions() { MaximumResults = 5 });
            foreach (Post post in list)
            {
                post.Name = $"Updated Post {random.Next()}";
            }
            Assert.DoesNotThrow(() => list.UpdateMassive());
        }

        [Test]
        public void DeleteMassive_ReturnsNoError()
        {
            List<Post> list = Post.SelectAll(new QueryOptions() { MaximumResults = 5 });
            foreach (Post post in list)
            {
                post.Id = Guid.NewGuid();
            }
            list.InsertMassive();
            Assert.DoesNotThrow(() => list.DeleteMassive());
        }

        [Test]
        public void InsertBlog_NewObject_ReturnsNoError()
        {
            Assert.DoesNotThrow(() => TestTools.GetBlogModel(true).Insert());
        }

        [Test]
        public void InsertPost_NewObject_ReturnsNoError()
        {
            List<Blog> blogs = Blog.SelectAllResult().Data.ToList();
            List<Author> authors = Author.SelectAllResult().Data.ToList();

            TestTools.GetPostModel(true).BlogId = blogs[0].Id;
            TestTools.GetPostModel(false).AuthorId = authors[0].Id;
            Assert.DoesNotThrow(() => TestTools.GetPostModel(false).Insert());
        }

        [Test]
        public void InsertAuthor_NewObject_ReturnsNoError()
        {
            Assert.DoesNotThrow(() => TestTools.GetAuthorModel(true).Insert());
        }

        [Test]
        public void SetIdentity_RealIdentity_ReturnsNoError()
        {
            UserTest user = new UserTest();
            Assert.DoesNotThrow(() => Manager.Identity = user);
        }

        [Test]
        public void LogWithIdentity_RealIdentity_ReturnsNoError()
        {
            UserTest user = new UserTest();
            user.Id = Guid.NewGuid();
            Manager.Identity = user;
            TestTools.GetLogTestIntModel(true).Insert();

            Assert.DoesNotThrow(() => Manager.Identity = user);
        }
    }
}
