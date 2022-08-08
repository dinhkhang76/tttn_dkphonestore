using MyClass.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class PostDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<PostInfo> getListByTopicId(int? topicid, string type = "Post", int? notid = null)
        {
            List<PostInfo> list = null;
            if (notid == null)
            {
                list = db.Posts
                              .Join(
                              db.Topics,
                              p => p.TopicId,
                              t => t.Id,
                              (p, t) => new PostInfo
                              {
                                  Id = p.Id,
                                  TopicName = t.Name,
                                  TopicId = p.TopicId,
                                  Title = p.Title,
                                  Slug = p.Slug,
                                  Detail = p.Detail,
                                  Img = p.Img,
                                  PostType = p.PostType,
                                  MetaKey = p.MetaKey,
                                  MetaDesc = p.MetaDesc,
                                  Created_By = p.Created_By,
                                  Created_At = p.Created_At,
                                  Updated_By = p.Updated_By,
                                  Updated_At = p.Updated_At,
                                  Status = p.Status
                              }
                              )
                              .Where(m => m.Status == 1 && m.PostType == type && m.TopicId == topicid)
                              .ToList();
            }
            else
            {
                list = db.Posts
                              .Join(
                              db.Topics,
                              p => p.TopicId,
                              t => t.Id,
                              (p, t) => new PostInfo
                              {
                                  Id = p.Id,
                                  TopicName = t.Name,
                                  TopicId = p.TopicId,
                                  Title = p.Title,
                                  Slug = p.Slug,
                                  Detail = p.Detail,
                                  Img = p.Img,
                                  PostType = p.PostType,
                                  MetaKey = p.MetaKey,
                                  MetaDesc = p.MetaDesc,
                                  Created_By = p.Created_By,
                                  Created_At = p.Created_At,
                                  Updated_By = p.Updated_By,
                                  Updated_At = p.Updated_At,
                                  Status = p.Status
                              }
                              )
                              .Where(m => m.Status == 1 && m.PostType == type && m.TopicId == topicid && m.Id != notid)
                              .ToList();

            }
            return list;
        }


        public IPagedList<PostInfo> getList(string type, int pageSize, int pageNumber)
        {
            IPagedList<PostInfo> list = db.Posts
                            .Join(
                            db.Topics,
                            p => p.TopicId,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                TopicName = t.Name,
                                TopicId = p.TopicId,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                Created_By = p.Created_By,
                                Created_At = p.Created_At,
                                Updated_By = p.Updated_By,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            )
                            .Where(m => m.Status == 1 && m.PostType == type)
                            .OrderByDescending(m => m.Created_At)
                            .ToPagedList(pageNumber, pageSize);
            return list;
        }

        public IPagedList<PostInfo> getListByTopicId(int? topicid, string type , int? notid, int pageSize, int pageNumber)
        {
            IPagedList<PostInfo> list = null;
            if (notid == null)
            {
                list = db.Posts
                              .Join(
                              db.Topics,
                              p => p.TopicId,
                              t => t.Id,
                              (p, t) => new PostInfo
                              {
                                  Id = p.Id,
                                  TopicName = t.Name,
                                  TopicId = p.TopicId,
                                  Title = p.Title,
                                  Slug = p.Slug,
                                  Detail = p.Detail,
                                  Img = p.Img,
                                  PostType = p.PostType,
                                  MetaKey = p.MetaKey,
                                  MetaDesc = p.MetaDesc,
                                  Created_By = p.Created_By,
                                  Created_At = p.Created_At,
                                  Updated_By = p.Updated_By,
                                  Updated_At = p.Updated_At,
                                  Status = p.Status
                              }
                              )
                              .Where(m => m.Status == 1 && m.PostType == type && m.TopicId == topicid)
                            .OrderByDescending(m => m.Created_At)
                            .ToPagedList(pageNumber, pageSize);
            }
            else
            {
                list = db.Posts
                              .Join(
                              db.Topics,
                              p => p.TopicId,
                              t => t.Id,
                              (p, t) => new PostInfo
                              {
                                  Id = p.Id,
                                  TopicName = t.Name,
                                  TopicId = p.TopicId,
                                  Title = p.Title,
                                  Slug = p.Slug,
                                  Detail = p.Detail,
                                  Img = p.Img,
                                  PostType = p.PostType,
                                  MetaKey = p.MetaKey,
                                  MetaDesc = p.MetaDesc,
                                  Created_By = p.Created_By,
                                  Created_At = p.Created_At,
                                  Updated_By = p.Updated_By,
                                  Updated_At = p.Updated_At,
                                  Status = p.Status
                              }
                              )
                              .Where(m => m.Status == 1 && m.PostType == type && m.TopicId == topicid && m.Id != notid)
                            .OrderByDescending(m => m.Created_At)
                            .ToPagedList(pageNumber, pageSize);

            }
            return list;
        }

        //Trả về danh sách các mẫu tin
        public List<Post> getList(string status = "All", string type = "Post")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Posts.Where(m => m.Status == 0 && m.PostType == type).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts.Where(m => m.PostType == type).ToList();
                        break;
                    }
            }
            return list;
        }

        public List<PostInfo> getListJoin(string page = "All", string type = "Post")
        {
            List<PostInfo> list = null;
            switch (page)
            {
                case "Index":
                    {
                        list = db.Posts
                            .Join(
                            db.Topics,
                            p => p.TopicId,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                TopicName = t.Name,
                                TopicId = p.TopicId,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                Created_By = p.Created_By,
                                Created_At = p.Created_At,
                                Updated_By = p.Updated_By,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            )
                            .Where(m => m.Status != 0 && m.PostType == type)
                            .ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Posts
                           .Join(
                           db.Topics,
                           p => p.TopicId,
                           t => t.Id,
                           (p, t) => new PostInfo
                           {
                               Id = p.Id,
                               TopicName = t.Name,
                               TopicId = p.TopicId,
                               Title = p.Title,
                               Slug = p.Slug,
                               Detail = p.Detail,
                               Img = p.Img,
                               PostType = p.PostType,
                               MetaKey = p.MetaKey,
                               MetaDesc = p.MetaDesc,
                               Created_By = p.Created_By,
                               Created_At = p.Created_At,
                               Updated_By = p.Updated_By,
                               Updated_At = p.Updated_At,
                               Status = p.Status
                           }
                           )
                           .Where(m => m.Status != 0 && m.PostType == type)
                           .ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts
                            .Join(
                            db.Topics,
                            p => p.TopicId,
                            t => t.Id,
                            (p, t) => new PostInfo
                            {
                                Id = p.Id,
                                TopicName = t.Name,
                                TopicId = p.TopicId,
                                Title = p.Title,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                PostType = p.PostType,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                Created_By = p.Created_By,
                                Created_At = p.Created_At,
                                Updated_By = p.Updated_By,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            }
                            )
                            .Where(m => m.Status != 0 && m.PostType == type)
                            .ToList();
                        break;
                    }
            }
            return list;
        }
        //Trả về 1 mẫu tin
        public Post getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }
        public Post getRow(string slug)
        {
            return db.Posts.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
        }

        public Post getRow(string slug, string posttype)
        {
            return db.Posts.Where(m => m.Slug == slug && m.PostType == posttype && m.Status == 1).FirstOrDefault();
        }
        //Thêm mẫu tin
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}
