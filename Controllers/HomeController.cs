using cms_pract.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Web;
using System.Text;
using System.Net;
using Azure.Core;
using System.Text.Json.Nodes;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using cms_pract.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Security.Claims;
using System.Runtime.CompilerServices;

namespace cms_pract.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        public readonly UserManager<CmsPractUser> _userManager;
        public readonly ApplicationDbContext _applicationDbContext;

        private LinkedInContactsInfoViewModel _linkedInContactsInfoViewModel = new LinkedInContactsInfoViewModel();

        Uri connectionEndpoint = new Uri("https://api.linkedin.com/v2/connections?q=viewer&projection=(elements*(to~(emailAddress)))");

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientFactory clientFactory, UserManager<CmsPractUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _clientFactory = clientFactory;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RedirectToLinkedIn() 
        {
            //https://www.linkedin.com/oauth/v2/authorization
            //?response_type=code
            //&client_id=_configuration["Authentication:LinkedIn:ClientId"]
            //&client_secret=_configuration["Authentication:LinkedIn:ClientSecret"]
            //&redirect_uri=https://oauth.pstmn.io/v1/callback
            //&scope=email%20profile

            var linkedInContactsEmails = new List<string> { "pending" };

            UriBuilder authorizationBaseUrl = new UriBuilder("https://www.linkedin.com/oauth/v2/authorization");
            var query = HttpUtility.ParseQueryString(authorizationBaseUrl.Query);

            query["response_type"] = "code";
            query["client_id"] = _configuration["Authentication:LinkedIn:ClientId"];
            query["client_secret"] = _configuration["Authentication:LinkedIn:ClientSecret"];
            query["redirect_uri"] = "https://localhost:5001/Home/LinkedInCallback";
            query["scope"] = "email profile";

            authorizationBaseUrl.Query = query.ToString();

            return Redirect(authorizationBaseUrl.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> ContactsInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var emailModels = await _applicationDbContext.ContactsEmails
                    .Where(e => e.UserProfileId == user.Id)
                    .Select(e => new EmailModel { 
                        email = e.Email,
                        isSelected = e.IsSelected
                    })
                    .ToListAsync();
                _linkedInContactsInfoViewModel.emails = emailModels;
                return View(_linkedInContactsInfoViewModel);
            }
            return View(_linkedInContactsInfoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ContactsInfo(LinkedInContactsInfoViewModel linkedInContactsInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    _linkedInContactsInfoViewModel.emails.Add(new EmailModel {
                        email = linkedInContactsInfoViewModel.email,
                        isSelected = false
                    });
                    user.ContactsEmail.Add(new ContactsEmail { Email = linkedInContactsInfoViewModel.email, IsSelected = false, UserProfileId = user.Id });

                    // Save the changes to the database
                    _applicationDbContext.Update(user);
                    await _applicationDbContext.SaveChangesAsync();

                    var  emailModels = await _applicationDbContext.ContactsEmails
                        .Where(e => e.UserProfileId == user.Id)
                        .Select(e => new EmailModel
                        {
                            email = e.Email,
                            isSelected = e.IsSelected
                        })
                        .ToListAsync();

                    return View(new LinkedInContactsInfoViewModel { emails = emailModels});
                }
            }
            return View(_linkedInContactsInfoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SelectEmail(LinkedInContactsInfoViewModel linkedInContactsInfoViewModel) 
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _applicationDbContext.Users.Include(u => u.ContactsEmail)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                var emailToSelect = user.ContactsEmail.FirstOrDefault(e => e.Email == linkedInContactsInfoViewModel.selectedEmail);
                if (emailToSelect != null)
                {
                    emailToSelect.IsSelected = true;
                    await _applicationDbContext.SaveChangesAsync();

                    var updatedemails = await _applicationDbContext.ContactsEmails
                        .Where(e => e.UserProfileId == user.Id)
                        .Select(e => new EmailModel
                        {
                            email = e.Email,
                            isSelected = e.IsSelected
                        })
                        .ToListAsync();

                    return View("ContactsInfo", new LinkedInContactsInfoViewModel { emails = updatedemails });
                }
            }

            var emails = await _applicationDbContext.ContactsEmails
                        .Where(e => e.UserProfileId == user.Id)
                        .Select(e => new EmailModel
                        {
                            email = e.Email,
                            isSelected = e.IsSelected
                        })
                        .ToListAsync();

            return View("ContactsInfo", new LinkedInContactsInfoViewModel { emails = emails });
        }
        [HttpPost]
        public async Task<IActionResult> UnselectEmail(LinkedInContactsInfoViewModel linkedInContactsInfoViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _applicationDbContext.Users.Include(u => u.ContactsEmail)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                var emailToUnselect = user.ContactsEmail.FirstOrDefault(e => e.Email == linkedInContactsInfoViewModel.unselectedEmail);
                if (emailToUnselect != null)
                {
                    emailToUnselect.IsSelected = false;
                    await _applicationDbContext.SaveChangesAsync();

                    var updatedemails = await _applicationDbContext.ContactsEmails
                        .Where(e => e.UserProfileId == user.Id)
                        .Select(e => new EmailModel
                        {
                            email = e.Email,
                            isSelected = e.IsSelected
                        })
                        .ToListAsync();

                    return View("ContactsInfo", new LinkedInContactsInfoViewModel { emails = updatedemails });
                }
            }

            var emails = await _applicationDbContext.ContactsEmails
                        .Where(e => e.UserProfileId == user.Id)
                        .Select(e => new EmailModel
                        {
                            email = e.Email,
                            isSelected = e.IsSelected
                        })
                        .ToListAsync();

            return View("ContactsInfo", new LinkedInContactsInfoViewModel { emails = emails });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveEmail(LinkedInContactsInfoViewModel linkedInContactsInfoViewModel) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _applicationDbContext.Users.Include(u => u.ContactsEmail)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                var emailToRemove = user.ContactsEmail.FirstOrDefault(e => e.Email == linkedInContactsInfoViewModel.removeEmail);
                if (emailToRemove != null) 
                {
                    user.ContactsEmail.Remove(emailToRemove);
                    await _applicationDbContext.SaveChangesAsync();

                    var updatedemails = await _applicationDbContext.ContactsEmails
                        .Where(e => e.UserProfileId == user.Id)
                        .Select(e => new EmailModel { email = e.Email, isSelected = e.IsSelected })
                        .ToListAsync();

                    return View("ContactsInfo", new LinkedInContactsInfoViewModel { emails = updatedemails });
                }
            }

            var emails = await _applicationDbContext.ContactsEmails
                        .Where(e => e.UserProfileId == user.Id)
                        .Select(e => new EmailModel
                        {
                            email = e.Email,
                            isSelected = e.IsSelected
                        })
                        .ToListAsync();

            return View("ContactsInfo", new LinkedInContactsInfoViewModel { emails = emails});
        }


        [HttpPost]
        public async Task<IActionResult> SendMail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var emails = await _applicationDbContext.ContactsEmails
                    .Where(e => e.UserProfileId == user.Id)
                    .Select(e => new EmailModel
                    {
                        email = e.Email,
                        isSelected = e.IsSelected
                    })
                   .ToListAsync();
                if (emails != null)
                {
                    foreach (var email in emails.Where(em => em.isSelected).ToList())
                    {

                        //var token = GenerateToken();
                        var expirationDate = DateTime.UtcNow.AddDays(1);
                        var invitation = new Invitation
                        {
                            Email = email.email,
                            //Token = token,
                            ExpirationDate = expirationDate,
                            IsUsed = false
                        };
                        _applicationDbContext.Invitations.Add(invitation);
                        await _applicationDbContext.SaveChangesAsync();
                        //var invitationLink = Url.Action("Register", "Identity/Account", new { token = token }, Request.Scheme);
                        var invitationLink = "https://localhost:5001/Identity/Account/Register";
                        

                        string fromMail = _configuration["Authentication:smtp:Username"];
                        string fromPassword = _configuration["Authentication:smtp:Password"];

                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(fromMail);

                        message.Subject = "Invitation Link";
                        message.To.Add(new MailAddress(email.email));
                        message.Body = "<html><body>" + invitationLink  + "</body></html>";
                        message.IsBodyHtml = true;

                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(fromMail, fromPassword),
                            EnableSsl = true,
                        };

                        smtpClient.Send(message);
                    }
                }
                return View("ContactsInfo", new LinkedInContactsInfoViewModel { emails = emails });
            }

            return View("ContactsInfo");
        }

        [HttpGet]
        public async Task<IActionResult> LinkedInCallback(string code, string state, string error, string error_description)
        {
            if (string.IsNullOrEmpty(error))
            {
                if (string.IsNullOrEmpty(code))
                {
                    ViewData["linkedInContactsEmail"] = new List<string> { "the callback failed to send back" };
                }
                else
                {
                    ViewData["linkedInContactsEmail"] = new List<string> { "the callback sent back: " + await GetAccessToken(code) };
                }
            }
            else {
                ViewData["linkedInContactsEmail"] = new List<string> { "something went wrong " + error_description };
            }


            return View("ContactsInfo");
        }

        [HttpGet]
        public async Task<string> GetAccessToken(string code)
        {
            UriBuilder accessTokenBaseUrl = new UriBuilder("https://www.linkedin.com/oauth/v2/accessToken");
            var query = HttpUtility.ParseQueryString(accessTokenBaseUrl.Query);

            query["grant_type"] = "authorization_code";
            query["code"] = code;
            query["client_id"] = _configuration["Authentication:LinkedIn:ClientSecret"];
            query["client_secret"] = _configuration["Authentication:LinkedIn:ClientSecret"];
            query["redirect_uri"] = "https://localhost:5001/Home/LinkedInCallback";

            accessTokenBaseUrl.Query = query.ToString();

            using (var client = _clientFactory.CreateClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, accessTokenBaseUrl.ToString());
                request.Content = new StringContent(string.Empty, Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return (string)JsonNode.Parse(responseJson)["access_token"];
                }
                else
                {
                    return "no";
                }
            }
        }


        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

    }
}