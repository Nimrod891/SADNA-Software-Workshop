using ConsoleApplication1.Permmissions;
using ikvm.extensions;
using Userpack;
using StorePack;
using Xunit;
using Moq;

public class AppointerPermissionTest
{
    
    private static readonly Mock<Store> store = new Mock<Store>();
    private static readonly Mock<Store> differentStore = new Mock<Store>();
    private static readonly Mock<Subscriber> target = new Mock<Subscriber>();
    private static readonly Mock<Subscriber> differentTarget = new Mock<Subscriber>();

    private  AbsStorePermission permission = AppointerPermission.getInstance(target.Object, store.Object);
    private  AbsStorePermission samePermission = AppointerPermission.getInstance(target.Object, store.Object);
    private  AbsStorePermission differentTargetPermission = AppointerPermission.getInstance(differentTarget.Object, store.Object);
    private  AbsStorePermission differentStorePermission = AppointerPermission.getInstance(target.Object, differentStore.Object);
    private  AbsStorePermission differentAllPermission = AppointerPermission.getInstance(differentTarget.Object, differentStore.Object);

   [Fact]
    void testPermissionSame() {
        Assert.NotSame(permission, samePermission);
    }

    [Fact]
    void testPermissionDifferentTarget() {
        Assert.NotSame(permission, differentTargetPermission);
    }

    [Fact]
    void testPermissionDifferentStore() {
        Assert.NotSame(permission, differentStorePermission);
    }

    [Fact]
    void testPermissionDifferentAll() {
        Assert.NotSame(permission, differentAllPermission);
    }
}