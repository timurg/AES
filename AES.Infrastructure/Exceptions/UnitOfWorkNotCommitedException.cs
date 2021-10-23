using System;

namespace AES.Infrastructure
{
    public class UnitOfWorkNotCommitedException : Exception
    {
        public UnitOfWorkNotCommitedException() : base("UnitOfWork: изменения не зафиксированы.")
        {

        }
    }
}
