/***************************************************************
* Copyright (C) 2011 Jeremy Reagan, All Rights Reserved.
* I may be reached via email at: jeremy.reagan@live.com
* 
* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; under version 2
* of the License.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7_Analyst
{
    /// <summary>
    /// MessageType Class: Used to store message type descriptions
    /// </summary>
    class MessageType
    {
        /// <summary>
        /// The message type name
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The message type description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// MessageType constructor
        /// </summary>
        /// <param name="MsgType">The message type name</param>
        public MessageType(string MsgType)
        {
            Type = MsgType;
            Description = GetMessageDescription(MsgType);
        }
        /// <summary>
        /// Loads and pulls the specified MessageType description
        /// </summary>
        /// <param name="MsgType">The message type to load</param>
        /// <returns>The message type description</returns>
        private string GetMessageDescription(string MsgType)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("A01", "Admit/visit notification");
            dic.Add("A02", "Transfer a patient");
            dic.Add("A03", "Discharge/end visit");
            dic.Add("A04", "Register a patient");
            dic.Add("A05", "Pre-admit a patient");
            dic.Add("A06", "Change an outpatient to an inpatient");
            dic.Add("A07", "Change an inpatient to an outpatient");
            dic.Add("A08", "Update patient information");
            dic.Add("A09", "Patient departing - tracking");
            dic.Add("A10", "Patient arriving - tracking");
            dic.Add("A11", "Cancel admit/visit notification");
            dic.Add("A12", "Cancel transfer");
            dic.Add("A13", "Cancel discharge/end visit");
            dic.Add("A14", "Pending admit");
            dic.Add("A15", "Pending transfer");
            dic.Add("A16", "Pending discharge");
            dic.Add("A17", "Swap patients");
            dic.Add("A18", "Merge patient information (for backward compatibility only)");
            dic.Add("A19", "QRY/ADR - Patient query");
            dic.Add("A20", "Bed status update");
            dic.Add("A21", "Patient goes on a leave of absence");
            dic.Add("A22", "Patient returns from a leave of absence");
            dic.Add("A23", "Delete a patient record");
            dic.Add("A24", "Link patient information");
            dic.Add("A25", "Cancel pending discharge");
            dic.Add("A26", "Cancel pending transfer");
            dic.Add("A27", "Cancel pending admit");
            dic.Add("A28", "Add person information");
            dic.Add("A29", "Delete person information");
            dic.Add("A30", "Merge person information (for backward compatibility only)");
            dic.Add("A31", "Update person information");
            dic.Add("A32", "Cancel patient arriving - tracking");
            dic.Add("A33", "Cancel patient departing - tracking");
            dic.Add("A34", "Merge patient information - patient ID only (for backward compatibility only)");
            dic.Add("A35", "Merge patient information - account number only (for backward compatibility only)");
            dic.Add("A36", "Merge patient information - patient ID and account number (for backward compatibility only)");
            dic.Add("A37", "Unlink patient information");
            dic.Add("A38", "Cancel pre-admit");
            dic.Add("A39", "Merge person - patient ID (for backward compatibility only)");
            dic.Add("A40", "Merge patient - patient identifier list");
            dic.Add("A41", "Merge account - patient account number");
            dic.Add("A42", "Merge visit - visit number");
            dic.Add("A43", "Move patient information - patient identifier list");
            dic.Add("A44", "Move account information - patient account number");
            dic.Add("A45", "Move visit information - visit number");
            dic.Add("A46", "Change patient ID (for backward compatibility only)");
            dic.Add("A47", "Change patient identifier list");
            dic.Add("A48", "Change alternate patient ID (for backward compatibility only)");
            dic.Add("A49", "Change patient account number");
            dic.Add("A50", "Change visit number");
            dic.Add("A51", "Change alternate visit ID");
            dic.Add("A52", "Cancel leave of absence for a patient");
            dic.Add("A53", "Cancel patient returns from a leave of absence");
            dic.Add("A54", "Change attending doctor");
            dic.Add("A55", "Cancel change attending doctor");
            dic.Add("A60", "Update allergy information");
            dic.Add("A61", "Change consulting doctor");
            dic.Add("A62", "Cancel change consulting doctor");
            dic.Add("B01", "Add personnel record");
            dic.Add("B02", "Update personnel record");
            dic.Add("B03", "Delete personnel re cord");
            dic.Add("B04", "Active practicing person");
            dic.Add("B05", "Deactivate practicing person");
            dic.Add("B06", "Terminate practicing person");
            dic.Add("B07", "Grant Certificate/Permission");
            dic.Add("B08", "Revoke Certificate/Permission");
            dic.Add("C01", "Register a patient on a clinical trial");
            dic.Add("C02", "Cancel a patient registration on clinical trial (for clerical mistakes only)");
            dic.Add("C03", "Correct/update registration information");
            dic.Add("C04", "Patient has gone off a clinical trial");
            dic.Add("C05", "Patient enters phase of clinical trial");
            dic.Add("C06", "Cancel patient entering a phase (clerical mistake)");
            dic.Add("C07", "Correct/update phase information");
            dic.Add("C08", "Patient has gone off phase of clinical trial");
            dic.Add("C09", "Automated time intervals for reporting, like monthly");
            dic.Add("C10", "Patient completes the clinical trial");
            dic.Add("C11", "Patient completes a phase of the clinical trial");
            dic.Add("C12", "Update/correction of patient order/result information");
            dic.Add("E01", "Submit HealthCare Services Invoice");
            dic.Add("E02", "Cancel HealthCare Services Invoice");
            dic.Add("E03", "HealthCare Services Invoice Status");
            dic.Add("E04", "Re-Assess HealthCare Services Invoice Request");
            dic.Add("E10", "Edit/Adjudication Results");
            dic.Add("E12", "Request Additional Information");
            dic.Add("E13", "Additional Information Response");
            dic.Add("E15", "Payment/Remittance Advice");
            dic.Add("E20", "Submit Authorization Request");
            dic.Add("E21", "Cancel Authorization Request");
            dic.Add("E22", "Authorization Request Status");
            dic.Add("E24", "Authorization Response");
            dic.Add("E30", "Submit Health Document related to Authorization Request");
            dic.Add("E31", "Cancel Health Document related to Authorization Request");
            dic.Add("I01", "Request for insurance information");
            dic.Add("I02", "Request/receipt of patient selection display list");
            dic.Add("I03", "Request/receipt of patient selection list");
            dic.Add("I04", "Request for patient demographic data");
            dic.Add("I05", "Request for patient clinical information");
            dic.Add("I06", "Request/receipt of clinical data listing");
            dic.Add("I07", "PIN/ACK - Unsolicited insurance information");
            dic.Add("I08", "Request for treatment authorization information");
            dic.Add("I09", "Request for modification to an authorization");
            dic.Add("I10", "Request for resubmission of an authorization");
            dic.Add("I11", "Request for cancellation of an authorization");
            dic.Add("I12", "Patient referral");
            dic.Add("I13", "Modify patient referral");
            dic.Add("I14", "Cancel patient referral");
            dic.Add("I15", "Request patient referral status");
            dic.Add("J01", "Cancel query/acknowledge message");
            dic.Add("J02", "Cancel subscription/acknowledge message");
            dic.Add("K11", "Segment pattern response in response to QBP^Q11");
            dic.Add("K13", "Tabular response in response to QBP^Q13");
            dic.Add("K15", "Display response in response to QBP^Q15");
            dic.Add("K21", "Get person demographics response");
            dic.Add("K22", "Find candidates response");
            dic.Add("K23", "Get corresponding identifiers response");
            dic.Add("K24", "Allocate identifiers response");
            dic.Add("K25", "Personnel Information by Segment Response");
            dic.Add("K31", "Dispense History Response");
            dic.Add("M01", "Master file not otherwise specified (for backward compatibility only)");
            dic.Add("M02", "Master file - staff practitioner");
            dic.Add("M03", "Master file - test/observation (for backward compatibility only)");
            dic.Add("M04", "Master files charge description");
            dic.Add("M05", "Patient location master file");
            dic.Add("M06", "Clinical study with phases and schedules master file");
            dic.Add("M07", "Clinical study without phases but with schedules master file");
            dic.Add("M08", "Test/observation (numeric) master file");
            dic.Add("M09", "Test/observation (categorical) master file");
            dic.Add("M10", "Test /observation batteries master file");
            dic.Add("M11", "Test/calculated observations master file");
            dic.Add("M12", "Master file notification message");
            dic.Add("M13", "Master file notification - general");
            dic.Add("M14", "Master file notification - site defined");
            dic.Add("M15", "Inventory item master file notification");
            dic.Add("M16", "Master File Notification Inventory Item Enhanced");
            dic.Add("M17", "DRG Master File Message");
            dic.Add("N01", "Application management query message");
            dic.Add("N02", "Application management data message (unsolicited)");
            dic.Add("O01", "Order message (also RDE, RDS, RGV, RAS)");
            dic.Add("O02", "Order response (also RRE, RRD, RRG, RRA)");
            dic.Add("O03", "Diet order");
            dic.Add("O04", "Diet order acknowledgment");
            dic.Add("O05", "Stock requisition order");
            dic.Add("O06", "Stock requisition acknowledgment");
            dic.Add("O07", "Non-stock requisition order");
            dic.Add("O08", "Non-stock requisition acknowledgment");
            dic.Add("O09", "Pharmacy/treatment order");
            dic.Add("O10", "Pharmacy/treatment order acknowledgment");
            dic.Add("O11", "Pharmacy/treatment encoded order");
            dic.Add("O12", "Pharmacy/treatment encoded order acknowledgment");
            dic.Add("O13", "Pharmacy/treatment dispense");
            dic.Add("O14", "Pharmacy/treatment dispense acknowledgment");
            dic.Add("O15", "Pharmacy/treatment give");
            dic.Add("O16", "Pharmacy/treatment give acknowledgment");
            dic.Add("O17", "Pharmacy/treatment administration");
            dic.Add("O18", "Pharmacy/treatment administration acknowledgment");
            dic.Add("O19", "General clinical order");
            dic.Add("O20", "General clinical order response");
            dic.Add("O21", "Laboratory order");
            dic.Add("O22", "General laboratory order response message to any OML");
            dic.Add("O23", "Imaging order");
            dic.Add("O24", "Imaging order response message to any OMI");
            dic.Add("O25", "Pharmacy/treatment refill authorization request");
            dic.Add("O26", "Pharmacy/Treatment Refill Authorization Acknowledgement");
            dic.Add("O27", "Blood product order");
            dic.Add("O28", "Blood product order acknowledgment");
            dic.Add("O29", "Blood product dispense status");
            dic.Add("O30", "Blood product dispense status acknowledgment");
            dic.Add("O31", "Blood product transfusion/disposition");
            dic.Add("O32", "Blood product transfusion/disposition acknowledgment");
            dic.Add("O33", "Laboratory order for multiple orders related to a single specimen");
            dic.Add("O34", "Laboratory order response message to a multiple order related to single specimen OML");
            dic.Add("O35", "Laboratory order for multiple orders related to a single container of a specimen");
            dic.Add("O36", "Laboratory order response message to a single container of a specimen OML");
            dic.Add("O37", "Population/Location-Based Laboratory Order Message");
            dic.Add("O38", "Population/Location-Based Laboratory Order Acknowledgment Message");
            dic.Add("P01", "Add patient accounts");
            dic.Add("P02", "Purge patient accounts");
            dic.Add("P03", "Post detail financial transaction");
            dic.Add("P04", "Generate bill and A/R statements");
            dic.Add("P05", "Update account");
            dic.Add("P06", "End account");
            dic.Add("P07", "Unsolicited initial individual product experience report");
            dic.Add("P08", "Unsolicited update individual product experience report");
            dic.Add("P09", "Summary product experience report");
            dic.Add("P10", "Transmit Ambulatory Payment Classification(APC)");
            dic.Add("P11", "Post Detail Financial Transactions - New");
            dic.Add("P12", "Update Diagnosis/Procedure");
            dic.Add("PC1", "PC/ problem add");
            dic.Add("PC2", "PC/ problem update");
            dic.Add("PC3", "PC/ problem delete");
            dic.Add("PC4", "PC/ problem query");
            dic.Add("PC5", "PC/ problem response");
            dic.Add("PC6", "PC/ goal add");
            dic.Add("PC7", "PC/ goal update");
            dic.Add("PC8", "PC/ goal delete");
            dic.Add("PC9", "PC/ goal query");
            dic.Add("PCA", "PC/ goal response");
            dic.Add("PCB", "PC/ pathway (problem-oriented) add");
            dic.Add("PCC", "PC/ pathway (problem-oriented) update");
            dic.Add("PCD", "PC/ pathway (problem-oriented) delete");
            dic.Add("PCE", "PC/ pathway (problem-oriented) query");
            dic.Add("PCF", "PC/ pathway (problem-oriented) query response");
            dic.Add("PCG", "PC/ pathway (goal-oriented) add");
            dic.Add("PCH", "PC/ pathway (goal-oriented) update");
            dic.Add("PCJ", "PC/ pathway (goal-oriented) delete");
            dic.Add("PCK", "PC/ pathway (goal-oriented) query");
            dic.Add("PCL", "PC/ pathway (goal-oriented) query response");
            dic.Add("Q01", "Query sent for immediate response");
            dic.Add("Q02", "Query sent for deferred response");
            dic.Add("Q03", "Deferred response to a query");
            dic.Add("Q05", "Unsolicited display update message");
            dic.Add("Q06", "Query for order status");
            dic.Add("Q11", "Query by parameter requesting an RSP segment pattern response");
            dic.Add("Q13", "Query by parameter requesting an RTB - tabular response");
            dic.Add("Q15", "Query by parameter requesting an RDY display response");
            dic.Add("Q16", "Create subscription");
            dic.Add("Q17", "Query for previous events");
            dic.Add("Q21", "Get person demographics");
            dic.Add("Q22", "Find candidates");
            dic.Add("Q23", "Get corresponding identifiers");
            dic.Add("Q24", "Allocate identifiers");
            dic.Add("Q25", "Personnel Information by Segment Query");
            dic.Add("Q26", "Pharmacy/treatment order response");
            dic.Add("Q27", "Pharmacy/treatment administration information");
            dic.Add("Q28", "Pharmacy/treatment dispense information");
            dic.Add("Q29", "Pharmacy/treatment encoded order information");
            dic.Add("Q30", "Pharmacy/treatment dose information");
            dic.Add("Q31", "Query Dispense history");
            dic.Add("R01", "Unsolicited transmission of an observation message");
            dic.Add("R02", "Query for results of observation");
            dic.Add("R04", "Response to query; transmission of requested observation");
            dic.Add("R21", "Unsolicited laboratory observation");
            dic.Add("R22", "Unsolicited Specimen Oriented Observation Message");
            dic.Add("R23", "Unsolicited Specimen Container Oriented Observation Message");
            dic.Add("R24", "Unsolicited Order Oriented Observation Message");
            dic.Add("R25", "Unsolicited Population/Location-Based Laboratory Observation Message");
            dic.Add("R30", "Unsolicited Point-Of-Care Observation Message Without Existing Order - Place An Order");
            dic.Add("R31", "Unsolicited New Point-Of-Care Observation Message - Search For An Order");
            dic.Add("R32", "Unsolicited Pre-Ordered Point-Of-Care Observation");
            dic.Add("ROR", "Pharmacy prescription order query response");
            dic.Add("S01", "Request new appointment booking");
            dic.Add("S02", "Request appointment rescheduling");
            dic.Add("S03", "Request appointment modification");
            dic.Add("S04", "Request appointment cancellation");
            dic.Add("S05", "Request appointment discontinuation");
            dic.Add("S06", "Request appointment deletion");
            dic.Add("S07", "Request addition of service/resource on appointment");
            dic.Add("S08", "Request modification of service/resource on appointment");
            dic.Add("S09", "Request cancellation of service/resource on appointment");
            dic.Add("S10", "Request discontinuation of service/resource on appointment");
            dic.Add("S11", "Request deletion of service/resource on appointment");
            dic.Add("S12", "Notification of new appointment booking");
            dic.Add("S13", "Notification of appointment rescheduling");
            dic.Add("S14", "Notification of appointment modification");
            dic.Add("S15", "Notification of appointment cancellation");
            dic.Add("S16", "Notification of appointment discontinuation");
            dic.Add("S17", "Notification of appointment deletion");
            dic.Add("S18", "Notification of addition of service/resource on appointment");
            dic.Add("S19", "Notification of modification of service/resource on appointment");
            dic.Add("S20", "Notification of cancellation of service/resource on appointment");
            dic.Add("S21", "Notification of discontinuation of service/resource on appointment");
            dic.Add("S22", "Notification of deletion of service/resource on appointment");
            dic.Add("S23", "Notification of blocked schedule time slot(s)");
            dic.Add("S24", "Notification of opened (unblocked) schedule time slot(s)");
            dic.Add("S25", "Schedule query message and response");
            dic.Add("S26", "Notification that patient did not show up for schedule appointment");
            dic.Add("S28", "Request new sterilization lot");
            dic.Add("S29", "Request Sterilization lot deletion");
            dic.Add("S30", "Request item");
            dic.Add("S31", "Request anti-microbial device data");
            dic.Add("S32", "Request anti-microbial device cycle data");
            dic.Add("S33", "Notification of sterilization configuration");
            dic.Add("S34", "Notification of sterilization lot");
            dic.Add("S35", "Notification of sterilization lot deletion");
            dic.Add("S36", "Notification of anti-microbial device data");
            dic.Add("S37", "Notification of anti-microbial device cycle data");
            dic.Add("T01", "Original document notification");
            dic.Add("T02", "Original document notification and content");
            dic.Add("T03", "Document status change notification");
            dic.Add("T04", "Document status change notification and content");
            dic.Add("T05", "Document addendum notification");
            dic.Add("T06", "Document addendum notification and content");
            dic.Add("T07", "Document edit notification");
            dic.Add("T08", "Document edit notification and content");
            dic.Add("T09", "Document replacement notification");
            dic.Add("T10", "Document replacement notification and content");
            dic.Add("T11", "Document cancel notification");
            dic.Add("T12", "Document query");
            dic.Add("U01", "Automated equipment status update");
            dic.Add("U02", "Automated equipment status request");
            dic.Add("U03", "Specimen status update");
            dic.Add("U04", "specimen status request");
            dic.Add("U05", "Automated equipment inventory update");
            dic.Add("U06", "Automated equipment inventory request");
            dic.Add("U07", "Automated equipment command");
            dic.Add("U08", "Automated equipment response");
            dic.Add("U09", "Automated equipment notification");
            dic.Add("U10", "Automated equipment test code settings update");
            dic.Add("U11", "Automated equipment test code settings request");
            dic.Add("U12", "Automated equipment log/service update");
            dic.Add("U13", "Automated equipment log/service request");
            dic.Add("V01", "Query for vaccination record");
            dic.Add("V02", "Response to vaccination query returning multiple PID matches");
            dic.Add("V03", "Vaccination record response");
            dic.Add("V04", "Unsolicited vaccination record update");
            dic.Add("W01", "Waveform result, unsolicited transmission of requested information");
            dic.Add("W02", "Waveform result, response to query");

            string returnValue = "";
            if (dic.TryGetValue(MsgType, out returnValue))
                return returnValue;
            else
                return "";
        }
    }
}
