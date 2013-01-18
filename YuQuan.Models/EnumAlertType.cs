namespace YuQuan.Models
{
    /// <summary>
    /// Reference: IC.Net ABXAlert Alert Primary Elements
    /// ADT  Element  –  use this to alert to a specific patient movement or for when a patient is admitted from a particular source. For instance you might create an alert for a patient that transfers to a location after having a positive microbiology result in a previous location; or an alert for a patient that is admitted from a nursing/rest home.
    /// Allergy Primary Element  –  used to alert to patients that have allergies, usually in combination with other elements e.g. a positive Streptococcus result for a patient with a Penicillin allergy.
    /// Condition Primary Element  –  this element allows you to alert on patients that have conditions logged against them, for instance you might create an alert for Diarrhoea followed by a positive faecal specimen.
    /// Demographic Primary Element – allows you to create alerts based on age or sex.
    /// Device Primary Element – allows you to alert when a patient has had a significant finding following insertion of a device e.g. a positive blood culture following insertion of a Central Line; or a radiology finding following a Ventilator.
    /// Diagnosis  Primary Element  –  used to alert to patients that have had a specific diagnosis on admission to hospital or at some time in the past, usually in combination with other elements.
    /// Observation  Element  –  used to alert if a patient falls above or below set criteria for one or more observation or vitals readings. For instance, you might have an alert based on someone having a high or low WBC, or an alert for someone who has a significant blood chemistry level of a particular medication.
    /// Pathology  Primary Element  –  contains all specimen-related data e.g. organism, specimen type, specimen location. If you were to create an alert based on two specimens from the same location within 7 days of each other, you would only need to use microbiology elements as you would be considering the specimen location, not the PAS/HIS patient location.
    /// Patient  Primary  Element  –  used to define if the patient is an inpatient or discharged at the time of the alert. For instance you might use this with a microbiology element to alert for patients that are discharged following a positive microbiology result in the hospital.
    /// Pharmacy  –  contains all the medications related data e.g.  the drug, the route of administration for the  drug and the therapy duration. Remember this is details of the drug being administered to the patient. Measured drug levels in a patient can be referenced as an observation element.
    /// Surgery Primary Element – allows alerts based upon various surgery elements such as type of surgery, specific surgeons or where the surgery took place, for instance you might create an alert for patients that had a cardiac surgery then had a device inserted, followed by a positive blood culture.
    /// Tag Primary Element  –  used to alert when a patient with a tag has a significant event. For instance, a patient with an MRSA tag is readmitted to hospital; or a patient with an SSI Surveillance tag has a positive wound culture
    /// </summary>
    public enum EnumAlertType
    {
        /// <summary>
        /// Business process, e.g. Admission, Discharge
        /// Use this to alert to a specific patient movement or for when a patient is admitted from a particular source
        /// </summary>
        Process, 
        
        /// <summary>
        /// used to alert to patients that have allergies, usually in combination with other elements e.g. a positive Streptococcus result for a patient with a Penicillin allergy.
        /// </summary>
        Allergy,
        
        /// <summary>
        /// this element allows you to alert on patients that have conditions logged against them, for instance you might create an alert for Diarrhoea followed by a positive faecal specimen
        /// </summary>
        Condition, 
        
        /// <summary>
        /// allows you to create alerts based on age or sex
        /// </summary>
        Demograph, 
        
        /// <summary>
        /// used to alert to patients that have had a specific diagnosis on admission to hospital or at some time in the past, usually in combination with other elements
        /// </summary>
        Diagnosis,

        /// <summary>
        /// Lab Test
        /// </summary>
        Test, 
        
        /// <summary>
        /// Including Examinations (CT,MRI,US) and Lab test(Blood,Urine).
        /// used to alert if a patient falls above or below set criteria for one or more observation or vitals readings. For instance, you might have an alert based on someone having a high or low WBC, or an alert for someone who has a significant blood chemistry level of a particular medication
        /// </summary>
        Observation, 
        
        /// <summary>
        /// contains all specimen-related data e.g. organism, specimen type, specimen location.
        /// </summary>
        Pathology, 
        
        /// <summary>
        /// contains all the medications related data e.g.  the drug, the route of administration for the  drug and the therapy duration. Remember this is details of the drug being administered to the patient. Measured drug levels in a patient can be referenced as an observation element
        /// </summary>
        Pharmacy,
        
        /// <summary>
        /// allows alerts based upon various surgery elements such as type of surgery, specific surgeons or where the surgery took place, for instance you might create an alert for patients that had a cardiac surgery then had a device inserted, followed by a positive blood culture.
        /// </summary>
        Surgery
    }
}
