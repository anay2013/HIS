using HIS.Repository;
using MediSoftTech_HIS.App_Start;
using MediSoftTech_HIS.Repository;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static HISWebApi.Models.IPDBO;

namespace MediSoftTech_HIS.Areas.IPD.Repository
{
    public class FormTemplateRepo:Controller
    {
        public string GetFormTemplate( string TemplateName, string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation,string FormHeader)
        {
            string TemplateContent = "";
            if (TemplateName =="AdmissionFrom")
            {
                 TemplateContent = AdmissionFormContent(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);

            }
            else if (TemplateName== "Undertaking for Corporate_TPA Patients")
            {
                TemplateContent = TPAPatientsFormContent(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "GeneralConsentforEntryHindi")
            {
                TemplateContent = GeneralConsentforEntryHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "GeneralConsentforEntryEnglish")
            {
                TemplateContent = GeneralConsentforEntryFormEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "DoctorOrderList")
            {
                TemplateContent = DoctorOrderListForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "DoctorOrderList2")
            {
                TemplateContent = DoctorOrderList2FormContent(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedAnesthesiaConsentHindi")
            {
                TemplateContent = InformedAnesthesiaConsentHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedAnesthesiaConsentEnglish")
            {
                TemplateContent = InformedAnesthesiaConsentEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "SurgicalSafetyChecked")
            {
                TemplateContent = SurgicalSafetyCheckedConsentFrom(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            // Single surgeryPrintFrom
            else if (TemplateName == "InformedConsentforSurgeryHindi")
           {
              TemplateContent = InformedforSurgeryHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
           }
            else if (TemplateName == "InformedConsentforSurgeryEnglish")
            {
                TemplateContent = InformedforSurgeryEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "NursesPreOperativeCheckList")
            {
                TemplateContent = NursesPreOperativeCheckListForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "AnesthesiaRecord")
            {
                TemplateContent = AnesthesiaRecordForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "PreInductionAssessment")
            {
                TemplateContent = PreInductionAssessmentForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "IntraOperativeMaintenance")
            {
                TemplateContent = IntraOperativeMaintenanceForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "PostAnesthesiaRecoveryRoomChart")
            {
                TemplateContent = PostAnesthesiaRecoveryRoomChartForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "OperationNotes")
            {
                TemplateContent = OperationNotesForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "AdditionalNotesIfRequired")
            {
                TemplateContent = AdditionalNotesIfRequiredForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            ///single Dialysis
            else if (TemplateName == "InformedConsentForHemoDialysisHindi")
            {
                   TemplateContent = InformedConsentForHemoDialysisHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedConsentForHemoDialysisEnglish")
            {
                TemplateContent = InformedConsentForHemoDialysisEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "HemoDialysisProforma")
            {
                TemplateContent = HemoDialysisProformaForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "DischargeSummary")
            {
                TemplateContent = DischargeSummaryForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            ///single Blood Print
            else if (TemplateName == "BloodRequisition")
            {
                TemplateContent = BloodRequisitionForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "BloodTransfusionNote")
            {
                TemplateContent = BloodTransfusionNoteForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "VitalSignsChart")
            {
                TemplateContent = VitalSignsChartForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "BloodTransfusionReactionReport")
            {
                TemplateContent = BloodTransfusionReactionReportForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "BlooadRelease")
            {
                TemplateContent = BlooadReleaseForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "DischrgeFromForBloodBank")
            {
                TemplateContent = DischrgeFromForBloodBankForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
           
            
            //CathLab
            else if (TemplateName == "InPatientHistoryAndPhysicalRecord")
            {
                TemplateContent = InPatientHistoryAndPhysicalRecordForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "MenstrualHistory")
            {
                TemplateContent = MenstrualHistoryForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "ProvisionalDiagnosis")
            {
                TemplateContent = ProvisionalDiagnosisForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "ReviewOfDocumentation")
            {
                TemplateContent = ReviewOfDocumentationForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "NursingInitalAssessmentFormForWard")
            {
                TemplateContent = NursingInitalAssessmentFormForWardForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "ConsultaionServices")
            {
                TemplateContent = ConsultaionServicesForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "NutritionalAssessmentScreeningForm")
            {
                TemplateContent = NutritionalAssessmentScreeningForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "NutritionalReAssessmentForm")
            {
                TemplateContent = NutritionalReAssessmentForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "NursingCarePlan")
            {
                TemplateContent = NursingCarePlanForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "NurseHandOverRecord")
            {
                TemplateContent = NurseHandOverRecordForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "NurseHandOverRecord")
            {
                TemplateContent = NurseHandOverRecordForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "IformedConsentCoronaryAngiographyHindi")
            {
                TemplateContent = IformedConsentCoronaryAngiographyHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "IformedConsentCoronaryAngiographyEnglish")
            {
                TemplateContent = IformedConsentCoronaryAngiographyEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "IformedConsentPTCAHindi")
            {
                TemplateContent = IformedConsentPTCAHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "IformedConsentPTCAEnglish")
            {
                TemplateContent = IformedConsentPTCAEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "HighRiskConsent")
            {
                TemplateContent = HighRiskConsentForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedConsentForImplantPlacementRemoval")
            {
                TemplateContent = InformedConsentForImplantPlacementRemovalForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedConsentForSpecialProcedureHindi")
            {
                TemplateContent = InformedConsentForSpecialProcedureHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedConsentForSpecialProcedure")
            {
                TemplateContent = InformedConsentForSpecialProcedureForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "CathLabchecklistPreOperative")
            {
                TemplateContent = CathLabchecklistPreOperativeForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "CathLabchecklistCathLab")
            {
                TemplateContent = CathLabchecklistCathLabForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "DailyProgressInformationReport")
            {
                TemplateContent = DailyProgressInformationReportForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedICUAdmissionConsent")
            {
                TemplateContent = InformedICUAdmissionConsentForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "PatientTranferRecordSecond")
            {
                TemplateContent = PatientTranferRecordSecondForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "PatientTranferRecordFrist")
            {
                TemplateContent = PatientTranferRecordFristFrom(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "CathLabProcedureNotes")
            {
                TemplateContent = CathLabProcedureNotesForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            // print form whatsapp
            else if (TemplateName == "InformedConsentForChemotherapyHindi")
            {
                TemplateContent = InformedConsentForChemotherapyHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedConsentForChemotherapyEnglish")
            {
                TemplateContent = InformedConsentForChemotherapyEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "IntraUterineDevicesConsent")
            {
                TemplateContent = IntraUterineDevicesConsentForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "ConsentForRestraintsEnglish")
            {
                TemplateContent = ConsentForRestraintsEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "ConsentForRestraintsHindi")
            {
                TemplateContent = ConsentForRestraintsHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedConsentForLeaveAgainstMedicalAdviceEnglish")
            {
                TemplateContent = InformedConsentForLeaveAgainstMedicalAdviceEnglishForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedConsentForLeaveAgainstMedicalAdviceHindi")
            {
                TemplateContent = InformedConsentForLeaveAgainstMedicalAdviceHindiForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "RequisitionFormForSpecimen")
            {
                TemplateContent = RequisitionFormForSpecimenForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "RequisitionFormForTissueBiopsy")
            {
                TemplateContent = RequisitionFormForTissueBiopsyForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "WellsModelForPredictingDVTProbability")
            {
                TemplateContent = WellsModelForPredictingDVTProbabilityForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "EmptyBloodBagReturnTransfusion")
            {
                TemplateContent = EmptyBloodBagReturnTransfusionForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "InformedConsentForBloodTranfusion")
            {
                TemplateContent = InformedConsentForBloodTranfusionForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }
            else if (TemplateName == "NotifiedConsentFormforBloodTransfusion")
            {
                TemplateContent = NotifiedConsentFormforBloodTransfusionForm(pname, uhidno, gender, admitdate, ipdno, doctor, Diagnosis, PageIndex, PageName, PageOrientation);
            }

            
            return TemplateContent;
        }
        // duplicate pattern
        public string AdmissionFormContent(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            StringBuilder body = new StringBuilder();
            
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");

            body.Append("<table style='width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; margin-top:10px; '>");
            body.Append("<tr>");
            body.Append("<td><b>DATE OF ADMISSION:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + admitdate + "</td>");
            body.Append("<td><b>TIME OF ADMISSION:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>UHID.No:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No:</b></td>");
            body.Append("<td></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");

            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>PATIENT NAME</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + pname.ToString() + "</td>");
            body.Append("<td><b>AGE/Gender:</b></td>");
            body.Append("<td></td>");
            body.Append("<td>" + gender.ToString() + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>FATHER/HUSBAND NAME:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>NAME OF ATTENDANT:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td><b>CONTACT NUMBER :</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>ALTERNATE CONTACT NUMBER</b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>ADDRESS</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>WARD :</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>ROOM NUMBER</b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>CONSULTANT NAME :</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");


            body.Append("<div class='row' style='text-align:justify; margin-top:10px'> I have understood clearly all the policies and protocols of the hospital and received the in- patient information booklet. I have been educated about patient rights and responsibilities.</div>");
            body.Append("<div class='row' style='text-align:justify'><p> मैंने अस्पताल की सभी नीतियाँ और प्रोटोकॉल को स्पष्ट रूप से समझा है और मुझे इन पेशेंट सूचना पुस्तिका प्राप्त हुई है। मुझे रोगी अधिकारों और जिम्मेदारियों के बारे में शिक्षित किया गया है। </p></div><br>");
            body.Append("<div class='row' style='text-align:justify'><p> Signature of Patient/Attendant  <lable style='margin-left:240px;'>Signature of Admitting Officer</lable><br> रोगी / परिचारक का हस्ताक्षर <lable style='margin-left:240px;'>प्रवेश करने वाले अधिकारी का हस्ताक्षर</lable></p></div>");
            body.Append("<div class='row' style='text-align:justify'><h5>DISCHARGE / DEATH DATE / TIME:_________________________________________________________ </h5></div>");
            body.Append("<div class='row' style='text-align:justify'><h5>TYPE OF DISCHARGE:_______________________________________________(NORMAL/LAMA/ABSCONDED)</h5></div>");
            body.Append("<div class='row' style='text-align:justify'><h5>RECEIVED IN MEDICAL RECORDS DATE/TIME:__________________________________________________</h5></div><br><br><br>");
            body.Append("<br>");
          
            body.Append("</div>");

            return body.ToString();

        }
        public string TPAPatientsFormContent(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            StringBuilder body = new StringBuilder();
          
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-70px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<h3 style='color:black;text-align:center;text-align: justify;margin-left:200px'>Undertaking for Corporate/TPA Patients</h3>");
            body.Append("<div class='row' style='text-align: justify'><p>I Mr/Ms.______________________________hereby authorize Chandan Hospital, to charge me the full amount incurred on the treatment in the event of denial by the third party/insurance upon presentation of cashless document, and the same shall be payable by me, if there is delay in authorization, partial authorization or denial of authorization from TPA/Insurance, Hospital is not responsible. </p></div>");
            body.Append("<div class='row'  style='text-align: justify'><p>I am availing__________room for Rs._______________________per day and undertake to pay all the charges on account of difference in room category. I understand that all other charges are also proportionately higher in this category and I shall pay the same before the surgery.Further I undertake to pay all the charges which were not payable by the TPA / Insurance / Corp.i.e.non - medical items, non - payable treatment and investigations, implant cost differential for which the TPA / Insurance / Corporate was not given authorization.The same shall also not be claimed by me for reimbursement from the TPA / Insurance / Corporate. <div class='row' style='margin-top:-10px;'>I hereby authorize Chandan Hospital to keep all my original reports with films for the purpose of the claim.I shall</div> be requesting the TPA not the hospital for the films and original reports if required for further treatment. I undertake to pay the shortfall in the event of the final bill amount is not fully paid by the insurance company/ TPA / Corporate under the cashless / credit arrangement.</p></div>");
            body.Append("<h3 style='color:black;text-align:center;text-align: justify;margin-left:200px'>कॉर्पोरेट / TPA रोगियों के लिए अंडरटेकिंग</h3>");
            body.Append("<div class='row' style='text-align: justify'><p>मैं श्री / सुश्री ।__________________________________चंदन अस्पताल को अधिकृत करें, मुझे कॅशलेस दस्तावेज़ की प्रस्तुति पर तीसरे पक्ष / बीमा द्वारा इनकार करने की स्थिति में इलाज पर पूरी राशि का भुगतान करने के लिए, मेरे द्वारा भुगतान किया जाएगा। यदि प्राधिकरण में देरी होती है, आंशिक प्राधिकरण या टीपीए / बीमा से प्राधिकरण से इनकार, अस्पताल जिम्मेदार नहीं है। </p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>मै___________________कक्ष का लाभ उठा रहा है और Rs.__________________________दिन और कमरे की श्रेणी में अंतर के कारण सभी शुल्कों का भुगतान करने का उपक्रम करूंगा। मैं समझता हूं कि अन्य सभी शुल्क भी इस श्रेणी में आनुपातिक रूप से अधिक हैं और मैं सर्जरी से पहले ही भुगतान करूंगा। इसके अलावा, मैं उन सभी शुल्कों का भुगतान करने का वचन देता हूं जो टीपीए / बीमा / कॉर्प द्वारा भुगतान नहीं किए गए थे यानी गैर-चिकित्सा आइटम, गैर-देय उपचार और जांच, प्रत्यारोपण लागत अंतर जिसके लिए टीपीए / बीमा / कॉर्पोरेट को प्राधिकरण नहीं दिया गया था। टीपीए / बीमा / कॉर्पोरेट से प्रतिपूर्ति के लिए भी मेरे द्वारा दावा नहीं किया जाएगा । <div class='row' style='margin-top:-10px;'> मैं इस दावे के उद्देश्य से फिल्मों के साथ अपनी सभी मूल रिपोर्ट रखने के लिए चंदन अस्पताल को अधिकृत करता हूं। मैं टीपीए से फिल्मों और मूल रिपोर्टों के लिए अस्पताल का अनुरोध नहीं करूंगा यदि आगे उपचार की आवश्यकता हो ।</div> <div class='row' style='margin-top:-2px;'> मैं अंतिम बिल राशि के भुगतान में कमी का भुगतान करने का बीड़ा उठाता हूं। कैशलेस / क्रेडिट व्यवस्था के तहत बीमा कंपनी / टीपीए / कॉर्पोरेट द्वारा पूरी तरह से भुगतान नहीं किया जाता है </div><p></div>");
            body.Append("<h5 style='margin-right:100px;text-align: justify'> Signature of Patient/Relative  <lable style='margin-left:200px;'>Signature of Admitting Officer</lable><br>Name: <lable style='margin-left:322px;'>Name:</lable><br>RealationShip:<br>Date:</h5><br>");

        
            body.Append("</div>");
            return body.ToString();
        }
        public string GeneralConsentforEntryHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
           
            StringBuilder body = new StringBuilder();
            
            //body.Append("<div class='row' style='padding:50px'>");
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-70px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<table style='width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
            body.Append("<tr>");
            body.Append("<td><b>नामः</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='font-size:15px;'>" + pname + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>आयु /लिंग</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td> " + gender + " </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>दिनांकः </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='font-size:15px;'> " + admitdate + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<tr>");
            body.Append("<td><b>S/D/W of:</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='font-size:15px;'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>IP No:</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td> " + ipdno + " </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>UHID: </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='font-size:15px;'> " + uhidno + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<h3 style='color:black;text-align:center;text-align: justify;margin-left:200px'>प्रवेश के लिए सामान्य सहमति</h3>");
            body.Append("<div class='row' style='text-align: justify'><p>• मैं इस अस्पताल में प्रवेश और उपचार के लिए अपनी पूर्ण सहमति देता हूँ|</p><p>•मैं अस्पताल के कर्मचारियों को संबंधित देखभाल प्रदान करने के लिए सहमति देता हूं और उपचार के लिए अधिकृत करता हूँ और उपचार के लिए चिकित्सक दल द्वारा आवश्यक समझा जाता है, जिसमें जरूरत पड़ने पर मुझे मेरे मरीज को अन्य / उच्च अस्पताल में रेफर करने सहित क्रॉस परामर्श और संदर्भ प्राप्त करना शामिल है|</p><p></p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• मुझे उपचार करने वाले डॉक्टर / टीम द्वारा सलाह के अनुसार आवश्यक दवाएं, दवाएं, अंतःशिरा तरल पदार्थ दिए जाने की भी सहमति है|</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• मैं सहायक डॉक्टरों जैसे अन्य डॉक्टरों (पीजी प्रशिक्षु नर्सों और अस्पताल द्वारा अन्य स्वास्थ्य कर्मियों सहित और डॉक्टर / टीम का इलाज करने वाले) का उपयोग करने के लिए भी सहमति देता हूं|</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• मुझे प्रस्तावित देखभाल योजना, अपेक्षित परिणाम, संभावित परिणाम, उपचार / अस्पताल में रहने की अपेक्षित लागत के बारे में समझाया गया है|</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• मैं समझता हूं कि अस्पताल मेरा / मेरे मरीज का ध्यान रखेगा, लेकिन, हमेशा अप्रत्याशित जटिलताओं की संभावना है जो लंबे समय तक रह सकती है और / या गहन देखभाल सेवाओं का उपयोग कर सकती है। ऐसे मामलों में, उन चिंतनशील और अन्य हस्तक्षेपों से अलग प्रक्रिया कभी-कभी आवश्यक हो सकती है|</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• घोषणा करता हूं कि मेरे पास मेरे चिकित्सा इतिहास के डॉक्टर को सूचित करेंगे, जिसमें पिछली बीमारी, एलजी, दवा की प्रतिक्रिया, शल्य प्रक्रिया, प्रासंगिक चिकित्सा परिवार का इतिहास और मेरे उपचार से संबंधित अन्य सभी तथ्य शामिल हैं| </p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• मुझे अस्पताल के नियमों और विनियमों के बारे में अवगत कराया गया है, जिनमें सुरक्षा शामिल है और मैं उनका पालन करने का वादा करता हूं। मुझे सूचित किया गया है कि अस्पताल में भर्ती के दौरान मेरे पास रखी नकदी, आभूषण या अन्य सामान पूरी तरह से मेरे जोखिम में होंगे और किसी भी नुकसान या चौरी के लिए अस्पताल को जिम्मेदार नहीं ठहराएगा|  </p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• मैं अपने उपचार विवरण / चिकित्सा, वैज्ञानिक या शैक्षिक उद्देश्यों (शिक्षण, अनुसंधान और शिक्षाविदों) के लिए मेरे उपचार विवरण / चिकित्सा रिकॉर्ड के उपयोग और / या प्रकाशन के लिए भी सहमत हूं और चित्र या वर्णनात्मक ग्रंथ प्रदान करता हूं, जो उनके साथ मेरी पहचान को प्रकट नहीं करते हैं|  </p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• मैं स्वास्थ्य अधिकारियों, पुलिस, टीपीए / बीमा कंपनियों को अपने स्वास्थ्य / रोग की स्थिति के बारे में जानकारी जारी करने . के लिए भी सहमत हूँ |</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• मैं यह घोषणा करता हूं कि मैं छुट्टी के समय अस्पताल परिसर छोड़ने से पहले बिल को निपटाने की पूरी जिम्मेदारी लेता हूँ| सहमति पत्र मेरे भाषा में मेरे लिए प्रकाशित और प्रकाशित किया गया है। मैंने पूरी तरह से सहमति के कार्यान्वयन को  पूरा किया है | </p></div>");
            body.Append("<div style='margin-right:100px;text-align: justify'> <lable style='margin-left:295px;'></lable><br>हस्ताक्षर: <br>नामः<lable style='margin-left:270px;'>रोगी / माता-पिता / अभिभावक का हस्ताक्षर: </lable><br><lable style='margin-left:300px;'>रोगी / माता-पिता / अभिभावक का नाम:  </lable><br><lable style='margin-left:300px;'>भिभावक के मामले में, रोगी से संबंध:  </lable><br><lable style='margin-left:300px;'> समय:  </lable><br>दिनांक:</div>");
           
            body.Append("</div>");
         
            return body.ToString();

        }
        public string GeneralConsentforEntryFormEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            StringBuilder body = new StringBuilder();
            
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-70px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("</h5>");
            body.Append("<table style='width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
            body.Append("<tr>");
            body.Append("<td><b>Name</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='font-size:15px;'>" + pname + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Age/Sex</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td> " + gender + " </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Date </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='font-size:15px;'> " + admitdate + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<tr>");
            body.Append("<td><b>S/D/W of:</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='font-size:15px;'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>IP No:</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td> " + ipdno + " </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>UHID: </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='font-size:15px;'> " + uhidno + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<h4 style='color:black;text-align:center;text-align: justify;margin-left:200px'>GENERAL CONSENT FOR ADMISSION</h4>");
            body.Append("<div class='row' style='text-align: justify'><p>• I give my full consent and authorization for admission and treatment at this hospital.</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I consent and authorize the hospital staff to provide relevant care and to conduct diagnostic as deemed necessary by the treating doctor team including obtaining cross consultations and references including referring me / my patient to other / higher hospital in case of need arises.</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I also consent to be administered necessary drugs, medications, intravenous fluids, as advised by the treating doctor/team.</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I also consent to use assistants such as resident doctors, other doctors, (including PG trainee nurses & other healthcare workers by the hospital and treating doctor / team .) </ p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I have been explained about the proposed care plan, expected results, possible outcomes, expected costs of treatment/hospital stay  </ p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I understand that the hospital will take care of me/my patient but, that there is always a possibility of an unexpected complications which may necessitate longer and stay and/or use of intensive care services. In such cases, procedure different from those contemplated and other interventions may sometimes be needed.   </ p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I declare that, I have and will inform the doctor of my medical history including previous illness, allergies, drug reactions, surgical procedure, relevant medical family history and all other facts relevant to my treatment.  </ p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I have been made aware of the rules and regulations of the hospital including those to the security and I promise to abide by them.I have been informed that cash, jewellery or other valuable kept with me during hospitalization will be completely at my risk and shall not hold the hospital responsible for any loss or theft.   </ p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I also consent and agree to use and/or publication of my treatment details/medical record for medical, scientific or educational purposes (teaching, research and academics) provided the pictures or the descriptive texts accompanying them do not reveal my identity.   </ p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I also consent to release information about my health/disease status to the health authorities, police, TPA/insurance companies. </ p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>• I declare that I take full responsibility of settling the bill before leaving hospital premises at the time of discharge. </ p></div>");
            body.Append("<h5 style='text-align: justify'><p>• I CERTIFY THAT THE STATEMENT MADE IN THE ABOVE CONSENT FORM HAVE BEEN READ AND EXPLAINED TO ME IN THE LANGUAGE THAT I UNDERSTAND AND I HAVE FULLY UNDERSTOOD THE IMPLICATIONS OF THE CONSENT. </ P></h5>");
            body.Append("<div style='text-align: justify'>Consent taken by : <br>Signature :<br>Name :<br>Date : </div>");
            body.Append("<div style='text-align: justify;margin-top:-80px'><lable style='margin-left:300px;'>Signature of patient/parent/guardian: </lable><br><lable style='margin-left:300px;'>Name of patient/parent/guardian:  </lable><br><lable style='margin-left:300px;'>In case of guardian, relationship to patient:  </lable><br><lable style='margin-left:300px;'>Time  </lable></div>");
     
            body.Append("</div>");
            return body.ToString();
        }
        public string DoctorOrderListForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");

            StringBuilder body = new StringBuilder();
           
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-70px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<center>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid';margin-top:-100px>");

            body.Append("<b>DOCTOR ORDER SHEET</b>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80px'><center><h3><b style='font-size:12px;padding:10px;'>DATE & TIME</b><h3></cenetr></td>");
            body.Append("<td><center style='margin-left:50px;'><h3><b>DOCTOR NOTES :</b></h3>(ASSESSMENT & REASSESSMENT)</center></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80px'><center><h3><b style='font-size:12px;padding:10px'>NAME & SIGN</b><h3></center></td>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:800px;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'></th>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</center>");
            body.Append("</div>");
            
            return body.ToString();
        }
        public string DoctorOrderList2FormContent(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");

            PdfGenerator pdfConverter = new PdfGenerator();
            string _result = string.Empty;
            StringBuilder body = new StringBuilder();
            StringBuilder h = new StringBuilder();
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-70px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<center>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid';margin-top:-100px>");

            body.Append("<b>DOCTOR ORDER SHEET</b>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80px'><center><h3><b style='font-size:12px;padding:10px;'>DATE & TIME</b><h3></cenetr></td>");
            body.Append("<td><center style='margin-left:50px;'><h3><b>DOCTOR NOTES :</b></h3>(ASSESSMENT & REASSESSMENT)</center></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80px'><center><h3><b style='font-size:12px;padding:10px'>NAME & SIGN</b><h3></center></td>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:800px;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'></th>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</center>");
            
            body.Append("</div>");
          
            return body.ToString();
        }
        public string InformedAnesthesiaConsentHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
         
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:180px; height:70px;></h3></div>");

            body.Append("<div class='row'><h3 style='color:black;text-align:center;border:1px solid;margin-left:250px;margin-right:225px;border-radius: 2px;padding:3px;width:300px;margin-top:-2%;'>शारीरिक अचेतना का सहमति पत्र</h3></div>");

            body.Append("<div class='row' style='text-align: justify; margin-top:-2%;font-size:14px'><b>दिनांक:......................</b> </div>");
            body.Append("<div class='row' style='text-align: justify; width:100%;'><lable>-----------------------------------------------------------------------------------------------------------------------------</lable></div>");

            body.Append("<table style='width:100%;margin-top:-1%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>रोगी का नाम</b></td>");
            body.Append("<td>:</td>");
            body.Append("<b><td>" + pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>उम्र/लिंग</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>भर्ती की दिनांक</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>यू०एच०आई०डी०नं०</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>चिकित्सक</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>निदान</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='text-align: justify; width:100%;'><lable>-----------------------------------------------------------------------------------------------------------------------------</lable></div>");
            body.Append("<div class='row' style='width:100%; font-size:14px'>");
            body.Append("<div class='row' style='text-align: justify; '>  इस पत्र के द्वारा में पदनामित  व्यक्ति को जो निष्चेतना विभाग से सम्बन्धित है को सामान्य अचेतन / क्षेत्रीय अचेतना / स्थानी अचेतना करने  के लिए अधिकृत करता / करती हूँ जिसे कि वो अपने विचार से ...................................................................................... पद्धति के लिए सही मानते हो उसका प्रयोग कर सकते हैं।</div>");
            body.Append("<br/>");
            body.Append("<div class='row' style='text-align: justify;'><b>विकल्प / लाभ / सम्भावित  जटिलताऐ :- </b> मैं यह समझता /समझती हूँ कि अचेतना का प्रयोग या अन्त: नाडी से दवाई को भेजना तोर पर कुछ निर्णय लेने के समन्वय को कम कर सकता है. ध्यान देने लगाने में कमी आ सकती है. उलटी  लगना / सर दर्द, गला सूखना, स्वर भंग होना, बदन में दर्द होगा, सभी सावधानियों एवं देखरेख के बाद भी हो सकता है इसकी भी कभी-कभी स्थिति बन  जाती  है मै  यह भी समझता हूँ कि बहुत अच्छे प्रयास होने के बाद भी दाँत का हिलना / जोड़ो सम्बन्धी बीमारी, कभी दाँत का गिरना, सभी बातों को जानते समझते हुए मै  अपनी सहमति उस अचेतना करने की देता / देती हूँ जो मेरे लिए आवश्यक है, और मेरे चिकित्सक के दृष्टिकोण से सही है। मुझे मेरी भाषा में इसके लाभ हानि / जटिलताओं को भली प्रकार से मुझे समझाया / बताया गया है सभी कुछ जानकार में अचेतना को प्रयोग करने के लिए सहमति देता / देती हूँ। सभी विकल्पों को अचेतना के प्रकार को मुझे मेरी भाषा में समझाया गया है।</div>");
            body.Append("<div class='row' style=''><h4>सम्भावित जोखिम और जटिलताएं:-</h4></div>");
            body.Append("<div class='row' style='text-align: justify;'>रोगी के हस्ताक्षर : ................................................ तिथिः ................................................. समय ............................................</div>");
            body.Append("<div class='row' style='text-align: justify;'>प्रतिनिधि के हस्ताक्षर: ............................................................................ नामः .......................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;'>सम्बन्ध: ................................................................................................ समय / तिथि ............................................................</div>");
            body.Append("<h3 style='color:black;text-align:center;border:1px solid;margin-left:225px;margin-right:225px;border-radius: 2px;padding:3px;width:250px'>सहमति के लिए गवाह</h1>");
            body.Append("<div class='row' style='text-align: justify;'> मैं..............................................................(गवाह का नाम)....................................................(रोगी से सम्बन्ध) का इस पते से यह निश्चित करता/करती हूँ कि उपरोक्त सहमति रोगी / प्रतिनिधि द्वारा मेरी उपस्थित में दी गयी। </div>");
            body.Append("<div class='row' style='text-align: justify;'> गवाह पर हस्ताक्षर ..............................................................दिनांक....................................................समय ............................ रोगी का सम्बन्धी/ प्रतिनिधि गवाह पर हस्ताक्षर कर सकता है (जब कोई सम्बन्धी या प्रतिनिधि न हो उस स्थिति में स्टाफ का सदस्य भी हस्ताक्षर कर सकता है )</div>");
            body.Append("<h3 style='color:black;text-align:center;border:1px solid;margin-left:225px;margin-right:225px;border-radius: 2px;padding:3px;width:250px'>चिकित्सक के द्वारा घोषणा</h1>");
            body.Append("<div class='row' style='text-align: justify;'> मैं.................................................................................................. (अचेतना करने वाले चिकित्सक का नाम) इस पत्र के द्वारा यह घोषणा करता/करती हूँ। रोगी को अवचेतना के विषय में सब कुछ उसकी भाषा में समझाया जा चुका है| </div>");
            body.Append("<div class='row' style='text-align: justify;'>अचेतना करने वाले चिकित्सक के हस्ताक्षर: ..............................................  दिनांक : .......................... समय: ............................</ div>");
           
            body.Append("</div>");
            return body.ToString();
        }
        public string InformedAnesthesiaConsentEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            
            StringBuilder body = new StringBuilder();

            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:80px;></h3></div>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:225px;margin-right:225px;margin-top:-3%; width:300px'>INFORMED ANESTHESIA CONSENT</h4></div>");

            body.Append("<div class='row' style='100%; margin-left:5px;font-size:14px'><b>Date:......................</b></div>");
            body.Append("<table style='width:100%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td>:</td>");
            body.Append("<b><td>" + pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>DOA</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>UHID NO</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='font-size:14px'>");
            body.Append("<div class='row' style='text-align: justify;margin-top:8px;'> I hereby authorize the designated personnel from the department of Anesthesiology to administer General/Regional/ Local anesthesia, as deemed appropriate in his/her opinion, for the procedure.............................................................................. which is necessary to treat my condition. Alternative benefit & possible complications: I understand that the use of anesthesia or placement of intra- vascular lines for invasive monitoring may despite all appropriate measures to prevent, pose various risks including but not limited to temporary impairment of judgment and motor coordination, temporary. decrease in attention span: nausea and/or vomiting, headache, sore throat, hoarseness of voice, muscle aches, despite all precautions and care a rare risk of reaction to medications that may result in severe and irreversible injury including death in exceptional cases. I do understand that despite best efforts, occasionally teeth/prosthesis may become loose and get damaged. Having understood occurrence of such complications I do give my consent to the use of such anesthetics as may be considered necessary and safe in the opinion of physician responsible for providing me these services. Further, I have been explained in my own language the intended benefits, possible complication and available alternative to the said type of anesthesia.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:8px;'>Possible Risks and complications: ........................................................................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:8px;'>Signature of Patient: ........................................................ Date: ........................................... Time: .....................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:8px;'>Signature of surrogate: ...................................................................... Name: .......................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:8px;'>Relation: ........................................................................................................... Date/Time: ................................................</div>");
            body.Append("<h4 style='color:black;text-align:center;margin-left:225px;margin-right:225px'>WITNESS FOR THE CONSENT</h4>");
            body.Append("<div class='row' style='text-align: justify'> I..............................................................(Name of witness)..........................................................(Relation with patient) of the patient hereby confirm that the above consent has been given by the patient/surrogate in my presence. </div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:8px;'>Signature of witness: ....................................................................... Date ................................Time .................................(A relative / attendant of the patient to sign as witness)(Staff may sign as witness only when no relative is available)</ div>");

            body.Append("<h4 style='color:black;text-align:center;solid;margin-left:225px;margin-right:225px'>DECLARATION BY DOCTOR</h4>");
            body.Append("<div class='row' style=''> I,...................................................................................................................... (Name of the anesthesiologist) hereby, state that the patient has been explained about the implications of the anesthesia in the vernacular language. </div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:8px;'>Signature of anesthesiologist: ....................................................... Date : .................................. Time: ...............................</ div>");

        
            body.Append("</div>");
            return body.ToString();
      
        }
        public string SurgicalSafetyCheckedConsentFrom(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            
            StringBuilder body = new StringBuilder();

            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-2px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:600px;margin-right:100px;text-align: justify ; margin-top:-72px; width:500px;height:55px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:14px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender : </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-20px'>SURGICAL SAFETY CHECKLIST</h4></div>");

            body.Append("<h5 style='width:33%;font-size:12px'>");

            body.Append("<table style='width:99%;font-size:12px;margin-top:-7%'>");
            // First row

            body.Append("<tr>");
            body.Append("<table style='width:99%;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<th style='background-color:black; color:white'>SIGN IN: Before induction of anesthesia</th>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align: center;'><b>PATIENT HAS CONFIRMED</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:40px'>");
            body.Append("<tr>");
            body.Append("<td><b>•</b></td>");
            body.Append("<td>IDENTITY</td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>•</b></td>");
            body.Append("<td>SITE</td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>•</b></td>");
            body.Append("<td>PROCEDURE</td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>•</b></td>");
            body.Append("<td>CONSENT</td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:40px'>");
            body.Append("<tr>");
            body.Append("<td><b>SITE MARKED/NOT APPLICABLE</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:40px'>");
            body.Append("<tr>");
            body.Append("<td><b>ANAESTHESIA SAFETY CHECK COMPLETED</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp; </b></td>");
            body.Append("<td><b>&nbsp; </b></td>");
            body.Append("<td><b>PULSE OXIMETER ON PATIENT AND FUNCTIONING  </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:40px'>");
            body.Append("<tr>");
            body.Append("<td><b>DOES PATIENT HAVE A:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:40px'>");
            body.Append("<tr>");
            body.Append("<td><b>KNOWN ALLERGY?</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");

            body.Append("<td><b>No</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Yes</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:40px'>");
            body.Append("<tr>");
            body.Append("<td ><b>DIFFICULT AIRWAY/ASPIRATION RISK? </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>YES, AND EQUIPMENT/ASSISTANCE AVAILABLE</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:40px'>");
            body.Append("<tr>");
            body.Append("<td ><b>RISK OF >500ML BLOOD LOSS (7ML/KG IN CHILDREN)?</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>YES, AND ADEQUATE INTRAVENOUS ACCESS</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:20px'>");
            body.Append("<tr>");
            body.Append("<td >AND FLUIDS PLANNED</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-top:16%;'>");
            body.Append("<tr>");
            body.Append("<td><b>NAME OF ANESTHETIST</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");

            body.Append("<td><b>SIGNATURE:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");

            body.Append("<td><b>DATE / TIME:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</tr>");


            // Second column by

            body.Append("<tr>");
            body.Append("<table style='width:99%;font-size:14px;margin-left:100%;margin-top:-155%'>");
            body.Append("<tr>");
            body.Append("<th style='background-color:black; color:white'>TIME OUT: Before skin incision</th>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align: center;'><b>CONFIRM ALL TEAM MEMBERS HAVE</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b> INTRODUCED THEMSELVES BY NAME AND ROLE</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>SURGEON, ANAESTHESIA PROFESSIONAL </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>AND NURSE VERBALLY CONFIRM</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;margin-left:450px'>");
            body.Append("<tr>");
            body.Append("<td><b>•</b></td>");
            body.Append("<td>PATIENT</td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>•</b></td>");
            body.Append("<td>SITE</td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>•</b></td>");
            body.Append("<td>PROCEDURE</td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>ANTICIPATED CRITICAL EVENTS</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>SURGEON REVIEWS:</b> WHAT ARE THE </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td> CRITICAL OR UNEXPECTED STEPS,OPERATIVE </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>DURATION, ANTICIPATED BLOOD LOSS? </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>ANAESTHESIA TEAM REVIEWS:</b>  ARE THERE </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td> ANY PATIENT-SPECIFIC CONCERNS? </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>NURSING TEAM REVIEWS:</b> HAS STERILITY </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td> (INCLUDING INDICATOR RESULTS) BEEN CONFIRMED?  </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td> ARE THERE EQUIPMENT ISSUES OR ANY CONCERNS? </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>HAS ANTIBIOTIC PROPHYLAXIS BEEN </b> </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:452px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b> GIVEN WITHIN THE LAST 60 MINUTES?</b> </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:482px'>");
            body.Append("<tr>");
            body.Append("<td><b>Yes</b> </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>NOT APPLICABLE</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>IS ESSENTIAL IMAGING DISPLAYED ?</b> </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Yes</b> </td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");

            body.Append("<td>NOT APPLICABLE</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:430px;'>");
            body.Append("<tr>");
            body.Append("<td><b>NAME OF ANESTHETIST</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>SIGNATURE:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>DATE / TIME:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("</tr>");
            body.Append("</table>");
            // third column by

            body.Append("<tr>");
            body.Append("<table style='width:99%;font-size:14px;margin-left:200%;margin-top:-156%'>");
            body.Append("<tr>");
            body.Append("<th style='background-color:black; color:white'>SIGN OUT: Before patient leaves operating room </th>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align: center'><b>NURSE VERBALLY CONFIRMS WITH THE </ b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:822px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>TEAM</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:849px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>THE NAME OF THE PROCEDURE </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:824px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>RECORDED</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:852px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>THAT INSTRUMENT, SPONGE AND </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:824px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>NEEDLE COUNTS ARE CORRECT </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:824px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>(OR NOT APPLICABLE) </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:852px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>HOW THE SPECIMEN IS LABELLED </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:824px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(INCLUDING PATIENT NAME)</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:852px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>WHETHER THERE ARE ANY EQUIPMENT </b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:824px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>PROBLEMS TO BE ADDRESSED</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:825px'>");
            body.Append("<tr>");
            body.Append("<td>__________________________________________________________________</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:850px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>SURGEON, ANAESTHESIA PROFESSIONAL</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:850px'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>AND NURSE REVIEW THE KEY CONCERNS</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:825px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>FOR RECOVERY AND MANAGEMENT OF THIS PATIENT</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-left:850px; margin-top:51%;'>");
            body.Append("<tr>");
            body.Append("<td><b>NAME OF ANESTHETIST</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");

            body.Append("<td><b>SIGNATURE:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");

            body.Append("<td><b>DATE / TIME:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</table>");
            body.Append("</h5>");

            return body.ToString();
        }
       
        
        // single  surgery Print From
        public string InformedforSurgeryHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();

            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='"+ imagePath + "' style='width:150px; height:80px;></h3></div>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center; margin-top:-3%; margin-left:200; margin-right:200'>सर्जरी हेतु सूचित अभिसूचित सहमतिप्रपत्र </h3></div>");
            body.Append("<div class='row' style='text-align: justify; float:right;margin-right:50;margin-top:-10px'><lable> <b>दिनांक :...............</b></lable></div>");

            body.Append("<table style='width:100%; margin-top:25px; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>रोगी का नाम </b></td>");
            body.Append("<b><td>" + pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आयु/लिंग</b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>यू०एच०आई०डी०नं०</b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:100%; font-size:14px'>");
            body.Append("<tr>");

            body.Append("<td><b>भर्ती होने का तिथि</b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>परामर्शदाताचिकित्सक</b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:100%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>निदान :</b></td>");
            body.Append("<td><b>" + Diagnosis + "</b></td>");

            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='margin-top:20px;font-size:14px'>");
            body.Append("<div class='row' style='text-align: justify;'>मेरी /मेरे रोगी की स्थति  की दृष्टि गत  रखतेहुए उपचार हेतु  अधिकृत चिकित्सक डा. ....................................................................तथा</div>");
            body.Append("<div class='row' style='text-align: justify;'>उनकी टीम द्रवारा  बताई  प्रक्रिया  और सर्जरी  करने क लिए अधिकृत  कर दिया है</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'><lable><b> प्रक्रिया का नाम</b> &nbsp;&nbsp;.......................................</lable> </div>");
            body.Append("<div class='row' style='text-align: justify;'>इस सहमति प्रपत्र के विषय मै मुझे स्पष्ट करके समझा दिया गया है जिसे मै पूरी तरह से समझ गया / गई हूँ।</div>");
            body.Append("<div class='row' style='text-align: justify;'> जो सूचनांए मुझे दी गयी है ,उनसे मै अवगत हो गया/गई  हूँ |</p></div>");
            body.Append("<div class='row' style='text-align: justify; margin-top:1%'><p> <b>जटिलताएँ:-</b>जानकारी  के आधार  पर  अधिकृत किया गया है कि किसी  भी  प्रकिया  संक्रमण ,  रक्त बहव ,तंत्रिका  धाव ,रक्त  के थक्के ,ह्र्दयघा ,न्यूनतम स्थति में  मृत्यु  और प्रतिक्रिया सम्भावित  है , यह स्थति घातक और गम्भीर  भी  हो सकती है |</p></div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'><b>विकल्प ,लाभ  और  जटिलताएं:- </b>वर्तमान प्रक्रिया तथा ऑपरेशन के अतिरिक्त  उपलब्ध   विकल्प सम्भावित  जटिलता और ऐचिछ्क   लाभ  क विषय  में  मेरी भाषा में मुझे बता दिया गया है। इस बात से अवगत हूँ कि किसी भी  ऑपरेशन  तथा  प्रक्रिया का परिणाम अलग -अलग  रोगी  में   भिन्न हो  सकता है। मुझे बताया गया है किसी  भी ऑपरेशन  और प्रक्रिया  की  सफलता शतप्रतिशत    निश्चित नहीं होती |प्रक्रिया  के   विषय में मुझे कोई  निश्चिचन्ता नहीं दी गई है। मै  यह भी जानता हूँ कि अधिकांश रोगियों के लिए ऑपरेशन असामयिक   स्वास्थ्य लाभ  के लिए होता है. परन्तु कुछ  मामलो  में जटिलताए  भी  हो सकती है जैसे  कि मुझे स्पष्टकिया गया है, मै ऑपरेशन   तथा उसकी प्रक्रिया से सम्बंधित जटिलताओं से अवगत हो गया हूँ। मैं इस तथ्य से भी अवगत हूँ कि  सभी  सम्भावित   जटिलताएं  जो ऑपरेशन तथा चिकित्सीय प्रक्रियाओं से जुड़ी है, उनको सूचीबद्र्ध नहीं  किया  जा सकता | </div>");
            body.Append("<div class='row' style='text-align: justify; margin-top:1%'><b>सामान्यतौर पर सम्भावित जोखिम तथा जटिलताएं:-</b></div>");
            body.Append("<div class='row' style='text-align: justify;'>..........................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;'>..........................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;'>..........................................................................................................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;'>................................................................................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'><b>फोटोग्राफी:-</b>प्रक्रियाओं के दौरान चिकित्सीय, वैज्ञानिक तथा शैक्षिक प्रयोजन हेतु मेरे शरीर के उचित भाग की फोटोग्राफी तथा प्रसारण पर मेरी सहमति है। <U>लेकिन मेरी पहचान को स्पष्टनहीं होनी चाहिए।</U></div>");
            body.Append("<div class='row'>मैं प्रमाणित करता हूँ कि मेरी सहमति मुझे दिए गए पर्याप्त स्पष्टीकरण के पश्चात दी गयी है, व इस प्रपत्र को मैं भली प्रकार से पढकर समझ लिया हैं/ समझा दिया गया है।</div>");
            body.Append("<div class='row' style='text-align: justify;'>मैंने बेहोशी के दौरान अपने सम्बन्धी.................................को अपनी ओर से निर्णय लेने के लिए अधिकृत कर दिया है। ऑपरेशन के दौरान उचित विकल्प चुनने की छूट में अपने डॉक्टर को देता है।</div>");

            body.Append("<div class='row' style='text-align: justify;margin-top:1%'>रोगी का नाम : .............................................................................दिनांक ................................सम........................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'>रोगी के प्रतिनिधि का ना: ..........................................................................हस्ताक्षर................................................................</div>");

            body.Append("<div class='row' style='text-align: justify;margin-top:1%'>रोगी से संबंध ........................................................................................दिनांक/समय ...........................................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'><b>कारण :-</b> नाबालिक &nbsp;&nbsp;&nbsp;&nbsp; []&nbsp;&nbsp;&nbsp;&nbsp;बेहोश &nbsp;&nbsp;[] &nbsp;&nbsp;&nbsp;&nbsp;गफलत में &nbsp;&nbsp; &nbsp;&nbsp;[]&nbsp;&nbsp; &nbsp;&nbsp;मानसिक रोगी []&nbsp;&nbsp;&nbsp;&nbsp; शारीरिक विकलांगता &nbsp;&nbsp;&nbsp;&nbsp;[] </div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'> मैं (चिकित्सक का नाम ) ........................................................................................................ यह घोषणा करता हॅू </div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'> कि स्थानीय भाषा में रोगी को ऑपरेशन के निहितार्थ के बारे में भली प्रकार बता दिया गया है। </div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'><lable> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;चिकित्सक के हस्ताक्षर </lable><lable style='margin-left:30%'> दिनांक</lable><lable style='margin-left:30%'>समय</lable></div>");
        
            body.Append("</div>");
            return body.ToString();
        }
        public string InformedforSurgeryEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:80px;' />");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:225px;margin-right:225px; margin-top:-10px'>INFORMED CONSENT FOR SURGERY</h4></div>");

            body.Append("<div class='row' style='margin-left:5px;font-size:14px'><b>Date:....................</b></div>");


            body.Append("<table style='width:100%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name </b></td>");
            body.Append("<td>:</td>");
            body.Append("<b><td>" + pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td><b>DOA:</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Reg No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");


            body.Append("</table>");

            body.Append("<div class='row' style='margin-top:10px;font-size:14px'>");
            body.Append("<div class='row' style='text-align: justify;'><lable>I AuthorizeDr............................................................................................... and his team for the performance of the following procedure/Surgerywhich is necessary to treat me/my patient's condition:</lable></div>");

            body.Append("<div class='row' style='text-align: justify;'><lable>(Procedure(s)tobe performed)...............................................................................................................................</lable></div>");
            body.Append("<div class='row' style='text-align: justify;'>I have been explained about this consent form, which I fully understand and have understood the information provided to me.</div>");

            body.Append("<div class='row' style='text-align: justify;'><b>Risks: </b>The authorization is given with the understanding that any procedure involves some risk and hazardslike infection, bleeding, nerve injury, blood clots, heart attack, in rare situation death andallergic reactionsSetc.They can be serious and possibly fatal. </div>");

            body.Append("<div class='row' style='text-align: justify;'><b>Alternative, Benefit & Complication:</b>Further, I have been explained in my own language that the intended benefits, possible complication, and available alternative to the said operation/procedure. I am also aware that result of any operation/procedure can vary patient to patient; and I declare that no guarantees have been made to me regarding success of this operation/procedure. I am aware that while majority of patient have an uneventful operation and recovery, few cases may be associated with complication. I am aware of the common risk as explained and complications associated with the operation/procedure and also understand that it is not possible to list all possible complications of any operation/procedure.</div>");

            body.Append("<div class='row' style='text-align: justify;'><b>Possible Risks & Complicationscommonly faced:</b><br/><b>...............................................................................................................................................................................................</b></div>");

            body.Append("<div class='row' style='text-align: justify;'><b>..............................................................................................................................................................................................................</b></div>");
            body.Append("<div class='row' style='text-align: justify;'><b>..............................................................................................................................................................................................................</b></div>");

            body.Append("<div class='row' style='text-align: justify;'><b>...............................................................................................................................................................................................</b></div>");
            body.Append("<div class='row' style='text-align: justify;'>..............................................................................................................................................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;'>.................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;'><b>Photography:</b>I consent to the photographing or telecast of procedures to be performed, includingappropriate portion of my body for medical, scientific or educational purposes, <U>provided my identity is not revealed.</U></div>");
            body.Append("<div class='row' style='text-align: justify;'> I certify that I have read and fully understood the above consent after adequate explanations were given to me.</div>");
            body.Append("<div class='row' style='text-align: justify;'>Signature or Patient:....................................................................................................Date & Time:.............................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'>Relative's Name..................................................................................................Signature:...........................................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:1%'>Relation:...................................................................................................Date/Time:....................................................................</div>");
            body.Append("</div>");
            //body.Append("<div class='row' style='text-align: justify;margin-top:1%'>Reason:&nbsp;Minor &nbsp;&nbsp;<input type='checkbox'/>&nbsp;&nbsp;&nbsp;&nbsp; Unconscious &nbsp;&nbsp;<input type='checkbox' class='larger' />&nbsp;&nbsp;&nbsp;&nbsp; Drowsy &nbsp;&nbsp;<input type='checkbox'  />&nbsp;&nbsp;&nbsp;&nbsp; Mentally &nbsp; &nbsp;<input type='checkbox'  />&nbsp;&nbsp;&nbsp;&nbsp; sically Challenged &nbsp;&nbsp;<input type='checkbox'  /></div>");


            body.Append("<table style='width:100%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td>Reason</td>");
            body.Append("<td>:</td>");
            body.Append("<td>Minor</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td>Unconscious</td>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Drowsy</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Mentally</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>sically Challenged</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='margin-top:10px;font-size:14px'>");
            body.Append("<div class='row' style='text-align: justify;'><b>...........................................................................................................................................................................................................</b></div>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:200px;margin-right:200px;'><U>DECLARATION BY DOCTOR</U></ h4></div>");
            body.Append("<div class='row' style='text-align: justify;'>I,..................................................................................................(Name of Doctor)hereby, state that the patient has explained about the implication of the operation in the vernacular.</div>");
            body.Append("<div class='row' style='text-align: justify;'>Signature of Doctor:........................................................Date:.......................................Time:...................................</div>");

           
            body.Append("</div>");
            return body.ToString();
        }
        public string NursesPreOperativeCheckListForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:340px;margin-right:130px;text-align: justify ; margin-top:-70px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:200px;margin-right:200px;margin-top:-10px'>NURSES PRE-OPERATIVE CHECKLIST</h4></div>");

            body.Append("<h5 style = 'margin-top:-10px'>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b></b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Ward Nurse</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Pre-OP Nurse</b></th>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Date & Time</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Consent for Surgery taken</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Consent for Anaesthesia taken</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>High Risk Consent</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Patient on NPO</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Preparation of Operation Site</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Investigation Reports Attached CT Plate</b><br/>&nbsp;&nbsp;&nbsp;<b>MRI Plate</b><br/>&nbsp;&nbsp;&nbsp;<b>X-Ray</b><br/>&nbsp;&nbsp;&nbsp;<b>Ultrasound</b><br/>&nbsp;&nbsp;&nbsp;<b>Any Other</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Enema Administered</ b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Bladder Empty</ b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Vitals Taken</ b><br/>&nbsp;&nbsp;&nbsp;<b>Temperature</b><br/>&nbsp;&nbsp;&nbsp;<b>Pulse</b><br/>&nbsp;&nbsp;&nbsp;<b>Respiration</b><br/>&nbsp;&nbsp;&nbsp;<b>Blood Pressure</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Pre-Medication Given</ b><br/>&nbsp;&nbsp;&nbsp;<b>1.</b><br/>&nbsp;&nbsp;&nbsp;<b>2.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>All ornaments Removed</ b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Any ornament left on body which cannot be removed</ b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Denture Removed</ b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Any blood Product received along with patient</ b><br/>&nbsp;&nbsp;&nbsp;<b>Type of Product:</b><br/>&nbsp;&nbsp;&nbsp;<b>PRBC/FFP/Platelet/Cryo/..................</b><br/>&nbsp;&nbsp;&nbsp;<b>Bag Number:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><b>Date of Expiry:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Any Medication received along with patient:</ b><br/>&nbsp;&nbsp;&nbsp;<b>1.</b><br/>&nbsp;&nbsp;&nbsp;<b>2.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>OT dresses Kurta...............Pyjama.................Gown....................</b><br/>&nbsp;&nbsp;&nbsp;<b>Any Other</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Name Tag On</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Remarks</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Name & Signature</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            return body.ToString();
        }
        public string AnesthesiaRecordForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
           
            StringBuilder body = new StringBuilder();
            
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:360px;margin-right:100px;text-align: justify ; margin-top:-70px; width:450px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:-20px'>ANESTHESIA RECORD</h4></div>");

            body.Append("<div class='row' style='text-align: justify; margin-top:-20px'>");

            body.Append("<div class='row'> <lable>Diagnosis:__________________________________________________________________________________________________</lable></div>");
            body.Append("<div class='row'> <lable>Procedure:_________________________________________________________________________________________________</lable></div>");
            body.Append("<div class='row'> <lable>Surgeon:__________________________________________________________________________________________________</lable></div>");
            body.Append("<div class='row'> <lable>Anesthesiologist:_____________________________________</lable><lable width:50%>Weight (kgs)/Height (cms),:_________________________________</lable></div>");
            body.Append("</div>");

            //body.Append("<div class='row' style='text-align: justify; margin-top:1%'><input type='checkbox'/>  Elective <input type='checkbox' class='larger' style='margin-left:100px' />Day care<input type='checkbox' style='margin-left:100px'  />In patient<input type='checkbox' style='margin-left:100px' />Emergency Surgery</div>");
            body.Append("<div class='row' style='margin-top:10px;font-size:13px'>");
            body.Append("<table style='width:100%;font-size:15px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Elective</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Day care</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>In patient</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Emergency Surgery</td>");

            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</div>");

            body.Append("<h5 style ='text-align:justify; font-size:12px'>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;'><b> PRE-ANESTHESIA ASSESSMENT</b></h4></div>");
            body.Append("<h5>Previous Anesthesia History:</h4>");
            body.Append("<h5 style='margin-top:5%'>History of medical illness and treatment (P/H/O, HTN/DM, Asthma/COPD, Epilepsy, etc.)</h5>");
            body.Append("<h5 style='margin-top:7%'>Clinical Examination:</h5>");
            // body.Append("<div class='row' style='color:black;'><lable style='width:100%'>Pulse:&nbsp;</lable><lable style='margin-left:5%'>Blood Pressure:&nbsp;</lable><lable style='margin-left:5%'>Respiratory Rate:&nbsp;</lable><lable style='margin-left:5%'>Temperature:&nbsp;</lable><lable style='margin-left:5%'>Pallor:&nbsp;</lable></div>");
            body.Append("</h5>");

            body.Append("<h5 style ='text-align:justify;margin-top:-6px; '>");
            body.Append("<table style='width:100%;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><p>Pulse</p></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Blood Pressure:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Respiratory Rate:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Temperature:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Pallor:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><p>Icterus: Edema:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Cyanosis:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Clubbing:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><p>RS:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>CVS:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><p>PA:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>CNS:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Airway</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><p>Mouth Opening:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Teeth:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Mallampatti Score:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><p>TMJ mobility:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Thyromental Distance</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Neck movements:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ; margin-top:10px;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><p>Difficult Intubation: </p></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black; margin-left:-20%'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Spine:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>ASA Grade:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ; margin-top:10px;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Exercise Tolerance:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ; margin-top:10px;font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td><b>History of:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black; margin-left:-20%'></td>");
            body.Append("<td>Tobacco</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Smoking</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>Alcohol:</p></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>Allergies</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<h5 style='margin-top:-5px'>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid;margin-top:-10px'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>INV</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Result</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Date</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>INV</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Result</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Date</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>INV</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Result</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Date</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>INV</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Result</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Date</b></th>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Hb</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>FBS</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>AIb</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>TLC</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>PPBS</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Bil</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>DLC</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>RBS</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>SGOT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Plt</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Creat</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>SGOT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>BT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Urea</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>T3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>CT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Na+</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>T4</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>PT/INR</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>K+</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>TSH</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>APTT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Cl-</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>HlV</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Group</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Urine</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>HbsAg</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td><b>ECG:</b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>CHest X ray:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td><b>2D Echo:</b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>Others:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");




            body.Append("<h5>");
            body.Append("<table style='width:100%;font-size:13px;margin-top:-5px;'>");
            body.Append("<tr>");
            body.Append("<table style='width:100% ;margin-top:-12px;font-size:13px'>");
            body.Append("<tr>");
            body.Append("<td><b>Anesthesia Risk:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Low</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Moderate</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>High:</p></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><p>Risk factors:</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px'>");
            body.Append("<tr>");
            body.Append("<td><b>Anesthesia Plan:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>GA</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Spinal</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>Epidural:</p></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>Sedation</p></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>Local</p></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>MAC</p></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px'>");
            body.Append("<tr>");
            body.Append("<td><b>May require post-operative ventilation:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:13px'>");
            body.Append("<tr>");
            body.Append("<td><b>Pre-anesthesia advice:</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:13px'>");
            body.Append("<tr>");
            body.Append("<td><b>Name and Signature of Anesthesiologist:</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Date & Time</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            return body.ToString();
        }
        public string PreInductionAssessmentForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            //body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:180px; height:70px;></h3></div>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px; margin-top:-3px' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:340px;margin-right:100px;text-align: justify ; margin-top:-70px; width:460px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:-10px'>PRE-INDUCTION ASSESSMENT</h4></div>");
            //body.Append("<h5 style = 'text-align: justify ;font-size:14px; margin-top:-15px;'>");
            body.Append("<table style='width:100%;font-size:14px;margin-bottom:-15px;'>");
            body.Append("<tr>");
            body.Append("<td>Pulse:</td>");
            body.Append("<b><td>:</td></b>");
            body.Append("<b><td></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>RR:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>BP:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>RS:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>CVS:</td>");
            body.Append("<td><b></d></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>Others:</td>");
            body.Append("<td><b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ; margin-top:10px;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td><p>Any significant change in condition:</p></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black; margin-left:-10%'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ; margin-top:5px;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>Fit to be anesthetized as per plan</p></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Change of plan required</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ; margin-top:5px;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><p>Prophylactic antibiotic: Drug:</p></td>");
            body.Append("<td>Dose:</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Time:</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");



            body.Append("<tr>");
            body.Append("<table style='width:100% ; margin-top:5px;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td><p>WHO surgical checklist completed:</p></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black; margin-left:-10%'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>"); body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100% ; margin-top:13px;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Name and Signature of Anesthesiologist:</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Date and Time:</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td><b>_____________________________________________________________________________________________________________________</b></td>");

            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");


            body.Append("<div class='row'><h5 style='color:black;text-align:center;margin-left:150px;margin-right:150px;margin-top:-20px'>INTRA-OPERATIVE ANESTHESIA RECORD</h5></div>");
            body.Append("<h5 style = 'text-align: justify ; margin-top:-10px; font-size:14px;'>");
            body.Append("<table style='width:100%; font-size:13px;'>");
            body.Append("<tr>");
            body.Append("<td>Date:</td>");
            body.Append("<b><td></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>Induction time:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>Position:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>Starting time:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td>Ending time:</td>");
            body.Append("<td><b></d></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td></td>");
            body.Append("<td><b></d></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>Monitoring:</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>SPO₂;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>ECG</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>NIBP</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>ETCO₂</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>ABP</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Urinary catheter</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>FlO₂</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>RGM</td>");

            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>Vascular Access:</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Peripheral Site:</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>LA</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>RA</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Central Site</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>IJV</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>SC.</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Arterial Site</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>R</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>F</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td><b>_____________________________________________________________________________________________________________________</b></td>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h5 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:-18px'>ANESTHESIA</h5></div>");
            body.Append("<h5 style = 'text-align: justify ;font-size:13px; margin-top:-3px;'>");
            body.Append("<table style='width:100%;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>General Anesthesia</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Regional Anesthesia</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Sedation</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>MAC</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>LA</b></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td><b>GA+RA</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>GA+BLOCK</b></td>");
            body.Append("<td><b></d></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Pre-induction:</b></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Midaz....mg</td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Glyco........mg</td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Fentanyl........us</td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Pentazocine.......mg</td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Morphine.......mg</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Pre-oxygenation and induction:</b></td></b>");
            //body.Append("<b><td><b>:</b></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Propofol........mg </td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Etomidate........mg </td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Ketamine........mg </td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>inhalational.........</td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Intubation Relaxant:</b></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Scoline........mg </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Atra </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Vec </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Pav</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Roc......mg</td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Airway Device</b></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>ETT-Size:..........</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Oral </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Nasal </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Cuffed</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Uncuffed</td>");


            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Type</b></td></b>");
            //body.Append("<b><td><b>:</b></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>PVC</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Armored  </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Face mask </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>LMA</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Tracheostomy tube</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>DLT</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>ThroatPack</b></td></b>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<b><td>No</td></b>");
            body.Append("<b><td><b>NGTube</b></td></b>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<b><td>No</td></b>");
            body.Append("<b><td><b>Circuit</b></td></b>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Closed</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Bain's</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Jackson Rees</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Maglis</td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Ventilation</b></td></b>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Spontaneous</td>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Controlled: </td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>FGF:</td></b>");
            body.Append("<b><td></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>TV:</td></b>");
            body.Append("<b><td></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>RR:</td></b>");
            body.Append("<b><td></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>Airway pressure:</td></b>");
            body.Append("<b><td></td></b>");
            //body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>PEEP:</td></b>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Maintenance:</b></td></b>");

            body.Append("<b><td>O₂.......L/min N20:......L/min</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Iso........ %</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Sevo........% </td>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Atra</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Vec</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");

            body.Append("<td> Roc </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Pav........mg</td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Reversal:</b></td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>Myopyrrolate:............cc Neostigmine:..............mg Glyco:..........mg </td></b>");
            body.Append("<b><td>Antiemitic: </td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Ondan </td>");
            body.Append("<b><td></td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Palonosetron</td>");
            body.Append("<b><td>&nbsp;</td></b>"); body.Append("<b><td>&nbsp;</td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:14px; margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<b><td><b>Post op Analgesia:</b></td></b>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Tramadol.......mg </td>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Diclo...mg</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Paracetamol.....ml </td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Morphine</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Buprenorphin</td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td><b>_____________________________________________________________________________________________________________________</b></td>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h5 style='color:black;text-align:center;margin-left:150px;margin-right:150px;margin-top:-20px'>REGIONAL ANESTHESIA</h5></div>");
            body.Append("<h5 style = 'text-align: justify ; margin-top:-5px;'>");
            body.Append("<table style='width:100%;font-size:14px;margin-bottom:-15px; margin-top:-8px '>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Spinal</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Epidural</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>SA+EA</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Caudal</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Local</b></td>");

            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Nerve Block.</b></td>");
            body.Append("<td><b></d></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:14px;margin-top:13px'>");
            body.Append("<tr>");
            body.Append("<td><b>Spinal</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td><b>Needle size:</b></td>");
            body.Append("<td><b></d></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td><b>Space:</b></td>");
            body.Append("<td><b></d></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td><b>Drug:</b></td>");
            body.Append("<td><b></d></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td><b>Dose:</b></td>");
            body.Append("<td><b></d></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>Adjunct:................</b></td>");
            body.Append("<td><b></d></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:14px;margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<td><b>Epidural:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Single Dose:</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Catheter:Size:..................Space:...............Drug..................Adjunct:......................</td>");

            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:14px;margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<td>Level:...................</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Action Adequate</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Action Inadequate</td>");
            body.Append("<td><b></d></td>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("<b><td>&nbsp;</td></b>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:14px;margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<td><b>Nerve Block:</b></td>");
            body.Append("<td style='height: 5px; width: 12px; border: 1px solid black;'></td>");
            body.Append("<td>Supra clavicular</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Inter scalene</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Axillary</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Wrist</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Ankle</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Femoral</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Other</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='width:100% ;font-size:14px;margin-top:5px'>");
            body.Append("<tr>");
            body.Append("<td><b>Drug and dose:</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            //body.Append("</h5>");
            return body.ToString();
        }
        public string IntraOperativeMaintenanceForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            //body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:180px; height:70px;></h3></div>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:100px;text-align: justify ; margin-top:-70px; width:450px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h5 style='color:black;text-align:center;margin-left:150px;margin-right:150px;'>INTRA-OPERATIVE MAINTENANCE AND MONITORING</ h5></div>");
            body.Append("<h5 style = 'margin-top:-20px'>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            // First row
            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");

            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align:Left;width:351px'>Time</td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("<td colspan='3' style='border-left: 2px solid;border-collapse: collapse;width:100px'></td>");
            body.Append("</tr>");

            // Second row
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:17px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align:Left;width:351px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            // Second row
            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:336px'>O₂L/min</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:328px'>N₂OL/min</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:323px'>Inhalational % <input style='border:1px solid black; height:10px; width:10px;'/> Iso<input style='border:1px solid black; height:10px; width:10px;'/>Sevo  </td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:242px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;220 </td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:282px;'>Relaxant  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;210 </td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            //       hkjhk
            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:242px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;200 </td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:294px;'>Ventilation  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;210 </td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");




            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:242px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;180</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:293px;'>IV Fluids  &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;170</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:242px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;160</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:242px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;150</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:242px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;140</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:293px;'>Pulse O  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;130</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");



            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:242px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;120</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:294px;'>BP &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;110</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:293px;'>Syst V &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;100</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");



            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:299px;'>Diast A &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;90</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:247px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;80</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:299px;'>SPO₂ <input style='border:1px solid black; height:10px; width:10px;'/> &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;70</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:247px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;60</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:299px;'>ETCO₂ <input style='border:1px solid black; height:10px; width:10px;'/> &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;50</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:247px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;40</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:299px;'>Temp <input style='border:1px solid black; height:10px; width:10px;'/> &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;30</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:247px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;20</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:247px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:247px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;00</td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border-left: 2px solid;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25px'></td>");
            body.Append("</tr>");



            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;width:100%;  text-align:Center'>FLUID BALANCE</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; width:100px;text-align:Center'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Center;width:100px'>Crystalloid</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Center;width:100px'>colloid</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:101px'>FFP</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:101px'>PCV</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'>WHole blood</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'>Pits/Cryo</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'>Total</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; width:92px;text-align:Center'>INPUT</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Center;width:103px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:99px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; width:95px;text-align:Center'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Center;width:101px'>Blood Loss</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Center;width:98px'>Urine Output</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:97px'>Other Loss</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; width:91px;text-align:Center'>OUTPUT</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Center;width:103px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Center;width:101px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:100px'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; width:17px;text-align:Center;height:20px'></td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px; text-align:Right;width:123px'>BALANCE</td>");
            body.Append("<td style='border-collapse: collapse;border: solid 1px;text-align:Center;width:18px'></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px'>");
            body.Append("<tr>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Eye Care</td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Pressure area care</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Tourniquet on time: </td>");
            //body.Append("<td>:</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Tourniquet off time: </td>");
            //body.Append("<td>:</td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px'>");
            body.Append("<tr>");
            body.Append("<td><b>Additional Drugs given:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-top:2%'>");
            body.Append("<tr>");
            body.Append("<td><b>Intra-operative investigations done:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-top:2%'>");
            body.Append("<tr>");
            body.Append("<td><b>Post-operative status:</b></td>");
            body.Append("<td>HR:</td>");
            body.Append("<td></td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td>BP:</td>");
            body.Append("<td></td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td>RR:</td>");
            body.Append("<td></td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td>SPO₂:</td>");
            body.Append("<td></td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td>ECG:</td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td>RS:</td>");
            body.Append("<td></td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td>CVS:</td>");
            body.Append("<td></td>");

            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Pre-extubation status after GA:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Eye opening:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td> Consciousness:</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Awake:</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Drowsy:</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Unresponsive:</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Reflexes:</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td>Tone</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td> Head lift:</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Extubation:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black; margin-left:20px'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px'>");
            body.Append("<tr>");
            body.Append("<td><b>Any adverse events:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table  style='width:100%;font-size:12px; margin-top:1%'>");
            body.Append("<tr>");
            body.Append("<td><b>Shift to:</b></td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Post anesthesia recovery room</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td style='margin-left:500px'>ICU</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");
            return body.ToString();
        }
        public string PostAnesthesiaRecoveryRoomChartForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:320px;margin-right:100px;text-align: justify ; margin-top:-70px; width:460px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px; font-size:12px; margin-right:150px; margin-top:-17px'>POST ANESTHESIA RECOVERY ROOM CHART</h3></div>");

            body.Append("<h5 style='width:50%;font-size:12px'>");
            body.Append("<table style='width:99%;font-size:12px;border-collapse:collapse;border:1px solid;margin-top:-2%'>");
            // First row
            body.Append("<tr>");
            body.Append("<table style='width:99%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th  colspan='4' style='border: solid 1px;border-collapse: collapse; text-align: center'>Anesthesia recovery room scoring system criteria to shift patient to ward</th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;height:5px'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Parameters</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>Evaluation</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>Score</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>1</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Consciousness</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Fully Awake</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Arousable on calling</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>No response</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>2</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Respiration</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Able to breathe deeply and cough freely </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Dyspnea, shallow or limited breathing </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Арпеа</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>3</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>O₂ saturation</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'> >92% while breathing room air </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '>Needs O₂, to maintain saturation >90% </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><90% with supplemental O₂</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>4</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Circulation</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>BP±20% of preanesthetic level </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>BP± 20%-49% of preanesthetic level </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'>BP ± > 50% of preanesthetic level.</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>4</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center; height:70px'>Activity</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Able to move 4 extremities on command</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Able to move 2 extremities on command </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Not able to move any extremities on command</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("<td style='text-align: right;height:54px'>Total Score</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("<td style='text-align:center; height:70px; center border: solid 1px;border-collapse: collapse;'>Score or 9/10 is considered adequate to shift patient to ward.</ td>");
            body.Append("<td></ td>");
            body.Append("</tr>");
            body.Append("</tr>");
            body.Append("</table>");

            // Second row
            body.Append("<tr>");
            body.Append("<table style='width:99%;font-size:12px;border-collapse:collapse;border:1px solid; margin-left:100%;margin-top:-117%'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th  colspan='4' style='border: solid 1px;border-collapse: collapse; text-align: center'>Anesthesia discharge scoring system criteria to discharge home in adult day care procedure</th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Parameters</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>Evaluation</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>Score</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>1</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Vitals</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'> BP±20% of preanesthetic level </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '>BP± 20%-39% of preanesthetic level  </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '>BP ± 40% of preanesthetic level</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>2</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Ambulation</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Steady gait meets preanesthetic level </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Toddle, requires assistance </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Unable to ambulate</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>3</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Nausea Vomiting </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>None to minimal </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapsep;'>Moderate </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Severe</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>4</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Pain </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'> minimal </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Moderate </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Severe</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>5</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Surgical Bleeding </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'> minimal/absent </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Moderate </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Severe</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>6</td>");
            body.Append("<td rowspan='3' style='border-left: 1px solid;border-collapse: collapse; text-align: center'>Voiding </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'> Able to pass urine </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>2</ td>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Passes urine with difficulty </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>1</ td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Urine retention</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'>0</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("<td style='text-align: right'>Total Score</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("<td style='text-align: center border: solid 1px;border-collapse: collapse; height:30px'>A score of 9/12 is considered adequate for discharge Vital signs criteria must never score less than 2 None of the other 5 criteria must ever score 0.</ td>");
            body.Append("<td></ td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h5 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:2%'>PAIN ASSESSMENT </h5></div>");

     
            string imageePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/download.png");

            body.Append("<img src='imageePath' style='width:390px; height:95px;' />");

            body.Append("<div class='row'><h5 style='color:black;text-align:center;margin-left:150px;margin-right:150px;'>POST OPERATIVE ORDERS</h5></div>");

            body.Append("<div class='row'style='width:100%;font-size:12px;'>");

            body.Append("<p><lable>Shift to:</lable></p>");
            body.Append("<p><lable>NBM till:</lable></p>");
            body.Append("<p><lable>Monitoring:</lable> </p>");
            body.Append("<p><lable>Fluids:</lable> </p>");
            body.Append("<p><lable>Post-operative analgesia:</lable> </p>");
            body.Append("<p><lable><b>Name and Signature of Anesthesiologist:</b></lable> <lable style='margin-left:250px'><b>Date & Time:</b></lable>  </p>");
            body.Append("</div>");
            
            return body.ToString();
        }
        public string OperationNotesForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:200px; height:100px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:500px;margin-right:100px;text-align: justify ; margin-top:-70px; width:650px;height:70px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender : </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:-10px'>OPERATION NOTES</h3></div>");

            body.Append("<h3 style='width:100%;font-size:12px;'>");

            body.Append("<h3><lable>Surgeon(s):_______________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3><lable>Anesthetist:_______________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3><lable>Date of Surgery :__________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3><lable>Pre-Operative Diagnosis:___________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3><lable>Procedure/Surgery Done:________________________________________</lable><lable>Post-Operative/Final Diagnosis:_________________________________</lable> </h3>");
            body.Append("<h3><lable>Operative Notes & Findings:</lable> </h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Incision:________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Surgical procedure:______________________________________________________________________________________________</lable> </h3>");

            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Implant:________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Drain:__________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Closure:________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Dressing:_______________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3><lable>Material for Histopathology:_________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3><lable>Post OP Orders:__________________________________________________________________________________________________________</lable> </h3>");

            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3 style ='text-align: justify'><lable>________________________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3><lable>Date & Time_______________________________</lable><lable>NAME & SIGNATURE OF SURGEON:____________________________________________</lable> </h3>");

            body.Append("</h3 >");

            return body.ToString();
        }
        public string AdditionalNotesIfRequiredForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
           
            StringBuilder body = new StringBuilder();

            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:340px;margin-right:100px;text-align: justify ; margin-top:-70px; width:460px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:-10px'>ADDITIONAL NOTES IF REQUIRED</h4></div>");
            return body.ToString();
        }


        //Dialysis
        public string InformedConsentForHemoDialysisHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            StringBuilder body = new StringBuilder();
        
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<center><h2 style ='margin-top:70px;'><b>हिमोडायलिसिस के लिए सहमति पत्र</b></h2><p>(हिमोडायलिसिस से पहले रोगी रोगी के अश्शिवक के द्वारा श्रा जाये )</p></center>");
            body.Append("<div class='row' style='text-align: justify'><p>मैं ये घोषणा करता हूं कि मुझे मेरे मरीज की बीमारी (अक्यूट रीनल  फेल्योर / क्रोनिक रीनल  फेल्योर / रेपीडली  प्रोगेसिव रीनल फेल्योर और हिमोडायलिसिस के बारे मे मुझे मेरी क्षेत्रीय भाषा में विस्तारपूर्वक बताया गया है। मुझे बताया गया है कि शरीर से रक्त आर्टरियोवेनस शंट / आर्टरियोवेनस फिस्टुला / फेमोरल कैथीटेराइजेशन /इन्टरनल जुगल कैथीटेराइजेशन / सबक्लेवियन कैथीटेराइजेशन के माध्यम द्वारा बाहर लाया जाता है। इस रक्त को हिमोडायलिसिस मशीन तथा कृत्रिम किडनी यंत्र के द्वारा साफ करके वापस शरीर में प्रवाहित कर दिया जाता है जो रक्त को शुद्ध करता है। यह प्रक्रिया आमतौर पर 2-5 घंटे तक चलती है।</p></div>");
            body.Append("<h3><u>हिमोडायलिसिस के दौरान होने वाली समस्याएं - </u></h3>");
            body.Append("<div class='row' style='text-align: justify'><p><u><b>साधारण समस्याएं- </b></u> रक्तचाप में कमी (20-30%) उल्टी (5-15%), सिरदर्द (5%), सीने का दर्द (2-5%), कमर का दर्द (2-5%) खुजली और जलन (5%). खून की नली डालने के स्थान पर खून का थक्का बन जाना (1%>) और जाड़े के साथ बुखार (1%>) |</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p><u><b>असाधारण समस्याएं -</b></u>डायलाईजर का गलत प्रशव, दिल की धड़कन का असंतुलित हो जाना, दिल का दौरा, कार्डियाक टैम्पोनैड, इंट्राकॅनियन रक्तसाव, दौरे, हेमोलाइसिस, इन्फेक्शन, एयर एम्बोलिज्म, किसी भी साइट से खून बहना, न्यूरोपेनिया और हाइपोक्सीमिया ये लक्षण असामान्य है जोकि हिमोडायलिसिस के दौरान या उसके बाद मौत का कारण भी हो सकते हैं।</p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>ये सारी जानकारी मुझे मेरी स्थानीय भाषा में डॉ / नर्स / टैक्नीशियन अच्छी तरह समझा दिया गया है। मैंने इस पूरी प्रक्रिया और इसमे हो सकने वाली समस्याओं को पूरी तरह से समझ गया हूं और हिमोडायलिसिस के लिए अपनी पूर्ण सहमती देता हूँ। </ p></div>");

            //body.Append("<div class='row' style='text-align: justify'><p>मरीज का नाम ..................................................उम्र / लिंग ................................................................ </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>यू। एच /आई। डी. ................................दिनांक ............................................................................... </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>मरीज /मरीज के अभिभावक का नाम एवं हस्ताक्षर ....................................................................................... </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>अभिभावक से मरीज का रिश्ता............................................................................................................ </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p> मरीज/अभिभावक का पता ..................................................................................................................</p></div>");
            body.Append("<table style='width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td>मरीज का नाम</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td>उम्र / लिंग</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>यू। एच /आई। डी.</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td>दिनांक</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + admitdate.ToString() + "</td>");

            body.Append("<tr>");
            body.Append("<td>मरीज /मरीज के अभिभावक का नाम एवं हस्ताक्षर</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>अभिभावक से मरीज का रिश्ता</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>मरीज/अभिभावक का पता</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='text-align: justify'><p>................................................................................................................................................................................</p></div>");

            body.Append("<div style='margin-right:100px;text-align: justify'> <lable style='margin-left:200px;'></lable><br>डायलिसिस नर्स / टैक्नीशियन के हस्ताक्षर:<lable style='margin-left:200px;'>कंसलटेंट का हस्ताक्षर </lable></div>");
        
            body.Append("</div>");
           
            return body.ToString();
        }
        public string InformedConsentForHemoDialysisEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            StringBuilder body = new StringBuilder();

            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<center><h3 style ='margin-top:80px;'><b>INFORMED CONSENT FOR HEMODIALYSIS</b></h3><p><b>(TO BE FILLED BY PATIENT'S GUARDIAN BEFORE HEMODIALYSIS)</b></p></center>");
            body.Append("<div class='row' style='text-align: justify'><p>I have been explained in my own local language, the nature of disease (Actual Renal Failure/Chronic Renal Failure/Rapidly Progressive Renal Failure) and the need for Hemodialysis. I have been told that hemodialysis involves passage of blood through an artificial kidney which cleans and purifies the blood. The blood from the body is taken out through Arteriovenous Shunt/Arteriovenous Fistula / Femoral catheterization / Internal Juglar catheterization/Subclavian catheterization. The procedure usually lasts for 2-5 Hours.</p></div>");
            body.Append("<h3>Complication which can occur during Hemodialysis are: - </h3>");
            body.Append("<div class='row' style='text-align: justify'><p><u><b>Common (mild) complications: </b></u> Decrease in blood Pressure (20-30%), Nausea and Vomiting (5-15%), Headache (5%), Chest Pain (2-5%), Backache (2-5%), Itching (5%), Hematoma formation at catheterization site & fever and chills (<1%) </p></div>");
            body.Append("<div class='row' style='text-align: justify'><p><u><b>Severe (Uncommon) complications: </b></u>Dialyzer reaction, Irregular heart rhythm, Cardiac arrest, Cardiac tamponade, Intracranial bleeding, Seizures, Hemolysis, Infection Air embolism, Bleeding from any site, Neutropenia & Hypoxemia. These complications are uncommon but can be lethal and cause Death during or after hemodialysis. </ p></div>");
            body.Append("<div class='row' style='text-align: justify'><p>The detail of the procedure and above complications have been explained to me in my own local language by Dr./Nurse/Technician........... and I have understood the procedure and possible complications and give my unreserved and informed consent for hemodialysis. </ p></div>");

            //body.Append("<div class='row' style='text-align: justify'><p>Name of Patient Mr./Mrs./Miss............................................Age/Gender ................................... </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>UHID................................................Date & Time................................................................ </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Signature of Patient/ Patient's Guardian..................................................................................... </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Relationship of Guardian to the Patient...................................................................................... </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p> Address of the Patient /Guardian ...................................................................................................</div>");
            //body.Append("<div class='row' style='text-align: justify'><p>..................................................................................................................................................</div><br>");

            body.Append("<table style='width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td>Name of Patient Mr./Mrs./Miss</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td>Age/Gender</td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>UHID.</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td>Date & Time</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + admitdate.ToString() + "</td>");

            body.Append("<tr>");
            body.Append("<td>Signature of Patient/ Patient's Guardian</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Relationship of Guardian to the Patient</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Address of the Patient /Guardian</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='text-align: justify'><p>................................................................................................................................................................................</p></div>");

            body.Append("<div style='text-align: justify'> <lable style='margin-left:200px;'></lable><br>Signature of Dialysis Nurse/Technician<lable style='margin-left:200px;'>Signature of the Consultant </lable></div>");

           
            body.Append("</div>");
            
            return body.ToString();
        }
        public string HemoDialysisProformaForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            StringBuilder body = new StringBuilder();
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-10px ' />");
            body.Append("<div class='row'style='text-align: justify;margin-top:-15px'><center><p>DEPARTMENT OF NEPHROLOGY<br>(HEMODIALYSIS UNIT)</p></center></div>");
            body.Append("<div class='row'style='text-align: justify;margin-top:-15px'><center><p>HEMODIALYSIS PROFORMA</p></center></div>");
            body.Append("<table style = 'border:1px solid;text-align: justify ; width:443px;height:150px; padding:10px;margin-top:-15px;<br>'>");
            body.Append("<tr>");
            body.Append("<td>UHID: &nbsp; " + uhidno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>NAME OF PATIENT: &nbsp; " + pname.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>AGE/GENDER: &nbsp; " + gender.ToString() + " </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>DRY WEIGHT............................................. </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>TARGET WEIGHT..................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>GENERAL CONDITION..............................</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style = 'border:1px solid;margin-left:460px;text-align: justify;width:443px;height:160px; padding:10px;margin-top:-160px'>");
            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>NUMBER OF DAILYSER REUSE...................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>NUMBER OF TUBING REUSE................................... </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>PRE HD WEIGHT.............K.G.BP.......................mmHg </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>POST HD WEIGHT.....................K.G.BP.............mmHg </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>OTHER........................................................................</td>");
            body.Append("</tr>");
            body.Append("</table><br>");

            body.Append("<table style = 'border:1px solid;text-align: justify ; width:443px;height:270px; padding:10px;<br>'>");
            body.Append("<thead>");
            body.Append("<tr>");
            body.Append("<th><center>HD PRESCRIPTION</center></th>");
            body.Append("</tr>");
            body.Append("</thead>");

            body.Append("<tr>");
            body.Append("<td>HD.............................................HOURS </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>NET UF..........................................LITERS</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>HEPARIN.............UNIT BOLUS AND............. </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>BEFORE TERMINATION</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>DIALYSIS Na....meq/L,CA.........................meq/L</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>TEMP..............°C,PHOSPHATE..................meq/L</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>TARGET PUMP SPEED................................./MIN </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>SODIUM PROFILING</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>VASCULAR ACCESS</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>MEDICATION DURING/AFTER</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>1. EPO............................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>2. IV IRON......................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>3. OTHERS............................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>4...........................................................</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style = 'border:1px solid;margin-left:460px;text-align: justify;width:443px;height:270px; padding:10px;margin-top:-387px'>");
            body.Append("<thead>");
            body.Append("<tr>");
            body.Append("<th><center>HD DOSE DELIVERED</center></th>");
            body.Append("</tr>");
            body.Append("</thead>");
            body.Append("<tr>");
            body.Append("<td>HD.....................................HOURS DONE </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>NET UF.................................LITERS DONE</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>HEPARIN.......................UNIT GIVEN </td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>MEDICATION GIVEN DURING/AFTER HD</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>1.............................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>2.......................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>3.............................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>COMPLICATION DURING/AFTER HD</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>......................................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>.....................................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>REMARKS..........................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>.....................................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>.....................................................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>.....................................................</td>");
            body.Append("</tr>");
            //body.Append("<tr>");
            //body.Append("<td>.....................................................</td>");
            //body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:900px;height:100px;font-size:12px;border-collapse:collapse;border:1px solid;'><center><b>VITAL CHART DURING HD</b></center>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:10px'><b>S.No.</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>TIME</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>P/R &nbsp;</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Net UF<br>(L)</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>BP<br>(mm of Hg)</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Complication</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Medication Used</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Remarks</b></th>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>1.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>2.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>3.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>4.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>5.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>6.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>7.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>8.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>9.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr  style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;padding:8px'><b>10.</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");


            body.Append("<div class='row' style='text-align: justify'><p>Next HD on...................at..............a.m/p.m</div><p></div>");
            body.Append("<div class='row' style='text-align: justify;margin-top:-20px'><p>Intructions for next HD......................................................<lable style='margin-left:150px;margin-bottom:200px'>Name & Signature of Nurse/Technician</lable></div></p></div>");
          
            body.Append("</div>");
            return body.ToString();
        }
        public string DischargeSummaryForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            StringBuilder body = new StringBuilder();
       
            body.Append("<div class='row' style='padding:20px'>");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<center><h2><b>DISCHARGE SUMMARY</b></p></center>");

            //body.Append("<div class='row' style='text-align: justify'><p>Patient's Name..........................................................................UHID/Reg No. ................................ </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Age/Sex......................................................................... </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Date Of Admission.......................................Date Of Discharge......................................... </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Address... ........................................................................................................ </ p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p> Consultant Name: ................................................Qualification & Registration No........................</div><br>");
            //body.Append("<div class='row' style='text-align: justify'><p>Diagnosis : </p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Reasons for Admission : </p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Course of stay : </p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Details of procedure : </p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Condition at the time of Descharge : </p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Treatment Advice on Discharge : </p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Follow Up advice  : </p></div>");
            //body.Append("<div class='row' style='text-align: justify'><p>Doctor Name & Signature : </p></div><br>");

            body.Append("<table style='width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient's Name:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>UHID/Reg No.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender.ToString() + "</td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Date Of Admission</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + admitdate.ToString() + "</td>");
            body.Append("<td><b>Date Of Discharge</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Address</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Consultant Name:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>Qualification & Registration No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td style='margin-top:10px'><b>Diagnosis:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='margin-top:10px'><b>Reasons for Admission</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='margin-top:10px'><b>Course of stay</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='margin-top:10px'><b>Details of procedure</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='margin-top:10px'><b>Condition at the time of Descharge :</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='margin-top:10px'><b>Treatment Advice on Discharge</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='margin-top:10px'><b>Follow Up advice :</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='margin-top:20px'><b>Doctor Name & Signature:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='text-align: justify; margin-top:50px'>.................................................................................................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify'><p><center><b>Discharge summary has been explained to me in details in my language,<br>Patient/Attendant name & Signature </b></center></p></div>");
            body.Append("<div class='row' style='text-align: justify'><p><center><h3 style='height:60px;width:200px; border:1px solid black'></h3></center></p></div>");
            body.Append("</div>");
           
            return body.ToString();
        }

        //BLOOD PRINT FROM
        public string BloodRequisitionForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();

            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<h5 style='padding:10px;'>");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:120px; height:50px;margin-top:-5px'></h3></div>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:200px;margin-right:200px;margin-top:-10px'>BLOOD REQUISITION FORM</h3></div>");


            body.Append("<h3 style='color:black;height:85px;width:100%; border:1px solid black; margin-top:-35px'>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td>Patient Name:</td>");
            body.Append("<td style='height: 15px; width: 252px; border: 1px solid black;'>" + pname.ToString() + "</td>");
            body.Append("<b><td>&nbsp;Hospital:</td></b>");
            body.Append("<td style='height: 15px; width: 170px; border: 1px solid black;'>CHANDAN HOSPITAL</td>");
            body.Append("<td style='height: 15px; width: 170px; border: 1px solid black;'>OTHER HOSPITAL NAME</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td>&nbsp;Age/Gender:</td>");
            body.Append("<td style='height: 15px; width: 300px; border: 1px solid black;'>" + gender + "</td>");
            //body.Append("<b><td>Gender:</td></b>");
            //body.Append("<td style='height: 15px; width: 150px; border: 1px solid black;'></td>");
            body.Append("<b><td>&nbsp;Address:</td></b>");
            body.Append("<td style='height: 15px; width: 297px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td> &nbsp;UHID:</td>");
            body.Append("<td style='height: 15px; width: 170px; border: 1px solid black;'>" + uhidno.ToString() + "</td>");
            body.Append("<b><td>IPD.No:</td></b>");
            body.Append("<td style='height: 15px; width: 170px; border: 1px solid black;'>" + ipdno + "</td>");
            body.Append("<b><td>Ward:</td></b>");
            body.Append("<td style='height: 15px; width: 111px; border: 1px solid black;'></td>");
            body.Append("<b><td>Bed:</td></b>");
            body.Append("<td style='height: 15px; width: 111px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h3>");


            body.Append("<h3 style='color:black;height:85px;width:100%; border:1px solid black; margin-top:-12px'>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td> &nbsp;Consultant</td>");
            body.Append("<td style='height: 15px; width: 300px; border: 1px solid black;'></td>");
            body.Append("<b><td> &nbsp;Diagnosis:</td></b>");
            body.Append("<td style='height: 15px; width: 300px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td> &nbsp;Indication For Transfusion:</td>");
            body.Append("<td style='height: 15px; width: 578px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<b><td> &nbsp;Previous  Transfusion</td></b>");
            body.Append("<td style='height: 15px; width: 50px; border: 1px solid black; text-align:center'>Yes</td>");
            body.Append("<td style='height: 15px; width: 50px; border: 1px solid black; text-align:center'>NO</td>");
            body.Append("<b><td>IF Yes:</td></b>");
            body.Append("<td style='height: 15px; width: 110px; border: 1px solid black;'></td>");
            body.Append("<td style='height: 15px; width: 110px; border: 1px solid black;'></td>");
            body.Append("<td style='height: 15px; width: 110px; border: 1px solid black;'></td>");
            body.Append("<td style='height: 15px; width: 108px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h3>");

            body.Append("<h3 style='color:black;height:70px;width:100%; border:1px solid black; margin-top:-12px'>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<th>&nbsp;FOR FEMALE PATIENT ONLY</th>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<b><td> &nbsp;Pregnancy </td></b>");
            body.Append("<td style='height: 15px; width: 50px; border: 1px solid black; text-align:center'>Yes</td>");
            body.Append("<b><td> H/O of haemolytic <br/> disease in new born:</td></b>");
            body.Append("<td> &nbsp;</td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'>Yes</td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'>No</td>");
            body.Append("<b><td> H/O of still birth/ <br/> miscarriage:</td></b>");
            body.Append("<td> &nbsp;</td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'>Yes</td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'>No</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h3>");


            body.Append("<h3 style='color:black;height:75px;width:100%; border:1px solid black; margin-top:-12px'>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<th>&nbsp;COMPONENT AND QUANTITY</th>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");

            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>SDP</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");

            body.Append("<th>RDP</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");

            body.Append("<th>FFP</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");

            body.Append("<th>PRBC</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");

            body.Append("<th>CRYO PPT</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");

            body.Append("<th>OTHER:</th>");
            body.Append("<th style='height: 10px; width: 95px; border: 1px solid black; text-align:center'></th>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>&nbsp;</th>");
            body.Append("<th>UNITS:</th>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td style='height: 10px; width: 50px; border: 1px solid black; text-align:center'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h3>");


            body.Append("<h3 style='color:black;height:170px;width:100%; border:1px solid black; margin-top:-12px'>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<th>&nbsp;REQUIREMENT TIME</th>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 25px; width: 100px; border: 1px solid black; text-align:center'><b>IMEDIATE</br>(Lifesaving)</b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 25px; width: 100px; border: 1px solid black; text-align:center'><b>URGENT</b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 25px; width: 100px; border: 1px solid black; text-align:center'><b>ROUTINE</b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 30px; width: 140px; border: 1px solid black; text-align:center'><b>GROUPING</br>CROSS MATCHING</br> RESERVE</b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 25px; width: 138px; border: 1px solid black; text-align:center'><b>ONLY</br>GROUPING</br> CROSS MATCHING</b></td>");
            body.Append("</tr>");



            body.Append("<tr>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 100px; border: 1px solid black; text-align:center'>Saline Cross</br>Match</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 100px; border: 1px solid black; text-align:center'>Regular Cross</br>Match</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 100px; border: 1px solid black; text-align:center'>Regular Cross</br>Match</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 140px; border: 1px solid black; text-align:center'>Regular Cross</br>Match</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 138px; border: 1px solid black; text-align:center'>Regular Cross</br>Match</b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 100px; border: 1px solid black; text-align:center'>SUPPLY TIME </br><b>IMMEDIATE</b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 100px; border: 1px solid black; text-align:center'>SUPPLY TIME </br>60-90 mins</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 100px; border: 1px solid black; text-align:center'>SUPPLY TIME </br>>90 mins</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 140px; border: 1px solid black; text-align:center'>Reserved for 72 hours </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height: 20px; width: 138px; border: 1px solid black; text-align:center'>SUPPLY TIME </br> > 120 min after call</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h3>");

            body.Append("<div class='row' style='color:black;margin-left:40px;margin-right:200px; font-size:16px; margin-top:-5px'> Name Of voluntary/replacement donors:</div>");

            body.Append("<h3 style='color:black;height:60px;width:100%; border:1px solid black; margin-top:-1px'>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<th>&nbsp; CERTIFIED THAT I HAVE PERSONALLY COLLECTED THE BLOOD SAMPLES & CHECKED THE LABELS</th>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td>&nbsp;Date:</td>");
            body.Append("<td style='height: 15px; width: 100px; border: 1px solid black; text-align:center'></td>");
            body.Append("<td>Time: &nbsp;</td>");
            body.Append("<td style='height: 15px; width: 100px; border: 1px solid black; text-align:center'></td>");
            body.Append("<td>Name Of Doctor</td>");
            body.Append("<td style='height: 15px; width: 120px; border: 1px solid black; text-align:center'></td>");
            body.Append("<td>Sign of Doctor &nbsp;</td>");
            body.Append("<td style='height: 15px; width: 120px; border: 1px solid black; text-align:center'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h3>");
            body.Append("<h3 style='color:black;height:2px;width:100%; border:2px solid black; margin-top:-1px'></h3>");

            body.Append("<div class='row' style='color:black;text-align:center;margin-left:100px;margin-right:100px;margin-top:-3px; font-size:16px'> TO BE FILLED BY DEPARTMENT OF TRANSFUSION MEDICINE</div>");


            body.Append("<h3 style='color:black;height:100px;width:100%; border:1px solid black; margin-top:-1px'>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td>&nbsp;Booking No:</td>");
            body.Append("<td style='height: 15px; width: 90px; border: 1px solid black;'></td>");
            body.Append("<b><td>&nbsp;Date:</td></b>");
            body.Append("<td style='height: 15px; width: 90px; border: 1px solid black;'></td>");
            body.Append("<b><td>&nbsp;Time:</td></b>");
            body.Append("<td style='height: 15px; width: 90px; border: 1px solid black;'></td>");
            body.Append("<b><td>&nbsp;Blood Group (ABO & Rh): &nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 130px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td>&nbsp;Mode of Adjustment:&nbsp;&nbsp;&nbsp;</td>");
            body.Append("<td style='height: 15px; width: 598px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td>&nbsp;Issued on date:</td>");
            body.Append("<td style='height: 15px; width: 160px; border: 1px solid black;'></td>");
            body.Append("<b><td>&nbsp;Booking No:</td></b>");
            body.Append("<td style='height: 15px; width: 160px; border: 1px solid black;'></td>");

            body.Append("<b><td>&nbsp;Donor No: &nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 160px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='font-size:14px;'>");
            body.Append("<tr>");
            body.Append("<td>&nbsp; Amount collected:</td>");
            body.Append("<td style='height: 15px; width: 160px; border: 1px solid black;'></td>");
            body.Append("<b><td>&nbsp; Bill No:&nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 160px; border: 1px solid black;'></td>");

            body.Append("<b><td>&nbsp;Bill Date:&nbsp; &nbsp;</td></b>");
            body.Append("<td style='height: 15px; width: 165px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h3>");
            body.Append("</h5>");
            return body.ToString();
        }
        public string BloodTransfusionNoteForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:80px; margin-top:-15px' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:600px;margin-right:100px;text-align: justify ; margin-top:-125px; width:500px;height:55px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:14px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-20px'>BLOOD TRANSFUSION NOTE</h4></div>");

            body.Append("<table style='width:80%;font-size:15px;border:0px solid; margin-left:150px;margin-top:-10px'>");
            body.Append("<tr>");
            body.Append("<td>Previous BT Date</td>");
            body.Append("<td>:</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td> Blood Group with Rh Factor of pervious BT</td>");
            body.Append("<td>:</td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Blood Group with Rh Factor of Current Blood Bag</td>");
            body.Append("<td>:</td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Blood Group with Rh Factor of Patient</td>");
            body.Append("<td>:</td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Blood Bag Label & Compatibility Note checked By RMO</td>");
            body.Append("<td>:</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Blood Bag Label & Compatibility Note checked By SN</td>");
            body.Append("<td>:</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:93%;font-size:14px;border-collapse:collapse;border:1px solid;margin-left:30px;margin-top:-5px'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");

            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Blood Bag <br/> Number & Date Of <br/> Expiry</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Group/ <br/> Type</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>starting <br/>time &<br/> Date</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Ending <br/>time &<br/> Date</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Blood Bag Label & <br/>Compatibility Note checked<br/> By RMO(Name Emp.Code & Sign)</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Name & sign Of <br/>Person<br/> Administering</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Remarks/ Reaction</b><br/> In case of any reaction pls fill the BT Reaction Report/ in<br/> case no reaction please mention<br/><b> 'No REACTION'</b></th>");

            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; height:370px'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");

            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");

            //pdfConverter.Header_Enabled = false;
            //pdfConverter.Footer_Enabled = false;
            //pdfConverter.Header_Hight = 150;
            //pdfConverter.PageMarginLeft = 10;
            //pdfConverter.PageMarginRight = 10;
            //pdfConverter.PageMarginBottom = 10;
            //pdfConverter.PageMarginTop = 10;
            //pdfConverter.PageMarginTop = 10;
            //pdfConverter.Browser_Width = 1200;
            //pdfConverter.PageName = "A4";
            //pdfConverter.PageOrientation = "Landscape";
            return body.ToString();
        }
        public string VitalSignsChartForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
  
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:80px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:600px;margin-right:100px;text-align: justify ; margin-top:-105px; width:500px;height:55px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:14px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender : </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-20px'>VITAL SIGNS CHART</h4></div>");

            body.Append("<table style='width:95%;font-size:14px;border-collapse:collapse;border:1px solid;margin-left:30px'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b></b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Date</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Time</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Pulse <br/>Rate</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Blood <br/>Pressure</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Respiratory <br/>Rate</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Temperature</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Spo2</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Attendant <br/>name</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Attendant <br/>sign</b></th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b> &nbsp;Immediately after start of BT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b> &nbsp;10 minutes after start of BT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>&nbsp;20 minutes after Previous check</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");

       
            return body.ToString();
        }
        public string BloodTransfusionReactionReportForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            
            StringBuilder body = new StringBuilder();

            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<h5 style='padding:20px;'>");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-5px'></h3></div>");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:100px;text-align: justify ; margin-top:-120px; width:450px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:100px;margin-right:100px;margin-top:-10px'>BLOOD TRANSFUSION REACTION REPORT</h4></div>");


            body.Append("<table style='width:35%;font-size:16px; margin-left:30px'>");
            body.Append("<tr>");
            body.Append("<td><b>Any BT Reaction:</b> </td>");
            body.Append("<td>Yes</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style= 'height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("<td style='height: 15px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='font-size:16px; margin-left:30px;margin-top:15px'><i>To be used for investigation of suspected reaction to blood products (blood, platelets, FFP, cryoprecipitate, granulocytes).</i></div>");

            body.Append("<div class='row' style='font-size:15px; margin-left:30px;margin-top:15px'><b>Please complete form and forward with appropriate blood specimens and used blood bag to the Blood Bank.</b></div>");

            body.Append("<table style='width:80%;font-size:16px; margin-left:30px;margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>Ward/Dept</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td>Date</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Clinical Diagnosis</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Product being transfused</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Time commenced</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Blood Bag Number:</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Volume transfused:</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td>mis</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:95%;font-size:14px;border-collapse:collapse;border:1px solid;margin-left:30px'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");

            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Check </b>(please circle)</th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'></th>");
            body.Append("<th colspan='2' style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Temperature in the 24hrs prior <br/>to transfusion:</b> (Please tick) </th>");

            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");

            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp; Patients Id correct</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;Yes/No</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center '>FEBRILE</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'>AFEBRILE</td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");

            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp; Blood Bag Correct</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;Yes/No</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");

            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp; Blood Transfusion Record correct</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;Yes/No</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center '></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("</table>");


            body.Append("<table style='width:95%;font-size:14px;border-collapse:collapse;border:1px solid;margin-left:30px; margin-top:5px'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;'><b>Vital Signs</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Time</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Temperature </b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>B.P</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Respiration</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Pulse</b></th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;<b> Pre-Reaction</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;<b> At time of Reaction</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:17px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>&nbsp;<b>Signs & Symptoms - Please Tick</b></ td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>&nbsp;<b>Fever</b></ td>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;<b>Low Back Pain</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;<b>skin Pallor</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>&nbsp;<b>Chils</b></ td>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;<b>Chest Pain</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;<b>Dark Urine</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>&nbsp;<b>Nausea/ Vomiting</b></ td>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;<b>Anxiety</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;<b>Dyspnoea</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td>&nbsp;<b>Hives/Itching</b></ td>");
            body.Append("<td></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;<b>Headache</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;<b>Bleed from wound/or IV Site/<br/> Other(specify below)</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            //body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            //body.Append("<td>&nbsp;<b>Fever</b></ td>");
            //body.Append("<td><h3 style='height: 10px; width: 15px; border: 1px solid black;'></h3></td>");
            //body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;<b>Low Back Pain</b></td>");
            //body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;<b>skin Pallor</b></td>");
            //body.Append("<td></td>");
            //body.Append("<td></td>");
            //body.Append("<td></td>");
            //body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");
            return body.ToString();
        }
        public string BlooadReleaseForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {

            StringBuilder body = new StringBuilder();
       
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<h5 style='padding:20px;'>");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:120px; height:50px;margin-top:-5px'></h3></div>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:200px;margin-right:200px;margin-top:-10px'>BLOOD RELEASE FORM</h3></div>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:250px;margin-right:250px;margin-top:-10px'>(To Be filled by Nurse)</h4></div>");



            body.Append("<table style='width:100%;margin-top:-1%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient's Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + pname + "</td></b>");


            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Hospital</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td><b>Ward</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Bed</b></td>");

            body.Append("<b><td></td></b>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:70%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>Name of Consultant</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Quantity of blood transfused</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>TRC Number</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");



            body.Append("<tr>");
            body.Append("<table style='width:70%;font-size:14px;margin-top:5%'>");
            body.Append("<tr>");
            body.Append("<td><b>Nurse signature</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Nurse name</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td><b> Date</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td><b>Time</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</tr>");


            body.Append("</table>");
            body.Append("</h5>");
            
            return body.ToString();
        }
        public string DischrgeFromForBloodBankForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<h5 style='padding:20px;'>");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:120px; height:50px;margin-top:-5px'></h3></div>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:200px;margin-right:200px;margin-top:-10px'>BLOOD RELEASE FORM</h3></div>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:250px;margin-right:250px;margin-top:-10px'>(To Be filled by Nurse)</h4></div>");



            body.Append("<table style='width:100%;margin-top:-1%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient's Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + pname + "</td></b>");


            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Hospital</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td><b>Ward</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Bed</b></td>");

            body.Append("<b><td></td></b>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:70%; font-size:14px'>");
            body.Append("<tr>");
            body.Append("<td><b>Name of Consultant</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Date of admission</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");

            body.Append("<td><b>Date of Discharge</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");

            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td><b>Quantity of blood transfused</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>TRC Number</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");



            body.Append("<tr>");
            body.Append("<table style='width:70%;font-size:14px;margin-top:5%'>");
            body.Append("<tr>");
            body.Append("<td><b>Nurse signature</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Nurse name</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td><b> Date</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td><b>Time</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            return body.ToString();
        }


        //CathLab

        public string InPatientHistoryAndPhysicalRecordForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            
            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-110px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-5px'>IN-PATIENT HISATORY AND PHYSICAL RECORED</ h4></div>");
            body.Append("<div class='row' style='color:black;margin-top:4%; margin-left:20px;margin-right:25px;font-size:13px; margin-top:-5px'><b>Date and Time Of Admission:</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Consultant:__________________________________________________ Specialty: ___________________________________</b></div>");

            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Source Of History:________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Chief Complaints_________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:4%; margin-left:20px;margin-right:25px;font-size:13px;'><b>History of Present illness:_________________________________________________________________________________</b></div>");

            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Past Medical History:_____________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:4%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Past Surgical History:(Year/procedure/Hospital/Anesthesia): ______________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:4%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Patient Family History:___________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");

            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b><label>Psychosocial History:</label><label style='margin-left:120px'>Smoke</label><label style='margin-left:120px'>None/Yes(Specify)_______________________________</label></b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b><label>Substance Abuse</label><label style='margin-left:145px'>Tobacco</label><label style='margin-left:115px'>None/Yes(Specify)_______________________________</label></b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b><label></label><label style='margin-left:240px'>Alcohol</label><label style='margin-left:115px'>None/Yes(Specify)_______________________________</label></b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b><label></label><label style='margin-left:240px'>Drug</label><label style='margin-left:124px'>None/Yes(Specify)_______________________________</label></b></div>");

            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Occupation:____________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Allergies:______________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");

            body.Append("</h5>");
            return body.ToString();
        }
        public string MenstrualHistoryForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePathh = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/download.png");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-110px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row' style='color:black;font-size:15px;margin-top:2%; margin-left:20px;margin-right:20px'><label>Menstrual History: Age At Menarche_______________</label><label>Age at Menopause________________</label><label>Cycle:Regular/Irregular</label></b></div>");

            body.Append("<h5 style = 'border:1px solid;margin-left:35px;margin-right:50px;text-align: justify ; width:650px;height:150px; padding:10px;font-size:15px;'>&nbsp;Relevant History/ Notes for specialities(Ob/Gyn,ENT,Ophthalmology,Dental etc.)</h5>");
            body.Append("<div class='row' style='color:black;margin-top:4%; margin-left:20px;margin-right:25px;font-size:13px;'><b>Current Medications:_____________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;font-size:14px;margin-top:2%; margin-right:20px;text-align:center;'><b><u>PHYSICAL EXAMINATION</u></b></div>");

            body.Append("<table style='width:60%; font-size:14px;margin-right:50px;margin-left:100px;'>");
            body.Append("<tr>");

            body.Append("<td><b>Vulnerable Patient:</b></td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>l</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Restraint:</b></td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Yes</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>No</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:95%; font-size:15px;margin-right:50px;margin-top:5px;margin-left:20px;'>");
            body.Append("<tr>");
            body.Append("<td>Counscious</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Drowsy</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Unresponsive</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:90%; font-size:14px;margin-right:35px;margin-left:40px; border: solid 1px;border-collapse: collapse;text-align:center; margin-top:5px'>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:20%'><b>B.P(mmHg)</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:20%'><b>Pulse (per min)</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:20%'><b>Resp .rate(per min)</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:20%'><b>Temp.(F)</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:20%'><b>SpO2(%)</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;text-align:center; height:50px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<h5 style = 'border:1px solid;margin-left:40px;text-align: justify ; width:200px;height:25px; font-size:15px;text-align:center;'>Pain Score:</h5>");
            body.Append("<h5 style = 'margin-left:250px;text-align: justify; margin-top:-50px; width:300px;height:40px; font-size:15px;text-align:left;'><img src='" + imagePathh + "' style='width:300px; height:40px;' /></h5>");

            body.Append("<table style='width:95%; font-size:15px;margin-right:20px;margin-top:5px;margin-left:20px;'>");
            body.Append("<tr>");
            body.Append("<td><b>General Description</b></td>");
            body.Append("<td>Pallor</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Icterus</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Cyanosis</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Raised JVP</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Lymphadenopathy</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Pedal Edema</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;font-size:15px;margin-top:2%; margin-left:21px;margin-right:20px'>REVIEW OF SYSTEMS</div>");


            body.Append("<table style='width:70%; font-size:15px;margin-right:50px;margin-top:5px;margin-left:20px;'>");
            body.Append("<tr>");
            body.Append("<td style='font-size:15px;'>Respiratory system:____________________________________________ NAD</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='font-size:15px; color:black'>Cardiiovascular system:_________________________________________ NAD</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Per Abdomen:________________________________________________ NAD</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='font-size:15px;'>Centerl nervous system:_________________________________________ NAD</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='font-size:15px;'>Any other finding:______________________________________________ NAD</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='font-size:15px;'>Local Examination:_____________________________________________ NAD</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            return body.ToString();
        }
        public string ProvisionalDiagnosisForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-110px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row' style='color:black;margin-top:4%; margin-left:20px;margin-right:25px;font-size:13px;'><b>PROVISIONAL DIOGNOSIS:_______________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>INVESTIGATION ADVISED:_______________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>TREATMENT:___________________________________________________________________________________________</b></div>");

            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>DIET ADVISED</b></div>");

            body.Append("<table style='width:80%; font-size:14px;margin-right:35px;margin-left:40px;'>");
            body.Append("<tr>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Normal Diet</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Liquid Diet</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Smi Solid Diet</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>NPO</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Special Diet(Please Specify)</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;font-size:14px;margin-top:2%; margin-right:20px;text-align:center;'><b>PLAN OF CARE</b></div>");
            body.Append("<table style='width:95%; font-size:14px;margin-right:35px;margin-left:20px; border: solid 1px;border-collapse: collapse;text-align:center;'>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:33%'>Medical</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:33%'>Surgical</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:34%'>Any Other /Investigation</td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;text-align:center; height:100px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='color:black;font-size:18px;margin-top:3%; margin-left:20px;margin-right:20px'>Preventive Aspects/Restrictions/Special Precautions(Like restraint, Drug interactions etc):________</div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px;font-size:13px;'><b>______________________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;font-size:18px;margin-top:5%; margin-left:20px;margin-right:20px'><b><label>Doctor's Signature:_____________________</label><label>Date:_______________</label><label>Time:________________</label></b></div>");


            body.Append("</h5>");
            return body.ToString();
        }
        public string ReviewOfDocumentationForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            string _result = string.Empty;
            StringBuilder body = new StringBuilder();
            StringBuilder h = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-110px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");


            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-10px'>REVIEW OF DOCUMENTATION</ h4></div>");
            body.Append("<div class='row' style='color:black;text-align:center;font-size:14px;margin-top:-2%'>(To be countersigned by the treating consultant within 24 hours of admission)</div>");

            body.Append("<div class='row' style='color:black;font-size:18px;margin-top:2%; margin-left:20px;margin-right:20px'>After reviewing this history & physical examination of this patient and discussing the above with the doctor responsible for completing this examination. I have the following additions/modifications or other notes or findings.</div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px'><b>____________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px'><b>____________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px'><b>____________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px'><b>____________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px'><b>____________________________________________________________________________________________</b></div>");
            body.Append("<div class='row' style='color:black;margin-top:2%; margin-left:20px;margin-right:25px'><b><label>____________________________________________________</label></b></div>");
            body.Append("<div class='row' style='color:black;font-size:18px;margin-top:3%; margin-left:20px;margin-right:20px'>I have also reviewed the contents of the Nursing Assessment and taken that data into consideration.</div>");
            body.Append("<div class='row' style='color:black;font-size:18px;margin-top:8%; margin-left:20px;margin-right:20px'><b><label>Consultant's Signature:_____________________</label><label>Date:_______________</label><label>Time:______________</label></b></div>");

            body.Append("</h5>");
            return body.ToString();
        }
        public string NursingInitalAssessmentFormForWardForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePathh = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/download.png");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-110px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-20px'>NURSING INITIAL ASSESSMENT FORM FOR WARD</ h4></div>");

            body.Append("<table style='width:95%;font-size:15px;border-collapse:collapse;border:1px solid;margin-left:20px;'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:50px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:15%'>TEMP:</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:15%'>Pulse:</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:15%'>BP:</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:25%'>Respiration:</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:15%'>SPO₂:</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:15%'>Weight:</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<table style='width:60%;font-size:15px;margin-left:20px;margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td><b>Level of Consciousness:</b> </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Conscious / Drowsy / Unconscious</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Allergies/ Adverse Reactions:</b> </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Ongoing medication:</b> </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:95%;font-size:15px;border-collapse:collapse;margin-left:20px;'>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:30%;text-align:center;'>Medication</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align:center;'>Dose</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:20%;text-align:center;'>Frequency</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:30%;text-align:center;'>Date/Time of last dose</td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row'><h4 style='color:black;text-align:left;margin-left:20px;font-size:15px;margin-top:-2px'>Pain Assessment:</ h4></div>");
            body.Append("<img src='" + imagePathh + "' style='width:400px; height:80px;margin-left:20px;'  />");
            body.Append("<div class='row'><h4 style='color:black;text-align:left;margin-left:20px;font-size:15px;margin-top:-2px'>Nursing Needs:</ h4></div>");

            body.Append("<table style='width:80%;font-size:15px;margin-left:50px;margin-top:-10px'>");
            body.Append("<tr>");
            body.Append("<td style='width:2%;text-align:center;'>1.</td>");
            body.Append("<td style='width:88%;'>Is there a language problem?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='width:10%;'>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>2.</td>");
            body.Append("<td>Are there any cultural /religious barriers?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>3.</td>");
            body.Append("<td>Is the patient at risk of falls?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>4.</td>");
            body.Append("<td>Is the patient incontinent?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>5.</td>");
            body.Append("<td> Does patient require oxygen therapy?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>6.</td>");
            body.Append("<td> Does patient have RT/Foleys/tracheostomy/ET/chest drain/central line?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>7.</td>");
            body.Append("<td style=''>Does the patient have pressure ulcer or is at risk for pressure ulcers?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>8.</td>");
            body.Append("<td> Does patient have any special nutrition needs?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>9.</td>");
            body.Append("<td>Does the patient have implants?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>10.</td>");
            body.Append("<td>Is the patient vulnerable?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>11.</td>");
            body.Append("<td>Does isolation precaution need to be taken?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:center;'>12.</td>");
            body.Append("<td>Any other needs?</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>YES/NO</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-left:20px;font-size:15px;margin-top:-2px'><b>Diet:</b>Normal/Liquid Diet/Semi solid diet/Salt Restricted Diet/Diabetic Diet/Renal Diet/other</div>");

            body.Append("<div class='row' style='color:black;text-align:left;margin-left:20px;font-size:15px;margin-top:-2px'><b>Provisional Diagnosis:</b></div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-left:20px;font-size:15px;margin-top:-2px'><b>Care Plan:</b></div>");
            body.Append("<table style='width:80%;font-size:15px;margin-left:20px;margin-top:30px'>");
            body.Append("<tr>");
            body.Append("<td><b>Form completed by:</b></td>");
            body.Append("<td>Name:</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Signature:</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td></td>");
            body.Append("<td>Date:</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Time:</td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");


            body.Append("</table>");

            body.Append("</h5>");
           
            return body.ToString();
        }
        public string ConsultaionServicesForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
        
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:80px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:600px;margin-right:100px;text-align: justify ; margin-top:-90px; width:500px;height:55px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:14px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender : </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-10px'>CONSULTATIONS/SERVICES</h4></div>");

            body.Append("<table style='width:95%;font-size:15px;border-collapse:collapse;border:1px solid;margin-left:30px; margin-top:-10px'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Date</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Time</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Consultation/Services</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Ward</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Doctor name</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>Signature</b></th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            return body.ToString();
        }
        public string NutritionalAssessmentScreeningForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
           
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-110px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");


            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-20px'>NUTRITIONAL ASSESSMENTSCREENING FORM</ h4></div>");
            body.Append("<div class='row'><h5 style='color:black;text-align:center;font-size:11px;margin-top:-2%'>Nutritional Assessment for each patient shall be carreed out within 24 Hours of patient being admitted. </ h5></div>");
            body.Append("<div class='row'><h5 style='color:black;text-align:center;font-size:11px;margin-top:-2%'>Tick whatsoever is applicable</ h5></div>");

            body.Append("<table style='width:98%;font-size:15px;margin-left:20px;margin-right:30px;;margin-top:-2%'>");
            body.Append("<tr>");
            body.Append("<td style='width:80%;'>Is the patient on Warfarin/MAOI/Ciprofloxacin/Tetracycline/Dokycycline: </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='width:10%;'>Yes/No</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='width:80%;'>If 'Yes' provide for relevant Patient Education regarding Food and Drug Interaction:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='width:10%;'>Yes/No</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<table style='width:98%;font-size:15px;margin-left:20px;margin-right:30px;'>");
            body.Append("<tr>");
            body.Append("<td>Appetite:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td> >50%/very poor </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Weight:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Height:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>BMI:</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='font-size:15px;margin-left:20px;'>Diet Prescription Instruction by Treating Consultant: Yes/No (Specify if any)_______________________</div>");
            body.Append("<div class='row' style='font-size:15px;margin-left:20px;'><b>Evaluation Criteria: (Tick all that apply)</b></div>");
            body.Append("<table style='width:95%;font-size:15px;border-collapse:collapse;border:1px solid;margin-left:20px;'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>Sr.no</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center;width:45%'><b>ADULTS</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center;width:45%'><b>PAEDIATRICS</b></th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>1</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Weight Loss>5kg/Month</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*Malnutrition/Anemia/Obesity/Weight Loss</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>2</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Difficulty Chewing and Swallowing</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*Lactose/wheat Intolerance</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>3</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Diabetes Mellitus</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*Multiple Trauma or Burn or Stress</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>4</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Renal Failure/ Renal Calculi</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*Inborn Errors or Metabolism</td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>5</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Hepatic Dysfunction</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*Renal Failure</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>6</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Cardiac Disorder/Hypertension</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;* Hepatic Dysfunction </ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>7</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Malnutrition/Cachexia/Anaemia/Obese</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;* Diarrhea and / or Vomiting </ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>8</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Full term Pregnancy/Normal Delivery/Caesarian <br/> &nbsp;  Section</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;for Days/Consumption</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>9</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Multiple Trauma or Burns or Stress</td>");
            body.Append("<td>&nbsp;&nbsp;*New Onset Diabetes Wheat/Milk </ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>10</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Peptic Ulcer GI Bleed / Ulcerative Colitis </td>");
            body.Append("<td>&nbsp;&nbsp; or any other Food Allergy and any</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>11</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Diagnosed Cancer and Undergoing Treatment</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*other treatment</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>12</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Enteral or Parenteral Nutrition Support</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*In Wearing period/Lactation Period</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>13</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Prolonged Fever / Tuberculosis / Asthmatic with<br/>&nbsp; Special treatment </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*Admitted in the ICU/NICU</br>&nbsp;&nbsp;*Enteral or Parenteral Nutrition Support</ td>");

            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>14</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Pancreatitis/Pre and Post Laparotomy</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;*Prolonged Fever</ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>15</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Gluten/Milk Intolerance/Food Allergy Crohn's <br/> &nbsp; Disease</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;</ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>16</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;Pregnancy with Complications if any</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>&nbsp;&nbsp;</ td>");
            body.Append("</tr>");
            body.Append("</table>");



            body.Append("<table style='width:100%;font-size:15px; margin-left:20px'>");
            body.Append("<tr>");
            body.Append("<td style='width:50%'>Any Other Criteria:</td>");
            body.Append("<td></ td>");
            body.Append("<td>&nbsp;</ td>");
            body.Append("<td>&nbsp;</ td>");
            body.Append("<td style='width:50%'>Any other Criteria:</ td>");
            body.Append("<td></ td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:100%;font-size:15px; margin-left:20px;margin-top:20px'>");
            body.Append("<tr>");
            body.Append("<td><b>Nutritional Intervention Required: Yes / NO</b></td>");
            body.Append("<td></ td>");
            body.Append("<td>&nbsp;</ td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:100%;font-size:15px; margin-left:20px;margin-top:20px'>");
            body.Append("<tr>");
            body.Append("<td style='width:50%'><b>Date/Time</b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</ td>");
            body.Append("<td>&nbsp;</ td>");
            body.Append("<td style='width:50%'><b>Name & Signature</b></ td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            return body.ToString();
        }
        public string NutritionalReAssessmentForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
         
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-110px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
           body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
           body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-20px'>NUTRITIONAL RE-ASSESSMENT FORM</ h4></div>");

            body.Append("<table style='width:100%;font-size:14px;margin-left:50px;margin-right:30px;'>");
            body.Append("<tr>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Calories:</b> </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Proteins:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Any Specific Requirement:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:95%;font-size:14px;border-collapse:collapse;border:1px solid;margin-left:30px;margin-right:30px;margin-top:10px>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:10%'><b>S.No</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:15%'><b>DATE <br/> TIME</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:45%'><b>REASSESSMENT<br/>(Type of Diet/feed & calorie Description etc)</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:15%'><b>DONE BY<br/>(Name & Sign)</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:15%'><b>REMARKD</b></th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:100px;width:100%'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:100px;width:100%'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:100px;width:100%'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:100px;width:100%'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>4</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:100px;width:100%'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>5</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:100px;width:100%'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>6</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            
            return body.ToString();
        }
        public string NursingCarePlanForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:80px; margin-top:-15px' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:600px;margin-right:100px;text-align: justify ; margin-top:-125px; width:500px;height:55px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:14px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:150px;margin-right:150px;  margin-top:-20px'>NURSING CARE PLAN</ h4></div>");

            body.Append("<table style='width:95%;font-size:14px;border-collapse:collapse;border:1px solid;margin-left:30px;margin-right:30px;margin-top:-5px'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:10%'><b>DATE AND <br/> TIME</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:20%'><b>ASSESSMENT</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:20%'><b>PLAN</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:20%'><b>IMPLEMENTATION</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:20%'><b>OUTCOMES</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center; width:10%'><b>NAME & </br>SIGH OF <br/>NURSE</b></th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:370px;width:100%'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");
            return body.ToString();
        }
        public string NurseHandOverRecordForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePathh = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/download.png");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-110px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            
            body.Append("<table style='width:95%;font-size:15px;border-collapse:collapse;border:1px solid;margin-left:20px;'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='font-size:13px;text-align:center;width:100%'><b>NURSE HANDOVER RECORD</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td  style='font-size:11px;text-align:left;width:100%'><b>PROVISIONAL DIAGNOSIS</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='font-size:11px;text-align:center;'><b>PAITENT ASSESSMENT</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:95%;font-size:11px;border-collapse:collapse;margin-left:20px;'>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25%'><b>GCS: E  V  M</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b>OXYGEN REQUIRMENT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25%'><b>FLOW:</b></td>");
            body.Append("<td colspan='2'style='border: solid 1px;border-collapse: collapse; width:30%'><b>ROUTE:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>BP:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>HR:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>RR:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>TEMP:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>SPO2 RA:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b>LEVEL OF CONSCIOUSNESS:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>CONSCIOUS</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>DROWSY</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>UNCONSIOUS</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b>ALTERED</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>PAIN ASSESSMENT</b></td>");
            body.Append("<td colspan='4'style='border: solid 1px;border-collapse: collapse;text-align:left;'><img src='" + imagePathh + "' style='width:300px; height:50px; padding:2px' /></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>ANAJGESIA</b></td>");
            body.Append("<td colspan='4'style='border: solid 1px;border-collapse: collapse;text-align:left;'></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>RISK OF FALL:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>INCONTIONENCE</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>VULNERABLE:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>PRESSURE ULCER:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>AREA</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>IV LINE:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>SITE & INSERTION DATE:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>FOLEYS CATHETER:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>RT:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>DATE:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b> CATHETER REMOVAL:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>HEAD OF BED ELEVATION:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>ANY OTHER DRAIN</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>SPECIFY</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td colspan='5'style='border: solid 1px;border-collapse: collapse;text-align:center; height:25px;font-size:12px;'><b>ICU HANDOVER ONLY</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b> MECHANICAL VENTILATION:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b><label>NIV</label><label style='margin-left:20px'>IMV</label><label style='margin-left:20px'>HFNC</label></b></td>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>SETTING:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td  colspan='2'style='border: solid 1px;border-collapse: collapse'><b>SPONTANEOUS BREATHING TRIAL:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b><label style='margin-left:20px'>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;text-align:left;'><b></b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;text-align:left;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>IONOTROPIC DRUG STATUS:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td colspan='2' style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>THIROMBOEMBOLIC PROPHYLAXIS:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>ULCER PROPHYLAXIS:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>BOWEL REGIMEN</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse'><b>SEDATIVE DRUG STATUS:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b>GLYCEMIC CONTROL</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'><b><label>Y</label><label style='margin-left:20px'>N</label></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:left;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td colspan='5'style='border: solid 1px;border-collapse: collapse;text-align:center; height:25px;font-size:12px;'><b>INVESTIGATIONS/REFERENCES/MEDICATION PRESCRIBED DURING SHIFT</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");

            body.Append("<td rowspan='6' style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>RADIOLOGY:</b></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>INVESTIGATION</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>DONE</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>PEDING</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>NO OF FILMS</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>X-RAY:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>CT:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>MRI:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>USG:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>DOPPLER</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td rowspan='11' style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>PATHOLOGY<br/>MICROBIOLOGY<br/>HISTOPATHOLOGY</b></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>INVESTIGATION</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>SENT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>PEDING</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");



            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td rowspan='5' style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>REFERENCES</b></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>DOCTOR/DEPARTMENT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>DONE</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>PEDING</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>REMARKS</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:20px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td rowspan='6' style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>MEDICATION:</b></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>DRUG NAME</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>DOSE</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>ROUTE</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>FREQUENCY</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse; height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;height:15px'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td colspan='6' style='border-left: 1px solid;border-collapse: collapse;'><b>DIET:</b></ td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td colspan='5' style='border: solid 1px;border-collapse: collapse;'><b>REMARKS:</b></ td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;'><b>HANDOVER GIVEN BY:</b></ td>");
            body.Append("<td colspan='2' style='border: solid 1px;border-collapse: collapse;'><b>HANDOVER GIVEN BY:</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td  colspan='3'style='border: solid 1px;border-collapse: collapse;'><b>NAME:</b></ td>");
            body.Append("<td colspan='2' style='border: solid 1px;border-collapse: collapse;'><b>NAME:</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;'><b>CHL:</b></ td>");
            body.Append("<td colspan='2' style='border: solid 1px;border-collapse: collapse;'><b>CHL:</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;'><b>DATE & TIME:</b></ td>");
            body.Append("<td  colspan='2' style='border: solid 1px;border-collapse: collapse;'><b>DATE & TIME:</b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");
            return body.ToString();
        }
        public string IformedConsentCoronaryAngiographyHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-3%;'></h3></div>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:100px;margin-right:100px;width:600px;margin-top:-2%'>सुचित सहमति प्रपत्रः कोरोनरी एन्जियोग्राफी</ h4></div>");
            body.Append("<div class='row' style='text-align: justify; font-size:14px;margin-left:2px;margin-top:-2%'><b>दिनांक......................</b></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b> रोगी का नाम</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>"+ pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आयु/लिंग</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आईपी संख्या </b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>यूएचआईडी संख्या</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>भर्ती होने का तिथि,</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>परामर्शदाता चिकित्सक</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>निदान</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>प्रक्रिया का विवरण:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>हृदय की रक्त धमनियों की रचना एवं उनमें उपस्थित रुकावट अथवा सिकुडन को जानने के लिये कोरोनरी एन्जियोग्राफी की जाती है। इसके लिये दांयी अथवा बांयी रेडियल या फीमोरल धमनी' मैं, जो हाथो पर पैरों को रक्त प्रदान करती है, एक मध्यम आकर की सुई से Local Anesthesia में एक अत्यन्त सूक्ष्म छेद किया जाता है। तत्पश्चात् एक्स-रे की मदद से देखते हुए एक ट्यूब जिसे Catheter कहते है, को हृदय की रक्त धमनियों के मुहाने पर ले जाते है। इस ट्यूब के जरिये एक विशेष प्रकार के आयोडीन युक्त Dye/Contrast Medicine को धमनियों में प्रवाहित करते हैं और हृदय की कई धमनियों की श्रृंखला में बहाव को विडियो रिकार्ड किया जाता है। Contrast Medicine की कुछ मात्रा को हृदय के मुख्य पम्पिंग चैम्बर Left Ventricle में इजेक्ट करते है। इस प्रकार हृदय को रक्त की अपूर्ति करने वाली समस्त धमनियों के कई चित्र लिए जाते हैं और उनको इसी समय देख/विश्लेषण कर यह तत्काल पता हो जाता है कि किस मुख्य धमनी/धमनियों और उनकी शाखाओं में कहाँ और कितनी बड़ी रुकावट/अवरोध है। इस विधि को एन्जियोग्राफी कहते है सम्पूर्ण प्रकिया में रोगी होश में रहता है तथा स्वयं भी टीवी मोनीटर पर यह कार्य देख सकता है।</div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>मैं प्रक्रिया के फोटोग्राफी (फोटो / वीडियो / टेलीविज़न) को देखने के लिए सहमत हूं। मैं वैज्ञानिक प्रकाशनों के लिए साझा किए जाने वाले अपने नैदानिक विवरणों से भी सहमत हूं बशर्तें मेरी पहचान का खुलासा न हो।</div>");


            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;margin-top:10px'><b>जटिलताय और जोखिमः </b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. 20 में 01 रोगी में रेडियल या फीमोरल धमनी की पंक्चर सूजन या खून का थक्का जम जाना।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. 100 में 01 रोगी में हृदय की धड़कन बिगडना, रेडियल या फीमोरल धमनी क्षति के लिये ऑपरेशन।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>3. 1000 में से 01 रोगी में लकवा, हृदयघात से खतरनाक रिएक्शन, अर्जेन्ट बायपास तथा एन्जिर्याप्लास्टी की आवश्यकता, अथवा मृत्यु ।</div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>लाभ: </b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. अवरोध और बीमारी की स्थिति को और जानने के लिए।</div>");


            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>विकल्प:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. कंजर्वेटिव प्रबंधन (उदाहरण के लिए दवाओं द्वारा थक्के को विघटित करना) </div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. कोरोनरी सी.टी. </div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'>उपर्युक्त उपचार विधियों के फायदे, जोखिम तथा संभावित जटिलताओं के विषय में, उस भाषा जिसे मैं अच्छी तरह समझता/समझती हूँ, में विस्तार से बता दिया गया है। मैं उक्त उपचार विधि तथा उसके किए जाते समय आवश्यकता पड़ने पर एनेस्थिसिया अथवा किसी भी तरह के अन्य आकस्मिक उपचार हेतु स्वीकृति देता /देती हूँ।</ div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px; margin-top:5px'>");
            body.Append("<tr>");
            body.Append("<td>(1) चिकित्सक नाम ________________________</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(1) रोगी का नाम ____________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td> चिकित्सक हस्ताक्षर________________________ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>रोगी का हस्ताक्षरः _____________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>(2)कैथटेक्नीशियन का नाम ______________</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(2) रोगी के परिजन का नाम ____________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>टेक्नीशियन हस्ताक्षरः________________________ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>रोगी से सम्बंधः ______________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td> </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>हस्ताक्षरः__________________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</div>");
            
            return body.ToString();
        }
        public string IformedConsentCoronaryAngiographyEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {

            PdfGenerator pdfConverter = new PdfGenerator();
            string _result = string.Empty;
            StringBuilder body = new StringBuilder();
            StringBuilder h = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-3%;'></h3></div>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:100px;margin-right:100px;width:600px;margin-top:-2%'>Informed Consent - Coronary Angiography</ h4></div>");
            body.Append("<div class='row' style='text-align: justify; font-size:14px;margin-left:2px;margin-top:-2%'><b>Date:......................</b></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>"+ pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>DOA.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>UNID No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>Detail of Procedure:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>Coronary angiography is done to know the composition of the heart's blood arteries and obstruction or contraction in them. For this, in the right or left radial or femoral artery, which provides blood to the hand or feet, a very small hole is made under local anesthesia. After that, with the help of X-rays, a tube called the catheter, leads to the heart of the heart's arteries. Through this tube, a special type of iodized Dye/Contrast medicine flows in the arteries and in the series of many cardiac arteries, the video is recorded to view the blockages. Some amount of contrast medication is injected into the Left Ventricle, the main pumping chamber of the heart. In this way many pictures of all the arteries can be seen immediately and analyzed as to where and how many obstructions are present. This procedure is called angiography. During this procedure the patient remains conscious and can also see the procedure being performed on the TV monitor.</div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>I agree to observing, photography (still/video/televising) of the procedure. I also agree to my clinical details being shared for scientific publications provided my identity is not disclosed.</div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;margin-top:10px'><b>Complications & Risk:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1.Bleeding, swelling or blood clots of radial or femoral artery in 01 in 20 patients.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. irregular heartbeat, radial or femoral artery damage in 01 in 100 patients.</ div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>3. 1 patient out of 1000 have the risk of stroke, adverse reaction to cardiovascular drugs, require urgent bypass or angioplasty and death.</ div>");


            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>Benefits:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. To know the status of the blockage and disease further.</div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>Alternatives:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1.Conservative management(eg.Dissolving the clot by medicines) </div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. Coronary C.T.</ div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'>The advantages, risks and potential complications of the above-mentioned treatment methods have been explained to me in detail in the language that I understand very well.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>I approve of the said treatment method and anesthesia or any other contingency treatment, if necessary, during its operation.</div>");

            body.Append("<table style='width:100%; font-size:15px;margin-right:20px; margin-top:5px'>");
            body.Append("<tr>");
            body.Append("<td>(1) Doctor name ________________________</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(1) Patient's name ____________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td> Doctor's signature________________________ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Patient's signature _____________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>(2) Name of the Cath Technician ______________</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(2) Name of the patient's family. ____________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td> Technician Signature________________________ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td> Relation to Patient ______________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td> </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Signature__________________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("</div>");
            return body.ToString();
        }
        public string IformedConsentPTCAHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-3%;'></h3></div>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:100px;margin-right:100px;width:600px;margin-top:-2%'>सुचित सहमति प्रपत्रः कोरोनरी एन्जियोप्लास्टी (PTCA)</ h4></div>");
            body.Append("<div class='row' style='text-align: justify; font-size:14px;margin-left:2px;margin-top:-2%'><b>दिनांक......................</b></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b> रोगी का नाम</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>"+pname+"</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आयु/लिंग</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आईपी संख्या </b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>यूएचआईडी संख्या</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>भर्ती होने का तिथि,</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>परामर्शदाता चिकित्सक</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>निदान</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>प्रक्रिया का विवरण:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>हृदय की रक्त धमनियों में उपस्थित रुकावट या सिकुडेपन को ठीक करने के लिए 'कोरानरी 'एन्जियोप्लास्टी नामक विधि के दवारा स्कावर को दूर करके धमनी में रक्त का सामान्य प्रवाह किया जाता है।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>यह उपचार विधि Catheterization Laboratory में Angiography की भांति की जाती है एक Tube / Catheter कलाई के पास रेडियल अथवा मेर की फीमोरन धमनी के रास्ते इदय की बीरग्रस्त धमनी के मुहाने पर से आते है। पूरा कार्य एक्स-रे की सहायता से देखते हुए किया जाता है। इस के पश्चात एक बहुत ही महीन तार जिसके ऊपर एक बैलून व स्टेन्ट की धमनी के संकुचित भाग में ले जा फुलाया जाता है, जिसके साथ-साथ स्टेन्ट भी खुल जाता है, बैलून धमनी में रुकावट वाले स्थान को बाहर की और दबाता है. जिससे धमनी के बीच व अन्दर की जगह पूरी तरह खुल आती है और पुनः रक्त सामान्य रूप से प्रवाहित होने लगता है, इसके पश्चात तार व बैलून को बाहर निकाल लिया जाता है और स्टैन्ट धमनी को थौड़ा बनाए रखता है।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>सभी मरीज जिन की Angioplasty होती है, उन्हें आजीवन Asprin की कुछ लघु मात्रा लेनी होती है। में प्रक्रिया के फोटोग्राफी (फोट । वीडियो / टेलीविजन) को देखने के लिए सहमत हूं। मैं वैज्ञानिक प्रकाशनों के लिए साझा किए जाने वाले अपने नैदानिक विवरणी से भी सहमत हू बशर्ते मेरी पहचान का खुलासा न हो।</div>");


            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'><b>जटिलताय और जोखिमः </b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1.5 में 1 व्यक्ति में Restenosis अर्थात Stent. Angioplasty की जगह, अगले 6 माह में फिर से स्कवाट की संभावना।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2.2 में 1 व्यक्ति में तुरन्त/urgent बायपास की आवश्यकता रेडियल/फीमोरल अथवा अन्य पंक्चर की जगह सूजन या खून कर जमना।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>3. 100 में 01 व्यक्ति में हार्ट अटैक, खून का थक्का बनने से रोकने वाली दवाईयों की खतरनाक रिएक्शन, हृदय की धड़कन / गति का अनियमित होना, मृत्यु।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'4. दिल के दौरे, एलर्जी प्रतिक्रियाओं, धमनियों के धक्के, स्ट्रोक, थ्रोम्बस और दुर्लभ स्थिति में मौत जैसी नैदानिक स्थिति में बिगड़ना।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'> 5. कोई अन्य.........................................................................</div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>लाभ: </b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. प्रभावित क्षेत्र के हृदय ऊतक में रक्त आपूर्ति में सुधार और / या बहाल करना।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. प्रभावित क्षेत्र की हृदय मांसपेशियों के कामकाज में सुधार। </ div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>3. रोग के लक्षर्णी (छाती के दर्द की तरह) से प्रभावित और जीवन की गुणवत्ता में सुधार।</ div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>विकल्प:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. कंजर्वेटिव प्रबंधन (उदाहरण के लिए दवाओं द्वारा क्लॉट को विसर्जित करना</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. सर्जिकल प्रबंधन (बायपास सर्जरी) वांछित उम्मीद के अनुसार परिणाम प्राप्त नहीं किया जा सकता है।</ div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'>उपर्युक्त उपचार विधियों के फायदे, जोखिम तथा संभावित जटिलताओं के विषय में, उस भाषा जिसे में अच्छी तरह समझता/समझती हूँ, में विस्तार से बता दिया गया है। मैं उक्त उपचार विधि तथा उसके किए जाते समय आवश्यकता पड़ने पर एनेस्थिसिया अथवा किसी भी तरह के अन्य आकस्मिक उपचार हेतु स्वीकृति देता /देती हूँ।</div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px; margin-top:5px'>");
            body.Append("<tr>");
            body.Append("<td>(1) चिकित्सक नाम ________________________</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(1) रोगी का नाम ____________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td> चिकित्सक हस्ताक्षर________________________ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>रोगी का हस्ताक्षरः _____________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>(2)कैथटेक्नीशियन का नाम ______________</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(2) रोगी के परिजन का नाम ____________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>टेक्नीशियन हस्ताक्षरः________________________ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>रोगी से सम्बंधः ______________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td> </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>हस्ताक्षरः__________________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("</div>");
            return body.ToString();
        }
        public string IformedConsentPTCAEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-3%;'></h3></div>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:100px;margin-right:100px;width:600px;margin-top:-2%'>Informed Consent - Percutaneous Transluminal Coronory Angioplasty (PTCA)</ h4></div>");
            body.Append("<div class='row' style='text-align: justify; font-size:14px;margin-left:2px;margin-top:-2%'><b>Date:......................</b></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>"+pname+"</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>DOA.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>UNID No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>Detail of Procedure:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>To correct the abstruction or shrinkage in the heart's blood arteries, normal flow of blood is restored in the artery by removing the blockage by a method called 'coronary angioplasty'.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>This treatment method is carried out in the in the Catheterization Laboratory.The whole procedure is done with the help of X - rays.A tube / catheter is inserted through the radial(wrist) or femoral(groin) artery to the arteries of the heart.Alter this, a very fine wire is inserted on which a balloon and / or stent is mounted.This balloon or stent is deposited on the narrowed part of the artery and opened.After this procedure the space within the artery opens completely and again the blood starts flowing normally.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>All patients with Angioplasty have to take some small quantities of lifelong Asprin.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>I agree to observing, photography (still/video/televising) of  the procedure. I also agree to my clinical details being shared  for scientific publications provided my identity is not disclosed.</div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'><b>Complications & Risk:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. 1 in 5 people have the risk of closure of the artery (Restenosis) within 6 months of this procedure.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. 1 out of 2 people need urgent bypass surgery, swelling of the puncture wound at the catheter insertion site and/or blood clotting</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>3. 1 in 100 people are at risk for a heart attack, adverse reaction to medicines preventing blood clots, irregular heart beat/speed.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>4. Other risks include arterial clot, stroke, thrombus and in rare situation death.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'> Any other.........................................................................</div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>Benefits:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. Improving and/or restoring blood supply to the heart tissue of the affected area.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. Improving functioning of the heart muscle of the affected area.</ div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>3. Relied from disease symptoms (like chest pain) and improving quality of life.</ div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'><b>Alternatives:</b></div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>1. Conservative Management (eg. Dissolving the clot by medicines)</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>2. Surgical Management (Bypass surgery)</ div>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px; margin-top:10px'>The advantages, risks and potential complications of the above mentioned treatment methods have been explained to me in detail in the language that I understand very well. </div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:15px;'>I approve of the said treatment method and anesthesia or any other contingency treatment, if necessary, during its operation.</div>");

            body.Append("<table style='width:100%; font-size:15px;margin-right:20px; margin-top:5px'>");
            body.Append("<tr>");
            body.Append("<td>(1) Doctor name ________________________</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(1) Patient's name ____________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td> Doctor's signature________________________ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Patient's signature _____________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>(2) Name of the Cath Technician ______________</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>(2) Name of the patient's family. ____________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td> Technician Signature________________________ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td> Relation to Patient ______________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td> </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Signature__________________________</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("</div>");
         
            return body.ToString();
        }
        public string HighRiskConsentForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
          
            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-6%;'></h3></div>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:200px;margin-right:200px;width:300px;margin-top:-5%'>HIGH RISK CONSENT</h4></div>");

            //body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            //body.Append("<tr>");
            //body.Append("<td><b>Patient Name:</b></td>");
            //body.Append("<td></td>");
            //body.Append("<b><td>"+pname+"</td></b>");
            //body.Append("<td><b>&nbsp;</b></td>");
            //body.Append("<td><b>Son/Daughter/Husband/Wife:</b></td>");
            //body.Append("<td><b></b></td>");
            //body.Append("<b><td></td></b>");
            //body.Append("<td><b>&nbsp;</b></td>");
            //body.Append("<td><b>Age/Gender</b></td>");
            //body.Append("<td><b></b></td>");
            //body.Append("<b><td>" + gender + "</td></b>");
            //body.Append("</tr>");

            //body.Append("<tr>");
            //body.Append("<td><b>Date/Time of Admission.</b></td>");
            //body.Append("<td><b></b></td>");
            //body.Append("<b><td>" + admitdate + "</td></b>");
            //body.Append("<td><b>&nbsp;</b></td>");
            //body.Append("<td><b>UHID NO</b></td>");
            //body.Append("<td><b></b></td>");
            //body.Append("<b><td>" + uhidno + "</td></b>");
            //body.Append("<td><b>&nbsp;</b></td>");
            //body.Append("<td><b>IPD No</b></td>");
            //body.Append("<td><b></b></td>");
            //body.Append("<b><td>" + ipdno + "</td></b>");
            //body.Append("</tr>");
            //body.Append("</table>");

            body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Son/Daughter/Husband/Wife:</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Date/Time of Admission.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>UHID NO</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px;margin-left:20px; margin-top:5px;Font-size:11px'><u><b>INSTRUCTIONS FOR STATE:</b></u> <label style='margin-left:14px'>1 .To be esptarsed and filled up by the consultant only</label><br/> <label style='margin-left:170px'>2. Use no short form, abbreviations ar technical names</label><br/> <label style='margin-left:170px'>3.This is no blanket consent. Uar specifie parts as applicable</label></div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>I,___________________________________have been explained about my Surgery/Anesthesia/Procedure(s)</div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>_________________________________________________________________________________________</div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>I have been explained by the doctors that the causes of my being High Risk Case are due to the following:</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:14px'><u><b>Reasons for High Risk</b></u></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b>1.</b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>4.</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>2.</b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>5.</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>3.</b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>6.</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>I am ready to take the high risk involved in the mentioned procedure. No guarantees have been promised to me. That the explanations referred to were made and that all blanks or statements requiring insertion or complications were filled in an applicable paragraph struck out before I signed.</div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>I also state that I or my family shall not hold responsible to the Hospital or its doctors or employees for any consequences whatsoever. It has also been explained to me, in the language I understand, that the package tariff is higher in high risk category cases. </div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>I further give my consent for post-operative/procedure ventilation to me/my patient, if deemed necessary by consultant.</ div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:14px'><u><b>AUTHORIZATION BY PATIENT</b></u></div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>I have had the opportunity to ask questionsabout______________________________________________the</div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>alternatives, the risks and benefits in the language that I understand. I acknowledge that all my queries are answered to my full satisfaction.</div>");

            body.Append("<table style='width:100%;font-size:16px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:25%'></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:35%'>Name</th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:20%'>Signature</th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:20%'>Date</th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25%'>Patient</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:35%'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:25%'>Witness</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:35%'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;Font-size:14px'><u><b>PATIENT REPRESENTATIVE/SURROGATE</b></u></div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>The patient is unable to give consent because_____________________________________________ and I,</div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'>_____________________________________________ (Name/Relationship to the patient), therefore, consent for the patient. I acknowledge that I have had an opportunity to discuss this procedure, as stated above, with the physician or physician designee and hereby consent to this procedure.</div>");
            body.Append("<table style='width:100%;font-size:16px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:20%'></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:35%'>Name</th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:15%'>Relationship</th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:15%'>Signature</th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;width:15%'>Date</th>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Patient Representative</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>Witness</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'><label><b>Surgeon's Name:____________________</b></label><label><b>Sign:_____________</b></label><label><b>Date:_____________</b></label><label><b>Time:__________</b></label></div>");
            body.Append("<div class='row' style='font-size:16px; margin-top:5px;text-align: justify;margin-right:10px;'><label><b>Anaesthetist's Name:________________</b></label><label><b>Sign:______________</b></label><label><b>Date:____________</b></label><label><b>Time:___________</b></label></div>");
            body.Append("</div>");
           
            return body.ToString();
        }
        public string InformedConsentForImplantPlacementRemovalForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();

            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-6%;'></h3></div>");

            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:100px;margin-right:100px;width:600px;margin-top:-5%'>INFORMED CONSENT FOR IMPLANT PLACEMENT/REMOVAL</h4></div>");
            body.Append("<div class='row' style='text-align: justify; font-size:14px;margin-left:2px;margin-top:-2%'><b>Date:......................</b></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>"+pname+"</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>DOA.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>UNID No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='font-size:17px; margin-top:5px;text-align: justify;margin-right:10px;'>I authorize Dr.........................................................................................................and/or his/her associates to</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:10px;font-size:17px'>perform the surgical placement of implants upon me. This procedure has been recommended to me by My doctor as an option.</ div>");
            body.Append("<div class='row' style='font-size:17px; margin-top:5px;text-align: justify;margin-right:10px;'> Procedure to be performed ............................................................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:10px;font-size:17px'>I have been exd have daders the using of implant in my body through surgical procedure, which I fully understand and have understood the information provided to me.</div>");
            body.Append("<div class='row' style='font-size:17px; margin-top:5px;text-align: justify;margin-right:10px;'> I authorize placement of implants in the areas ......................................................................of of my body</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:10px;font-size:17px'>I understand that there are potential risks, complications and side effects associated with any procedure. Although it is impossible to list every potential risk, complication and side effect, I have been informed of some of the possible risks, complications and side effects of this implant surgery. These could include but may not be limited to the following:</div>");
            body.Append("<table style='width:100%;font-size:17px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Postoperative pain, discomfort and swelling</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Failure of implant itself</td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Bleeding</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Allergic or adverse reaction to any medications</td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Injury or damage to adjacent area/organ/tissue</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Disability</td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Injury or damage to nerves, causing temporary or permanent numbness and tingling or pain in Localized/general area</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Revision surgery or Additional Surgery</td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Leading to second surgery or implant removal</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:50%'>Any other:</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='text-align: justify;margin-right:10px;font-size:17px'>Most of these risks, complications or side effects are not serious and do not occur frequently and cannot be predicted or prevented by performing the procedure. I accept that there is no guarantee for the absence of risk, complications or side effects of this procedure. I have been also explained that guarantee of the implant lies with the manufacturer of the implant not with the doctor who is performing the procedure or hospital authority. I have been given other options and the nature of implant to be used has been decided by me. I also give permission for bone grafting.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:10px;font-size:17px'>I certify that I have read or had read to me the contents of this form. I will follow any patient instructions related to this procedure. I understand the potential risks, complications and side effects involved with any treatment or procedure and have decided to proceed with this procedure after considering the possibility of both known and unknown risks, complications, side effects and alternatives to the procedure. I declare that I have had the opportunity to ask questions and all of my questions have been answered to my satisfaction.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:10px;font-size:17px'>I certify that I have read and fully understood the above consent after adequate explanations were given to me.</div>");

            body.Append("<table style='width:100%; font-size:17px;margin-right:20px; margin-top:1px'>");
            body.Append("<tr>");
            body.Append("<td>Signature of Patient : ............................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Date & Time : ........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Relative's Name : ................................................. </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Signature of Relative : ...............................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Date/Time: ............................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:80%; font-size:17px;margin-right:35px;'>");
            body.Append("<tr>");
            body.Append("<td>(</td>");
            body.Append("<td>Reason:</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Minor</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Unconscious</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Drowsy</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Mentally Challenged</td>");
            body.Append("<td>)</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='text-align: justify;margin-right:20px; margin-top:5px;Font-size:17px'><u><b>DECLARATION BY DOCTOR</b></u></div>");
            body.Append("<div class='row' style='font-size:17px; margin-top:3px;text-align: justify;margin-right:10px;'>I,......................................................................................(Name of Doctor) hereby, state that the patient has been explained about the implication of the operation in vernacular.</div>");
            body.Append("<div class='row' style='text-align: justify;'>Signature of Doctor: ............................................ Date :.............................. Time: .........................</ div>");
            body.Append("</div>");
            return body.ToString();
        }
        public string InformedConsentForSpecialProcedureHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-6%'></h3></div>");

            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:200px;width:500px;margin-top:-6%'>विशेष प्रावधानों पर अभिसूचित सहमतिप्रपत्र</h3></div>");
            body.Append("<div class='row' style='text-align: justify; font-size:14px;margin-left:2px'><b>दिनांक ......................</b></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px;margin-top:5px'>");
            body.Append("<tr>");
            body.Append("<td><b>रोगी का नाम</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>"+pname+"</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आयु/लिंग</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आईपी संख्या </b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>यूएचआईडी संख्या</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>भर्ती होने का तिथि,</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>परामर्शदाता चिकित्सक</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>निदान</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='font-size:17px; margin-top:10px'>निम्नलिखित चिकित्सकिय प्रक्रिया के लिए सहमति प्रदान करता/करती हूँ:</div>");

            body.Append("<table style='width:90%; font-size:16px;margin-right:20px;margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>लम्बर पंचर (Lumber Puncture)[ ]</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>लीवर बायोप्सी (Liver Biopsy)[ ],</td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>केंद्रीय वेनस केन्युलेशन (Central Venous Cannulation)[ ]</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td></td>");

            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>ब्रोकोस्पसी (Bronchoscopy)[ ]</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>किडनी बायोप्सी (Kidney Biopsy)[ ],</td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>एंडोस्कोपी प्रक्रिया (Endoscopy Procedure)[ ]</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>ऐसिटिक टेप Ascitic Tap)[ ],</td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>फूफ्फूरल टेप (Pleural Tap)[ ]</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>इंटरकोस्टल ड्रेनेज (Intercostal Drainage)[ ]</td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>बोन मैरो एम्पिरेशन (Bone Marrow Aspiration)[ ]</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td></td></b>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='font-size:17px; margin-top:10px;text-align: justify; margin-right:20px;'> कोई अन्य प्रक्रिया ...............................................................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;font-size:17px'>चिकित्सक द्वारा मेरी मेरे रोगी की स्थिति, प्रकिया का प्रकार, विकल्प, जोखिम, लाभ और जटिलताएं जो प्रावधान के साथ जुड़ी हैं, उनको मेरी/हमारी भाषा में बता दिया गया है। इस प्रक्रिया के तहत होने वाली जटिलताओं तथा किसी बड़े जोखिम की आशंका के बारे में बताया जा चुका है। मुझे इस बात का संकेत दिया जा चुका हैं किसी भी प्रक्रिया में अप्रत्याशित जोखिम की आशंका बनी रहती है। इसके अतिरिक्त जटिलताएं उत्पन्न होने पर संक्रमण, रक्म बहाव, नस में घाव, रक्त के थक्के तथा हृदयाघात हो सकता है। बहुत कम परिस्थितियों में मृत्यु तथा एलर्जी प्रतिक्रिया आदि हो सकती है। ये कभी-कभी यह संभावित घटनाये गम्भीर तथा घातक रूप ले सकते हैं।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;font-size:17px'>बेहोश करने की किसी भी आवश्यक प्रक्रिया के लिये मैं पूर्ण रूप से सहमत हूं। तेज सांस, दवाइयों की प्रतिक्रिया, अल्प रक्तचाप, आकस्मिक अपूर्ण दर्द/आराम आदि के बारे में मुझे मेरी भाषा में बता दिया गया है।</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;font-size:17px'>इस प्रक्रिया में सम्बंधित सभी प्रश्नों का उत्तर मुझे तथा हमको हमारी अपनी भाषा में दे दिया गया है। मैं/हम इस प्रक्रिया के बारे में अपनी इच्छानुसार सहमति दे चुके हैं।</div>");
            body.Append("<table style='width:100%; font-size:17px;margin-right:20px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>रोगी का नाम ...........................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>रोगी के हस्ताक्षर .................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>संबंधी का नाम ......................................... </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td>संबंधी के हस्ताक्षर ...............................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>संबंध .......................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>चिकित्सक का नाम ...................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>चिकित्सक के हस्ताक्षर .................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>दिनांक ............................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>समय ............................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</div>");
            return body.ToString();
        }
        public string InformedConsentForSpecialProcedureForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-3%;'></h3></div>");

            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:200px;width:500px;'>INFORMED CONSENT FOR SPECIAL PROCEDURE</h3></div>");
            body.Append("<div class='row' style='text-align: justify; font-size:14px;margin-left:2px'><b>Date:......................</b></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>"+pname+"</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>DOA.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
          
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>UNID No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");

            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='font-size:17px; margin-top:10px'>I hereby give consent for the performance of:</div>");

            body.Append("<table style='width:80%; font-size:16px;margin-right:20px;margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>Lumbar Puncture</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Liver Biopsy</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Kidney Biopsy</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Central Venous Cannulation</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Bronchoscopy</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Endoscopy Procedure</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Ascitic Tap</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Pleural Tap</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Bone Marrow Aspiration</td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>ntercostal Drainage (ICD)</ td>");
            body.Append("<td style='height: 15px; width: 35px; border: 1px solid black;'></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='font-size:17px; margin-top:10px;text-align: justify;margin-right:20px;'>Any Other Procedure........................................................................................................................................</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;font-size:17px'>I have been explained by the doctor the nature of my/my patient's condition, the nature of the procedure, the alternatives, risks, benefits and complication associated with the procedure in my/our own language. I have been explained the likelihood of major risks or complications of this procedure.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;font-size:17px'>I have been also indicated that with any procedure there is always the possibility of unexpected risks and complications like infection, bleeding, nerve injury, blood clots, heart attack, in rare situation death and allergic reactions etc. They can be sometimes serious and possibly fatal.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;font-size:17px'>Conscious sedation / Local anesthesia or other form of anesthesia that may be deemed necessary is being used for this procedure and I have been explained in my own language that risks include suppressed breathing, Adverse Drug Reaction, low blood pressure and occasional incomplete pain relief.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-right:20px;font-size:17px'>All questions were answered related to the procedure and the possible management of complications to me/us in our own language and I/we give will-full consent for the procedure</ div>");

            body.Append("<table style='width:100%; font-size:17px;margin-right:20px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>Name of Patient : ...................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Signature of Patient : .................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Name of Relative : ................................. </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td>Signature of Relative : ...............................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Relationship : ........................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Name of Doctor : ...................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Signature of Doctor : .................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Date: ............................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Time : ............................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</div>");
         
            return body.ToString();
        }
        public string CathLabchecklistPreOperativeForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<h5 style='padding:20px;'>");
            body.Append("<img src='" + imagePath + "' style='width:200px; height:100px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:500px;margin-right:100px;text-align: justify ; margin-top:-140px; width:650px;height:80px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>"+pname+"</td>");
            body.Append("<td><b>Age/Gender : </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Date</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + admitdate + "</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:-10px'>CATH LAB CHECKLIST PRE OPERATIVE</h3></div>");
            body.Append("<table style='width:100%;font-size:20px;border-collapse:collapse;border:1px solid;margin-right:20px'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th  style='border: solid 1px;border-collapse: collapse; text-align: center; width:3%'><b></b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center; width:42%'><b></b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center; width:27%'><b>SIGNATURE/CHL</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center; width:28%'><b>SIGNATURE/CHL</b></th>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center; border-top:5px solid '><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px;border-top:5px solid '><label>Track patient admission and shifting to CATH LAB</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>STAFF NURSE-PRE-OP </lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>ADMIN</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px'><label'>Identify patient through wrist band and case sheet</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>STAFF NURSE-PRE-OP</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px'><label'>Pre-operative medication completed and all reports present in case sheett</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>STAFF NURSE-PRE-OP</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");



            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center; width:3%; border-top:5px solid '><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px;border-top:5px solid'><label>PAC completed (IF REQUIRED)</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>ANES TECH PATIENT ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>ANESTHETIST</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px'><label'>Adequate fasting</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>DOCTOR</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px'><label'>Preoperative instructions have been followed</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>DOCTOR</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center; width:3%; border-top:5px solid '><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px;border-top:5px solid'><label> Financial Clearance for OT</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>ADMIN</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px'><label'>Consents complete (Surgeon, Patient, Patient Attendant)</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>DOCTOR</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px'><label>Pharmacy, Implant, Instrument preparation</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>SCRUBBED CATHLAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;border-bottom:5px solid '><b>4</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:23px;border-bottom:5px solid'><label>Patient shifting from Pre Op to CATH LAB</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;border-bottom:5px solid'><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;border-bottom:5px solid'><label style='margin-left:5px'>SCRUBBED CATHLAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5'>");
       
            return body.ToString();
        }
        public string CathLabchecklistCathLabForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<h5 style='padding:20px;'>");
            body.Append("<img src='" + imagePath + "' style='width:200px; height:100px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:500px;margin-right:100px;text-align: justify ; margin-top:-140px; width:650px;height:80px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender : </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Date</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + admitdate + "</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:-10px'>CATH LAB CHECKLIST CATH LAB</h3></div>");
            body.Append("<table style='width:100%;font-size:20px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th  style='border: solid 1px;border-collapse: collapse; text-align: center; width:3%'><b></b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center; width:38%'><b></b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center; width:29%'><b>SIGNATURE/CHL</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center; width:30%'><b>SIGNATURE/CHL</b></th>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center; border-top:5px solid '><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:25px;border-top:5px solid'><label style='margin-left:35px'>Doctor:</label><br/> <label style='margin-left:35px;margin-top:10px'>Anesthetist:</label><br/> <label style='margin-left:35px;'>Cath Lab Assistant:</label><br/> <label style='margin-left:35px;'>Scrubbed Cath Lab Assistant:</label><br/> <label style='margin-left:35px;'>Anes. Tech-Anes. Assistant:</label><br/> <label style='margin-left:30px;'>Attendant:</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;border-top:5px solid '><label style='margin-left:5px'>DOCTOR</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:25px'><label style='margin-left:25px'>WHO CHECKLIST PART 1</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>DOCTOR</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:25px'><label style='margin-left:25px'>Inducation (SA, GA, Sedation,block)</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>ANES.TECH.-ANES ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>ANESTHETIST</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center; width:3%'><b>4</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:45%; font-size:25px'><label style='margin-left:25px'>Patient Positioning, paint and drape</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:25%'><label style='margin-left:5px'>SCRUBBED CATHLAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:27%'><label style='margin-left:5px'>DOCTOR</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center; border-top:5px solid '><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:25px;border-top:5px solid'><label style='margin-left:25px'>Confirm instrumet list and prepare trolley:</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>SCRUBBED CATHLAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;border-top:5px solid '><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;  font-size:25px'><label style='margin-left:25px'>Ensure'0 COUNT' Mops and Gauze used  for induction and painting</label></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>SCRUBBED CATHLAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>DOCTOR<br/><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:25px'><label style='margin-left:25px'>WHO CHECKLIST PART 2</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>OT TECH.- PATIENT ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'>SURGEON,ANESTHETIST<br/><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;  border-top:5px solid '><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:25px;border-top:5px solid'><label style='margin-left:25px'>Connsumable count during surgery</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>SCRUBBED CATHLAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b>CONFIRM POST SURGERY</label></td>");
            // body.Append("<td style='border: solid 1px;border-collapse: collapse;border-top:5px solid '><label style='margin-left:5px'>DOCTOR</lable><br/><label style='margin-left:5px'><b>TIME:</b>CONFIRM POST SURGERY</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;border-top:5px solid '><label style='margin-left:5px'>DOCTOR<br/><b>TIME:</b>CONFIRM POST SURGERY</label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;  font-size:25px'><label style='margin-left:25px'>Trolley management</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>SCRUBBED CATHLAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>DOCTOR<br/><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:25px'><label style='margin-left:25px'>Mop count before closure</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><label style='margin-left:5px'> CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>No:open:</b>&nbsp;&nbsp;&nbsp;<b>No:counted:  </b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>DOCTOR<br/><b>TIME:</b>ORAL CONFIRMATION</label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>4</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; font-size:25px'><label style='margin-left:25px'>WHO CHECKLIST PART 3</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; '><label style='margin-left:5px'>DOCTOR,ANESTHETIST</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center;  border-top:5px solid '><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;font-size:25px;border-top:5px solid'><label style='margin-left:25px'>Shifting patient from OT to post-op</label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;  border-top:5px solid '><label style='margin-left:5px'>CATH LAB ASSISTANT</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; border-top:5px solid '><label style='margin-left:5px'>STAFF NURSE-POST-OP</lable><br/><label style='margin-left:5px'><b>TIME:</b></label></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center; width:3%'><b>2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:45%; font-size:25px'><label style='margin-left:25px'>Instrument count and return to CSSD</label></td>");
            body.Append("<td colspan='2' style='border: solid 1px;border-collapse: collapse; width:25%;text-align: center;'><label style='margin-left:5px'>AS PER CSSD PROTOCOL</lable></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center; border-bottom:5px solid'><b>3</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;font-size:25px;border-bottom:5px solid'><label style='margin-left:25px'>Medicine return and count</label></td>");
            body.Append("<td colspan='2' style='border: solid 1px;border-collapse: collapse; width:25%;text-align: center;border-bottom:5px solid'><label style='margin-left:5px'>AS PER PHARMACY PROTOCOL</lable></td>");
            body.Append("</tr>");



            body.Append("</table>");
            body.Append("</h5'>");
           
            return body.ToString();
        }
        public string DailyProgressInformationReportForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {

            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:80px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:600px;margin-right:100px;text-align: justify ; margin-top:-105px; width:500px;height:55px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:16px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender : </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:100px;margin-right:100px;  margin-top:-20px'>DAILY PROGRESS INFORMATION REPORT OF PATIENTS IN ICU</h3></div>");
            body.Append("<table style='width:100%;font-size:16px;border-collapse:collapse;border:1px solid;'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");

            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>DATE/TIME</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>TODAY'SPRPGRESS REPORT & PATIENT <br/>CONDITION</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>DOCTOR'S <br/>NAME</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>ATTENDANT<br/>NAME</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>SIGNATURE OF<br/>ATTENDANT</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center'><b>RELATION<br/>WITH<br/> PATIENT</b></th>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; height:200px'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; height:200px'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; height:200px'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; height:200px'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

        
            return body.ToString();
        }
        public string InformedICUAdmissionConsentForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();

            //body.Append("<h5 style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:180px; height:70px;></h3></div>");

            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:200px;margin-right:200px;width:400px;margin-top:-4%;'>INFORMED ICU ADMISSION CONSENT</h3></div>");
            body.Append("<div class='row' style='text-align: justify; font-size:14px;margin-left:600px'><b>Date:......................</b></div>");

            body.Append("<div class='row' style='text-align: justify; width:100%;'><lable>------------------------------------------------------------------------------------------------------------------------------------</lable></div>");

            body.Append("<table style='width:100%; font-size:14px;margin-left:35px;margin-right:20px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>"+ pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>UHID NO.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>IPNO</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
         
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='text-align: justify; width:100%;'><lable>------------------------------------------------------------------------------------------------------------------------------------</lable></div>");
            body.Append("<div class='row' style='width:100%; font-size:16px'>");
            body.Append("<div class='row' style='text-align: justify;margin-left:20px;margin-right:20px'>I have been explained by the doctor about the serious condition of my Patient's which requires to be treated in ICU and I am ready for such treatment knowing all the conditions. The patient may require Bipap/C-Pap support and urinary catheterization, Ryle's tube insertion etc and intubation followed by mechanical ventilator if the conditions become worse. I am ready for the treatment on my responsibility.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-left:20px;margin-right:20px; margin-top:10px'>I have been also explained that depending of the patient condition the need of other procedure may arise like central line/arterial line insertion, Tracheostomy, Lumbar Puncture, ICD insertion, Dialysis etc for that an separate informed consent will be obtained from me/legal guardian.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-left:20px;margin-right:20px; margin-top:10px'>If the conditions become worse or he/she dies during such treatment then the hospital administration, Doctor and any other hospital staff won't be responsible for the same. I have been explained about the expected cost of treatment. I am ready to pay the bill completely. I understand that each of the procedure under intensive care is linked to certain leval of risk to my life and health and I have been explained about the risk.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-left:20px;margin-right:20px; margin-top:10px'>I understand that while staff giving my intensive care will use their professional judgement and skills to their best for my safety, no guarantee can be made for the outcome of intensive care. I have been explained about my condition, purpose for putting me under intensive care, expected outcome and consequences of not taking intensive care.</div>");

            body.Append("<div class='row' style='text-align: justify;margin-left:20px;margin-right:20px; margin-top:10px'>I also understand that during intensive care, my withdrawal of consent can be life-threatening to me/my patient. In case of my decision to withdraw in between from intensive care, the doctors and nurses will take legal regulation into consideration before accepting my decision. I also acknowledge that I have truthful disclose, to the best of my knowledge, all medical history and condition, asked to me, by my doctor.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-left:20px;margin-right:20px; margin-top:10px'>I certify that I have read fully & understood the above consent after adequate explanations were given to me in a language I understand.</div>");
            body.Append("</div>");

            body.Append("<table style='width:100%; font-size:16px;margin-left:50px;margin-right:40px;margin-top:10px'>");
            body.Append("<tr>");
            body.Append("<td>Signature of Patient: ................................. &nbsp;Date: ...............................&nbsp;Time: ...............................</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Surrogate's &nbsp;&nbsp;Name :<b>______________________</b>&nbsp;&nbsp;Signature &nbsp;&nbsp;<b>_______________</b>&nbsp;&nbsp;Relation:<b>____________________</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Date/Time<b>__________________________</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<table style='width:90%; font-size:16px;margin-left:50px;margin-right:35px;'>");
            body.Append("<tr>");
            //body.Append("<td>(</td>");
            body.Append("<td>Reason:</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Minor</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Unconscious</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td> Drowsy</td>");
            body.Append("<td style='height: 10px; width: 15px; border: 1px solid black;'></td>");
            body.Append("<td>Mentally Challenged</td>");
            //body.Append("<td>)</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<hr style='color:black;width:92%; margin-left:50px;margin-right:20px; border: 1px solid'></hr>");
            body.Append("<div class='row' style='text-align: justify;margin-left:50px;margin-right:20px; margin-top:10px;Font-size:16px'>I,<b>_______________</b>&nbsp;<b>__________</b>(Name of Witness)<b>___________________</b>(Relationship with Patient) of the patient hereby confirm that the above consent has been given by the patient/surrogate in my presence (A relative/attendant of the patient to sign as witness) ('Staff may sign as witness only when no relative is available)</div>");
            body.Append("<div class='row' style='text-align: justify;margin-left:50px;margin-right:20px; margin-top:15px;Font-size:16px'><u><b>DECLARATION BY DOCTOR</b></u></div>");
            body.Append("<div class='row' style='text-align: justify;margin-left:50px;margin-right:20px; margin-top:10px;Font-size:16px'>I,<b>_________________________</b>(Name of Doctor) hereby, state that the patient has been explained about the treatment in ICU in the vernacular language.He / She understand.</div>");
            body.Append("<div class='row' style='text-align: justify;margin-left:50px;margin-right:20px; margin-top:20px;Font-size:16px'>Signature of Doctor :<b>__________________________________</b>Date:<b>________________</b>Time:<b>________________</b></div>");
        
            return body.ToString();
        }
        public string PatientTranferRecordSecondForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            
            StringBuilder body = new StringBuilder();
            body.Append("<h5 style='padding:20px;'>");
            body.Append("<table style='width:90%;font-size:12px;border-collapse:collapse;border:1px solid;margin-top:-2%'>");
            // First row
            body.Append("<tr>");
            body.Append("<table style='width:99%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center'><b>|||</b></th>");
            body.Append("<th colspan='5' style='border: solid 1px;border-collapse: collapse; text-align: center'>INVESTIGATIONS/REFERENCES</th>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>a</b></td>");
            body.Append("<td rowspan='6' style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>RADIOLOGY:</b></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>INVESTIGATION</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>DONE</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>PEDING</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>NO OF FILMS</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>X-RAY:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>CT:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>MRI:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>USG:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>DOPPLER</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>b</b></td>");
            body.Append("<td rowspan='11' style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>PATHOLOGY:</b></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>INVESTIGATION</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>SENT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>PEDING</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>1</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>2</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>3</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>4</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>5</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>6</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>7</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>8</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>9</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>10</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>c</b></td>");
            body.Append("<td rowspan='5' style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>REFERENCES:</b></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>DOCTOR/DEPARTMENT</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>DONE</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>PEDING</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>REMARKS</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>1</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>2</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>3</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>4</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>d</b></td>");
            body.Append("<td rowspan='6' style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>MEDICATION:</b></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>DRUG NAME</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>DOSE</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>ROUTE</b></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; text-align: center'><b>FREQUENCY</b></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>1</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>2</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>3</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>4</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;text-align: center'><b>5</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>IV</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>DIET:</b></ td>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center'><b>V</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>LIST OF <br/> VALUABLES/CLOTHES/<br/>FOOTWEAR</b></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td></ td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></ td>");
            body.Append("<td>SIGNATURE/NAME OF<br/>ATTENDANT</ td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; text-align: center; height:40px'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse; margin-top:-5px'><b>REMARKS:</b></ td>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("<td></ td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>HANDOVER GIVEN BY:</b></ td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>HANDOVER GIVEN BY:</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>NAME:</b></ td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>NAME:</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>CHL:</b></ td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>CHL:</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>DATE & TIME:</b></ td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td></td>");
            body.Append("<td style='border-left: 1px solid;border-collapse: collapse;'><b>DATE & TIME:</b></td>");
            body.Append("</tr>");

            body.Append("</tr>");
            body.Append("</table>");

            body.Append("</h5>");
            
            return body.ToString();
        }
        public string PatientTranferRecordFristFrom(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
           
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            string imagePathh = System.Web.HttpContext.Current.Server.MapPath(@"/Content/logo/download.png");
            //body.Append("<img src='" + imagePath + "' style='width:150px; height:80px;' />");

            body.Append("<h5 style='padding:20px;'>");
            body.Append("<table style='width:100%;font-size:12px;border-collapse:collapse;border:1px solid'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center; width:25%'><img src='" + imagePath + "' style='width:110px; height:40px; margin-left:-40px; padding:2px' /></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; text-align: center; margin-left:-10px;width:75%'><b>PATIENT TRANSFER RECORD</b></th>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:11px;border-collapse:collapse;'>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:56%'><b>PATIENT NAME:</b>" + pname + "</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:20%'><b>DATE/TIME:</b>" + admitdate + "</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:20%'><b>AGE/GENDER:</b>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:56%'><b>CONSULTANT NAME:</b>" + doctor + "</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:20%'><b>UHID:</b>" + uhidno + "</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:20%'><b>IPD NO:</b>" + ipdno + "</td>");

            body.Append("<tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%'><b></b></td>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;'><b>PROVISIONAL DIAGNOSIS:</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%'><b></b></td>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;'><b>ALLERGY HISTORY:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%'><b></b></td>");
            body.Append("<td colspan='1' style='border: solid 1px;border-collapse: collapse;width:56%'><b>TRANSFER FROM:</b></td>");
            body.Append("<td colspan='2' style='border: solid 1px;border-collapse: collapse; width:40%'><b>TRANSFER TO:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>I</b></td>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse; text-align: center;'><b>PATIENT ASSESSMENT</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:11px;border-collapse:collapse;'>");
            body.Append("<td rowspan='2' style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>a</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>VITALS:</b></td>");
            body.Append("<td colspan='4' style='border: solid 1px;border-collapse: collapse;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>BP:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b>HR:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b>RR:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b>TEMP:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b>SPO2 RA:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%; text-align: center'><b>b</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>LEVEL OF<br/> CONSCIOUSNESS:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b>CONSCIOUS</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b>DROWSY</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b>UNCONSCIOUS</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b>ALTERED</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td rowspan='3' style='border: solid 1px;border-collapse: collapse; width:4%;text-align:center'><b>c</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>OXYGEN<br/> REQUIREMENT:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>IF YES THEN:</b></td>");
            body.Append("<td colspan='4' style='width:20%;border: solid 1px;border-collapse: collapse'><b>FLOW:</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b>ROUTE:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b>NASAL</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b>FACE MASK</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align: center;width:20%'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:11px;border-collapse:collapse;'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; margin-left:-10px;width:4%;text-align: center'><b>d</b></th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse; margin-left:-10px;width:21%'><b>PAIN ASSESMENT</b></th>");
            body.Append("<th colspan='5' style='border: solid 1px;border-collapse: collapse; width:75%'><img src='" + imagePathh + "' style='width:300px; height:50px; margin-left:-200px; padding:2px' /></th>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>e</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>RISK OF FALL:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>f</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>INCONTINENCE:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>g</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>LANGUAGE BARRIER:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>h</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>IMPLANT PLACEMENT:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>i</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>VULNERABLE:</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td rowspan='2' style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>j</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>PRESSURE ULCER:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>IF YES SPECIFY AREA:</b></td>");
            body.Append("<td colspan='4'  style='border: solid 1px;border-collapse: collapse;width:20%;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td rowspan='2' style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>k</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>IV LINE:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>IF YES SITE:</b></td>");
            body.Append("<td colspan='4'  style='border: solid 1px;border-collapse: collapse;width:20%;'><b></b></td>");
            body.Append("</tr>");



            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:11px;border-collapse:collapse;'>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>l</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>FOLEYS CATHETER :</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>m</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>RT:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>n</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>ET/TT :</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");





            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td rowspan='2' style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>o</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>ANY OTHER DRAIN:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>IF YES THEN SPECIFY:</b></td>");
            body.Append("<td colspan='4'  style='border: solid 1px;border-collapse: collapse;width:20%;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:11px;border-collapse:collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%; text-align: center'><b>p</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:21%'><b>REQUIRES ISOLATION:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<table style='width:100%;font-size:11px;border-collapse:collapse;'>");
            body.Append("<tr>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse; width:4%; text-align: center;height:25px'><b>II</b></td>");
            body.Append("<td colspan='5' style='border: solid 1px;border-collapse: collapse; text-align: center'><b>REQUIRES ISOLATION:</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>a</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>ICU CONSENT:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align:center'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align:center;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align:center'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td  rowspan='2' style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>b</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>MECHANICAL<br/>VENTILATION:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>IF  YES THEN SPECIFY:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>NIV</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>IMV</b></td>");
            body.Append("<td colspan='2'  style='border: solid 1px;border-collapse: collapse;width:20%;'><b>SETTING</b></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td rowspan='2' style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>c</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>IONOTROPIC DRUG<br/>STATUS:</b></td>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>Y</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b>N</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:20%;text-align: center;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>IF YES SPECIFY</b></td>");
            body.Append("<td colspan='4'  style='border: solid 1px;border-collapse: collapse;width:20%;'><b></b></td>");
            body.Append("</tr>");


            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td  style='border: solid 1px;border-collapse: collapse; width:4%;text-align: center'><b>d</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:21%;'><b>OTHER INFUSION:</b></td>");
            body.Append("<td  colspan='4'style='border: solid 1px;border-collapse: collapse;width:20%;height:30px'><b></b></td>");

            body.Append("</tr>");


            body.Append("</table>");
            body.Append("</tr>");

           


            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</h5>");
            
            return body.ToString();
        }
        public string CathLabProcedureNotesForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
       
            StringBuilder body = new StringBuilder();
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<h5 style='padding:20px;'>");
            body.Append("<img src='" + imagePath + "' style='width:200px; height:100px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:500px;margin-right:100px;text-align: justify ; margin-top:-130px; width:650px;height:70px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender : </b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:150px; margin-top:-10px'>CATH LAB PROCEDURE NOTES</h3></div>");

            body.Append("<h3 style='width:100%;font-size:12px; margin-left:20px; margin-right:20px;text-align:justify'>");

            body.Append("<h3><lable>Cardiologist(s):____________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3><lable>Interventional:_____________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3><lable>Surgeon(s):________________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3><lable>Aesthesiologist:____________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3><lable>Date Of Procedure:_____________________________________________</lable><lable>Time started:____________________</lable><lable>Time ended:__________________</lable></h3>");
            body.Append("<h3><lable>Pre-Operative Diagnosis:_________________________________</lable><lable>Post-Operative/Final Diagnosis:________________________________________</lable></h3>");
            body.Append("<h3><lable>Procedure Done:__________________________________________________________________________________________________________</lable> </h3>");
            body.Append("<h3><lable>Vital Signs:</lable> </h3>");
            body.Append("<h3><lable>Pre-Procedure:</lable>&nbsp;<lable style='margin-left:60px'><lable>HR:______________________</lable><lable>RR:_______________________</lable><lable>BP:____________________</lable><lable>SPO2:_____________________</lable></h3>");
            body.Append("<h3><lable>Post-Procedure:</lable>&nbsp;<lable style='margin-left:55px'><lable>HR:______________________</lable><lable>RR:_______________________</lable><lable>BP:____________________</lable><lable>SPO2:_____________________</lable></h3>");
            body.Append("<h3><lable>Procedure Notes & Findings:</lable> </h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Site:__________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Procedure:_____________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Angiography finding:_____________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>Implant:________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style =' text-align: justify ;margin-left:100px'><lable>PTCA/Intervention details:________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify ;margin-left:100px'><lable>_______________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3><lable>Post Procedure Order and medication:_________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify'><lable>_________________________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify'><lable>_________________________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify'><lable>_________________________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify'><lable>_________________________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3 style ='text-align: justify'><lable>_________________________________________________________________________________________________________________________</lable></h3>");
            body.Append("<h3><lable>Date & Time_______________________________</lable><lable>NAME & SIGNATURE OF SURGEON:_____________________________________________</lable> </h3>");

            body.Append("</h3 >");
            body.Append("</h5'>");
           
            return body.ToString();
        }

        //print whatsapp

        public string InformedConsentForChemotherapyHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-3%' />");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px;margin-top:-3%'>कीमोथैरेपी के लिए अधिसूचित सहमति</h3></div>");
            body.Append("<table style='width:100%;font-size:16px;text-align:left;border:0px solid #dcdcdc;margin-top:-3px;'>");
            body.Append("<tr>");
            body.Append("<td><b>रोगी का नाम</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>आयुः लिंग</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td><b>जन्म तिथि</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>एमआर संख्या</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td><b>परामर्शदाता चिकित्सक</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + doctor + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td><b>निदान</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + Diagnosis + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:1%;text-align:justify;'>मैं यह स्पष्ठ कर चुका हूं कि चिकित्सा में क्या-क्या जुडता सम्भ्वावित है, उसके लाभ के साथ जोखिम भी। अन्य उपलब्ध वैकल्पिक चिकित्सा के साथ चिकित्सा न कराने के बारे में भी बताया जा चुका है। विशेष तौर पर किसी भी उपलब्ध विकल्प के साथ-साथ लाभ एवं जोखिमों को भी स्पष्टा कर दिया गया है।</div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:17px;text-align:justify;'><b></b>इस चिकित्सा प्रक्रिया का उद्देश्य है कि-</div>");
            body.Append("<h3 style='height:15px; width:15px; border:1px solid black; margin-left:14px;'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-40px;font-size:17px; margin-left:50px;text-align:justify;'>रोग निवारक स्वस्थ होने के लिए सबसे उत्तम तथा सम्भावित अवसर प्रदान करना।</div>");
            body.Append("<h3 style='height:15px; width:15px; border:1px solid black; margin-left:14px;'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-40px;font-size:17px; margin-left:50px;text-align:justify;'>रोग निवारक रहित इसका उद्देश्य रोग को नियंत्रित करना और उसको कम करता है। यदि ऐसे लक्षण प्रतीत होते हैं तो जितना सम्भाव हो, उतना लम्बा उपचार किया जा सकता है, भले ही रोगी तत्काल ठीक अनुभव न करें।</div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:17px;text-align:justify;margin-left:5px'>प्रायः उपचार के दौरान गम्भीर तथा जोखिम की स्थिति उत्पन्न हो सकती है. जिसका विवरण निम्नलिखित है.</ div>");

            body.Append("<h5 style = 'border:1px solid;text-align: justify ;width:670px;height:140px; padding:10px;margin-top:-2px'>");
            body.Append("<table style='width:95%;font-size:16px;text-align:left;border:0px solid #dcdcdc;'>");
            body.Append("<tr>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td style='width:'>रक्त अल्पता</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>थकान</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>फेफड़ों का प्रभाव</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>संक्रमण का खतरा</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='height:10px; width:15px; border:1px solid black;'></td>");
            body.Append("<td> रक्त थक्का</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>हृदय पर प्रभाव </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>मतली और उल्टी</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>त्वचा पर प्रभाव</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='height:10px; width:15px; border:1px solid black;'></td>");
            body.Append("<td> कब्ज़</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>मूर्दे या मूजावय पर प्रभाव</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>नस की क्षति</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>मुँह या गले में खराश</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='height:10px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>दस्त</ td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>उत्तक को स्थानीय क्षति </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>रक्तस्राव का खतरा</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>सहायक उपजाऊपन</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='height:10px; width:15px; border:1px solid black;'></td>");
            body.Append("<td> रक्त की आवश्यकत </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>प्रभाव में विलम्ब</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>मृत्यु</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>अन्य</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px;margin-top:-10px;text-align: justify ;'><b>रोगी या यह व्यक्ति जो रोगी के समानांतर दायित्वों का निर्वहन कर रहा हो, उसका बयान</b></div>");
            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px;text-align: justify ;'>उपचार के सफल होने पर उसके लाभों को मैं समझ गया हूँ। मैं यह भी जान गया हूँ कि इस बात की कोई प्रतिभूति नहीं है कि उपचार स्वस्थ होने में मेरी सहायता करेगा। मैं समझाता हूँ कि इस उपचार से अल्पकालीन तथा दीर्घकालीन दुष्प्रभाव हो सकते हैं। मुझे यह भी स्पष्ट करा दिया गया है कि इस उपचार के दौरान वे दुष्प्रभाव भी हो सकते हैं, जिनका उल्लेख इस प्रपत्र में नहीं है। अलग - अलग रोगियों में केमोथिरेपी का प्रभाव अलग - अलग देखा जा सकता है। ऐसा दुष्प्रभाव भी हो सकता है जो दूसरे रोगियों में परिलक्षित कर होता हो। इस प्रपत्र पर हस्ताक्षर करते हुए मैं यह जानता हूँ कि मेरे चिकित्सक द्वारा संस्तुत एवं प्रपत्र में वर्णित उपचार को स्वीकार किया है। मैं यह जानता हूँ कि मेरे उपचार के एक भाग के रूप में रक्त - उत्पाद की आवश्यकता हो सकती है जिसके लिए पूजाक सहमति प्राप्त की जाएगी। चिकित्सीय शोध कार्य हेतु, मैं अपने चिकित्सीय अभिलेखों के आंकडे तथा जानकारी प्रयोग करने की पूर्ण सहमति प्रदान करता हूँ। मुझे अपने उपचार के बारे में प्रश्न करने का अवसर प्रदान किया गया है और संतोषजनक उत्तर भी दिए गए हैं।</div>");
            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px; margin-top:3px;text-align: justify ;'><b>प्रजनन काल की महिला</b></div>");

            body.Append("<h3 style='margin-top:-1px'>[ ]</h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-39px;font-size:17px;text-align: justify; margin-left:50px'>में निश्चिततौर पर कहती हूँ कि में गर्भवती नहीं हूँ।</div>");
            body.Append("<h3>[]</h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-39px;font-size:17px;text-align: justify; margin-left:50px'>मैं भली प्रकार जानती हूँ कि मुझे चिकित्सा के मध्य गर्भवती होने से बचना चाहिए।</div>");

            body.Append("<h3>[ ]</h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-39px; text-align: justify;font-size:17px; margin-left:50px'>अगर मुझे किसी भी स्तर से यह अनुभव हो रह कि मैं तत्काल गर्भवती हो सकती हूं तो अपने चिकित्सक को इसकी जानकारी दूंगी।</div>");

            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px;'><b>पुरुष-</b></div>");
            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px; margin-top:3px;text-align: justify ;'>मैं समझाता हूँ कि मुझे उपचार के मध्य बच्चों का पिता नहीं बनना चाहिए क्योंकि मेरे शुक्राणुओं पर अजात प्रभाव हो सकता है। मैंने शुक्राणुओं के क्रायाप्रेजर्वेशन को अस्वीकार किया है या उसके लिए अनुरोध किया है। (उचित को हटाएं)</div>");
            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px; margin-top:3px;text-align: justify ;'>---------------------------------------------------------------------------------------------------------------------</div>");

            body.Append("<table style='width:90%;font-size:17px;'>");
            body.Append("<tr>");
            body.Append("<td style='text-align:left'>रोगी का नाम</td>");
            body.Append("<td>..................................................</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='text-align:left'>रोगी के हस्ताक्षर:</td>");
            body.Append("<td></td>");
            body.Append("<td>..................................................</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:left'>साक्षी का नाम</td>");
            body.Append("<td>..................................................</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='text-align:left'>साक्षी के हस्ताक्षर</td>");
            body.Append("<td></td>");
            body.Append("<td>..................................................</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='text-align:left'>चिकित्सक का नाम</td>");
            body.Append("<td>..................................................</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='text-align:left'>चिकित्सक के हस्ताक्षर</td>");
            body.Append("<td></td>");
            body.Append("<td>..................................................</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</div>");
            return body.ToString();
        }
        public string InformedConsentForChemotherapyEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-3%' />");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px;margin-top:-3%'> INFORMED CONSENT FOR CHEMOTHERAPY </h3></div>");
            body.Append("<table style='width:100%;font-size:16px;text-align:left;border:0px solid #dcdcdc;margin-top:-3px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td><b>DOA</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>UHID No.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + doctor + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + Diagnosis + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:1%;'>I have explained the treatment, what it is likely to involve, its benefits and risks together with those of any available alternative treatments (including no treatment). In particular, I have explained:</div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:17px;'><b>• &nbsp;</b>That the aim of the procedure is</div>");
            body.Append("<h3 style='height:15px; width:15px; border:1px solid black; margin-left:14px;'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-40px;font-size:17px; margin-left:50px'><label><b>Curative -</b> to give the best possible chance of being cured</label></div>");
            body.Append("<h3 style='height:15px; width:15px; border:1px solid black; margin-left:14px;'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-40px;font-size:17px; margin-left:50px'><label><b>Non-Curative -</b> the aim is to control or shrink the disease especially if it is causing specific symptoms, to try and keep you as well as possible for as long as possible but not cure you</label></div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:17px;'><b>• &nbsp;</b>The serious or frequently occurring risks of the treatment.These are detailed in the information  </div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:17px;'><b>&nbsp;&nbsp;&nbsp;</b>provided(see below) and include(tick those that may apply)</div>");
            body.Append("<h5 style = 'border:1px solid;margin-left:10px;text-align: justify ;width:670px;height:160px; padding:10px;margin-top:-2px'>");
            body.Append("<table style='width:95%;font-size:16px;text-align:left;border:0px solid #dcdcdc;'>");
            body.Append("<tr>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td style='width:'>Anaemia</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Fatigue/ tiredness</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Lung effects</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Risk of infection</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='height:10px; width:15px; border:1px solid black;'></td>");
            body.Append("<td> Blood clots</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Heart effects</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Nausea and vomiting</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Skin effects / hair loss</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td style='height:10px; width:15px; border:1px solid black;'></td>");
            body.Append("<td> Constipation</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Kidney or bladder effects</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Nerve damage</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Sore mouth or throat</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='height:10px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Diarrhoea </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Local damage to tissues</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Risk of bleeding</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Subfertility</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='height:10px; width:15px; border:1px solid black;'></td>");
            body.Append("<td> Need for </td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Late effects..........</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>...................</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td></td>");
            body.Append("<td>...................</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px;'>blood transfusion</div>");
            body.Append("<table style='width:95%;font-size:16px;text-align:left;border:0px solid #dcdcdc;'>");
            body.Append("<tr>");
            body.Append("<td style='width:45%;'></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Other.............</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td style='height:15px; width:15px; border:1px solid black;'></td>");
            body.Append("<td>Death</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");

            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px;margin-top:-10px'><b>Statement of patient/person with parental responsibility for patient:</b></div>");
            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px'><b>I understand</b> that there are benefits of this treatment if it is successful. I also understand that there is no guarantee that the treatment will help me. <b>I understand</b> that this treatment can have short term and long- term side effects.  <b>I have been </b>also explained that I could have side effects from my treatment that are not listed on this form. Each patient can respond differently to chemotherapy, and could have side effects that have not been reported by others.<b> I understand</b> that I may stop this treatment at any time. <b> I understand</b> that by signing this form I am consenting to receive the treatment recommended by my doctor and described on this form. <b> I understand</b> that I may need blood products as part of my treatment for which a separate consent will be obtained. I give permission for data held in my medical records to be used for research purposes. <b> I have had the chance </b>to ask questions about this treatment and my questions have been answered to my satisfaction.</div>");
            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px; margin-top:3px'><b>Females of reproductive age:</b></div>");

            body.Append("<h3 style='height:15px; width:15px; border:1px solid black;margin-top:-1px'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-39px;font-size:17px;text-align: justify; margin-left:50px'><label><b>I confirm </b>that I am not pregnant</label></div>");
            body.Append("<h3 style='height:15px; width:15px; border:1px solid black;'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-39px;font-size:17px;text-align: justify; margin-left:50px'><label><b>I understand </b>that I need to avoid becoming pregnant during my treatment</label></div>");

            body.Append("<h3 style='height:15px; width:15px; border:1px solid black;'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-39px; text-align: justify;font-size:17px; margin-left:50px'><label><b>I will inform</b> the staff treating me as soon as I can, if I think at any stage that I might be pregnant</label></div>");

            body.Append("<div class='row' style='color:black;text-align:left; font-size:17px;margin-top:-1px'><b>Males:</b></div>");
            body.Append("<h3 style='height:15px; width:15px; border:1px solid black;margin-top:-1px'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-39px; text-align: justify;font-size:17px; margin-left:50px'><label><b>I understand</b>  that I should not father children while I am receiving my treatment as there may beunknown effects on my sperm.</label></div>");
            body.Append("<h3 style='height:15px; width:15px; border:1px solid black;margin-top:-1px'></h3>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:-39px; text-align: justify;font-size:17px; margin-left:50px'><label><b>I have declined*/requested* </b> sperm cryopreservation<b> (* delete as appropriate) </b>.</label></div>");
            body.Append("<table style='width:90%;font-size:17px;'>");
            body.Append("<tr>");
            body.Append("<td style='width:50%;text-align:left'>Name of Patient/Guardian:</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Signature of Patient/Guardian:  </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td style='width:50%;text-align:left'>Name of Witness: </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Signature of Witness:</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td style='width:50%;text-align:left'>Name of Doctor:</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Signature of Doctor:</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</div>");
            
            return body.ToString();
        }
        public string IntraUterineDevicesConsentForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");

            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:250px;margin-right:150px;margin-top:-3%; width:300px'><u>CHANDAN HOSPITAL</u></h3></div>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px;margin-top:10px'> <u>INTRA UTERINE DEVICES CONSENT FORM</u> </h3></div>");

            body.Append("<table style='width:100%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px;'>");
            body.Append("<tr>");
            body.Append("<td>Name</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Age/Gender</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td>Date</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + admitdate + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>UHID No.</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td>IPD No</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td>Consultant</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + doctor + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Diagnosis</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + Diagnosis + "</td>");
            body.Append("<td>&nbsp;</td>");

            body.Append("<td>Contact No</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:5%;'><label>Address-______________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>_____________________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>History-_______________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>Procedure –____________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>Product description-_____________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>_____________________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>Indications-____________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>_____________________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>Possible complications-__________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>_____________________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>Alternative methods-_____________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>_____________________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>Benefits –_____________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>_____________________________________________________________________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:18px;margin-top:2%;'><label>Signature of patient-_____________________</label><label>Date-_______________</label><label>Time-________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:17px;margin-top:2%;'><label>Declaration by doctor</label></div>");
            body.Append("<div class='row' style='color:black;font-size:18px;margin-top:2%;'><label>I,__________________________</label>(name of doctor) hereby state that the patient has been explained about the implications of IUD in the vernacular language.</div>");
            body.Append("<div class='row' style='color:black;font-size:18px;margin-top:2%;'><label>Sign of doctor-________________________</label><label>Date-_______________</label><label>Time-________________</label></div>");
            body.Append("</div>");
           
            return body.ToString();
        }
        public string ConsentForRestraintsEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:100px;margin-right:100px; width:600px;margin-top:-10px'>CONSENT FOR RESTRAINTS</h3></div>");
            body.Append("<table style='width:100%; font-size:14px;margin-top:10px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<td>"+pname+"</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Date</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + admitdate + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>IPD No.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + doctor + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td></td>");
            body.Append("<td>" + Diagnosis + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='color:black;font-size:15px;text-align: justify;margin-top:5px'><b>'The restraining of the patient from undesirable movement by stabilizing the patient's hands, upper body,head and leg movements with the intention of providing medical care including administration of drugs  and preventing injury to the patient and medical staff.'</b></div>");
            body.Append("<div class='row' style='color:black;font-size:15px;text-align: justify;margin-top:3px'> We work with the professional intent that we can provide the best possible quality care to the patients.Providing a high quality of care can sometimes be made very difficult, or even impossible, due to a lack of cooperation from the patient.The following(improper) behavior that can interfere with the proper provision of quality medical care include: Hyperactivity, Resistive movements, refusing to cooperate with the staff,kicking and screaming </ div>");
            body.Append("<div class='row' style='color:black;font-size:15px;text-align: justify;'> I hereby give my consent, to use Chemical and/or Physical restraints including, but not limited to, a mouth prop, soft wrist restraints, leg and head restraints or as advised by the doctors. I further agree that this.consent shall remain in full force unless withdrawn in writing by the person who has signed below on behalf of the patient.</ div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;margin-top:3px'><b> Types of restraints:</b></div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;margin-left:45px;margin-top:3px'><b>• &nbsp;Physical restraints</b> are devices that limit specific parts of the patient's body, such as arms or legs. Belt or vest restraints may be used to stop the patient from getting out of bed or a chair </div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;margin-left:45px;margin-top:3px'><b>• &nbsp;Chemical restraints </b>are medicines used to quickly sedate a violent patient. These will be given as a pill or an injection.</div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;margin-top:3px'><b>Risks & Possible Complication:</b> The patient may become more angry or violent while in physical restraints.The patient may struggle against physical restraints.This can cause skin wounds, pressure sore or block blood.flow.It can also increase the patient's heart rate and breathing rate. This can be (very rarely) life-threatening.Chemical restraints can cause low blood pressure, heart rhythm problems, and slow or shallow breathing.This can affect how much oxygen the patient gets.Chemical restraints can also cause drooling, shuffled walk,muscle spasms and stiffness, and tremors.</ div>");
            body.Append("<div class='row' style='color:black;font-size:16px;margin-top:3%;'><label><b>Parent/Legal Guardian Name: </b>___________________________________ </label><label>Signature:___________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:16px;margin-top:3%;'><label><b>Witness Name & Relation:</b>______________________________________ </label><label>Signature:____________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:16px;margin-top:3%;'><label><b>Doctor Name:</b>_________________________________________________ </label><label>Signature:____________________</label></div>");
            body.Append("</div>");
            return body.ToString();
        }
        public string ConsentForRestraintsHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            string _result = string.Empty;
            StringBuilder body = new StringBuilder();
            StringBuilder h = new StringBuilder();


            body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:100px;margin-right:100px; width:600px;margin-top:-10px'>प्रतिबंध के लिए सहमति</h3></div>");
            body.Append("<table style='width:100%; font-size:15px;margin-top:10px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आयु/लिंग</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>दिनांक</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + admitdate + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>IPD No.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + doctor + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + Diagnosis + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<div class='row' style='color:black;font-size:15px;text-align: justify;margin-top:5px'><b>'रोगी के हाथ, ऊपरी शरीर, सिर और पैर के हिलने को स्थिर करने और दवाओं के प्रशासन सहित चिकित्सा देखभाल प्रदान करने और रोगी और चिकित्सा कर्मचारियों को चोट को रोकने के उद्देश्य से अवांछनीय आंदोलन से रोगी को रोकना।'</b></div>");
            body.Append("<div class='row' style='color:black;font-size:15px;text-align: justify;margin-top:3px'> हम पेशेवर इरादे के साथ काम करते हैं कि हम रोगियों को सर्वोतम संभव गुणवत्ता देखभाल प्रदान कर सकें। रोगी से सहयोग की कमी के कारण उच्च गुणवता की देखभाल प्रदान करना कभी-कभी बहुत कठिन या असंभव भी हो सकता है। निम्न (अनुचित) व्यवहार जो गुणवत्ता चिकित्सा देखभाल के उचित प्रावधान में हस्तक्षेप कर सकते हैं, उनमें शामिल हैं: सक्रियता, प्रतिरोधक चाल,कर्मचारियों के साथ सहयोग करने से इनकार करना, मारना और चीखना।</ div>");
            body.Append("<div class='row' style='color:black;font-size:15px;text-align: justify;'>मैं इसमें अपनी सहमति देता हूं, जिसमें रासायनिक और / या भौतिक निरोधकों का उपयोग करना शामिल है, लेकिन यह सीमित नहीं है, एक मुंह का सहारा, नरम कलाई पर प्रतिबंध, पैर और सिर पर प्रतिबंध या डॉक्टरों द्वारा सलाह के अनुसार। मैं आगे मानता हूं कि यह सहमति पूरी तरह से लागू रहेगी जब तक कि उस व्यक्ति द्वारा लिखित रूप से वापस नहीं ली जाती है जिसने रोगी की ओर से नीचे हस्ताक्षर किया है।</ div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;margin-top:3px'><b>संयम के प्रकार:</b></div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;margin-left:45px;margin-top:3px'><b>• &nbsp;शारीरिक निरोधकः</b>वे उपकरण हैं जो रोगी के शरीर के विशिष्ट भागों, जैसे कि हाथ या पैर को सीमित करते हैं। रोगी को बिस्तर या कुर्सी से बाहर निकलने से रोकने के लिए बेल्ट या बनियान की मरहम का इस्तेमाल किया जा सकता है।</div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;margin-left:45px;margin-top:3px'><b>• &nbsp;रासायनिक संयमः</b>एक हिंसक रोगी को जल्दी बेहोश करने के लिए इस्तेमाल की जाने वाली दवाएं हैं। इन्हें गोली या इंजेक्शन के रूप में दिया जाएगा।</div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;margin-top:3px'><b>जोखिम और संभावित जटिलताः</b> शारीरिक बाधाओं में रोगी अधिक क्रोधित या हिंसक हो सकता है। रोगी शारीरिक संयम के खिलाफ संघर्ष कर सकता है। इससे त्वचा पर घाव, दबाव में खराश या रक्त प्रवाह अवरुद्ध हो सकता है। यह रोगी की हृदय गति और श्वास दर को भी बढ़ा सकता है। यह (बहुत कम ही) जानलेवा हो सकता है।</ div>");
            body.Append("<div class='row' style='color:black;text-align:left;margin-top:2px;font-size:16px;'>रासायनिक संयोजनों से निम्न रक्तचाप, हृदय की ताल की समस्याएं और धीमी या उथली सॉस लेने में समस्या हो सकती है। यह प्रभावित कर सकता है कि रोगी को कितनी ऑक्सीजन मिलती है। रासायनिक संयमों से डोलिंग, फेरबदल चलना, मांसपेशियों में ऐंठन और कठोरता और कंपकंपी भी हो सकती है।</ div>");

            body.Append("<div class='row' style='color:black;font-size:16px;margin-top:3%;'><label>अभिभावक / कानूनी अभिभावक का नाम:_______________________________ </label><label>हस्ताक्षर___________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:16px;margin-top:3%;'><label>साक्षी का नाम और संबंध:__________________________________________ </label><label>हस्ताक्षर____________________</label></div>");
            body.Append("<div class='row' style='color:black;font-size:16px;margin-top:3%;'><label>डॉक्टर का नाम:_________________________________________________ </label><label>हस्ताक्षर____________________</label></div>");
            body.Append("</div>");
            
            return body.ToString();
        }       
        public string InformedConsentForLeaveAgainstMedicalAdviceEnglishForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:100px;margin-right:100px; width:600px;margin-top:10px'>INFORMED CONSENT FOR LEAVE AGAINST MEDICAL ADVICE</h3></div>");
            body.Append("<div class='row' style='100%; margin-left:5px;font-size:14px'><b>Date:.........................</b></div>");
            body.Append("<table style='width:100%; font-size:14px;margin-top:10px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<td>"+pname+"</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>DOA</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>IPD No.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + doctor + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + Diagnosis + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;'>----------------------------------------------------------------------------------------------------------------------</div>");
            body.Append("<div class='row' style='color:black;font-size:16px;text-align: justify;'>I acknowledge on behalf of myself or my patient name.......................................that this action of leaving the hospital is against the medical advice of the attending doctor and I hereby give consent for the same.</div>");
            body.Append("<div class='row' style='color:black;font-size:16px;text-align: justify;'> Due to my / our personal reason, I / We are not able to continue further treatment, although I / we have been explained by the doctor in my / our own language the nature of illness, need for continuing hospitalization &treatment and the possible dangers and complications to my / our patient's health including death at this stage. I understand all the risks involved and accept the consequences on my own responsibility and hereby release the doctor, hospital administration and other hospital staff from any liability for the effects that may results from discontinuance of the treatment.</ div>");
            body.Append("<div class='row' style='color:black;font-size:16px;text-align: justify;'>I/We have signed the consent voluntarily out of my free will without any coercion and in my full senses.</ div>");


            body.Append("<table style='width:100%; font-size:17px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>Patient Signature: ..........................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Name: ..........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Witness Signature. ........................................ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td>Name:  ...........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Address: .................................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>Relationship: .............................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;'>----------------------------------------------------------------------------------------------------------------------</div>");
            body.Append("<div class='row' style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px;margin-top:10px;font-size:17px;'><b>If patient is not competent to give consent, relative to give the same</b></div>");
            body.Append("<table style='width:100%; font-size:17px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>Relative Signature: .................................................. </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Name :...........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Address: ....................................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Relationship: ............................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>Doctor’s Signature: ........................................ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>Name  ...........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td> Date& Time: ...................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</div>");
            return body.ToString();
        }
        public string InformedConsentForLeaveAgainstMedicalAdviceHindiForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px;margin-top:10px'>चिकित्सीय परामर्श के खिलाफ छुट्टी लेने की अभिसूचित सहमति</h3></div>");
            body.Append("<div class='row' style='100%; margin-left:5px;font-size:14px'><b>दिनांक:.........................</b></div>");
            body.Append("<table style='width:100%; font-size:14px;margin-top:10px'>");
            body.Append("<tr>");
            body.Append("<td><b>रोगी का नाम</b></td>");
            body.Append("<td></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आयु/लिंग</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>यूएचआईडीसंख्या</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>आईपी संख्या</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>भर्ती होने का तिथि</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + admitdate + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>परामर्शदाता चिकित्सक</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + doctor + "</td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>निदान</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + Diagnosis + "</td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;'>----------------------------------------------------------------------------------------------------------------------</div>");
            body.Append("<div class='row' style='color:black;font-size:16px;text-align: justify;'>मैं स्वयं अपने लिए या अपने रोगी जिसका नाम....................................... है, के बारे में, जो चिकित्सक देखभाल कर रहे है, उनकी चिकित्सीय परामर्श के खिलाफ अस्पताल से छुट्टी लेने की सहमति प्रदान करता हैं। </div>");
            body.Append("<div class='row' style='color:black;font-size:16px;text-align: justify;'> मेरे। हमारे व्यक्तिगत कारणों से, मैं हम आगे चिकित्सा कराने में सक्षम नहीं हैं। यद्यपि चिकित्सक ने मेरी अपनी भाषा में रोग की प्रवृति, अस्पताल में इलाज कराने की अनिवार्यता, इलाज, सम्भावित खतरे और मेरे तथा मेरे रोगी के लिए इस स्थिति में पैदा होने वाली जटिलताओं को भी बता दिया है जिसमें मृत्यु भी शामिल है।</ div>");
            body.Append("<div class='row' style='color:black;font-size:16px;text-align: justify;'>  मैं अस्पताल से छुट्टी लेने के बाद के सभी जुड़े खतरों को समझ गया हूँ और अपनी जिम्मेदारी पर सभी खतरों को स्वीकार करता हूँ। चिकित्सा के बाधित तथा अनियमित होने के कारण जो भी परिणाम होंगे, उसके लिए चिकित्सक, अस्पताल प्रशासन तथा अस्पताल के अन्य कर्मचारी किसी भी दायित्व से मुक्त है।</ div>");
            body.Append("<div class='row' style='color:black;font-size:16px;text-align: justify;'>बिना किसी बाहरी दबाव के अपनी पूर्ण चेतना में, स्वेच्छा से तथा अपनी स्वतंत्र इच्छा के अनुसार मैने/हमने सहमति पर हस्ताक्षर किए हैं।</ div>");

            body.Append("<table style='width:100%; font-size:17px; margin-top:15px'>");
            body.Append("<tr>");
            body.Append("<td>रोगी के हस्ताक्षर: ..........................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>नाम: ..........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>साक्षी के हस्ताक्षर ........................................ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td>नाम:  ...........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>पता : ........................................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>रोगी से संबंध : ..................................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;'>----------------------------------------------------------------------------------------------------------------------</div>");
            body.Append("<div class='row' style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px;margin-top:10px;font-size:17px;'>यदि रोगी सहमति देने में सक्षम नहीं है तो संबंधी अपनी सहमति दे</div>");
            body.Append("<table style='width:100%; font-size:17px; margin-top:15px'>");


            body.Append("<tr>");
            body.Append("<td>साक्षी के हस्ताक्षर .................................................. </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td>नाम:  ...........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td>पता : ............................................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>संबंध : ...........................................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>चिकित्सक के हस्ताक्षर ........................................ </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");

            body.Append("<td>नाम:  ...........................................................</td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");


            body.Append("<tr>");
            body.Append("<td> दिनांक /समय   ...................................................</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("</div>");
         
            return body.ToString();
        }
        public string RequisitionFormForSpecimenForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");

            body.Append("<div class='row'><h2 style='color:black;text-align:center;margin-left:250px;margin-right:150px;margin-top:-3%; width:300px'><u>CHANDAN HOSPITAL</u></h2></div>");
            body.Append("<div class='row'><h2 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px;margin-top:10px'> <u>REQUISITION FORM FOR SPECIMEN</u> </h2></div>");

            body.Append("<table style='width:95%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px;margin-left:20px;margin-right:20px '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b> </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + uhidno + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td>" + ipdno + "</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Surgeon Name</b> </td>");
            body.Append("<td><b></b></td>");
             body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td><b>Ward</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Name of Specimen:</b> </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Clinical History</b> </td>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;margin-left:20px;margin-right:20px;margin-top:10%;font-size:17px'>Other Investigation reports (X-ray, CT Scan, MRI, FNAC etc.)</div>");
            body.Append("<div class='row' style='color:black;margin-left:20px;margin-right:20px;margin-top:15%;font-size:17px'>Clinical Diagnosis:</div>");
            body.Append("<div class='row' style='color:black;margin-left:20px;margin-right:20px;font-size:17px;margin-top:8px'>Specimen to be sent to: </div>");
            body.Append("<table style='width:60%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px;margin-left:20px;margin-right:20px ;margin-top:8px'>");
            body.Append("<tr>");
            body.Append("<td>Histopathology</td>");
            body.Append("<td style='height:10px;width:25px; border:1px solid black'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Microbiology</td>");
            body.Append("<td style='height:10px;width:25px; border:1px solid black'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("<td>Other</td>");
            body.Append("<td style='height:10px;width:25px; border:1px solid black'></td>");
            body.Append("<td>&nbsp;</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row' style='color:black;margin-left:20px;margin-right:20px;font-size:17px;margin-top:10%'>Name and Signature of Doctor </div>");
            body.Append("</div>");
            return body.ToString();
        }
        public string RequisitionFormForTissueBiopsyForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
        StringBuilder body = new StringBuilder();
        body.Append("<div style='padding:20px;'>");
        string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
        body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");

        body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:200px; width:500px'>REQUISITION FORM FOR TISSUE BIOPSY</h3></div>");

        body.Append("<table style='width:95%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px;margin-left:20px;margin-right:20px '>");
        body.Append("<tr>");
        body.Append("<td><b>Date</b></td>");
        body.Append("<td><b></b></td>");
        body.Append("<td></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("<td><b></b> </td>");
        body.Append("<td><b></b></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("</tr>");
        body.Append("<tr>");
        body.Append("<td><b>Patient Name</b> </td>");
        body.Append("<td><b></b></td>");
        body.Append("<td>" + pname + "</td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("<td><b>Age/Gender</b></td>");
        body.Append("<td><b></b></td>");
        body.Append("<td>" + gender + "</td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("</tr>");
        body.Append("<tr>");
        body.Append("<td><b>UHID No</b> </td>");
        body.Append("<td><b></b></td>");
        body.Append("<td>" + uhidno + "</td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("<td><b>IPD No</b></td>");
        body.Append("<td><b></b></td>");
        body.Append("<td>" + ipdno + "</td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("</tr>");
        body.Append("<tr>");
        body.Append("<td><b>Surgeon Name</b> </td>");
        body.Append("<td><b></b></td>");
        body.Append("<td></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("<td><b>Ward</b></td>");
        body.Append("<td><b></b></td>");
        body.Append("<td></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("</tr>");
        body.Append("<tr>");
        body.Append("<td><b>Name of Specimen:</b> </td>");
        body.Append("<td><b></b></td>");
        body.Append("<td></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("</tr>");
        body.Append("<tr>");
        body.Append("<td><b>Clinical History</b> </td>");
        body.Append("<td><b></b></td>");
        body.Append("<td></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("</tr>");
        body.Append("</table>");
        body.Append("<div class='row' style='color:black;margin-left:20px;margin-right:20px;margin-top:15%;font-size:17px'>Other Investigation reports (X-ray, CT Scan, MRI, FNAC etc.)</div>");
        body.Append("<div class='row' style='color:black;margin-left:20px;margin-right:20px;margin-top:20%;font-size:17px'>Clinical Diagnosis:</div>");
        body.Append("<div class='row' style='color:black;margin-left:20px;margin-right:20px;font-size:17px;margin-top:5px'>Specimen to be sent to: </div>");
        body.Append("<table style='width:60%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px;margin-left:20px;margin-right:20px ;margin-top:5px'>");
        body.Append("<tr>");
        body.Append("<td>Histopathology</td>");
        body.Append("<td style='height:10px;width:25px; border:1px solid black'></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("<td>Microbiology</td>");
        body.Append("<td style='height:10px;width:25px; border:1px solid black'></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("<td>Other</td>");
        body.Append("<td style='height:10px;width:25px; border:1px solid black'></td>");
        body.Append("<td>&nbsp;</td>");
        body.Append("</tr>");
        body.Append("</table>");
        body.Append("<div class='row' style='color:black;margin-left:20px;margin-right:20px;font-size:17px;margin-top:10%'>Name and Signature of Doctor </div>");
        body.Append("</div>");
           
        return body.ToString();
        }
        public string WellsModelForPredictingDVTProbabilityForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            body.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-70px; width:455px;height:50px; padding:10px;'>");
            body.Append("<table style='width:100%;font-size:13px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px; '>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b> </td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + pname + "</td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + gender + "</td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + uhidno.ToString() + "</td>");
            body.Append("<td><b>IPD No</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<td>" + ipdno.ToString() + "</td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</h5>");
            body.Append("<div class='row'><h2 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px'>Wells Model for predicting DVT Probability</h2></div>");

            body.Append("<table style='width:95%;font-size:16px;border-collapse:collapse;border:2px solid;margin-left:30px;margin-right:30px;margin-top:-10px;'>");
            body.Append("<tr style='border: solid 2px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 2px;border-collapse: collapse;text-align:center;width:80%;'>CLINICAL FEATURES</th>");
            body.Append("<th colspan='2' style='border: solid 2px;border-collapse: collapse;text-align:center;width:30%;'>POINTS</th>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>Active cancer (Treatment ongoing, within 6 months or palliative)</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>Paralysis,Paresis or recent plaster immobilisation of lower extremity</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>Recently Bedridden ≥3 days, or undergone major surgery within 12 weeks requiring general / Regional Anaesthesia </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>Localised tenderness along the distribution of the deep venous system  </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>Entire leg swollen</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>Calf swelling at least 3cm larger than asymptomatic side</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>   Pitting edema confined to the symptomatic leg</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>Collateral superficial vein (non-varicose) </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>Previously documented DVT </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>1</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");

            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%'>An alternative diagnosis is at least as likely as DVT </ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><b>-2</b></td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;width:10%'><h5 style='height:20px;width:30px;border: solid 2px;text-align:center;margin-left:20px;margin-top:20px;'></h5></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 2px;border-collapse: collapse;'>");
            body.Append("<td colspan='3' style='border: solid 1px;border-collapse: collapse;'>TOTAL SCORE = </ td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row'><h3 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px'>Clinical probability simplified score</h3></div>");

            body.Append("<table style='width:95%;font-size:16px;border-collapse:collapse;border:2px solid;margin-left:30px;margin-right:30px;margin-top:10px;'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:80%;'>DVT Likely</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;width:30%;text-align:center'>≥2 Points</td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;'>DVT Unlikely</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'> ≥2 Points</td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:95%;font-size:18px;margin-left:30px;margin-top:10px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Doctor name:</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Doctor's signature:</b></td>");
            body.Append("</tr>");
            body.Append("<tr>");
            body.Append("<td><b>Date/Time:</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            //CH / FORM / 11B
            body.Append("<div class='row' style='color:black;margin-left:30px;font-size:11px;'><b>Source:- Wells et al. Evaluation of D-dimer in the diagnosis of suspected deep vein thrombosis, New England Journal of Medicine,2003</b> </ div>");
            body.Append("</div>");
            return body.ToString();
        }
        public string EmptyBloodBagReturnTransfusionForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div style='padding:10px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:70px;margin-top:-4%'></h3></div>");
            body.Append("<div class='row'><h2 style='color:black;text-align:center;margin-left:250px;margin-right:150px;margin-top:-8%; width:300px'>CHANDAN HOSPITAL</h2></div>");
            body.Append("<div class='row'><h2 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px'> <u>EMPTY BLOOD BAG RETURN FORM</u> </h2></div>");

            body.Append("<table style='width:100%; font-size:16px;margin-top:10px;margin-left:30px;margin-right:50px;'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>UHID No</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>IPD No.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Ward</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:90%;font-size:16px;border-collapse:collapse;border:1px solid;margin-left:30px;margin-right:30px;margin-top:10px;'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center;width:50%;'>Type of Blood Component Bag </th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center;width:50%;'>Blood bag Unit no. </th>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>PRBC/ WHOLE BLOOD </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>FFP</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>RDP </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>SDP</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>CPP</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>CPPT</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("</table>");

            body.Append("<table style='width:90%;font-size:17px;margin-left:30px;margin-right:30px;'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Blood bag send by - </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Blood bag received by - </ td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Sign -  </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Sign - </ td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>E. Code- </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>E. Code-</ td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Date & time-</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Date & time-</ td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<div class='row'><h2 style='color:black;text-align:center;margin-left:150px;margin-right:100px; width:500px'> <u>FOR BLOOD BANK USE ONLY</u></h2></div>");
            body.Append("<div class='row' style='100%; margin-left:30px;font-size:17px;'>Blood Dispatch No   ..............................</div>");

            body.Append("<table style='width:90%;font-size:16px;border-collapse:collapse;border:1px solid;margin-left:30px;margin-right:30px;margin-top:10px;'>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center;width:50%;'>Type of Blood Component Bag </th>");
            body.Append("<th style='border: solid 1px;border-collapse: collapse;text-align:center;width:50%;'>Blood bag Unit no. </th>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>PRBC/ WHOLE BLOOD </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>FFP</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>RDP </td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>SDP</td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>CPP</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("<tr style='border: solid 1px;border-collapse: collapse;'>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center;'>CPPT</ td>");
            body.Append("<td style='border: solid 1px;border-collapse: collapse;text-align:center'></td>");
            body.Append("</tr>");
            body.Append("</table>");


            body.Append("<table style='width:90%;font-size:17px;margin-left:30px;margin-right:30px;'>");
            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Blood bag received by-</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Sign -  </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td style='width:50%;text-align:left'>Date & time-  </td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("</div>");
            return body.ToString();
        }
        public string InformedConsentForBloodTranfusionForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();

            body.Append("<div style='padding:10px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:80px;margin-top:-4%'></h3></div>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:200px;margin-right:150px;margin-top:-4%; width:400px'>INFORMED CONSENT FOR BLOOD TRANSFUSION</h4></div>");

            body.Append("<div class='row' style='100%; margin-left:5px;font-size:14px;'><b>Date:_______________________</b></div>");
            body.Append("<table style='width:100%; font-size:14px;margin-top:10px'>");
            body.Append("<tr>");
            body.Append("<td><b>Patient Name</b></td>");
            body.Append("<td></td>");
            body.Append("<b><td>" + pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Age/Gender</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>DOA-</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>UHID No</ b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>IPD No.</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Consultant</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>Diagnosis</b></td>");
            body.Append("<td><b></b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            //body.Append("<div class='row' style='text-align: justify; width:100%;'><lable>__________________________________________________________________________________________</lable></div>");
            //body.Append("<div class='row' style='text-align: justify; width:100%;'><lable>__________________________________________________________________________________________</lable></div>");
            body.Append("<div class='row' style='text-align: justify; width:100%;font-size:16px;margin-top:5px'><lable>I,  ________________________________________________________hereby give my consent for whole blood transfusion/blood components as part of treatment to myself / my patient while being admitted at this Hospital. I have been explained all the known risks of transfusion reactions like fever, chills, rigors, itching etc. I have also been explained that the donor blood has been screened for HIV antibodies, Hepatitis B surface antigen, Hepatitis C antibodies, Malaria and Syphilis. I have also been explained that transfusion transmitted infections can very rarely occur even with screened blood, especially if it is in the 'window period' and also due to various other infections which have not been screened for.</lable></div>");
            body.Append("<div class='row' style='text-align: justify; width:100%;font-size:16px;margin-top:5px'>I have also been told that multiple transfusions may be required to get the desired response.</div>");
            body.Append("<div class='row' style='text-align: justify; width:100%;font-size:16px;margin-top:5px'>All the above-mentioned risks have been explained to me by the doctor treating me/ my patient in the language that I fully understand and I accept the same and give my consent for the whole blood /component transfusion to me/my Patient.</div>");
            body.Append("<table style='width:90%; font-size:16px;margin-top:5%'>");
            body.Append("<tr>");
            body.Append("<td><b>Signature of Doctor</b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Signature of Patient / Attendant</b></ td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("</table>");
            body.Append("<table style='width:90%; font-size:16px;margin-top:5%'>");
            body.Append("<tr>");
            body.Append("<td><b>Name of Doctor</b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Name of Patient / Attendant</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            body.Append("<table style='width:90%; font-size:16px;margin-top:5%'>");
            body.Append("<tr>");
            body.Append("<td><b><b>Date & Time</b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>Relation with Patient</b></td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</table>");
            body.Append("</div>");           
            return body.ToString();
        }
        public string NotifiedConsentFormforBloodTransfusionForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();
            //body.Append("<div style='padding:20px;'>");
            string imagePath = HttpContext.Server.MapPath(@"/Content/image/chandn.jpeg");
            body.Append("<div class='row'><h3><img src='" + imagePath + "' style='width:150px; height:80px;></h3></div>");
            body.Append("<div class='row'><h4 style='color:black;text-align:center;margin-left:200px;margin-right:150px;margin-top:-3%; width:400px'>रक्त आधान (ट्रांसफ्यूजन) के लिए अभिसूचित सहमतिप्रपत्र</h4></div>");

            body.Append("<div class='row' style='100%; margin-left:5px;font-size:14px'><b>दिनांक:_______________________</b></div>");
            body.Append("<table style='width:100%; font-size:14px;margin-top:10px'>");
            body.Append("<tr>");
            body.Append("<td><b>रोगी का नाम</b></td>");
            body.Append("<td>:</td>");
            body.Append("<b><td>" + pname + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>आयु/लिंग</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + gender + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>यूएचआईडीसंख्या</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + uhidno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>आईपी संख्या</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + ipdno + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>भर्ती होने का तिथि</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + admitdate + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>परामर्शदाता चिकित्सक</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + doctor + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b>निदान</b></td>");
            body.Append("<td><b>:</b></td>");
            body.Append("<b><td>" + Diagnosis + "</td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            //body.Append("<div class='row' style='text-align: justify; width:100%;'><lable>__________________________________________________________________________________________</lable></div>");
            //body.Append("<div class='row' style='text-align: justify; width:100%;'><lable>__________________________________________________________________________________________</lable></div>");
            body.Append("<div class='row' style='text-align: justify; width:100%;font-size:16px;margin-top:5px'><lable> मैं/मेरा रोगी  _______________________________________________________________________________</lable></div>");
            body.Append("<div class='row' style='text-align: justify; width:100%;font-size:16px;margin-top:5px'>जो इस चिकित्सालय में भर्ती हैं, चिकित्सा के एक आवश्यक अंग के रूप में, पूर्ण रक्त आधान (ट्रांसफयूजन), रक्त काम्पोनेंट के सभी परिचित जोखिमों जैसे बुखार, ठंड लगना, अकडल, शरीर पर लाल धब्बे पड़ना, एलर्जिक प्रतिक्रिया के बारे में मुझे बता दिया गया है। मुझे यह भी बता दिया गया है कि किसी दाता द्वारा दिया गया रक्त एचआईवी एंटीबांडी, हेपेटाइटिस बी, सतह प्रतिजन (सरफेस एंटीबॉडी) हेपेटाइटिस सी एंटीबॉडी, मलेरिया, सिफलिस के लिए जांच किया जा चुका होता है। मुझे यह भी स्पष्ट किया गया है कि बहुत कम परिस्थितियों में ट्रांसफयूजन से संक्रमण हो सकता है, विशेषकर जब वह 'विंडो पीरियड'में हों तथा विभिन्न दूसरे संक्रमण जिसको जांचा नहीं जा सकता हों मुझे यह भी स्पष्टकिया जा चुका है कि ऐच्छित परिणाम प्राप्त करने के लिए एक से अधिक बार भी ट्रांसफ्यूजन करना पड़ सकता है ।</div>");
            body.Append("<div class='row' style='text-align: justify; width:100%;font-size:16px;margin-top:5px'>जो चिकित्सक मेरा मेरे रोगी का इलाज कर रहे हैं, उनके द्वारा मेरी अपनी भाषा में मुझे रक्त आधान (ट्रांसफ्यूजन) के लार्भा, सभी जोखिमों, सम्बंधित जटिलताओं को बता दिया गया है। मैं पूरी तरह से समझ गया हूँ और उनको स्वीकार करता हूँ तथा सम्पूर्ण रक्त / कॉम्पोनेंट ट्रान्सफ्यूजन जो मेरा मेरे रोगी का होना है, उसकी सहमति देता देती हूँ । </div>");
            body.Append("<table style='width:80%; font-size:16px;margin-top:10px'>");
            body.Append("<tr>");
            body.Append("<td>चिकित्सक के हस्ताक्षरः</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>रोगी के हस्ताक्षरः </td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td>चिकित्सक का नामः</td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>रोगी का नामः</td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>साक्षी / सरोगेट के हस्ताक्षरः</td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>साक्षी / सरोगेट का नाम:</td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");

            body.Append("<tr>");
            body.Append("<td><b></b></td>");
            body.Append("<td></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("<td>दिनांक/समयः-</td>");
            body.Append("<b><td></td></b>");
            body.Append("<td><b>&nbsp;</b></td>");
            body.Append("</tr>");
            body.Append("</table>");
            return body.ToString();
        }

    }
}