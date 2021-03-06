﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;


namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IEditLockService
    {
        [OperationContract]
        void Lock(DcEditLock model);
        [OperationContract]
        void UnLock(string fingerprint);
        [OperationContract]
        void UnLockAll();
        [OperationContract]
        void UnLockByLocker(string locker);


        [OperationContract]
        DcEditLock CheckLock(string fingerprint);
    }
}
