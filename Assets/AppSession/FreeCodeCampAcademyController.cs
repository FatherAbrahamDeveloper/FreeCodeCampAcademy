using FreeCodeCampAcademy.Assets.AppSession.Core;
using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.Assets.AppSession;

public class FreeCodeCampAcademyController(IStartSession sessionStarter, ICacheUtil cacheUtil) : Controller
{
    private readonly IStartSession _sessionStarter = sessionStarter;
    private readonly ICacheUtil _cacheUtil = cacheUtil; 

    protected string GetSessionKey()
    {
        try
        {
            var thisVal = HttpContext.Session.GetString(StaticVals.APP_SESSION_ID) ?? "";
            return thisVal;

        }
        catch (Exception ex)
        {

            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return "";
        }

    }

    protected void SetItemCache<T>(T item, string cacheName = "") where T : class
    {
        try
        {
            var sessionid = GetSessionKey();
            if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4)
            {
                _sessionStarter.StartSession(HttpContext.Session);
                sessionid = GetSessionKey();
                if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4) return; 
            }
            cacheName = string.IsNullOrEmpty(cacheName) ? typeof (T).Name : cacheName;
            var usercode = StaticVals.APP_CACHE_NAME.Replace("{{ID}}", sessionid).Replace("{{NAME}}", cacheName);
            if (_cacheUtil.GetCache(usercode) != null)
            {
                _cacheUtil.RemoveCache(usercode);
            }
            _cacheUtil.SetCache(usercode, item, TimeSpan.FromMinutes(30));
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message );
        }
    }

    protected T GetItemCache<T>(string cacheName ="") where T: class, new()
    {
        try
        {
            var sessionid = GetSessionKey();
            if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4)
            {
                _sessionStarter.StartSession(HttpContext.Session);
                sessionid = GetSessionKey();
                if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4) return new T();
            }
            var usercode = StaticVals.APP_CACHE_NAME.Replace("{{ID}}", sessionid).Replace("{{NAME}}", cacheName);
            return _cacheUtil.GetCache(usercode) as T ?? new T();
         }

        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new T();
        }
    }

    protected void ClearItemCache(string cacheName)
    {
        try
        {
            if (string.IsNullOrEmpty(cacheName)) return;
            var sessionid = GetSessionKey();
            if (string.IsNullOrEmpty(sessionid))
            {

            }
        }
        catch (Exception)
        {

            throw;
        }
    
    
    
    }
}
