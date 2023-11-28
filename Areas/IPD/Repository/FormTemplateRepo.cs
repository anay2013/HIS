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
            return TemplateContent;
        }
        // duplicate pattern
        public string AdmissionFormContent(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
           
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");

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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");

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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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

            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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

            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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

            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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

     
            string imageePath = System.Web.HttpContext.Current.Server.MapPath(@"/Content/logo/download.png");

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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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

            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            body.Append("<div class='row' style='text-align: justify'><p><center><b>Discharge summary has been expained to me in details in my language,<br>Patient/Attendant name & Signature </b></center></p></div>");
            body.Append("<div class='row' style='text-align: justify'><p><center><h3 style='height:60px;width:200px; border:1px solid black'></h3></center></p></div>");
            body.Append("</div>");
           
            return body.ToString();
        }

        //BLOOD PRINT FROM
        public string BloodRequisitionForm(string pname, string uhidno, string gender, string admitdate, string ipdno, string doctor, string Diagnosis, string PageIndex, string PageName, string PageOrientation)
        {
            StringBuilder body = new StringBuilder();

            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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

            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
       
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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
            
            string imagePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/logo/ChandanLogo.jpg");
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

    }
}