using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace NoteWriter
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public List<wsNoteWriterItem> getAllUserItems()
        {
            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                List<wsNoteWriterItem> results = new List<wsNoteWriterItem>();
                foreach (NoteWriterItem item in dc.NoteWriterItems)
                {
                    results.Add(new wsNoteWriterItem()
                    {
                        numRow = item.numRow,
                        usr = item.usr,
                        cat = item.cat,
                        subcat = item.subcat,
                        item = item.item,
                        dialog = item.dialog
                    });
                }
                return results;
            }
            catch (Exception ex)
            {
                // Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public List<wsNoteWriterItem> getUserItems(string sUserId)
        {
            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                List<wsNoteWriterItem> results = new List<wsNoteWriterItem>();
                foreach (NoteWriterItem item in dc.NoteWriterItems.Where(s => s.usr == sUserId))
                {
                    results.Add(new wsNoteWriterItem()
                    {
                        numRow = item.numRow,
                        usr = item.usr,
                        cat = item.cat,
                        subcat = item.subcat,
                        item = item.item,
                        dialog = item.dialog
                    });
                }
                return results;
            }
            catch (Exception ex)
            {
                // Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsSQLResult deleteUserItems(string sUserId)
        {
            wsSQLResult result = new wsSQLResult();

            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                List<wsNoteWriterItem> results = new List<wsNoteWriterItem>();
                foreach (NoteWriterItem item in dc.NoteWriterItems.Where(s => s.usr == sUserId))
                {
                    dc.NoteWriterItems.DeleteOnSubmit(item);
                    dc.SubmitChanges();
                }

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;     // Success !
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsSQLResult addUserItems(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // ..then convert the string into a single "wsCustomer" record.
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var root = jss.Deserialize<List<wsNoteWriterItem>>(JSONdata);
                if (root == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsCustomer" object.
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }

                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                foreach (var item in root)
                {
                    NoteWriterItem newCustomer = new NoteWriterItem()
                    {
                        usr = item.usr,
                        cat = item.cat,
                        subcat = item.subcat,
                        item = item.item,
                        dialog = item.dialog
                    };

                    dc.NoteWriterItems.InsertOnSubmit(newCustomer);
                    dc.SubmitChanges();
                }

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;
            }
            catch (Exception ex)
            {
                result.WasSuccessful = 0;
                result.Exception = ex.Message;
                return result;
            }
        }

        public List<wsNoteWriterItem> getUserTipCategories(string sUserId)
        {
            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                List<wsNoteWriterItem> results = new List<wsNoteWriterItem>();
                foreach (NoteWriterTip item in dc.NoteWriterTips.Where(s => s.usr == sUserId).GroupBy(s=> s.cat).Select(s => s.OrderBy(i => i.cat).First()))
                {
                    results.Add(new wsNoteWriterItem()
                    {
                        cat = item.cat,
                    });
                }
                return results;
            }
            catch (Exception ex)
            {
                // Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public List<wsNoteWriterItem> getUserTipSubcategories(string sUserId, string sCat)
        {
            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                List<wsNoteWriterItem> results = new List<wsNoteWriterItem>();
                foreach (NoteWriterTip item in dc.NoteWriterTips.Where(s => s.usr == sUserId && s.cat == sCat).GroupBy(s => s.cat).Select(s => s.OrderBy(i => i.cat).First()))
                {
                    results.Add(new wsNoteWriterItem()
                    {
                        subcat = item.subcat,
                    });
                }
                return results;
            }
            catch (Exception ex)
            {
                // Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public List<wsNoteWriterItem> getUserTips(string sUserId)
        {
            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                List<wsNoteWriterItem> results = new List<wsNoteWriterItem>();
                foreach (NoteWriterTip item in dc.NoteWriterTips.Where(s => s.usr == sUserId))
                {
                    results.Add(new wsNoteWriterItem()
                    {
                        numRow = item.numRow,
                        usr = item.usr,
                        cat = item.cat,
                        subcat = item.subcat,
                        item = item.item,
                        dialog = item.dialog
                    });
                }
                return results;
            }
            catch (Exception ex)
            {
                // Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsSQLResult deleteUserTips(string sUserId)
        {
            wsSQLResult result = new wsSQLResult();

            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                List<wsNoteWriterTip> results = new List<wsNoteWriterTip>();
                foreach (NoteWriterItem item in dc.NoteWriterItems.Where(s => s.usr == sUserId))
                {
                    dc.NoteWriterItems.DeleteOnSubmit(item);
                    dc.SubmitChanges();
                }

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;     // Success !
            }
            catch (Exception ex)
            {
                //  Return any exception messages back to the Response header
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message.Replace("\r\n", "");
                return null;
            }
        }

        public wsSQLResult addUserTips(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // ..then convert the string into a single "wsCustomer" record.
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var root = jss.Deserialize<List<wsNoteWriterItem>>(JSONdata);
                if (root == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsCustomer" object.
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }

                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                foreach (var item in root)
                {
                    NoteWriterTip newCustomer = new NoteWriterTip()
                    {
                        usr = item.usr,
                        cat = item.cat,
                        subcat = item.subcat,
                        item = item.item,
                        dialog = item.dialog
                    };

                    dc.NoteWriterTips.InsertOnSubmit(newCustomer);
                    dc.SubmitChanges();
                }

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;
            }
            catch (Exception ex)
            {
                result.WasSuccessful = 0;
                result.Exception = ex.Message;
                return result;
            }
        }

        public wsSQLResult createTestDbItem(Stream JSONdataStream)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                // Read in our Stream into a string...
                StreamReader reader = new StreamReader(JSONdataStream);
                string JSONdata = reader.ReadToEnd();

                // ..then convert the string into a single "wsCustomer" record.
                JavaScriptSerializer jss = new JavaScriptSerializer();
                wsNoteWriterItem item = jss.Deserialize<wsNoteWriterItem>(JSONdata);
                if (item == null)
                {
                    // Error: Couldn't deserialize our JSON string into a "wsCustomer" object.
                    result.WasSuccessful = 0;
                    result.Exception = "Unable to deserialize the JSON data.";
                    return result;
                }

                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                NoteWriterItem newCustomer = new NoteWriterItem()
                {
                    usr = item.usr,
                    cat = item.cat,
                    subcat = item.subcat,
                    item = item.item,
                    dialog = item.dialog
                };

                dc.NoteWriterItems.InsertOnSubmit(newCustomer);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;
            }
            catch (Exception ex)
            {
                result.WasSuccessful = 0;
                result.Exception = ex.Message;
                return result;
            }
        }

        public wsSQLResult deleteTestDbItem(string TestDbItemID)
        {
            wsSQLResult result = new wsSQLResult();
            try
            {
                narfdaddy2DataContext dc = new narfdaddy2DataContext();
                NoteWriterItem item = dc.NoteWriterItems.Where(s => s.numRow == Int32.Parse(TestDbItemID)).FirstOrDefault();
                if (item == null)
                {
                    // We couldn't find a [Customer] record with this ID.
                    result.WasSuccessful = -3;
                    result.Exception = "Could not find a [ListBuilder1] record with ID: " + TestDbItemID.ToString();
                    return result;
                }

                dc.NoteWriterItems.DeleteOnSubmit(item);
                dc.SubmitChanges();

                result.WasSuccessful = 1;
                result.Exception = "";
                return result;     // Success !
            }
            catch (Exception ex)
            {
                result.WasSuccessful = -1;
                result.Exception = "An exception occurred: " + ex.Message;
                return result;     // Failed.
            }
        }
    }
}
