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
    [Attributes.ServiceObject("LIMXmlField", "LIM Xml Field", "LIM Xml Field")]
    public class XmlField
    {

        [Attributes.Property("Name", SoType.Text, "Name", "Name")]
        public string Name { get; set; }

        [Attributes.Property("Value", SoType.Text, "Value", "Value")]
        public string Value { get; set; }

        [Attributes.Property("Category", SoType.Text, "Category", "Category")]
        public string Category { get; set; }

        [Attributes.Property("Hidden", SoType.YesNo, "Hidden", "Hidden")]
        public bool Hidden { get; set; }

        [Attributes.Property("Metadata", SoType.YesNo, "Metadata", "Metadata")]
        public string Metadata { get; set; }

        [Attributes.Property("Schema", SoType.YesNo, "Schema", "Schema")]
        public string Schema { get; set; }

        [Attributes.Property("Xsl", SoType.YesNo, "Xsl", "Xsl")]
        public string Xsl { get; set; }

        [Attributes.Property("ProcessId", SoType.Number, "Process Id", "Process Id")]
        public int ProcessId { get; set; }

        [Attributes.Property("ProcessInstanceId", SoType.Number, "Process Instance Id", "Process Instance Id")]
        public int ProcessInstanceId { get; set; }

        [Attributes.Property("ResultStatus", SoType.Number, "Result Status", "Result Status")]
        public string ResultStatus { get; set; }

        [Attributes.Property("ResultMessage", SoType.Text, "Result Message", "Result Message")]
        public string ResultMessage { get; set; }


        [Attributes.Method("GetXmlFields", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.List, "Get Xml Fields", "Get Xml Fields",
        new string[] { "ProcessInstanceId" }, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "Name", "Value", "Category", "Hidden", "Metadata", "Schema", "Xsl" })]
        public List<LIM.XmlField> GetProcessInstanceXmlFields()
        {
            List<LIM.XmlField> result = new List<LIM.XmlField>();

            SourceCode.Workflow.Client.Connection k2con = new SourceCode.Workflow.Client.Connection();

            k2con.Open("localhost");


            var k2pi = k2con.OpenProcessInstance(this.ProcessInstanceId);

            if (k2pi != null)
            {
                for (int i = 0; i < k2pi.XmlFields.Count; i++)
                {
                    LIM.XmlField df = new LIM.XmlField();
                    df.ProcessInstanceId = this.ProcessInstanceId;
                    df.Name = k2pi.XmlFields[i].Name;
                    df.Value = k2pi.XmlFields[i].Value.ToString();
                    df.Category = k2pi.XmlFields[i].Category;
                    df.Metadata = k2pi.XmlFields[i].MetaData;
                    df.Hidden = k2pi.XmlFields[i].Hidden;
                    df.Schema = k2pi.XmlFields[i].Schema;
                    df.Xsl = k2pi.XmlFields[i].Xsl;

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
        new string[] { "ProcessInstanceId", "Name", "Value", "Category", "Hidden", "Metadata", "Schema", "Xsl", "Status", "ResultStatus", "ResultMessage" })]
        public LIM.XmlField GetProcessInstanceDataField()
        {
            SourceCode.Workflow.Client.Connection k2con = new SourceCode.Workflow.Client.Connection();

            k2con.Open("localhost");

            var k2pi = k2con.OpenProcessInstance(this.ProcessInstanceId);

            if (k2pi != null)
            {
                if (k2pi.XmlFields[Name] != null)
                {
                    this.Name = k2pi.XmlFields[Name].Name;
                    this.Value = k2pi.XmlFields[Name].Value.ToString();
                    this.Category = k2pi.XmlFields[Name].Category;
                    this.Metadata = k2pi.XmlFields[Name].MetaData;
                    this.Hidden = k2pi.XmlFields[Name].Hidden;
                    this.Schema = k2pi.XmlFields[Name].Schema;
                    this.Xsl = k2pi.XmlFields[Name].Xsl;

                }
            }
            k2con.Close();
            k2con.Dispose();

            return this;
        }

        [Attributes.Method("UpdateDataField", SourceCode.SmartObjects.Services.ServiceSDK.Types.MethodType.Read, "Update Data Field", "Update Data Field",
        new string[] { "ProcessInstanceId", "Name", "Value" }, //required property array (no required properties for this sample)
        new string[] { "ProcessInstanceId", "Name", "Value" }, //input property array (no optional input properties for this sample)
        new string[] { "ProcessInstanceId", "Name", "Value", "Category", "Hidden", "Metadata", "Schema", "Xsl", "ResultStatus", "ResultMessage" })]
        public LIM.XmlField UpdateDataField()
        {
            SourceCode.Workflow.Client.Connection k2con = new SourceCode.Workflow.Client.Connection();

            k2con.Open("localhost");

            var k2pi = k2con.OpenProcessInstance(this.ProcessInstanceId);

            if (k2pi != null)
            {
                if (k2pi.XmlFields[Name] != null)
                {
                    k2pi.XmlFields[Name].Value = this.Value;
                    k2pi.Update();
                }
            }
            k2con.Close();
            k2con.Dispose();

            return this;
        }
    }
}
