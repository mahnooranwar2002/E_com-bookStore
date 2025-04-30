
using Bookstore.Migrations;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private mycontext _con;

        public HomeController(ILogger<HomeController> logger, mycontext con)
        {
            _con = con;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login !=null)
            {
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
                mainmodel mydata = new mainmodel()
                {
                   
                    admin_data = admin_details
                };
               
                TempData["registered"] = "login";
                return View(mydata);
            }
            else
            {
                TempData["not_registered"] = "not_login";
            }
            return View();
        }
        public IActionResult about()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
                mainmodel mydata = new mainmodel()
                {

                    admin_data = admin_details
                };
                TempData["registered"] = "login";
                return View(mydata);
            }
            else
            {
                TempData["not_registered"] = "not_login";
            }
            return View();
        }
        public IActionResult Privacy()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
                mainmodel mydata = new mainmodel()
                {

                    admin_data = admin_details
                };
                TempData["registered"] = "login";
                return View(mydata);
            }
            else
            {
                TempData["not_registered"] = "not_login";
            }
            return View();
        }
      
        public IActionResult blog()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
                mainmodel mydata = new mainmodel()
                {

                    admin_data = admin_details
                };
                TempData["registered"] = "login";
                return View(mydata);
            }
            else
            {
                TempData["not_registered"] = "not_login";
            }
            return View();
        }

        public IActionResult contact()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
                mainmodel mydata = new mainmodel()
                {

                    admin_data = admin_details
                };
                TempData["registered"] = "login";
                return View(mydata);
            }
            else
            {
                TempData["not_registered"] = "not_login";
            }
            return View();
        }
        [HttpPost]
        public IActionResult contact(contact con)
        {
            _con.tbl_contact.Add(con);
            _con.SaveChanges();
            TempData["msg"] = "Your Thoughts Are Important , Share Them with Us!";
            return RedirectToAction("contact");


        }
        public IActionResult login_form()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login_form(string email, string password)
        {
            var data = _con.tbl_users.FirstOrDefault(e => e.Email == email);

            if (data != null && data.Password == password)
            {
                if (data.status == 1) // Active User
                {
                    if (data.is_admin == 1) // Admin User
                    {
                        HttpContext.Session.SetString("admin_session", data.Id.ToString());
                        return RedirectToAction("Index", "Admin");
                    }
                    else // Regular User
                    {
                        HttpContext.Session.SetString("user_session", data.Id.ToString());
                        return RedirectToAction("Index"); // Redirect to user dashboard or relevant page
                    }
                }
               
                else // Handle unexpected status values
                {
                    return RedirectToAction("WaitedPage"); // Optionally, redirect to an error page
                }
            }
            else
            {
                // Invalid login attempt (email not found or incorrect password)
                TempData["msg"] = "Invalid email or password.";
                return View("login_form");
            }
        }
        
        public IActionResult register_form()
        {
            return View();
        }
        [HttpPost]

        public IActionResult register_form(User myuser)
        {
         
           var emailExisting=_con.tbl_users.FirstOrDefault(e=>e.Email==myuser.Email);
            if (emailExisting != null)
            {
                TempData["MSG"] = "This email is already registered!";
                return RedirectToAction("register_form");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    myuser.status = 1;
                    myuser.is_admin = 0;
                    _con.tbl_users.Add(myuser);
                    _con.SaveChanges();
                    TempData["msg"] = "your account is created please login!";
                    return RedirectToAction("register_form");

                }
                else
                {

                    return RedirectToAction("register_form");
                }
                
            }
           
        }
       
   

        public IActionResult regs()
        {
            return View();
        }

        public IActionResult acb(User _user)
        {
            string pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            Random rd = new Random();
            char[] mypass = new char[5];
            for (int i = 0; i < 5; i++)
            {
                mypass[i] = pass[(int)(35 * rd.NextDouble())];
            }
            string mypassString = new string(mypass);

            // Store the string in TempData
            //TempData["abc"] = mypassString;

            _user.status = 1;
            _user.is_admin = 0;
            _user.Password = mypassString;

          //  MailMessage mailmassage = new MailMessage();
          //  mailmassage.Body = _user.Email;
          //mailmassage.From= new MailAddress("mahnooranwat191@gmail.com");
          //  mailmassage.IsBodyHtml = false;
          //  mailmassage.Body = "Your account has been created. Your temporary password is: " + _user.Password;
          //  mailmassage.To.Add(new MailAddress(_user.Email));


          

            // ... other code ...

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("mahnnooranwar191@gmail.com");
            mailMessage.To.Add(new MailAddress(_user.Email)); // Corrected To address
            mailMessage.Subject = "Your Account Details";
            mailMessage.Body = "Your account has been created. Your temporary password is: " + mypassString;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mahnnooranwar191@gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("mahnnooranwar191@gmail.com", "wintersnow1438130");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Hello   Thanks for Register at Hariti Study Hub";
            msg.Body = "Hi, Thanks For Your Registration at Hariti Study Hub, We will Provide You The Latest Update. Thanks";
            string toaddress = _user.Email;
            msg.To.Add(toaddress);
            string fromaddress = "Hariti Study Hub <mahnnooranwar191@gmail.com>";
            msg.From = new MailAddress(fromaddress);
            try
            {
                smtp.Send(msg);
              

            }
            catch
            {
                throw;
            }

            // ... rest of your code ...

            //catch (Exception ex)
            //{
            //    // Handle any exceptions that occur during sending
            //    // You can log the exception or show a message to the user

            //    // 1. Log the full exception details (VERY IMPORTANT)
            //    System.Diagnostics.Debug.WriteLine(ex.ToString()); // Or use your logging framework

            //    // 2. Throw a more informative exception (optional, but good practice)
            //    throw new InvalidOperationException("Error sending email: " + ex.Message, ex);


            //}
            //string pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            //Random rd = new Random();
            //char[] mypass = new char[5];
            //for (int i = 0; i < 5; i++)
            //{
            //    mypass[i] = pass[(int)(35 * rd.NextDouble())];
            //}
            //string mypassString = new string(mypass);

            // Store the string in TempData
            //TempData["abc"] = mypassString;

            _user.status = 1;
            _user.is_admin = 0;
            _user.Password = mypassString;

            _con.tbl_users.Add(_user);
            _con.SaveChanges();
            TempData["abc"] = "your account is created please login!";
            return RedirectToAction("regs");
        }
        public IActionResult logout()
        {
            HttpContext.Session.Remove("user_session");
            return RedirectToAction("index");
        }
        public IActionResult profile(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var data_admin = _con.tbl_users.Find(id);
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
                mainmodel mydata = new mainmodel()
                {
                    admin_profiles = data_admin,
                    admin_data = admin_details
                };
                TempData["mgs"] = "admin";
                return View(mydata);


            }
            else
            {
                return RedirectToAction("login_form", "home");

            }



        }
        [HttpPost]
        public IActionResult profile(User user)
        {
            _con.tbl_users.Update(user);
            _con.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult books_details(string search_text, string mydatass)
        {
            var login = HttpContext.Session.GetString("user_session");
            var authors = _con.tbl_authors.ToList();
            var category = _con.tbl_genres.ToList();
        
            List<Book> book_data = new List<Book>();
            if(search_text == null)
            {
                book_data = _con.tbl_books.ToList();
            }
            else
            {
               
                book_data = _con.tbl_books.FromSqlInterpolated($"select * from tbl_books where name like '%'+{search_text}+'%'").ToList();
            }
            if (book_data.Count == 0)
            {
                TempData["msg"] = $"we didn't have this book yet {search_text}";
            }

            if (login != null)
            {
               
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
                mainmodel mydata = new mainmodel()
                {
                    auth_details=authors,
                    genres_details=category,
                   book_details=book_data,
                    admin_data = admin_details
                };
                TempData["registered"] = "login";
                return View(mydata);
            }
            else
            {
                mainmodel mydats = new mainmodel()
                {
                    auth_details = authors,
                    genres_details = category,
                    book_details = book_data,

                };
                TempData["not_registered"] = "not_login";
                return View(mydats);
            
               
            }
           
        }
        public IActionResult book_data(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var purchase_data = _con.tbl_books.Find(id);
               var gnres = _con.tbl_genres.Where(e => e.id == purchase_data.genres_id).ToList();
                var auth = _con.tbl_authors.Where(e => e.id == purchase_data.auth_id).ToList();
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
                mainmodel mydata = new mainmodel()
                {
                   auth_details=auth,
                    genres_details=gnres,
                    book_data = purchase_data,
                    admin_data = admin_details
                };
                TempData["registered"] = "login";
                return View(mydata);


            }
            else
            {
                return RedirectToAction("login_form", "home");

            }
        }
        [HttpPost]
      public IActionResult add_wishlist(int p_id,wishlist wish)
        {
            var product = _con.tbl_books.Find(p_id);
            var login = HttpContext.Session.GetString("user_session");
            var p_o = _con.tbl_wishlist.FirstOrDefault(p => p.product_id == product.id);
           
           
            if (login != null && p_o ==null)
            {
                wish.product_name = product.name;
                wish.product_id=product.id;
                wish.user_id = int.Parse(login);
                _con.tbl_wishlist.Add(wish);
                _con.SaveChanges();
                TempData["msg2"] = "book has been added in  wishlist!";

                return RedirectToAction("books_details");
            }
            if(p_o != null)
            {
                TempData["msg1"] = "book already  added in  wishlist!";

                return RedirectToAction("books_details");
            }

            else
            {
                TempData["msg1"] = "please login first  to add the book in your wishlist!";
                return RedirectToAction("books_details");
            }
        }
        [HttpPost]
        public IActionResult add_cart(int pt_id,cart book_pro)
        {
            var login = HttpContext.Session.GetString("user_session");
            var findid = _con.tbl_books.Find(pt_id);

            var p = _con.tbl_books.FirstOrDefault(e=>e.id==book_pro.product_id);
            var cart_data = _con.tbl_cart.FirstOrDefault(p => p.product_id == findid.id);
            if (cart_data == null || login!=null)
            {
            if (book_pro.qunatity <= findid.qunatiy)
            {
             book_pro.user_id=int.Parse(login);
                book_pro.product_id = findid.id;
                book_pro.price = findid.price;
                book_pro.total = book_pro.price * book_pro.qunatity;
                findid.qunatiy = findid.qunatiy - book_pro.qunatity;
                findid.total = findid.price * findid.qunatiy;
                _con.tbl_cart.Add(book_pro);

                _con.SaveChanges();
                TempData["msg1"] = "book has been added in your cart";
                return RedirectToAction("books_details");
            }
            else
            {
                TempData["msg1"] = "sorry we do not have much quantity of" +" "+ book_pro.qunatity;
                return RedirectToAction("books_details");
            }
            }
            else
            {
                TempData["msg1"] = "This Book is already add";
                return RedirectToAction("books_details");
            }
        }
        public IActionResult carts_views()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();

                var cart_count = _con.tbl_cart.Include(p => p.book_data).Where(i => i.user_id == int.Parse(login));
               
                if (cart_count == null)
                {
                    TempData["mydata"] = "your cart is empty";
                    mainmodel mydata = new mainmodel()
                    {

                        admin_data = admin_details
                    };
                    TempData["registered"] = "login";
                    return View(mydata);
                }
                else
                {
                    var cart_data=_con.tbl_cart.Include(p=>p.book_data).Where(p=>p.user_id==int.Parse(login)).ToList();
                    var count = _con.tbl_cart.Where(p => p.user_id == int.Parse(login)).Count();
                    if (count == 0)
                    {
                        TempData["mydata"] = "your cart is empty";
                    }
                    else
                    {
                        TempData["cart_count"] = count;
                    }
                   
                    var income = _con.tbl_cart.Where(p => p.user_id == int.Parse(login)).Sum(p => p.total);
                    TempData["income"] = income;
                    mainmodel mydata = new mainmodel()
                    {cart_details=cart_data,

                        admin_data = admin_details
                    };
                    TempData["registered"] = "login";
                    return View(mydata);
                }
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
       
        public IActionResult wishlist_view()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();

                var cart_count = _con.tbl_wishlist.Include(p => p.book_data).Where(i => i.user_id == int.Parse(login));

                if (cart_count == null)
                {
                    TempData["mydata"] = "your wishlist is empty";
                    mainmodel mydata = new mainmodel()
                    {

                        admin_data = admin_details
                    };
                    TempData["registered"] = "login";
                    return View(mydata);
                }
                else
                {
                    var cart_data = _con.tbl_wishlist.Include(p => p.book_data).Where(p => p.user_id == int.Parse(login)).ToList();
                    var count = _con.tbl_wishlist.Where(p => p.user_id == int.Parse(login)).Count();
                    if (count == 0)
                    {
                        TempData["mydata"] = "your cart is empty";
                    }
                    else
                    {
                        TempData["cart_count"] = count;
                    }

                    var income = _con.tbl_cart.Where(p => p.user_id == int.Parse(login)).Sum(p => p.total);
                    TempData["income"] = income;
                    mainmodel mydata = new mainmodel()
                    {
                        wishlist_detail=cart_data,

                        admin_data = admin_details
                    };
                    TempData["registered"] = "login";
                    return View(mydata);
                }
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        public IActionResult remove_cartProduct(int id)
        {
            var del= _con.tbl_cart.Find(id);
            var quantity = _con.tbl_books.FirstOrDefault(i => i.id == del.product_id);
            quantity.qunatiy = quantity.qunatiy + del.qunatity;
            quantity.total = quantity.price * quantity.qunatiy;
            _con.tbl_cart.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("carts_views");
        }
        public IActionResult remove_productWishlist(int id)
        {
            var del = _con.tbl_wishlist.Find(id);
            _con.tbl_wishlist.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("wishlist_view");
        }
        public IActionResult paymentGetway()
        {
            var login = HttpContext.Session.GetString("user_session");
            var count = _con.tbl_cart.Where(p => p.user_id == int.Parse(login)).Count();
            var income = _con.tbl_cart.Where(p => p.user_id == int.Parse(login)).Sum(p => p.total);
            TempData["income"] = income;
            TempData["cart_count"] = count; 
            var products = _con.tbl_cart.Include(p=>p.book_data).Where(e => e.user_id == int.Parse(login)).ToList();
            var user_data = _con.tbl_users.Find(int.Parse(login));
            mainmodel mydata = new mainmodel()
            {    cart_details=products,
                admin_profiles=user_data,
            };
           
            return View(mydata);
     
        }
        [HttpPost]
        public IActionResult paymentGetway(order cus_order)
        {
            var login = HttpContext.Session.GetString("user_session");
            var count = _con.tbl_cart.Where(p => p.user_id == int.Parse(login)).Count();
            var products = _con.tbl_cart.Where(e => e.user_id == int.Parse(login));
            var income = _con.tbl_cart.Where(p => p.user_id == int.Parse(login)).Sum(p => p.total);
            var productss = _con.tbl_cart.Include(p => p.book_data).Where(e => e.user_id == int.Parse(login)).ToList();
            cus_order.product_name = string.Join(", ", productss.Select(p => p.book_data.name));
            cus_order.product_price = string.Join(",", productss.Select(p => p.price));
            cus_order.product_quantity = string.Join(",", productss.Select(p => p.qunatity));
            cus_order.status = 1;
            Random random = new Random();
            var days = random.Next(00, 11).ToString();
            cus_order.day_counts = days;
            cus_order.sum = income.ToString();
            cus_order.product_count = count.ToString();
            _con.tbl_order.Add(cus_order);
            _con.SaveChanges();
            TempData["msg"] = "your delivery is delivered soon in" +" "+ " " + cus_order.day_counts + " "+ " " + "days";
            return RedirectToAction("paymentGetway");

        }
        public IActionResult order_views(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            var order_data = _con.tbl_order.Where(o => o.user_ids == int.Parse(login)).ToList();
            if (order_data.Count == 0)
            {
                TempData["product"] = "you didn't ordered something yet";
            }
            var admin_details = _con.tbl_users.Where(e => e.Id == int.Parse(login)).ToList();
            mainmodel mydata = new mainmodel()
            { 
                orders_Details=order_data,

                admin_data = admin_details
            };
            TempData["registered"] = "login";
            return View(mydata);

        }
        public IActionResult mypage()
        {
            return View();
        }
    }
}
    

