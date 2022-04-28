// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using System.Diagnostics.CodeAnalysis;
// using System.Security.Permissions;
// using ikvm.extensions;
// using java.lang;
// using java.lang.reflect;
// using java.security;
// using java.util;
// using Userpack;
// using StorePack;
// using Xunit;
// using Moq;
// using java.util.concurrent;
// using java.util.concurrent.atomic;
// using ArrayList = System.Collections.ArrayList;
// using Random = System.Random;
//
// public class UserAuthenticationTest {
//
//     private Mock<UserAuthentication>auth;
//
//     private  Mock<MessageDigest> digest =new Mock<MessageDigest>(MessageDigest.getInstance("SHA-256"));
//
//      private Mock<ConcurrentHashMap> records; //<String, UserAuthentication.Record>
//
//     private  string userName = "Jones";
//     private  string password = "jones12345";
//     private  Random random = new Random();
//     
//
//     [Fact]
//     void setUp() {
//
//         auth =new Mock<UserAuthentication>(new UserAuthentication(records.Object, digest.Object, random));
//     }
//
//     [Fact]
//     void register()  {
//
//         AtomicReference re = new AtomicReference(); //byte[]
//
//         doansw(invocation -> {
//             byte[] result = (byte[])invocation.callRealMethod();
//             ref.set(result);
//             return result;
//         }).when(digest).digest();
//
//         auth.register(userName, password);
//         verify(digest).reset();
//         String hash = records.get(userName).hash();
//         assertEquals(new String(ref.get()), hash);
//         
//     }
//     
//     [Fact]
//     void register_existingUser() {
//
//         doReturn(null).when(records).computeIfAbsent(same(userName), any());
//         assertThrows(SubscriberAlreadyExistsException.class, () -> auth.register(userName, password));
//     }
//
//     [Fact]
//     void authenticate()  {
//
//         byte[] salt = "12345".getBytes();
//         String hash = "SomeHash";
//         UserAuthentication.Record record = new UserAuthentication.Record(salt, hash);
//         when(records.get(userName)).thenReturn(record);
//         when(digest.digest()).thenReturn(hash.getBytes());
//         auth.authenticate(userName, password);
//     }
//
//     [Fact]
//     void authenticate_subscriberDoesNotExist() {
//
//         assertThrows(SubscriberDoesNotExistException.class, () -> auth.authenticate(userName, password));
//     }
//
//     [Fact]
//     void authenticate_wrongPassword() {
//
//         byte[] salt = "12345".getBytes();
//         UserAuthentication.Record record = new UserAuthentication.Record(salt, "SomeHash");
//         when(records.get(userName)).thenReturn(record);
//         when(digest.digest()).thenReturn("SomeOtherHash".getBytes());
//         assertThrows(WrongPasswordException.class, () -> auth.authenticate(userName, password));
//     }
// }