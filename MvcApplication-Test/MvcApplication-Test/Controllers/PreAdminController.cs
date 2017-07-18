using MvcApplication.DAL;
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
            List<PreAdminIndexModel> modelList = new List<PreAdminIndexModel>();
            modelList = GetProjectInfoList(page);
            ViewData["modelList"] = modelList;
            return View();
        }

        public ActionResult Edit(int id)
        {
            List<PreAdminIndexModel> list = GetProjectInfoList(0, id);
            ProAdminEditModel model = new ProAdminEditModel();
            
            if (list.Count > 0)
            {
                
                model = GetMachine(list[0].id);
                model.listPro = list[0];
            }
            
            return View(model);
        }
        public ActionResult Create()
        {
            PreAdminIndexModel model = new PreAdminIndexModel();
            return View(model);
        }
        public   List<PreAdminIndexModel> GetProjectInfoList( int page,int id=0)
        {
            using (var db = new TestTryEntities1())
            {
                List<PreAdminIndexModel> modelList = new List<PreAdminIndexModel>();
                List<ProjectInfo> proList = new List<ProjectInfo>();
                if (id == 0)
                {
                    proList = db.ProjectInfo.OrderBy(x => x.createtime).Skip(page - 1 * 1).Take(1).ToList(); ;
                }
                else
                {
                    proList = db.ProjectInfo.Where(x=>x.id==id).Take(1).ToList(); ;
                }
               
                ViewData["ProCount"] = db.ProjectInfo.Count().ToString();

                foreach (var item in proList)
                {
                    PreAdminIndexModel model = new PreAdminIndexModel();
                    model.id = item.id;
                    model.PjName = item.pname;
                    model.pjCity = item.pjCity;
                    model.PjXSName = item.pjXSName;
                    model.Phone = item.phone;
                    model.MchineNum = item.machineNum;
                    model.IsDealer = item.isDealer.ToString(); ;
                    model.IsNow = item.isNow.ToString();
                    model.DateTime = item.other;
                    modelList.Add(model);
                    
                }
                return modelList;
            }
        }

        public ProAdminEditModel GetMachine(int proId)
        {
            using ( var db=new TestTryEntities1())
            {
                  var malist = db.machineinfo.Where(x => x.proID == proId).ToList();
                  ProAdminEditModel model = new ProAdminEditModel();
                  foreach (var ma in malist)
                    {

                       // model.Machine. = ma.id;
                       // model.Machine.machineName = ma.machineName;
                       // model.Machine.test = ma.test;
                       // model.Machine.other = ma.other;
                       //model.List.Add(model.Machine)
                        model.Machine.id = ma.id;
                        model.Machine.machineName = ma.machineName;
                        model.Machine.machineNum = ma.machineNum;
                        model.Machine.test = ma.test;
                        model.Machine.other = ma.other;

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
                    pname = Pname,
                    pjCity = PjCity,
                    machineNum = MachineNum,
                    isNow = Convert.ToInt32(IsNow),
                    pjXSName = PjXSName,
                    isDealer = Convert.ToInt32(IsDealer),
                    phone = Phone,
                    other = Other,
                    createtime = DateTime.UtcNow,
                    fixtime=DateTime.UtcNow
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
                                pname = jo["Pname"].ToString(),
                                pjCity = jo["PjCity"].ToString(),
                                machineNum = jo["MachineNum"].ToString(),
                                isNow = Convert.ToInt32(jo["IsNow"].ToString()),
                                pjXSName = jo["PjXSName"].ToString(),
                                isDealer = Convert.ToInt32(jo["IsDealer"].ToString()),
                                phone = jo["Phone"].ToString(),
                                other = jo["Other"].ToString(),
                                createtime = DateTime.Now,
                                fixtime = DateTime.Now
                            };
                            db.ProjectInfo.Add(pro);
                            db.SaveChanges();
                            proId = pro.id;
                        }
                        if (oList.Count >= 1 & jt != o[0])
                        {
                            machineinfo model = new machineinfo()
                            {
                                machineName = jo["PjName"].ToString(),
                                machineNum = Convert.ToInt32(jo["PjNum"].ToString()),
                                test = jo["Xdd"].ToString(),
                                other = jo["Xddw"].ToString(),
                                createtime = DateTime.Now,
                                fixtime = DateTime.Now,
                                proID=proId

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

