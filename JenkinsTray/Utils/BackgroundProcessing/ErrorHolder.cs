using System;
using System.Collections.Generic;
using System.Text;

namespace Jenkins.Tray.Utils.BackgroundProcessing
{
    class ErrorHolder
    {
        private Exception error;

        public Exception Error
        {
            get { return error; }
            set { error = value; }
        }
    }
}
