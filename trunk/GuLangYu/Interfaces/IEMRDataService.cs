using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YuQuan.Models;

namespace GuLangYu.Interfaces
{
    /// <summary>
    /// Provides methods for communicating data with EMR
    /// </summary>
    public interface IEMRDataService
    {
        IList<Encounter> GetActiveEncounterList(KBEntities db);        
        IList<Encounter> GetEncounterList(string PatientId = null);
    }
}
