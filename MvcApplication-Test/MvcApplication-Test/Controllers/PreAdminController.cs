﻿using MvcApplication.DAL;
using MvcApplication.BLL;
using MvcApplication_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcApplication_Test.Controllers
{
    public class PreAdminController : Controller
    {
        //
        // GET: /PreAdmin/
        public ActionResult Index(int page=1)
        {
            //
            List<PreAdminIndex> modelList = new List<PreAdminIndex>();
            modelList = GetProjectInfoList(page);
            GetProjectInfoList(page);
            ViewData["modelList"] = modelList;
            return View();
        }

        public ActionResult Edit(int id)
        {
            List<PreAdminIndex> list = GetProjectInfoList(0, id);
            ProAdminEdit model = new ProAdminEdit();

            if (list.Count > 0)
            {

                model = GetMachine(list[0].id);
                model.Pro = list[0];
            }

            return View(model);
            return View();
        }
        public ActionResult Show(int id)
        {
            List<PreAdminIndex> list = GetProjectInfoList(0, id);
            ProAdminEdit model = new ProAdminEdit();

            if (list.Count > 0)
            {

                model = GetMachine(list[0].id);
                model.Pro = list[0];
            }
            return View(model);
        }
        public ActionResult Create()
        {
            //PreAdminIndexModel model = new PreAdminIndexModel();
            return View();
        }
        //public void GetProjectInfoList(int page, int id = 0)
        //{
        //     DBHelper<ProjectInfo> dbhelper = new DBHelper<ProjectInfo>();
        //     var count = dbhelper.FindList(x => x.id > 0).ToList().Count(); ;
        //     int i = 0;
        //     List<ProjectInfo> entityes = dbhelper.FindPagedList(1, 1, out i, x => x.id > 0, s => s.createtime, true).ToList<ProjectInfo>();

        //     //List<UserInfo> list = entityes.ToList<UserInfo>();
        //}
        public List<PreAdminIndex> GetProjectInfoList(int page, int id = 0)
        {
            using (var db = new TestTryEntities1())
            {
                List<PreAdminIndex> modelList = new List<PreAdminIndex>();
                List<ProjectInfo> proList = new List<ProjectInfo>();
                if (id == 0)
                {
                    proList = db.ProjectInfo.OrderBy(x => x.CreateTime).Skip(page - 1 * 1).Take(1).ToList(); ;
                }
                else
                {
                    proList = db.ProjectInfo.Where(x => x.id == id).Take(1).ToList(); ;
                }

                ViewData["ProCount"] = db.ProjectInfo.Count();

                foreach (var item in proList)
                {
                    PreAdminIndex model = new PreAdminIndex();
                    model.id = item.id;
                    model.PName = item.PName;
                    model.PCity = item.PCity;
                    model.Salesman = item.Salesman;
                    model.SalesPhone = item.SalesPhone;
                    model.MchineNum = item.MchineNum;
                    model.StartTime = item.StartTime; ;
                    model.IsNow = item.IsNow;
                    model.SubmitTime = item.SubmitTime;
                    modelList.Add(model);

                }
                return modelList;
            }
        }

        public ProAdminEdit GetMachine(int proId)
        {
            using (var db = new TestTryEntities1())
            {
                List<machineinfo> malist = db.machineinfo.Where(x => x.ProId == proId).ToList();
                //TestTryEntities1<machineinfo> entry = db.machineinfo<machineinfo>.Where(x => x.proID == proId).ToList();
                ProAdminEdit model = new ProAdminEdit();
                foreach (var ma in malist)
                {

                    // model.Machine. = ma.id;
                    // model.Machine.machineName = ma.machineName;
                    // model.Machine.test = ma.test;
                    // model.Machine.other = ma.other;
                    //model.List.Add(model.Machine)
                    model.Machine.id = ma.id;
                    model.Machine.MachineName =ma.MachineName;
                    model.Machine.MachineNum = ma.MachineNum;
                    model.Machine.MachineModel = ma.MachineModel;
                    model.Machine.Remark = ma.Remark;

                    model.List.Add(model.Machine);
                }
                return model;
            }

        }
        [HttpPost]
        public int InserProject(string Pname, string PjCity, string MachineNum, string IsNow, string PjXSName, string IsDealer, string Phone, string Other)
        {
            using (var db = new TestTryEntities1())
            {
                //数据操作
                //var getGser = db.User.Where(x => x.Name == name || x.Email == email).Take(1).ToList();
                //if (getGser.Count > 0)
                //{
                //}
                ProjectInfo pro = new ProjectInfo()
                {
                    PName = Pname,
                    PCity = PjCity,
                    MchineNum = MachineNum,
                    IsNow = Convert.ToInt32(IsNow),
                    Salesman = PjXSName,
                    StartTime = IsDealer,
                    SalesPhone = Phone,
                    SubmitTime = Other,
                    CreateTime = DateTime.UtcNow,
                    UpdateTime=DateTime.UtcNow
                };
                db.ProjectInfo.Add(pro);
                db.SaveChanges();
                return pro.id;
            }
        }

        [HttpPost]
        public bool InserMachineInfo(string jsonstr,int id)
        {
            try
            {
                //use json.net
                JArray o = (JArray)JsonConvert.DeserializeObject(jsonstr);
                IList<JToken> oList = (IList<JToken>)o;
                int proId = 0;
                foreach (JToken jt in oList)
                {

                    JObject jo = jt as JObject;
                    using (var db = new TestTryEntities1())
                    {
                        if (jt == o[0])
                        {
                            ProjectInfo pro = new ProjectInfo()
                            {
                                PName = jo["Pname"].ToString(),
                                PCity = jo["PjCity"].ToString(),
                                MchineNum = jo["MachineNum"].ToString(),
                                StartTime = jo["IsNow"].ToString(),
                                Salesman = jo["PjXSName"].ToString(),
                                IsNow =Convert.ToInt32(jo["IsDealer"].ToString()),
                                SalesPhone = jo["Phone"].ToString(),
                                SubmitTime = jo["Other"].ToString(),
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now
                            };
                            db.ProjectInfo.Add(pro);
                            db.SaveChanges();
                            proId = pro.id;
                        }
                        if (oList.Count >= 1 & jt != o[0])
                        {
                            machineinfo model = new machineinfo()
                            {
                                MachineName = jo["PjName"].ToString(),
                                MachineNum = Convert.ToInt32(jo["PjNum"].ToString()),
                                MachineModel = jo["Xdd"].ToString(),
                                Remark = jo["Xddw"].ToString(),
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                ProId= proId

                            };
                            db.machineinfo.Add(model);
                            db.SaveChanges();
                        }

                    }
                }
                return true;
            }
            catch (Exception e )
            {

                return false;
            }
           
        }
    }
}

