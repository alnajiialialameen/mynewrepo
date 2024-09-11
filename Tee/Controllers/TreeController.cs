using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tee.Models;
namespace Tee.Controllers
{
    public class TreeController : Controller
    {
        Entities db = new Entities();
       // GET: Tree
            public ActionResult Index()
        {
            var accounts = db.Accounts.ToList();
            var treeData = GenerateTree(accounts, null);
            ViewBag.TreeData = treeData;
            ViewBag.AllAccounts = db.Accounts.ToList();
            return View();
        }

        private List<object> GenerateTreerr(List<Account> accounts, int? parentId)
        {
            var children = accounts.Where(a => a.ParentAccountId == parentId).ToList();
            return children.Select(c => new
            {
                text = new { name = c.AccountName },
                children = GenerateTree(accounts, c.AccountId)
            }).ToList<object>();
        }
        public JsonResult GetTreeData()
        {
            var accounts = db.Accounts.ToList();
            var treeData = GenerateTree(accounts, null);
            return Json(treeData, JsonRequestBehavior.AllowGet);
        }

        private List<object> GenerateTree(List<Account> accounts, int? parentId)
        {
            var children = accounts.Where(a => a.ParentAccountId == parentId).ToList();
            return children.Select(c => new
            {
                text = new { name = c.AccountName },
                children = GenerateTree(accounts, c.AccountId)
            }).ToList<object>();
        }



        [HttpPost]
        public ActionResult AddAccount(string accountName, int? parentAccountId)
        {
            var newAccount = new Account
            {
                AccountName = accountName,
                ParentAccountId = parentAccountId
            };
            db.Accounts.Add(newAccount);
            db.SaveChanges();

            return Json(new { success = true });
        }



        private string GetNameAddClass(string name)
        {
            string className = "<span class ='depart'>" + name + "</span>";
            return className;
        }










    }
}