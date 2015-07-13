/// <reference path="../../riskmeterRedirection.html" />
/// <reference path="../../riskmeterRedirection.html" />
/// <reference path="../../riskmeterRedirection.html" />
TerritoryLocatorController.controllerProvider.register('indexTemplateController', function ($scope, $rootScope, communicationService, popUpService) {

    $scope.hasPlan = false;
    $scope.newUser = true;
    $scope.planExpired = false;
    $scope.notPersonifyUser = true;
    init();

    

    function init() {
    
 
        if (currentUser == null || currentUser == undefined) {
            $scope.IsLoggedIn = false;
            $scope.newUser = true;
            $scope.planExpired = false;
        }
        else {
           
            if (currentUser.IsLoggedIn)
            {
                fetchAlerts();
            }
            

            setScope(currentUser);
            setUserDetails(currentUser);
          
        }
      
        $rootScope.$on('UserDataReceivedEvent', function (event, data) {

            onUserDataReceived(data);
        });
    };

    function onUserDataReceived(user) {
        if (user != null || user != undefined) {

            setScope(user);
        }
    };


    //click event of login 
    $scope.Verify = function () {

        communicationService.makeWebRequest(homeapiVerify, "POST", [], onVerificationComplete, null, true);

    }

    function onVerificationComplete(data) {

        if (data.Status === true) {
            window.location.href = esiUrl;
        }
    };

    function setScope(user) {

        if (user != null || user != undefined) {

            if (user.IsExpired || user.MemberShip < 1) {
                window.location.href = getBaseURL() + subscribeUrl;
            }

            $scope.IsLoggedIn = user.IsLoggedIn;
            $scope.IsCCMI = user.IsCCMIUSer;
            $scope.MemberLevel = user.MemberShip;

            setUserDetails(user);
        }
    };

    function setUserDetails(user) {

        $scope.newUser = false;
        $scope.planExpired = false;

        if (user.FirstName == null || user.LastName == null) {
            $scope.CustomerName = "";
        }
        else {
            $scope.hasPlan = true;
            $scope.notPersonifyUser = true;
            $scope.CustomerName = user.FirstName + " " + user.LastName;

            if (currentUser.ExpiryDate != null)
                $scope.ExpiryDate = user.ExpiryDate.replace(/[+]/g, ' ');
            else
                $scope.hasPlan = false;




            if (user.MemberShip == 100)
                $scope.notPersonifyUser = false;

        }

        if (user.IsExpired) {

            if ((user.MemberShip > 0))
                $scope.planExpired = true;
        }
        else {
            $scope.planExpired = false;
        }

    };

    $scope.showWhatIsTerritorypopUp = function () {

        var objTemplate = new Object;
        objTemplate.Name = AboutTerritoryTemplate;
        objTemplate.Area = root;

        popUpService.showPopUp(objTemplate, []);

    };

    $scope.openQuickDemo = function () {


  
        alert('Coming Soon!');


    }
    $scope.openRiskMeter = function () {
        var href = getBaseURL() + riskmeterUrl;
        window.location.href = href; // To load in same page.
        // New riskmeter view (From Constant) End


        //Old Riskmeter Integration--(Corelogic) start
        //Redirection riskmeter. To start the other working, comment starts from here
        //var href = getBaseURL() + "/riskmeterRedirection";
        //window.open(href, "_blank"); // To load in a new tab    
        //Old Riskmeter Integration--(Corelogic) end

    }
    $scope.openPictometry = function () {
       
        var pathUrl = getBaseURL() + pictometryUrl;

        window.location.href = pathUrl;
    }
    $scope.detectIE = function () {
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf('MSIE ');
        var trident = ua.indexOf('Trident/');
        var retVal = false;
        if (msie > 0) {
            // IE 10 or older => return version number
            //return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
            retVal = true;
        }

        if (trident > 0) {
            // IE 11 (or newer) => return version number
            var rv = ua.indexOf('rv:');
            //return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
            retVal = true;
        }

        if (retVal)
            alert('We are currently experiencing an issue with Internet Explorer and Business Analyst Online. BAO will work on Chrome, FireFox or Safari.');
        else
            window.location.href = getBaseURL() + "/Home/Verify";
    }
    $scope.readMore = function (id) {

        angular.forEach($scope.alertList, function (value, key) {
            if (value.ID == id) {
                value.isFullText = true;
            }
        });
    }

    $scope.readLess = function (id) {

        angular.forEach($scope.alertList, function (value, key) {
            if (value.ID == id) {
                value.isFullText = false;
            }
        });
    }
    function fetchAlerts() {
        var retrievalParams = new Object;
        communicationService.makeWebRequest(homeapiGetAlertList, "GET", [], onFetchAlertComplete, null, true);

    }
    function onFetchAlertComplete(data) {
      
        if (data != null  || data!=undefined) {
            $scope.alertList = [];
           if (data.Alert.length > 0) {
                angular.forEach(data.Alert, function (value, key) {
                    if (value.IsPublished) {
                        $scope.alertList.push({ ID: value.ID, Header: value.Header, AlertText: value.AlertText, isFullText: false });

                    }
                });
            }
        }
    }
});