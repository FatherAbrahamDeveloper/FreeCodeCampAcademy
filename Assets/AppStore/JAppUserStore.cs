namespace FreeCodeCampAcademy;

    public class JAppUserStore
    {
        public static string AppMessage { get; private set; }

        #region AppData Helpers
        //public static PortalUserObj GetAppData(string username)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(username) || username.Length < 5)
        //        {
        //            AppMessage = "Session Has Expired! Please Re-Login";
        //            return null;
        //        }

        //        username = username.Replace("@", "_").Replace(".", "_");
        //        string sKey = GlobalSessionVars._APP_CURR_APP_DATA_KEY.Replace("{{username}}", username);
        //        if (string.IsNullOrEmpty(sKey) || sKey.Length < 5)
        //        {
        //            AppMessage = "Session Has Expired! Please Re-Login";
        //            return null;
        //        }
        //        PortalUserObj userData = CacheUtils.GetCache(sKey) as PortalUserObj ?? new PortalUserObj();
        //        return userData;
        //    }
        //    catch (Exception ex)
        //    {
        //        AppMessage = "Unknown Error! Please try again later";
        //        UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
        //        return null;
        //    }
        //}
        //public static bool SetAppData(PortalUserObj obj, string username)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(username) || username.Length < 5)
        //        {
        //            AppMessage = "Session Has Expired! Please Re-Login";
        //            return false;
        //        }

        //        username = username.Replace("@", "_").Replace(".", "_");
        //        string sKey = GlobalSessionVars._APP_CURR_APP_DATA_KEY.Replace("{{username}}", username);
        //        if (string.IsNullOrEmpty(sKey) || sKey.Length < 5)
        //        {
        //            AppMessage = "Session Has Expired! Please Re-Login";
        //            return false;
        //        }

        //        if (CacheUtils.GetCache(sKey) != null)
        //        {
        //            CacheUtils.RemoveCache(sKey);
        //        }
        //        CacheUtils.SetCache(sKey, obj, TimeSpan.FromMinutes(WebConnectSettings.AppConnect.SessionDuration));
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        AppMessage = "Unknown Error! Please try again later";
        //        UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
        //        return false;
        //    }
        //}
        //public static void ClearData(string username)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(username) || username.Length < 5)
        //        {
        //            AppMessage = "Session Has Expired! Please Re-Login";
        //            return;
        //        }

        //        username = username.Replace("@", "_").Replace(".", "_");
        //        string sKey = GlobalSessionVars._APP_CURR_APP_DATA_KEY.Replace("{{username}}", username);
        //        if (string.IsNullOrEmpty(sKey) || sKey.Length < 5)
        //        {
        //            AppMessage = "Session Has Expired! Please Re-Login";
        //            return;
        //        }

        //        if (CacheUtils.GetCache(sKey) != null)
        //        {
        //            CacheUtils.RemoveCache(sKey);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        AppMessage = "Unknown Error! Please try again later";
        //        UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
        //    }
        //}


        #endregion


    }

