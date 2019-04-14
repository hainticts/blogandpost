﻿using MC01BlogAndPost.Entities;
using MC01BlogAndPost.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MC01BlogAndPost.Services.Mocks
{
    public class MockBlogService : IBlogService
    {

        private static List<Blog> ListBlogs = new List<Blog>();
        private static int AutoIncrease = 1;
        private readonly IPostService postService;

        public MockBlogService(IPostService postService)
        {
            if (ListBlogs.Count <= 0)
            {
                for (var i = 1; i <= 10; ++i)
                {
                    ListBlogs.Add(new Blog()
                    {
                        BlogId = (AutoIncrease++),
                        Title = "Title " + i,
                        Content = "Content " + i
                    });
                }
            }
            this.postService = postService;
        }

        public bool AddNew(Blog data)
        {
            data.BlogId = (AutoIncrease++);
            ListBlogs.Add(data);
            return true;
        }

        public bool Delete(int blogId)
        {
            ListBlogs = ListBlogs.Where(p => p.BlogId != blogId).ToList();
            postService.DeletePostBelongToBlog(blogId);
            return true;
        }

        public List<Blog> GetLists()
        {
            ListBlogs.Sort((b1, b2) => b2.BlogId.CompareTo(b1.BlogId));
            return ListBlogs;
        }
    }
}