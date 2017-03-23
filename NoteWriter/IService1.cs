using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NoteWriter
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getUserItems/{sUserId}")]
        List<wsNoteWriterItem> getUserItems(string sUserId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "deleteUserItems/{sUserId}")]
        wsSQLResult deleteUserItems(string sUserId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "addUserItems")]
        wsSQLResult addUserItems(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getUserTips/{sUserId}")]
        List<wsNoteWriterItem> getUserTips(string sUserId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "deleteUserTips/{sUserId}")]
        wsSQLResult deleteUserTips(string sUserId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getUserTipCategories/{sUserId}")]
        List<wsNoteWriterItem> getUserTipCategories(string sUserId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getUserTipSubcategories/{sUserId}/{sCat}")]
        List<wsNoteWriterItem> getUserTipSubcategories(string sUserId, string sCat);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "addUserTips")]
        wsSQLResult addUserTips(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getAllUserItems")]
        List<wsNoteWriterItem> getAllUserItems();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "createTestDbItem")]
        wsSQLResult createTestDbItem(Stream JSONdataStream);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "deleteTestDbItem/{TestDbItemID}")]
        wsSQLResult deleteTestDbItem(string TestDbItemID);
    }
}
