namespace AuthServer.DatabaseEntity
{
    public class UserValidationModel
    {
        private readonly DigitalBooksContext _context=new DigitalBooksContext();
        public string userName { get; set; }
        public string password { get; set; }
        public bool validateCredential(string userName, string password)
        {
            if(_context.Users==null)
            {
                return false;
            }
            var encryptPass = EncryptionDecryprion.EncodePasswordToBase64(password);
            var user = (from x in _context.Users
                        where x.UserName == userName
                        //&& x.Password == encryptPass
                        select x.Password).SingleOrDefault();
            if(user!=null)
            {
                if (encryptPass == user)
                {
                    return true;
                }
            }
            
            //var user = (from x in _context.Users
            //            where x.UserName == userName && x.Password == password
            //            select x.UserId).SingleOrDefault();
            //if (user > 0)
            //{
            //    return true;
            //}
            return false;
        }

        public string GetRole(string userName)
        {
            var userRole = (from x in _context.Users
                        join r in _context.RoleMasters on x.RoleId equals r.RoleId
                        where x.UserName == userName
                        select r.RoleName).SingleOrDefault();
            return userRole;
        }
        public int GetUser(string userName)
        {
            var userID = (from x in _context.Users
                            where x.UserName == userName
                            select x.UserId).SingleOrDefault();
            return userID;
        }
        public string GetEmailID(string userName)
        {
            var emailId = (from x in _context.Users
                          where x.UserName == userName
                          select x.EmailId).SingleOrDefault();
            return emailId;
        }
    }
}
