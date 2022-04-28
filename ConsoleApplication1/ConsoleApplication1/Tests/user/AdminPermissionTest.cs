using Userpack;
using Xunit;

public class AdminPermissionTest {

    private readonly AbsPermission permission = AdminPermission.getInstance();
    private readonly AbsPermission samePermission = AdminPermission.getInstance();

    [Fact]
   public void testPermissionSame() {
        Assert.Same(permission, samePermission);

    }
}
