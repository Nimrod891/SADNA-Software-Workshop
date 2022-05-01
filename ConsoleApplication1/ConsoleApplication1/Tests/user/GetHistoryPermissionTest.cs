using System.Security.Permissions;
using ConsoleApplication1.Permmissions;
using ikvm.extensions;
using Userpack;
using StorePack;
using Xunit;
using Moq;
using java.util.concurrent;
public class GetHistoryPermissionTest
{

    private static Mock<Store> store = new Mock<Store>();
    private static Mock<Store> differentStore = new Mock<Store>();
    private  AbsStorePermission permission = GetHistoryPermission.getInstance(store.Object);
    private  AbsStorePermission samePermission = GetHistoryPermission.getInstance(store.Object);
    private  AbsStorePermission differentPermission = GetHistoryPermission.getInstance(differentStore.Object);

    [Fact]
    
    void testPermissionSame() {
        Assert.Same(permission, samePermission);
    }

    [Fact]
    void testPermissionNotSame() {
        Assert.NotSame(permission, differentPermission);
    }
}