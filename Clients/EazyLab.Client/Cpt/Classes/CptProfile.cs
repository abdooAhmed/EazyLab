﻿using EazyLabClient;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.SmartCards;
using Newtonsoft;
using Newtonsoft.Json;
using EazyLab.Classes;
using EazyLab.Model;
namespace EazyLab.Cpt.Classes
{
    public class CptProfile : Entity
    {
        public enum ProfileSource {T0,T1, T2, T3, T4, T5, T0_T1,T2_T3,T4_T5,Current,Power,Energy}


        /// <summary>
        /// The include string.
        /// all BSonexpression sepated by ','
        /// </summary>
        public const string IncludeString = "$.TempZones";
        
        public double MaxCurrent { set; get; }
 
        public double MinPowerFactor { set; get; }
        public bool RejectIfPowerFactor { set; get; }
        public bool RejectIfCurrent { set; get; }

        [BsonCtor]
        public CptProfile()
        {
           if( JsonString== null) JsonString = String.Empty;
            TempZones =(List<CptTempZone>) JsonConvert.DeserializeObject<List<CptTempZone>>(JsonString);
            if(TempZones==null) TempZones = new List<CptTempZone>();
            Id = 1; 
            // BsonMapper.Global.Entity<CptTempProfile>().DbRef(x => x.TempZones, "CptTempZone");
        }

        public ProfileSource  Source { set;get; }
        public   List<CptTempZone> TempZones { set; get; }
        public string JsonString { get; set; }
        internal CptTempZone GetZone(int index)
        {
            return TempZones[index];
        }

        public void Add(CptTempZone zone)
        {
            if (zone == null) { return; }

            for(int i = 0; i < TempZones.Count; i++)
            {
                if (TempZones[i].Time == zone.Time) TempZones.RemoveAt(i);
            }
            TempZones.Add(zone);
            JsonString = JsonConvert.SerializeObject(TempZones,Formatting.Indented);
        }

        public void Del(CptTempZone zone)
        {
            if (zone == null) { return; }

            for (int i = 0; i < TempZones.Count; i++)
            {
                if (TempZones[i].Time == zone.Time) TempZones.RemoveAt(i);
            }
            JsonString = JsonConvert.SerializeObject(TempZones);
        }
        public void Del(int  indx)
        {
            if(indx < TempZones.Count && indx>=0) { TempZones.RemoveAt(indx);
                JsonString = JsonConvert.SerializeObject(TempZones, Formatting.Indented);
            }
            
        }
        public void DelByTime(double time)
        {
            var i = TempZones.FindIndex(x => x.Time == time);
            if (i >= 0) { TempZones.RemoveAt(i); }
        }
    }
}
