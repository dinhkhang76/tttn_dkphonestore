﻿using MyClass.Models;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take)
        {
            List<ProductInfo> list = db.Products
                .Join(
                    db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Number = p.Number,
                    MetaKey = p.MetaKey,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.Created_At)
                .Take(take)
                .ToList();
            return list;
        }

        public IPagedList<ProductInfo> getListByListCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(
                    db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Number = p.Number,
                    MetaKey = p.MetaKey,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.Created_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }

        public List<ProductInfo> getListByListCatId(int? catid, int? notid = null, int take = 0)
        {
            List<ProductInfo> list = null;
            if (notid == null)
            {
                list = db.Products
                .Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                Name = p.Name,
                                CatName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                Created_By = p.Created_By,
                                Created_At = p.Created_At,
                                Updated_By = p.Updated_By,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            })
                .Where(m => m.Status == 1 && m.CatId == catid)
                .OrderByDescending(m => m.Created_At)
                .Take(take)
                .ToList();
            }
            else
            {
                list = db.Products
                .Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                CatId = p.CatId,
                                Name = p.Name,
                                CatName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Img = p.Img,
                                Price = p.Price,
                                PriceSale = p.PriceSale,
                                Number = p.Number,
                                MetaKey = p.MetaKey,
                                MetaDesc = p.MetaDesc,
                                Created_By = p.Created_By,
                                Created_At = p.Created_At,
                                Updated_By = p.Updated_By,
                                Updated_At = p.Updated_At,
                                Status = p.Status
                            })
                .Where(m => m.Status == 1 && m.CatId == catid && m.Id != notid)
                .OrderByDescending(m => m.Created_At)
                .Take(take)
                .ToList();
            }
            return list;
        }

        public List<ProductInfo> getList(int take)
        {

            List<ProductInfo> list = db.Products
                .Join(
                    db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Number = p.Number,
                    MetaKey = p.MetaKey,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.Created_At)
                .Take(take)
                .ToList();
            return list;
        }

        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {

            IPagedList<ProductInfo> list = db.Products
                .Join(
                    db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Number = p.Number,
                    MetaKey = p.MetaKey,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
                .Where(m => m.Status == 1)
                .OrderByDescending(m => m.Created_At)
                .ToPagedList(pageNumber, pageSize);
            return list;
        }
        public List<ProductInfo> GetList(string status = "All")
        {
            List<ProductInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products
                            .Join(
                    db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Number = p.Number,
                    MetaKey = p.MetaKey,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
                            .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products
                            .Join(
                    db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Number = p.Number,
                    MetaKey = p.MetaKey,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
                            .Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products
                            .Join(
                    db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Number = p.Number,
                    MetaKey = p.MetaKey,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
                            .ToList();
                        break;
                    }
            }
            return list; //select * from Categorys
        }
        //tim kiem
        public List<ProductInfo> Search(string keyword)
        {
            List<ProductInfo> list = db.Products
                .Join(
                db.Categorys,
                p => p.CatId,
                c => c.Id,
                (p, c) => new ProductInfo
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    Name = p.Name,
                    CatName = c.Name,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    Number = p.Number,
                    MetaKey = p.MetaKey,
                    MetaDesc = p.MetaDesc,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
                .Where(m => m.Status == 1 && m.Name.Contains(keyword))
                .OrderByDescending(m => m.Created_At)
                .ToList();
            return list;
        }
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        public Product getRow(string slug)
        {
            return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
        }

        public int getCount()
        {
            return db.Products.Count();
        }
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
