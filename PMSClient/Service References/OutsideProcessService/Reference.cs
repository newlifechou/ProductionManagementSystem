﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMSClient.OutsideProcessService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DcOutsideProcess", Namespace="http://schemas.datacontract.org/2004/07/PMSWCFService.DataContracts")]
    [System.SerializableAttribute()]
    public partial class DcOutsideProcess : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DimensionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PMINumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PONumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProcessorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProductIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProgressBarField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Composition {
            get {
                return this.CompositionField;
            }
            set {
                if ((object.ReferenceEquals(this.CompositionField, value) != true)) {
                    this.CompositionField = value;
                    this.RaisePropertyChanged("Composition");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Creator {
            get {
                return this.CreatorField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatorField, value) != true)) {
                    this.CreatorField = value;
                    this.RaisePropertyChanged("Creator");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Customer {
            get {
                return this.CustomerField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerField, value) != true)) {
                    this.CustomerField = value;
                    this.RaisePropertyChanged("Customer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Dimension {
            get {
                return this.DimensionField;
            }
            set {
                if ((object.ReferenceEquals(this.DimensionField, value) != true)) {
                    this.DimensionField = value;
                    this.RaisePropertyChanged("Dimension");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMINumber {
            get {
                return this.PMINumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PMINumberField, value) != true)) {
                    this.PMINumberField = value;
                    this.RaisePropertyChanged("PMINumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PONumber {
            get {
                return this.PONumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PONumberField, value) != true)) {
                    this.PONumberField = value;
                    this.RaisePropertyChanged("PONumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Processor {
            get {
                return this.ProcessorField;
            }
            set {
                if ((object.ReferenceEquals(this.ProcessorField, value) != true)) {
                    this.ProcessorField = value;
                    this.RaisePropertyChanged("Processor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductID {
            get {
                return this.ProductIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ProductIDField, value) != true)) {
                    this.ProductIDField = value;
                    this.RaisePropertyChanged("ProductID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProgressBar {
            get {
                return this.ProgressBarField;
            }
            set {
                if ((object.ReferenceEquals(this.ProgressBarField, value) != true)) {
                    this.ProgressBarField = value;
                    this.RaisePropertyChanged("ProgressBar");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Remark {
            get {
                return this.RemarkField;
            }
            set {
                if ((object.ReferenceEquals(this.RemarkField, value) != true)) {
                    this.RemarkField = value;
                    this.RaisePropertyChanged("Remark");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OutsideProcessService.IOutsideProcessService")]
    public interface IOutsideProcessService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcess", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessResponse")]
        PMSClient.OutsideProcessService.DcOutsideProcess[] GetOutsideProcess(int s, int t, string productid, string composition);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcess", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessResponse")]
        System.Threading.Tasks.Task<PMSClient.OutsideProcessService.DcOutsideProcess[]> GetOutsideProcessAsync(int s, int t, string productid, string composition);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessCount", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessCountResponse")]
        int GetOutsideProcessCount(string productid, string composition);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessCount", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessCountResponse")]
        System.Threading.Tasks.Task<int> GetOutsideProcessCountAsync(string productid, string composition);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompleted", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedResponse")]
        PMSClient.OutsideProcessService.DcOutsideProcess[] GetOutsideProcessUnCompleted(int s, int t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompleted", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedResponse")]
        System.Threading.Tasks.Task<PMSClient.OutsideProcessService.DcOutsideProcess[]> GetOutsideProcessUnCompletedAsync(int s, int t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedCount", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedCountRespon" +
            "se")]
        int GetOutsideProcessUnCompletedCount();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedCount", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedCountRespon" +
            "se")]
        System.Threading.Tasks.Task<int> GetOutsideProcessUnCompletedCountAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedBack", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedBackRespons" +
            "e")]
        PMSClient.OutsideProcessService.DcOutsideProcess[] GetOutsideProcessUnCompletedBack(int s, int t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedBack", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedBackRespons" +
            "e")]
        System.Threading.Tasks.Task<PMSClient.OutsideProcessService.DcOutsideProcess[]> GetOutsideProcessUnCompletedBackAsync(int s, int t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedBackCount", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedBackCountRe" +
            "sponse")]
        int GetOutsideProcessUnCompletedBackCount();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedBackCount", ReplyAction="http://tempuri.org/IOutsideProcessService/GetOutsideProcessUnCompletedBackCountRe" +
            "sponse")]
        System.Threading.Tasks.Task<int> GetOutsideProcessUnCompletedBackCountAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/Add", ReplyAction="http://tempuri.org/IOutsideProcessService/AddResponse")]
        int Add(PMSClient.OutsideProcessService.DcOutsideProcess model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/Add", ReplyAction="http://tempuri.org/IOutsideProcessService/AddResponse")]
        System.Threading.Tasks.Task<int> AddAsync(PMSClient.OutsideProcessService.DcOutsideProcess model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/Update", ReplyAction="http://tempuri.org/IOutsideProcessService/UpdateResponse")]
        int Update(PMSClient.OutsideProcessService.DcOutsideProcess model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutsideProcessService/Update", ReplyAction="http://tempuri.org/IOutsideProcessService/UpdateResponse")]
        System.Threading.Tasks.Task<int> UpdateAsync(PMSClient.OutsideProcessService.DcOutsideProcess model);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOutsideProcessServiceChannel : PMSClient.OutsideProcessService.IOutsideProcessService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OutsideProcessServiceClient : System.ServiceModel.ClientBase<PMSClient.OutsideProcessService.IOutsideProcessService>, PMSClient.OutsideProcessService.IOutsideProcessService {
        
        public OutsideProcessServiceClient() {
        }
        
        public OutsideProcessServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OutsideProcessServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OutsideProcessServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OutsideProcessServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PMSClient.OutsideProcessService.DcOutsideProcess[] GetOutsideProcess(int s, int t, string productid, string composition) {
            return base.Channel.GetOutsideProcess(s, t, productid, composition);
        }
        
        public System.Threading.Tasks.Task<PMSClient.OutsideProcessService.DcOutsideProcess[]> GetOutsideProcessAsync(int s, int t, string productid, string composition) {
            return base.Channel.GetOutsideProcessAsync(s, t, productid, composition);
        }
        
        public int GetOutsideProcessCount(string productid, string composition) {
            return base.Channel.GetOutsideProcessCount(productid, composition);
        }
        
        public System.Threading.Tasks.Task<int> GetOutsideProcessCountAsync(string productid, string composition) {
            return base.Channel.GetOutsideProcessCountAsync(productid, composition);
        }
        
        public PMSClient.OutsideProcessService.DcOutsideProcess[] GetOutsideProcessUnCompleted(int s, int t) {
            return base.Channel.GetOutsideProcessUnCompleted(s, t);
        }
        
        public System.Threading.Tasks.Task<PMSClient.OutsideProcessService.DcOutsideProcess[]> GetOutsideProcessUnCompletedAsync(int s, int t) {
            return base.Channel.GetOutsideProcessUnCompletedAsync(s, t);
        }
        
        public int GetOutsideProcessUnCompletedCount() {
            return base.Channel.GetOutsideProcessUnCompletedCount();
        }
        
        public System.Threading.Tasks.Task<int> GetOutsideProcessUnCompletedCountAsync() {
            return base.Channel.GetOutsideProcessUnCompletedCountAsync();
        }
        
        public PMSClient.OutsideProcessService.DcOutsideProcess[] GetOutsideProcessUnCompletedBack(int s, int t) {
            return base.Channel.GetOutsideProcessUnCompletedBack(s, t);
        }
        
        public System.Threading.Tasks.Task<PMSClient.OutsideProcessService.DcOutsideProcess[]> GetOutsideProcessUnCompletedBackAsync(int s, int t) {
            return base.Channel.GetOutsideProcessUnCompletedBackAsync(s, t);
        }
        
        public int GetOutsideProcessUnCompletedBackCount() {
            return base.Channel.GetOutsideProcessUnCompletedBackCount();
        }
        
        public System.Threading.Tasks.Task<int> GetOutsideProcessUnCompletedBackCountAsync() {
            return base.Channel.GetOutsideProcessUnCompletedBackCountAsync();
        }
        
        public int Add(PMSClient.OutsideProcessService.DcOutsideProcess model) {
            return base.Channel.Add(model);
        }
        
        public System.Threading.Tasks.Task<int> AddAsync(PMSClient.OutsideProcessService.DcOutsideProcess model) {
            return base.Channel.AddAsync(model);
        }
        
        public int Update(PMSClient.OutsideProcessService.DcOutsideProcess model) {
            return base.Channel.Update(model);
        }
        
        public System.Threading.Tasks.Task<int> UpdateAsync(PMSClient.OutsideProcessService.DcOutsideProcess model) {
            return base.Channel.UpdateAsync(model);
        }
    }
}
