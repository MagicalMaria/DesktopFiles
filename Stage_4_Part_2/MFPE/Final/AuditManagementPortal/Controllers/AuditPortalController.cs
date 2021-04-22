using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditManagementPortal.Models;
using AuditManagementPortal.Services;
using Microsoft.Extensions.Configuration;
using AuditManagementPortal.Models;
using Microsoft.AspNetCore.Http;

namespace AuditManagementPortal.Controllers
{
    public class AuditPortalController : Controller
    {
        private IConfiguration _configuration;
        private readonly AuditManagementContext _context;

        public AuditPortalController(IConfiguration configuration, AuditManagementContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                HttpContext.Response.Cookies.Delete("Token"); //This deletes the old token that was used
                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Login(Credentials credentials)
        {
            try
            {
                var authService = new AuthorizationService(_configuration); //Authorization Service
                var token = authService.GetToken(credentials);

                if (token != null)
                {
                    //HttpContext.Session.SetString("token", token);    //another method to store the token
                    //HttpContext.Session.GetString("token"));
                    HttpContext.Response.Cookies.Append("Token", token); //Add the token to the cookies
                    return View("Home");
                }

                ViewBag.Message = "Invalid Credentials";
                return View("Login");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Home()
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult CheckList(AuditDetails auditDetails)
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                List<CheckListQuestion> questionList = new List<CheckListQuestion>();
                CheckListService checkListService = new CheckListService(_configuration);


                questionList = checkListService.GetQuestions(auditDetails.Type, token);

                return View(questionList);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult AuditForm()
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Severity(AuditRequest auditRequest)
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                if (auditRequest.AuditDetails.Type == "Internal") return RedirectToAction("Internal");
                else if (auditRequest.AuditDetails.Type == "SOX") return RedirectToAction("SOX");

                return View("Error");
            }
            catch (Exception e)
            {
                return View("Error");

            }
        }

        [HttpGet]
        public IActionResult Internal()
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult InternalAuditResponse(InternalQuestions questions)
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                SeverityService service = new SeverityService(_configuration);
                var auditResponse = service.GetInternalResponse(token, questions);

                //save to the context
                //_context.AuditResponses.Add(auditResponse);
                //_context.SaveChanges();

                return View("AuditResponse",auditResponse);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult SOX()
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult SOXAuditResponse(SOXQuestions questions)
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                SeverityService service = new SeverityService(_configuration);
                var auditResponse = service.GetSOXResponse(token, questions);

                //add auditResponse to DB
                _context.AuditResponses.Add(auditResponse);
                _context.SaveChanges();

                return View("AuditResponse",auditResponse);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                var token = HttpContext.Request.Cookies["Token"]; //Get the value of Token from Cookie

                if (token == null)
                {
                    ViewBag.Message = "Session Expired !! Please Login ....";
                    return View("Login");
                }

                HttpContext.Response.Cookies.Delete("Token");   //delete the token from the cookie
                return View("Login");
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }
    }
}
