﻿namespace FreeCodeCampAcademy.Assets.AppKits;

public static class FileManagerExtensionHelper
{
    public static bool IsFileSizeAllowed(this IFormFile file, int minSize, int maxSize, out string msg)
    {
		try
		{
			if (file == null)
			{
				msg = $"The file you are trying to upload is invalid";
				return false;
			}
			var fileSize = file.Length/1024;
			if (fileSize < minSize) 
			{
				msg = $"The size of the document you are trying to upload is too small and of low quality. Minimum required upload size is {minSize} kilobytes. Your document size is {fileSize} kilobytes";
				return false;
			}

			if (fileSize > maxSize)
			{
				msg = $"The size of the document you are trying to upload is too large. Maximum required upload size is {maxSize} kilobytes. Your document size is {fileSize} kilobytes";
				return false;
			}
			msg = "";
			return true;
		
		}
		catch (Exception ex) 
		{
			UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
			msg = "We are unable to complete your request due to document size error. Please try again later";
			return false;
		}
		
    }
	public static bool IsExtensionAllowed(this IFormFile file, string[] allowed, out string msg)
	{

	}
}
