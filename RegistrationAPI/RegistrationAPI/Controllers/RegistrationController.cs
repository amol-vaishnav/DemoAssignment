using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using RegistrationAPI.Models;
using RegistrationAPI.Services;

namespace RegistrationAPI.Controllers
{
    public class RegistrationController : ApiController
    {
        public HttpResponseMessage RegisterUser()
        {
            HttpResponseMessage objHttpResponseMessage = null;
            RegistrationModel objRegistrationModel = new RegistrationModel();
            Byte[] imageByteArray = null;
            try
            {
                var httpRequest = HttpContext.Current.Request;
                System.Collections.Specialized.NameValueCollection objFormData = httpRequest.Form;

                objRegistrationModel.Address = objFormData["address"];
                objRegistrationModel.FirstName = objFormData["firstName"];
                objRegistrationModel.LastName = objFormData["lastName"];
                objRegistrationModel.Password = objFormData["password"];
                objRegistrationModel.UserName = objFormData["username"];

                //TODO: To assign the image file to model property
                if (httpRequest.Files.Count > 0)
                {
                    var profilePhoto = httpRequest.Files[0];

                    int fileLength = profilePhoto.ContentLength;
                    imageByteArray = new Byte[fileLength];
                    System.IO.Stream inputStream;
                    inputStream = profilePhoto.InputStream;
                    inputStream.Read(imageByteArray, 0, fileLength);


                }
                objRegistrationModel.ProfilePhoto = imageByteArray;
                //if ((objRegistrationModel.ProfilePhoto)==null)
                //{
                //    objRegistrationModel.ProfilePhoto = DBNull.Value;            
                //}

                if (RegistrationService.RegisterUser(objRegistrationModel) > 0)
                    objHttpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                else
                    objHttpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           return objHttpResponseMessage;

        }
    }
}