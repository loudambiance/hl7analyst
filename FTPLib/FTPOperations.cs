/***************************************************************
* Copyright (C) 2011 Jeremy Reagan, All Rights Reserved.
* I may be reached via email at: jeremy.reagan@live.com
* 
* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; under version 2
* of the License.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
****************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace FTPLib
{
    /// <summary>
    /// Provides functions for FTP Operations: Upload, Download, and List.
    /// </summary>
    public class FTPOperations
    {
        /// <summary>
        /// Uploads a file to the specified FTP site.
        /// </summary>
        /// <param name="Options">The FTP Options to use</param>
        /// <param name="fileContents">The contents of the file to upload</param>
        /// <param name="RemotePath">The remote path to upload file to</param>
        /// <param name="fileIndex">The index of the file to upload</param>
        /// <returns>The file name after upload</returns>
        public static string Send(FTPOptions Options, string fileContents, string RemotePath, int fileIndex)
        {
            byte[] contents = Encoding.UTF8.GetBytes(fileContents);
            string returnFName = String.Format("HL7Analyst{0}{1}.hl7", DateTime.Now.ToString("MMddyyyyHHmmss"), fileIndex);
            FtpWebRequest request = SetupRequest(Options, RemotePath + "/" + returnFName);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.ContentLength = contents.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(contents, 0, contents.Length);
            requestStream.Close();
            return returnFName;
        }
        /// <summary>
        /// Downloads the selected file from the FTP site
        /// </summary>
        /// <param name="Options">The FTP Options to use</param>
        /// <param name="RemotePath">The remove file path to download</param>
        /// <returns>The file contents after download</returns>
        public static string Get(FTPOptions Options, string RemotePath)
        {
            FtpWebRequest request = SetupRequest(Options, RemotePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream);
            string s = sr.ReadToEnd();
            sr.Close();
            response.Close();
            return s;
        }
        /// <summary>
        /// Lists the available files in an FTP Remote path
        /// </summary>
        /// <param name="Options">The FTP Options to use</param>
        /// <param name="RemotePath">The remote path to list files for</param>
        /// <param name="Extensions">The file extension list</param>
        /// <returns>The list of files to display</returns>
        public static List<string> ListFiles(FTPOptions Options, string RemotePath, List<string> Extensions)
        {
            List<string> returnList = new List<string>();
            FtpWebRequest request = SetupRequest(Options, RemotePath);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream);
            string line = sr.ReadLine();

            while (line != null)
            {
                if (line.Contains("."))
                {
                    foreach (string ext in Extensions)
                    {
                        if (line.Contains("." + ext))
                        {
                            returnList.Add(line);
                            break;
                        }
                    }
                }
                line = sr.ReadLine();
            }
            sr.Close();
            response.Close();            
            return returnList;
        }
        /// <summary>
        /// Lists the directories in the remote FTP path
        /// </summary>
        /// <param name="Options">The FTP Options to use</param>
        /// <param name="RemotePath">The Remote path to check</param>
        /// <returns>The list of directories returned.</returns>
        public static List<string> ListDirs(FTPOptions Options, string RemotePath)
        {
            List<string> returnList = new List<string>();
            FtpWebRequest request = SetupRequest(Options, RemotePath);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream);
            string line = sr.ReadLine();

            while (line != null)
            {
                if (!line.Contains("."))
                    returnList.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            response.Close();            
            return returnList;
        }
        /// <summary>
        /// Sets up the FTPWebRequest object to be used
        /// </summary>
        /// <param name="Options">The FTP Options to use</param>
        /// <param name="RemotePath">The remote path to use</param>
        /// <returns>The FTP Web Request to be used</returns>
        private static FtpWebRequest SetupRequest(FTPOptions Options, string RemotePath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemotePath);
            request.UsePassive = Options.UsePassive;
            if (Options.AnonymousLogin)
                request.Credentials = new NetworkCredential("Anonymous", "");
            else
                request.Credentials = new NetworkCredential(Options.UserName, Options.UserPassword);
            if (Options.UseSSL)
                request.EnableSsl = true;
            else
                request.EnableSsl = false;
            return request;
        }
    }
}
