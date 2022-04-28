using System.Security.Permissions;
using ikvm.extensions;
using Userpack;
using StorePack;
using Xunit;
using Moq;
using java.util.concurrent;
public class EditPolicyPermissionTest
{

    private static Mock<Store> store = new Mock<Store>();
    private static Mock<Store> differentStore = new Mock<Store>();
    private  AbsStorePermission permission = EditPolicyPermission.getInstance(store.Object);
    private  AbsStorePermission samePermission = EditPolicyPermission.getInstance(store.Object);
    private  AbsStorePermission differentPermission = EditPolicyPermission.getInstance(differentStore.Object);

    [Fact]
    
    void testPermissionSame() {
        Assert.Same(permission, samePermission);
    }

   [Fact]
    void testPermissionNotSame() {
        Assert.NotSame(permission, differentPermission);
    }
}