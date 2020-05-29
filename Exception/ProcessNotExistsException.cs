using System;

namespace OnTopperLib.Exception
{
    class ProcessNotExistsException : SystemException
    {

        public ProcessNotExistsException()
        {
        }

        public ProcessNotExistsException(string message)
            : base(message)
        {
        }
    }
}
