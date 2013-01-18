using System.Workflow.Runtime.Hosting;
using System.Configuration;
using System.Workflow.Activities.Rules;
using System.Xml;
using System.Workflow.ComponentModel.Serialization;
using System;
using System.IO;
using System.Data.SqlClient;
namespace WuKeSong.Services
{
    public class RuleSetService : WorkflowRuntimeService
    {
        public RuleSetService()
        {
            var connectionStringSettingsCollection = ConfigurationManager.ConnectionStrings;

            foreach (ConnectionStringSettings connectionStringSettings in connectionStringSettingsCollection)
            {
                if (string.CompareOrdinal(connectionStringSettings.Name, "RuleSets") == 0)
                {
                    connectionString = connectionStringSettings.ConnectionString;
                    break;
                }
            }

            if (string.IsNullOrEmpty(connectionString))
                throw new ConfigurationErrorsException("SQL connection string not available for the (should be provided in the config file).");
        }

        string connectionString;

        public RuleSet GetRuleSet(string ruleSetName)
        {
            RuleSet ruleSet = null;

            if (!string.IsNullOrEmpty(ruleSetName))
            {
                var sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                string commandString;

                commandString = "SELECT TOP 1 * FROM TriggerRule WHERE Name=@name ORDER BY Version DESC"; //TODO: refine later
                
                SqlCommand command = new SqlCommand(commandString, sqlConn);
                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 128);
                command.Parameters["@name"].Value = ruleSetName;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    try
                    {
                        Object[] values = new Object[reader.FieldCount];
                        reader.GetValues(values);
                        var ruleSetXmlDefinition = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            if (name == "RuleSet")
                            {
                                ruleSetXmlDefinition = values[i] as string;
                                break;
                            }
                        }

                        if (!String.IsNullOrEmpty(ruleSetXmlDefinition))
                        {                           
                            ruleSet = (new WorkflowMarkupSerializer()).Deserialize(new XmlTextReader(new StringReader(ruleSetXmlDefinition))) as RuleSet;
                        }
                    }
                    catch (InvalidCastException)
                    {
                        
                    }
                }

                sqlConn.Close();                
            }

            return ruleSet;
        }
    }
}
