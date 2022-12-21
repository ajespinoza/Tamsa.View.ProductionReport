// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientConfiguration.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the ClientConfiguration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// Configuration client class.
    /// </summary>
    public static class ClientConfiguration
    {
        #region Constants
        
        /// <summary>
        /// The section configuration section name.
        /// </summary>
        private static string sectionname = "ExitApplicationClientConfiguration";
        
        #endregion

        #region Members

        /// <summary>
        /// The section names.
        /// </summary>
        private static ExitApplicationClientConfiguration settings = ConfigurationManager.GetSection(sectionname) as ExitApplicationClientConfiguration;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets settings.
        /// </summary>
        public static ExitApplicationClientConfiguration Settings
        {
            get { return settings; }
        }
        #endregion
    }

    /// <summary>
    /// Configuration section 
    /// </summary>
    public class ExitApplicationClientConfiguration : ConfigurationSection, IXmlSerializable
    {
        /// <summary>
        /// Gets or sets application process property.
        /// </summary>
        [ConfigurationProperty("Application.Process", IsRequired = true)]
        public string ApplicationProcess
        {
            get { return (string)base["Application.Process"]; }
            set { base["Application.Process"] = value; }
        }

        /// <summary>
        /// Gets or sets interval timer property.
        /// </summary>
        [ConfigurationProperty("Application.IntervalTimer", IsRequired = true)]
        public string ApplicationIntervalTimer
        {
            get { return (string)base["Application.IntervalTimer"]; }
            set { base["Application.IntervalTimer"] = value; }
        }

        /// <summary>
        /// Gets or sets application mode property.
        /// </summary>
        [ConfigurationProperty("Application.IsEnableChangeUser", IsRequired = true)]
        public string IsEnableChangeUser
        {
            get { return (string)base["Application.IsEnableChangeUser"]; }
            set { base["Application.IsEnableChangeUser"] = value; }
        }

        /// <summary>
        /// Gets or sets application mode property.
        /// </summary>
        [ConfigurationProperty("Application.IsNotifyChangeUser", IsRequired = true)]
        public string IsEnableNotifyChangeUser
        {
            get { return (string)base["Application.IsNotifyChangeUser"]; }
            set { base["Application.IsNotifyChangeUser"] = value; }
        }

        /// <summary>
        /// Gets or sets is enable rejection code property.
        /// </summary>
        [ConfigurationProperty("Application.IsEnableRejectionCode", IsRequired = true)]
        public string IsEnableRejectionCode
        {
            get { return (string)base["Application.IsEnableRejectionCode"]; }
            set { base["Application.IsEnableRejectionCode"] = value; }
        }

        /// <summary>
        /// Gets or sets is enable rejection code property.
        /// </summary>
        [ConfigurationProperty("Application.ForceGranel", IsRequired = true)]
        public string IsForceGranel
        {
            get { return (string)base["Application.ForceGranel"]; }
            set { base["Application.ForceGranel"] = value; }
        }

        /// <summary>
        /// Gets or sets is full mode property.
        /// </summary>
        [ConfigurationProperty("Application.FullMode", IsRequired = true)]
        public string IsFullMode
        {
            get { return (string)base["Application.FullMode"]; }
            set { base["Application.FullMode"] = value; }
        }

        /// <summary>
        /// Gets or sets process to report property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetProcessToReport", IsRequired = true)]
        public string StoredProcedureGetProcessToReport
        {
            get { return (string)base["StoredProcedure.GetProcessToReport"]; }
            set { base["StoredProcedure.GetProcessToReport"] = value; }
        }

        /// <summary>
        /// Gets or sets keys history property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetKeysHistory", IsRequired = true)]
        public string StoredProcedureGetKeysHistory
        {
            get { return (string)base["StoredProcedure.GetKeysHistory"]; }
            set { base["StoredProcedure.GetKeysHistory"] = value; }
        }

        /// <summary>
        /// Gets or sets detail key history property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetDetailKeyHistory", IsRequired = true)]
        public string StoredProcedureGetDetailKeyHistory
        {
            get { return (string)base["StoredProcedure.GetDetailKeyHistory"]; }
            set { base["StoredProcedure.GetDetailKeyHistory"] = value; }
        }

        /// <summary>
        /// Gets or sets ins key property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.InsKey", IsRequired = true)]
        public string StoredProcedureInsKey
        {
            get { return (string)base["StoredProcedure.InsKey"]; }
            set { base["StoredProcedure.InsKey"] = value; }
        }

        /// <summary>
        /// Gets or sets del group.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.DelGroup", IsRequired = true)]
        public string StoredProcedureDelGroup
        {
            get { return (string)base["StoredProcedure.DelGroup"]; }
            set { base["StoredProcedure.DelGroup"] = value; }
        }

        /// <summary>
        /// Gets or sets update key for reejction code property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.UpdGroup", IsRequired = true)]
        public string StoredProcedureUpdGroup
        {
            get { return (string)base["StoredProcedure.UpdGroup"]; }
            set { base["StoredProcedure.UpdGroup"] = value; }
        }

        /// <summary>
        /// Gets or sets get local user property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetLocalUser", IsRequired = true)]
        public string StoredProcedureGetLocalUser
        {
            get { return (string)base["StoredProcedure.GetLocalUser"]; }
            set { base["StoredProcedure.GetLocalUser"] = value; }
        }

        /// <summary>
        /// Gets or sets upd local user property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.UpdLocalUser", IsRequired = true)]
        public string StoredProcedureUpdLocalUser
        {
            get { return (string)base["StoredProcedure.UpdLocalUser"]; }
            set { base["StoredProcedure.UpdLocalUser"] = value; }
        }

        /// <summary>
        /// Gets or sets shift information  user property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetShiftInformation", IsRequired = true)]
        public string StoredProcedureGetShiftInformation
        {
            get { return (string)base["StoredProcedure.GetShiftInformation"]; }
            set { base["StoredProcedure.GetShiftInformation"] = value; }
        }

        /// <summary>
        /// Gets or sets tracking to balance property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetTrackingToBalance", IsRequired = true)]
        public string StoredProcedureGetTrackingToBalance
        {
            get { return (string)base["StoredProcedure.GetTrackingToBalance"]; }
            set { base["StoredProcedure.GetTrackingToBalance"] = value; }
        }

        /// <summary>
        /// Gets or sets tracking property.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetTrackings", IsRequired = true)]
        public string StoredProcedureGetTrackings
        {
            get { return (string)base["StoredProcedure.GetTrackings"]; }
            set { base["StoredProcedure.GetTrackings"] = value; }
        }

        /// <summary>
        /// Gets or sets Elaboration state group.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetGroupElaborationState", IsRequired = true)]
        public string StoredProcedureGetGroupElaborationState
        {
            get { return (string)base["StoredProcedure.GetGroupElaborationState"]; }
            set { base["StoredProcedure.GetGroupElaborationState"] = value; }
        }

        /// <summary>
        /// Gets or sets Elaboration state by group.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetElaborationStateByGroup", IsRequired = true)]
        public string StoredProcedureGetElaborationStateByGroup
        {
            get { return (string)base["StoredProcedure.GetElaborationStateByGroup"]; }
            set { base["StoredProcedure.GetElaborationStateByGroup"] = value; }
        }

        /// <summary>
        /// Gets Rejections code to IT.
        /// </summary>
        [ConfigurationProperty("StoredProcedure.GetRejectionCodeByElaborationState", IsRequired = true)]
        public string StoredProcedureGetRejectionCodeByElaborationState
        {
            get { return (string)base["StoredProcedure.GetRejectionCodeByElaborationState"]; }
            set { base["StoredProcedure.GetRejectionCodeByElaborationState"] = value; }
        }

        /// <summary>
        /// Gets or sets group elaboration state default.
        /// </summary>
        [ConfigurationProperty("Application.DefaultGroupEECode", IsRequired = true)]
        public string DefaultGroupEECode
        {
            get { return (string)base["Application.DefaultGroupEECode"]; }
            set { base["Application.DefaultGroupEECode"] = value; }
        }

        /// <summary>
        /// Gets or sets elaboration state default.
        /// </summary>
        [ConfigurationProperty("Application.DefaultEECode", IsRequired = true)]
        public string DefaultEECode
        {
            get { return (string)base["Application.DefaultEECode"]; }
            set { base["Application.DefaultEECode"] = value; }
        }

        /// <summary>
        /// Gets areas property.
        /// </summary>
        [ConfigurationProperty("areas", IsRequired = true, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(AreaConfigurationCollection))]
        public AreaConfigurationCollection Areas
        {
            get { return (AreaConfigurationCollection)base["areas"]; }
        }

        #region IXmlSerializable Members

        /// <summary>
        /// IXmlSerializable implementation
        /// </summary>
        /// <returns>
        /// Returns null
        /// </returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// IXmlSerializable implementation
        /// </summary>
        /// <param name="reader">
        /// The reader
        /// </param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.DeserializeElement(reader, false);
        }

        /// <summary>
        /// IXmlSerializable implementation
        /// </summary>
        /// <param name="writer">
        /// The writer
        /// </param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            return;
        }

        #endregion
    }

    /// <summary>
    /// Area configuration collection
    /// </summary>
    [ConfigurationCollection(typeof(AreaConfigurationCollection))]
    public class AreaConfigurationCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets default area.
        /// </summary>
        [ConfigurationProperty("defaultArea", IsRequired = true)]
        public string DefaultArea
        {
            get
            {
                return (string)base["defaultArea"];
            }
        }

        #region Default Accessor
        /// <summary>
        /// Area access.
        /// </summary>
        /// <param name="index">
        /// The area index.
        /// </param>
        /// <returns>
        /// The area configuration.
        /// </returns>
        public AreaConfiguration this[int index]
        {
            get
            {
                return base.BaseGet(index) as AreaConfiguration;
            }

            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }
        #endregion

        #region ConfigurationElementCollection overrides
        
        /// <summary>
        /// Create new element method.
        /// </summary>
        /// <returns>
        /// The area configuration.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AreaConfiguration();
        }

        /// <summary>
        /// get element key method.
        /// </summary>
        /// <param name="element">
        /// The element value.
        /// </param>
        /// <returns>
        /// The area configuration.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AreaConfiguration)element).AreaCode;
        }

        /// <summary>
        /// Gets collection type.
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }
        #endregion
    }

    /// <summary>
    /// Area configuration element
    /// </summary>
    public class AreaConfiguration : ConfigurationElement
    {
        #region Configuration Properties

        /// <summary>
        /// Gets area code.
        /// </summary>
        [ConfigurationProperty("areaCode", IsRequired = true)]
        public string AreaCode
        {
            get { return (string)base["areaCode"]; }
        }

        /// <summary>
        /// Gets production machines.
        /// </summary>
        [ConfigurationProperty("productionMachines", IsRequired = true)]
        public string ProductionMachines
        {
            get { return (string)base["productionMachines"]; }
        }

        /// <summary>
        /// Gets sets default production machine.
        /// </summary>
        [ConfigurationProperty("defaultProductionMachine", IsRequired = true)]
        public string DefaultProductionMachine
        {
            get { return (string)base["defaultProductionMachine"]; }
        }

        /// <summary>
        /// Gets is active reworked machines.
        /// </summary>
        [ConfigurationProperty("isActiveReworkedMachines", IsRequired = false)]
        public bool? IsActiveReworkedMachines
        {
            get { return base["isActiveReworkedMachines"] as bool?; }
        }

        /// <summary>
        /// Gets reworked machines.
        /// </summary>
        [ConfigurationProperty("reworkedMachines", IsRequired = true)]
        public string ReworkedMachines
        {
            get { return (string)base["reworkedMachines"]; }
        }

        /// <summary>
        /// Gets default reworked machines.
        /// </summary>
        [ConfigurationProperty("defaultReworkedMachine", IsRequired = true)]
        public string DefaultReworkedMachine
        {
            get { return (string)base["defaultReworkedMachine"]; }
        }

        #endregion
    }
}

