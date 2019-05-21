using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace SampleMVCApp
{
    public class Docent : AspnetUser
    {
        private int code;
        private int salarisschaal;
        private Docent manager;
    }
}