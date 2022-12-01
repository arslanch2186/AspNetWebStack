using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Models.Consts
{
    public class ConstHelper
    {
        public static string JWT_KEY = "";

        public static int ACCCESS_TOKEN_EXPIRY = 0;
        public static int REFRESH_TOKEN_EXPIRY = 0;


        public const string RESPONSE_SUCCESS = "SUCCESS";
        public const string RESPONSE_ERROR = "ERROR";
        public const string ERROR_MESSAGE = "Something went wrong. Please try again in a while.";
        public const string GET_SUCCESS_MESSAGE = " retrieved successfully";
        public const string CREATE_SUCCESS_MESSAGE = " created successfully";
        public const string UPDATE_SUCCESS_MESSAGE = " updated successfully";
        public const string DELETE_SUCCESS_MESSAGE = " deleted successfully";
        public const string USER_PROFILE_ENTITY_NAME = "User profile";
        public const string CITY_ENTITY_NAME = "City";
        public const string COUNTRY_ENTITY_NAME = "Country";
        public const string AD_POSTING_ENTITY_NAME = "Ad post";
    }
}
