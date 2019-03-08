using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public static class Constants
    {
        public const string BASE_URL = "https://clickncheck.azurewebsites.net/api/"; /* alternative url for testing on local db https://localhost:44347/api/ */
        public const string IS_LOGGED_IN = "authentication/isLoggedIn";
        public const string ADD_USER = "Users/PostUsers";
        public const string GET_USER_TYPES = "Users/userTypes";
        public const string REGISTER_USER = "users/register";
        public const string CANDIDATE_VERIFICATION = "candidate/sendVerification";
        public const string GET_CANDIDATE = "Candidates/GetCandidate/";
        public const string GET_JOB_PROFILE_CHECKS = "JobProfiles/jobChecks/";
        public const string CANDIDATE_CONSENT = "Candidates/PutConsent/";
        public const string RECRUITER_MANAGERS = "users/recruiter/organisation/managers/";
        public const string OTP_AUTHENTICATION = "authentication/otp";
        public const string CREATE_CANDIDATE = "Candidates/CreateCandidate/";
        public const string RECRUITER_JOB_PROFILES = "JobProfiles/recruiterJobs/";
        public const string GET_ALL_JOB_PROFILE_CHECKS = "JobProfiles/getAllChecks";
        public const string AUTHENTICATE_LOGIN = "authentication/login";
        public const string CHECK_OTP = "authentication/checkOtp";
    }
}
