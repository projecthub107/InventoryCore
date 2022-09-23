<%@ Application Language="C#" %>
<%@ Import Namespace="System.Security.Principal" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        // Extract the forms authentication cookie
        string cookieName = FormsAuthentication.FormsCookieName;
        HttpCookie authCookie = Context.Request.Cookies[cookieName];

        if (null == authCookie)
        {
            // There is no authentication cookie.
            return;
        }

        FormsAuthenticationTicket authTicket = null;
        try
        {
            authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        }
        catch //(Exception ex)
        {
            // Log exception details (omitted for simplicity)
            return;
        }

        if (null == authTicket)
        {
            // Cookie failed to decrypt.
            return;
        }

        // When the ticket was created, the UserData property was assigned a
        // pipe delimited string of role names.
        string[] roles = authTicket.UserData.Split(new char[] { '|' });


        // Create an Identity object
        FormsIdentity id = new FormsIdentity(authTicket);


        GenericPrincipal principal = new GenericPrincipal(id, roles);
        Context.User = principal;


    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {


        //string strRequestedURLWithQueryString = ((System.Web.HttpApplication)(sender)).Request.Url.AbsoluteUri;

        //if (!strRequestedURLWithQueryString.ToLower().Contains(".jpg") && 
        //    !strRequestedURLWithQueryString.ToLower().Contains(".png") && 
        //    !strRequestedURLWithQueryString.ToLower().Contains(".css") && 
        //    !strRequestedURLWithQueryString.ToLower().Contains(".jpeg") && 
        //    !strRequestedURLWithQueryString.ToLower().Contains(".gif") && 
        //    !strRequestedURLWithQueryString.ToLower().Contains(".js"))
        //{
        //    if (strRequestedURLWithQueryString.ToLower().IndexOf("commonscript") == -1)
        //    {
        //        Server.Transfer("~/error.aspx?RequestedURL=" + Server.UrlEncode(strRequestedURLWithQueryString));
        //    }
        //}
       
       // Server.Transfer("~/error.aspx?RequestedURL=" + Server.UrlEncode(strRequestedURLWithQueryString));
        
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
	

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
   
       
</script>
