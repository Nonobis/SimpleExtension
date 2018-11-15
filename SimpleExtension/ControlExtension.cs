using System;
using System.Windows.Forms;

namespace SimpleExtension
{
    /// <summary>
    /// Class ControlExtensions.
    /// </summary>
    public static class ControlExtension
    {
        /// <summary>
        /// UIs the thread invoke.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="code">The code.</param>
        public static void UiThreadInvoke(this Control control, Action code)
        {
            if (control!=null && control.InvokeRequired)
                control.Invoke(code);
            else
                code.Invoke();
        }

        /// <summary>
        /// UIs the thread invoke.
        /// </summary>
        /// <param name="usercontrol">The usercontrol.</param>
        /// <param name="code">The code.</param>
        public static void UiThreadInvoke(this UserControl usercontrol, Action code)
        {
            if (usercontrol != null && usercontrol.InvokeRequired)
                usercontrol.Invoke(code);
            else
                code.Invoke();
        }
    }
}