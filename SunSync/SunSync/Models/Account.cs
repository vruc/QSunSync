﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace SunSync.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    class Account
    {
        [JsonProperty("access_key")]
        public string AccessKey { set; get; }
        [JsonProperty("secret_key")]
        public string SecretKey { set; get; }

        /// <summary>
        /// load account settings from local file if exists
        /// 
        /// return empty object if none
        /// </summary>
        public static Account TryLoadAccount()
        {
            Account acct = null;
            string myDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string accPath = System.IO.Path.Combine(myDocPath, "qsunbox", "account.json");
            if (File.Exists(accPath))
            {
                string accData = "";
                using (StreamReader sr = new StreamReader(accPath, Encoding.UTF8))
                {
                    accData = sr.ReadToEnd();
                }
                try
                {
                    acct = new Account();
                    acct = JsonConvert.DeserializeObject<Account>(accData);
                }
                catch (Exception ex)
                {
                    Log.Error("parse account info failed, " + ex.Message);
                }
            }
            return acct;
        }
    }
}
