using System;

using StorePack;
using externalService;
using policies;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Collections;
using ikvm.extensions;
using java.lang;
using java.security;
using java.util;
using java.util.concurrent;
using static System.Security.Cryptography.RandomNumberGenerator;
using static System.Security.Cryptography.RNGCryptoServiceProvider;
using Exception = java.lang.Exception;
using Random = System.Random;

namespace Userpack
{
    public class UserAuthentication
    {

        record struct Record(byte[] salt, string hash)
        {
        }

        private ConcurrentHashMap records; // k:String, v:Record
        private MessageDigest digest; // TO DO CHECK SOMETHING ELSE
        private Random random;

        public UserAuthentication()
        {
            var sr = new ConcurrentHashMap(); // string, Record
            var r = new Random();
            this.records = sr;
            this.digest = createMessageDigest();
            this.random = r;
        }

        public  UserAuthentication(ConcurrentHashMap sr, MessageDigest messageDigest, Random random1)
        {
            this.records = sr;
            this.digest = messageDigest;
            this.random = random1;        }

        private static MessageDigest createMessageDigest()
        {
            try
            {
                return MessageDigest.getInstance("SHA-256");
            }
            catch (NoSuchAlgorithmException e)
            {
                throw new RuntimeException(e);
            }
        }

        /*public UserAuthentication1(ConcurrentHashMap c, MessageDigest digest, Random random)
        {

            this.records = c;
            this.digest = digest;
            this.random = random;
        }*/

        public void register(string userName, string password)
        {

            var r = new { absent = true };

            records.putIfAbsent(userName, new Func<object, Record>(k => {
                var absent = r.absent;
                absent = false;
                var salt = new byte[16];
                random.NextBytes(salt);
                return new Record(salt, computeHash(password, salt));
            }));

            if (r.absent)
                throw new Exception("SubscriberAlreadyExistsException: "+userName);
        }

        private string computeHash(string password, byte[] salt)
        {
            digest.reset();
            digest.update(password.getBytes());
            digest.update(salt);
            var s = digest.digest();
            return s.ToString();
        }

        public void authenticate(string userName, string password)
        {
            var record = (Record)records.get(userName);
            if (record.Equals(null))
                throw new Exception("SubscriberDoesNotExistException: "  + userName);
            if (!record.hash.Equals(computeHash(password, record.salt)))
                throw new System.Exception("WrongPasswordException: user: " +userName+" , pass: " +password);
        }
        
    }
}
