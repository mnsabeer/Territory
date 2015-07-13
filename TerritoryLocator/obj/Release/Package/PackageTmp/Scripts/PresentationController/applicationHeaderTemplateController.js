TerritoryLocatorController.controllerProvider.register('applicationHeaderTemplateController', function ($scope, communicationService, $rootScope, popUpService) {

    init();

    function setURLs(isLoggedIn) {
        $scope.FeaturesUrl = featuresUrl;
        $scope.TransitionUrl = transitionUrl;
        $scope.DashboardUrl = indexUrl;
        $scope.LogInUrl = loginUrl;
        $scope.SampleUrl = sampleUrl;
        $scope.AdminUrl = adminhomeUrl;
        $scope.ContactUsUrl = contactUsUrl;
        if (!isLoggedIn) {
            $scope.SubscribeUrl = subscribeUrl;
        }
        else {
            $scope.SubscribeUrl = renewUrl;
        }
    }

    function init() {
        //var loggedInUser = STDBWebNewPortalSession.CurrentUser;
        if (currentUser != null || currentUser != undefined) {
            $scope.IsLoggedIn = currentUser.IsLoggedIn;
            $scope.IsCCMI = currentUser.IsCCMIUSer;
            $scope.IsAdmin = currentUser.IsAdmin;
            $rootScope.$broadcast('UserDataReceivedEvent', currentUser);
            setURLs($scope.IsLoggedIn);
        }
        else {
            $scope.IsLoggedIn = false;
            setURLs(false);
        }
        
        setActiveMenu();


    };


    //method for building the model for the view 
    function setActiveMenu() {

        switch (getLocation()) {
            case verify:
                $scope.isVerify = "active";
                break;
            case transition:
                $scope.isTransition = "active";
                break;
            case subscribeAI:
                $scope.isSubscribeAI = "active";
                break;
            case subscribe:
                $scope.isSubscribe = "active";
                break;
            case sample:
                $scope.isSample = "active";
                break;
            case index:
                $scope.isIndex = "active";
                break;
            case group:
                $scope.isGroup = "active";
                break;
            case features:
                $scope.isFeatures = "active";
                break;
            case contactUs:
                $scope.isContactUs = "active";
                break;
            case error:
                $scope.isError = "active";
                break;
            case details:
                $scope.isDetails = "active";
                break;
            case contactUs:
                $scope.isContactUs = "active";
                break;
            case aboutUs:
                $scope.isAboutUs = "active";
                break;
            case adminhome:
                $scope.isAdmin = "active";
                break;
            case support:
                $scope.isSupport = "active";
        };
    }


    //click event of login 
    $scope.DashBoardClick = function (url) {

        //var user = STDBWebNewPortalSession.CurrentUser;

        if (currentUser != null || currentUser != undefined) {
            if (currentUser.IsExpired || currentUser.MemberShip < 1) {
                window.location.href = getBaseURL() + $scope.SubscribeUrl;
            }
            else {
                if (url == $scope.AdminUrl) {

                    window.location.href = getBaseURL() + $scope.AdminUrl;
                    //  showMessage("Provide the admin url");
                }
                else
                    window.location.href = getBaseURL() + url;

            }
        }
        else {
            // user will be null , if not logged in
            window.location.href = getBaseURL() + url;
        }
    }

    //click event of login 
    $scope.SignOutClick = function () {

        communicationService.makeWebRequest(homeApiValidateLogOutUser, "POST", [], onLogOutComplete, null, true);
    }

    //click event of login 
    $scope.MenuClick = function (url) {

        window.location.href = getBaseURL() + url;
    };

    //success callback for logout complete
    function onLogOutComplete(data) {

        //window.location.href = "http://beta.stdb.com/Shibboleth.sso/Logout?return=https://idp.xceligent.com/idp/profile/Logout?return=http://beta.stdb.com/STDBHome";
        window.location.href = "https://www.stdb.com/Shibboleth.sso/Logout?return=https://idp.xceligent.com/idp/profile/Logout?return=https://www.stdb.com/STDBHome";
    };




});