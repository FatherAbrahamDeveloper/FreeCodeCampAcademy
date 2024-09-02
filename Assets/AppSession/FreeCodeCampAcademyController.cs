using FreeCodeCampAcademy.Assets.AppSession.Core;
using FreeCodeCampAcademy.DTO.Account;
using FreeCodeCampAcademy.Models.RegVMs;
using Microsoft.AspNetCore.Mvc;
using XP.Kit.ResultUtils;

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
                _sessionStarter.StartSession(HttpContext.Session);
                sessionid = GetSessionKey();
                if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4) return;
            }
            var usercode = StaticVals.APP_CACHE_NAME.Replace("{{ID}}", sessionid).Replace("{{NAME}}", cacheName);
            if(_cacheUtil.GetCache(usercode) != null) 
            { 
                _cacheUtil.RemoveCache(usercode);
            }
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
        }

    }

    protected UserItem GetUserInfo(string username)
    {
        return GetUserData(username) ?? new UserItem();
    }
    private UserItem? GetUserData(string uname)
    {
        try
        {
            if (string.IsNullOrEmpty(uname) || uname.Length < 3)
            {
                return null;
            }

            uname = uname.Replace("@", "_").Replace(".", "_");
            string key = StaticVals.USER_STORE_KEY.Replace("{{username}}", uname);
            if (string.IsNullOrEmpty(key) || key.Length < 5)
            {
                return null;
            }
            var item = _cacheUtil.GetCache(key) as UserItem;
            if (item == null)
            {
                return null;
            }

            return item;
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return null;
        }
    }
    protected Result SaveRegToStore(RegistrationVM item)
    {
        try
        {
            var sessionid = GetSessionKey();
            if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4)
            {
                _sessionStarter.StartSession(HttpContext.Session);
                sessionid = GetSessionKey();

                if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4)
                    return Result.Failure([Error.Validation("RegInfo.EmptySession", "Invalid / Empty Session")]);
            }
            var cacheName = "Academy";
            var usercode = StaticVals.APP_CACHE_NAME.Replace("{{ID}}", sessionid).Replace("{{NAME}}", cacheName);
            if (_cacheUtil.GetCache(usercode) != null)
            {
                _cacheUtil.RemoveCache(usercode);
            }

            _cacheUtil.SetCache(usercode, item, TimeSpan.FromDays(7));
            return Result.Success();
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Result.Failure([Error.Validation("RegInfo.Error", "Service Error Occurred!")]);
        }
    }
    protected Result<RegistrationVM> GetRegFromStore()
    {
        try
        {
            var sessionid = GetSessionKey();
            if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4)
            {
                _sessionStarter.StartSession(HttpContext.Session);
                sessionid = GetSessionKey();
                if (string.IsNullOrEmpty(sessionid) || sessionid.Length < 4)
                    return Result.Failure<RegistrationVM>(
                        [Error.Validation("RegInfo.EmptySession", "Invalid / Empty Session")]);
            }
            var usercode = StaticVals.APP_CACHE_NAME.Replace("{{ID}}", sessionid).Replace("{{NAME}}", "Academy");
            var item = _cacheUtil.GetCache(usercode) as RegistrationVM ?? new RegistrationVM
            {
                ApplicationId = Guid.NewGuid(),
                BioData = new BioDataVM(),
                Contact = new ContactVM(),
                Education = new EducationVM(),
                IsSubmitted = false,
                OtherInfo = new OtherInfoVM(),
                PhotoPath = "",
                ProgInfo = new ProgInfoVM(),
                RegStage = Enums.RegStage.BioData
            };
            return Result.Success(item);
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Result.Failure<RegistrationVM>(
                        [Error.Validation("RegInfo.Error", "Store Error Occurred")]);
        }
    }
    protected void ClearRegStore()
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
            var usercode = StaticVals.APP_CACHE_NAME.Replace("{{ID}}", sessionid).Replace("{{NAME}}", "Academy");
            if (_cacheUtil.GetCache(usercode) != null)
            {
                _cacheUtil.RemoveCache(usercode);
            }
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }




}
