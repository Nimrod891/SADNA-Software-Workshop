
using WeakReference = System.WeakReference;

namespace ConsoleApplication1.Permmissions
{
    public class AdminPermission : AbsPermission
    {
        public AdminPermission() 
        { }
        

        public string toString()
        {
            return "AdminPermission";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}