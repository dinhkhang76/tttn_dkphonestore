using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
namespace MyClass.DAO
{
    public class CategoryDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<Category> getListByParentId(int parentId = 0)
        {
            return db.Categorys
                .Where(m => m.ParentId == parentId && m.Status == 1)
                .OrderBy(m => m.Orders)
                .ToList();
        }
       
        //Trả về ds các mâu tin
        public List<Category> getList(string status = "All")
        {
            List<Category> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Categorys.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Categorys.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Categorys.ToList();
                        break;
                    }
            }
            //if(status=="All")
            //{
            //    return db.Categorys.ToList();
            //}
            //else
            //{
            //    if(status=="Index")
            //    {
            //        //lấy ra những mẫu tin có trạng thái khác 0
            //        return db.Categorys.Where(m => m.Status != 0).ToList();
            //    }
            //    if (status == "Trash")
            //    {
            //        //lấy ra những mẫu tin có trạng thái = 0
            //        return db.Categorys.Where(m => m.Status == 0).ToList();
            //    }
            //}
            return list; //select * from Categorys
        }
        //Trả về 1 mẫu tin
        public Category getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Categorys.Find(id);
            }
        }
        public Category getRow(string slug)
        {
            return db.Categorys.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();

        }
        //Thêm mẫu tin
        public int Insert(Category row)
        {
            db.Categorys.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật
        public int Update(Category row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa
        public int Delete(Category row)
        {
            db.Categorys.Remove(row);
            return db.SaveChanges();
        }
    }
}
