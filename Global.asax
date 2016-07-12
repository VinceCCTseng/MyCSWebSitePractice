<%@ Application Language="C#" %>
<%@ Import Namespace="MyWebSitePractice1" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        //add
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery.ui.combined", new ScriptResourceDefinition
        {
        Path = "~/Scripts/jquery-ui-1.10.4.custom.min.js", 
        DebugPath = "~/Scripts/jquery-ui-1.10.4.custom.js", 
        CdnPath = "http://ajax.aspnetcdn.com/ajax/jquery.ui/1.10.4/jquery-ui.min.js", 
        CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jquery.ui/1.10.4/jquery-ui.js", 
        CdnSupportsSecureConnection = true
        });
    }

</script>
