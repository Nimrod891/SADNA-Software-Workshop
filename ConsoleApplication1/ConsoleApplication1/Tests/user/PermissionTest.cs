using System.Security.Permissions;
using ConsoleApplication1.Permmissions;
using ikvm.extensions;
using Userpack;
using StorePack;
using Xunit;
using Moq;
using java.util.concurrent;
public class PermissionTest
{

    private static Mock<Store> store = new Mock<Store>();
    private  AbsStorePermission permission = ManagerPermission.getInstance(store.Object);
    private  AbsStorePermission differentClassPermission = OwnerPermission.getInstance(store.Object);

    [Fact]
    
    void testPermissionNotSame() {
        Assert.Equal(permission, differentClassPermission);
    }
}