(function () {

    // Default pagination template index.
    paginationDefaultIndex = 1;

    // Default pagination template page size.
    paginationDefaultPageSize = 100;


    // Default pagination template maximum page count.
    paginationTemplateDefaultMaxPageCount = 10;

    //Date Format String which defines the format of the date across application
    dateFormatString = 'dd-M-yy';

    IndexTemplateName = "indexTemplate";
    HeaderTemplateName = "applicationHeaderTemplate";
    CompareSubscriptionTemplate = "compareSubscriptionTemplate";
    SubscribeTemplateName = "subscribeTemplate";
    SubscribeBuyNowTemplateName = "subscribeBuyNowTemplate";
    TermsOfServiceTemplate = "termsOfServiceTemplate";
    AboutSTDBTemplate = "aboutSTDBTemplate";
    SamplePopupTemplateName = "samplePopupTemplate";
    SampleTemplateName = "sampleTemplate";
    AdminHomeTemplateName = "adminHomeTemplate";
    CustomerDetailsPopUpTemplateName = "customerDetailsPopUpTemplate";
    AdminEditCustomerDetailsTemplateName = "adminEditCustomerDetailsTemplate";
    ResetCustomerPasswordTemplateName = "resetCustomerPasswordTemplate";
    AdminAddCustomerTemplateName = "adminAddCustomerTemplate";
    AdminAddAlertTemplateName = "adminAddAlertTemplate";
    RiskMeterTemplateName = "riskMeterTemplate";
    PictometryTemplateName = "pictometryTemplate";
    DataBaseUSATemplateName = "dataBaseUSATemplate";
    SampleDemoTemplateName = "sampleDemoTemplate";
    UserLogDetailsPopupTemplateName = "userLogDetailsPopupTemplate";
    DevelopmentDocumentTemplateName = "developmentDocumentTemplate";
    root = "/";
    esiUrl = "http://bao.arcgis.com/?portal=ccim.maps.arcgis.com&cacheBust=1";

    SamplePopupBaseAddress = root + "Content/images/samplespage/";
    homeApiBaseUrl = "/api/HomeApi/";
    homeApiValidateLoggedOnUser = homeApiBaseUrl + "LogOn";
    homeApiValidateLogOutUser = homeApiBaseUrl + "LogOut";
    homeApiAddCustomerBillingInfo = homeApiBaseUrl + "AddCustomerBillingInfo";
    homeApiUpdateCustomerProfileInfo = homeApiBaseUrl + "UpdateCustomerPersonalInfo";
    homeApiPaymentCheckOut = homeApiBaseUrl + "MakePaymentAfterCheckout";
    homeApiValidateCustomerUsingPersonify = homeApiBaseUrl + "ValidateCustomerUsingPersonify?userId=";
    homeApiGetPersonalInfo = homeApiBaseUrl + "GetPersonalInfo";
    homeapiGetPromoCode = homeApiBaseUrl + "GetPromoCode?promoCode=";
    homeapiVerify = homeApiBaseUrl + "Verify";    
    homeapiGetCustomers = homeApiBaseUrl + "GetCustomers";
    homeapiGetCustomerDetails = homeApiBaseUrl + "GetCustomerDetails?customerId=";
    homeapiGetMemberClass = homeApiBaseUrl + "GetMemberClass";
    homeapiGetUpdateCustomer = homeApiBaseUrl + "UpdateCustomer";
    homeapiGetValidateEmail = homeApiBaseUrl + "ValidateEmail";
    homeapiGetResetPassword = homeApiBaseUrl + "ResetPassword";
    homeapiAddCustomerDetails = homeApiBaseUrl + "AddCustomerInformation";
    homeapiAddCustomer = homeApiBaseUrl + "AddCustomer";
    homeapiGetAlerts = homeApiBaseUrl + "GetAlerts";
    homeapiAddAlert = homeApiBaseUrl + "AddAlert";
    homeapiGetAlertList = homeApiBaseUrl + "GetAlertList";
    homeapiDeleteAlert = homeApiBaseUrl + "DeleteAlert";
    homeapiEditAlert = homeApiBaseUrl + "EditAlert";
    homeapiToPublishAlerts = homeApiBaseUrl + "PublishAlerts";
    homeapiToUnPublishAlerts = homeApiBaseUrl + "UnPublishAlerts";
    homeapiGetRiskMeterData = homeApiBaseUrl + "GetRiskMeterData";
    homeapiGetRiskMeterMap = homeApiBaseUrl + "GetRiskMeterMap";
    homeapiGetPictometryData = homeApiBaseUrl + "GetPictometryData";
    homeapiLogUserActions = homeApiBaseUrl + "LogUserActivity";
    homeapiOpenOutlook = homeApiBaseUrl + "OpenOutlook";
    homeapiGetUserLog = homeApiBaseUrl + "GetUserLog";
    homeapiGetUserLogDetailsById = homeApiBaseUrl + "GetUserLogDetailsById?id=";
    homeapiGetDataBaseUSAData = homeApiBaseUrl + "GetDataBaseUSAData";
    homeapiGetDataBaseUSASICData = homeApiBaseUrl + "GetDataBaseUsaSIC";    
    homeapiDownLoadRiskMeterPdf = homeApiBaseUrl + "DownLoadRiskMeterPdf";    
    homeapiSendEmailRiskMeterPdf = homeApiBaseUrl + "SendEmailRiskMeterPdf";
    homeapiGetDatabaseUSASICSearchFromApi = homeApiBaseUrl + "GetDataBaseUsaApiData";    
    homeapiDownLoadListView = homeApiBaseUrl + "DownLoadListView";

    lite = 7;
    professional = 51;
    platinum = 60;
    liteUnitPrice = 0;// Set at runtime from webconfig
    professionalUnitPrice = 0;// Set at runtime from webconfig
    platinumUnitPrice = 0;// Set at runtime from webconfig

    verify = "Verify";
    transition = "Transition";
    subscribeAI = "SubscribeAI";
    subscribe = "Join";
    sample = "Sample";
    index = "Index";
    group = "Group";
    features = "Features";
    error = "Error";
    details = "Details";
    contactUs = "ContactUs";
    aboutUs = "AboutUs";
    adminhome = "Admin";
    riskmeter = "RiskMeter";
    pictometry = "Pictometry";
    support = "Support";
    renew = "Renew";
    dataBaseUSA = "DataBaseUSA";
    developmentDocument = "DevelopmentDocument";

    Validation_EmailPattern = /^[_a-z0-9-A-Z]+(\.[_a-z0-9-]+)*@[a-z0-9-A-Z]+(\.[a-z0-9-]+)*(\.[a-z]{2,15})$/;
    Validation_NumberPattern = /^([1-9][0-9]*)*$/;
    Validation_FloatingNumberPattern = /^\-?\d*((\.)\d+)?$/;
    Validation_PostalCodePattern = /[0-9]+/;


    transitionUrl = root + "Transition";
    subscribeAIUrl = root + "SubscribeAI";
    subscribeUrl = root + "Join";
    sampleUrl = root + "Sample";
    indexUrl = root + "Index";
    groupUrl = root + "Group";
    featuresUrl = root + "Features";
    errorUrl = root + "Error";
    detailsUrl = root + "Details";
    contactUsUrl = root + "ContactUs";
    aboutUsUrl = root + "AboutUs";
    adminhomeUrl = root + "AdminHome";
    riskmeterUrl = root + "RiskMeter";
    pictometryUrl = root + "Pictometry";
    supportUrl = root + "Support";
    renewUrl = root + "Renew";
    loginUrl = root + "Home/Login";

    fusionGoogleApi = "AIzaSyDbo_wi_KMjt0yqgf2hQWeuEtEYEVBoIX8";
    fusionZipcodeUrl = "1-bR6mmFAmigJnuU_VzC41-h22iLbcLYHh6rciuN4";
    fusionStateUrl = "1aNgcpZ8n-l29cF5OH-r0DDFLsjY2nZqSFX965JQK";
    fusionCountyUrl = "1xLXpLCNwPIU5F-Q2G3M6aWnDAtRozb_p5lzYjb5-";
    fusionCityUrl = "152_WC_i01FXR_vklCCYZ6yFWEtKzs4qN-0bHXjM";

    xmlDocumentdatabaseUSAUrl = "DevelopmentDocuments/dataBaseUSA.xml";
    xmlDocumentriskMeterUrl = "DevelopmentDocuments/RiskMeter.xml";
    xmlDocumentpictometryUrl = "DevelopmentDocuments/Pictometry.xml";
    xslDocumentUrl = "DevelopmentDocuments/DocumentXSLTnewStyle.xslt";
    
})();