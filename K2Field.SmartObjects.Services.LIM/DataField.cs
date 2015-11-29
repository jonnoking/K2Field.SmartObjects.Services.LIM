using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using Attributes = SourceCode.SmartObjects.Services.ServiceSDK.Attributes;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System.Net;
using SourceCode.Workflow.Management;

namespace K2Field.SmartObjects.Services.LIM
{
    [Attributes.ServiceObject("LIMDataField", "LIM Data Field", "LIM Data Field")]
    public class DataField
    {
        [Attributes.Property("Name", SoType.Text, "Name", "Name")]
        public string Name { get; set; }

        [Attributes.Property("Value", SoType.Text, "Value", "Value")]
        public string Value { get; set; }

        [Attributes.Property("Category", SoType.Text, "Category", "Category")]
        public string Category { get; set; }

        [Attributes.Property("Hidden", SoType.YesNo, "Hidden", "Hidden")]
        public bool Hidden { get; set; }

        [Attributes.Property("Metadata", SoType.Text, "Metadata", "Metadata")]
        public string Metadata { get; set; }

        [Attributes.Property("ValueType", SoType.Text, "ValueType", "ValueType")]
        public string ValueType { get; set; }

        [Attributes.Property("ProcessId", SoType.Number, "Process Id", "Process Id")]
        public int ProcessId { get; set; }

        [Attributes.Property("ProcessInstanceId", SoType.Number, "Process Instance Id", "Process Instance Id")]
        public int ProcessInstanceId { get; set; }

        [Attributes.Property("ResultStatus", SoType.Number, "Result Status", "Result Status")]
        public string ResultStatus { get; set; }

        [Attributes.Property("ResultMessage", SoType.Text, "Result Message", "Result Message")]
        public string ResultMessage { get; set; }

        [Attributes.Method("GetDataFields", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.List, "Get Data Fields", "Get Data Fields",
        new string[] { "ProcessInstanceId" }, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "Name", "Value", "Category", "Hidden", "Metadata", "ValueType" })] 
        public List<LIM.DataField> GetProcessInstanceDataFields()
        {
            List<LIM.DataField> result = new List<DataField>();

            SourceCode.Workflow.Client.Connection k2con = new SourceCode.Workflow.Client.Connection();
            
            k2con.Open("localhost");


            var k2pi = k2con.OpenProcessInstance(this.ProcessInstanceId);

            if (k2pi != null)
            {
                for (int i=0;i<k2pi.DataFields.Count;i++)
                {
                    LIM.DataField df = new DataField();
                    df.ProcessInstanceId = this.ProcessInstanceId;
                    df.Name = k2pi.DataFields[i].Name;
                    df.Value = k2pi.DataFields[i].Value.ToString();
                    df.Category = k2pi.DataFields[i].Category;
                    df.Metadata = k2pi.DataFields[i].MetaData;
                    df.Hidden = k2pi.DataFields[i].Hidden;
                    df.ValueType = k2pi.DataFields[i].ValueType.ToString().Replace("Type", "");

                    result.Add(df);
                }
            }
            k2con.Close();
            k2con.Dispose();

            return result;
        }

        [Attributes.Method("GetDataField", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.Read, "Get Data Field", "Get Data Field",
        new string[] { "ProcessInstanceId", "Name" }, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId", "Name" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "Name", "Value", "Category", "Hidden", "Metadata", "ValueType", "ResultStatus", "ResultMessage" })]
        public LIM.DataField GetProcessInstanceDataField()
        {
            SourceCode.Workflow.Client.Connection k2con = new SourceCode.Workflow.Client.Connection();

            k2con.Open("localhost");

            var k2pi = k2con.OpenProcessInstance(this.ProcessInstanceId);

            if (k2pi != null)
            {
                if (k2pi.DataFields[Name] != null)
                {
                    this.Name = k2pi.DataFields[Name].Name;
                    this.Value = k2pi.DataFields[Name].Value.ToString();
                    this.Category = k2pi.DataFields[Name].Category;
                    this.Metadata = k2pi.DataFields[Name].MetaData;
                    this.Hidden = k2pi.DataFields[Name].Hidden;
                    this.ValueType = k2pi.DataFields[Name].ValueType.ToString().Replace("Type", "");
                }
            }
            k2con.Close();
            k2con.Dispose();

            return this;
        }

        [Attributes.Method("UpdateDataField", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.Read, "Update Data Field", "Update Data Field",
        new string[] { "ProcessInstanceId", "Name", "Value"}, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId", "Name", "Value" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "Name", "Value", "Category", "Hidden", "Metadata", "ValueType", "ResultStatus", "ResultMessage" })]
        public LIM.DataField UpdateDataField()
        {
            SourceCode.Workflow.Client.Connection k2con = new SourceCode.Workflow.Client.Connection();

            k2con.Open("localhost");

            var k2pi = k2con.OpenProcessInstance(this.ProcessInstanceId);

            if (k2pi != null)
            {
                if (k2pi.DataFields[Name] != null)
                {
                    k2pi.DataFields[Name].Value = System.ComponentModel.TypeDescriptor.GetConverter(k2pi.DataFields[Name].Value).ConvertFromString(this.Value);
                    k2pi.Update();
                }
            }
            k2con.Close();
            k2con.Dispose();

            return this;
        }
    }
}
