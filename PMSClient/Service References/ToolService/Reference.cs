﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMSClient.ToolService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DcToolSieve", Namespace="http://schemas.datacontract.org/2004/07/PMSWCFService.DataContracts")]
    [System.SerializableAttribute()]
    public partial class DcToolSieve : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BoxNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ManufactureField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MaterialGroupField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SearchIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SpecificationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StopTimeField;
        
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
        public string BoxNumber {
            get {
                return this.BoxNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.BoxNumberField, value) != true)) {
                    this.BoxNumberField = value;
                    this.RaisePropertyChanged("BoxNumber");
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
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Manufacture {
            get {
                return this.ManufactureField;
            }
            set {
                if ((object.ReferenceEquals(this.ManufactureField, value) != true)) {
                    this.ManufactureField = value;
                    this.RaisePropertyChanged("Manufacture");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MaterialGroup {
            get {
                return this.MaterialGroupField;
            }
            set {
                if ((object.ReferenceEquals(this.MaterialGroupField, value) != true)) {
                    this.MaterialGroupField = value;
                    this.RaisePropertyChanged("MaterialGroup");
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
        public string SearchID {
            get {
                return this.SearchIDField;
            }
            set {
                if ((object.ReferenceEquals(this.SearchIDField, value) != true)) {
                    this.SearchIDField = value;
                    this.RaisePropertyChanged("SearchID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Specification {
            get {
                return this.SpecificationField;
            }
            set {
                if ((object.ReferenceEquals(this.SpecificationField, value) != true)) {
                    this.SpecificationField = value;
                    this.RaisePropertyChanged("Specification");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime StartTime {
            get {
                return this.StartTimeField;
            }
            set {
                if ((this.StartTimeField.Equals(value) != true)) {
                    this.StartTimeField = value;
                    this.RaisePropertyChanged("StartTime");
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime StopTime {
            get {
                return this.StopTimeField;
            }
            set {
                if ((this.StopTimeField.Equals(value) != true)) {
                    this.StopTimeField = value;
                    this.RaisePropertyChanged("StopTime");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ToolService.IToolSieveService")]
    public interface IToolSieveService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetToolSieve", ReplyAction="http://tempuri.org/IToolSieveService/GetToolSieveResponse")]
        PMSClient.ToolService.DcToolSieve[] GetToolSieve(string searchid, string materialGroup, int s, int t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetToolSieve", ReplyAction="http://tempuri.org/IToolSieveService/GetToolSieveResponse")]
        System.Threading.Tasks.Task<PMSClient.ToolService.DcToolSieve[]> GetToolSieveAsync(string searchid, string materialGroup, int s, int t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetToolSieveCount", ReplyAction="http://tempuri.org/IToolSieveService/GetToolSieveCountResponse")]
        int GetToolSieveCount(string searchid, string materialGroup);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetToolSieveCount", ReplyAction="http://tempuri.org/IToolSieveService/GetToolSieveCountResponse")]
        System.Threading.Tasks.Task<int> GetToolSieveCountAsync(string searchid, string materialGroup);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetToolSieveUsed", ReplyAction="http://tempuri.org/IToolSieveService/GetToolSieveUsedResponse")]
        PMSClient.ToolService.DcToolSieve[] GetToolSieveUsed(string searchid, string materialGroup, int s, int t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetToolSieveUsed", ReplyAction="http://tempuri.org/IToolSieveService/GetToolSieveUsedResponse")]
        System.Threading.Tasks.Task<PMSClient.ToolService.DcToolSieve[]> GetToolSieveUsedAsync(string searchid, string materialGroup, int s, int t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetToolSieveUsedCount", ReplyAction="http://tempuri.org/IToolSieveService/GetToolSieveUsedCountResponse")]
        int GetToolSieveUsedCount(string searchid, string materialGroup);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetToolSieveUsedCount", ReplyAction="http://tempuri.org/IToolSieveService/GetToolSieveUsedCountResponse")]
        System.Threading.Tasks.Task<int> GetToolSieveUsedCountAsync(string searchid, string materialGroup);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/AddToolSieve", ReplyAction="http://tempuri.org/IToolSieveService/AddToolSieveResponse")]
        void AddToolSieve(PMSClient.ToolService.DcToolSieve model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/AddToolSieve", ReplyAction="http://tempuri.org/IToolSieveService/AddToolSieveResponse")]
        System.Threading.Tasks.Task AddToolSieveAsync(PMSClient.ToolService.DcToolSieve model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/UpdateToolSieve", ReplyAction="http://tempuri.org/IToolSieveService/UpdateToolSieveResponse")]
        void UpdateToolSieve(PMSClient.ToolService.DcToolSieve model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/UpdateToolSieve", ReplyAction="http://tempuri.org/IToolSieveService/UpdateToolSieveResponse")]
        System.Threading.Tasks.Task UpdateToolSieveAsync(PMSClient.ToolService.DcToolSieve model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/CheckToolSieveExist", ReplyAction="http://tempuri.org/IToolSieveService/CheckToolSieveExistResponse")]
        int CheckToolSieveExist(string searchid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/CheckToolSieveExist", ReplyAction="http://tempuri.org/IToolSieveService/CheckToolSieveExistResponse")]
        System.Threading.Tasks.Task<int> CheckToolSieveExistAsync(string searchid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/CheckToolMillingBoxExist", ReplyAction="http://tempuri.org/IToolSieveService/CheckToolMillingBoxExistResponse")]
        int CheckToolMillingBoxExist(string boxnumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/CheckToolMillingBoxExist", ReplyAction="http://tempuri.org/IToolSieveService/CheckToolMillingBoxExistResponse")]
        System.Threading.Tasks.Task<int> CheckToolMillingBoxExistAsync(string boxnumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetMaxToolSieveNumber", ReplyAction="http://tempuri.org/IToolSieveService/GetMaxToolSieveNumberResponse")]
        string GetMaxToolSieveNumber();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IToolSieveService/GetMaxToolSieveNumber", ReplyAction="http://tempuri.org/IToolSieveService/GetMaxToolSieveNumberResponse")]
        System.Threading.Tasks.Task<string> GetMaxToolSieveNumberAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IToolSieveServiceChannel : PMSClient.ToolService.IToolSieveService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ToolSieveServiceClient : System.ServiceModel.ClientBase<PMSClient.ToolService.IToolSieveService>, PMSClient.ToolService.IToolSieveService {
        
        public ToolSieveServiceClient() {
        }
        
        public ToolSieveServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ToolSieveServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ToolSieveServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ToolSieveServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PMSClient.ToolService.DcToolSieve[] GetToolSieve(string searchid, string materialGroup, int s, int t) {
            return base.Channel.GetToolSieve(searchid, materialGroup, s, t);
        }
        
        public System.Threading.Tasks.Task<PMSClient.ToolService.DcToolSieve[]> GetToolSieveAsync(string searchid, string materialGroup, int s, int t) {
            return base.Channel.GetToolSieveAsync(searchid, materialGroup, s, t);
        }
        
        public int GetToolSieveCount(string searchid, string materialGroup) {
            return base.Channel.GetToolSieveCount(searchid, materialGroup);
        }
        
        public System.Threading.Tasks.Task<int> GetToolSieveCountAsync(string searchid, string materialGroup) {
            return base.Channel.GetToolSieveCountAsync(searchid, materialGroup);
        }
        
        public PMSClient.ToolService.DcToolSieve[] GetToolSieveUsed(string searchid, string materialGroup, int s, int t) {
            return base.Channel.GetToolSieveUsed(searchid, materialGroup, s, t);
        }
        
        public System.Threading.Tasks.Task<PMSClient.ToolService.DcToolSieve[]> GetToolSieveUsedAsync(string searchid, string materialGroup, int s, int t) {
            return base.Channel.GetToolSieveUsedAsync(searchid, materialGroup, s, t);
        }
        
        public int GetToolSieveUsedCount(string searchid, string materialGroup) {
            return base.Channel.GetToolSieveUsedCount(searchid, materialGroup);
        }
        
        public System.Threading.Tasks.Task<int> GetToolSieveUsedCountAsync(string searchid, string materialGroup) {
            return base.Channel.GetToolSieveUsedCountAsync(searchid, materialGroup);
        }
        
        public void AddToolSieve(PMSClient.ToolService.DcToolSieve model) {
            base.Channel.AddToolSieve(model);
        }
        
        public System.Threading.Tasks.Task AddToolSieveAsync(PMSClient.ToolService.DcToolSieve model) {
            return base.Channel.AddToolSieveAsync(model);
        }
        
        public void UpdateToolSieve(PMSClient.ToolService.DcToolSieve model) {
            base.Channel.UpdateToolSieve(model);
        }
        
        public System.Threading.Tasks.Task UpdateToolSieveAsync(PMSClient.ToolService.DcToolSieve model) {
            return base.Channel.UpdateToolSieveAsync(model);
        }
        
        public int CheckToolSieveExist(string searchid) {
            return base.Channel.CheckToolSieveExist(searchid);
        }
        
        public System.Threading.Tasks.Task<int> CheckToolSieveExistAsync(string searchid) {
            return base.Channel.CheckToolSieveExistAsync(searchid);
        }
        
        public int CheckToolMillingBoxExist(string boxnumber) {
            return base.Channel.CheckToolMillingBoxExist(boxnumber);
        }
        
        public System.Threading.Tasks.Task<int> CheckToolMillingBoxExistAsync(string boxnumber) {
            return base.Channel.CheckToolMillingBoxExistAsync(boxnumber);
        }
        
        public string GetMaxToolSieveNumber() {
            return base.Channel.GetMaxToolSieveNumber();
        }
        
        public System.Threading.Tasks.Task<string> GetMaxToolSieveNumberAsync() {
            return base.Channel.GetMaxToolSieveNumberAsync();
        }
    }
}
