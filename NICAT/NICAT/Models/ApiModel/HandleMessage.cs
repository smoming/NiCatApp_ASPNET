using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NICAT.Models.ApiModel
{
    public class HandleMessage
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }

        public HandleMessage(bool success, string msg)
        {
            isSuccess = success;
            Message = msg;
        }

        public static HandleMessage CreateFail(string msg)
        {
            return new HandleMessage(false, msg);
        }

        public static HandleMessage CreateSuccess(string msg)
        {
            return new HandleMessage(true, msg);
        }

        public static HandleMessage Create(bool success, string successmsg, string failmessage)
        {
            return new HandleMessage(success, success ? successmsg : failmessage);
        }
    }
}