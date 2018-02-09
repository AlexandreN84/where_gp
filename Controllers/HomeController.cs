using GestaoPatrimonio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GestaoPatrimonio.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (Session["path"] == null)
            {
                ViewBag.Path = ((System.Web.HttpContextWrapper)HttpContext).Request.Url.AbsoluteUri;
                Session["path"] = ViewBag.Path;
            }
            else
            {
                ViewBag.Path = Session["path"].ToString();
            }
            //importa();
            return View();
        }

        public void importa()
        {
            int i = 0;
            string linhascompau = "";
            //
            using (var rd = new StreamReader(@"D:\GoogleDrive\Empresa\Projetos\Senai\Docs\dados.csv", Encoding.GetEncoding("iso-8859-1")))
            {
                while (!rd.EndOfStream)
                {
                    //0 - descricao 1 - ncm 2 - cst 3 - cfop 4 - un 5 - quantidade 6 - valor unidade 7 - valor total 8 - base calc icm 9 - valor icms 10 - valor ipi 11 - aliq icms 12 - ali ipi
                    var splits = rd.ReadLine().Split(',');
                    //
                    if (splits.Length == 8)
                    {
                        string centro = splits[0].ToString();
                        string conta = splits[1].ToString();
                        string numero_patrimonio = splits[2].ToString();
                        string descricao_patrimonio = splits[3].ToString();
                        string data_aquisicao = splits[4].ToString();
                        string data_inicio_uso = splits[5].ToString();
                        string tava_depreciacao = splits[6].ToString();
                        string vl_original = splits[7].ToString();
                        //
                        int icentro = Convert.ToInt32(centro);
                        int iconta = Convert.ToInt32(conta);
                        //
                        Patrimonio patr = new Patrimonio();
                        patr.Centro = db.Centro.Where(a => a.Id == icentro).FirstOrDefault();
                        patr.Conta = db.Conta.Where(a => a.Cd_Hospital == conta).FirstOrDefault();
                        //
                        patr.Numero = Convert.ToInt32(numero_patrimonio);
                        patr.Descricao = descricao_patrimonio;
                        patr.Dt_Aquisicao = data_aquisicao;
                        patr.Dt_InicioUso = data_inicio_uso;
                        patr.Tv_Depreciacao = tava_depreciacao;
                        patr.Valor_Original = vl_original;
                        //
                        db.Patrimonio.Add(patr);


                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {

                            #region Log

                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }

                            #endregion
                        }
                        catch (Exception exc)
                        {
                            //sRet = exc.Message;
                        }
                    }
                    else
                    {
                        linhascompau += i + ",";
                    }
                    //
                    i++;
                }
            }
        }

        public ActionResult Index_outro(string achmISjRI05bmZzv3hcBHXw)
        {
            //string ipaddress = GetIPAddress();
            //string sql = string.Empty;
            ////
            //if (!string.IsNullOrEmpty(achmISjRI05bmZzv3hcBHXw))
            //{
            //    sql = "INSERT INTO TestTable (a,b,c,d) VALUES ('" + Request.Url.OriginalString + " -- " + achmISjRI05bmZzv3hcBHXw + "','" + ipaddress + "','" + Request.UserHostAddress + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "')";
            //}
            //else
            //{
            //    sql = "INSERT INTO TestTable (a,b,c,d) VALUES ('" + Request.Url.OriginalString + "','" + ipaddress + "','" + Request.UserHostAddress + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "')";               
            //}
            ////
            //ExecutaSQLNonQuery(sql);
            //
            List<string> lista = new List<string>();
            int j = 47240;
            //
            for (int i = 0; i < 1; i++)
            {
                try
                {
                    //47242
                    //8028547244
                    //
                    WebRequest request = WebRequest.Create("https://www.google.com.br/search?q=cadeira+hospitalar&oq=cadeira+hospitalar");
                    request.Method = "GET";
                    using (WebResponse response2 = request.GetResponse())
                    {
                        using (Stream stream = response2.GetResponseStream())
                        {
                            StreamReader www = new StreamReader(stream);
                            string responsasasaseFromServer = www.ReadToEnd();
                            //XmlTextReader readerwww = new XmlTextReader(stream);
                            //string data = "THExxQUICKxxBROWNxxFOX";
                            string valida = "a href=\"/url?q=";
                            string[] ssss = responsasasaseFromServer.Split(new string[] { valida }, StringSplitOptions.None);
                        }
                    }

                    j = j + 2;
                    //
                    string url = "https://www.google.com.br/search?q=cadeira+hospitalar&oq=cadeira+hospitalar";
                    WebRequest request2 = WebRequest.Create(url);
                    request.Method = "GET";
                    request.Timeout = 120000;
                    string postData = "";
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse response = request.GetResponse();
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    //
                    if (responseFromServer.Length > 1800)
                    {
                        lista.Add(url);
                    }
                    //if (responseFromServer.Length.ToString().Equals("6242"))
                    //{

                    //}
                    //if (!string.IsNullOrWhiteSpace(responseFromServer))
                    //{
                    //    lista.Add(responseFromServer.Length.ToString());
                    //}
                    //else
                    //{
                    //    lista.Add("bug");
                    //}
                    //bool brom_comissao = false;
                    //HtmlString
                    //XmlDocument xml = new XmlDocument();

                    //xml.LoadXml(responseFromServer);
                }
                catch (Exception exc)
                {
                    lista.Add("errro " + exc.Message);
                }
                //responseFromServer.Length
            }
            //


            return View();
        }

        public ActionResult Index_webmanining(string achmISjRI05bmZzv3hcBHXw)
        {
            //string ipaddress = GetIPAddress();
            //string sql = string.Empty;
            ////
            //if (!string.IsNullOrEmpty(achmISjRI05bmZzv3hcBHXw))
            //{
            //    sql = "INSERT INTO TestTable (a,b,c,d) VALUES ('" + Request.Url.OriginalString + " -- " + achmISjRI05bmZzv3hcBHXw + "','" + ipaddress + "','" + Request.UserHostAddress + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "')";
            //}
            //else
            //{
            //    sql = "INSERT INTO TestTable (a,b,c,d) VALUES ('" + Request.Url.OriginalString + "','" + ipaddress + "','" + Request.UserHostAddress + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "')";               
            //}
            ////
            //ExecutaSQLNonQuery(sql);
            //
            List<string> lista = new List<string>();
            int j = 47240;
            //
            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    //47242
                    //8028547244
                    //

                    j = j + 2;
                    //
                    string url = "http://ssw.inf.br/cgi-local/tracking/11200418000401/80285" + j.ToString();
                    WebRequest request = WebRequest.Create(url);
                    request.Method = "POST";
                    request.Timeout = 120000;
                    string postData = "";
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse response = request.GetResponse();
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    //
                    if (responseFromServer.Length > 1800)
                    {
                        lista.Add(url);
                    }
                    //if (responseFromServer.Length.ToString().Equals("6242"))
                    //{

                    //}
                    //if (!string.IsNullOrWhiteSpace(responseFromServer))
                    //{
                    //    lista.Add(responseFromServer.Length.ToString());
                    //}
                    //else
                    //{
                    //    lista.Add("bug");
                    //}
                    //bool brom_comissao = false;
                    //HtmlString
                    //XmlDocument xml = new XmlDocument();

                    //xml.LoadXml(responseFromServer);
                }
                catch (Exception exc)
                {
                    lista.Add("errro " + exc.Message);
                }
                //responseFromServer.Length
            }
            //


            return View();
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Panel()
        //{
        //    var model = new PanelModel();

        //    if (Session["email"] != null)
        //    {
        //        string email = Session["email"].ToString();
        //        User user = db.User.Where(a => a.Email.Equals(email)).FirstOrDefault();
        //        //
        //        model = new PanelModel
        //        {
        //            User = user
        //        };
        //        //
        //        ViewBag.Message = "Your panel page.";
        //        return View(model);
        //    }
        //    model = new PanelModel
        //    {
        //        User = null
        //    };
        //    ViewBag.Message = "User not found";
        //    return View(model);
        //}

        public ActionResult Registered()
        {
            ViewBag.Message = "Check your email to complete the registration.";

            return View();
        }

        public ActionResult Patrimonio(int pagesize, string SearchKey)
        {
            List<Patrimonio> lista_patrimonio = new List<Models.Patrimonio>();
            //
            if (!string.IsNullOrEmpty(SearchKey))
            {
                lista_patrimonio = db.Patrimonio.Where(a => a.Descricao.Contains(SearchKey)).ToList();
                ViewBag.SearchRetorno = lista_patrimonio.Count + " itens encontrados";
            }
            else
            {
                //lista_patrimonio = db.Patrimonio.Where(a => a.Id > 4).ToList();
                lista_patrimonio = db.Patrimonio.ToList();
            }
            //
            lista_patrimonio = lista_patrimonio.Select(s => new Patrimonio()
            {
                Descricao = s.Descricao.Length > 60 ? s.Descricao.Substring(0, 60) : s.Descricao,
                Id = s.Id,
                Numero = s.Numero,
                Dt_Aquisicao = s.Dt_Aquisicao,
                Dt_InicioUso = s.Dt_InicioUso,
                Valor_Original = s.Valor_Original,
                Tv_Depreciacao = s.Tv_Depreciacao,
                Resumo = s.Resumo,
                Qt_DiasCalibracao = s.Qt_DiasCalibracao,
                Qt_DiasDepreciacao = s.Qt_DiasDepreciacao,
                Qt_DiasManutencao = s.Qt_DiasManutencao,
                Qt_DiasTesteEletrico = s.Qt_DiasTesteEletrico

            }).ToList();
            ViewBag.Patrimonio = "ok";
            ViewBag.PageSize = pagesize;
            ViewBag.Search = SearchKey;
            //
            return View(lista_patrimonio);
        }

        public ActionResult Conta(int pagesize, string SearchKey)
        {
            List<Conta> lista_conta = new List<Models.Conta>();
            //
            if (!string.IsNullOrEmpty(SearchKey))
            {
                lista_conta = db.Conta.Where(a => a.Descricao.Contains(SearchKey)).ToList();
                ViewBag.SearchRetorno = lista_conta.Count + " itens encontrados";
            }
            else
            {
                lista_conta = db.Conta.Where(a => a.Id > 0).ToList();
            }
            //
            lista_conta = lista_conta.Select(s => new Conta()
            {
                Descricao = s.Descricao.Length > 60 ? s.Descricao.Substring(0, 60) : s.Descricao,
                Id = s.Id,
                Resumo = s.Resumo,
                Cd_Hospital = s.Cd_Hospital

            }).ToList();
            ViewBag.Conta = "ok";
            ViewBag.PageSize = pagesize;
            ViewBag.Search = SearchKey;
            //
            return View(lista_conta);
        }

        public ActionResult PatrimonioBusca(string SearchKey)
        {
            List<Patrimonio> lista_patrimonio = new List<Models.Patrimonio>();
            if (!string.IsNullOrEmpty(SearchKey))
            {
                lista_patrimonio = db.Patrimonio.Where(a => a.Descricao.Contains(SearchKey)).ToList();
            }
            else
            {
                lista_patrimonio = db.Patrimonio.Where(a => a.Id > 4).ToList();
            }
            //
            lista_patrimonio = lista_patrimonio.Select(s => new Patrimonio()
            {
                Descricao = s.Descricao.Length > 60 ? s.Descricao.Substring(0, 60) : s.Descricao,
                Id = s.Id,
                Numero = s.Numero,
                Dt_Aquisicao = s.Dt_Aquisicao,
                Valor_Original = s.Valor_Original,

            }).ToList();
            ViewBag.Patrimonio = "ok";
            ViewBag.PageSize = 15;
            //
            return View(lista_patrimonio);
        }

        public ActionResult EditPatrimonio(int id)
        {
            Patrimonio patrimonio = db.Patrimonio.Where(a => a.Id == id).FirstOrDefault();
            List<Centro> lista_centros = db.Centro.ToList();
            List<Conta> lista_contas = db.Conta.ToList();
            //
            ViewBag.ContaId = lista_contas.Where(a => a.Id == patrimonio.Id_Conta).FirstOrDefault().Id;
            ViewBag.CentroId = lista_centros.Where(a => a.Id == patrimonio.Id_Centro).FirstOrDefault().Id;
            ViewBag.Contas = lista_contas;
            ViewBag.Centros = lista_centros;
            //
            return View(patrimonio);
        }

        public JsonResult EditPatrimonioPost(int id, string descricao, string vloriginal, string txdepreciacao, string qtdiascalibracao, string qtdiasdepreciacao,
                                              string qtdiasmanutencao, string qtdiastesteeletrico, int centro, int conta)
        {
            string ret = string.Empty;
            string page = string.Empty;
            string pagesize = string.Empty;
            //
            if (Session["CurrentPage"] != null)
            {
                page = Session["CurrentPage"].ToString();
            }
            //
            if (Session["PageSize"] != null)
            {
                pagesize = Session["PageSize"].ToString();
            }
            //
            try
            {
                Patrimonio pat = db.Patrimonio.Where(a => a.Id == id).FirstOrDefault();
                //
                if (pat != null)
                {
                    pat.Descricao = descricao;
                    pat.Valor_Original = vloriginal;
                    pat.Tv_Depreciacao = txdepreciacao;
                    pat.Qt_DiasCalibracao = Convert.ToInt32(qtdiascalibracao);
                    pat.Qt_DiasDepreciacao = Convert.ToInt32(qtdiasdepreciacao);
                    pat.Qt_DiasManutencao = Convert.ToInt32(qtdiasmanutencao);
                    pat.Qt_DiasTesteEletrico = Convert.ToInt32(qtdiastesteeletrico);
                    //
                    Centro ocentro = db.Centro.Where(a => a.Id == centro).FirstOrDefault();
                    //
                    if (ocentro != null)
                    {
                        pat.Centro = ocentro;
                    }
                    //
                    Conta oconta = db.Conta.Where(a => a.Id == conta).FirstOrDefault();
                    //
                    if (oconta != null)
                    {
                        pat.Conta = oconta;
                    }
                    //
                    db.Entry(pat).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ret = "ok";
                }
                else
                {
                    return this.Json((object)new
                    {
                        data = "Patrimônio (" + id + ") não encontrado",
                        success = true,
                        errors = string.Empty
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return this.Json((object)new
                {
                    data = (ex.Message + " - " + ex.StackTrace),
                    success = true,
                    errors = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            return this.Json((object)new
            {
                data = ret,
                page = page,
                pagesize = pagesize,
                success = true,
                errors = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Manutencao(int pagesize, string SearchKey)
        {

            //List<Manutencao> listaManutencao = new List<Models.Manutencao>();
            ////
            //if (!string.IsNullOrEmpty(SearchKey))
            //{
            //    listaManutencao = db.Manutencao.Where(a => a.Descricao.Contains(SearchKey)).ToList();
            //    ViewBag.SearchRetorno = listaManutencao.Count + " itens encontrados";
            //}
            //else
            //{
            //    listaManutencao = db.Manutencao.Where(a => a.Id > 0).ToList();
            //}
            ////
            //listaManutencao = listaManutencao.Select(s => new Manutencao()
            //{
            //    Descricao = s.Descricao.Length > 60 ? s.Descricao.Substring(0, 60) : s.Descricao,
            //    Id = s.Id,
            //    Resumo = s.Resumo
            //}).ToList();
            //ViewBag.Manutencao = "ok";
            //ViewBag.PageSize = pagesize;
            //ViewBag.Search = SearchKey;
            //
            //return View(listaManutencao);
            return View();
        }

        public ActionResult Centro(int pagesize, string SearchKey)
        {
            List<Centro> lista_centro = new List<Models.Centro>();
            //
            if (!string.IsNullOrEmpty(SearchKey))
            {
                lista_centro = db.Centro.Where(a => a.Descricao.Contains(SearchKey)).ToList();
                ViewBag.SearchRetorno = lista_centro.Count + " itens encontrados";
            }
            else
            {
                lista_centro = db.Centro.Where(a => a.Id > 0).ToList();
            }
            //
            lista_centro = lista_centro.Select(s => new Centro()
            {
                Descricao = s.Descricao.Length > 60 ? s.Descricao.Substring(0, 60) : s.Descricao,
                Id = s.Id,
                Resumo = s.Resumo,


            }).ToList();
            ViewBag.Centro = "ok";
            ViewBag.PageSize = pagesize;
            ViewBag.Search = SearchKey;
            //
            return View(lista_centro);
        }

        public ActionResult Tag(int pagesize, string SearchKey)
        {
            List<Tag> lista_tag = new List<Models.Tag>();
            //
            if (!string.IsNullOrEmpty(SearchKey))
            {
                lista_tag = db.Tag.Where(a => a.Descricao.Contains(SearchKey)).ToList();
                ViewBag.SearchRetorno = lista_tag.Count + " itens encontrados";
            }
            else
            {
                lista_tag = db.Tag.Where(a => a.Id > 4).ToList();
            }
            //
            lista_tag = lista_tag.Select(s => new Tag()
            {
                Descricao = s.Descricao.Length > 60 ? s.Descricao.Substring(0, 60) : s.Descricao,
                Id = s.Id,
                Id_Patrimonio = s.Id_Patrimonio,
                Id_RadioBase = s.Id_RadioBase,
                Resumo = s.Resumo


            }).ToList();
            ViewBag.Patrimonio = "ok";
            ViewBag.PageSize = pagesize;
            ViewBag.Search = SearchKey;
            //
            return View(lista_tag);
        }

        public ActionResult Tarefas()
        {
            return View();
        }

        public ActionResult Mercadowhere()
        {
            return View();
        }

        public ActionResult PatrimonioManutencao()
        {
            ViewBag.Message = "Conta";
            return View();
        }

        public JsonResult TesteRele(string status)
        {
            int num;
            try
            {
                num = !status.Equals("0") ? ExecutaSQLNonQuery("update Sensor set Status = 1 where ID = 2") : ExecutaSQLNonQuery("update Sensor set Status = 0 where ID = 2");
            }
            catch (Exception ex)
            {
                return this.Json((object)new
                {
                    data = (ex.Message + " - " + ex.StackTrace),
                    success = true,
                    errors = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            return this.Json((object)new
            {
                data = num.ToString(),
                success = true,
                errors = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Email(string texto)
        {
            string str = "";
            try
            {
                //DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time")).AddHours(1.0);
                //LogAccess logAccess = new LogAccess();
                //logAccess.Data = new DateTime?(dateTime);
                //Sensor sensor = db.Sensor.Where(a => a.ID == 1).FirstOrDefault();
                //logAccess.Sensor = sensor;
                //db.LogAccess.Add(logAccess);
                //db.SaveChanges();
            }
            catch (Exception ex)
            {
                return this.Json((object)new
                {
                    data = ("exc1 : " + ex.Message + " - " + ex.StackTrace),
                    success = true,
                    errors = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            return this.Json((object)new
            {
                data = str,
                success = true,
                errors = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditConta(int id)
        {
            Conta conta = db.Conta.Where(a => a.Id == id).FirstOrDefault();
            List<Centro> lista_centros = db.Centro.ToList();
            List<Conta> lista_contas = db.Conta.ToList();
            //
            //ViewBag.ContaId = lista_contas.Where(a => a.Id == patrimonio.Id_Conta).FirstOrDefault().Id;
            //ViewBag.CentroId = lista_centros.Where(a => a.Id == patrimonio.Id_Centro).FirstOrDefault().Id;
            ViewBag.Contas = lista_contas;
            ViewBag.Centros = lista_centros;
            //
            return View(conta);
        }

        public ActionResult EditCentro(int id)
        {
            Centro centro = db.Centro.Where(a => a.Id == id).FirstOrDefault();
            List<Centro> lista_centros = db.Centro.ToList();
            List<Conta> lista_contas = db.Conta.ToList();
            //
            //ViewBag.ContaId = lista_contas.Where(a => a.Id == patrimonio.Id_Conta).FirstOrDefault().Id;
            //ViewBag.CentroId = lista_centros.Where(a => a.Id == patrimonio.Id_Centro).FirstOrDefault().Id;
            ViewBag.Contas = lista_contas;
            ViewBag.Centros = lista_centros;
            //
            return View(centro);
        }

        public JsonResult EditContaPost(int id, string cd_Hospital, string resumo, string descricao, string vloriginal, int centro, int conta)
        {
            string ret = string.Empty;
            string page = string.Empty;
            string pagesize = string.Empty;
            //
            if (Session["CurrentPage"] != null)
            {
                page = Session["CurrentPage"].ToString();
            }
            //
            if (Session["PageSize"] != null)
            {
                pagesize = Session["PageSize"].ToString();
            }
            //
            try
            {
                Conta pat = db.Conta.Where(a => a.Id == id).FirstOrDefault();
                //
                if (pat != null)
                {
                    pat.Descricao = descricao;
                    pat.Resumo = resumo;
                    pat.Cd_Hospital = cd_Hospital;

                    db.Entry(pat).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ret = "ok";
                }
                else
                {
                    return this.Json((object)new
                    {
                        data = "Patrimônio (" + id + ") não encontrado",
                        success = true,
                        errors = string.Empty
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return this.Json((object)new
                {
                    data = (ex.Message + " - " + ex.StackTrace),
                    success = true,
                    errors = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            return this.Json((object)new
            {
                data = ret,
                page = page,
                pagesize = pagesize,
                success = true,
                errors = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditCentroPost(int id, string resumo, string descricao, string vloriginal, int centro, int conta)
        {
            string ret = string.Empty;
            string page = string.Empty;
            string pagesize = string.Empty;
            //
            if (Session["CurrentPage"] != null)
            {
                page = Session["CurrentPage"].ToString();
            }
            //
            if (Session["PageSize"] != null)
            {
                pagesize = Session["PageSize"].ToString();
            }
            //
            try
            {
                Centro pat = db.Centro.Where(a => a.Id == id).FirstOrDefault();
                //
                if (pat != null)
                {
                    pat.Descricao = descricao;
                    pat.Resumo = resumo;
                    db.Entry(pat).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ret = "ok";
                }
                else
                {
                    return this.Json((object)new
                    {
                        data = "Centro (" + id + ") não encontrado",
                        success = true,
                        errors = string.Empty
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return this.Json((object)new
                {
                    data = (ex.Message + " - " + ex.StackTrace),
                    success = true,
                    errors = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            return this.Json((object)new
            {
                data = ret,
                page = page,
                pagesize = pagesize,
                success = true,
                errors = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Tag_Save()
        {
            Tag_SVG_Symbol oTag_SVG_Symbol = new Tag_SVG_Symbol();
            Tag_Item_li oTag_Item_li = new Tag_Item_li();
            Tag_Hyperlink oTag_Hyperlink = new Tag_Hyperlink();
            Tag_Div oTag_Div = new Tag_Div();
            Tag_HTML oTag_HTML = new Tag_HTML();
            Tag oTag = new Tag();
            //
            //oTag_SVG_Symbol.Classe = 
            //
            return this.Json((object)new
            {
                data = "".ToString(),
                success = true,
                errors = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }
    }
}