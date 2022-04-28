using System.Security.Permissions;
using ikvm.extensions;
using Userpack;
using StorePack;
using Xunit;
using Moq;
using java.util.concurrent;
public class ManageInventoryPermissionTest
{

    private static Mock<Store> store = new Mock<Store>();
    private static Mock<Store> differentStore = new Mock<Store>();
    private  AbsStorePermission permission = ManagerPermission.getInstance(store.Object);
    private  AbsStorePermission samePermission = ManagerPermission.getInstance(store.Object);
    private  AbsStorePermission differentPermission = ManagerPermission.getInstance(differentStore.Object);

    [Fact]
    
    void testPermissionSame() {
        Assert.Same(permission, samePermission);
    }

    [Fact]
    void testPermissionNotSame() {
        Assert.NotSame(permission, differentPermission);
    }
}