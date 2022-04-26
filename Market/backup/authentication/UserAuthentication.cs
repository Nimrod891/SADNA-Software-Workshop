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
using System.Security.Cryptography.RandomNumberGenerator;
using System.Security.Cryptography.RNGCryptoServiceProvider;
namespace Userpack{
public class UserAuthentication {

    record Record(byte[] salt, string hash) {
    }

    private  ConcurrentDictionary<string, Record> records;
    private MessageDigest digest; // TO DO CHECK SOMETHING ELSE
    private Random random;

    public UserAuthentication() {
        this(new ConcurrentDictionary<>(), createMessageDigest(), new Random());
    }

    private static MessageDigest createMessageDigest() {
        try {
            return MessageDigest.getInstance("SHA-256");
        } catch (NoSuchAlgorithmException e) {
            throw new RuntimeException(e);
        }
    }

    public UserAuthentication(ConcurrentDictionary<string, Record> records, MessageDigest digest, Random random) {

        this.records = records;
        this.digest = digest;
        this.random = random;
    }

    public void register(string userName, string password)  {

        var r = new {absent = true};

        ComputeIfAbsent(records,userName, (k)=>{
            r.absent = false;
            byte[] salt = new byte[16];
            random.NextBytes(salt);
            return new Record(salt, computeHash(password, salt));
        });

        if (r.absent)
            throw  SubscriberAlreadyExistsException(userName);
    }

    string computeHash(string password, byte[] salt) {
        digest.reset();
        digest.update(password.getBytes());
        digest.update(salt);
        return new string(digest.digest());
    }

    public void authenticate(string userName, string password) {
        Record record; 
        records.TryGetValue(userName,record);
        if (record == null)
            throw  SubscriberDoesNotExistException(userName);
        if (!record.hash.Equals(computeHash(password, record.salt)))
            throw  WrongPasswordException(userName, password);
    }

    private static V ComputeIfAbsent<K, V>(this Dictionary<K, V> dict, K key, Func<K, V> generator) {
    bool exists = dict.TryGetValue(key, out var value);
    if (exists) {
        return value;
    }
    var generated = generator(key);
    dict.Add(key, generated);
    return generated;
}
}
}
